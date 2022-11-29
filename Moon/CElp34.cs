using Acamat.LMath;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt die Elp2000-Theorie.
/// </summary>
internal partial class CElp
{
	// CElp.Elp34[]
	/// <summary>
	/// Datenvektor für Elp34 (Planetary perturbations – Solar eccentricity – Longitude/t^2).
	/// </summary>
	private TElpB[] Elp34 = new TElpB[]
	{
		new TElpB(0, new int[]{0, 1,-2, 0},   0.000000, 0.000070, 0.039000),
		new TElpB(0, new int[]{0, 1,-1, 0},   0.000000, 0.001080, 0.082000),
		new TElpB(0, new int[]{0, 1, 0, 0},   0.000000, 0.004870, 1.000000),
		new TElpB(0, new int[]{0, 1, 1, 0},   0.000000, 0.000800, 0.070000),
		new TElpB(0, new int[]{0, 1, 2, 0},   0.000000, 0.000060, 0.036000),
		new TElpB(0, new int[]{0, 2,-1, 0},   0.000000, 0.000040, 0.089000),
		new TElpB(0, new int[]{0, 2, 0, 0},   0.000000, 0.000110, 0.500000),
		new TElpB(0, new int[]{0, 2, 1, 0},   0.000000, 0.000020, 0.066000),
		new TElpB(0, new int[]{1, 1, 0, 0}, 180.000000, 0.000130, 0.075000),
		new TElpB(0, new int[]{2,-2,-1, 0}, 180.000000, 0.000110, 0.105000),
		new TElpB(0, new int[]{2,-2, 0, 0}, 180.000000, 0.000120, 0.044000),
		new TElpB(0, new int[]{2,-2, 1, 0}, 180.000000, 0.000010, 0.028000),
		new TElpB(0, new int[]{2,-1,-2, 0}, 180.000000, 0.000060, 0.360000),
		new TElpB(0, new int[]{2,-1,-1, 0}, 180.000000, 0.001500, 0.095000),
		new TElpB(0, new int[]{2,-1, 0,-2}, 180.000000, 0.000020, 0.322000),
		new TElpB(0, new int[]{2,-1, 0, 0}, 180.000000, 0.001200, 0.042000),
		new TElpB(0, new int[]{2,-1, 1, 0}, 180.000000, 0.000110, 0.027000),
		new TElpB(0, new int[]{2, 0,-1, 0},   0.000000, 0.000020, 0.087000),
		new TElpB(0, new int[]{2, 0, 0, 0},   0.000000, 0.000030, 0.040000),
		new TElpB(0, new int[]{2, 1,-2, 0}, 180.000000, 0.000020, 1.292000),
		new TElpB(0, new int[]{2, 1,-1, 0},   0.000000, 0.000210, 0.080000),
		new TElpB(0, new int[]{2, 1, 0,-2},   0.000000, 0.000010, 0.903000),
		new TElpB(0, new int[]{2, 1, 0, 0},   0.000000, 0.000180, 0.039000),
		new TElpB(0, new int[]{2, 1, 1, 0},   0.000000, 0.000020, 0.026000),
		new TElpB(0, new int[]{2, 2,-1, 0},   0.000000, 0.000040, 0.074000),
		new TElpB(0, new int[]{4,-1,-2, 0}, 180.000000, 0.000020, 0.046000),
		new TElpB(0, new int[]{4,-1,-1, 0}, 180.000000, 0.000030, 0.028000),
		new TElpB(0, new int[]{4,-1, 0, 0}, 180.000000, 0.000010, 0.021000)
	};

	// CElp.Elp34Size
	/// <summary>
	/// Größe des Datenvektor für Elp34 (Planetary perturbations – Solar eccentricity – Longitude/t^2).
	/// </summary>
	private const int Elp34Size = 28;

	// CElp.SumElp34(double[])
	/// <summary>
	/// Liefert das Ergebnis für Elp34 (Planetary perturbations – Solar eccentricity – Longitude/t^2) zum Jahrhundertbruchteil.
	/// </summary>
	/// <param name="t">Jahrhundertbruchteil.</param>
	/// <returns>Ergebnis für Elp34 (Planetary perturbations – Solar eccentricity – Longitude/t^2) zum Jahrhundertbruchteil.</returns>
	private double SumElp34(double[] t)
	{
		// TODO: CElp.SumElp34(double[]): Implementation vervollständigen.
		throw new NotImplementedException("Methode ist nicht implementiert.");
	}
}
