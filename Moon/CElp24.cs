using Acamat.LMath;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt die Elp2000-Theorie.
/// </summary>
internal partial class CElp
{
	// CElp.Elp24[]
	/// <summary>
	/// Datenvektor für Elp24 (Tidal effects – Distance).
	/// </summary>
	private TElpB[] Elp24 = new TElpB[]
	{
		new TElpB(0, new int[]{1,1,-1,-1}, 282.936650, 0.000040, 0.075000),
		new TElpB(0, new int[]{1,1, 1,-1}, 102.936650, 0.000040, 0.076000)
	};

	// CElp.Elp24Size
	/// <summary>
	/// Größe des Datenvektor für Elp24 (Tidal effects – Distance).
	/// </summary>
	private const int Elp24Size = 2;

	// CElp.SumElp24(double[])
	/// <summary>
	/// Liefert das Ergebnis für Elp24 (Tidal effects – Distance). zum Jahrhundertbruchteil.
	/// </summary>
	/// <param name="t">Jahrhundertbruchteil.</param>
	/// <returns>Ergebnis für Elp24 (Tidal effects – Distance). zum Jahrhundertbruchteil.</returns>
	private double SumElp24(double[] t)
	{
		// TODO: CElp.SumElp24(double[]): Implementation vervollständigen.
		throw new NotImplementedException("Methode ist nicht implementiert.");
	}
}
