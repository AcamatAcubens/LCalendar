using Acamat.LCore;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Stellt ein generisches Kalenderdatum dar.
/// </summary>
public class CDate : CListItem
{
	// ------------- //
	// Konstruktoren //
	// ------------- //
	// CDate()
	/// <summary>
	/// Standardkonstruktor.
	/// </summary>
	public CDate()
	{
		// Felder einrichten
		this.Day   = 0;
		this.Month = 0;
		this.Year  = 0;
	}

	// CDate(int)
	/// <summary>
	/// Konstruktor mit einer Angabe zur Identität.
	/// </summary>
	/// <param name="id">Identität.</param>
	public CDate(int id) : this(){ this.Id = id; }

	// CDate(CDate)
	/// <summary>
	/// Konstruktor mit einer Angabe zum Datum.
	/// </summary>
	/// <param name="item">Datum.</param>
	public CDate(CDate item)
	{
		// Argumente anwenden
		this.Day   = item.Day;
		this.Month = item.Month;
		this.Year  = item.Year;
	}

	// CDate(CDate, int)
	/// <summary>
	/// Konstruktor mit einer Angabe zum Datum und zur Identität.
	/// </summary>
	/// <param name="item">Datum.</param>
	/// <param name="id">Identität.</param>
	/// <returns></returns>
	public CDate(CDate item, int id) : this(item){ this.Id = id; }

	// CDate(int, int, int)
	/// <summary>
	/// Konstruktor mit einer Angabe zur Jahres-, Monats- und Tageszahl.
	/// </summary>
	/// <param name="year">Jahreszahl.</param>
	/// <param name="month">Monatszahl.</param>
	/// <param name="day">Tageszahl.</param>
	public CDate(int year, int month, int day)
	{
		// Felder einrichten
		this.Day   = day;
		this.Month = month;
		this.Year  = year;
	}

	// CDate(int, int, int, int)
	/// <summary>
	/// Konstruktor mit einer Angabe zur Jahres-, Monats-, Tageszahl und zur Identität.
	/// </summary>
	/// <param name="year">Jahreszahl.</param>
	/// <param name="month">Monatszahl.</param>
	/// <param name="day">Tageszahl.</param>
	/// <param name="id">Identität.</param>
	public CDate(int year, int month, int day, int id) : this(year, month, day){ this.Id = id; }

	// CDate(DateTime)
	/// <summary>
	/// Konstruktor eines gregorianischen Kalenders mit einer Angabe zum DateTime.
	/// </summary>
	/// <param name="item">DateTime.</param>
	public CDate(DateTime item)
	{
		// Felder einrichten
		this.Day   = item.Day;
		this.Month = item.Month;
		this.Year  = item.Year;
	}

	// CDate(DateTime, int)
	/// <summary>
	/// Konstruktor eines gregorianischen Kalenders mit einer Angabe zum DateTime und zur Identität.
	/// </summary>
	/// <param name="item">Datumswert.</param>
	/// <param name="id">Identität.</param>
	public CDate(DateTime item, int id) : this(item){ this.Id = id; }

	// CDate(double)
	/// <summary>
	/// Konstruktor eines gregorianischen Kalenders mit einer Angabe zum julianischen Datum.
	/// </summary>
	/// <param name="jd">Julianisches Datum.</param>
	public CDate(double jd)
	{
		// Felder einrichten
		DateTime itm = MCalendar.ToDateTime(jd);
		this.Day   = itm.Day;
		this.Month = itm.Month;
		this.Year  = itm.Year;
	}

	// CDate(double, int)
	/// <summary>
	/// Konstruktor eines greogrianischen Kalendars mit einer Angabe zum julianischen Datum und zur Identität.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="id">Identiät.</param>
	public CDate(double jd, int id) : this(jd) { this.Id = id; }

	// ------------------- //
	// Felder und Methoden //
	// ------------------- //
	// CDate.Day
	/// <summary>
	/// Liefert oder übernimmt die Tageszahl.
	/// </summary>
	public int Day{ get; set; }

	// CDate.Month
	/// <summary>
	/// Liefert oder übernimmt die Monatszahl.
	/// </summary>
	public int Month{ get;  set; }

	// CDate.ToJdn()
	/// <summary>
	/// Liefert die julianische Tageszahl zum gregorianischen Datum.
	/// </summary>
	/// <returns>Julianische Tageszahl zum gregorianischen Datum.</returns>
	public double ToJdn(){ return MCalendar.FromGregorian(this.Year, this.Month, this.Day); }

	// CDate.ToString()
	/// <summary>
	/// Liefert den Datumswert als Zeichenkette.
	/// </summary>
	/// <returns>Datumswert als Zeichenkette.</returns>
	public override string ToString(){ return string.Format("{0:0000}-{1:00}-{2:00}", this.Year, this.Month, this.Day); }

	// CDate.Year
	/// <summary>
	/// Liefert oder übernimmt die Jahreszahl.
	/// </summary>
	public int Year{ get;  set; }
}
