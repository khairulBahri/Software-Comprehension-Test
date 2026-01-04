# Software-Comprehension-Test

Frame Processing Comparison Demo

This repository demonstrates two implementations of a camera frame processing pipeline:

Intern Version (Buggy) – intentionally flawed, based on the original problem statement

Correct Version (Recommended) – safe, deterministic, and production-ready

The goal is to highlight common pitfalls in native interop and real-time systems, and how to properly design and test such systems.

# Problem Summary

Frames are received from a native camera callback at 30 FPS

The native buffer (IntPtr pFrame) is reused after the callback returns

Each frame must be processed to compute the average pixel value

The computed value is streamed to a real-time value reporter


# Structure
GrabTheFrame/
│
├── Interfaces.cs          
├── InternVersion.cs       
├── CorrectVersion.cs      
├── FakeCamera.cs          
├── FakeValueReporter.cs   
└── Program.cs             

# How to Run
Build and run the console application

Choose which version to execute:
1 = Intern Version (Buggy & Unsafe)
2 = Correct Version (Safe & Deterministic)


