using System;

namespace Software10101.Units {
	public readonly struct Duration : IEquatable<Duration>, IComparable<Duration> {
		private const string UnitString = "s";

		public static readonly Duration ZeroTime    =   0.0;           // s
		public static readonly Duration Nanosecond  =   0.000000001;   // s
		public static readonly Duration Microsecond =   0.000001;      // s
		public static readonly Duration Millisecond =   0.001;         // s
		public static readonly Duration Second      =   1.0;           // s
		public static readonly Duration Minute      =  60.0;           // s
		public static readonly Duration Hour        =  60.0 * Minute;
		public static readonly Duration Day         =  24.0 * Hour;
		public static readonly Duration Week        =   7.0 * Day;
		public static readonly Duration Year        = 365.24 * Day;
		public static readonly Duration Decade      =  10.0 * Year;
		public static readonly Duration Century     =  10.0 * Decade;
		public static readonly Duration Millennium  =  10.0 * Century;
		public static readonly Duration MaxTime     = double.MaxValue;

		private readonly double _seconds;

		/////////////////////////////////////////////////////////////////////////////
		// BOXING
		/////////////////////////////////////////////////////////////////////////////
		public Duration(double s) {
			_seconds = s;
		}

		public Duration(double d, Duration unit) {
			_seconds = d * unit._seconds;
		}

		public static Duration From(double s) {
			return new Duration(s);
		}

		public static Duration From(double d, Duration unit) {
			return new Duration(d, unit);
		}

		public static implicit operator Duration(double s) {
			return From(s);
		}

		/////////////////////////////////////////////////////////////////////////////
		// UN-BOXING
		/////////////////////////////////////////////////////////////////////////////
		public double To (Duration unit) {
			return _seconds / unit._seconds;
		}

		public static implicit operator double(Duration d) {
			return d._seconds;
		}

		/////////////////////////////////////////////////////////////////////////////
		// OPERATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Duration operator +(Duration first, Duration second) {
			return first._seconds + second._seconds;
		}

		public static Duration operator +(Duration first, double second) {
			return first._seconds + second;
		}

		public static Duration operator +(double first, Duration second) {
			return first + second._seconds;
		}

		public static Duration operator +(Duration first, float second) {
			return first._seconds + second;
		}

		public static Duration operator +(float first, Duration second) {
			return first + second._seconds;
		}

		public static Duration operator -(Duration first, Duration second) {
			return first._seconds - second._seconds;
		}

		public static Duration operator -(Duration first, double second) {
			return first._seconds - second;
		}

		public static Duration operator -(double first, Duration second) {
			return first - second._seconds;
		}

		public static Duration operator -(Duration first, float second) {
			return first._seconds - second;
		}

		public static Duration operator -(float first, Duration second) {
			return first - second._seconds;
		}

		public static Duration operator *(Duration first, double second) {
			return first._seconds * second;
		}

		public static Duration operator *(double first, Duration second) {
			return first * second._seconds;
		}

		public static Duration operator *(Duration first, float second) {
			return first._seconds * second;
		}

		public static Duration operator *(float first, Duration second) {
			return first * second._seconds;
		}

		public static double operator /(Duration first, Duration second) {
			return first._seconds / second._seconds;
		}

		public static Duration operator /(Duration first, double second) {
			return first._seconds / second;
		}

		public static Duration operator /(Duration first, float second) {
			return first._seconds / second;
		}

		/////////////////////////////////////////////////////////////////////////////
		// EQUALITY
		/////////////////////////////////////////////////////////////////////////////
		public bool Equals(Duration other) {
			return _seconds.Equals(other._seconds);
		}

		public bool Equals(Duration other, Duration delta) {
			return Math.Abs(_seconds - other._seconds) < Math.Abs(delta._seconds);
		}

		public override bool Equals(object obj) {
			return obj is Duration other && Equals(other);
		}

		public override int GetHashCode() {
			return _seconds.GetHashCode();
		}

		public static bool operator ==(Duration first, Duration second) {
			return first.Equals(second);
		}

		public static bool operator !=(Duration first, Duration second) {
			return !first.Equals(second);
		}

		public int CompareTo(Duration other) {
			return _seconds.CompareTo(other._seconds);
		}

		/////////////////////////////////////////////////////////////////////////////
		// TO STRING
		/////////////////////////////////////////////////////////////////////////////
		public override string ToString() {
			return Si.ToLargestSiString(_seconds, UnitString);
		}

		public string ToStringNanoseconds() {
			return Si.ToLargestSiString(_seconds, UnitString, 2, 0, -9, -9);
		}

		public string ToStringMicroseconds() {
			return Si.ToLargestSiString(_seconds, UnitString, 2, 0, -6, -6);
		}

		public string ToStringMilliseconds() {
			return Si.ToLargestSiString(_seconds, UnitString, 2, 0, -3, -3);
		}

		public string ToStringSeconds() {
			return Si.ToLargestSiString(_seconds, UnitString, 2, 0, 0, 0);
		}

		public string ToStringMinutes() {
			return FormatNonSiTime(Minute, "minute");
		}

		public string ToStringHours() {
			return FormatNonSiTime(Hour, "hour");
		}

		public string ToStringDays() {
			return FormatNonSiTime(Day, "day");
		}

		public string ToStringWeeks() {
			return FormatNonSiTime(Week, "week");
		}

		public string ToStringYears() {
			return FormatNonSiTime(Year, "year");
		}

		public string ToStringDecades() {
			return FormatNonSiTime(Decade, "decade");
		}

		public string ToStringCenturies() {
			// ReSharper disable once StringLiteralTypo
			return FormatNonSiTime(Century, "centur", "ies", "y");
		}

		public string ToStringMillennia() {
			// ReSharper disable once StringLiteralTypo
			return FormatNonSiTime(Millennium, "millenni", "a", "um");
		}

		private string FormatNonSiTime(Duration unit, string unitString, string pluralSuffix = "s", string singularSuffix = "") {
			string formattedNumber = $"{To(unit):F2}";
			return $"{formattedNumber} {unitString}{(formattedNumber.Equals("1.00") ? singularSuffix : pluralSuffix)}";
		}
	}
}
