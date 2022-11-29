using Acamat.LMath;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt die Elp2000-Theorie.
/// </summary>
internal partial class CElp
{
	// CElp.Elp31[]
	/// <summary>
	/// Datenvektor für Elp31 (Relativistic perturbations – Longitude).
	/// </summary>
	private TElpB[] Elp31 = new TElpB[]
	{
		new TElpB(0, new int[]{0, 1,-1, 0}, 179.934730, 0.000060, 0.082000),
		new TElpB(0, new int[]{0, 1, 0, 0}, 179.985320, 0.000810, 1.000000),
		new TElpB(0, new int[]{0, 1, 1, 0}, 179.963230, 0.000050, 0.070000),
		new TElpB(0, new int[]{1, 0, 0, 0},   0.000010, 0.000130, 0.081000),
		new TElpB(0, new int[]{1, 1, 0, 0}, 180.022820, 0.000010, 0.075000),
		new TElpB(0, new int[]{2,-1,-1, 0},   0.022640, 0.000020, 0.095000),
		new TElpB(0, new int[]{2, 0,-1, 0}, 359.988260, 0.000020, 0.087000),
		new TElpB(0, new int[]{2, 0, 0, 0}, 180.000190, 0.000550, 0.040000),
		new TElpB(0, new int[]{2, 0, 1, 0}, 180.000170, 0.000060, 0.026000),
		new TElpB(0, new int[]{2, 1,-1, 0}, 180.749540, 0.000010, 0.080000),
		new TElpB(0, new int[]{4, 0,-1, 0}, 180.000350, 0.000010, 0.028000)
	};

	// CElp.Elp31Size
	/// <summary>
	/// Größe des Datenvektor für Elp31 (Relativistic perturbations – Longitude).
	/// </summary>
	private const int Elp31Size = 11;

	// CElp.SumElp31(double[])
	/// <summary>
	/// Liefert das Ergebnis für Elp31 (Relativistic perturbations – Longitude) zum Jahrhundertbruchteil.
	/// </summary>
	/// <param name="t">Jahrhundertbruchteil.</param>
	/// <returns>Ergebnis für Elp31 (Relativistic perturbations – Longitude) zum Jahrhundertbruchteil.</returns>
	private double SumElp31(double[] t)
	{
		// TODO: CElp.SumElp31(double[]): Implementation vervollständigen.
		throw new NotImplementedException("Methode ist nicht implementiert.");
	}
}
