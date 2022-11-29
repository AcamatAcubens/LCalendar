using Acamat.LMath;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt die Elp2000-Theorie.
/// </summary>
internal partial class CElp
{
	// CElp.Elp23[]
	/// <summary>
	/// Datenvektor für Elp23 (Tidal effects – Latitude).
	/// </summary>
	private TElpB[] Elp23 = new TElpB[]
	{
		new TElpB(0, new int[]{1,1,0,-2}, 192.936630, 0.000040, 0.074000),
		new TElpB(0, new int[]{1,1,0, 0}, 192.936640, 0.000040, 0.075000)
	};

	// CElp.Elp23Size
	/// <summary>
	/// Größe des Datenvektor für Elp23 (Tidal effects – Latitude).
	/// </summary>
	private const int Elp23Size = 2;

	// CElp.SumElp23(double[])
	/// <summary>
	/// Liefert das Ergebnis für Elp23 (Tidal effects – Latitude) zum Jahrhundertbruchteil.
	/// </summary>
	/// <param name="t">Jahrhundertbruchteil.</param>
	/// <returns>Ergebnis für Elp23 (Tidal effects – Latitude) zum Jahrhundertbruchteil.</returns>
	private double SumElp23(double[] t)
	{
		// TODO: CElp.SumElp23(double[]): Implementation vervollständigen.
		throw new NotImplementedException("Methode ist nicht implementiert.");
	}
}
