using Acamat.LMath;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt die Elp2000-Theorie.
/// </summary>
internal partial class CElp
{
	// CElp.Elp35[]
	/// <summary>
	/// Datenvektor für Elp35 (Planetary perturbations – Solar eccentricity – Latitude/t^2).
	/// </summary>
	private TElpB[] Elp35 = new TElpB[]
	{
		new TElpB(0, new int[]{0, 1,-1,-1},   0.000000, 0.000050, 0.039000),
		new TElpB(0, new int[]{0, 1,-1, 1},   0.000000, 0.000040, 0.857000),
		new TElpB(0, new int[]{0, 1, 0,-1},   0.000000, 0.000040, 0.081000),
		new TElpB(0, new int[]{0, 1, 0, 1},   0.000000, 0.000050, 0.069000),
		new TElpB(0, new int[]{0, 1, 1,-1},   0.000000, 0.000040, 1.200000),
		new TElpB(0, new int[]{0, 1, 1, 1},   0.000000, 0.000040, 0.036000),
		new TElpB(0, new int[]{2,-2, 0,-1}, 180.000000, 0.000020, 0.107000),
		new TElpB(0, new int[]{2,-1,-1,-1}, 180.000000, 0.000050, 0.340000),
		new TElpB(0, new int[]{2,-1,-1, 1}, 180.000000, 0.000060, 0.042000),
		new TElpB(0, new int[]{2,-1, 0,-1}, 180.000000, 0.000220, 0.097000),
		new TElpB(0, new int[]{2,-1, 0, 1}, 180.000000, 0.000060, 0.027000),
		new TElpB(0, new int[]{2,-1, 1,-1}, 180.000000, 0.000010, 0.042000),
		new TElpB(0, new int[]{2, 1, 0,-1},   0.000000, 0.000090, 0.081000)
	};

	// CElp.Elp35Size
	/// <summary>
	/// Größe des Datenvektor für Elp35 (Planetary perturbations – Solar eccentricity – Latitude/t^2).
	/// </summary>
	private const int Elp35Size = 13;

	// CElp.SumElp35(double[])
	/// <summary>
	/// Liefert das Ergebnis für Elp35 (Planetary perturbations – Solar eccentricity – Latitude/t^2) zum Jahrhundertbruchteil.
	/// </summary>
	/// <param name="t">Jahrhundertbruchteil.</param>
	/// <returns>Ergebnis für Elp35 (Planetary perturbations – Solar eccentricity – Latitude/t^2) zum Jahrhundertbruchteil.</returns>
	private double SumElp35(double[] t)
	{
		// TODO: CElp.SumElp35(double[]): Implementation vervollständigen.
		throw new NotImplementedException("Methode ist nicht implementiert.");
	}
}
