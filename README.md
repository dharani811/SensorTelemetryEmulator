# SensorTelemetryEmulator
Sensor Telemetry Emulator — a lightweight system that mimics real-time sensor data flow using System.Threading.Channels in C#.

🔧 What it does:
Simulates a continuous stream of telemetry from multiple sensor types (Temperature, Pressure, Wind, etc.)
Uses an asynchronous producer-consumer pipeline with built-in support for:

Backpressure handling

Cancellation tokens

Polymorphic sensor readings

Clean separation between emitters and receivers

👨‍💻 Why I built it:
As part of my daily practice during my transition period, I wanted to model a realistic, decoupled streaming system — something I could scale later into a diagnostics tool or integrate with visualization layers like WPF or Avalonia.

💡 Tech behind it:
.NET 8 · Async/Await · Channels · OOP · System Design Principles

Always open to feedback, thoughts, or collaborations.
