using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using UnityEngine;

namespace Software10101.Units {
	// ideally the entire struct would be readonly
	// however, Unity does not serialize readonly fields
	// this is unfortunate but we can enforce immutability by not exposing the value publicly
	[Serializable]
	public struct Area : IEquatable<Area>, IComparable<Area>, ISerializable {
		private const string UnitString = "km²";

		public static readonly Area ZeroArea        = new Area();
		public static readonly Area SquareKilometer = new Area { _kmSquared = 1.0 };

		[SerializeField]
		internal double _kmSquared;

		/////////////////////////////////////////////////////////////////////////////
		// BOXING
		/////////////////////////////////////////////////////////////////////////////
		public Area(double a, Area unit) {
			_kmSquared = a * unit._kmSquared;
		}

		public static Area From(double a, Area unit) {
			return new Area(a, unit);
		}

		/////////////////////////////////////////////////////////////////////////////
		// UN-BOXING
		/////////////////////////////////////////////////////////////////////////////
		public double To(Area unit) {
			return _kmSquared / unit._kmSquared;
		}

		/////////////////////////////////////////////////////////////////////////////
		// SERIALIZATION
		/////////////////////////////////////////////////////////////////////////////
		private const string KmSquaredSerializedFieldName = "kmSquared";

		public Area(SerializationInfo info, StreamingContext context) : this() {
			_kmSquared = info.GetDouble(KmSquaredSerializedFieldName);
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue(KmSquaredSerializedFieldName, _kmSquared);
		}

		/////////////////////////////////////////////////////////////////////////////
		// OPERATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Area operator +(Area first, Area second) {
			return new Area { _kmSquared = first._kmSquared + second._kmSquared };
		}

		public static Area operator -(Area first, Area second) {
			return new Area { _kmSquared = first._kmSquared - second._kmSquared };
		}

		public static Area operator *(Area first, double second) {
			return new Area { _kmSquared = first._kmSquared * second };
		}

		public static Area operator *(double first, Area second) {
			return new Area { _kmSquared = first * second._kmSquared };
		}

		public static double operator /(Area first, Area second) {
			return first._kmSquared / second._kmSquared;
		}

		public static Area operator /(Area first, double second) {
			return new Area { _kmSquared = first._kmSquared / second };
		}

		/////////////////////////////////////////////////////////////////////////////
		// MUTATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Length operator /(Area area, Length length) {
			return new Length { _kilometers = area._kmSquared / length._kilometers };
		}

		public static Volume operator *(Area first, Length second) {
			return new Volume { _kmCubed = first._kmSquared * second._kilometers };
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

		[SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
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
