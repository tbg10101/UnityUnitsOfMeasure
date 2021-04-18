using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using UnityEngine;

namespace Software10101.Units {
	// ideally the entire struct would be readonly
	// however, Unity does not serialize readonly fields
	// this is unfortunate but we can enforce immutability by not exposing the value publicly
	[Serializable]
	public struct Speed : IEquatable<Speed>, IComparable<Speed>, ISerializable {
		private const string UnitString = "m/s";

		public static readonly Speed ZeroSpeed        = new Speed { _length = Length.ZeroLength, _duration = Duration.Second };
		public static readonly Speed MeterPerSecond   = new Speed { _length = Length.Meter, _duration = Duration.Second };
		public static readonly Speed KilometerPerHour = new Speed { _length = Length.Kilometer, _duration = Duration.Hour };
		public static readonly Speed C                = new Speed { _length = Length.LightSecond, _duration = Duration.Second };

		[SerializeField]
		internal Length _length;

		[SerializeField]
		internal Duration _duration;

		/////////////////////////////////////////////////////////////////////////////
		// BOXING
		/////////////////////////////////////////////////////////////////////////////
		public Speed(Length l, Duration d) {
			_length = l;
			_duration = d;
		}

		public static Speed From(Length l, Duration d) {
			return new Speed(l, d);
		}

		/////////////////////////////////////////////////////////////////////////////
		// UN-BOXING
		/////////////////////////////////////////////////////////////////////////////
		public double To(Speed unit) {
			return _length.To(unit._length) / _duration.To(unit._duration);
		}

		/////////////////////////////////////////////////////////////////////////////
		// SERIALIZATION
		/////////////////////////////////////////////////////////////////////////////
		private const string LengthSerializedFieldName = "length";
		private const string DurationSerializedFieldName = "duration";

		public Speed(SerializationInfo info, StreamingContext context) {
			_length = (Length)info.GetValue(LengthSerializedFieldName, typeof(Length));
			_duration = (Duration)info.GetValue(DurationSerializedFieldName, typeof(Duration));
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue(LengthSerializedFieldName, _length, typeof(Length));
			info.AddValue(DurationSerializedFieldName, _duration, typeof(Duration));
		}

		/////////////////////////////////////////////////////////////////////////////
		// OPERATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Speed operator +(Speed first, Speed second) {
			return Length.From(first.To(MeterPerSecond) + second.To(MeterPerSecond), Length.Meter) / Duration.Second;
		}

		public static Speed operator -(Speed first, Speed second) {
			return Length.From(first.To(MeterPerSecond) - second.To(MeterPerSecond), Length.Meter) / Duration.Second;
		}

		public static Speed operator *(Speed first, double second) {
			return new Speed(first._length * second, first._duration);
		}

		public static Speed operator *(double first, Speed second) {
			return new Speed(first * second._length, second._duration);
		}

		public static double operator /(Speed first, Speed second) {
			return first.To(second);
		}

		public static Speed operator /(Speed first, double second) {
			return new Speed(first._length / second, first._duration);
		}

		/////////////////////////////////////////////////////////////////////////////
		// MUTATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Length operator *(Speed speed, Duration duration) {
			return speed._length * duration.To(speed._duration);
		}

		public static Momentum operator *(Speed first, Mass second) {
			return Momentum.From(second, first);
		}

		/////////////////////////////////////////////////////////////////////////////
		// EQUALITY
		/////////////////////////////////////////////////////////////////////////////
		public bool Equals(Speed other) {
			return _length.Equals(other._length) && _duration.Equals(other._duration);
		}

		public bool Equals(Speed other, Speed delta) {
			return Math.Abs(To(MeterPerSecond) - other.To(MeterPerSecond)) < Math.Abs(delta.To(MeterPerSecond));
		}

		public override bool Equals(object obj) {
			return obj is Speed other && Equals(other);
		}

		[SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
		public override int GetHashCode() {
			return _length.GetHashCode() ^ _duration.GetHashCode();
		}

		public static bool operator ==(Speed first, Speed second) {
			return first.Equals(second);
		}

		public static bool operator !=(Speed first, Speed second) {
			return !first.Equals(second);
		}

		public int CompareTo(Speed other) {
			return To(MeterPerSecond).CompareTo(other.To(MeterPerSecond));
		}

		/////////////////////////////////////////////////////////////////////////////
		// TO STRING
		/////////////////////////////////////////////////////////////////////////////
		public override string ToString() {
			return ToStringMetersPerSecond();
		}

		public string ToStringMetersPerSecond() {
			return $"{To(MeterPerSecond):F2}{UnitString}";
		}

		public string ToStringKilometersPerHour() {
			return $"{To(KilometerPerHour):F2}km/h";
		}
	}
}
