// A performance test example demonstrating concurrent operations.
// This example writes data to a number of namespaces and makes concurrent
// queries while monitoring total execution time and peak memory usage.
//
// Run this example with: dotnet run --project examples/Turbopuffer.Client.Examples ConcurrentPerformance
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Turbopuffer.Client;
using Turbopuffer.Client.Exceptions;
using Turbopuffer.Client.Models.Namespaces;

namespace Turbopuffer.Client.Examples;

static class ConcurrentPerformance
{
    // This test generates a number of concurrent requests, so it's helpful
    // to use a higher value for max concurrent requests than the default.
    const int MaxConcurrentRequests = 128;
    const int NumNamespaces = 128;
    const int NumWrites = 512;
    const int NumQueries = 512;
    const int VectorDimensions = 1024;
    const string NamespacePrefix = "turbopuffer-csharp-concurrent-performance-";

    public static async Task Run()
    {
        Console.WriteLine($"Max concurrent requests: {MaxConcurrentRequests}");
        Console.WriteLine($"Number of namespaces: {NumNamespaces}");
        Console.WriteLine($"Number of writes: {NumWrites}");
        Console.WriteLine($"Number of queries: {NumQueries}");
        Console.WriteLine($"Vector dimensions: {VectorDimensions}");
        Console.WriteLine();

        var initialPeakMemory = GC.GetTotalMemory(forceFullCollection: false);

        var httpHandler = new SocketsHttpHandler
        {
            MaxConnectionsPerServer = MaxConcurrentRequests,
        };
        using var client = new TurbopufferClient
        {
            HttpClient = new HttpClient(httpHandler),
            // pick the right region: https://turbopuffer.com/docs/regions
            Region = "gcp-us-central1",
        };

        Console.WriteLine("Writing data...");
        var writeStopwatch = Stopwatch.StartNew();
        var writeTasks = new Task[NumWrites];
        for (int i = 0; i < NumWrites; i++)
        {
            writeTasks[i] = Write(client, i % NumNamespaces);
            // simulate a 25ms delay between write requests arriving
            await Task.Delay(25);
        }
        await Task.WhenAll(writeTasks);
        writeStopwatch.Stop();

        Console.WriteLine("Querying data...");
        var queryStopwatch = Stopwatch.StartNew();
        var queryTasks = new Task[NumQueries];
        for (int i = 0; i < NumQueries; i++)
        {
            queryTasks[i] = Query(client, i % NumNamespaces);
            // simulate a 25ms delay between query requests arriving
            await Task.Delay(25);
        }
        await Task.WhenAll(queryTasks);
        queryStopwatch.Stop();

        Console.WriteLine($"Write execution time: {writeStopwatch.ElapsedMilliseconds} ms");
        Console.WriteLine($"Query execution time: {queryStopwatch.ElapsedMilliseconds} ms");
        Console.WriteLine($"Initial peak memory: {initialPeakMemory / (1024 * 1024)} MB");
        Console.WriteLine(
            $"Ending peak memory: {GC.GetTotalMemory(forceFullCollection: false) / (1024 * 1024)} MB"
        );
    }

    static async Task Write(TurbopufferClient client, int namespaceIndex)
    {
        var ns = $"{NamespacePrefix}{namespaceIndex}";
        var nsClient = client.Namespace(ns);

        try
        {
            await nsClient.DeleteAll(new NamespaceDeleteAllParams());
        }
        catch (TurbopufferNotFoundException) { }

        var random = new Random();
        var documentIdBase = random.Next(10000);
        var rows = new List<Row>(5);
        for (int i = 0; i < 5; i++)
        {
            rows.Add(
                new Row()
                    .Set("id", $"doc-{documentIdBase + i}")
                    .Set("vector", GenerateRandomVector())
            );
        }
        await nsClient.Write(
            new NamespaceWriteParams
            {
                UpsertRows = rows,
                DistanceMetric = DistanceMetric.CosineDistance,
            }
        );
    }

    static async Task Query(TurbopufferClient client, int namespaceIndex)
    {
        var ns = $"{NamespacePrefix}{namespaceIndex}";
        var queryResult = await client
            .Namespace(ns)
            .Query(
                new NamespaceQueryParams
                {
                    RankBy = RankBy.Ann("vector", GenerateRandomVector()),
                    TopK = 2,
                    IncludeAttributes = true,
                }
            );
        if (queryResult.Rows?.Count != 2)
        {
            throw new InvalidOperationException(
                $"Query returned {queryResult.Rows?.Count ?? 0} results for namespace {ns}; expected 2"
            );
        }
    }

    static float[] GenerateRandomVector()
    {
        var random = new Random();
        return Enumerable
            .Range(0, VectorDimensions)
            .Select(_ => (float)(random.NextDouble() * 2 - 1))
            .ToArray();
    }
}
