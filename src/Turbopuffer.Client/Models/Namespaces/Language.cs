using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Client.Exceptions;

namespace Turbopuffer.Client.Models.Namespaces;

/// <summary>
/// Describes the language of a text attribute. Defaults to `english`.
/// </summary>
[JsonConverter(typeof(LanguageConverter))]
public enum Language
{
    Arabic,
    Danish,
    Dutch,
    English,
    Finnish,
    French,
    German,
    Greek,
    Hungarian,
    Italian,
    Norwegian,
    Portuguese,
    Romanian,
    Russian,
    Spanish,
    Swedish,
    Tamil,
    Turkish,
}

sealed class LanguageConverter : JsonConverter<Language>
{
    public override Language Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<string>(ref reader, options) switch
        {
            "arabic" => Language.Arabic,
            "danish" => Language.Danish,
            "dutch" => Language.Dutch,
            "english" => Language.English,
            "finnish" => Language.Finnish,
            "french" => Language.French,
            "german" => Language.German,
            "greek" => Language.Greek,
            "hungarian" => Language.Hungarian,
            "italian" => Language.Italian,
            "norwegian" => Language.Norwegian,
            "portuguese" => Language.Portuguese,
            "romanian" => Language.Romanian,
            "russian" => Language.Russian,
            "spanish" => Language.Spanish,
            "swedish" => Language.Swedish,
            "tamil" => Language.Tamil,
            "turkish" => Language.Turkish,
            _ => (Language)(-1),
        };
    }

    public override void Write(Utf8JsonWriter writer, Language value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(
            writer,
            value switch
            {
                Language.Arabic => "arabic",
                Language.Danish => "danish",
                Language.Dutch => "dutch",
                Language.English => "english",
                Language.Finnish => "finnish",
                Language.French => "french",
                Language.German => "german",
                Language.Greek => "greek",
                Language.Hungarian => "hungarian",
                Language.Italian => "italian",
                Language.Norwegian => "norwegian",
                Language.Portuguese => "portuguese",
                Language.Romanian => "romanian",
                Language.Russian => "russian",
                Language.Spanish => "spanish",
                Language.Swedish => "swedish",
                Language.Tamil => "tamil",
                Language.Turkish => "turkish",
                _ => throw new TurbopufferInvalidDataException(
                    string.Format("Invalid value '{0}' in {1}", value, nameof(value))
                ),
            },
            options
        );
    }
}
