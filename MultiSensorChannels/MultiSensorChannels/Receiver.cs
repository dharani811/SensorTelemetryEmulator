using MultiSensorChannels.Producers;
using System.Threading.Channels;

namespace MultiSensorChannels
{
	/// <summary>
	/// Receiver - This emulates a reading agrregator from a emitter hub
	/// </summary>
	public class Receiver : IDisposable
	{
		private readonly ChannelReader<SensorReadingBase> reader;
		private readonly CancellationTokenSource cts = new();
		private Task? receiveTask;
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="channel"></param>
		public Receiver(Channel<SensorReadingBase> channel)
		{
			this.reader = channel;
		}
		/// <summary>
		/// Starts receiving the readings
		/// </summary>
		public void StartReceiving()
		{
			receiveTask = Task.WhenAll(
	Task.Run(() => ConsumeAsync(reader, cts.Token)),
	Task.Run(() => ConsumeAsync(reader, cts.Token)),
	Task.Run(() => ConsumeAsync(reader, cts.Token)),
	Task.Run(() => PrintBackPressure(cts.Token))
);
		}

		private static async Task ConsumeAsync(ChannelReader<SensorReadingBase> reader, CancellationToken token)
		{
			while (await reader.WaitToReadAsync(token))
			{
				while (reader.TryRead(out SensorReadingBase? coordinates))
				{
					Console.WriteLine($"Received => {coordinates} with GUID => {coordinates.ID}");
					await Task.Delay(500);
				}
			}
		}
		private async Task PrintBackPressure(CancellationToken token)
		{
			while (!token.IsCancellationRequested)
			{
				Console.WriteLine($"Backpressure => {reader.Count}");
				await Task.Delay(500);
			}
		}
		///<inheritdoc/>
		public void Dispose()
		{
			cts.Cancel();

			if (!cts.IsCancellationRequested)
				receiveTask?.Wait();

			cts.Dispose();
		}

	}
}
