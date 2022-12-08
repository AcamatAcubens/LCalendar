using Acamat.LMath;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt die Elp2000-Theorie.
/// </summary>
internal partial class CElp
{
	// CElp.Elp33[]
	/// <summary>
	/// Datenvektor für Elp33 (Relativistic perturbations – Distance).
	/// </summary>
	private TElpB[] Elp33 = new TElpB[]
	{
		new TElpB(0, new int []{0, 0, 0, 0}, 270.000000, 0.008280, 99999.999000),
		new TElpB(0, new int []{0, 0, 1, 0},  89.999940, 0.000430,     0.075000),
		new TElpB(0, new int []{0, 1,-1, 0}, 269.932920, 0.000050,     0.082000),
		new TElpB(0, new int []{0, 1, 0, 0}, 270.009080, 0.000090,     1.000000),
		new TElpB(0, new int []{0, 1, 1, 0},  89.957650, 0.000050,     0.070000),
		new TElpB(0, new int []{1, 0, 0, 0}, 270.000020, 0.000060,     0.081000),
		new TElpB(0, new int []{2,-1, 0, 0},  89.970710, 0.000020,     0.042000),
		new TElpB(0, new int []{2, 0,-1, 0}, 269.993670, 0.000030,     0.087000),
		new TElpB(0, new int []{2, 0, 0, 0},  90.000140, 0.001060,     0.040000),
		new TElpB(0, new int []{2, 0, 1, 0},  90.000100, 0.000080,     0.026000)
	};

	// CElp.Elp33Size
	/// <summary>
	/// Größe des Datenvektor für Elp33 (Relativistic perturbations – Distance).
	/// </summary>
	private const int Elp33Size = 10;

	// CElp.SumElp33(double[])
	/// <summary>
	/// Liefert das Ergebnis für Elp33 (Relativistic perturbations – Distance) zum Jahrhundertbruchteil.
	/// </summary>
	/// <param name="t">Jahrhundertbruchteil.</param>
	/// <returns>Ergebnis für Elp33 (Relativistic perturbations – Distance) zum Jahrhundertbruchteil.</returns>
	private double SumElp33(double[] t)
	{
		// TODO: CElp.SumElp33(double[]): Implementation vervollständigen.
		throw new NotImplementedException("Methode ist nicht implementiert.");
	}
}
