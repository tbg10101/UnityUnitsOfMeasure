using System;

namespace Software10101.Units {
	public readonly struct Length : IEquatable<Length>, IComparable<Length> {
		private const string UnitString = "m";

		public static readonly Length ZeroLength       =             0.0;         // km
		public static readonly Length Micrometer       =             0.000000001; // km
		public static readonly Length Millimeter       =             0.000001;    // km
		public static readonly Length Centimeter       =             0.00001;     // km
		public static readonly Length Meter            =             0.001;       // km
		public static readonly Length Kilometer        =             1.0;         // km
		public static readonly Length EarthRadius      =          6371.0;         // km
		public static readonly Length SolarRadius      =        695700.0;         // km
		public static readonly Length AstronomicalUnit =     149597870.7;         // km
		public static readonly Length LightYear        = 9460730472580.8;         // km
		public static readonly Length MaxLength        = double.MaxValue;

		private readonly double _kilometers;

		/////////////////////////////////////////////////////////////////////////////
		// BOXING
		/////////////////////////////////////////////////////////////////////////////
		private Length(double km) {
			_kilometers = km;
		}

		private Length(double l, Length unit) {
			_kilometers = l * unit;
		}

		public static Length From(double km) {
			return new Length(km);
		}

		public static Length From(double l, Length unit) {
			return new Length(l, unit);
		}

		public static implicit operator Length(double km) {
			return From(km);
		}

		/////////////////////////////////////////////////////////////////////////////
		// UN-BOXING
		/////////////////////////////////////////////////////////////////////////////
		public double To(Length unit) {
			return _kilometers / unit._kilometers;
		}

		public static implicit operator double(Length l) {
			return l._kilometers;
		}

		/////////////////////////////////////////////////////////////////////////////
		// OPERATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Length operator +(Length first, Length second) {
			return first._kilometers + second._kilometers;
		}

		public static Length operator +(Length first, double second) {
			return first._kilometers + second;
		}

		public static Length operator +(double first, Length second) {
			return first + second._kilometers;
		}

		public static Length operator -(Length first, Length second) {
			return first._kilometers - second._kilometers;
		}

		public static Length operator -(Length first, double second) {
			return first._kilometers - second;
		}

		public static Length operator -(double first, Length second) {
			return first - second._kilometers;
		}

		public static Length operator *(Length first, double second) {
			return first._kilometers * second;
		}

		public static Length operator *(double first, Length second) {
			return first * second._kilometers;
		}

		public static double operator /(Length first, Length second) {
			return first._kilometers / second._kilometers;
		}

		public static Speed operator /(Length first, Duration second) {
			return Speed.From(first, second);
		}

		public static Length operator /(Length first, double second) {
			return first._kilometers / second;
		}

		/////////////////////////////////////////////////////////////////////////////
		// MUTATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Area operator *(Length first, Length second) {
			return first._kilometers * second._kilometers;
		}

		public static Volume operator *(Length first, Area second) {
			return first._kilometers * second.To(Area.SquareKilometer);
		}

		/////////////////////////////////////////////////////////////////////////////
		// EQUALITY
		/////////////////////////////////////////////////////////////////////////////
		public bool Equals(Length other) {
			return _kilometers.Equals(other._kilometers);
		}

		public bool Equals(Length other, Length delta) {
			return Math.Abs(_kilometers - other._kilometers) < Math.Abs(delta._kilometers);
		}

		public override bool Equals(object obj) {
			return obj is Length other && Equals(other);
		}

		public override int GetHashCode() {
			return _kilometers.GetHashCode();
		}

		public static bool operator ==(Length first, Length second) {
			return first.Equals(second);
		}

		public static bool operator !=(Length first, Length second) {
			return !first.Equals(second);
		}

		public int CompareTo(Length other) {
			return _kilometers.CompareTo(other._kilometers);
		}

		/////////////////////////////////////////////////////////////////////////////
		// TO STRING
		/////////////////////////////////////////////////////////////////////////////
		public override string ToString() {
			return Si.ToLargestSiString(_kilometers, UnitString, 2, 3, 0);
		}

		public string ToStringCentimeters() {
			return $"{To(Centimeter):F2}cm";
		}

		public string ToStringMeters() {
			return Si.ToLargestSiString(_kilometers, UnitString, 2, 3, 0, 0);
		}

		public string ToStringKilometers() {
			return Si.ToLargestSiString(_kilometers, UnitString, 2, 3, 3, 3);
		}

		public string ToStringEarthRadii() {
			return $"{To(EarthRadius):F2}R⊕";
		}

		public string ToStringSolarRadii() {
			return $"{To(SolarRadius):F2}R☉";
		}

		public string ToStringAstronomicalUnits() {
			return $"{To(AstronomicalUnit):F2}au";
		}

		public string ToStringLightYears() {
			return $"{To(LightYear):F2}ly";
		}
	}
}
