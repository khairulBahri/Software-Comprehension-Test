using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Timers;

public class InternFrameProcessor
{
    private Queue<Frame> _frames = new Queue<Frame>();
    private Timer _timer;
    private IValueReporter _reporter;

    public InternFrameProcessor(FrameGrabber grabber, IValueReporter reporter)
    {
        grabber.OnFrameUpdated += HandleFrame;
        _reporter = reporter;

        _timer = new Timer(1000 / 30);
        _timer.Elapsed += OnTimerElapsed;
    }

    public void Start() => _timer.Start();

    private void HandleFrame(Frame frame)
    {
        _frames.Enqueue(frame);
    }

    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        if (_frames.Count == 0)
            return;

        Frame frame = _frames.Dequeue();
        byte[] raw = frame.GetRawData();

        int sum = 0;
        foreach (var b in raw)
            sum += b;

        _reporter.Report(sum / raw.Length);
    }
}

public class FrameGrabber : IFrameCallback
{
    private byte[] _buffer;
    public event Action<Frame> OnFrameUpdated;

    public void FrameReceived(IntPtr frame, int width, int height)
    {
        if (_buffer == null)
            _buffer = new byte[width * height];

        Marshal.Copy(frame, _buffer, 0, _buffer.Length);
        var f = new Frame(_buffer);
        OnFrameUpdated?.Invoke(f);
        f.Dispose();
    }
}

public class Frame : IDisposable
{
    private bool _disposed;
    private byte[] _raw;

    public Frame(byte[] raw)
    {
        _raw = raw;
    }

    public byte[] GetRawData()
    {
        if (_disposed)
            throw new ObjectDisposedException("Frame disposed");
        return _raw;
    }

    public void Dispose()
    {
        _disposed = true;
    }
}
