// https://rdrr.io/cran/astrolibR/man/moonpos.html
using Acamat.LMath;
using Acamat.LMath.Geometry;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt die Elp2000-Theorie.
/// </summary>
internal partial class CElp
{
	// TElpA
	/// <summary>
	/// Struktur für Elp01 - Elp03 (Main Problem).
	/// </summary>
	private struct TElpA
	{
		// ------------- //
		// Konstruktoren //
		// ------------- //
		// TElpA(int[], double, double[])
		/// <summary>
		/// Konstruktur.
		/// </summary>
		/// <param name="i">Werte für Koeffizientenvektor I.</param>
		/// <param name="a">Wert für Koeffizient A.</param>
		/// <param name="b">Werte für Koeffizientenvektor B.</param>
		public TElpA(int[] i, double a, double[] b){I = i; A = a; B = b;}

		// ------ //
		// Felder //
		// ------ //
		// TElpA.I[]
		/// <summary>
		/// Koeffizientenvektor I.
		/// </summary>
		public int[]    I;

		// TElpA.A
		/// <summary>
		/// Koeffizient A.
		/// </summary>
		public double   A;

		// TElpA.B
		/// <summary>
		/// Koeffizientenvektor B.
		/// </summary>
		public double[] B;
	}

	// TElpB
	/// <summary>
	/// Struktur für Elp04 - Elp09 (Earth perturbance).
	///              Elp22 - Elp36 (Tidal effekt, …).
	/// </summary>
	private struct TElpB
	{
		// ------------- //
		// Konstruktoren //
		// ------------- //
		// TElpB(int, int[], double, double, double)
		/// <summary>
		/// Konstruktur.
		/// </summary>
		/// <param name="z">Wert für Koeffizient Z.</param>
		/// <param name="i">Werte für Koeffizientenvektor I.</param>
		/// <param name="o">Wert für Koeffizient O.</param>
		/// <param name="a">Wert für Koeffizient A.</param>
		/// <param name="p">Wert für Koeffizient P.</param>
		public TElpB(int z, int[] i, double o, double a, double p){Z = z; I = i; O = o; A = a; P = p;}

		// ------ //
		// Felder //
		// ------ //
		// TElpB.Z
		/// <summary>
		/// Koeffizient Z.
		/// </summary>
		public int    Z;

		// TElpB.I[]
		/// <summary>
		/// Koeffizientenvektor I.
		/// </summary>
		public int[]  I;

		// TElpB.O
		/// <summary>
		/// Koeffizient O.
		/// </summary>
		public double O;

		// TElpB.A
		/// <summary>
		/// Koeffizient A.
		/// </summary>
		public double A;

		// TElpB.P
		/// <summary>
		/// Koeffizient P.
		/// </summary>
		public double P;
	}

	// TElpC
	/// <summary>
	/// Struktur für Elp10 - Elp21 (Planetary perturbations).
	/// </summary>
	private struct TElpC
	{
		// ------------- //
		// Konstruktoren //
		// ------------- //
		// TElpC(int[], double, double, double)
		/// <summary>
		/// Konstruktur.
		/// </summary>
		/// <param name="i">Werte für Koeffizientenvektor I.</param>
		/// <param name="t">Wert für Koeffizient T.</param>
		/// <param name="o">Wert für Koeffizient O.</param>
		/// <param name="p">Wert für Koeffizient P.</param>
		public TElpC(int[] i, double t, double o, double p){I = i; T = t; O = o; P = p;}

		// ------ //
		// Felder //
		// ------ //
		// TElpC.I[]
		/// <summary>
		/// Koeffizientenvektor I.
		/// </summary>
		public int[]  I;

		// TElpC.T
		/// <summary>
		/// Koeffizient T.
		/// </summary>
		public double T;

		// TElpC.O
		/// <summary>
		/// Koeffizient O.
		/// </summary>
		public double O;

		// TElpC.P
		/// <summary>
		/// Koeffizient P.
		/// </summary>
		public double P;
	}

	// ------------------- //
	// Felder und Methoden //
	// ------------------- //
	// CElp.Position(double)
	/// <summary>
	/// Liefert die geozentrische Position des Mondes zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Geozentrische Position des Mondes zur julianischen Tageszahl.</returns>
	public CVector Position(double jd)
	{
					// Lokale Felder einrichten
		double[] elp = new double[37]; // Vektor für Elp-Werte.
		double[]   t = new double[ 5]; // Vektor für Jahrhundertbruchteil.
		CVector  rtn = new CVector();

		// Jahrhundertbruchteil einrichten
		t[0] = 1.0;                                    // t^0.
		t[1] = (jd - MCalendar.Jdn20000101) / 36525.0; // t^1.
		t[2] = t[1] * t[1];                            // t^2.
		t[3] = t[1] * t[2];                            // t^3.
		t[4] = t[1] * t[3];                            // t^4.

		// Elp-Terme berechnen
		elp[ 0] = 0.0;              // [Nicht verwendet].
		elp[ 1] = this.SumElp01(t); // Main problem – Longitude.
		elp[ 2] = this.SumElp02(t); // Main problem – Latitude.
		elp[ 3] = this.SumElp03(t); // Main problem – Distance.
		elp[ 4] = this.SumElp04(t); // Earth perturbations – Longitude.
		elp[ 5] = this.SumElp05(t); // Earth perturbations – Latitude.
		elp[ 6] = this.SumElp06(t); // Earth perturbations – Distance.
		elp[ 7] = this.SumElp07(t); // Earth perturbations – Longitude/t.
		elp[ 8] = this.SumElp08(t); // Earth perturbations – Latitude/t.
		elp[ 9] = this.SumElp09(t); // Earth perturbations – Distance/t.
		elp[10] = this.SumElp10(t); // Planetary perturbations – Table 1 Longitude.
		elp[11] = this.SumElp11(t); // Planetary perturbations – Table 1 Latitude.
		elp[12] = this.SumElp12(t); // Planetary perturbations – Table 1 Distance.
		elp[13] = this.SumElp13(t); // Planetary perturbations – Table 1 Longitude/t.
		elp[14] = this.SumElp14(t); // Planetary perturbations – Table 1 Latitude/t.
		elp[15] = this.SumElp15(t); // Planetary perturbations – Table 1 Distance/t.
		elp[16] = this.SumElp16(t); // Planetary perturbations – Table 2 Longitude.
		elp[17] = this.SumElp17(t); // Planetary perturbations – Table 2 Latitude.
		elp[18] = this.SumElp18(t); // Planetary perturbations – Table 2 Distance.
		elp[19] = this.SumElp19(t); // Planetary perturbations – Table 2 Longitude/t.
		elp[20] = this.SumElp20(t); // Planetary perturbations – Table 2 Latitude/t.
		elp[21] = this.SumElp21(t); // Planetary perturbations – Table 2 Distance/t.
		elp[22] = this.SumElp22(t); // Tidal effects – Longitude.
		elp[23] = this.SumElp23(t); // Tidal effects – Latitude.
		elp[24] = this.SumElp24(t); // Tidal effects – Distance.
		elp[25] = this.SumElp25(t); // Tidal effects – Longitude/t.
		elp[26] = this.SumElp26(t); // Tidal effects – Latitude/t.
		elp[27] = this.SumElp27(t); // Tidal effects – Distance/t.
		elp[28] = this.SumElp28(t); // Moon figure perturbations – Longitude.
		elp[29] = this.SumElp29(t); // Moon figure perturbations – Latitude.
		elp[30] = this.SumElp30(t); // Moon figure perturbations – Distance.
		elp[31] = this.SumElp31(t); // Relativistic perturbations – Longitude.
		elp[32] = this.SumElp32(t); // Relativistic perturbations – Latitude.
		elp[33] = this.SumElp33(t); // Relativistic perturbations – Distance.
		elp[34] = this.SumElp34(t); // Planetary perturbations – Solar eccentricity – Longitude/t^2.
		elp[35] = this.SumElp35(t); // Planetary perturbations – Solar eccentricity – Latitude/t^2.
		elp[36] = this.SumElp36(t); // Planetary perturbations – Solar eccentricity – Distance/t^2.
		return rtn;
	}
}
