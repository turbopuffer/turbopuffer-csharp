// A straightforward example of storing and retrieving documents via vector
// similarity search.
//
// Run this example with: dotnet run --project examples/Turbopuffer.Client.Examples WriteAndQuery
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Turbopuffer.Client;
using Turbopuffer.Client.Exceptions;
using Turbopuffer.Client.Models.Namespaces;

namespace Turbopuffer.Client.Examples;

static class WriteAndQuery
{
    public static async Task Run()
    {
        using var client = new TurbopufferClient
        {
            // pick the right region: https://turbopuffer.com/docs/regions
            Region = "gcp-us-central1",
        };

        var ns = "turbopuffer-csharp-write-and-query-test";
        Console.WriteLine($"Operating on namespace: {ns}");

        var nsClient = client.Namespace(ns);

        // Delete the namespace if it already exists.
        try
        {
            await nsClient.DeleteAll(new NamespaceDeleteAllParams());
        }
        catch (TurbopufferNotFoundException)
        {
            Console.WriteLine("Namespace not found, continuing");
        }

        // Upsert some documents.
        var upsert = await nsClient.Write(
            new NamespaceWriteParams
            {
                UpsertRows =
                [
                    new Row()
                        .Set("id", "b3ff34ea-87bb-469c-a854-9cb7e3713fc3")
                        .Set("vector", new[] { 1.0, 2.0, 3.0 })
                        .Set("name", "Luke")
                        .Set("age", 32),
                    new Row()
                        .Set("id", "580d4471-9a9b-44fb-b59d-637ade604f72")
                        .Set("vector", new[] { 4.0, 5.0, 6.0 })
                        .Set("name", "Leia")
                        .Set("age", 28),
                ],
                DistanceMetric = DistanceMetric.CosineDistance,
                Schema = new Dictionary<string, AttributeSchemaConfig>
                {
                    ["id"] = new AttributeSchemaConfig { Type = "uuid" },
                    ["name"] = new AttributeSchemaConfig { Type = "string", Filterable = false },
                    ["age"] = new AttributeSchemaConfig { Type = "uint" },
                },
            }
        );
        Console.WriteLine($"Rows upserted: {upsert.RowsUpserted}");

        // Do a vector query.
        var query = await nsClient.Query(
            new NamespaceQueryParams
            {
                RankBy = RankBy.Ann("vector", [3.0f, 4.0f, 5.0f]),
                TopK = 10,
                IncludeAttributes = true,
                Filters = Filter.And([Filter.Gt("age", 30), Filter.Lt("age", 35)]),
            }
        );
        Console.WriteLine($"Query result:\n{query}");

        // Print the age from the first row.
        Console.WriteLine($"Age: {query.Rows![0]["age"]}");

        // Print the schema.
        var schema = await nsClient.Schema(new NamespaceSchemaParams());
        Console.WriteLine($"Schema:\n{JsonSerializer.Serialize(schema)}");

        // Patch one document.
        var patch = await nsClient.Write(
            new NamespaceWriteParams
            {
                PatchRows =
                [
                    new Row().Set("id", "580d4471-9a9b-44fb-b59d-637ade604f72").Set("age", 82),
                ],
                DistanceMetric = DistanceMetric.CosineDistance,
            }
        );
        Console.WriteLine($"Rows patched: {patch.RowsPatched}");

        // Do a non-vector query to see the patched results.
        var query2 = await nsClient.Query(
            new NamespaceQueryParams
            {
                RankBy = RankBy.Attribute("id", RankByAttributeOrder.ASC),
                TopK = 10,
                IncludeAttributes = true,
            }
        );
        Console.WriteLine($"Query result:\n{query2}");
    }
}
