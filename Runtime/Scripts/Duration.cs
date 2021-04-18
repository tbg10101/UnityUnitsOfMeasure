using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using UnityEngine;

namespace Software10101.Units {
	// ideally the entire struct would be readonly
	// however, Unity does not serialize readonly fields
	// this is unfortunate but we can enforce immutability by not exposing the value publicly
	[Serializable]
	public struct Duration : IEquatable<Duration>, IComparable<Duration>, ISerializable {
		private const string UnitString = "s";

		public static readonly Duration ZeroTime    = new Duration();
		public static readonly Duration Nanosecond  = new Duration { _seconds = 0.000000001 };
		public static readonly Duration Microsecond = new Duration { _seconds = 0.000001 };
		public static readonly Duration Millisecond = new Duration { _seconds = 0.001 };
		public static readonly Duration Second      = new Duration { _seconds = 1.0 };
		public static readonly Duration Minute      = new Duration( 60.0,  Second);
		public static readonly Duration Hour        = new Duration( 60.0,  Minute);
		public static readonly Duration Day         = new Duration( 24.0,  Hour);
		public static readonly Duration Week        = new Duration(  7.0,  Day);
		public static readonly Duration Year        = new Duration(365.25, Day);
		public static readonly Duration Decade      = new Duration( 10.0,  Year);
		public static readonly Duration Century     = new Duration( 10.0,  Decade);
		public static readonly Duration Millennium  = new Duration( 10.0,  Century);

		[SerializeField]
		internal double _seconds;

		/////////////////////////////////////////////////////////////////////////////
		// BOXING
		/////////////////////////////////////////////////////////////////////////////
		public Duration(double d, Duration unit) {
			_seconds = d * unit._seconds;
		}

		public static Duration From(double d, Duration unit) {
			return new Duration(d, unit);
		}

		/////////////////////////////////////////////////////////////////////////////
		// UN-BOXING
		/////////////////////////////////////////////////////////////////////////////
		public double To (Duration unit) {
			return _seconds / unit._seconds;
		}

		/////////////////////////////////////////////////////////////////////////////
		// SERIALIZATION
		/////////////////////////////////////////////////////////////////////////////
		private const string SecondsSerializedFieldName = "seconds";

		public Duration(SerializationInfo info, StreamingContext context) {
			_seconds = info.GetDouble(SecondsSerializedFieldName);
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue(SecondsSerializedFieldName, _seconds);
		}

		/////////////////////////////////////////////////////////////////////////////
		// OPERATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Duration operator +(Duration first, Duration second) {
			return new Duration { _seconds = first._seconds + second._seconds };
		}

		public static Duration operator -(Duration first, Duration second) {
			return new Duration { _seconds = first._seconds - second._seconds };
		}

		public static Duration operator *(Duration first, double second) {
			return new Duration { _seconds = first._seconds * second };
		}

		public static Duration operator *(double first, Duration second) {
			return new Duration { _seconds = first * second._seconds };
		}

		public static double operator /(Duration first, Duration second) {
			return first._seconds / second._seconds;
		}

		public static Duration operator /(Duration first, double second) {
			return new Duration { _seconds = first._seconds / second };
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

		[SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
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
