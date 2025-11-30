using BenchmarkDotNet.Running;
using Benchmarks.InventoryServices;
using Benchmarks.InventoryServices.Capacity;
namespace Benchmarks;

class Program
{
    static void Main(string[] args)
    {
        //Console.WriteLine("Hello, World!");
        //BenchmarkRunner.Run<StringConcatenationBench>();
        // Uncomment the benchmark you want to run:

        // Capacity
         //BenchmarkRunner.Run<DefaultCapacityPolicyBench>();

        // Search
        // BenchmarkRunner.Run<LinearSearchPolicyBench>();

        // InventoryService
        BenchmarkRunner.Run<InventoryServiceBench>();
    }
}