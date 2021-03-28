﻿namespace Software10101.Units {
	public readonly struct Density {
		private const string UnitString = "g/cm³";

		public static readonly Density ZeroDensity = 0.0; // g/cm³
		public static readonly Density Water       = 1.0; // g/cm³
		public static readonly Density MaxDensity  = double.MaxValue;

		private readonly Mass _mass;
		private readonly Volume _volume;

		/////////////////////////////////////////////////////////////////////////////
		// BOXING
		/////////////////////////////////////////////////////////////////////////////
		public Density(double d) {
			_mass = Mass.From(d, Mass.Gram);
			_volume = Volume.CubicCentimeter;
		}

		public Density(Mass m, Volume v) {
			_mass = m;
			_volume = v;
		}

		public static Density From(double p) {
			return new Density(p);
		}

		public static Density From(Mass m, Volume v) {
			return new Density(m, v);
		}

		public static implicit operator Density(double p) {
			return From(p);
		}

		/////////////////////////////////////////////////////////////////////////////
		// UN-BOXING
		/////////////////////////////////////////////////////////////////////////////
		public double To(Density unit) {
			return _mass.To(unit._mass) / _volume.To(unit._volume);
		}

		public static implicit operator double(Density p) {
			return p.To(Water);
		}

		/////////////////////////////////////////////////////////////////////////////
		// OPERATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Density operator *(Density first, double second) {
			return new Density(first._mass * second, first._volume);
		}

		public static Density operator *(double first, Density second) {
			return new Density(second._mass * first, second._volume);
		}

		public static double operator /(Density first, Density second) {
			return first.To(Water) / second.To(Water);
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
