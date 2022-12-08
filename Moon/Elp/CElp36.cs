using Acamat.LMath;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt die Elp2000-Theorie.
/// </summary>
internal partial class CElp
{
	// CElp.Elp36[]
	/// <summary>
	/// Datenvektor für Elp36 (Planetary perturbations – Solar eccentricity – Distance/t^2).
	/// </summary>
	private TElpB[] Elp36 = new TElpB[]
	{
		new TElpB(0, new int[]{0, 1,-2, 0},  90.000000, 0.000050, 0.039000),
		new TElpB(0, new int[]{0, 1,-1, 0},  90.000000, 0.000950, 0.082000),
		new TElpB(0, new int[]{0, 1, 0, 0}, 270.000000, 0.000360, 1.000000),
		new TElpB(0, new int[]{0, 1, 1, 0}, 270.000000, 0.000770, 0.070000),
		new TElpB(0, new int[]{0, 1, 2, 0}, 270.000000, 0.000040, 0.036000),
		new TElpB(0, new int[]{0, 2,-1, 0},  90.000000, 0.000030, 0.089000),
		new TElpB(0, new int[]{1, 1, 0, 0},  90.000000, 0.000120, 0.075000),
		new TElpB(0, new int[]{2,-2,-1, 0},  90.000000, 0.000070, 0.105000),
		new TElpB(0, new int[]{2,-2, 0, 0},  90.000000, 0.000140, 0.044000),
		new TElpB(0, new int[]{2,-1,-2, 0}, 270.000000, 0.000070, 0.360000),
		new TElpB(0, new int[]{2,-1,-1, 0},  90.000000, 0.001110, 0.095000),
		new TElpB(0, new int[]{2,-1, 0, 0},  90.000000, 0.001490, 0.042000),
		new TElpB(0, new int[]{2,-1, 1, 0},  90.000000, 0.000090, 0.027000),
		new TElpB(0, new int[]{2, 0, 0, 0}, 270.000000, 0.000040, 0.040000),
		new TElpB(0, new int[]{2, 1,-1, 0}, 270.000000, 0.000180, 0.080000),
		new TElpB(0, new int[]{2, 1, 0, 0}, 270.000000, 0.000230, 0.039000),
		new TElpB(0, new int[]{2, 1, 1, 0}, 270.000000, 0.000020, 0.026000),
		new TElpB(0, new int[]{2, 2,-1, 0}, 270.000000, 0.000030, 0.074000),
		new TElpB(0, new int[]{4,-1,-1, 0},  90.000000, 0.000030, 0.028000)
	};

	// CElp.Elp36Size
	/// <summary>
	/// Größe des Datenvektor für Elp36 (Planetary perturbations - solar eccentricity – Distance/t^2).
	/// </summary>
	private const int Elp36Size = 19;

	// CElp.SumElp36(double[])
	/// <summary>
	/// Liefert das Ergebnis für Elp36 (Planetary perturbations - solar eccentricity – Distance/t^2) zum Jahrhundertbruchteil.
	/// </summary>
	/// <param name="t">Jahrhundertbruchteil.</param>
	/// <returns>Ergebnis für Elp36 (Planetary perturbations - solar eccentricity – Distance/t^2) zum Jahrhundertbruchteil.</returns>
	private double SumElp36(double[] t)
	{
		// TODO: CElp.SumElp36(double[]): Implementation vervollständigen.
		throw new NotImplementedException("Methode ist nicht implementiert.");
	}
}
