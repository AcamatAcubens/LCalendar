using Acamat.LMath;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt die Elp2000-Theorie.
/// </summary>
internal partial class CElp
{
	// CElp.Elp22[]
	/// <summary>
	/// Datenvektor für Elp22 (Tidal effects – Longitude).
	/// </summary>
	private TElpB[] Elp22 = new TElpB[]
	{
		new TElpB(0, new int[]{1,1,-1,-1}, 192.936650, 0.000040,  0.075000),
		new TElpB(0, new int[]{1,1, 0,-1}, 192.936650, 0.000820, 18.600000),
		new TElpB(0, new int[]{1,1, 1,-1}, 192.936650, 0.000040,  0.076000)
	};

	// CElp.Elp22Size
	/// <summary>
	/// Größe des Datenvektor für Elp22 (Tidal effects – Longitude).
	/// </summary>
	private const int Elp22Size = 3;

	// CElp.SumElp22(double[])
	/// <summary>
	/// Liefert das Ergebnis für Elp22 (Tidal effects – Longitude). zum Jahrhundertbruchteil.
	/// </summary>
	/// <param name="t">Jahrhundertbruchteil.</param>
	/// <returns>Ergebnis für Elp22 (Tidal effects – Longitude). zum Jahrhundertbruchteil.</returns>
	private double SumElp22(double[] t)
	{
		// TODO: CElp.SumElp22(double[]): Implementation vervollständigen.
		throw new NotImplementedException("Methode ist nicht implementiert.");
	}
}
