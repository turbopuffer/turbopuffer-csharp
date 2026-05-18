// Upserts several large batches of documents and prints the total time taken.
//
// Run this example with: dotnet run --project examples/Turbopuffer.Examples BulkWrite
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;
using Turbopuffer;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Examples;

static class BulkWrite
{
    const int NumBatches = 10;
    const int VectorDim = 1024;

    // Batch size is tuned to produce 32MB payloads. This achieves the maximum
    // discount (50%) without being close to the limit (256MB).
    const int BatchSize = 1500;

    public static async Task Run()
    {
        using var client = new TurbopufferClient
        {
            // pick the right region: https://turbopuffer.com/docs/regions
            Region = "gcp-us-central1",
            // gzip-compress request bodies (also serves as a basic integration test
            // that compression works end-to-end against a live server)
            Compression = true,
        };

        var ns = "turbopuffer-csharp-bulk-upsert-test";
        Console.WriteLine($"Operating on namespace: {ns}");

        var random = new Random();
        var totalStopwatch = Stopwatch.StartNew();

        for (int batch = 0; batch < NumBatches; batch++)
        {
            var batchStopwatch = Stopwatch.StartNew();

            var ids = new List<JsonElement>(BatchSize);
            var vectors = new List<JsonElement>(BatchSize);
            for (int i = 0; i < BatchSize; i++)
            {
                ids.Add(JsonSerializer.SerializeToElement(batch * BatchSize + i));
                var vector = new double[VectorDim];
                for (int j = 0; j < VectorDim; j++)
                {
                    vector[j] = random.NextDouble();
                }
                vectors.Add(JsonSerializer.SerializeToElement(vector));
            }

            var upsert = await client
                .Namespace(ns)
                .Write(
                    new NamespaceWriteParams
                    {
                        UpsertColumns = new Columns()
                            .SetColumn("id", ids)
                            .SetColumn("vector", vectors),
                        DistanceMetric = DistanceMetric.CosineDistance,
                    }
                );

            batchStopwatch.Stop();
            Console.WriteLine(
                $"Batch {batch + 1} complete, rows upserted: {upsert.RowsUpserted}, "
                    + $"time: {batchStopwatch.Elapsed.TotalSeconds:F2} seconds"
            );
        }

        totalStopwatch.Stop();
        Console.WriteLine($"Total time: {totalStopwatch.Elapsed.TotalSeconds:F2} seconds");
    }
}
