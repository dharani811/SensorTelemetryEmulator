namespace MultiSensorChannels.Producers
{
	public class WindSensorReading : SensorReadingBase
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public WindSensorReading() : base()
		{
		}
		///<inheritdoc/>
		public override ESensorType SensorType => ESensorType.Wind;
	}
}
