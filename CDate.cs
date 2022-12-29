using Acamat.LCore;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Stellt ein generisches Kalenderdatum dar.
/// </summary>
public class CDate
{
	// ------------- //
	// Konstruktoren //
	// ------------- //
	/// <summary>
	/// Leerkonstruktor.
	/// </summary>
	public CDate() : this(0, 0, 0) { }

	/// <summary>
	/// Kopierkonstruktor.
	/// </summary>
	/// <param name="item">Datum</param>
	public CDate(CDate item) : this(item.Year, item.Month, item.Day) { }

	/// <summary>
	/// Konstruktor mit einer Angabe zum DateOnly.
	/// </summary>
	/// <param name="item">DateOnly</param>
	public CDate(DateOnly item) : this(item.Year, item.Month, item.Day) { }

	/// <summary>
	/// Konstruktor mit einer Angabe zum DateTime.
	/// </summary>
	/// <param name="item">DateTime</param>
	public CDate(DateTime item) : this(item.Year, item.Month, item.Day) { }

	/// <summary>
	/// Standardkonstruktor mit einer Angabe zur Jahres-, Monats- und Tageszahl.
	/// </summary>
	/// <param name="year">Jahreszahl</param>
	/// <param name="month">Monatszahl</param>
	/// <param name="day">Tageszahl</param>
	public CDate(int year, int month, int day)
	{
		// Felder einrichten
		this.Day   = day;
		this.Month = month;
		this.Year  = year;
	}

	// ------ //
	// Object //
	// ------ //
	/// <summary>
	/// Liefert die Textrepräsentation zum Datumswert.
	/// </summary>
	public override string ToString()
		=> string.Format("{0:0000}-{1:00}-{2:00}", this.Year, this.Month, this.Day);

	// ------------------- //
	// Felder und Methoden //
	// ------------------- //
	/// <summary>
	/// Liefert oder übernimmt die Tageszahl.
	/// </summary>
	public int Day{ get; set; }

	/// <summary>
	/// Liefert oder übernimmt die Monatszahl.
	/// </summary>
	public int Month{ get; set; }

	/// <summary>
	/// Liefert das aktuelle Tagesdatum.
	/// </summary>
	public static CDate Today()
		=> new CDate(DateTime.Today);

	/// <summary>
	/// Liefert die julianische Tageszahl zum gregorianischen Datum
	/// </summary>
	public double ToJdn()
		=> MCalendar.FromGregorian(this.Year, this.Month, this.Day);

	/// <summary>
	/// Liefert oder übernimmt die Jahreszahl.
	/// </summary>
	public int Year{ get; set; }
}
