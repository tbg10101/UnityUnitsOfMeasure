namespace Software10101.Units {
	public readonly struct Momentum {
		private const string UnitString = "kg⋅m/s";

		public static readonly Momentum ZeroMomentum           = 0.0; // kg⋅m/s
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
