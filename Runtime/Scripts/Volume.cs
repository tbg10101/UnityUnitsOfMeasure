using System;
using System.Runtime.Serialization;

namespace Software10101.Units {
	[Serializable]
	public readonly struct Volume : IEquatable<Volume>, IComparable<Volume>, ISerializable {
		private const string UnitString = "km³";

		public static readonly Volume ZeroVolume      = 0.0;               // km³
		public static readonly Volume CubicCentimeter = 0.000000000000001; // km³
		public static readonly Volume CubicMeter      = 0.000000001;       // km³
		public static readonly Volume CubicKilometer  = 1.0;               // km³
		public static readonly Volume MaxVolume       = double.MaxValue;

		private readonly double _kmCubed;

		/////////////////////////////////////////////////////////////////////////////
		// BOXING
		/////////////////////////////////////////////////////////////////////////////
		public Volume(double km3) {
			_kmCubed = km3;
		}

		public Volume(double v, Volume unit) {
			_kmCubed = v * unit;
		}

		public static Volume From(double km3) {
			return new Volume(km3);
		}

		public static Volume From(double v, Volume unit) {
			return new Volume(v, unit);
		}

		public static implicit operator Volume(double km3) {
			return From(km3);
		}

		/////////////////////////////////////////////////////////////////////////////
		// UN-BOXING
		/////////////////////////////////////////////////////////////////////////////
		public double To(Volume unit) {
			return _kmCubed / unit._kmCubed;
		}

		public static implicit operator double(Volume v) {
			return v._kmCubed;
		}

		/////////////////////////////////////////////////////////////////////////////
		// SERIALIZATION
		/////////////////////////////////////////////////////////////////////////////
		public Volume(SerializationInfo info, StreamingContext context) {
			_kmCubed = info.GetDouble("kmCubed");
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue("kmCubed", _kmCubed);
		}

		/////////////////////////////////////////////////////////////////////////////
		// OPERATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Volume operator +(Volume first, Volume second) {
			return first._kmCubed + second._kmCubed;
		}

		public static Volume operator +(Volume first, double second) {
			return first._kmCubed + second;
		}

		public static Volume operator +(double first, Volume second) {
			return first + second._kmCubed;
		}

		public static Volume operator -(Volume first, Volume second) {
			return first._kmCubed - second._kmCubed;
		}

		public static Volume operator -(Volume first, double second) {
			return first._kmCubed - second;
		}

		public static Volume operator -(double first, Volume second) {
			return first - second._kmCubed;
		}

		public static Volume operator *(Volume first, double second) {
			return first._kmCubed * second;
		}

		public static Volume operator *(double first, Volume second) {
			return first * second._kmCubed;
		}

		public static VolumetricFlowRate operator /(Volume first, Duration second) {
			return VolumetricFlowRate.From(first, second);
		}

		public static double operator /(Volume first, Volume second) {
			return first._kmCubed / second._kmCubed;
		}

		public static Volume operator /(Volume first, double second) {
			return first._kmCubed / second;
		}

		/////////////////////////////////////////////////////////////////////////////
		// MUTATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Area operator /(Volume volume, Length length) {
			return volume._kmCubed / length.To(Length.Kilometer);
		}

		public static Length operator /(Volume volume, Area area) {
			return volume._kmCubed / area.To(Area.SquareKilometer);
		}

		public static Mass operator *(Volume volume, Density density) {
			return density.To(Density.From(Mass.Kilogram, CubicMeter)) * volume.To(CubicMeter);
		}

		/////////////////////////////////////////////////////////////////////////////
		// EQUALITY
		/////////////////////////////////////////////////////////////////////////////
		public bool Equals(Volume other) {
			return _kmCubed.Equals(other._kmCubed);
		}

		public bool Equals(Volume other, Volume delta) {
			return Math.Abs(_kmCubed - other._kmCubed) < Math.Abs(delta._kmCubed);
		}

		public override bool Equals(object obj) {
			return obj is Volume other && Equals(other);
		}

		public override int GetHashCode() {
			return _kmCubed.GetHashCode();
		}

		public static bool operator ==(Volume first, Volume second) {
			return first.Equals(second);
		}

		public static bool operator !=(Volume first, Volume second) {
			return !first.Equals(second);
		}

		public int CompareTo(Volume other) {
			return _kmCubed.CompareTo(other._kmCubed);
		}

		/////////////////////////////////////////////////////////////////////////////
		// TO STRING
		/////////////////////////////////////////////////////////////////////////////
		public override string ToString() {
			return ToStringCubicKilometers();
		}

		public string ToStringCubicCentimeters() {
			return $"{To(CubicCentimeter):F2}cm³";
		}

		public string ToStringCubicMeters() {
			return $"{To(CubicMeter):F2}m³";
		}

		public string ToStringCubicKilometers() {
			return $"{_kmCubed:F2}{UnitString}";
		}
	}
}
