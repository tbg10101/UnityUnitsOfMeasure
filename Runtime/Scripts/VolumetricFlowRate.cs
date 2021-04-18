using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using UnityEngine;

namespace Software10101.Units {
	// ideally the entire struct would be readonly
	// however, Unity does not serialize readonly fields
	// this is unfortunate but we can enforce immutability by not exposing the value publicly
	[Serializable]
	public struct VolumetricFlowRate : IEquatable<VolumetricFlowRate>, IComparable<VolumetricFlowRate>, ISerializable {
		private const string UnitString = "m³/s";

		public static readonly VolumetricFlowRate ZeroRate            = new VolumetricFlowRate();
		public static readonly VolumetricFlowRate MeterCubedPerSecond = new VolumetricFlowRate {
			_volume = Volume.CubicMeter,
			_duration = Duration.Second
		};

		[SerializeField]
		internal Volume _volume;

		[SerializeField]
		internal Duration _duration;

		/////////////////////////////////////////////////////////////////////////////
		// BOXING
		/////////////////////////////////////////////////////////////////////////////
		public VolumetricFlowRate(Volume v, Duration d) {
			_volume = v;
			_duration = d;
		}

		public static VolumetricFlowRate From(Volume v, Duration d) {
			return new VolumetricFlowRate(v, d);
		}

		/////////////////////////////////////////////////////////////////////////////
		// UN-BOXING
		/////////////////////////////////////////////////////////////////////////////
		public double To(VolumetricFlowRate unit) {
			return _volume.To(unit._volume) / _duration.To(unit._duration);
		}

		/////////////////////////////////////////////////////////////////////////////
		// SERIALIZATION
		/////////////////////////////////////////////////////////////////////////////
		private const string VolumeSerializedFieldName = "volume";
		private const string DurationSerializedFieldName = "duration";

		public VolumetricFlowRate(SerializationInfo info, StreamingContext context) {
			_volume = (Volume)info.GetValue(VolumeSerializedFieldName, typeof(Volume));
			_duration = (Duration)info.GetValue(DurationSerializedFieldName, typeof(Duration));
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue(VolumeSerializedFieldName, _volume, typeof(Volume));
			info.AddValue(DurationSerializedFieldName, _duration, typeof(Duration));
		}

		/////////////////////////////////////////////////////////////////////////////
		// OPERATORS
		/////////////////////////////////////////////////////////////////////////////
		public static VolumetricFlowRate operator +(VolumetricFlowRate first, VolumetricFlowRate second) {
			return new VolumetricFlowRate(
				Volume.From(first.To(MeterCubedPerSecond) + second.To(MeterCubedPerSecond), Volume.CubicMeter),
				Duration.Second);
		}

		public static VolumetricFlowRate operator -(VolumetricFlowRate first, VolumetricFlowRate second) {
			return new VolumetricFlowRate(
				Volume.From(first.To(MeterCubedPerSecond) - second.To(MeterCubedPerSecond), Volume.CubicMeter),
				Duration.Second);
		}

		public static VolumetricFlowRate operator *(VolumetricFlowRate first, double second) {
			return new VolumetricFlowRate(first._volume * second, first._duration);
		}

		public static VolumetricFlowRate operator *(double first, VolumetricFlowRate second) {
			return new VolumetricFlowRate(first * second._volume, second._duration);
		}

		public static double operator /(VolumetricFlowRate first, VolumetricFlowRate second) {
			return first.To(second);
		}

		public static VolumetricFlowRate operator /(VolumetricFlowRate first, double second) {
			return new VolumetricFlowRate(first._volume / second, first._duration);
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

		[SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
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
