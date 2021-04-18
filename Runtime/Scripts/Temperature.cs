using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using UnityEngine;

namespace Software10101.Units {
	// ideally the entire struct would be readonly
	// however, Unity does not serialize readonly fields
	// this is unfortunate but we can enforce immutability by not exposing the value publicly
	[Serializable]
	public struct Temperature : IEquatable<Temperature>, IComparable<Temperature>, ISerializable {
		private const string UnitString = "K";

		public static Temperature AbsoluteZero = new Temperature();
		public static Temperature Freezing     = new Temperature { _kelvin = 273.15 };
		public static Temperature Boiling      = new Temperature { _kelvin = 373.15 };

		[SerializeField]
		internal double _kelvin;

		/////////////////////////////////////////////////////////////////////////////
		// BOXING
		/////////////////////////////////////////////////////////////////////////////
		public static Temperature FromKelvin(double k) {
			return new Temperature { _kelvin = k};
		}

		public static Temperature FromCelsius(double c) {
			return FromKelvin(c + 273.15);
		}

		public static Temperature FromFahrenheit(double f) {
			return FromKelvin((f + 459.67) * (5.0 / 9.0));
		}

		/////////////////////////////////////////////////////////////////////////////
		// UN-BOXING
		/////////////////////////////////////////////////////////////////////////////
		public double ToKelvin() {
			return _kelvin;
		}

		public double ToCelsius() {
			return _kelvin - 273.15;
		}

		public double ToFahrenheit() {
			return _kelvin * (9.0 / 5.0) - 459.67;
		}

		/////////////////////////////////////////////////////////////////////////////
		// SERIALIZATION
		/////////////////////////////////////////////////////////////////////////////
		private const string KelvinSerializedFieldName = "kelvin";

		public Temperature(SerializationInfo info, StreamingContext context) {
			_kelvin = info.GetDouble(KelvinSerializedFieldName);
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue(KelvinSerializedFieldName, _kelvin);
		}

		/////////////////////////////////////////////////////////////////////////////
		// OPERATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Temperature operator +(Temperature first, Temperature second) {
			return new Temperature { _kelvin = first._kelvin + second._kelvin };
		}

		public static Temperature operator -(Temperature first, Temperature second) {
			return new Temperature { _kelvin = first._kelvin - second._kelvin };
		}

		public static Temperature operator *(Temperature first, double second) {
			return new Temperature { _kelvin = first._kelvin * second };
		}

		public static Temperature operator *(double first, Temperature second) {
			return new Temperature { _kelvin = first * second._kelvin };
		}

		public static double operator /(Temperature first, Temperature second) {
			return first._kelvin / second._kelvin;
		}

		public static Temperature operator /(Temperature first, double second) {
			return new Temperature { _kelvin = first._kelvin / second };
		}

		/////////////////////////////////////////////////////////////////////////////
		// EQUALITY
		/////////////////////////////////////////////////////////////////////////////
		public bool Equals(Temperature other) {
			return _kelvin.Equals(other._kelvin);
		}

		public bool Equals(Temperature other, Temperature delta) {
			return Math.Abs(_kelvin - other._kelvin) < Math.Abs(delta._kelvin);
		}

		public override bool Equals(object obj) {
			return obj is Temperature other && Equals(other);
		}

		[SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
		public override int GetHashCode() {
			return _kelvin.GetHashCode();
		}

		public static bool operator ==(Temperature first, Temperature second) {
			return first.Equals(second);
		}

		public static bool operator !=(Temperature first, Temperature second) {
			return !first.Equals(second);
		}

		public int CompareTo(Temperature other) {
			return _kelvin.CompareTo(other._kelvin);
		}

		/////////////////////////////////////////////////////////////////////////////
		// TO STRING
		/////////////////////////////////////////////////////////////////////////////
		public override string ToString() {
			return ToStringKelvin();
		}

		public string ToStringKelvin() {
			return $"{ToKelvin():F2}{UnitString}";
		}

		public string ToStringCelsius() {
			return $"{ToCelsius():F2}°C";
		}

		public string ToStringFahrenheit() {
			return $"{ToFahrenheit():F2}°F";
		}
	}
}
