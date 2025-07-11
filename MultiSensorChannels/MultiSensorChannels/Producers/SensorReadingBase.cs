namespace MultiSensorChannels.Producers
{
	/// <summary>
	/// Sensor Types
	/// </summary>
	public enum ESensorType
	{
		/// <summary>
		/// Deafult
		/// </summary>
		None = 0,
		/// <summary>
		/// Pressure Sensor
		/// </summary>
		Pressure = 1,
		/// <summary>
		/// Temperature Sensor
		/// </summary>
		Temperature = 2,
		/// <summary>
		/// Wind Sensor
		/// </summary>
		Wind = 3
	}
	/// <summary>
	/// Sensor Base
	/// </summary>
	public class SensorReadingBase
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public SensorReadingBase()
		{
			ID = Guid.NewGuid();
		}
		/// <summary>
		/// Reading ID
		/// </summary>
		public Guid ID { get; }
		/// <summary>
		/// Sensor Type
		/// </summary>
		public virtual ESensorType SensorType { get; } = ESensorType.None;
	}
}
