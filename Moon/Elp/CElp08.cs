using Acamat.LMath;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt die ELP2000-Theorie.
/// </summary>
internal partial class CElp
{
	// CElp.Elp08[]
	/// <summary>
	/// Datenvektor für Elp08 (Earth perturbations – Latitude/t).
	/// </summary>
	private TElpB[] Elp08 = new TElpB[]
	{
		new TElpB(1, new int[]{-2,0, 0, 0},180.00000,0.00012,0.088),
		new TElpB(1, new int[]{-2,0, 1, 0},180.00000,0.00003,0.530),
		new TElpB(1, new int[]{ 0,0,-1,-2},180.00000,0.00001,0.037),
		new TElpB(1, new int[]{ 0,0,-1, 0},180.00000,0.00019,8.847),
		new TElpB(1, new int[]{ 0,0, 0,-2},180.00000,0.00014,0.074),
		new TElpB(1, new int[]{ 0,0, 0, 0},  0.00000,0.00342,0.075),
		new TElpB(1, new int[]{ 0,0, 1, 0},  0.00000,0.00018,0.038),
		new TElpB(1, new int[]{ 0,0, 2, 0},  0.00000,0.00001,0.025),
		new TElpB(1, new int[]{ 2,0,-1, 0},  0.00000,0.00004,0.040),
		new TElpB(1, new int[]{ 2,0, 0, 0},  0.00000,0.00002,0.026),
		new TElpB(2, new int[]{ 0,0, 0,-1},180.00000,0.00009,0.075)
	};

	// CElp.Elp08Size
	/// <summary>
	/// Größe des Datenvektor für Elp08 (Earth perturbations – Latitude/t).
	/// </summary>
	private const int Elp08Size = 11;

	// CElp.SumElp08(double[])
	/// <summary>
	/// Liefert das Ergebnis für Elp08 (Earth perturbations – Latitude/t) zum Jahrhundertbruchteil.
	/// </summary>
	/// <param name="t">Jahrhundertbruchteil.</param>
	/// <returns>Ergebnis für Elp08 (Earth perturbations – Latitude/t) zum Jahrhundertbruchteil.</returns>
	private double SumElp08(double[] t)
	{
		// TODO: CElp.SumElp08(double[]): Implementation vervollständigen.
		throw new NotImplementedException("Methode ist nicht implementiert.");
	}
}
