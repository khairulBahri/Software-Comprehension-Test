using System;
using System.Runtime.InteropServices;

public class CorrectFrameProcessor : IFrameCallback
{
    private readonly IValueReporter _reporter;

    public CorrectFrameProcessor(IValueReporter reporter)
    {
        _reporter = reporter;
    }

    public void FrameReceived(IntPtr pFrame, int width, int height)
    {
        int size = width * height;
        byte[] buffer = new byte[size];

        Marshal.Copy(pFrame, buffer, 0, size);

        long sum = 0;
        for (int i = 0; i < buffer.Length; i++)
            sum += buffer[i];

        double avg = (double)sum / buffer.Length;
        _reporter.Report(avg);
    }
}
