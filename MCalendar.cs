using Acamat.LCore;
using Acamat.LMath;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt kalendarische Berechnungen.
/// </summary>
public static class MCalendar
{
	// ----------------------------------- //
	// Deklaration der privaten Konstanten //
	// ----------------------------------- //
	private const double Dbl_SecondsPerMinute  =      60.0;
	private const double Dbl_SecondsPerHour    =    3600.0;
	private const double Dbl_SecondsPerDay     =   84600.0;
	private const double Dbl_SystemDateTimeMin = 1721425.5;
	private const double Dbl_SystemDateTimeMax = 5373485.5;

	// ---------------------- //
	// Konstanten der Epochen //
	// ---------------------- //
	// MCalendar.EpochHebrew
	/// <summary>
	/// Julianische Tageszahl zum Epochenbeginn des jüdischen Kalenders.
	/// </summary>
	public const double EpochHebrew      =  347997.5;

	// MCalendar.EpochMayan
	/// <summary>
	/// Julianische Tageszahl zum Epochenbeginn des myanischen Kalenders.
	/// </summary>
	public const double EpochMayan       =  584282.5;

	// MCalendar.EpochHindu
	/// <summary>
	/// Julianische Tageszahl zum Epochenbeginn des hinduistischen Kalenders.
	/// </summary>
	public const double EpochHindu       =  588465.5;

	// MCalendar.EpochChinese
	/// <summary>
	/// Julianische Tageszahl zum Epochenbeginn des chinesichen Kalenders.
	/// </summary>
	public const double EpochChinese     =  758325.5;

	// MCalendar.EpochEgyptian
	/// <summary>
	/// Julianische Tageszahl zum Epochenbeginn des ägyptischen Kalenders.
	/// </summary>
	public const double EpochEgyptian    = 1448637.5;

	// MCalendar.EpochJulian
	/// <summary>
	/// Julianische Tageszahl zum Epchenbeginn des julianischen Kalenders.
	/// </summary>
	public const double EpochJulian      = 1721423.5;

	// MCalendar.EpochGregorian
	/// <summary>
	/// Julianische Tageszahl zum Epochenbeginn des gregorianischen Kalenders.
	/// </summary>
	public const double EpochGregorian   = 1721425.5;

	// MCalendar.EpochEthiopic
	/// <summary>
	/// Julianische Tageszahl zum Epochenbeginn des äthiopischen Kalenders.
	/// </summary>
	public const double EpochEthiopic    = 1724220.5;

	// MCalendar.EpochCoptic
	/// <summary>
	/// Julianische Tageszahl zum Epochenbeginn des koptischen Kalenders.
	/// </summary>
	public const double EpochCoptic      = 1825029.5;

	// MCalendar.EpochZoroastrian
	/// <summary>
	/// Julianische Tageszahl zum Epochenbeginn des zoroastrischen Kalenders.
	/// </summary>
	public const double EpochZoroastrian = 1862836.5;

	// MCalendar.EpochArmenian
	/// <summary>
	/// Julianische Tageszahl zum Epochenbeginn des armenischen Kalenders.
	/// </summary>
	public const double EpochArmenian    = 1922867.5;

	// MCalendar.EpochPersian
	/// <summary>
	/// Julianische Tageszahl zum Epochenbeginn des persischen Kalenders.
	/// </summary>
	public const double EpochPersian     = 1948320.5;

	// MCalendar.EpochIslamic
	/// <summary>
	/// Julianische Tageszahl zum Epochenbeginn des islamischen Kalenders.
	/// </summary>
	public const double EpochIslamic     = 1948439.5;

	// MCaledar.EpochYazdegerd
	/// <summary>
	/// Julianische Tageszahl zum Epochenbeginn des yazdegerdischen Kalenders.
	/// </summary>
	public const double EpochYazdegerd   = 1952062.5;

	// MCalendar.MCalendar.Jdn20000101
	/// <summary>
	/// Julianische Tageszahl zum 01.01.2000 12:00.
	/// </summary>
	public const double Jdn20000101      = 2451545.0;

	// MCalendar.SecondsPerDay
	/// <summary>
	/// Anzahl der Sekunden pro Tag.
	/// </summary>
	public const int SecondsPerDay = 86400;

	// ------------------- //
	// Felder und Methoden //
	// ------------------- //
	// MCalendar.CenturyFragment()
	/// <summary>
	/// Liefert den Jahrhundertbruchteil zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Jahrhundertbruchteil zur aktuellen Systemzeit.</returns>
	public static double CenturyFragment()
	{
		// Lokalen Felder einrichten und Jahrhundertbruchteil berechnen
		double jd = DateTime.Now.ToJdn();
		return MCalendar.CenturyFragment(jd);
	}

	// MCalendar.CenturyFragment(double)
	/// <summary>
	/// Liefert den Jahrhundertbruchteil zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Jahrhundertbruchteil zur julianischen Tageszahl.</returns>
	public static double CenturyFragment(double jd){ return(jd - MCalendar.Jdn20000101) / 36525.0; }

	// MCalendar.DayAfter(double, EWeekDayList, int)
	/// <summary>
	/// Liefert die julianische Tageszahl des n-ten Wochentages nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="weekDay">Kennung zum Wochentag.</param>
	/// <param name="n">Ordinalzahl.</param>
	/// <returns>Julianische Tageszahl des n-ten Wochentages nach der julianischen Tageszahl.</returns>
	public static double DayAfter(double jd, EWeekDay weekDay, int n){ return jd - MMod.Mod(MCalendar.DayOfWeek(jd) - weekDay, 7.0) + 7.0 * n; }

	// MCalendar.DayBefore(double, EWeekDay, int)
	/// <summary>
	/// Liefert das julianische Datum des n-ten Wochentages vor der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianisches Datum.</param>
	/// <param name="weekDay">Kennung zum Wochentag.</param>
	/// <param name="n">Ordinalzahl.</param>
	/// <returns>Julianisches Datum des n-ten Wochentages vor der julianischen Tageszahl.</returns>
	public static double DayBefore(double jd, EWeekDay weekDay, int n){ return jd + MMod.Mod(weekDay - MCalendar.DayOfWeek(jd), 7.0) - 7.0 * n; }

	// MCalendar.DayNearest(double, EWeekDayList)
	/// <summary>
	/// Liefert das julianische Datum des nächstgelegenen Wochentages zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianisches Datum.</param>
	/// <param name="weekDay">Kennung des Wochentags.</param>
	/// <returns>Julianisches Datum des nächstgelegenen Wochentages zur julianischen Tageszahl.</returns>
	public static double DayNearest(double jd, EWeekDay weekDay){ return MCalendar.DayOnOrBefore(jd + 3.0, weekDay, 1); }

	// MCalendar.DayOfWeek(double)
	/// <summary>
	/// Liefert die Kennung zum Wochentag zum julianischen Datum.
	/// </summary>
	/// <param name="jd">Julianisches Datum.</param>
	/// <returns>Kennung des Wochentags zum julianischen Datum.</returns>
	public static EWeekDay DayOfWeek(double jd){ return (EWeekDay)((int)jd % 7); }

	// MCalendar.DayOfYear(double)
	/// <summary>
	/// Liefert die Tageszahl im gregorianischen Jahr zum julianischen Datum.
	/// </summary>
	/// <param name="jd">Julianisches Datum.</param>
	/// <returns>Tageszahl im gregorianischen Jahr zum julianischen Datum.</returns>
	public static double DayOfYear(double jd){ return MCalendar.DaysElapsed(jd) + 1.0; }

	// MCalendar.DayOnOrAfter(double, EWeekDayList, int)
	/// <summary>
	/// Liefert das julianische Datum des n-ten Wochentages zur oder nach dem julianischen Datum.
	/// </summary>
	/// <param name="jd">Julianisches Datum.</param>
	/// <param name="weekDay">Kennung des Wochentags.</param>
	/// <param name="n">Anzahl.</param>
	/// <returns>Julianisches Datum des n-ten Wochentages zur oder nach dem julianischen Datum.</returns>
	public static double DayOnOrAfter(double jd, EWeekDay weekDay, int n){ return jd + MMod.Mod(weekDay - MCalendar.DayOfWeek(jd), 7.0) + 7.0 * (n - 1); }

	// MCalendar.DayOnOrBefore(double, EWeekDayList, int)
	/// <summary>
	/// Liefert das julianische Datum des n-ten Wochentages zur oder vor dem julianischen Datum.
	/// </summary>
	/// <param name="jd">Julianisches Datum.</param>
	/// <param name="weekDay">Kennung des Wochentags.</param>
	/// <param name="n">Anzahl.</param>
	/// <returns>Julianisches Datum des n-ten Wochentages zur oder vor dem julianischen Datum.</returns>
	public static double DayOnOrBefore(double jd, EWeekDay weekDay, int n){ return jd - MMod.Mod(MCalendar.DayOfWeek(jd) - weekDay, 7.0) - 7.0 * (n - 1); }

	// MCalendar.DaysElapsed(double)
	/// <summary>
	/// Liefert die Anzahl der verstrichenen Tage im gregorianischen Jahr zum julianischen Datum.
	/// </summary>
	/// <param name="jd">Julianisches Datum.</param>
	/// <returns>Anzahl der verstrichenen Tage im gregorianischen Jahr zum julianischen Datum.</returns>
	public static double DaysElapsed(double jd)
	{
		// Anzahl berechnen
		double v0101 = MCalendar.FromGregorian(MCalendar.GregorianYear(jd), 1, 1);
		return jd - v0101;
	}

	// MCalendar.DaysInYear(int)
	/// <summary>
	/// Liefert die Anzahl der Tage im Jahr zum gregorianischen Jahr.
	/// </summary>
	/// <param name="year">Gregorianische Jahreszahl.</param>
	/// <returns>Anzahl der Tage im Jahr zum gregorianischen Jahr.</returns>
	public static int DaysInYear(int year){ return MCalendar.IsLeapYear(year) ? 366 : 365; }

	// MCalendar.DaysRemaining(double)
	/// <summary>
	/// Liefert die Anzahl der verbleibenden Tage im gregorianischen Jahr zum julianischen Datum.
	/// </summary>
	/// <param name="jd">Julianisches Datum.</param>
	/// <returns>Anzahl der verbleibenden Tage im gregorianischen Jahr zum julianischen Datum.</returns>
	public static double DaysRemaining(double jd)
	{
		// Anzahl berechnen
		double v0101 = MCalendar.FromGregorian(MCalendar.GregorianYear(jd) + 1, 1, 1);
		return v0101 - jd - 1.0;
	}

	// MCalendar.FromArmenian(CDate)
	/// <summary>
	/// Liefert das julianische Datum zum armenischen Datum.
	/// </summary>
	/// <param name="date">Armenisches Datum.</param>
	/// <returns>Julianisches Datum zum armenischen Datum.</returns>
	public static double FromArmenian(CDate date){ return MCalendar.FromArmenian(date.Year, date.Month, date.Day); }

	// MCalendar.FromArmenian(int)
	/// <summary>
	/// Liefert das julianische Datum zum Jahresbeginn des armenischen Jahres.
	/// </summary>
	/// <param name="year">Armenische Jahreszahl.</param>
	/// <returns>Julianisches Datum zum Jahresbeginn des armenischen Jahres.</returns>
	public static double FromArmenian(int year){ return MCalendar.FromArmenian(year, 1, 1); }

	// MCalendar.FromArmenian(int, int, int)
	/// <summary>
	/// Liefert das julianische Datum zum armenischen Datum.
	/// </summary>
	/// <param name="year">Armenische Jahreszahl.</param>
	/// <param name="month">Armenische Monatszahl.</param>
	/// <param name="day">Armenische Tageszahl.</param>
	/// <returns>Julianisches Datum zum armenischen Datum.</returns>
	public static double FromArmenian(int year, int month, int day){ return MCalendar.EpochArmenian + (double)(365 * (year - 1)) + (double)(30 * (month - 1)) + (double)(day - 1); }

	// MCalendar.FromCoptic(CDate)
	/// <summary>
	/// Liefert das julianische Datum zum koptischen Datum.
	/// </summary>
	/// <param name="date">Koptisches Datum.</param>
	/// <returns>Julianisches Datum zum koptischen Datum.</returns>
	public static double FromCoptic(CDate date){ return MCalendar.FromCoptic(date.Year, date.Month, date.Day); }

	// MCalendar.FromCoptic(int)
	/// <summary>
	/// Liefert das julianische Datum zum Jahresbeginn des koptischen Jahres.
	/// </summary>
	/// <param name="year">Koptische Jahreszahl.</param>
	/// <returns>Julianisches Datum zum Jahresbeginn des koptischen Jahres.</returns>
	public static double FromCoptic(int year){ return MCalendar.FromCoptic(year, 1, 1); }

	// MCalendar.FromCoptic(int, int, int)
	/// <summary>
	/// Liefert das julianische Datum zum koptischen Datum.
	/// </summary>
	/// <param name="year">Koptische Jahreszahl.</param>
	/// <param name="month">Koptische Monatszahl.</param>
	/// <param name="day">Koptische Tageszahl.</param>
	/// <returns>Julianisches Datum zum koptischen Datum.</returns>
	public static double FromCoptic(int year, int month, int day){ return MCalendar.EpochCoptic + (double)(365 * (year - 1)) + MMath.Floor(year / 4.0) + (double)(30 * (month - 1)) + (double)(day - 1); }

	// MCalendar.FromEgyptian(CDate)
	/// <summary>
	/// Liefert das julianische Datum zum ägyptischen Datum.
	/// </summary>
	/// <param name="date">Ägyptisches Datum.</param>
	/// <returns>Julianisches Datum zum ägyptischen Datum.</returns>
	public static double FromEgyptian(CDate date){ return MCalendar.FromEgyptian(date.Year, date.Month, date.Day); }

	// MCalendar.FromEgyptian(int)
	/// <summary>
	/// Liefert das julianische Datum zum Jahresbeginn des ägyptischen Jahres.
	/// </summary>
	/// <param name="year">Ägyptische Jahreszahl.</param>
	/// <returns>Julianisches Datum zum Jahresbeginn des ägyptischen Jahres.</returns>
	public static double FromEgyptian(int year){ return MCalendar.FromEgyptian(year, 1, 1); }

	// MCalendar.FromEgyptian(int, int, int)
	/// <summary>
	/// Liefert das julianische Datum zum ägyptischen Datum.
	/// </summary>
	/// <param name="year">Ägyptische Jahreszahl.</param>
	/// /// <param name="month">Ägyptische Monatszahl.</param>
	/// <param name="day">Ägyptische Tageszahl.</param>
	/// <returns>Julianisches Datum zum ägyptischen Datum.</returns>
	public static double FromEgyptian(int year, int month, int day){ return MCalendar.EpochEgyptian + (double)(365 * (year - 1)) + (double)(30 * (month - 1)) + (double)(day - 1); }

	// MCalendar.FromEthiopic(CDate)
	/// <summary>
	/// Liefert das julianische Datum zum äthiopischen Datum.
	/// </summary>
	/// <param name="date">Äthiopischen Datum.</param>
	/// <returns>Julianisches Datum zum äthiopischen Datum.</returns>
	public static double FromEthiopic(CDate date){ return MCalendar.FromEthiopic(date.Year, date.Month, date.Day); }

	// MCalendar.FromEthiopic(int)
	/// <summary>
	/// Liefert das julianische Datum zum Jahresbeginn des äthiopischen Jahres.
	/// </summary>
	/// <param name="year">Äthiopische Jahreszahl.</param>
	/// <returns>Julianisches Datum zum Jahresbeginn des äthiopischen Jahres.</returns>
	public static double FromEthiopic(int year){ return MCalendar.FromEthiopic(year, 1, 1); }

	// MCalendar.FromEthiopic(int, int, int)
	/// <summary>
	/// Liefert das julianische Datum zum äthiopischen Datum.
	/// </summary>
	/// <param name="year">Äthiopische Jahreszahl.</param>
	/// <param name="month">Äthiopische Monatszahl.</param>
	/// <param name="day">Äthiopische Tageszahl.</param>
	/// <returns>Julianisches Datum zum äthiopischen Datum.</returns>
	public static double FromEthiopic(int year, int month, int day){ return MCalendar.EpochEthiopic + (double)(365 * (year - 1)) + MMath.Floor(year / 4.0) + (double)(30 * (month - 1)) + (double)(day - 1); 	}

	// MCalendar.FromGregorian(CDate)
	/// <summary>
	/// Liefert das julianische Datum zum gregorianischen Datum.
	/// </summary>
	/// <param name="date">Gregorianisches Datum.</param>
	/// <returns>Julianisches Datum zum gregorianischen Datum.</returns>
	public static double FromGregorian(CDate date){ return MCalendar.FromGregorian(date.Year, date.Month, date.Day); }

	// MCalendar.FromGregorian(int)
	/// <summary>
	/// Liefert das julianische Datum zum Jahresbeginn des greogorianischen Jahres.
	/// </summary>
	/// <param name="year">Gregorianische Jahreszahl.</param>
	/// <returns>Julianisches Datum zum Jahresbeginn des greogorianischen Jahres.</returns>
	public static double FromGregorian(int year){ return MCalendar.FromGregorian(year, 1, 1); }

	// MCalendar.FromGregorian(int, int, int)
	/// <summary>
	/// Liefert das julianische Datum zum greogorianischen Datum.
	/// </summary>
	/// <param name="year">Gregorianische Jahreszahl.</param>
	/// <param name="month">Gregorianische Monatszahl.</param>
	/// <param name="day">Gregorianische Tageszahl.</param>
	/// <returns>Julianisches Datum zum greogorianischen Datum.</returns>
	public static double FromGregorian(int year, int month, int day)
	{
		// Lokale Felder einrichten
		double Y = (double)year - 1.0;
		double M = (double)month;
		double D = (double)day - 1.0;
		double V = MCalendar.EpochGregorian;

		// Tageszahl berechnen
		V += 365.0 * Y + MMath.Floor(Y / 4.0) - MMath.Floor(Y / 100.0) + MMath.Floor(Y / 400.0);
		V += MMath.Floor((367.0 * M - 362.0) / 12.0) + D;

		// Schaltjahr verarbeiten
		if(M > 2.0) V -= MCalendar.IsLeapYear(year) ? 1.0 : 2.0;
		return V;
	}

	// MCalendar.FromHebrew(CDate)
	/// <summary>
	/// Liefert das julianische Datum zum jüdischen Datum.
	/// </summary>
	/// <param name="date">Jüdisches Datum.</param>
	/// <returns>Julianisches Datum zum jüdischen Datum.</returns>
	public static double FromHebrew(CDate date){ return MCalendar.FromHebrew(date.Year, date.Month, date.Day); }

	// MCalendar.FromHebrew(int)
	/// <summary>
	/// Liefert das julianische Datum zum Jahresbeginn des jüdischen Jahres.
	/// </summary>
	/// <param name="year">Jüdische Jahreszahl.</param>
	/// <returns>Julianisches Datum zum Jahresbeginn des jüdischen Jahres.</returns>
	public static double FromHebrew(int year){ return MCalendar.FromHebrew(year, 1, 1); }

	// MCalendar.FromHebrew(int, int, int)
	/// <summary>
	/// Liefert das julianische Datum zum jüdischen Datum.
	/// </summary>
	/// <param name="year">Jüdische Jahreszahl.</param>
	/// <param name="month">Jüdische Monatszahl.</param>
	/// <param name="day">Jüdische Tageszahl.</param>
	/// <returns>Julianisches Datum zum jüdischen Datum.</returns>
	public static double FromHebrew(int year, int month, int day)
	{
		// Lokale Felder einrichten
		int N = 0;
		int i = 0;

		// Jahreswechsel verarbeiten
		if(month < 7)
		{
			// Tage summieren
			for (i = 7; i <= MCalendar.HebrewLastMonthOfYear(year); i++)
				N += MCalendar.HebrewLastDayOfMonth(year, i);
			for (i = 1; i <  month;                                 i++)
				N += MCalendar.HebrewLastDayOfMonth(year, i);
		}
		else for (i = 7; i < month; i++)
			N += MCalendar.HebrewLastDayOfMonth(year, i);

		// Tageszahl berechnen
		return MCalendar.FromHebrew(year) + (double)N + (double)day - 1.0;
	}

	// MCalendar.FromIslamic(CDate)
	/// <summary>
	/// Liefert das julianische Datum zum islamischen Datum.
	/// </summary>
	/// <param name="date">Islamisches Datum.</param>
	/// <returns>Julianisches Datum zum islamischen Datum.</returns>
	public static double FromIslamic(CDate date){ return MCalendar.FromIslamic(date.Year, date.Month, date.Day); }

	// MCalendar.FromIslamic(int)
	/// <summary>
	/// Liefert das julianische Datum zum Jahresbeginn des islamischen Jahres.
	/// </summary>
	/// <param name="year">Islamische Jahreszahl.</param>
	/// <returns>Julianisches Datum zum Jahresbeginn des islamischen Jahres.</returns>
	public static double FromIslamic(int year){ return MCalendar.FromIslamic(year, 1, 1); }

	// MCalendar.FromIslamic(int, int, int)
	/// <summary>
	/// Liefert das julianische Datum zum islamischen Datum.
	/// </summary>
	/// <param name="year">Islamische Jahreszahl.</param>
	/// <param name="month">Islamische Monatszahl.</param>
	/// <param name="day">Islamische Tageszahl.</param>
	/// <returns>Julianisches Datum zum islamischen Datum.</returns>
	public static double FromIslamic(int year, int month, int day)
	{
		// Lokale Felder einrichten
		double Y = (double)year;
		double M = (double)month;
		double D = (double)day - 1.0;
		double V = MCalendar.EpochIslamic;

		// Tageszahl berechnen
		V += 29.0 * (M - 1.0) + MMath.Floor((6.0 * M - 1.0) / 11.0) + 354.0 * (Y - 1.0) + MMath.Floor((3.0 + 11.0 * Y) / 30.0) + D;
		return V;
	}

	// MCalendar.FromJulian(CDate)
	/// <summary>
	/// Liefert das julianische Datum zum julianischen Datum.
	/// </summary>
	/// <param name="date">Julianisches Datum.</param>
	/// <returns>Julianisches Datum zum julianischen Datum.</returns>
	public static double FromJulian(CDate date){ return MCalendar.FromJulian(date.Year, date.Month, date.Day); }

	// MCalendar.FromJulian(int)
	/// <summary>
	/// Liefert das julianische Datum zum Jahresbeginn des julianischen Jahres.
	/// </summary>
	/// <param name="year">Julianische Jahreszahl.</param>
	/// <returns>Julianisches Datum zum Jahresbeginn des julianischen Jahres.</returns>
	public static double FromJulian(int year){ return MCalendar.FromJulian(year, 1, 1);	}

	// MCalendar.FromJulian(int, int, int)
	/// <summary>
	/// Liefert das julianische Datum zum julianischen Datum.
	/// </summary>
	/// <param name="year">Julianische Jahreszahl.</param>
	/// <param name="month">Julianische Monatszahl.</param>
	/// <param name="day">Julianische Tageszahl.</param>
	/// <returns>Julianisches Datum zum julianischen Datum.</returns>
	public static double FromJulian(int year, int month, int day)
	{
		// Lokale Felder einrichten
		double Y = (year < 0) ? (double)year + 1.0 : (double)year;
		double M = (double)month;
		double D = (double)day - 1.0;
		double V = MCalendar.EpochJulian;

		// Tageszahl berechnen
		V += 365.0 * (Y - 1.0) + MMath.Floor((Y - 1.0) / 4.0) + MMath.Floor((367.0 * M - 362.0) / 12.0) + D;

		// Schaltjahr korregieren
		if(M > 2.0)
			V -= (MCalendar.IsLeapYearJulian(year)) ? 1.0 : 2.0;
		return V;
	}

	// MCalendar.FromYazdegerd(CDate)
	/// <summary>
	/// Liefert das julianische Datum zum yazdegerdischen Datum.
	/// </summary>
	/// <param name="date">Yazdegerdischen Datum.</param>
	/// <returns>Julianisches Datum zum yazdegerdischen Datum.</returns>
	public static double FromYazdegerd(CDate date){ return MCalendar.FromYazdegerd(date.Year, date.Month, date.Day); }

	// MCalendar.FromYazdegerd(int)
	/// <summary>
	/// Liefert das julianische Datum zum Jahresbeginn des yazdegerdischen Jahres.
	/// </summary>
	/// <param name="year">Yazdegerdische Jahreszahl.</param>
	/// <returns>Julianisches Datum zum Jahresbeginn des yazdegerdischen Jahres.</returns>
	public static double FromYazdegerd(int year){ return MCalendar.FromYazdegerd(year, 1, 1); }

	// MCalendar.FromYazdegerd(int, int, int)
	/// <summary>
	/// Liefert das julianische Datum zum yazdegerdischen Datum.
	/// </summary>
	/// <param name="year">Yazdegerdische Jahreszahl.</param>
	/// <param name="month">Yazdegerdische Monatszahl.</param>
	/// <param name="day">Yazdegerdische Tageszahl.</param>
	/// <returns>Julianisches Datum zum yazdegerdischen Datum.</returns>
	public static double FromYazdegerd(int year, int month, int day){ return MCalendar.EpochYazdegerd + (double)(365 * (year - 1)) + (double)(30 * (month - 1)) + (double)(day - 1); }

	// MCalendar.FromZoroastrian(CDate)
	/// <summary>
	/// Liefert das julianische Datum zum zoroastrischen Datum.
	/// </summary>
	/// <param name="date">Zoroastrisches Datum.</param>
	/// <returns>Julianisches Datum zum zoroastrischen Datum.</returns>
	public static double FromZoroastrian(CDate date){ return MCalendar.FromZoroastrian(date.Year, date.Month, date.Day); }

	// MCalendar.FromZoroastrian(int)
	/// <summary>
	/// Liefert das julianische Datum zum Jahresbeginn des zoroastrischen Jahres.
	/// </summary>
	/// <param name="year">Zoroastrische Jahreszahl.</param>
	/// <returns>Julianisches Datum zum Jahresbeginn des zoroastrischen Jahres</returns>
	public static double FromZoroastrian(int year){ return MCalendar.FromZoroastrian(year, 1, 1); }

	// MCalendar.FromZoroastrian(int, int, int)
	/// <summary>
	/// Liefert das julianische Datum zum zoroastrischem Datum.
	/// </summary>
	/// <param name="year">Zoroastrische Jahreszahl.</param>
	/// <param name="month">Zoroastrische Monatszahl.</param>
	/// <param name="day">Zoroastrische Tageszahl.</param>
	/// <returns>Julianisches Datum zum zoroastrischem Datum.</returns>
	public static double FromZoroastrian(int year, int month, int day){ return MCalendar.EpochZoroastrian + (double)(365 * (year - 1)) + (double)(30 * (month - 1)) + (double)(day - 1); }

	// MCalendar.GregorianFromArmenian(int, int, int)
	/// <summary>
	/// Liefert die Anzahl der Ereignisse und das julianische Datum zum Ereignis zum armenischen Datum und zum gregorianischen Jahr.
	/// </summary>
	/// <param name="monthArmenian">Armenische Monatszahl.</param>
	/// <param name="dayArmenian">Armenische Tageszahl.</param>
	/// <param name="yearGregorian">Gregorianische Jahreszahl.</param>
	/// <returns>Anzahl der Ereignisse und das julianische Datum zum Ereignis des armenischen Datums und zum gregorianischen Jahr.</returns>
	public static (int count, double jd) GregorianFromArmenian(int monthArmenian, int dayArmenian, int yearGregorian)
	{
		// Lokale Felder einrichten
		double Jan01 = MCalendar.FromGregorian(yearGregorian);         // Jahresbeginn
		double Dec31 = MCalendar.FromGregorian(yearGregorian, 12, 31); // Jahresende
		CDate  Y     = MCalendar.ToArmenian(Jan01)                     // Armentische Datum zum Jahresbeginn
		int    Y     = 0;                                              // Armenische Jahreszahl
		int    M     = 0;                                              // Armenische Monatszahl
		int    D     = 0;                                              // Armenische Tageszahl

		// Lokale Felder einrichten
		double D1 = MCalendar.FromArmenian(D.Year    , monthArmenian, dayArmenian);
		double D2 = MCalendar.FromArmenian(D.Year + 1, monthArmenian, dayArmenian);

		// Erstes Datum verarbeiten
		if(Jan01 <= D1 && D1 <= Dec31)
			return(1, D1);

		// Zweites Datum verarbeiten
		if(Jan01 <= D2 && D2 <= Dec31)
			return(1, D2);

		// Kein Ereignis im gregorianischen Jahr
		return(0, 0.0);
	}

	// MCalendar.GregorianFromCoptic(int, int, int)
	/// <summary>
	/// Liefert die Anzahl der Ereignisse und das julianische Datum zum Ereignis zum koptischen Datum und zum gregorianischen Jahr.
	/// /// </summary>
	/// <param name="monthCoptic">Koptische Monatszahl.</param>
	/// <param name="dayCoptic">Koptische Tageszahl.</param>
	/// <param name="yearGregorian">Gregorianische Jahreszahl.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Anzahl der Ereignisse und das julianische Datum zum Ereignis zum koptischen Datum und zum gregorianischen Jahr.</returns>
	public static (int count, double jd) GregorianFromCoptic(int monthCoptic, int dayCoptic, int yearGregorian)
	{
		// Lokale Felder einrichten
		double Jan01 = MCalendar.FromGregorian(yearGregorian);         // Jahresbeginn
		double Dec31 = MCalendar.FromGregorian(yearGregorian, 12, 31); // Jahresende
		CDate  D     = MCalendar.ToCoptic(Jan01);                      // Koptisches Datum zum Jahresbeginn

		// Lokale Felder einrichten
		double D1 = MCalendar.FromCoptic(D.Year,     monthCoptic, dayCoptic); // Erstes mögliche Datum
		double D2 = MCalendar.FromCoptic(D.Year + 1, monthCoptic, dayCoptic); // Zweites mögliche Datum

		// Erstes Datum verarbeiten
		if(Jan01 <= D1 && D1 <= Dec31)
			return(1, D1);

		// Zweites Datum verarbeiten
		if(Jan01 <= D2 && D2 <= Dec31)
			return(1, D2);

		// Kein Ereignis im gregorianischen Jahr
		return(0, 0.0);
	}

	// MCalendar.GregorianFromEthiopic(int, int, int)
	/// <summary>
	/// Liefert die Anzahl der Ereignisse und das julianische Datum zum Ereignis zum äthiopischen Datum und zum gregorianischen Jahr.
	/// /// </summary>
	/// <param name="monthEthiopic">Äthiopisches Monatszahl.</param>
	/// <param name="dayEthiopic">Äthiopisches Tageszahl.</param>
	/// <param name="yearGregorian">Gregorianische Jahreszahl.</param>
	/// <returns>Anzahl der Ereignisse und das julianische Datum zum Ereignis zum äthiopischen Datum und zum gregorianischen Jahr.</returns>
	public static (int count, double jd) GregorianFromEthiopic(int monthEthiopic, int dayEthiopic, int yearGregorian)
	{
		// Lokale Felder einrichten
		double Jan01 = MCalendar.FromGregorian(yearGregorian);         // Jahresbeginn
		double Dec31 = MCalendar.FromGregorian(yearGregorian, 12, 31); // Jahresende
		int    Y     = 0;                                              // Ethiopische Jahreszahl
		int    M     = 0;                                              // Ethiopische Monatszahl
		int    D     = 0;                                              // Ethiopische Tageszahl

		// Ethiopisches Datum des Jahresbeginn berechnen
		MCalendar.ToEthiopic(Jan01, ref Y, ref M, ref D);

		// Lokale Felder einrichten
		double D1 = MCalendar.FromEthiopic(Y,     monthEthiopic, dayEthiopic); // Erstes mögliche Datum
		double D2 = MCalendar.FromEthiopic(Y + 1, monthEthiopic, dayEthiopic); // ZWeites mögliche Datum

		// Erstes Datum verarbeiten
		if(Jan01 <= D1 && D1 <= Dec31)
			return(1, D1);

		// Zweites Datum verarbeiten
		if(Jan01 <= D2 && D2 <= Dec31)
			return(1, D2);

		// Kein Ereignis im gregorianischen Jahr
		return(0, 0.0);
	}

	// MCalendar.GregorianFromHebrew(int, int, int)
	/// <summary>
	/// Liefert das julianische Datum des jüdischen Datums zum gregorianischen Jahr.
	/// </summary>
	/// <param name="monthHebrew">Jüdische Monatszahl.</param>
	/// <param name="dayHebrew">Jüdische Tageszahl.</param>
	/// <param name="yearGregorian">Gregorianische Jahrezahl.</param>
	/// <returns>Julianisches Datum des jüdischen Datums zum gregorianischen Jahr.</returns>
	public static double GregorianFromHebrew(int monthHebrew, int dayHebrew, int yearGregorian)
	{
		// Lokale Felder einrichten
		double Jan01 = MCalendar.FromGregorian(yearGregorian);         // Jahresbeginn
		double Dec31 = MCalendar.FromGregorian(yearGregorian, 12, 31); // Jahresende
		int    Y     = 0;                                              // Jüdische Jahreszahl
		int    M     = 0;                                              // Jüdische Monatszahl
		int    D     = 0;                                              // Jüdische Tageszahl

		// Jüdisches Datum des Jahresbeginn berechnen
		MCalendar.ToHebrew(Jan01, ref Y, ref M, ref D);

		// Lokale Felder einrichten
		double D1 = MCalendar.FromHebrew(Y,     monthHebrew, dayHebrew); // Erstes mögliche Datum
		double D2 = MCalendar.FromHebrew(Y + 1, monthHebrew, dayHebrew); // Zweites mögliche Datum

		// Zutreffende Tageszahl zurückgeben
		if(Jan01 <= D1 && D1 <= Dec31)
			return D1;
		return D2;
	}

	// MCalendar.GregorianFromHebrewBirthday(int, int, int, int)
	/// <summary>
	/// Liefert das julianische Datum zum jüdischen Geburtstag und zum gregorianischen Jahr.
	/// </summary>
	/// <param name="yearHebrew">Jüdische Jahreszahl.</param>
	/// <param name="monthHebrew">Jüdische Monatszahl.</param>
	/// <param name="dayHebrew">Jüdische Tageszahl.</param>
	/// <param name="yearGregorian">Gregorianische Jahreszahl.</param>
	/// <returns>Julianische Datum zum jüdischen Geburtstag und zum gregorianischen Jahr.</returns>
	public static double GregorianFromHebrewBirthday(int yearHebrew, int monthHebrew, int dayHebrew, int yearGregorian)
	{
		// Lokale Felder einrichten
		double Jan01 = MCalendar.FromGregorian(yearGregorian);         // Jahresbeginn
		double Dec31 = MCalendar.FromGregorian(yearGregorian, 12, 31); // Jahresende
		int    Y     = 0;                                              // Jüdische Jahreszahl
		int    M     = 0;                                              // Jüdische Monatszahl
		int    D     = 0;                                              // Jüdische Tageszahl

		// Jüdischews Datum des Jahresbeginn berechnen
		MCalendar.ToHebrew(Jan01, ref Y, ref M, ref D);

		// Lokale Felder einrichten
		double D1 = MCalendar.HebrewBirthday(yearHebrew, monthHebrew, dayHebrew, Y);     // Erstes mögliche Datum
		double D2 = MCalendar.HebrewBirthday(yearHebrew, monthHebrew, dayHebrew, Y + 1); // Zweites mögliche Datum

		// Zutreffende Tageszahl zurückgeben
		if(Jan01 <= D1 && D1 <= Dec31)
			return D1;
		return D2;
	}

	// MCalendar.GregorianFromHebrewYahrzeit(int, int, int, int)
	/// <summary>
	/// Liefert das julianische Datum zum jüdischen Todestag und zum gregorianischen Jahr.
	/// </summary>
	/// <param name="yearHebrew">Jüdische Jahreszahl.</param>
	/// <param name="monthHebrew">Jüdische Monatszahl.</param>
	/// <param name="dayHebrew">Jüdische Tageszahl.</param>
	/// <param name="yearGregorian">Gregorianische Jahreszahl</param>
	/// <returns>Julianische Datum zum jüdischen Todestag und zum gregorianischen Jahr.</returns>
	public static double GregorianFromHebrewYahrzeit(int yearHebrew, int monthHebrew, int dayHebrew, int yearGregorian)
	{
		// Lokale Felder einrichten
		double Jan01 = MCalendar.FromGregorian(yearGregorian);         // Jahresbeginn
		double Dec31 = MCalendar.FromGregorian(yearGregorian, 12, 31); // Jahresende
		int    Y     = 0;                                              // Jüdische Jahreszahl
		int    M     = 0;                                              // Jüdische Monatszahl
		int    D     = 0;                                              // Jüdische Tageszahl

		// Jüdisches Datum des Jahresbeginn berechnen
		MCalendar.ToHebrew(Jan01, ref Y, ref M, ref D);

		// Lokale Felder einrichten
		double D1 = MCalendar.HebrewYahrzeit(yearHebrew, monthHebrew, dayHebrew, Y);     // Erstes mögliche Datum
		double D2 = MCalendar.HebrewYahrzeit(yearHebrew, monthHebrew, dayHebrew, Y + 1); // Zweites mögliche Datum

		// Zutreffende Tageszahl zurückgeben
		if(Jan01 <= D1 && D1 <= Dec31)
			return D1;
		return D2;
	}

	// MCalendar.GregorianFromIslamic(int, int, int)
	/// <summary>
	/// Liefert die Anzahl der Ereignisse und die julianischen Daten zum islamischen Datum und zum gregorianischen Jahr.
	/// /// </summary>
	/// <param name="monthIslamic">Islamische Monatszahl.</param>
	/// <param name="dayIslamic">Islamische Tageszahl.</param>
	/// <param name="yearGreogorian">Gregorianische Jahreszahl.</param>
	/// <param name="jd1">Julianische Tageszahl der ersten Ereignisses.</param>
	/// <param name="jd2">Julianische Tageszahl des zweiten Ereignisses.</param>
	/// <returns>Anzahl der Ereignisse.</returns>
	public static (int count, double jd1, double jd2) GregorianFromIslamic(int monthIslamic, int dayIslamic, int yearGreogorian)
	{
		// Lokale Felder einrichten
		double Jan01 = MCalendar.FromGregorian(yearGreogorian);         // Jahresbeginn
		double Dec31 = MCalendar.FromGregorian(yearGreogorian, 12, 31); // Jahresende
		int    Y     = 0;                                               // Islamische Jahreszahl
		int    M     = 0;                                               // Islamische Monatszahl
		int    D     = 0;                                               // Islamische Tageszahl
		int    N     = 0;                                               // Anzahl der Ereignisse

		// Islamisches Datum des Jahresbeginn berechnen
		MCalendar.ToIslamic(Jan01, ref Y, ref M, ref D);

		// Lokale Felder einrichten
		double D1 = MCalendar.FromIslamic(Y,     monthIslamic, dayIslamic); // Erstes mögliche Datum
		double D2 = MCalendar.FromIslamic(Y + 1, monthIslamic, dayIslamic); // Zweites mögliche Datum
		double D3 = MCalendar.FromIslamic(Y + 2, monthIslamic, dayIslamic); // Drittes mögliche Datum

		// Rückgabewerte einrichten
		double jd1 = 0.0;
		double jd2 = 0.0;

		// Erstes Datum verarbeiten
		if(Jan01 <= D1 && D1 <= Dec31)
		{
			// Tageszahl setzen und Anzahl inkrementieren
			jd1 = D1;
			N++;
		}

		// Zweites Datum verarbeiten
		if(Jan01 <= D2 && D2 <= Dec31)
		{
			// Nach Anzahl der Ereignisse unterscheiden
			if(N == 0)
			{
				// Tageszahl setzen und Anzahl inkrementieren
				jd1 = D2;
				N++;
			}
			else
			{
				// Tageszahl setzen und Anzahl inkrementieren
				jd2 = D2;
				N++;
			}
		}

		// Drittes Datum
		if(Jan01 <= D3 && D3 <= Dec31)
		{
			// Tageszahl setzen und Anzahl inkrementieren
			jd2 = D3;
			N++;
		}

		// Rückgabe
		return(M, jd1, jd2);
	}

	// MCalendar.GregorianFromJulian(int, int, int)
	/// <summary>
	/// Liefert die Anzahl der Ereignisse und das juliansische Datum zum Eregnis zum gregorianischen Jahr.
	/// /// </summary>
	/// <param name="monthJulian">Julianische Tageszahl.</param>
	/// <param name="dayJulian">Julianische Monatszahl.</param>
	/// <param name="yearGregorian">Gregorianische Jahreszahl.</param>
	/// <returns>Anzahl der Ereignisse und das juliansische Datum zum Eregnis zum gregorianischen Jahr.</returns>
	public static (int count, double jd) GregorianFromJulian(int monthJulian, int dayJulian, int yearGregorian)
	{
		// Lokale Felder einrichten
		double Jan01 = MCalendar.FromGregorian(yearGregorian);         // Jahresbeginn
		double Dec31 = MCalendar.FromGregorian(yearGregorian, 12, 31); // Jahresende
		int    Y     = 0;                                              // Julianische Jahreszahl
		int    M     = 0;                                              // Julianische Monatszahl
		int    D     = 0;                                              // Julianische Tageszahl

		// Julianisches Datum des Jahresbeginn berechnen
		MCalendar.ToJulian(Jan01, ref Y, ref M, ref D);

		// Lokale Felder einrichten
		double D1 = MCalendar.FromJulian(Y,                   monthJulian, dayJulian); // Erstes mögliche Datum
		double D2 = MCalendar.FromJulian(Y == -1 ? 1 : Y + 1, monthJulian, dayJulian); // Zweites mögliche Datum

		// Erstes Datum verarbeiten
		if(Jan01 <= D1 && D1 <= Dec31)
			return(1, D1);

		// Zweites Datum verarbeiten
		if(Jan01 <= D2 && D2 <= Dec31)
			return(1, D2);

		// Kein Ereignis im gregorianischen Jahr
		return(0, 0.0);
	}

	// MCalendar.GregorianYear(double)
	/// <summary>
	/// Liefert die gregorianische Jahreszahl zum julianischen Datum.
	/// </summary>
	/// <param name="jd">Julianisches Datum.</param>
	/// <returns>Gregorianische Jahreszahl zum julianischen Datum.</returns>
	public static int GregorianYear(double jd)
	{
		// Lokale Felder einrichten
		double D0   = jd - MCalendar.EpochGregorian;
		double N400 = MMath.Floor(D0 / 146097.0);
		double D1   = MMod .Mod  (D0,  146097.0);
		double N100 = MMath.Floor(D1 /  36524.0);
		double D2   = MMod .Mod  (D1,   36524.0);
		double N4   = MMath.Floor(D2 /   1461.0);
		double D3   = MMod .Mod  (D2,    1461.0);
		double N1   = MMath.Floor(D3 /    365.0);

		// Jahreszahl berechnen
		return (int)(400.0 * N400 + 100.0 * N100 + 4.0 * N4 + N1 + ((N100 == 4.0 || N1 == 4.0) ? 0.0 : 1.0));
	}

	// MCalendar.HebrewBirthday(int, int, int, int)
	/// <summary>
	/// Liefert das julianische Datum zum Geburtstag zum jüdischen Datum und zum jüdischen Jahr.
	/// </summary>
	/// <param name="year">Jüdische Jahreszahl des Geburtstages.</param>
	/// <param name="month">Jüdische Monatszahl des Geburtstages.</param>
	/// <param name="day">Jüdische Tageszahl des Geburtstages.</param>
	/// <param name="yearHebrew">Jüdische Jahreszahl.</param>
	/// <returns>Julianisches Datum zum Geburtstag zum jüdischen Datum und zum jüdischen Jahr.</returns>
	public static double HebrewBirthday(int year, int month, int day, int yearHebrew)
	{
		// Tageszahl berechnen
		if(month == MCalendar.HebrewLastMonthOfYear(year)) return MCalendar.FromHebrew(yearHebrew, MCalendar.HebrewLastMonthOfYear(yearHebrew), day);
		return MCalendar.FromHebrew(yearHebrew, month, 1) + day - 1;
	}

	// MCalendar.HebrewDaysElapsed(int)
	/// <summary>
	/// Liefert die Anzahl der verstrichenen Tage seit Beginn der jüdischen Zeitzählung zum jüdischen Jahr.
	/// </summary>
	/// <param name="year">Jüdische Jahreszahl.</param>
	/// <returns>Anzahl der verstrichenen Tage seit Beginn der jüdischen Zeitzählung zum jüdischen Jahr.</returns>
	public static int HebrewDaysElapsed(int year)
	{
		// Lokale Felder einrichten
		double M = MMath.Floor((235.0 * (double)year - 234.0) / 19.0);
		double P = 12084.0 + 13753.0 * M;
		int    D = 29 * (int)M + (int)MMath.Floor(P / 25920.0);

		// Anzahl berechnen
		return D + ((int)MMod.Mod(3.0 * (double)(D + 1), 7.0) < 3 ? 1 : 0);
	}

	// MCalendar.HebrewDaysInYear(int)
	/// <summary>
	/// Liefert die Anzahl der Tage im Jahr zur jüdischen Jahreszahl.
	/// </summary>
	/// <param name="year">Jüdische Jahreszahl.</param>
	/// <returns>Anzahl der Tage im Jahr zur jüdischen Jahreszahl.</returns>
	public static int HebrewDaysInYear(int year){ return (int)(MCalendar.FromHebrew(year + 1) - MCalendar.FromHebrew(year)); }

	// MCalendar.HebrewIsLongMarheshvan(int)
	/// <summary>
	/// Liefert TRUE, falls der Monat Marhershvan 30 Tage zur jüdischen Jahreszahl zählt.
	/// </summary>
	/// <param name="year">Jüdische Jahreszahl.</param>
	/// <returns>TRUE, falls der Monat Marhershvan 30 Tage zur jüdischen Jahreszahl zählt.</returns>
	public static bool HebrewIsLongMarheshvan(int year)
	{
		// Schalter berechnen
		int D = MCalendar.HebrewDaysInYear(year);
		return D == 355 || D == 385 ? true : false;
	}

	// MCalendar.HebrewIsShortKislev(int)
	/// <summary>
	/// Liefert TRUE, falls der Monat Kislev 29 Tage zur jüdischen Jahreszahl zählt.
	/// </summary>
	/// <param name="year">Jüdische Jahreszahl.</param>
	/// <returns>TRUE, falls der Monat Kislev 29 Tage zur jüdischen Jahreszahl zählt.</returns>
	public static bool HebrewIsShortKislev(int year)
	{
		// Schalter berechnen
		long D = MCalendar.HebrewDaysInYear(year);
		return D == 353 || D == 383 ? true : false;
	}

	// MCalendar.HebrewLastDayOfMonth(int, int)
	/// <summary>
	/// Liefert die Anzahl der Tage im Monat zur jüdischen Jahreszahl.
	/// </summary>
	/// <param name="year">Jüdische Jahreszahl.</param>
	/// <param name="month">Jüdische Monatszahl.</param>
	/// <returns>Anzahl der Tage im Monat zur jüdischen Jahreszahl.</returns>
	public static int HebrewLastDayOfMonth(int year, int month)
	{
		// Nach Monat unterscheiden
		switch(month)
		{
			// Monate mit 29 Kalendertagen
			case 2: case 4: case 6: case 10: case 13:
				return 29;

			// Monate mit 30 Kalendertagen
			case 1: case 3: case 5: case 7: case 11:
				return 30;

			// Monat Marheshvan
			case 8:
				return MCalendar.HebrewIsLongMarheshvan(year) ? 30 : 29;

			// Monat Kislev
			case 9:
				return MCalendar.HebrewIsShortKislev(year) ? 29 : 30;

			// Monat Adar
			case 12:
				return MCalendar.IsLeapYearHebrew(year) ? 30 : 29;
		}
		throw new InvalidArgumentException("Ungültiger jüdischer Monat.");
	}

	// MCalendar.HebrewLastMonthOfYear(int)
	/// <summary>
	/// Liefert die Monatszahl zum letzten Monat zur jüdischen Jahreszahl {12 = Adar rishon, 13 = Adar sheni}.
	/// </summary>
	/// <param name="year">Jüdische Jahreszahl.</param>
	/// <returns>Monatszahl zum letzten Monat zur jüdischen Jahreszahl {12 = Adar rishon, 13 = Adar sheni}.</returns>
	public static int HebrewLastMonthOfYear(int year){ return MCalendar.IsLeapYearHebrew(year) ? 13 : 12; }

	// MCalendar.HebrewMolad(int, int)
	/// <summary>
	/// Liefert die julianische Tageszahl des Molads zur jüdischen Monatzahl und jüdischen Jahreszahl.
	/// </summary>
	/// <param name="year">Jüdische Jahreszahl.</param>
	/// <param name="month">Jüdische Monatszahl.</param>
	/// <returns>Julianische Tageszahl des Molads zur jüdischen Monatzahl und jüdischen Jahreszahl.</returns>
	public static double HebrewMolad(int year, int month)
	{
		// Tageszahl des Ereignis berechnen
		double V = (double)(month - 7) + MMath.Floor((235.0 * (double)year - 234.0) / 19.0);
		return MCalendar.EpochHebrew - 876.0 / 25920.0 + V * (29.0 + 13753.0 / 25920.0);
	}

	// MCalendar.HebrewNewYear(int)
	/// <summary>
	/// Liefert die julianische Tageszahl des Jahresbeginns zur jüdischen Jahreszahl.
	/// </summary>
	/// <param name="year">Jüdische Jahreszahl</param>
	/// <returns>Julianische Tageszahl des Jahresbeginns zur jüdischen Jahreszahl.</returns>
	public static double HebrewNewYear(int year){ return MCalendar.EpochHebrew + (double)MCalendar.HebrewDaysElapsed(year) + (double)MCalendar.HebrewNewYearDelay(year); }

	// MCalendar.HebrewNewYearDelay(int)
	/// <summary>
	/// Liefert die Anzahl der Aufschubtage des Jahresbeginns zur jüdischen Jahrezahl.
	/// </summary>
	/// <param name="year">Jüdische Jahreszahl.</param>
	/// <returns>Anzahl der Aufschubtage des Jahresbeginns zur jüdischen Jahrezahl.</returns>
	public static int HebrewNewYearDelay(int year)
	{
		// Lokale Felder einrichten
		int N0 = MCalendar.HebrewDaysElapsed(year - 1);
		int N1 = MCalendar.HebrewDaysElapsed(year);
		int N2 = MCalendar.HebrewDaysElapsed(year + 1);

		// Anzahl der Tage berechnen
		if(N2 - N1 == 356)
			return 2;
		if(N1 - N0 == 382)
			return 1;
		return 0;
	}

	// MCalendar.HebrewYahrzeit(int, int, int, int)
	/// <summary>
	/// Liefert das julianische Datum zum Todestages zum jüdischen Datum und zur jüdischen Jahreszahl.
	/// </summary>
	/// <param name="year">Jüdische Jahreszahl des Todestages.</param>
	/// <param name="month">Jüdische Monatszahl des Todestages.</param>
	/// <param name="day">Jüdische Tageszahl des Todestages.</param>
	/// <param name="yearHebrew">Jüdische Jahreszahl.</param>
	/// <returns>Julianische Datum des Todestages zum jüdischen Datum und zur jüdischen Jahreszahl.</returns>
	public static double HebrewYahrzeit(int year, int month, int day, int yearHebrew)
	{
		// Nach Todesmonat unterscheiden
		if(month ==  8 && day == 30 && !MCalendar.HebrewIsLongMarheshvan(year + 1))
			return MCalendar.FromHebrew(yearHebrew,  9,  1);
		if(month ==  9 && day == 30 &&  MCalendar.HebrewIsShortKislev(year + 1))
			return MCalendar.FromHebrew(yearHebrew, 10,  1);
		if(month == 12 && day == 30 && !MCalendar.IsLeapYearHebrew(yearHebrew))
			return MCalendar.FromHebrew(yearHebrew, 11, 30);
		if(month == 13)
			return MCalendar.FromHebrew(yearHebrew, MCalendar.HebrewLastMonthOfYear(yearHebrew), day);
		return MCalendar.FromHebrew(yearHebrew, month, 1) + day - 1;
	}

	// MCalendar.IsLeapYear(int)
	/// <summary>
	/// Liefert TRUE, falls das gregorianische Jahr ein Schaltjahr ist.
	/// </summary>
	/// <param name="year">Gregorianische Jahreszahl.</param>
	/// <returns>TRUE, falls das gregorianische Jahr ein Schaltjahr ist.</returns>
	public static bool IsLeapYear(int year){ return (year % 4 == 0) && ((year % 100 != 0) || (year % 400 == 0)) ? true : false; }

	// MCalendar.IsLeapYearCoptic(int)
	/// <summary>
	/// Liefert TRUE, falls das koptische Jahr ein Schaltjahr ist.
	/// </summary>
	/// <param name="year">Koptische Jahreszahl.</param>
	/// <returns>TRUE, falls das koptische Jahr ein Schaltjahr ist.</returns>
	public static bool IsLeapYearCoptic(int year){ return MMod.Mod(year, 4.0) == 3.0; }

	// MCalendar.IsLeapYearEthiopic(int)
	/// <summary>
	/// Liefert TRUE, falls das äthiopische Jahr ein Schaltjahr ist.
	/// </summary>
	/// <param name="year">Äthiopische Jahreszahl.</param>
	/// <returns>TRUE, falls das äthiopische Jahr ein Schaltjahr ist.</returns>
	public static bool IsLeapYearEthiopic(int year){ return MMod.Mod(year, 4.0) == 3.0; }

	// MCalendar.IsLeapYearHebrew(int)
	/// <summary>
	/// Liefert TRUE, falls das jüdische Jahr ein Schaltjahr ist.
	/// </summary>
	/// <param name="year">Jüdische Jahreszahl.</param>
	/// <returns>TRUE, falls das jüdische Jahr ein Schaltjahr ist.</returns>
	public static bool IsLeapYearHebrew(int year){ return MMod.Mod((7.0 * year + 1.0), 19.0) < 7.0; }

	// MCalendar.IsLeapYearIslamic(int)
	/// <summary>
	/// Liefert TRUE, falls das islamische Jahr ein Schaltjahr ist.
	/// </summary>
	/// <param name="year">Islamische Jahreszahl.</param>
	/// <returns>TRUE, falls das islamische Jahr ein Schaltjahr ist.</returns>
	public static bool IsLeapYearIslamic(int year){ return MMod.Mod(14.0 + 11.0 * (double)year, 30.0) < 11.0; }

	// MCalendar.IsLeapYearJulian(int)
	/// <summary>
	/// Liefert TRUE, falls das julianische Jahr ein Schaltjahr ist.
	/// </summary>
	/// <param name="year">Julianische Jahreszahl.</param>
	/// <returns>TRUE, falls das julianische Jahr ein Schaltjahr ist.</returns>
	public static bool IsLeapYearJulian(int year){ return (year > 0) ? MMod.Mod((double)year, 4.0) == 0.0 : MMod.Mod((double)year, 4.0) == 3.0; }

	// MCalendar.IsoCalendarWeek(double)
	/// <summary>
	/// Liefert die Kalenderwoche zum julianischen Datum.
	/// </summary>
	/// <param name="jd">Julianisches Datum.</param>
	/// <returns>Kalenderwoche zum julianischen Datum.</returns>
	public static int IsoCalendarWeek(double jd)
	{
		// Lokale Felder einrichten
		int    Y = MCalendar.GregorianYear(jd);
		double D = jd - MCalendar.IsoNewYear(Y);

		// Tageszahl prüfen
		if(D < 0.0 || D > 363.0)
		{
			if(D < 0) D += MCalendar.IsoDaysInYear(Y - 1);
			else
			{
				double L = (double)MCalendar.IsoDaysInYear(Y);
				if(D >= L) D -= L;
			}
		}

		// Rückgabewert berechnen
		return (int)(MMath.Floor(D / 7.0) + 1.0);
	}

	// MCalendar.IsoDaysInYear(int)
	/// <summary>
	/// Liefert die Anzahl der Tage zur gregorianischen Jahreszahl.
	/// </summary>
	/// <param name="year">Gregorianische Jahreszahl.</param>
	/// <returns>Anzahl der Tage zur gregorianischen Jahreszahl.</returns>
	public static int IsoDaysInYear(int year){ return (int)(MCalendar.IsoNewYear(year + 1) - MCalendar.IsoNewYear(year)); }

	// MCalendar.IsoNewYear(int)
	/// <summary>
	/// Liefert das julianische Datum des Jahresbeginns zur gregorianischen Jahreszahl.
	/// </summary>
	/// <param name="year">Gregorianische Jahreszahl.</param>
	/// <returns>Julianisches Datum des Jahresbeginns zur gregorianischen Jahreszahl.</returns>
	public static double IsoNewYear(int year){ return MCalendar.DayOnOrAfter((MCalendar.FromGregorian(year - 1, 12, 29)), EWeekDay.Monday, 1); }

	// MCalendar.LastDayOfMonth(int, int)
	/// <summary>
	/// Liefert die Anzahl der Tage im Monat zur gregorianischen Jahreszahl.
	/// </summary>
	/// <param name="year">Gregorianische Jahreszahl.</param>
	/// <param name="month">Gregorianische Monatszahl.</param>
	/// <returns>Anzahl der Tage im Monat zur gregorianischen Jahreszahl.</returns>
	public static int LastDayOfMonth(int year, int month)
	{
		// Nach Monatszahl unterscheiden
		switch(month)
		{
			// Monate mit 31 Kalendertagen
			case 1: case 3: case 5: case 7: case 8: case 10: case 12:
				return 31;

			// Monate mit 30 Kalendertagen
			case 4: case 6: case 9: case 11:
				return 30;

			// Monat Februar
			case 2:
				return MCalendar.IsLeapYear(year) ? 29 : 28;
		}

		// Ausnahme auslösen
		throw new InvalidArgumentException(string.Format("Die Monatszahl '{0}' ist ungültig.", month));
	}

	// MCalendar.NewYear(int)
	/// <summary>
	/// Liefert das julianische Datum des Jahresbeginns zur gregorianischen Jahreszahl.
	/// </summary>
	/// <param name="year">Gregorianische Jahreszahl</param>
	/// <returns>Julianische Datum des Jahresbeginns zur gregorianischen Jahreszahl.</returns>
	public static double NewYear(int year){ return MCalendar.FromGregorian(year, 1, 1); }

	// MCalendar.ToArmenian()
	/// <summary>
	/// Liefert das armenischen Datum zum heutigen Tag.
	/// </summary>
	/// <returns>Armenisches Datum zum heutigen Tag.</returns>
	public static CDate ToArmenian(){ return MCalendar.ToArmenian(MCalendar.FromGregorian(new CDate(DateTime.Today))); }

	// MCalendar.ToArmenian(double)
	/// <summary>
	/// Liefert das armenische Datum zum julianischen Datum.
	/// </summary>
	/// <param name="jd">Julianisches Datum.</param>
	/// <returns>Armenische Datum zum julianischen Datum.</returns>
	public static CDate ToArmenian(double jd)
	{
		// Lokale Felder einrichten
		int N = (int)MMath.Floor(jd - MCalendar.EpochArmenian);
		int Y = (int)MMath.Floor(N / 365.0) + 1;
		int M = (int)MMath.Floor(MMod.Mod(N, 365.0) / 30.0) + 1;
		int D = N - 365 * (Y - 1) - 30 * (M - 1) + 1;

		// Rückgabe
		return new(Y, M, D);
	}

	// MCalendar.ToCoptic()
	/// <summary>
	/// Liefert das koptische Datum zum heutigen Tag.
	/// </summary>
	/// <returns>Koptisches Datum zum heutigen Tag.</returns>
	public static CDate ToCoptic(){ return MCalendar.ToCoptic(MCalendar.FromGregorian(new CDate(DateTime.Today))); }

	// MCalendar.ToCoptic(double)
	/// <summary>
	/// Liefert das koptische Datum zum julianischen Datum.
	/// </summary>
	/// <param name="jd">Julianisches Datum.</param>
	/// <returns>Koptisches Datum zur julianischen Datum.</returns>
	public static CDate ToCoptic(double jd)
	{
		// Lokalen Felder einrichten
		int Y = (int)MMath.Floor((4.0 * (jd - MCalendar.EpochCoptic) + 1463.0) / 1461.0);
		int M = (int)MMath.Floor((jd  - MCalendar.FromCoptic(Y      )) / 30.0) + 1;
		int D = (int)MMath.Floor( jd  - MCalendar.FromCoptic(Y, M, 1)) + 1;

		// Rückgabe
		return new(Y, M, D);
	}

	// MCalendar.ToDate(double)
	/// <summary>
	/// Liefert das DateTime zum julianischen Datum.
	/// </summary>
	/// <param name="jd">Julianisches Datum.</param>
	/// <returns>Datumswert zur julianischen Tageszahl.</returns>
	/// <exception cref="System.ArgumentOutOfRangeException">Wird ausgelöst, falls die julianischen Tageszahl außerhalb des Gültigkeitszeitraumes ist.</exception>
	public static DateTime ToDate(double jd)
	{
		// Gültige Tageszahl sicherstellen
		if(jd - Dbl_SystemDateTimeMin < 0)
			throw new System.ArgumentOutOfRangeException("Die julianischen Tageszahl liegt vor dem Beginn des Gültigkeitszeitraumes.");

		// Gültige Tageszahl sicherstellen
		if(Dbl_SystemDateTimeMax - jd < 0)
			throw new System.ArgumentOutOfRangeException("Die julianischen Tageszahl liegt nach dem Ende des Gültigkeitszeitraumes.");

		// Lokale Felder einrichten
		CDate D = MCalendar.ToGregorian(jd);
		return new(D.Year, D.Month, D.Day);
	}

	// MCalendar.ToEgytian()
	/// <summary>
	/// Liefert das ägyptische Datum zum heutigen Tag.
	/// </summary>
	/// <returns>Ägyptisches Datum zum heutigen Tag.</returns>
	public static CDate ToEgytian(){ return MCalendar.ToEgytian(MCalendar.FromGregorian(new CDate(DateTime.Today))); }

	// MCalendar.ToEgytian(double)
	/// <summary>
	/// Liefert das ägyptische Datum zum julianischen Datum.
	/// </summary>
	/// <param name="jd">Julianisches Datum.</param>
	/// <returns>Ägyptisches Datum zum julianischen Datum.</returns>
	public static CDate ToEgytian(double jd)
	{
		// Lokale Felder einrichten
		int N = (int)MMath.Floor(jd - MCalendar.EpochEgyptian);
		int Y = (int)MMath.Floor(N / 365.0) + 1;
		int M = (int)MMath.Floor(MMod.Mod(N, 365.0) / 30.0) + 1;
		int D = N - 365 * (Y - 1) - 30 * (M - 1) + 1;

		// Rückgabe
		return new(Y, M, D);
	}

	// MCalendar.ToEthiopic()
	/// <summary>
	/// Liefert das äthiopische Datum zum heutigen Tag.
	/// </summary>
	/// <returns>Äthiopisches Datum zum heutigen Tag.</returns>
	public static CDate ToEthiopic(){ return MCalendar.ToEthiopic(MCalendar.FromGregorian(new CDate(DateTime.Today))); }

	// MCalendar.ToEthiopic(double)
	/// <summary>
	/// Liefert das äthiopische Datum zum julianischen Datum.
	/// </summary>
	/// <param name="jd">Julianisches Datum.</param>
	/// <returns>Äthiopisches Datum zum julianischen Datum.</returns>
	public static CDate ToEthiopic(double jd)
	{
		// Lokalen Felder einrichten
		int Y = (int)MMath.Floor((4.0 * (jd - MCalendar.EpochEthiopic) + 1463.0) / 1461.0);
		int M = (int)MMath.Floor((jd - MCalendar.FromEthiopic(Y)) / 30.0) + 1;
		int D = (int)MMath.Floor( jd - MCalendar.FromEthiopic(Y, M, 1)) + 1;

		// Rückgabe
		return new(Y, M, D);
	}

	// MCalendar.ToGregorian()
	/// <summary>
	/// Liefert das gregorianische Datum zum heutigen Tag.
	/// </summary>
	/// <returns>Gregorianisches Datum zum heutigen Tag.</returns>
	public static CDate ToGregorian(){ return MCalendar.ToGregorian(MCalendar.FromGregorian(new CDate(DateTime.Today)));	}

	// MCalendar.ToGregorian(double)
	/// <summary>
	/// Liefert das gregorianische Datum zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Gregorianische Datum zur julianischen Tageszahl.</returns>
	public static CDate ToGregorian(double jd)
	{
		// Lokale Felder einrichten
		int Y = MCalendar.GregorianYear(jd);
		int P = (int)MMath.Floor(jd - MCalendar.FromGregorian(Y));
		int C = 0;

		if(jd >= MCalendar.FromGregorian(Y, 3, 1))
			C += MCalendar.IsLeapYear(Y) ? 1 : 2;

		int M = (int)MMath.Floor((12.0 * (P + C) + 373.0) / 367.0);
		int D = (int)MMath.Floor(jd - MCalendar.FromGregorian(Y, M, 1)) + 1;

		// Rückgabe
		return new(Y, M, D);
	}

	// MCalendar.ToHebrew()
	/// <summary>
	/// Liefert das jüdische Datum zum heutigen Tag.
	/// </summary>
	/// <returns>Jüdisches Datum zum heutigen Tag.</returns>
	public static CDate ToHebrew(){ return MCalendar.ToHebrew(MCalendar.FromGregorian(new CDate(DateTime.Today)));	}

	// MCalendar.ToHebrew(double)
	/// <summary>
	/// Liefert das jüdische Datum zum julianischen Datum.
	/// </summary>
	/// <param name="jd">Julianisches Datum.</param>
	/// <returns>Jüdisches Datum zur julianischen Datum.</returns>
	public static CDate ToHebrew(double jd)
	{
		// Lokale Felder einrichten
		int Y = (int)MMath.Floor(98496.0 * (jd - MCalendar.EpochHebrew) / 35975351.0);
		int M = 1;
		int D = 1;

		while(MCalendar.HebrewNewYear(Y + 1) <= jd) Y++;
		M = jd < MCalendar.FromHebrew(Y) ? 7 : 1;
		while(jd > MCalendar.FromHebrew(Y, M, MCalendar.HebrewLastDayOfMonth(Y, M))) M++;
		D = (int)(jd - MCalendar.FromHebrew(Y, M, 1)) + 1;

		// Rückgabe
		return new(Y, M, D);
	}

	// MCalendar.ToIslamic()
	/// <summary>
	/// Liefert das islamische Datum zum heutigen Tag.
	/// </summary>
	/// <returns>Islamisches Datum zum heutigen Tag.</returns>
	public static CDate ToIslamic(){ return MCalendar.ToIslamic(MCalendar.FromGregorian(new CDate(DateTime.Today))); }

	// MCalendar.ToIslamic(double)
	/// <summary>
	/// Liefert das islamische Datum zum julianischen Datum.
	/// </summary>
	/// <param name="jd">Julianisches Datum.</param>
	/// <returns>Islamisches Datum zum julianischen Datum.</returns>
	public static CDate ToIslamic(double jd)
	{
		// Lokale Felder einrichten
		int    Y = (int)MMath.Floor((30.0 * (jd - MCalendar.EpochIslamic) + 10646.0) / 10631.0);
		double P = jd - MCalendar.FromIslamic(Y);
		int    M = (int)MMath.Floor((11.0 * P + 330.0) / 325.0);
		int    D = (int)(jd - MCalendar.FromIslamic(Y, M, 1)) + 1;

		// Rückgabe
		return new(Y, M, D);
	}

	// MCalendar.ToJulian()
	/// <summary>
	/// Liefert das julianische Datum zum heutigen Tag.
	/// </summary>
	/// <returns>Julianisches Datum zum heutigen Tag.</returns>
	public static CDate ToJulian(){ return MCalendar.ToJulian(MCalendar.FromGregorian(new CDate(DateTime.Today)));	}

	// MCalendar.ToJulian(double)
	/// <summary>
	/// Liefert das julianische Datum zum julianischen Datum.
	/// </summary>
	/// <param name="jd">Julianisches Datum.</param>
	/// <returns>Julianisches Datum zum julianischen Datum.</returns>
	public static CDate ToJulian(double jd)
	{
		// Lokale Felder einrichten
		int A = (int)MMath.Floor((4.0 * (jd - MCalendar.EpochJulian) + 1464.0) / 1461.0);
		int Y = A <= 0 ? A - 1 : A;
		int P = (int)MMath.Floor(jd - MCalendar.FromJulian(Y));
		int C = 0;

		if(jd >= MCalendar.FromJulian(Y, 3, 1))
			C += MCalendar.IsLeapYearJulian(Y) ? 1 : 2;

		int M = (int)MMath.Floor((12.0 * (P + C) + 373.0) / 367.0);
		int D = (int)MMath.Floor(jd - MCalendar.FromJulian(Y, M, 1)) + 1;

		// Rückgabe
		return new(Y, M, D);
	}

	// MCalendar.ToTime(double)
	/// <summary>
	/// Liefert die Zeit zum julianischen Datum.
	/// </summary>
	/// <param name="jd">Julianisches Datum.</param>
	/// <returns>Zeit zum julianischen Datum..</returns>
	public static TimeOnly ToTime(double jd)
	{
		// Lokale Felder einrichten
		double P = MMod.Mod((jd - 0.5).Round(0) * (double)Dbl_SecondsPerDay, (double)Dbl_SecondsPerDay);
		int    H = (int)(MMath.Floor(P / 3600.0));
		P        = MMod.Mod(P, 3600.0);
		int    M = (int)(MMath.Floor(P / 60.0));
		int    S = (int)(MMod .Mod(P, 60.0));

		// Rückgabe
		return new(H, M, S);
	}

	// MCalendar.ToYazdegerd()
	/// <summary>
	/// Liefert das yazdegerdische Datum zum heutigen Tag.
	/// </summary>
	/// <returns>Yazdegerdisches Datum zum heutigen Tag.</returns>
	public static CDate ToYazdegerd(){ return MCalendar.ToYazdegerd(MCalendar.FromGregorian(new CDate(DateTime.Today))); }

	// MCalendar.ToYazdegerd(double)
	/// <summary>
	/// Liefert das yazdegerdische Datum zum julianischen Datum.
	/// </summary>
	/// <param name="jd">Julianisches Datum.</param>
	/// <returns>Yazdegerdisches Datum zum julianischen Datum.</returns>
	public static CDate ToYazdegerd(double jd)
	{
		// Lokale Felder einrichten
		int N = (int)MMath.Floor(jd - MCalendar.EpochYazdegerd);
		int Y = (int)MMath.Floor(N / 365.0) + 1;
		int M = (int)MMath.Floor(MMod.Mod(N, 365.0) / 30.0) + 1;
		int D = N - 365 * (Y - 1) - 30 * (Y - 1) + 1;

		// Rückgabe
		return new(Y, M, D);
	}

	// MCalendar.ToZoroastrian()
	/// <summary>
	/// Liefert das zoroastrische Datum zum heutigen Tag.
	/// </summary>
	/// <returns>Zoroastrisches Datum zum heutigen Tag.</returns>
	public static CDate ToZoroastrian(){ return MCalendar.ToZoroastrian(MCalendar.FromGregorian(new CDate(DateTime.Today))); }

	// MCalendar.ToZoroastrian(double)
	/// <summary>
	/// Liefert das zoroastrische Datum zum julianischen Datum.
	/// </summary>
	/// <param name="jd">Julianisches Datum.</param>
	/// <returns>Zoroastrisches Datum zum julianischen Datum.</returns>
	public static CDate ToZoroastrian(double jd)
	{
		// Lokale Felder einrichten
		int N = (int)MMath.Floor(jd - MCalendar.EpochZoroastrian);
		int Y = (int)MMath.Floor(N / 365.0) + 1;
		int M = (int)MMath.Floor(MMod.Mod(N, 365.0) / 30.0) + 1;
		int D = N - 365 * (Y - 1) - 30 * (M - 1) + 1;

		// Rückgabe
		return new CDate(Y, M, D);
	}

	// MCalendar.YearFragment()
	/// <summary>
	/// Liefert den Jahresbruchteil zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Jahresbruchteil zur aktuellen Systemzeit.</returns>
	public static double YearFragment() 
	{
		// Lokale Felder einrichten und Jahresbruchteil berechnen
		double jd = DateTime.Now.ToJdn();
		return MCalendar.YearFragment(jd);
	}

	// MCalendar.YearFragment(double)
	/// <summary>
	/// Liefert den Jahresbruchteil zum julianischen Datum.
	/// </summary>
	/// <param name="jd">Julianisches Datum.</param>
	/// <returns>Jahresbruchteil zum julianischen Datum.</returns>
	public static double YearFragment(double jd)
	{
		// Lokale Felder einrichten
		int    y   = MCalendar.GregorianYear(jd);
		double jd0 = MCalendar.FromGregorian(y);
		double jd1 = MCalendar.FromGregorian(y + 1);

		// Jahresbruchteil berechnen
		return (y - jd0) / (jd1 - jd0);
	}
}
