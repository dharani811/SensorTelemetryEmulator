namespace MultiSensorChannels.Producers
{
	public class PressureSensorReading : SensorReadingBase
	{
		public PressureSensorReading() : base()
		{
		}
		public override ESensorType SensorType { get; } = ESensorType.Pressure;
	}
}
