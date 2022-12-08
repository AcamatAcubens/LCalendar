using Acamat.LMath;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt die Elp2000-Theorie.
/// </summary>
internal partial class CElp
{
	// CElp.Elp27[]
	/// <summary>
	/// Datenvektor für Elp27 (Tidal effects – Distance/t).
	/// </summary>
	private TElpB[] Elp27 = new TElpB[]
	{
		new TElpB(0, new int[]{0,0, 0,0},  90.000000, 0.003560, 99999.999000),
		new TElpB(0, new int[]{0,0, 1,0}, 270.000000, 0.000720,     0.075000),
		new TElpB(0, new int[]{0,0, 2,0}, 270.000000, 0.000030,     0.038000),
		new TElpB(0, new int[]{2,0,-1,0}, 270.000000, 0.000190,     0.087000),
		new TElpB(0, new int[]{2,0, 0,0}, 270.000000, 0.000130,     0.040000)
	};

	// CElp.Elp27Size
	/// <summary>
	/// Größe des Datenvektor für Elp27 (Tidal effects – Distance/t).
	/// </summary>
	private const int Elp27Size = 5;

	// CElp.SumElp27(double[])
	/// <summary>
	/// Liefert das Ergebnis für Elp27 (Tidal effects – Distance/t) zum Jahrhundertbruchteil.
	/// </summary>
	/// <param name="t">Jahrhundertbruchteil.</param>
	/// <returns>Ergebnis für Elp27 (Tidal effects – Distance/t) zum Jahrhundertbruchteil.</returns>
	private double SumElp27(double[] t)
	{
		// TODO: CElp.SumElp27(double[]): Implementation vervollständigen.
		throw new NotImplementedException("Methode ist nicht implementiert.");
	}
}
