using Acamat.LMath;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt die Elp2000-Theorie.
/// </summary>
internal partial class CElp
{
	// CElp.Elp26[]
	/// <summary>
	/// Datenvektor für Elp26 (Tidal Effects – Latitude/t).
	/// </summary>
	private TElpB[] Elp26 = new TElpB[]
	{
		new TElpB(0, new int[]{0,0,0, 1}, 180.000000, 0.000050, 0.075000),
		new TElpB(0, new int[]{0,0,1,-1},   0.000000, 0.000030, 5.997000),
		new TElpB(0, new int[]{0,0,1, 1},   0.000000, 0.000030, 0.037000),
		new TElpB(0, new int[]{2,0,0,-1},   0.000000, 0.000010, 0.088000)
	};

	// CElp.m_Elp26Size
	/// <summary>
	/// Größe des Datenvektor für Elp26 (Tidal Effects – Latitude/t).
	/// </summary>
	private const int Elp26Size = 4;

	// CElp.m_SumElp26(double[])
	/// <summary>
	/// Liefert das Ergebnis für Elp26 (Tidal Effects – Latitude/t). zum Jahrhundertbruchteil.
	/// </summary>
	/// <param name="t">Jahrhundertbruchteil.</param>
	/// <returns>Ergebnis für Elp26 (Tidal Effects – Latitude/t) zum Jahrhundertbruchteil.</returns>
	private double SumElp26(double[] t)
	{
		// TODO: CElp.SumElp26(double[]): Implementation vervollständigen.
		throw new NotImplementedException("Methode ist nicht implementiert.");
	}
}
