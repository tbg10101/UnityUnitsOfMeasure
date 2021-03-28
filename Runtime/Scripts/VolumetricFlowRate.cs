namespace Software10101.Units {
	public readonly struct VolumetricFlowRate {
		private const string UnitString = "m³/s";

		public static readonly VolumetricFlowRate ZeroRate            = 0.0; // m³/s
		public static readonly VolumetricFlowRate MeterCubedPerSecond = 1.0; // m³/s
		public static readonly VolumetricFlowRate MaxRate             = double.MaxValue;

		private readonly Volume _volume;
		private readonly Duration _duration;

		/////////////////////////////////////////////////////////////////////////////
		// BOXING
		/////////////////////////////////////////////////////////////////////////////
		public VolumetricFlowRate(double q) {
			_volume = Volume.From(q, Volume.CubicMeter);
			_duration = Duration.Second;
		}

		public VolumetricFlowRate(Volume v, Duration d) {
			_volume = v;
			_duration = d;
		}

		public static VolumetricFlowRate From(double q) {
			return new VolumetricFlowRate(q);
		}

		public static VolumetricFlowRate From(Volume v, Duration d) {
			return new VolumetricFlowRate(v, d);
		}

		public static implicit operator VolumetricFlowRate(double q) {
			return From(q);
		}

		/////////////////////////////////////////////////////////////////////////////
		// UN-BOXING
		/////////////////////////////////////////////////////////////////////////////
		public double To(VolumetricFlowRate unit) {
			return _volume.To(unit._volume) / _duration.To(unit._duration);
		}

		public static implicit operator double(VolumetricFlowRate q) {
			return q.To(MeterCubedPerSecond);
		}

		/////////////////////////////////////////////////////////////////////////////
		// OPERATORS
		/////////////////////////////////////////////////////////////////////////////
		public static VolumetricFlowRate operator +(VolumetricFlowRate first, VolumetricFlowRate second) {
			return first.To(MeterCubedPerSecond) + second.To(MeterCubedPerSecond);
		}

		public static VolumetricFlowRate operator +(VolumetricFlowRate first, double second) {
			return first.To(MeterCubedPerSecond) + second;
		}

		public static VolumetricFlowRate operator +(double first, VolumetricFlowRate second) {
			return first + second.To(MeterCubedPerSecond);
		}

		public static VolumetricFlowRate operator -(VolumetricFlowRate first, VolumetricFlowRate second) {
			return first.To(MeterCubedPerSecond) - second.To(MeterCubedPerSecond);
		}

		public static VolumetricFlowRate operator -(VolumetricFlowRate first, double second) {
			return first.To(MeterCubedPerSecond) - second;
		}

		public static VolumetricFlowRate operator -(double first, VolumetricFlowRate second) {
			return first - second.To(MeterCubedPerSecond);
		}

		public static VolumetricFlowRate operator *(VolumetricFlowRate first, double second) {
			return first.To(MeterCubedPerSecond) * second;
		}

		public static VolumetricFlowRate operator *(double first, VolumetricFlowRate second) {
			return first * second.To(MeterCubedPerSecond);
		}

		public static double operator /(VolumetricFlowRate first, VolumetricFlowRate second) {
			return first.To(MeterCubedPerSecond) / second.To(MeterCubedPerSecond);
		}

		public static VolumetricFlowRate operator /(VolumetricFlowRate first, double second) {
			return first.To(MeterCubedPerSecond) / second;
		}

		/////////////////////////////////////////////////////////////////////////////
		// MUTATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Volume operator *(VolumetricFlowRate volumetricFlowRate, Duration duration) {
			return volumetricFlowRate._volume * (duration / volumetricFlowRate._duration);
		}

		/////////////////////////////////////////////////////////////////////////////
		// TO STRING
		/////////////////////////////////////////////////////////////////////////////
		public override string ToString() {
			return ToStringMetersCubedPerSecond();
		}

		public string ToStringMetersCubedPerSecond() {
			return $"{To(MeterCubedPerSecond):F2}{UnitString}";
		}
	}
}
