using Acamat.LMath;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt die ELP2000-Theorie.
/// </summary>
internal partial class CElp
{
	// CElp.Elp07[]
	/// <summary>
	/// Datenvektor für Elp07 (Earth perturbations – Longitude/t).
	/// </summary>
	private TElpB[] Elp07 = new TElpB[]
	{
		new TElpB(1, new int[]{-2,0, 0,-1},180.00000,0.00003, 0.040),
		new TElpB(1, new int[]{-2,0, 0, 1},180.00000,0.00002, 0.487),
		new TElpB(1, new int[]{-2,0, 1,-1},180.00000,0.00002, 0.087),
		new TElpB(1, new int[]{ 0,0,-2,-1},180.00000,0.00001, 0.038),
		new TElpB(1, new int[]{ 0,0,-1,-1},180.00000,0.00021, 0.075),
		new TElpB(1, new int[]{ 0,0,-1, 1},180.00000,0.00002, 0.074),
		new TElpB(1, new int[]{ 0,0, 0,-1},180.00000,0.00300,18.613),
		new TElpB(1, new int[]{ 0,0, 0, 1},180.00000,0.00015, 0.037),
		new TElpB(1, new int[]{ 0,0, 1,-1},180.00000,0.00021, 0.076),
		new TElpB(1, new int[]{ 0,0, 1, 1},180.00000,0.00002, 0.025),
		new TElpB(1, new int[]{ 0,0, 2,-1},180.00000,0.00002, 0.038),
		new TElpB(1, new int[]{ 2,0,-1,-1},180.00000,0.00003, 0.088),
		new TElpB(1, new int[]{ 2,0, 0,-1},180.00000,0.00004, 0.041),
		new TElpB(2, new int[]{ 0,0, 0,-2},  0.00000,0.00004, 9.307)
	};

	// CElp.Elp07Size
	/// <summary>
	/// Größe des Datenvektor für Elp07 (Earth perturbations – Longitude/t).
	/// </summary>
	private const int Elp07Size = 14;

	// CElp.SumElp07(double[])
	/// <summary>
	/// Liefert das Ergebnis für Elp07 (Earth perturbations – Longitude/t) zum Jahrhundertbruchteil.
	/// </summary>
	/// <param name="t">Jahrhundertbruchteil.</param>
	/// <returns>Ergebnis für Elp07 (Earth perturbations – Longitude/t) zum Jahrhundertbruchteil.</returns>
	private double SumElp07(double[] t)
	{
		// TODO: CElp.SumElp07(double[]): Implementation vervollständigen.
		throw new NotImplementedException("Methode ist nicht implementiert.");
	}
}
