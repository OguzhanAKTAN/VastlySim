using System.Text;
using BenchmarkDotNet.Attributes;
namespace Benchmarks;

public class StringConcatenationBench
{
    private const int N = 1000;
    private readonly string[] Words = new string[N];

    // Setup method runs once before all benchmark tests.
    [GlobalSetup]
    public void Setup()
    {
        // Populate the array with simple strings
        for (int i = 0; i < N; i++)
        {
            Words[i] = "word" + i;
        }
    }

    // --- Benchmark Method 1: Standard Concatenation (Expected to be slow) ---
    
    // The [Benchmark] attribute marks a method to be measured.
    [Benchmark]
    public string ConcatenateWithPlus()
    {
        string result = string.Empty;
        
        // Repeatedly concatenating strings with '+' creates many temporary string objects,
        // leading to poor performance and high memory allocation.
        foreach (var word in Words)
        {
            result += word;
        }
        return result;
    }

    // --- Benchmark Method 2: StringBuilder (Expected to be fast) ---

    [Benchmark]
    public string ConcatenateWithStringBuilder()
    {
        // StringBuilder is optimized for creating a single mutable buffer, 
        // which avoids the overhead of creating many intermediate string objects.
        var sb = new StringBuilder();
        foreach (var word in Words)
        {
            sb.Append(word);
        }
        return sb.ToString();
    }
    
}