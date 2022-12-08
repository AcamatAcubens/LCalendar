using Acamat.LMath;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt die Elp2000-Theorie.
/// </summary>
internal partial class CElp
{
	// CElp.Elp25[]
	/// <summary>
	/// Datenvektor für Elp25 (Tidal effects – Longitude/t).
	/// </summary>
	private TElpB[] Elp25 = new TElpB[]
	{
		new TElpB(0, new int[]{0,0, 1,0}, 0.000000, 0.000580, 0.075000),
		new TElpB(0, new int[]{0,0, 2,0}, 0.000000, 0.000040, 0.038000),
		new TElpB(0, new int[]{2,0,-2,0}, 0.000000, 0.000020, 0.564000),
		new TElpB(0, new int[]{2,0,-1,0}, 0.000000, 0.000210, 0.087000),
		new TElpB(0, new int[]{2,0, 0,0}, 0.000000, 0.000090, 0.040000),
		new TElpB(0, new int[]{2,0, 1,0}, 0.000000, 0.000010, 0.026000)
	};

	// CElp.Elp25Size
	/// <summary>
	/// Größe des Datenvektor für Elp25 (Tidal effects – Longitude/t).
	/// </summary>
	private const int Elp25Size = 6;

	// CElp.SumElp25(double[])
	/// <summary>
	/// Liefert das Ergebnis für Elp25 (Tidal effects – Longitude/t) zum Jahrhundertbruchteil.
	/// </summary>
	/// <param name="t">Jahrhundertbruchteil.</param>
	/// <returns>Ergebnis für Elp25 (Tidal effects – Longitude/t) zum Jahrhundertbruchteil.</returns>
	private double SumElp25(double[] t)
	{
		// TODO: CElp.SumElp25(double[]): Implementation vervollständigen.
		throw new NotImplementedException("Methode ist nicht implementiert.");
	}
}
