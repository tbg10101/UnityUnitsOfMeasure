using System;
using System.Runtime.Serialization;

namespace Software10101.Units {
	[Serializable]
	public readonly struct Momentum : IEquatable<Momentum>, IComparable<Momentum>, ISerializable {
		private const string UnitString = "kg⋅m/s";

		public static readonly Momentum ZeroMomentum           = 0.0;                                  // kg⋅m/s
		public static readonly Momentum KilogramMeterPerSecond = Mass.Kilogram * Speed.MeterPerSecond; // kg⋅m/s
		public static readonly Momentum MaxMomentum            = double.MaxValue;

		private readonly Mass _mass;
		private readonly Speed _speed;

		/////////////////////////////////////////////////////////////////////////////
		// BOXING
		/////////////////////////////////////////////////////////////////////////////
		public Momentum(double d) {
			_mass = Mass.From(d, Mass.Gram);
			_speed = Speed.MeterPerSecond;
		}

		public Momentum(Mass m, Speed s) {
			_mass = m;
			_speed = s;
		}

		public static Momentum From(double p) {
			return new Momentum(p);
		}

		public static Momentum From(Mass m, Speed s) {
			return new Momentum(m, s);
		}

		public static implicit operator Momentum(double p) {
			return From(p);
		}

		/////////////////////////////////////////////////////////////////////////////
		// UN-BOXING
		/////////////////////////////////////////////////////////////////////////////
		public double To(Momentum unit) {
			return _mass.To(unit._mass) / _speed.To(unit._speed);
		}

		public static implicit operator double(Momentum p) {
			return p.To(KilogramMeterPerSecond);
		}

		/////////////////////////////////////////////////////////////////////////////
		// SERIALIZATION
		/////////////////////////////////////////////////////////////////////////////
		public Momentum(SerializationInfo info, StreamingContext context) {
			_mass = (Mass)info.GetValue("mass", typeof(Mass));
			_speed = (Speed)info.GetValue("speed", typeof(Speed));
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue("mass", _mass, typeof(Mass));
			info.AddValue("speed", _speed, typeof(Speed));
		}

		/////////////////////////////////////////////////////////////////////////////
		// OPERATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Momentum operator *(Momentum first, double second) {
			return new Momentum(first._mass * second, first._speed);
		}

		public static Momentum operator *(double first, Momentum second) {
			return new Momentum(second._mass * first, second._speed);
		}

		public static double operator /(Momentum first, Momentum second) {
			return first.To(KilogramMeterPerSecond) / second.To(KilogramMeterPerSecond);
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
			return new Speed(momentum._speed * (momentum._mass / mass));
		}

		/////////////////////////////////////////////////////////////////////////////
		// EQUALITY
		/////////////////////////////////////////////////////////////////////////////
		public bool Equals(Momentum other) {
			return _mass.Equals(other._mass) && _speed.Equals(other._speed);
		}

		public bool Equals(Momentum other, Momentum delta) {
			return Math.Abs(To(KilogramMeterPerSecond) - other.To(KilogramMeterPerSecond)) < Math.Abs(delta.To(KilogramMeterPerSecond));
		}

		public override bool Equals(object obj) {
			return obj is Momentum other && Equals(other);
		}

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
			return To(KilogramMeterPerSecond).CompareTo(other.To(KilogramMeterPerSecond));
		}

		/////////////////////////////////////////////////////////////////////////////
		// TO STRING
		/////////////////////////////////////////////////////////////////////////////
		public override string ToString() {
			return ToStringKilogramMetersPerSecond();
		}

		public string ToStringKilogramMetersPerSecond() {
			return $"{To(KilogramMeterPerSecond):F2}{UnitString}";
		}
	}
}
