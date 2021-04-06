using System;
using System.Runtime.Serialization;

namespace Software10101.Units {
	[Serializable]
	public readonly struct VolumetricFlowRate : IEquatable<VolumetricFlowRate>, IComparable<VolumetricFlowRate>, ISerializable {
		private const string UnitString = "m³/s";

		public static readonly VolumetricFlowRate ZeroRate            = 0.0; // m³/s
		public static readonly VolumetricFlowRate MeterCubedPerSecond = Volume.CubicMeter / Duration.Second;
		public static readonly VolumetricFlowRate MaxRate             = double.MaxValue;

		private readonly Volume _volume;
		private readonly Duration _duration;

		/////////////////////////////////////////////////////////////////////////////
		// BOXING
		/////////////////////////////////////////////////////////////////////////////
		public VolumetricFlowRate(double q) {
			_volume = Volume.From(q, Volume.CubicMeter);
			_duration = Duration.Second;
		}

		public VolumetricFlowRate(Volume v, Duration d) {
			_volume = v;
			_duration = d;
		}

		public static VolumetricFlowRate From(double q) {
			return new VolumetricFlowRate(q);
		}

		public static VolumetricFlowRate From(Volume v, Duration d) {
			return new VolumetricFlowRate(v, d);
		}

		public static implicit operator VolumetricFlowRate(double q) {
			return From(q);
		}

		/////////////////////////////////////////////////////////////////////////////
		// UN-BOXING
		/////////////////////////////////////////////////////////////////////////////
		public double To(VolumetricFlowRate unit) {
			return _volume.To(unit._volume) / _duration.To(unit._duration);
		}

		public static implicit operator double(VolumetricFlowRate q) {
			return q.To(MeterCubedPerSecond);
		}

		/////////////////////////////////////////////////////////////////////////////
		// SERIALIZATION
		/////////////////////////////////////////////////////////////////////////////
		public VolumetricFlowRate(SerializationInfo info, StreamingContext context) {
			_volume = (Volume)info.GetValue("volume", typeof(Volume));
			_duration = (Duration)info.GetValue("duration", typeof(Duration));
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue("volume", _volume, typeof(Volume));
			info.AddValue("duration", _duration, typeof(Duration));
		}

		/////////////////////////////////////////////////////////////////////////////
		// OPERATORS
		/////////////////////////////////////////////////////////////////////////////
		public static VolumetricFlowRate operator +(VolumetricFlowRate first, VolumetricFlowRate second) {
			return first.To(MeterCubedPerSecond) + second.To(MeterCubedPerSecond);
		}

		public static VolumetricFlowRate operator +(VolumetricFlowRate first, double second) {
			return first.To(MeterCubedPerSecond) + second;
		}

		public static VolumetricFlowRate operator +(double first, VolumetricFlowRate second) {
			return first + second.To(MeterCubedPerSecond);
		}

		public static VolumetricFlowRate operator -(VolumetricFlowRate first, VolumetricFlowRate second) {
			return first.To(MeterCubedPerSecond) - second.To(MeterCubedPerSecond);
		}

		public static VolumetricFlowRate operator -(VolumetricFlowRate first, double second) {
			return first.To(MeterCubedPerSecond) - second;
		}

		public static VolumetricFlowRate operator -(double first, VolumetricFlowRate second) {
			return first - second.To(MeterCubedPerSecond);
		}

		public static VolumetricFlowRate operator *(VolumetricFlowRate first, double second) {
			return first.To(MeterCubedPerSecond) * second;
		}

		public static VolumetricFlowRate operator *(double first, VolumetricFlowRate second) {
			return first * second.To(MeterCubedPerSecond);
		}

		public static double operator /(VolumetricFlowRate first, VolumetricFlowRate second) {
			return first.To(MeterCubedPerSecond) / second.To(MeterCubedPerSecond);
		}

		public static VolumetricFlowRate operator /(VolumetricFlowRate first, double second) {
			return first.To(MeterCubedPerSecond) / second;
		}

		/////////////////////////////////////////////////////////////////////////////
		// MUTATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Volume operator *(VolumetricFlowRate volumetricFlowRate, Duration duration) {
			return volumetricFlowRate._volume * (duration / volumetricFlowRate._duration);
		}

		/////////////////////////////////////////////////////////////////////////////
		// EQUALITY
		/////////////////////////////////////////////////////////////////////////////
		public bool Equals(VolumetricFlowRate other) {
			return _volume.Equals(other._volume) && _duration.Equals(other._duration);
		}

		public bool Equals(VolumetricFlowRate other, VolumetricFlowRate delta) {
			return Math.Abs(To(MeterCubedPerSecond) - other.To(MeterCubedPerSecond)) < Math.Abs(delta.To(MeterCubedPerSecond));
		}

		public override bool Equals(object obj) {
			return obj is VolumetricFlowRate other && Equals(other);
		}

		public override int GetHashCode() {
			return _volume.GetHashCode() ^ _duration.GetHashCode();
		}

		public static bool operator ==(VolumetricFlowRate first, VolumetricFlowRate second) {
			return first.Equals(second);
		}

		public static bool operator !=(VolumetricFlowRate first, VolumetricFlowRate second) {
			return !first.Equals(second);
		}

		public int CompareTo(VolumetricFlowRate other) {
			return To(MeterCubedPerSecond).CompareTo(other.To(MeterCubedPerSecond));
		}

		/////////////////////////////////////////////////////////////////////////////
		// TO STRING
		/////////////////////////////////////////////////////////////////////////////
		public override string ToString() {
			return ToStringMetersCubedPerSecond();
		}

		public string ToStringMetersCubedPerSecond() {
			return $"{To(MeterCubedPerSecond):F2}{UnitString}";
		}
	}
}
