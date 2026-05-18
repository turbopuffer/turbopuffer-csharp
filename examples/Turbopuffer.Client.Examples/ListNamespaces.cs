// A simple example that lists all extant namespaces.
//
// Run this example with: dotnet run --project examples/Turbopuffer.Client.Examples ListNamespaces
using System;
using System.Threading.Tasks;
using Turbopuffer.Client;

namespace Turbopuffer.Client.Examples;

static class ListNamespaces
{
    public static async Task Run()
    {
        using var client = new TurbopufferClient
        {
            // pick the right region: https://turbopuffer.com/docs/regions
            Region = "gcp-us-central1",
        };

        var page = await client.Namespaces();
        await foreach (var ns in page.Paginate())
        {
            Console.WriteLine(ns.ID);
        }
    }
}
