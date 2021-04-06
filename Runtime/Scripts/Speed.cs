using System;
using System.Runtime.Serialization;

namespace Software10101.Units {
	[Serializable]
	public readonly struct Speed : IEquatable<Speed>, IComparable<Speed>, ISerializable {
		private const string UnitString = "m/s";

		public static readonly Speed ZeroSpeed        = 0.0; // m/s
		public static readonly Speed MeterPerSecond   = Length.Meter / Duration.Second;
		public static readonly Speed KilometerPerHour = Length.Kilometer / Duration.Hour;
		public static readonly Speed C                = Length.From(299792458.0, Length.Meter) / Duration.Second;
		public static readonly Speed MaxSpeed         = double.MaxValue;

		private readonly Length _length;
		private readonly Duration _duration;

		/////////////////////////////////////////////////////////////////////////////
		// BOXING
		/////////////////////////////////////////////////////////////////////////////
		public Speed(double s) {
			_length = Length.From(s, Length.Meter);
			_duration = Duration.Second;
		}

		public Speed(Length l, Duration d) {
			_length = l;
			_duration = d;
		}

		public static Speed From(double s) {
			return new Speed(s);
		}

		public static Speed From(Length l, Duration d) {
			return new Speed(l, d);
		}

		public static implicit operator Speed(double s) {
			return From(s);
		}

		/////////////////////////////////////////////////////////////////////////////
		// UN-BOXING
		/////////////////////////////////////////////////////////////////////////////
		public double To(Speed unit) {
			return _length.To(unit._length) / _duration.To(unit._duration);
		}

		public static implicit operator double(Speed s) {
			return s.To(MeterPerSecond);
		}

		/////////////////////////////////////////////////////////////////////////////
		// SERIALIZATION
		/////////////////////////////////////////////////////////////////////////////
		public Speed(SerializationInfo info, StreamingContext context) {
			_length = (Length)info.GetValue("length", typeof(Length));
			_duration = (Duration)info.GetValue("duration", typeof(Duration));
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue("length", _length, typeof(Length));
			info.AddValue("duration", _duration, typeof(Duration));
		}

		/////////////////////////////////////////////////////////////////////////////
		// OPERATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Speed operator +(Speed first, Speed second) {
			return first.To(MeterPerSecond) + second.To(MeterPerSecond);
		}

		public static Speed operator +(Speed first, double second) {
			return first.To(MeterPerSecond) + second;
		}

		public static Speed operator +(double first, Speed second) {
			return first + second.To(MeterPerSecond);
		}

		public static Speed operator -(Speed first, Speed second) {
			return first.To(MeterPerSecond) - second.To(MeterPerSecond);
		}

		public static Speed operator -(Speed first, double second) {
			return first.To(MeterPerSecond) - second;
		}

		public static Speed operator -(double first, Speed second) {
			return first - second.To(MeterPerSecond);
		}

		public static Speed operator *(Speed first, double second) {
			return first.To(MeterPerSecond) * second;
		}

		public static Speed operator *(double first, Speed second) {
			return first * second.To(MeterPerSecond);
		}

		public static double operator /(Speed first, Speed second) {
			return first.To(MeterPerSecond) / second.To(MeterPerSecond);
		}

		public static Speed operator /(Speed first, double second) {
			return first.To(MeterPerSecond) / second;
		}

		/////////////////////////////////////////////////////////////////////////////
		// MUTATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Length operator *(Speed speed, Duration duration) {
			return speed._length * (duration / speed._duration);
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
