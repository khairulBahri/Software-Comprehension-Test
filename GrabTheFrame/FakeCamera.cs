using System;
using System.Runtime.InteropServices;
using System.Threading;

public class FakeCamera
{
    private readonly IFrameCallback _callback;
    private readonly int _width;
    private readonly int _height;

    public FakeCamera(IFrameCallback callback, int width, int height)
    {
        _callback = callback;
        _width = width;
        _height = height;
    }

    public void Start(int frames)
    {
        int size = _width * _height;
        byte[] buffer = new byte[size];

        for (int f = 0; f < frames; f++)
        {
            for (int i = 0; i < size; i++)
                buffer[i] = (byte)(f + 1);

            IntPtr unmanaged = Marshal.AllocHGlobal(size);
            Marshal.Copy(buffer, 0, unmanaged, size);

            _callback.FrameReceived(unmanaged, _width, _height);

            Marshal.FreeHGlobal(unmanaged);
            Thread.Sleep(33);
        }
    }
}
