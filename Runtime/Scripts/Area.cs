using System;
using System.Runtime.Serialization;

namespace Software10101.Units {
	[Serializable]
	public readonly struct Area : IEquatable<Area>, IComparable<Area>, ISerializable {
		private const string UnitString = "km²";

		public static readonly Area ZeroArea        = 0.0; // km²
		public static readonly Area SquareKilometer = 1.0; // km²
		public static readonly Area MaxArea         = double.MaxValue;

		private readonly double _kmSquared;

		/////////////////////////////////////////////////////////////////////////////
		// BOXING
		/////////////////////////////////////////////////////////////////////////////
		public Area(double a) {
			_kmSquared = a;
		}

		public Area(double a, Area unit) {
			_kmSquared = a * unit._kmSquared;
		}

		public static Area From(double a) {
			return new Area(a);
		}

		public static Area From(double a, Area unit) {
			return new Area(a, unit);
		}

		public static implicit operator Area(double a) {
			return From(a);
		}

		/////////////////////////////////////////////////////////////////////////////
		// UN-BOXING
		/////////////////////////////////////////////////////////////////////////////
		public double To(Area unit) {
			return _kmSquared / unit;
		}

		public static implicit operator double(Area a) {
			return a._kmSquared;
		}

		/////////////////////////////////////////////////////////////////////////////
		// SERIALIZATION
		/////////////////////////////////////////////////////////////////////////////
		public Area(SerializationInfo info, StreamingContext context) {
			_kmSquared = info.GetDouble("kmSquared");
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue("kmSquared", _kmSquared);
		}

		/////////////////////////////////////////////////////////////////////////////
		// OPERATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Area operator +(Area first, Area second) {
			return first._kmSquared + second._kmSquared;
		}

		public static Area operator +(Area first, double second) {
			return first._kmSquared + second;
		}

		public static Area operator +(double first, Area second) {
			return first + second._kmSquared;
		}

		public static Area operator -(Area first, Area second) {
			return first._kmSquared - second._kmSquared;
		}

		public static Area operator -(Area first, double second) {
			return first._kmSquared - second;
		}

		public static Area operator -(double first, Area second) {
			return first - second._kmSquared;
		}

		public static Area operator *(Area first, double second) {
			return first._kmSquared * second;
		}

		public static Area operator *(double first, Area second) {
			return first * second._kmSquared;
		}

		public static double operator /(Area first, Area second) {
			return first._kmSquared / second._kmSquared;
		}

		public static Area operator /(Area first, double second) {
			return first._kmSquared / second;
		}

		/////////////////////////////////////////////////////////////////////////////
		// MUTATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Length operator /(Area area, Length length) {
			return area._kmSquared / length.To(Length.Kilometer);
		}

		public static Volume operator *(Area first, Length second) {
			return first._kmSquared * second.To(Length.Kilometer);
		}

		/////////////////////////////////////////////////////////////////////////////
		// EQUALITY
		/////////////////////////////////////////////////////////////////////////////
		public bool Equals(Area other) {
			return _kmSquared.Equals(other._kmSquared);
		}

		public bool Equals(Area other, Area delta) {
			return Math.Abs(_kmSquared - other._kmSquared) < Math.Abs(delta._kmSquared);
		}

		public override bool Equals(object obj) {
			return obj is Area other && Equals(other);
		}

		public override int GetHashCode() {
			return _kmSquared.GetHashCode();
		}

		public static bool operator ==(Area first, Area second) {
			return first.Equals(second);
		}

		public static bool operator !=(Area first, Area second) {
			return !first.Equals(second);
		}

		public int CompareTo(Area other) {
			return _kmSquared.CompareTo(other._kmSquared);
		}

		/////////////////////////////////////////////////////////////////////////////
		// TO STRING
		/////////////////////////////////////////////////////////////////////////////
		public override string ToString() {
			return ToStringSquareKilometers();
		}

		public string ToStringSquareKilometers() {
			return $"{To(SquareKilometer):F2}{UnitString}";
		}
	}
}
