using Acamat.LCore;
using Acamat.LMath;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt ephemeridale Funktionen.
/// </summary>
public static partial class MEphemerides
{
	// ------------------- //
	// Felder und Methoden //
	// ------------------- //
	// MEphemerides.NutationInLongitude()
	/// <summary>
	/// Liefert die Nutation der Länge zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Nutation der Länge zur aktuellen Systemzeit.</returns>
	public static double NutationInLongitude()
	{
		// Nutation berechnen
		double jd = DateTime.Now.ToJdn();
		return MEphemerides.NutationInLongitude(jd);
	}

	// MEphemerides.NutationInLongitude(double)
	/// <summary>
	/// Liefert die Nutation der Länge zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Nutation der Länge zur julianischen Tageszahl.</returns>
	public static double NutationInLongitude(double jd)
	{
		// Lokale Felder einrichten
		double t = (jd - MCalendar.Jdn20000101) / 36525.0;
		double d = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 297.85036, 445267.111480, -0.0019142,  1/189474)), MMath.Pi2);
		double s = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 357.52772,  35999.050340, -0.0001603, -1/300000)), MMath.Pi2);
		double m = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 134.96298, 477198.867398,  0.0086972,  1/ 56250)), MMath.Pi2);
		double f = MMod.Mod(MMath.ToRad(MMath.Polynome(t,  93.27191, 483202.017538, -0.0036825,  1/327270)), MMath.Pi2);
		double o = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 125.04452,  -1934.136261,  0.0020708,  1/450000)), MMath.Pi2);
		double p = 0.0;

		// Korrektur berechnen und anwenden
		p += (-171996.0 - 174.2 * t) * MMath.Sin(                                               o);
		p += ( -13187.0 -   1.6 * t) * MMath.Sin(-2.0 * d                     + 2.0 * f + 2.0 * o);
		p += (  -2274.0 -   0.2 * t) * MMath.Sin(                             + 2.0 * f + 2.0 * o);
		p += (   2062.0 +   0.2 * t) * MMath.Sin(                                         2.0 * o);
		p += (   1426.0 -   3.4 * t) * MMath.Sin(                 s                              );
		p += (    712.0 +   0.1 * t) * MMath.Sin(                           m                    );
		p += (   -517.0 +   1.2 * t) * MMath.Sin(-2.0 * d +       s           + 2.0 * f + 2.0 * o);
		p += (   -386.0 -   0.4 * t) * MMath.Sin(                               2.0 * f +       o);
		p += (   -301.0            ) * MMath.Sin(                           m + 2.0 * f + 2.0 * o);
		p += (    217.0 -   0.5 * t) * MMath.Sin(-2.0 * d -       s           + 2.0 * f + 2.0 * o);
		p += (   -158.0            ) * MMath.Sin(-2.0 * d           +       m                    );
		p += (    129.0 +   0.1 * t) * MMath.Sin(-2.0 * d                     + 2.0 * f +       o);
		p += (    123.0            ) * MMath.Sin(                   -       m + 2.0 * f + 2.0 * o);
		p += (     63.0            ) * MMath.Sin( 2.0 * d                                        );
		p += (     63.0 +   1.0 * t) * MMath.Sin(                           m           +       o);
		p += (    -59.0            ) * MMath.Sin( 2.0 * d           -       m + 2.0 * f + 2.0 * o);
		p += (    -58.0 -   0.1 * t) * MMath.Sin(                   -       m           +       o);
		p += (    -51.0            ) * MMath.Sin(                           m + 2.0 * f +       o);
		p += (     48.0            ) * MMath.Sin(-2.0 * d           + 2.0 * m                    );
		p += (     46.0            ) * MMath.Sin(                   - 2.0 * m + 2.0 * f +       o);
		p += (    -38.0            ) * MMath.Sin( 2.0 * d                     + 2.0 * f + 2.0 * o);
		p += (    -31.0            ) * MMath.Sin(                   + 2.0 * m + 2.0 * f + 2.0 * o);
		p += (     29.0            ) * MMath.Sin(                   + 2.0 * m                    );
		p += (     29.0            ) * MMath.Sin(-2.0 * d           +       m + 2.0 * f + 2.0 * o);
		p += (     26.0            ) * MMath.Sin(                             + 2.0 * f          );
		p += (    -22.0            ) * MMath.Sin(-2.0 * d                     + 2.0 * f          );
		p += (     21.0            ) * MMath.Sin(                   -       m + 2.0 * f +       o);
		p += (     17.0 -   0.1 * t) * MMath.Sin(         + 2.0 * s                              );
		p += (     16.0            ) * MMath.Sin(                   -       m           +       o);
		p += (    -16.0 +   0.1 * t) * MMath.Sin(-2.0 * d + 2.0 * s           + 2.0 * f + 2.0 * o);
		p += (    -15.0            ) * MMath.Sin(                 s                     +       o);
		p += (    -13.0            ) * MMath.Sin(-2.0 * d           +       m           +       o);
		p += (    -12.0            ) * MMath.Sin(         -       s                     +       o);
		p += (     11.0            ) * MMath.Sin(                   + 2.0 * m - 2.0 * f          );
		p += (    -10.0            ) * MMath.Sin( 2.0 * d           -       m + 2.0 * f +       o);
		p += (     -8.0            ) * MMath.Sin( 2.0 * d           +       m + 2.0 * f + 2.0 * o);
		p += (      7.0            ) * MMath.Sin(                 s           + 2.0 * f + 2.0 * o);
		p += (     -7.0            ) * MMath.Sin(-2.0 * d +       s +       m                    );
		p += (     -7.0            ) * MMath.Sin(         -       s           + 2.0 * f + 2.0 * o);
		p += (     -7.0            ) * MMath.Sin( 2.0 * d                     + 2.0 * f +       o);
		p += (      6.0            ) * MMath.Sin( 2.0 * d           +       m                    );
		p += (      6.0            ) * MMath.Sin(-2.0 * d           + 2.0 * m + 2.0 * f + 2.0 * o);
		p += (      6.0            ) * MMath.Sin(-2.0 * d           +       m + 2.0 * f +       o);
		p += (     -6.0            ) * MMath.Sin( 2.0 * d           - 2.0 * m           +       o);
		p += (     -6.0            ) * MMath.Sin( 2.0 * d                               +       o);
		p += (      5.0            ) * MMath.Sin(         -       s +       m                    );
		p += (     -5.0            ) * MMath.Sin(-2.0 * d -       s           + 2.0 * f +       o);
		p += (     -5.0            ) * MMath.Sin(-2.0 * d                               +       o);
		p += (     -5.0            ) * MMath.Sin(                   + 2.0 * m + 2.0 * f +       o);
		p += (      4.0            ) * MMath.Sin(-2.0 * d           + 2.0 * m           +       o);
		p += (      4.0            ) * MMath.Sin(-2.0 * d +       s           + 2.0 * f +       o);
		p += (      4.0            ) * MMath.Sin(                   -       m - 2.0 * f          );
		p += (     -4.0            ) * MMath.Sin(-      d           -       m                    );
		p += (     -4.0            ) * MMath.Sin(-2.0 * d +       s                              );
		p += (     -4.0            ) * MMath.Sin(       d                                        );
		p += (      3.0            ) * MMath.Sin(                           m + 2.0 * f          );
		p += (     -3.0            ) * MMath.Sin(                   - 2.0 * m + 2.0 * f + 2.0 * o);
		p += (     -3.0            ) * MMath.Sin(-      d -       s +       m                    );
		p += (     -3.0            ) * MMath.Sin(                 s +       m                    );
		p += (     -3.0            ) * MMath.Sin(                 s +       m + 2.0 * f + 2.0 * o);
		p += (     -3.0            ) * MMath.Sin( 2.0 * d +       s -       m + 2.0 * f + 2.0 * o);
		p += (     -3.0            ) * MMath.Sin(                   + 3.0 * m + 2.0 * f + 2.0 * o);
		p += (     -3.0            ) * MMath.Sin( 2.0 * d -       s           + 2.0 * f + 2.0 * o);
		return MMath.ToRad(p / (10000.0 * 3600.0));
	}

	// MEphemerides.NutationInObliquity()
	/// <summary>
	/// Liefert die Nutation der Ekliptikschiefe zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Nutation der Ekliptikschiefe zur aktuellen Systemzeit.</returns>
	public static double NutationInObliquity()
	{
		// Nutation berechnen
		double jd = DateTime.Now.ToJdn();
		return MEphemerides.NutationInObliquity(jd);
	}

	// MEphemerides.NutationInObliquity(double)
	/// <summary>
	/// Liefert die Nutation der Ekliptikschiefe zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Nutation der Ekliptikschiefe zur julianischen Tageszahl.</returns>
	public static double NutationInObliquity(double jd)
	{
		// Lokale Felder einrichten
		double t = (jd - MCalendar.Jdn20000101) / 36525.0;
		double d = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 297.85036, 445267.111480, -0.0019142,  1/189474)), MMath.Pi2);
		double s = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 357.52772,  35999.050340, -0.0001603, -1/300000)), MMath.Pi2);
		double m = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 134.96298, 477198.867398,  0.0086972,  1/ 56250)), MMath.Pi2);
		double f = MMod.Mod(MMath.ToRad(MMath.Polynome(t,  93.27191, 483202.017538, -0.0036825,  1/327270)), MMath.Pi2);
		double o = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 125.04452,  -1934.136261,  0.0020708,  1/450000)), MMath.Pi2);
		double e = 0.0;

		// Korrektur berechnen und anwenden
		e += ( 92025.0 + 8.9 * t) * MMath.Cos(                                               o);
		e += (  5736.0 - 3.1 * t) * MMath.Cos(-2.0 * d                     + 2.0 * f + 2.0 * o);
		e += (   977.0 - 0.5 * t) * MMath.Cos(                             + 2.0 * f + 2.0 * o);
		e += (  -895.0 + 0.5 * t) * MMath.Cos(                                       + 2.0 * o);
		e += (    54.0 - 0.1 * t) * MMath.Cos(                 s                              );
		e += (    -7.0          ) * MMath.Cos(                           m                    );
		e += (   224.0 - 0.6 * t) * MMath.Cos(-2.0 * d +       s           + 2.0 * f + 2.0 * o);
		e += (   200.0          ) * MMath.Cos(                             + 2.0 * f +       o);
		e += (   129.0 - 0.1 * t) * MMath.Cos(                           m + 2.0 * f + 2.0 * o);
		e += (   -95.0 + 0.3 * t) * MMath.Cos(-2.0 * d -       s           + 2.0 * f + 2.0 * o);
		e += (   -70.0          ) * MMath.Cos(-2.0 * d                     + 2.0 * f +       o);
		e += (   -53.0          ) * MMath.Cos(                   -       m + 2.0 * f + 2.0 * o);
		e += (   -33.0          ) * MMath.Cos(                           m           +       o);
		e += (    26.0          ) * MMath.Cos( 2.0 * d           -       m + 2.0 * f + 2.0 * o);
		e += (    32.0          ) * MMath.Cos(                   -       m           +       o);
		e += (    27.0          ) * MMath.Cos(                           m + 2.0 * f +       o);
		e += (   -24.0          ) * MMath.Cos(                   - 2.0 * m + 2.0 * f +       o);
		e += (    16.0          ) * MMath.Cos( 2.0 * d                     + 2.0 * f + 2.0 * o);
		e += (    13.0          ) * MMath.Cos(                   + 2.0 * m + 2.0 * f + 2.0 * o);
		e += (   -12.0          ) * MMath.Cos(-2.0 * d           +       m + 2.0 * f + 2.0 * o);
		e += (   -10.0          ) * MMath.Cos(                   -       m + 2.0 * f +       o);
		e += (    -8.0          ) * MMath.Cos( 2.0 * d           -       m           +       o);
		e += (     7.0          ) * MMath.Cos(-2.0 * d + 2.0 * s           + 2.0 * f + 2.0 * o);
		e += (     9.0          ) * MMath.Cos(                 s                     +       o);
		e += (     7.0          ) * MMath.Cos(-2.0 * d           +       m           +       o);
		e += (     6.0          ) * MMath.Cos(         -       s                     +       o);
		e += (     5.0          ) * MMath.Cos( 2.0 * d           -       m + 2.0 * f +       o);
		e += (     3.0          ) * MMath.Cos( 2.0 * d           +       m + 2.0 * f + 2.0 * o);
		e += (    -3.0          ) * MMath.Cos(                 s           + 2.0 * f + 2.0 * o);
		e += (     3.0          ) * MMath.Cos(         -       s           + 2.0 * f + 2.0 * o);
		e += (     3.0          ) * MMath.Cos( 2.0 * d                     + 2.0 * f +       o);
		e += (    -3.0          ) * MMath.Cos(-2.0 * d           + 2.0 * m + 2.0 * f + 2.0 * o);
		e += (    -3.0          ) * MMath.Cos(-2.0 * d           +       m + 2.0 * f +       o);
		e += (     3.0          ) * MMath.Cos( 2.0 * d           - 2.0 * m           +       o);
		e += (     3.0          ) * MMath.Cos( 2.0 * d                               +       o);
		e += (     3.0          ) * MMath.Cos(-2.0 * d -       s           + 2.0 * f +       o);
		e += (     3.0          ) * MMath.Cos(-2.0 * d                               +       o);
		e += (     3.0          ) * MMath.Cos(                   + 2.0 * m + 2.0 * f +       o);
		return MMath.ToRad(e / (10000.0 * 3600.0));
	}
}
