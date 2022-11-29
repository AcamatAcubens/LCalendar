using Acamat.LMath;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt die Elp2000-Theorie.
/// </summary>
internal partial class CElp
{
	// CElp.Elp32[]
	/// <summary>
	/// Datenvektor für Elp32 (Relativistic perturbations – Latitude).
	/// </summary>
	private TElpB[] Elp32 = new TElpB[]
	{
		new TElpB(0, new int[]{0, 1, 0,-1}, 179.998030, 0.000040, 0.081000),
		new TElpB(0, new int[]{0, 1, 0, 1}, 179.997980, 0.000040, 0.069000),
		new TElpB(0, new int[]{2, 0, 0,-1}, 359.998100, 0.000020, 0.088000),
		new TElpB(0, new int[]{2, 0, 0, 1}, 180.000260, 0.000020, 0.026000)
	};

	// CElp.Elp32Size
	/// <summary>
	/// Größe des Datenvektor für Elp32 (Relativistic perturbations – Latitude).
	/// </summary>
	private const int Elp32Size = 4;

	// CElp.SumElp32(double[])
	/// <summary>
	/// Liefert das Ergebnis für Elp32 (Relativistic perturbations – Latitude) zum Jahrhundertbruchteil.
	/// </summary>
	/// <param name="t">Jahrhundertbruchteil.</param>
	/// <returns>Ergebnis für Elp32 (Relativistic perturbations – Latitude) zum Jahrhundertbruchteil.</returns>
	private double SumElp32(double[] t)
	{
		// TODO: CElp.SumElp32(double[]): Implementation vervollständigen.
		throw new NotImplementedException("Methode ist nicht implementiert.");
	}
}
