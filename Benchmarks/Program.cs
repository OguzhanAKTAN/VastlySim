using BenchmarkDotNet.Running;
namespace Benchmarks;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        BenchmarkRunner.Run<StringConcatenationBench>();
    }
}