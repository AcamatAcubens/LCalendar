using Acamat.LMath;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt die ELP2000-Theorie.
/// </summary>
internal partial class CElp
{
	// CElp.Elp09[]
	/// <summary>
	/// Datenvektor für Elp09 (Earth perturbations – Distance/t).
	/// </summary>
	private TElpB[] Elp09 = new TElpB[]
	{
		new TElpB(0, new int[]{ 0, 0, 0, 0},270.00000,0.00004,99999.999),
		new TElpB(1, new int[]{-2, 0, 0,-1},270.00000,0.00004,    0.040),
		new TElpB(1, new int[]{-2, 0, 1,-1},270.00000,0.00002,    0.087),
		new TElpB(1, new int[]{ 0, 0,-1,-1},270.00000,0.00019,    0.075),
		new TElpB(1, new int[]{ 0, 0,-1, 1}, 90.00000,0.00003,    0.074),
		new TElpB(1, new int[]{ 0, 0, 1,-1}, 90.00000,0.00019,    0.076),
		new TElpB(1, new int[]{ 2, 0,-1,-1}, 90.00000,0.00002,    0.088),
		new TElpB(1, new int[]{ 2, 0, 0,-1}, 90.00000,0.00004,    0.041)
	};

	// CElp.Elp09Size
	/// <summary>
	/// Größe des Datenvektor für Elp09 (Earth perturbations – Distance/t).
	/// </summary>
	private const int Elp09Size = 8;

	// CElp.SumElp09(double[])
	/// <summary>
	/// Liefert das Ergebnis für Elp09 (Earth perturbations – Distance/t) zum Jahrhundertbruchteil.
	/// </summary>
	/// <param name="t">Jahrhundertbruchteil.</param>
	/// <returns>Ergebnis für Elp09 (Earth perturbations – Distance/t) zum Jahrhundertbruchteil.</returns>
	private double SumElp09(double[] t)
	{
		// TODO: CElp.SumElp09(double[]): Implementation vervollständigen.
		throw new NotImplementedException("Methode ist nicht implementiert.");
	}
}
