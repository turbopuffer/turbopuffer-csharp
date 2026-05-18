using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Turbopuffer.Examples;

static class Program
{
    static readonly IReadOnlyDictionary<string, Func<Task>> Examples = new Dictionary<
        string,
        Func<Task>
    >(StringComparer.OrdinalIgnoreCase)
    {
        ["BulkWrite"] = BulkWrite.Run,
        ["ConcurrentPerformance"] = ConcurrentPerformance.Run,
        ["ListNamespaces"] = ListNamespaces.Run,
        ["WriteAndQuery"] = WriteAndQuery.Run,
    };

    static async Task<int> Main(string[] args)
    {
        if (args.Length != 1 || !Examples.TryGetValue(args[0], out var run))
        {
            Console.Error.WriteLine(
                "usage: dotnet run --project examples/Turbopuffer.Examples <example>"
            );
            Console.Error.WriteLine("");
            Console.Error.WriteLine("available examples:");
            foreach (var name in Examples.Keys)
            {
                Console.Error.WriteLine($"  {name}");
            }
            return 1;
        }

        await run();
        return 0;
    }
}
