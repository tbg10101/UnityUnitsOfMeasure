using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using UnityEngine;

namespace Software10101.Units {
	// ideally the entire struct would be readonly
	// however, Unity does not serialize readonly fields
	// this is unfortunate but we can enforce immutability by not exposing the value publicly
	[Serializable]
	public struct Mass : IEquatable<Mass>, IComparable<Mass>, ISerializable {
		private const string UnitString = "g";

		public static readonly Mass ZeroMass  = new Mass();
		public static readonly Mass Gram      = new Mass { _kilograms =                               0.001 };
		public static readonly Mass Kilogram  = new Mass { _kilograms =                               1.0 };
		public static readonly Mass EarthMass = new Mass { _kilograms =       5972200000000000000000000.0 };
		public static readonly Mass SolarMass = new Mass { _kilograms = 1988550000000000000000000000000.0 };

		[SerializeField]
		internal double _kilograms;

		/////////////////////////////////////////////////////////////////////////////
		// BOXING
		/////////////////////////////////////////////////////////////////////////////
		public Mass(double m, Mass unit) {
			_kilograms = m * unit._kilograms;
		}

		public static Mass From(double m, Mass unit) {
			return new Mass(m, unit);
		}

		/////////////////////////////////////////////////////////////////////////////
		// UN-BOXING
		/////////////////////////////////////////////////////////////////////////////
		public double To(Mass unit) {
			return _kilograms / unit._kilograms;
		}

		/////////////////////////////////////////////////////////////////////////////
		// SERIALIZATION
		/////////////////////////////////////////////////////////////////////////////
		private const string KilogramsSerializedFieldName = "kilograms";

		public Mass(SerializationInfo info, StreamingContext context) {
			_kilograms = info.GetDouble(KilogramsSerializedFieldName);
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue(KilogramsSerializedFieldName, _kilograms);
		}

		/////////////////////////////////////////////////////////////////////////////
		// OPERATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Mass operator +(Mass first, Mass second) {
			return new Mass { _kilograms = first._kilograms + second._kilograms };
		}

		public static Mass operator -(Mass first, Mass second) {
			return new Mass { _kilograms = first._kilograms - second._kilograms };
		}

		public static Mass operator *(Mass first, double second) {
			return new Mass { _kilograms = first._kilograms * second };
		}

		public static Mass operator *(double first, Mass second) {
			return new Mass { _kilograms = first * second._kilograms };
		}

		public static double operator /(Mass first, Mass second) {
			return first._kilograms / second._kilograms;
		}

		public static Mass operator /(Mass first, double second) {
			return new Mass { _kilograms = first._kilograms / second };
		}

		/////////////////////////////////////////////////////////////////////////////
		// MUTATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Density operator /(Mass mass, Volume volume) {
			return new Density(mass, volume);
		}

		public static Momentum operator *(Mass first, Speed second) {
			return new Momentum(first, second);
		}

		/////////////////////////////////////////////////////////////////////////////
		// EQUALITY
		/////////////////////////////////////////////////////////////////////////////
		public bool Equals(Mass other) {
			return _kilograms.Equals(other._kilograms);
		}

		public bool Equals(Mass other, Mass delta) {
			return Math.Abs(_kilograms - other._kilograms) < Math.Abs(delta._kilograms);
		}

		public override bool Equals(object obj) {
			return obj is Mass other && Equals(other);
		}

		[SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
		public override int GetHashCode() {
			return _kilograms.GetHashCode();
		}

		public static bool operator ==(Mass first, Mass second) {
			return first.Equals(second);
		}

		public static bool operator !=(Mass first, Mass second) {
			return !first.Equals(second);
		}

		public int CompareTo(Mass other) {
			return _kilograms.CompareTo(other._kilograms);
		}

		/////////////////////////////////////////////////////////////////////////////
		// TO STRING
		/////////////////////////////////////////////////////////////////////////////
		public override string ToString() {
			return Si.ToLargestSiString(_kilograms, UnitString, 2, 3, 0);
		}

		public string ToStringGrams() {
			return Si.ToLargestSiString(_kilograms, UnitString, 2, 3, 0, 0);
		}

		public string ToStringKilograms() {
			return Si.ToLargestSiString(_kilograms, UnitString, 2, 3, 3, 3);
		}

		public string ToStringEarthMasses() {
			return $"{To(EarthMass):F2}M⊕";
		}

		public string ToStringSolarMasses() {
			return $"{To(SolarMass):F2}M☉";
		}
	}
}
