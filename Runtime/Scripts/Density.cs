using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using UnityEngine;

namespace Software10101.Units {
	// ideally the entire struct would be readonly
	// however, Unity does not serialize readonly fields
	// this is unfortunate but we can enforce immutability by not exposing the value publicly
	[Serializable]
	public struct Density : IEquatable<Density>, IComparable<Density>, ISerializable {
		private const string UnitString = "g/cm³";

		public static readonly Density ZeroDensity = new Density { _mass = Mass.ZeroMass, _volume = Volume.CubicCentimeter };
		public static readonly Density Water       = new Density { _mass = Mass.Gram, _volume = Volume.CubicCentimeter };

		[SerializeField]
		internal Mass _mass;

		[SerializeField]
		internal Volume _volume;

		/////////////////////////////////////////////////////////////////////////////
		// BOXING
		/////////////////////////////////////////////////////////////////////////////
		public Density(Mass m, Volume v) {
			_mass = m;
			_volume = v;
		}

		public static Density From(Mass m, Volume v) {
			return new Density(m, v);
		}

		/////////////////////////////////////////////////////////////////////////////
		// UN-BOXING
		/////////////////////////////////////////////////////////////////////////////
		public double To(Density unit) {
			return _mass.To(unit._mass) / _volume.To(unit._volume);
		}

		/////////////////////////////////////////////////////////////////////////////
		// SERIALIZATION
		/////////////////////////////////////////////////////////////////////////////
		private const string MassSerializedFieldName = "mass";
		private const string VolumeSerializedFieldName = "volume";

		public Density(SerializationInfo info, StreamingContext context) {
			_mass = (Mass)info.GetValue(MassSerializedFieldName, typeof(Mass));
			_volume = (Volume)info.GetValue(VolumeSerializedFieldName, typeof(Volume));
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue(MassSerializedFieldName, _mass, typeof(Mass));
			info.AddValue(VolumeSerializedFieldName, _volume, typeof(Volume));
		}

		/////////////////////////////////////////////////////////////////////////////
		// OPERATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Density operator *(Density first, double second) {
			return new Density(first._mass * second, first._volume);
		}

		public static Density operator *(double first, Density second) {
			return new Density(first * second._mass, second._volume);
		}

		public static double operator /(Density first, Density second) {
			return first.To(second);
		}

		public static Density operator /(Density first, double second) {
			return new Density(first._mass / second, first._volume);
		}

		/////////////////////////////////////////////////////////////////////////////
		// MUTATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Mass operator *(Density density, Volume volume) {
			return density._mass * (volume / density._volume);
		}

		/////////////////////////////////////////////////////////////////////////////
		// EQUALITY
		/////////////////////////////////////////////////////////////////////////////
		public bool Equals(Density other) {
			return _mass.Equals(other._mass) && _volume.Equals(other._volume);
		}

		public bool Equals(Density other, Density delta) {
			return Math.Abs(To(Water) - other.To(Water)) < Math.Abs(delta.To(Water));
		}

		public override bool Equals(object obj) {
			return obj is Density other && Equals(other);
		}

		[SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
		public override int GetHashCode() {
			return _mass.GetHashCode() ^ _volume.GetHashCode();
		}

		public static bool operator ==(Density first, Density second) {
			return first.Equals(second);
		}

		public static bool operator !=(Density first, Density second) {
			return !first.Equals(second);
		}

		public int CompareTo(Density other) {
			return To(Water).CompareTo(other.To(Water));
		}

		/////////////////////////////////////////////////////////////////////////////
		// TO STRING
		/////////////////////////////////////////////////////////////////////////////
		public override string ToString() {
			return ToStringGramsPerCubicCentimeter();
		}

		public string ToStringGramsPerCubicCentimeter() {
			return $"{To(Water):F2}{UnitString}";
		}
	}
}
