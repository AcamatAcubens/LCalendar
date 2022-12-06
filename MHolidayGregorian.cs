using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt Methoden zu den beweglichen gregorianischen Feiertagen.
/// </summary>
public static class MHolidayGregorian
{
	// MHolidayGregorian.Advent(int)
	/// <summary>
	/// Liefert die julianische Tageszahl zum Advent zur gregorianischen Jahreszahl.
	/// </summary>
	/// <param name="year">Gregorianische Jahreszahl.</param>
	/// <returns>Julianische Tageszahl zum Advent zur gregorianischen Jahreszahl.</returns>
	public static double Advent(int year){ return MCalendar.DayBefore(new DateOnly(year, 12, 25).ToJdn(), EWeekDay.Sunday, 4); }

	// MHolidayGregorian.AdventFourth(int)
	/// <summary>
	/// Liefert die julianische Tageszahl zum vierten Advent zur gregorianischen Jahreszahl.
	/// </summary>
	/// <param name="year">Gregorianische Jahreszahl.</param>
	/// <returns>Julianische Tageszahl zum vierten Advent zur gregorianischen Jahreszahl.</returns>
	public static double AdventFourth(int year){ return MHolidayGregorian.Advent(year) + 21.0; }

	// MHolidayGregorian.AdventSecond(int)
	/// <summary>
	/// Liefert die julianische Tageszahl zum zweiten Advent zur gregorianischen Jahreszahl.
	/// </summary>
	/// <param name="year">Gregorianische Jahreszahl.</param>
	/// <returns>Julianische Tageszahl zum zweiten Advent zur gregorianischen Jahreszahl.</returns>
	public static double AdventSecond(int year){ return MHolidayGregorian.Advent(year) + 7.0; }

	// MHolidayGregorian.AdventThird(int)
	/// <summary>
	/// Liefert die julianische Tageszahl zum dritten Advent zur gregorianischen Jahreszahl.
	/// </summary>
	/// <param name="year">Gregorianische Jahreszahl.</param>
	/// <returns>Julianische Tageszahl zum dritten Advent zur gregorianischen Jahreszahl.</returns>
	public static double AdventThird(int year){ return MHolidayGregorian.Advent(year) + 14.0; }

	// MHolidayGregorian.AscensionDay(int)
	/// <summary>
	/// Liefert die julianische Tageszahl zu Christi Himmelfahrt zur gregorianischen Jahreszahl.
	/// </summary>
	/// <param name="year">Gregorianische Jahreszahl.</param>
	/// <returns>Julianische Tageszahl zu Christi Himmelfahrt zur gregorianischen Jahreszahl.</returns>
	public static double AscensionDay(int year){ return MHolidayGregorian.EasterSunday(year) + 39.0; }

	// MHolidayGregorian.AshWednesday(int)
	/// <summary>
	/// Liefert die julianische Tageszahl zum Aschermittwoch zur gregorianischen Jahreszahl.
	/// </summary>
	/// <param name="year">Gregorianische Jahreszahl.</param>
	/// <returns>Julianische Tageszahl zum Aschermittwoch zur gregorianischen Jahreszahl.</returns>
	public static double AshWednesday(int year){ return MHolidayGregorian.EasterSunday(year) - 46.0; }

	// MHolidayGregorian.CorpusChristi(int)
	/// <summary>
	/// Liefert die julianische Tageszahl zu Fronleichnam zur gregorianischen Jahreszahl.
	/// </summary>
	/// <param name="year">Gregorianische Jahreszahl.</param>
	/// <returns>Julianische Tageszahl zu Fronleichnam zur gregorianischen Jahreszahl.</returns>
	public static double CorpusChristi(int year){ return MHolidayGregorian.EasterSunday(year) + 60.0; }

	// MHolidayGregorian.DayOfRepentanceAndPrayer(int)
	/// <summary>
	/// Liefert die julianische Tageszahl zum Buß- und Bettag zur gregorianischen Jahreszahl.
	/// </summary>
	/// <param name="year">Gregorianische Jahreszahl.</param>
	/// <returns>Julianische Tageszahl zum Buß- und Bettag zur gregorianischen Jahreszahl.</returns>
	public static double DayOfRepentanceAndPrayer(int year){ return MHolidayGregorian.Advent(year) - 11.0; }

	// MHolidayGregorian.EasterMonday(int)
	/// <summary>
	/// Liefert die julianische Tageszahl zum Ostermontag zur gregorianischen Jahreszahl.
	/// </summary>
	/// <param name="year">Gregorianische Jahreszahl.</param>
	/// <returns>Julianische Tageszahl zum Ostermontag zur gregorianischen Jahreszahl.</returns>
	public static double EasterMonday(int year){ return MHolidayGregorian.EasterSunday(year) + 1.0; }

	// MHolidayGregorian.EasterSunday(int)
	/// <summary>
	/// Liefert die julianische Tageszahl zum Ostersonntag zur gregorianischen Jahreszahl.
	/// </summary>
	/// <param name="year">Gregorianische Jahreszahl.</param>
	/// <returns>Julianische Datum zum Ostersonntag zur gregorianischen Jahreszahl.</returns>
	public static double EasterSunday(int year)
	{
		// Hilfsgrößen berechnen
		int K  = year / 100;                              // Säkularzahl
		int M  = 15 + (3 * K + 3) / 4 - (8 * K +13) / 25; // Säkulare Mondschaltung
		int S  = 2 - (3 * K + 3) / 4;                     // Säkulare Sonnenschaltung
		int A  = year % 19;                               // Mondparameter 
		int D  = (19 * A + M) % 30;                       // Keim für den erstenl Vollmond im Frühling
		int R  = (D + A / 11) / 29;                       // Kalendarische Korrekturgröße
		int OG = 21 + D - R;                              // Ostergrenze
		int SZ = 7 - (year + year / 4 + S) % 7;           // Erster Sonntag im März
		int OE = 7 - (OG - SZ) % 7;                       // Osterentfernung in Tagen
		int OS = OG + OE;                                 // Datum des Ostersonntags als Märzdatum

		// Osterdatum bilden und Rückgabe
		if(OS <= 31)
			return new CDate(year, 3, OS).ToJdn();
		return new CDate(year, 4, OS - 31).ToJdn();
	}

	// MHolidayGregorian.FatThursday(int)
	/// <summary>
	/// Liefert die julianische Tageszahl zum Fetten Donnerstag zur gregorianischen Jahrszahl.
	/// </summary>
	/// <param name="year">Gregorianische Jahreszahl.</param>
	/// <returns>Julianische Tageszahl zum Fetten Donnerstag zur gregorianischen Jahrszahl.</returns>
	public static double FatThursday(int year){ return MHolidayGregorian.EasterSunday(year) - 52.0; }

	// MHolidayGregorian.GoodFriday(int)
	/// <summary>
	/// Liefert die julianische Tageszahl zum Karfreitag zur gregorianischen Jahreszahl.
	/// </summary>
	/// <param name="year">Gregorianische Jahreszahl.</param>
	/// <returns>Julianische Tageszahl zum Karfreitag zur gregorianischen Jahreszahl.</returns>
	public static double GoodFriday(int year){ return MHolidayGregorian.EasterSunday(year) - 2.0; }

	// MHolidayGregorian.Invovabit(int)
	/// <summary>
	/// Liefert die julianische Tageszahl zum 1. Sonntag der Passionszeit zur gregorianischen Jahreszahl.
	/// </summary>
	/// <param name="year">Gregorianische Jahreszahl.</param>
	/// <returns>Julianische Tageszahl zum 1. Sonntag der Passionszeit zur gregorianischen Jahreszahl.</returns>
	public static double Invovabit(int year){ return MHolidayGregorian.EasterSunday(year) - 42.0; }

	// MHolidayGregorian.Judica(int)
	/// <summary>
	/// Liefert die julianische Tageszahl zum 5. Sonntag der Passionszeit zur gregorianischen Jahreszahl,
	/// </summary>
	/// <param name="year">Gregorianische Jahreszahl.</param>
	/// <returns>Julianische Tageszahl zum 5. Sonntag der Passionszeit zur gregorianischen Jahreszahl,</returns>
	public static double Judica(int year){ return MHolidayGregorian.EasterSunday(year) - 14.0; }

	// MHolidayGregorian.Laetare(int)
	/// <summary>
	/// Liefert die julianische Tageszahl zum 4. Sonntag der Passionszeit zur gregorianischen Jahreszahl.
	/// </summary>
	/// <param name="year">Grgorianische Jahreszahl.</param>
	/// <returns>Julianische Tageszahl zum 4. Sonntag der Passionszeit zur gregorianischen Jahreszahl.</returns>
	public static double Laetare(int year){ return MHolidayGregorian.EasterSunday(year) - 21.0; }

	// MHolidayGreogorian.MaundyThursday(int)
	/// <summary>
	/// Liefert die julianische Tageszahl zum Gründonnerstag zur gregorianischen Jahreszahl.
	/// </summary>
	/// <param name="year">Gregorianische Jahreszahl.</param>
	/// <returns>Julianische Tageszahl zum Gründonnerstag zur gregorianischen Jahreszahl.</returns>
	public static double MaundyThursday(int year){ return MHolidayGregorian.EasterSunday(year) - 3.0; }

	// MHolidayGregorian.Oculi(int)
	/// <summary>
	/// Liefert die julianische Tageszahl zum 3. Sonntag der Passionszeit zur gregorianischen Jahreszahl.
	/// </summary>
	/// <param name="year">Gregorianische Jahreszahl.</param>
	/// <returns>Julianische Tageszahl zum 3. Sonntag der Passionszeit zur gregorianischen Jahreszahl.</returns>
	public static double Oculi(int year){ return MHolidayGregorian.EasterSunday(year) - 28.0; }

	// MHolidayGregorian.PalmSunday(int)
	/// <summary>
	/// Liefert die julianische Tageszahl zum Palmsonntag zur gregorianischen Jahreszahl.
	/// </summary>
	/// <param name="year">Gregorianischen Jahreszahl.</param>
	/// <returns>Julianische Tageszahl zum Palmsonntag zur gregorianischen Jahreszahl.</returns>
	public static double PalmSunday(int year){ return MHolidayGregorian.EasterSunday(year) - 7.0; }

	// MHolidayGregorian.Pentecost(int)
	/// <summary>
	/// Liefert die julianische Tageszahl zum Pfingstsonntag zur gregorianischen Jahreszahl.
	/// </summary>
	/// <param name="year">Gregorianische Jahreszahl.</param>
	/// <returns>Julianische Tageszahl zum Pfingstsonntag zur gregorianischen Jahreszahl.</returns>
	public static double Pentecost(int year){ return MHolidayGregorian.EasterSunday(year) + 49.0; }

	// MHolidayGregorian.PeoplesDayOfMourning(int)
	/// <summary>
	/// Liefert die julianische Tageszahl zum Volkstrauertag zur gregorianischen Jahreszahl.
	/// </summary>
	/// <param name="year">Gregorianische Jahreszahl.</param>
	/// <returns>Julianische Tageszahl zum Volkstrauertag zur gregorianischen Jahreszahl.</returns>
	public static double PeoplesDayOfMourning(int year){ return MHolidayGregorian.Advent(year) - 14.0; }

	// MHolidayGregorian.Reminiscere(int)
	/// <summary>
	/// Liefert die julianische Tageszahl zum 2. Sonntag der Passionszeit zur gregorianischen Jahreszahl.
	/// </summary>
	/// <param name="year">Gregorianische Jahreszahl.</param>
	/// <returns>Julianische Tageszahl zum 2. Sonntag der Passionszeit zur gregorianischen Jahreszahl.</returns>
	public static double Reminiscere(int year){ return MHolidayGregorian.EasterSunday(year) - 35.0; }

	// MHolidayGregorian.ShroveMonday(int)
	/// <summary>
	/// Liefert die julianische Tageszahl zum Rosenmontag zur gregorianischen Jahreszahl.
	/// </summary>
	/// <param name="year">Gregorianische Jahreszahl.</param>
	/// <returns>Julianische Tageszahl zum Rosenmontag zur gregorianischen Jahreszahl.</returns>
	public static double ShroveMonday(int year){ return MHolidayGregorian.EasterSunday(year) - 48.0; }

	// MHolidayGregorian.ShroweTuesday(int)
	/// <summary>
	/// Liefert die julianische Tageszahl zum Veilchendienstagg zur gregorianischen Jahreszahl.
	/// </summary>
	/// <param name="year">Gregorianische Jahreszahl.</param>
	/// <returns>Julianische Tageszahl zum Veilchendienstag zur gregorianischen Jahreszahl.</returns>
	public static double ShroveTuesday(int year){ return MHolidayGregorian.EasterSunday(year) - 47.0; }

	// MHolidayGregorian.SpyWednesday(int)
	/// <summary>
	/// Liefert die julianische Tageszahl zum Karmittwoch zur gregorianischen Jahreszahl.
	/// </summary>
	/// <param name="year">Gregorianische Jahreszahl.</param>
	/// <returns>Julianische Tageszahl zum Karmittwoch zur gregorianischen Jahreszahl.</returns>
	public static double SpyWednesday(int year){ return MHolidayGregorian.EasterSunday(year) - 4.0; }

	// MHolidayGregorian.SundayOfTheDead(int)
	/// <summary>
	/// Liefert die juliansiche Tageszahl zum Totensonntag zur gregorianischen Jahreszahl.
	/// </summary>
	/// <param name="year">Gregorianische Jahreszahl.</param>
	/// <returns>Juliansiche Tageszahl zum Totensonntag zur gregorianischen Jahreszahl.</returns>
	public static double SundayOfTheDead(int year){ return MHolidayGregorian.Advent(year) - 7.0; }

	// MHolidayGregorian.WhitMonday(int)
	/// <summary>
	/// Liefert die julianische Tageszahl zum Pfingstmontag zur gregorianischen Jahreszahl.
	/// </summary>
	/// <param name="year">Gregorianischen Jahreszahl.</param>
	/// <returns>Julianische Tageszahl zum Pfingstmontag zur gregorianischen Jahreszahl.</returns>
	public static double WhitMonday(int year){ return MHolidayGregorian.EasterSunday(year) + 50.0; }
}
