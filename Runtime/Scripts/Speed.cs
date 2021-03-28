namespace Software10101.Units {
	public readonly struct Speed {
		private const string UnitString = "m/s";

		public static readonly Speed ZeroSpeed        = 0.0; // m/s
		public static readonly Speed MeterPerSecond   = Length.Meter / Duration.Second;
		public static readonly Speed KilometerPerHour = Length.Kilometer / Duration.Hour;
		public static readonly Speed C                = Length.From(299792458.0, Length.Meter) / Duration.Second;
		public static readonly Speed MaxSpeed         = double.MaxValue;

		private readonly Length _length;
		private readonly Duration _duration;

		/////////////////////////////////////////////////////////////////////////////
		// BOXING
		/////////////////////////////////////////////////////////////////////////////
		public Speed(double s) {
			_length = Length.From(s, Length.Meter);
			_duration = Duration.Second;
		}

		public Speed(Length l, Duration d) {
			_length = l;
			_duration = d;
		}

		public static Speed From(double s) {
			return new Speed(s);
		}

		public static Speed From(Length l, Duration d) {
			return new Speed(l, d);
		}

		public static implicit operator Speed(double s) {
			return From(s);
		}

		/////////////////////////////////////////////////////////////////////////////
		// UN-BOXING
		/////////////////////////////////////////////////////////////////////////////
		public double To(Speed unit) {
			return _length.To(unit._length) / _duration.To(unit._duration);
		}

		public static implicit operator double(Speed s) {
			return s.To(MeterPerSecond);
		}

		/////////////////////////////////////////////////////////////////////////////
		// OPERATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Speed operator +(Speed first, Speed second) {
			return first.To(MeterPerSecond) + second.To(MeterPerSecond);
		}

		public static Speed operator +(Speed first, double second) {
			return first.To(MeterPerSecond) + second;
		}

		public static Speed operator +(double first, Speed second) {
			return first + second.To(MeterPerSecond);
		}

		public static Speed operator -(Speed first, Speed second) {
			return first.To(MeterPerSecond) - second.To(MeterPerSecond);
		}

		public static Speed operator -(Speed first, double second) {
			return first.To(MeterPerSecond) - second;
		}

		public static Speed operator -(double first, Speed second) {
			return first - second.To(MeterPerSecond);
		}

		public static Speed operator *(Speed first, double second) {
			return first.To(MeterPerSecond) * second;
		}

		public static Speed operator *(double first, Speed second) {
			return first * second.To(MeterPerSecond);
		}

		public static double operator /(Speed first, Speed second) {
			return first.To(MeterPerSecond) / second.To(MeterPerSecond);
		}

		public static Speed operator /(Speed first, double second) {
			return first.To(MeterPerSecond) / second;
		}

		/////////////////////////////////////////////////////////////////////////////
		// MUTATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Length operator *(Speed speed, Duration duration) {
			return speed._length * (duration / speed._duration);
		}

		/////////////////////////////////////////////////////////////////////////////
		// TO STRING
		/////////////////////////////////////////////////////////////////////////////
		public override string ToString() {
			return ToStringMetersPerSecond();
		}

		public string ToStringMetersPerSecond() {
			return $"{To(MeterPerSecond):F2}{UnitString}";
		}

		public string ToStringKilometersPerHour() {
			return $"{To(KilometerPerHour):F2}km/h";
		}
	}
}
