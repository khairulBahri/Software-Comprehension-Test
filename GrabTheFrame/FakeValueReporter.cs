using System;

public class FakeValueReporter : IValueReporter
{
    public void Report(double value)
    {
        Console.WriteLine($"Reported Avg = {value}");
    }
}
