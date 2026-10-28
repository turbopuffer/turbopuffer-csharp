using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Turbopuffer.Core;
using Turbopuffer.Exceptions;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

// Stands in for the generated params tests excluded in the test project, which don't compile
// against the custom types. Params are built from wire JSON and their properties found by
// reflection, so nested types like consistency, limit and sub-queries are covered without naming
// generated types that spec changes rename or restructure.
public class ParamsWireFormatTest : TestBase
{
    const string QueryBody = """
        {
            "aggregate_by": {"count": ["Count"]},
            "compute_attributes": {"foo": "bar"},
            "consistency": {"level": "eventual", "max_staleness_seconds": 1},
            "distance_metric": "cosine_distance",
            "exclude_attributes": ["string"],
            "filters": ["attr", "Eq", "value"],
            "group_by": ["attr"],
            "include_attributes": true,
            "limit": {"total": 0, "per": {"attributes": ["string"], "limit": 0}},
            "offset": 0,
            "rank_by": ["id", "asc"],
            "rerank_by": {"attribute": "attribute", "model": "model", "query": "query"},
            "top_k": 0,
            "vector_encoding": "float"
        }
        """;

    const string QueryBodyAlternateVariants = """
        {
            "consistency": {"level": "strong"},
            "include_attributes": ["attr"],
            "limit": 10
        }
        """;

    const string ExplainQueryBody = """
        {
            "aggregate_by": {"count": ["Count"]},
            "compute_attributes": {"foo": "bar"},
            "consistency": {"level": "eventual", "max_staleness_seconds": 1},
            "distance_metric": "cosine_distance",
            "exclude_attributes": ["string"],
            "filters": ["attr", "Eq", "value"],
            "group_by": ["attr"],
            "include_attributes": true,
            "limit": {"total": 0, "per": {"attributes": ["string"], "limit": 0}},
            "offset": 0,
            "rank_by": ["id", "asc"],
            "top_k": 0,
            "vector_encoding": "float"
        }
        """;

    const string MultiQueryBody = """
        {
            "queries": [
                {
                    "aggregate_by": {"count": ["Count"]},
                    "compute_attributes": {"foo": "bar"},
                    "distance_metric": "cosine_distance",
                    "exclude_attributes": ["string"],
                    "filters": ["attr", "Eq", "value"],
                    "group_by": ["attr"],
                    "include_attributes": true,
                    "limit": {"total": 0, "per": {"attributes": ["string"], "limit": 0}},
                    "offset": 0,
                    "rank_by": ["id", "asc"],
                    "top_k": 0
                }
            ],
            "consistency": {"level": "eventual", "max_staleness_seconds": 1},
            "limit": {"total": 0},
            "offset": 0,
            "rerank_by": ["RRF"],
            "vector_encoding": "float"
        }
        """;

    const string MultiQueryBodyAlternateVariants = """
        {
            "queries": [{"limit": 10, "rank_by": ["id", "asc"]}],
            "consistency": {"level": "strong"},
            "limit": 10
        }
        """;

    const string RecallBody = """
        {
            "filters": ["attr", "Eq", "value"],
            "include_ground_truth": true,
            "num": 0,
            "rank_by": ["id", "asc"],
            "top_k": 0
        }
        """;

    public static TheoryData<Type, string> Bodies =>
        new()
        {
            { typeof(NamespaceQueryParams), QueryBody },
            { typeof(NamespaceQueryParams), QueryBodyAlternateVariants },
            { typeof(NamespaceExplainQueryParams), ExplainQueryBody },
            { typeof(NamespaceExplainQueryParams), QueryBodyAlternateVariants },
            { typeof(NamespaceMultiQueryParams), MultiQueryBody },
            { typeof(NamespaceMultiQueryParams), MultiQueryBodyAlternateVariants },
            { typeof(NamespaceRecallParams), RecallBody },
        };

    public static TheoryData<Type> Types =>
        new()
        {
            typeof(NamespaceQueryParams),
            typeof(NamespaceExplainQueryParams),
            typeof(NamespaceMultiQueryParams),
            typeof(NamespaceRecallParams),
        };

    public static TheoryData<Type> TypesWithConsistency =>
        new()
        {
            typeof(NamespaceQueryParams),
            typeof(NamespaceExplainQueryParams),
            typeof(NamespaceMultiQueryParams),
        };

    [Theory]
    [MemberData(nameof(Bodies))]
    public void Properties_RoundtripThroughWireFormat(Type type, string body)
    {
        var expected = Json(body);
        var parameters = FromWire(type, body);

        var keys = expected.EnumerateObject().Select(p => p.Name).ToList();
        foreach (var prop in BodyProperties(type).Where(p => keys.Contains(WireName(p))))
        {
            keys.Remove(WireName(prop));
            var value = prop.GetValue(parameters);
            Assert.NotNull(value);

            var serialized = Serialize(value, prop.PropertyType);
            AssertJson(expected.GetProperty(WireName(prop)), serialized, prop.Name);

            var deserialized = JsonSerializer.Deserialize(
                serialized,
                prop.PropertyType,
                ModelBase.SerializerOptions
            );
            AssertJson(serialized, Serialize(deserialized, prop.PropertyType), prop.Name);

            ValidateAll(value);
        }
        Assert.True(keys.Count == 0, $"no property for: {string.Join(", ", keys)}");
    }

    [Theory]
    [MemberData(nameof(Bodies))]
    public void NestedModels_CopyAndDefaults_Work(Type type, string body)
    {
        var parameters = FromWire(type, body);

        foreach (var model in BodyProperties(type).SelectMany(p => Models(p.GetValue(parameters))))
        {
            var modelType = model.GetType();
            var copy = modelType.GetConstructor([modelType])!.Invoke([model]);
            Assert.Equal(model, copy);
            Assert.NotSame(model, copy);

            var empty = modelType
                .GetMethod("FromRawUnchecked", BindingFlags.Public | BindingFlags.Static)!
                .Invoke(null, [new Dictionary<string, JsonElement>()]);
            var optional = OptionalProperties(modelType).ToList();
            foreach (var prop in optional)
            {
                Assert.Null(prop.GetValue(empty));
            }
            if (optional.Count == ModelProperties(modelType).Count())
            {
                Validate(empty!);
            }
        }
    }

    [Theory]
    [MemberData(nameof(Bodies))]
    public void CopyConstructor_Works(Type type, string body)
    {
        var parameters = FromWire(type, body);

        var copy = type.GetConstructor([type])!.Invoke([parameters]);

        Assert.Equal(parameters, copy);
        Assert.NotSame(parameters, copy);
    }

    [Theory]
    [MemberData(nameof(Bodies))]
    public async Task BodyContent_IsWireFormat(Type type, string body)
    {
        var parameters = FromWire(type, body);

        var content = parameters.BodyContent()!;
#if NET
        var json = await content.ReadAsStringAsync(TestContext.Current.CancellationToken);
#else
        var json = await content.ReadAsStringAsync();
#endif

        AssertJson(Json(body), Json(json), type.Name);
    }

    [Theory]
    [MemberData(nameof(Types))]
    public void OptionalParamsUnset_AreNotSet(Type type)
    {
        var parameters = FromWire(type, "{}");

        foreach (var prop in OptionalProperties(type))
        {
            Assert.Null(prop.GetValue(parameters));
        }
        Assert.Empty(RawBodyData(parameters));
    }

    [Theory]
    [MemberData(nameof(Types))]
    public void OptionalParamsSetToNull_AreNotSet(Type type)
    {
        var parameters = FromWire(type, "{}");

        foreach (var prop in OptionalProperties(type).Where(p => p.CanWrite))
        {
            prop.SetValue(parameters, null);
            Assert.Null(prop.GetValue(parameters));
        }
        Assert.Empty(RawBodyData(parameters));
    }

    [Theory]
    [InlineData(typeof(NamespaceQueryParams), "/v2/namespaces/namespace/query")]
    [InlineData(typeof(NamespaceExplainQueryParams), "/v2/namespaces/namespace/explain_query")]
    [InlineData(
        typeof(NamespaceMultiQueryParams),
        "/v2/namespaces/namespace/query?stainless_overload=multiQuery"
    )]
    [InlineData(typeof(NamespaceRecallParams), "/v1/namespaces/namespace/_debug/recall")]
    public void Url_Works(Type type, string path)
    {
        var parameters = FromWire(type, "{}");

        var url = parameters.Url(new() { Region = "gcp-us-central1", ApiKey = "tpuf_A1..." });

        Assert.True(
            TestBase.UrisEqual(new Uri("https://gcp-us-central1.turbopuffer.com" + path), url),
            url.ToString()
        );
    }

    [Theory]
    [MemberData(nameof(TypesWithConsistency))]
    public void InvalidConsistencyLevel_RoundtripsAndFailsValidation(Type type)
    {
        const string body = """{"consistency": {"level": "invalid value"}}""";
        var parameters = FromWire(type, body);
        var prop = type.GetProperty("Consistency")!;

        var consistency = prop.GetValue(parameters)!;

        AssertJson(
            Json(body).GetProperty("consistency"),
            Serialize(consistency, prop.PropertyType)
        );
        var e = Assert.Throws<TargetInvocationException>(() => Validate(consistency));
        Assert.IsType<TurbopufferInvalidDataException>(e.InnerException);
    }

    static JsonElement Json(string json) => JsonSerializer.Deserialize<JsonElement>(json);

    static JsonElement Serialize(object? value, Type type) =>
        JsonSerializer.SerializeToElement(value, type, ModelBase.SerializerOptions);

    static void AssertJson(JsonElement expected, JsonElement actual, string what = "") =>
        Assert.True(
            JsonElement.DeepEquals(expected, actual),
            $"{what}: expected {expected.GetRawText()}, got {actual.GetRawText()}"
        );

    static ParamsBase FromWire(Type type, string body)
    {
        var rawBody = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(body)!;
        var empty = new Dictionary<string, JsonElement>();
        var method = type.GetMethod("FromRawUnchecked", BindingFlags.Public | BindingFlags.Static)!;
        var args = method
            .GetParameters()
            .Select(p =>
                p.ParameterType == typeof(string) ? "namespace"
                : p.Name == "rawBodyData" ? (object)rawBody
                : empty
            )
            .ToArray();
        return (ParamsBase)method.Invoke(null, args)!;
    }

    static IReadOnlyDictionary<string, JsonElement> RawBodyData(object parameters) =>
        (IReadOnlyDictionary<string, JsonElement>)
            parameters.GetType().GetProperty("RawBodyData")!.GetValue(parameters)!;

    static IEnumerable<PropertyInfo> ModelProperties(Type type) =>
        type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Where(p => p.GetIndexParameters().Length == 0 && !p.Name.StartsWith("Raw"));

    static IEnumerable<PropertyInfo> BodyProperties(Type type) =>
        ModelProperties(type).Where(p => p.Name != "Namespace");

    static IEnumerable<PropertyInfo> OptionalProperties(Type type) =>
        BodyProperties(type)
            .Where(p =>
                !p.GetCustomAttributes().Any(a => a.GetType().Name == "RequiredMemberAttribute")
            );

    static string WireName(PropertyInfo prop) =>
        string.Concat(
            prop.Name.Select(
                (c, i) =>
                    char.IsUpper(c) ? (i > 0 ? "_" : "") + char.ToLowerInvariant(c) : c.ToString()
            )
        );

    // JsonModels reachable from a property value, including list and dictionary elements.
    static IEnumerable<object> Models(object? value) =>
        value switch
        {
            null or string or JsonElement => [],
            JsonModel model => [model],
            IDictionary dictionary => dictionary.Values.Cast<object>().SelectMany(Models),
            IEnumerable items => items.Cast<object>().SelectMany(Element),
            _ => [],
        };

    static IEnumerable<object> Element(object item)
    {
        var type = item.GetType();
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
        {
            return Models(type.GetProperty("Value")!.GetValue(item));
        }
        return Models(item);
    }

    static void Validate(object value) =>
        value.GetType().GetMethod("Validate", Type.EmptyTypes)?.Invoke(value, null);

    static void ValidateAll(object? value)
    {
        switch (value)
        {
            case null or string or JsonElement:
                return;
            case IEnumerable items
                when value.GetType().GetMethod("Validate", Type.EmptyTypes) == null:
                foreach (var item in items)
                {
                    ValidateAll(
                        item?.GetType() is { IsGenericType: true } t
                        && t.GetGenericTypeDefinition() == typeof(KeyValuePair<,>)
                            ? t.GetProperty("Value")!.GetValue(item)
                            : item
                    );
                }
                return;
            default:
                Validate(value);
                return;
        }
    }
}
