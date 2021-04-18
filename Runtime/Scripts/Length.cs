using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using UnityEngine;

namespace Software10101.Units {
	// ideally the entire struct would be readonly
	// however, Unity does not serialize readonly fields
	// this is unfortunate but we can enforce immutability by not exposing the value publicly
	[Serializable]
	public struct Length : IEquatable<Length>, IComparable<Length>, ISerializable {
		private const string UnitString = "m";

		public static readonly Length ZeroLength       = new Length();
		public static readonly Length Micrometer       = new Length { _kilometers =             0.000000001 };
		public static readonly Length Millimeter       = new Length { _kilometers =             0.000001 };
		public static readonly Length Centimeter       = new Length { _kilometers =             0.00001 };
		public static readonly Length Meter            = new Length { _kilometers =             0.001 };
		public static readonly Length Kilometer        = new Length { _kilometers =             1.0 };
		public static readonly Length EarthRadius      = new Length { _kilometers =          6371.0 };
		public static readonly Length SolarRadius      = new Length { _kilometers =        695700.0 };
		public static readonly Length LightSecond      = new Length { _kilometers =        299792.458 };
		public static readonly Length AstronomicalUnit = new Length { _kilometers =     149597870.7 };
		public static readonly Length LightYear        = new Length { _kilometers = 9460730472580.8 };

		[SerializeField]
		internal double _kilometers;

		/////////////////////////////////////////////////////////////////////////////
		// BOXING
		/////////////////////////////////////////////////////////////////////////////
		private Length(double l, Length unit) {
			_kilometers = l * unit._kilometers;
		}

		public static Length From(double l, Length unit) {
			return new Length(l, unit);
		}

		/////////////////////////////////////////////////////////////////////////////
		// UN-BOXING
		/////////////////////////////////////////////////////////////////////////////
		public double To(Length unit) {
			return _kilometers / unit._kilometers;
		}

		/////////////////////////////////////////////////////////////////////////////
		// SERIALIZATION
		/////////////////////////////////////////////////////////////////////////////
		private const string KilometersSerializedFieldName = "kilometers";

		public Length(SerializationInfo info, StreamingContext context) {
			_kilometers = info.GetDouble(KilometersSerializedFieldName);
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue(KilometersSerializedFieldName, _kilometers);
		}

		/////////////////////////////////////////////////////////////////////////////
		// OPERATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Length operator +(Length first, Length second) {
			return new Length { _kilometers = first._kilometers + second._kilometers };
		}

		public static Length operator -(Length first, Length second) {
			return new Length { _kilometers = first._kilometers - second._kilometers };
		}

		public static Length operator *(Length first, double second) {
			return new Length { _kilometers = first._kilometers * second };
		}

		public static Length operator *(double first, Length second) {
			return new Length { _kilometers = first * second._kilometers };
		}

		public static double operator /(Length first, Length second) {
			return first._kilometers / second._kilometers;
		}

		public static Length operator /(Length first, double second) {
			return new Length { _kilometers = first._kilometers / second };
		}

		/////////////////////////////////////////////////////////////////////////////
		// MUTATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Area operator *(Length first, Length second) {
			return new Area { _kmSquared = first._kilometers * second._kilometers };
		}

		public static Speed operator /(Length first, Duration second) {
			return new Speed(first, second);
		}

		public static Volume operator *(Length first, Area second) {
			return new Volume { _kmCubed = first._kilometers * second._kmSquared };
		}

		/////////////////////////////////////////////////////////////////////////////
		// EQUALITY
		/////////////////////////////////////////////////////////////////////////////
		public bool Equals(Length other) {
			return _kilometers.Equals(other._kilometers);
		}

		public bool Equals(Length other, Length delta) {
			return Math.Abs(_kilometers - other._kilometers) < Math.Abs(delta._kilometers);
		}

		public override bool Equals(object obj) {
			return obj is Length other && Equals(other);
		}

		[SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
		public override int GetHashCode() {
			return _kilometers.GetHashCode();
		}

		public static bool operator ==(Length first, Length second) {
			return first.Equals(second);
		}

		public static bool operator !=(Length first, Length second) {
			return !first.Equals(second);
		}

		public int CompareTo(Length other) {
			return _kilometers.CompareTo(other._kilometers);
		}

		/////////////////////////////////////////////////////////////////////////////
		// TO STRING
		/////////////////////////////////////////////////////////////////////////////
		public override string ToString() {
			return Si.ToLargestSiString(_kilometers, UnitString, 2, 3, 0);
		}

		public string ToStringCentimeters() {
			return $"{To(Centimeter):F2}cm";
		}

		public string ToStringMeters() {
			return Si.ToLargestSiString(_kilometers, UnitString, 2, 3, 0, 0);
		}

		public string ToStringKilometers() {
			return Si.ToLargestSiString(_kilometers, UnitString, 2, 3, 3, 3);
		}

		public string ToStringEarthRadii() {
			return $"{To(EarthRadius):F2}R⊕";
		}

		public string ToStringSolarRadii() {
			return $"{To(SolarRadius):F2}R☉";
		}

		public string ToStringAstronomicalUnits() {
			return $"{To(AstronomicalUnit):F2}au";
		}

		public string ToStringLightYears() {
			return $"{To(LightYear):F2}ly";
		}
	}
}
