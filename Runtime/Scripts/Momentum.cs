using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using UnityEngine;

namespace Software10101.Units {
	// ideally the entire struct would be readonly
	// however, Unity does not serialize readonly fields
	// this is unfortunate but we can enforce immutability by not exposing the value publicly
	[Serializable]
	public struct Momentum : IEquatable<Momentum>, IComparable<Momentum>, ISerializable {
		private const string UnitString = "kg⋅m/s";

		public static readonly Momentum ZeroMomentum = new Momentum { _mass = Mass.ZeroMass, _speed = Speed.MeterPerSecond };
		public static readonly Momentum NewtonSecond = new Momentum { _mass = Mass.Kilogram, _speed = Speed.MeterPerSecond };

		[SerializeField]
		internal Mass _mass;

		[SerializeField]
		internal Speed _speed;

		/////////////////////////////////////////////////////////////////////////////
		// BOXING
		/////////////////////////////////////////////////////////////////////////////
		public Momentum(Mass m, Speed s) {
			_mass = m;
			_speed = s;
		}

		public static Momentum From(Mass m, Speed s) {
			return new Momentum(m, s);
		}

		/////////////////////////////////////////////////////////////////////////////
		// UN-BOXING
		/////////////////////////////////////////////////////////////////////////////
		public double To(Momentum unit) {
			return _mass.To(unit._mass) / _speed.To(unit._speed);
		}

		/////////////////////////////////////////////////////////////////////////////
		// SERIALIZATION
		/////////////////////////////////////////////////////////////////////////////
		private const string MassSerializedFieldName = "mass";
		private const string SpeedSerializedFieldName = "speed";

		public Momentum(SerializationInfo info, StreamingContext context) {
			_mass = (Mass)info.GetValue(MassSerializedFieldName, typeof(Mass));
			_speed = (Speed)info.GetValue(SpeedSerializedFieldName, typeof(Speed));
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue(MassSerializedFieldName, _mass, typeof(Mass));
			info.AddValue(SpeedSerializedFieldName, _speed, typeof(Speed));
		}

		/////////////////////////////////////////////////////////////////////////////
		// OPERATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Momentum operator *(Momentum first, double second) {
			return new Momentum(first._mass * second, first._speed);
		}

		public static Momentum operator *(double first, Momentum second) {
			return new Momentum(first * second._mass, second._speed);
		}

		public static double operator /(Momentum first, Momentum second) {
			return first.To(second);
		}

		public static Momentum operator /(Momentum first, double second) {
			return new Momentum(first._mass / second, first._speed);
		}

		/////////////////////////////////////////////////////////////////////////////
		// MUTATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Mass operator /(Momentum momentum, Speed speed) {
			return momentum._mass * (momentum._speed / speed);
		}

		public static Speed operator /(Momentum momentum, Mass mass) {
			return momentum._speed * (momentum._mass / mass);
		}

		/////////////////////////////////////////////////////////////////////////////
		// EQUALITY
		/////////////////////////////////////////////////////////////////////////////
		public bool Equals(Momentum other) {
			return _mass.Equals(other._mass) && _speed.Equals(other._speed);
		}

		public bool Equals(Momentum other, Momentum delta) {
			return Math.Abs(To(NewtonSecond) - other.To(NewtonSecond)) < Math.Abs(delta.To(NewtonSecond));
		}

		public override bool Equals(object obj) {
			return obj is Momentum other && Equals(other);
		}

		[SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
		public override int GetHashCode() {
			return _mass.GetHashCode() ^ _speed.GetHashCode();
		}

		public static bool operator ==(Momentum first, Momentum second) {
			return first.Equals(second);
		}

		public static bool operator !=(Momentum first, Momentum second) {
			return !first.Equals(second);
		}

		public int CompareTo(Momentum other) {
			return To(NewtonSecond).CompareTo(other.To(NewtonSecond));
		}

		/////////////////////////////////////////////////////////////////////////////
		// TO STRING
		/////////////////////////////////////////////////////////////////////////////
		public override string ToString() {
			return ToStringKilogramMetersPerSecond();
		}

		public string ToStringKilogramMetersPerSecond() {
			return $"{To(NewtonSecond):F2}{UnitString}";
		}
	}
}
