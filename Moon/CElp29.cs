using Acamat.LMath;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt die Elp2000-Theorie.
/// </summary>
internal partial class CElp
{
	// CElp.Elp29[]
	/// <summary>
	/// Datenvektor für Elp29 (Moon figure perturbations – Latitude).
	/// </summary>
	private TElpB[] Elp29 = new TElpB[]
	{
		new TElpB(0, new int[]{0, 0, 1,-1},   0.021990, 0.000030, 5.997000),
		new TElpB(0, new int[]{0, 0, 1, 0}, 245.990670, 0.000010, 0.075000),
		new TElpB(0, new int[]{0, 0, 1, 1},   0.005300, 0.000010, 0.037000),
		new TElpB(0, new int[]{0, 0, 2,-3},   0.422830, 0.000020, 0.073000),
		new TElpB(0, new int[]{0, 0, 2,-1},   0.745050, 0.000010, 0.076000),
		new TElpB(0, new int[]{0, 1,-1,-1}, 359.999820, 0.000010, 0.039000),
		new TElpB(0, new int[]{0, 1, 0,-1}, 359.999820, 0.000100, 0.081000),
		new TElpB(0, new int[]{0, 1, 0, 1}, 359.999820, 0.000100, 0.069000),
		new TElpB(0, new int[]{0, 1, 1, 1}, 359.999820, 0.000010, 0.036000),
		new TElpB(0, new int[]{2, 0,-2,-1}, 179.983560, 0.000010, 0.066000),
		new TElpB(0, new int[]{2, 0,-2, 1}, 179.983530, 0.000010, 0.086000),
		new TElpB(0, new int[]{2, 0, 0,-1}, 179.994780, 0.000050, 0.088000)
	};

	// CElp.Elp29Size
	/// <summary>
	/// Größe des Datenvektor für Elp29 (Moon figure perturbations – Latitude).
	/// </summary>
	private const int Elp29Size = 12;

	// CElp.SumElp29(double[])
	/// <summary>
	/// Liefert das Ergebnis für Elp29 (Moon figure perturbations – Latitude) zum Jahrhundertbruchteil.
	/// </summary>
	/// <param name="t">Jahrhundertbruchteil.</param>
	/// <returns>Ergebnis für Elp29 (Moon figure perturbations – Latitude) zum Jahrhundertbruchteil.</returns>
	private double SumElp29(double[] t)
	{
		// TODO: CElp.SumElp29(double[]): Implementation vervollständigen.
		throw new NotImplementedException("Methode ist nicht implementiert.");
	}
}
