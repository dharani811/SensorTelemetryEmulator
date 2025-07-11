namespace MultiSensorChannels.Producers
{
	public class TemperatureSensorReading : SensorReadingBase
	{
		public TemperatureSensorReading() : base()
		{
		}
		public override ESensorType SensorType { get; } = ESensorType.Temperature;
	}
}
