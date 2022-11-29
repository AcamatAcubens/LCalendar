using Acamat.LMath;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt die Elp2000-Theorie.
/// </summary>
internal partial class CElp
{
	// CElp.Elp28[]
	/// <summary>
	/// Datenvektor für Elp28 (Moon figure perturbations – Longitude)
	/// </summary>
	private TElpB[] Elp28 = new TElpB[]
	{
		new TElpB(0, new int[]{0, 0, 0, 1}, 303.961850, 0.000040, 0.075000),
		new TElpB(0, new int[]{0, 0, 1,-1}, 259.883930, 0.000160, 5.997000),
		new TElpB(0, new int[]{0, 0, 2,-2},   0.430200, 0.000400, 2.998000),
		new TElpB(0, new int[]{0, 0, 3,-2},   0.433790, 0.000020, 0.077000),
		new TElpB(0, new int[]{0, 1,-1, 0}, 359.998580, 0.000140, 0.082000),
		new TElpB(0, new int[]{0, 1, 0, 0}, 359.999820, 0.002230, 1.000000),
		new TElpB(0, new int[]{0, 1, 1, 0}, 359.999610, 0.000140, 0.070000),
		new TElpB(0, new int[]{1, 0,-1, 0}, 359.993310, 0.000090, 1.127000),
		new TElpB(0, new int[]{1, 0, 0, 0}, 359.995370, 0.000010, 0.081000),
		new TElpB(0, new int[]{1, 1,-1, 0},   0.064180, 0.000030, 8.850000),
		new TElpB(0, new int[]{2,-1,-1, 0}, 180.000950, 0.000040, 0.095000),
		new TElpB(0, new int[]{2,-1, 0, 0}, 180.000140, 0.000030, 0.042000),
		new TElpB(0, new int[]{2, 0,-3, 0}, 179.981260, 0.000010, 0.067000),
		new TElpB(0, new int[]{2, 0,-2, 0}, 179.983660, 0.000250, 0.564000),
		new TElpB(0, new int[]{2, 0,-1, 0}, 179.996380, 0.000140, 0.087000),
		new TElpB(0, new int[]{2, 0, 0,-2}, 179.958640, 0.000030, 0.474000),
		new TElpB(0, new int[]{2, 0, 0, 0}, 179.999040, 0.000020, 0.040000),
		new TElpB(0, new int[]{2, 1,-2, 0}, 179.991840, 0.000020, 1.292000),
		new TElpB(0, new int[]{2, 1,-1, 0},   0.003130, 0.000020, 0.080000),
		new TElpB(0, new int[]{2, 1, 0, 0}, 359.999650, 0.000020, 0.039000)
	};

	// CElp.Elp28Size
	/// <summary>
	/// Größe des Datenvektor für Elp28 (Moon figure perturbations – Longitude).
	/// </summary>
	private const int Elp28Size = 20;

	// CElp.SumElp28(double[])
	/// <summary>
	/// Liefert das Ergebnis für Elp28 (Moon figure perturbations – Longitude) zum Jahrhundertbruchteil.
	/// </summary>
	/// <param name="t">Jahrhundertbruchteil.</param>
	/// <returns>Ergebnis für Elp28 (Moon figure perturbations – Longitude) zum Jahrhundertbruchteil.</returns>
	private double SumElp28(double[] t)
	{
		// TODO: CElp.SumElp28(double[]): Implementation vervollständigen.
		throw new NotImplementedException("Methode ist nicht implementiert.");
	}
}
