using System;

public interface IFrameCallback
{
    void FrameReceived(IntPtr pFrame, int width, int height);
}

public interface IValueReporter
{
    void Report(double value);
}
