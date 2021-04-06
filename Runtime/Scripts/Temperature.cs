using System;
using System.Runtime.Serialization;

namespace Software10101.Units {
	[Serializable]
	public readonly struct Temperature : IEquatable<Temperature>, IComparable<Temperature>, ISerializable {
		private const string UnitString = "K";

		public static Temperature AbsoluteZero   =   0.0;  // K
		public static Temperature Freezing       = 273.15; // K
		public static Temperature Boiling        = 373.15; // K
		public static Temperature MaxTemperature = double.MaxValue;

		private readonly double _kelvin;

		/////////////////////////////////////////////////////////////////////////////
		// BOXING
		/////////////////////////////////////////////////////////////////////////////
		public Temperature(double k) {
			_kelvin = k;
		}

		public static Temperature FromKelvin(double k) {
			return new Temperature(k);
		}

		public static Temperature FromCelsius(double c) {
			return new Temperature(c + 273.15);
		}

		public static Temperature FromFahrenheit(double f) {
			return new Temperature((f + 459.67) * (5.0 / 9.0));
		}

		public static implicit operator Temperature(double k) {
			return FromKelvin(k);
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

		public static implicit operator double(Temperature t) {
			return t.ToKelvin();
		}

		/////////////////////////////////////////////////////////////////////////////
		// SERIALIZATION
		/////////////////////////////////////////////////////////////////////////////
		public Temperature(SerializationInfo info, StreamingContext context) {
			_kelvin = info.GetDouble("kelvin");
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue("kelvin", _kelvin);
		}

		/////////////////////////////////////////////////////////////////////////////
		// OPERATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Temperature operator +(Temperature first, Temperature second) {
			return first._kelvin + second._kelvin;
		}

		public static Temperature operator +(Temperature first, double second) {
			return first._kelvin + second;
		}

		public static Temperature operator +(double first, Temperature second) {
			return first + second._kelvin;
		}

		public static Temperature operator -(Temperature first, Temperature second) {
			return first._kelvin - second._kelvin;
		}

		public static Temperature operator -(Temperature first, double second) {
			return first._kelvin - second;
		}

		public static Temperature operator -(double first, Temperature second) {
			return first - second._kelvin;
		}

		public static Temperature operator *(Temperature first, double second) {
			return first._kelvin * second;
		}

		public static Temperature operator *(double first, Temperature second) {
			return first * second._kelvin;
		}

		public static Temperature operator /(Temperature first, double second) {
			return first._kelvin / second;
		}

		public static double operator /(Temperature first, Temperature second) {
			return first._kelvin / second._kelvin;
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
