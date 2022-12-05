using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt Methoder zur gregorianischen Feiertagen.
/// </summary>
public static class MHolidayGregorian
{
	// MHolidayGregorian.EasterSunday(int)
	/// <summary>
	/// Liefert das julianische Datum zum Ostermonatag zur gregorianischen Jahreszahl.
	/// </summary>
	/// <param name="year">Gregorianische Jahreszahl.</param>
	/// <returns>Julianische Datum zum Ostermonatag zur gregorianischen Jahreszahl.</returns>
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
}
