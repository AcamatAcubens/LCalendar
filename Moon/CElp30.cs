using Acamat.LMath;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt die Elp2000-Theorie.
/// </summary>
internal partial class CElp
{
	// CElp.Elp30[]
	/// <summary>
	/// Datenvektor für Elp30 (Moon figure perturbations – Distance).
	/// </summary>
	private TElpB[] Elp30 = new TElpB[]
	{
		new TElpB(0, new int[]{0, 0, 0, 0},  90.000000, 0.001300, 99999.999000),
		new TElpB(0, new int[]{0, 0, 0, 1}, 213.957200, 0.000030,     0.075000),
		new TElpB(0, new int[]{0, 0, 0, 2}, 270.037450, 0.000020,     0.037000),
		new TElpB(0, new int[]{0, 0, 1, 0},  90.075970, 0.000040,     0.075000),
		new TElpB(0, new int[]{0, 0, 3,-2}, 270.434290, 0.000020,     0.077000),
		new TElpB(0, new int[]{0, 1,-1, 0},  89.999190, 0.000130,     0.082000),
		new TElpB(0, new int[]{0, 1, 0, 0}, 270.000070, 0.000220,     1.000000),
		new TElpB(0, new int[]{0, 1, 1, 0}, 269.999030, 0.000110,     0.070000),
		new TElpB(0, new int[]{2,-1,-1, 0},  89.998150, 0.000020,     0.095000),
		new TElpB(0, new int[]{2,-1, 0, 0},  90.000520, 0.000030,     0.042000),
		new TElpB(0, new int[]{2, 0,-2, 0}, 269.985850, 0.000050,     0.564000),
		new TElpB(0, new int[]{2, 0,-1, 0},  89.998630, 0.000130,     0.087000),
		new TElpB(0, new int[]{2, 1,-1, 0}, 269.999820, 0.000020,     0.080000),
		new TElpB(0, new int[]{2, 1, 0, 0}, 269.999820, 0.000030,     0.039000)
	};

	// CElp.Elp30Size
	/// <summary>
	/// Größe des Datenvektor für Elp30 (Moon figure perturbations – Distance).
	/// </summary>
	private const int Elp30Size = 14;

	// CElp.SumElp30(double[])
	/// <summary>
	/// Liefert das Ergebnis für Elp30 (Moon figure perturbations – Distance) zum Jahrhundertbruchteil.
	/// </summary>
	/// <param name="t">Jahrhundertbruchteil.</param>
	/// <returns>Ergebnis für Elp30 (Moon figure perturbations – Distance) zum Jahrhundertbruchteil.</returns>
	private double SumElp30(double[] t)
	{
		// TODO: CElp.SumElp30(double[]): Implementation vervollständigen.
		throw new NotImplementedException("Methode ist nicht implementiert.");
	}
}
