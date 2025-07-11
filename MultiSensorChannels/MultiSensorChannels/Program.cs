// See https://aka.ms/new-console-template for more information
using MultiSensorChannels;

public class Program
{
	public static void Main(string[] args)
	{
		Console.WriteLine("Starting emitter");
		Emitter emitter = new Emitter();
		emitter.StartEmittingReadings();

		Console.WriteLine("Starting receiver");
		Receiver receiver = new Receiver(emitter.EmitterChannel);
		receiver.StartReceiving();

		Console.WriteLine("Press any key to end");
		Console.ReadLine();

		emitter.Dispose();
		receiver.Dispose();
	}
}
