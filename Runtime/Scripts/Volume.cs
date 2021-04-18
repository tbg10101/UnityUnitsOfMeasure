using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using UnityEngine;

namespace Software10101.Units {
	// ideally the entire struct would be readonly
	// however, Unity does not serialize readonly fields
	// this is unfortunate but we can enforce immutability by not exposing the value publicly
	[Serializable]
	public struct Volume : IEquatable<Volume>, IComparable<Volume>, ISerializable {
		private const string UnitString = "km³";

		public static readonly Volume ZeroVolume      = new Volume();
		public static readonly Volume CubicCentimeter = new Volume { _kmCubed = 0.000000000000001 };
		public static readonly Volume Milliliter      = CubicCentimeter;
		public static readonly Volume Liter           = new Volume { _kmCubed = 0.000000000001 };
		public static readonly Volume CubicMeter      = new Volume { _kmCubed = 0.000000001 };
		public static readonly Volume CubicKilometer  = new Volume { _kmCubed = 1.0 };

		[SerializeField]
		internal double _kmCubed;

		/////////////////////////////////////////////////////////////////////////////
		// BOXING
		/////////////////////////////////////////////////////////////////////////////
		public Volume(double v, Volume unit) {
			_kmCubed = v * unit._kmCubed;
		}

		public static Volume From(double v, Volume unit) {
			return new Volume(v, unit);
		}

		/////////////////////////////////////////////////////////////////////////////
		// UN-BOXING
		/////////////////////////////////////////////////////////////////////////////
		public double To(Volume unit) {
			return _kmCubed / unit._kmCubed;
		}

		/////////////////////////////////////////////////////////////////////////////
		// SERIALIZATION
		/////////////////////////////////////////////////////////////////////////////
		private const string KmCubedSerializedFieldName = "kmCubed";

		public Volume(SerializationInfo info, StreamingContext context) {
			_kmCubed = info.GetDouble(KmCubedSerializedFieldName);
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue(KmCubedSerializedFieldName, _kmCubed);
		}

		/////////////////////////////////////////////////////////////////////////////
		// OPERATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Volume operator +(Volume first, Volume second) {
			return new Volume { _kmCubed = first._kmCubed + second._kmCubed };
		}

		public static Volume operator -(Volume first, Volume second) {
			return new Volume { _kmCubed = first._kmCubed - second._kmCubed };
		}

		public static Volume operator *(Volume first, double second) {
			return new Volume { _kmCubed = first._kmCubed * second };
		}

		public static Volume operator *(double first, Volume second) {
			return new Volume { _kmCubed = first * second._kmCubed };
		}

		public static double operator /(Volume first, Volume second) {
			return first._kmCubed / second._kmCubed;
		}

		public static Volume operator /(Volume first, double second) {
			return new Volume { _kmCubed = first._kmCubed / second };
		}

		/////////////////////////////////////////////////////////////////////////////
		// MUTATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Area operator /(Volume volume, Length length) {
			return new Area { _kmSquared = volume._kmCubed / length._kilometers };
		}

		public static Length operator /(Volume volume, Area area) {
			return new Length { _kilometers = volume._kmCubed / area._kmSquared };
		}

		public static Mass operator *(Volume volume, Density density) {
			return density._mass * volume.To(density._volume);
		}

		public static VolumetricFlowRate operator /(Volume first, Duration second) {
			return VolumetricFlowRate.From(first, second);
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

		[SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
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
