using MultiSensorChannels.Producers;
using System.Threading.Channels;

namespace MultiSensorChannels
{
	/// <summary>
	/// Reading Emitter - This emulates sensor readings being sent from a real sensor hub
	/// </summary>
	public class Emitter : IDisposable
	{
		private bool isQuitting = false;
		private List<ESensorType> SensorTypes = new List<ESensorType>();
		private readonly Channel<SensorReadingBase> channel;
		private Task? sensorTask;
		/// <summary>
		/// Constructor
		/// </summary>
		public Emitter()
		{
			SensorTypes.Add(ESensorType.Pressure);
			SensorTypes.Add(ESensorType.Temperature);
			SensorTypes.Add(ESensorType.Wind);
			channel = Channel.CreateBounded<SensorReadingBase>(
	new BoundedChannelOptions(1000)
	{
		AllowSynchronousContinuations = true,
		FullMode = BoundedChannelFullMode.DropOldest
	},
	static void (SensorReadingBase dropped) =>
		Console.WriteLine($"Coordinates dropped: {dropped}"));
		}
		/// <summary>
		/// Writer channel
		/// </summary>
		public Channel<SensorReadingBase> EmitterChannel => channel;
		/// <summary>
		/// Starts emitting readings
		/// </summary>
		public void StartEmittingReadings()
		{
			sensorTask = Task.Run(async () =>
			{
				Random random = new Random(0);

				while (!isQuitting)
				{
					int next = random.Next(0, SensorTypes.Count);
					ESensorType sensorType = SensorTypes[next];
					SensorReadingBase? sensorBase = null;
					switch (sensorType)
					{
						case ESensorType.Pressure:
							sensorBase = new PressureSensorReading();
							break;
						case ESensorType.Temperature:
							sensorBase = new TemperatureSensorReading();
							break;
						case ESensorType.Wind:
							sensorBase = new WindSensorReading();
							break;
					}
					await ProduceWithWhileWriteAsync(channel, sensorBase);
					await Task.Delay(10);
				}
				channel.Writer.Complete();
			});
		}
		private async ValueTask ProduceWithWhileWriteAsync(
	ChannelWriter<SensorReadingBase> writer, SensorReadingBase? coordinates)
		{
			if (coordinates is not null)
			{
				await writer.WriteAsync(coordinates);
			}
		}
		///<inheritdoc/>
		public void Dispose()
		{
			isQuitting = true;
			sensorTask?.Wait();
		}
	}
}
