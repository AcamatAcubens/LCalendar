using Acamat.LMath;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt Berechnungen zum Mond.
/// </summary>
public static partial class MMoon
{
	// MMoon.LatitudeLow(double)
	/// <summary>
	/// Liefert die geozentrisch-ekliptikale Breite des Mondes zur julianischen Tageszahl in geringer Genauigkeit.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Geozentrisch-ekliptikale Breite des Mondes zur julianischen Tageszahl.</returns>
	private static double LatitudeLow(double jd)
	{
		// Lokale Felder einrichten
		double t = (jd - MCalendar.Jdn20000101) / 36525.0;
		double b = 0;

		// Terme aufsummieren
		b +=  5.13 * MMath.Sin(MMath.ToRad( 93.3 + 483202.03 * t));
		b +=  0.28 * MMath.Sin(MMath.ToRad(228.2 + 960400.87 * t));
		b += -0.28 * MMath.Sin(MMath.ToRad(318.3 +   6003.18 * t));
		b += -0.17 * MMath.Sin(MMath.ToRad(217.6 - 407332.20 * t));
		return MMath.ToRad(b);
	}

	// MMoon.LongitudeLow(double)
	/// <summary>
	/// Liefert die geozentrisch-ekliptikale Länge des Mondes zur julianischen Tageszahl in geringer Genauigkeit.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl</param>
	/// <returns>Geozentrisch-ekliptikale Länge des Mondes zur julianischen Tageszahl.</returns>
	private static double LongitudeLow(double jd)
	{
		// Lokale Felder einrichten
		double t = (jd - MCalendar.Jdn20000101) / 36525.0;
		double l = (218.32 + 481267.883 * t).Mod(360.0);

		// Terme aufsummieren
		l +=  6.29 * MMath.Sin(MMath.ToRad(134.9 + 477198.85 * t));
		l += -1.27 * MMath.Sin(MMath.ToRad(259.2 - 413335.38 * t));
		l +=  0.66 * MMath.Sin(MMath.ToRad(235.7 + 890534.23 * t));
		l +=  0.21 * MMath.Sin(MMath.ToRad(269.9 + 954397.70 * t));
		l += -0.19 * MMath.Sin(MMath.ToRad(357.5 +  35999.05 * t));
		l += -0.11 * MMath.Sin(MMath.ToRad(186.6 + 966404.05 * t));
		return MMath.ToRad(l.Mod(360.0));
	}

	// MMoon.LongitudeMedium(double)
	/// <summary>
	/// Liefert die geozentrisch-ekliptikale Länge des Mondes zur julianischen Tageszahl in mittlerer Genauigkeit.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Geozentrisch-ekliptikale Länge des Mondes zur julianischen Tageszahl.</returns>
	private static double LongitudeMedium(double jd)
	{
		// Lokale Felder einrichten
		double t  = (jd - MCalendar.Jdn20000101) / 36525.0;
		double dl = 0.0;
		double l  = MMath.ToRad(MMath.Polynome(t, 218.3164477, 481267.88123421, -0.0015786, 1/  538841, -1/ 65194000).Mod(360.0));
		double d  = MMath.ToRad(MMath.Polynome(t, 297.8501921, 445267.1114034 , -0.0018819, 1/  545868, -1/113065000).Mod(360.0));
		double s  = MMath.ToRad(MMath.Polynome(t, 357.5291092,  35999.0502909 , -0.0001536, 1/24490000              ).Mod(360.0));
		double m  = MMath.ToRad(MMath.Polynome(t, 134.9633964, 477198.8675055 ,  0.0087414, 1/   69699, -1/ 14712000).Mod(360.0));
		double f  = MMath.ToRad(MMath.Polynome(t,  93.2720950, 483202.0175233 , -0.0036539, 1/ 3526000,  1/863310000).Mod(360.0));

		// Störungen durch Planeten Jupiter, Venus und Erde
		double a1 = MMath.ToRad((119.75 +    131.849 * t).Mod(360.0));
		double a2 = MMath.ToRad(( 53.09 + 479264.290 * t).Mod(360.0));
		double a3 = MMath.ToRad((313.45 + 481266.848 * t).Mod(360.0));

		// Terme für Extentrizität
		double e0 = 1.0;
		double e1 = MMath.Polynome(t, 1.0, -0.002516, -0.0000074);
		double e2 = e1 * e1;

		// Störungsterme aufsummieren
		dl += 6288774 * e0 * MMath.Sin(                  +       m          );
		dl += 1274027 * e0 * MMath.Sin(2.0 * d           -       m          );
		dl +=  658314 * e0 * MMath.Sin(2.0 * d                              );
		dl +=  213618 * e0 * MMath.Sin(                  + 2.0 * m          );
		dl += -185116 * e1 * MMath.Sin(        +       s                    );
		dl += -114332 * e0 * MMath.Sin(                            + 2.0 * f);
		dl +=   58793 * e0 * MMath.Sin(2.0 * d           - 2.0 * m          );
		dl +=   57066 * e1 * MMath.Sin(2.0 * d -       s -       m          );
		dl +=   53322 * e0 * MMath.Sin(2.0 * d           +       m          );
		dl +=   45758 * e1 * MMath.Sin(2.0 * d -       s                    );
		dl +=  -40923 * e1 * MMath.Sin(        +       s -       m          );
		dl +=  -34720 * e0 * MMath.Sin(      d                              );
		dl +=  -30383 * e1 * MMath.Sin(        +       s -       m          );
		dl +=   15327 * e0 * MMath.Sin(2.0 * d                     - 2.0 * f);
		dl +=  -12528 * e0 * MMath.Sin(                  +       m + 2.0 * f);
		dl +=   10980 * e0 * MMath.Sin(                  +       m - 2.0 * f);
		dl +=   10675 * e0 * MMath.Sin(4.0 * d           -       m          );
		dl +=   10034 * e0 * MMath.Sin(                  + 3.0 * m          );
		dl +=    8548 * e0 * MMath.Sin(4.0 * d           - 2.0 * m          );
		dl +=   -7888 * e1 * MMath.Sin(2.0 * d +       s -       m          );
		dl +=   -6766 * e1 * MMath.Sin(2.0 * d +       s                    );
		dl +=   -5163 * e0 * MMath.Sin(      d           -       m          );
		dl +=    4987 * e1 * MMath.Sin(      d +       s                    );
		dl +=    4036 * e1 * MMath.Sin(2.0 * d -       s                    );
		dl +=    3994 * e0 * MMath.Sin(2.0 * d           + 2.0 * m          );
		dl +=    3861 * e0 * MMath.Sin(4.0 * d                              );
		dl +=    3665 * e0 * MMath.Sin(2.0 * d           - 3.0 * m          );
		dl +=   -2689 * e1 * MMath.Sin(        +       s - 2.0 * m          );
		dl +=   -2602 * e0 * MMath.Sin(2.0 * d           -       m + 2.0 * f);
		dl +=    2390 * e1 * MMath.Sin(2.0 * d -       s - 2.0 * m          );
		dl +=   -2348 * e0 * MMath.Sin(      d           +       m          );
		dl +=    2236 * e2 * MMath.Sin(2.0 * d + 2.0 * s                    );
		dl +=   -2120 * e1 * MMath.Sin(                s + 2.0 * m          );
		dl +=   -2069 * e2 * MMath.Sin(          2.0 * s                    );
		dl +=    2048 * e2 * MMath.Sin(2.0 * d - 2.0 * s -       m          );
		dl +=   -1773 * e0 * MMath.Sin(2.0 * d           +       m - 2.0 * f);
		dl +=   -1595 * e0 * MMath.Sin(2.0 * d                     + 2.0 * f);
		dl +=    1215 * e1 * MMath.Sin(4.0 * d -       s -       m          );
		dl +=   -1110 * e0 * MMath.Sin(                    2.0 * m + 2.0 * f);
		dl +=    -892 * e0 * MMath.Sin(3.0 * d           -       m          );
		dl +=    -810 * e1 * MMath.Sin(2.0 * d +       s +       m          );
		dl +=     759 * e1 * MMath.Sin(4.0 * d -       s - 2.0 * m          );
		dl +=    -713 * e2 * MMath.Sin(          2.0 * s -       m          );
		dl +=    -700 * e2 * MMath.Sin(2.0 * d + 2.0 * s -       m          );
		dl +=     691 * e1 * MMath.Sin(2.0 * d -       s - 2.0 * m          );
		dl +=     596 * e1 * MMath.Sin(2.0 * d -       s           - 2.0 * f);
		dl +=     549 * e0 * MMath.Sin(4.0 * d           +       m          );
		dl +=     537 * e0 * MMath.Sin(                    4.0 * m          );
		dl +=     520 * e1 * MMath.Sin(4.0 * d -       s                    );
		dl +=    -487 * e0 * MMath.Sin(      d           - 2.0 * m          );
		dl +=    -399 * e1 * MMath.Sin(2.0 * d -       s           - 2.0 * f);
		dl +=    -381 * e0 * MMath.Sin(                    2.0 * m - 2.0 * f);
		dl +=     351 * e1 * MMath.Sin(      d +       s +       m          );
		dl +=    -340 * e0 * MMath.Sin(3.0 * d           - 2.0 * m          );
		dl +=     330 * e0 * MMath.Sin(4.0 * d           - 3.0 * m          );
		dl +=     327 * e1 * MMath.Sin(2.0 * d -       s + 2.0 * m          );
		dl +=    -323 * e2 * MMath.Sin(          2.0 * s +       m          );
		dl +=     299 * e1 * MMath.Sin(      d +       s -       m          );
		dl +=     294 * e0 * MMath.Sin(2.0 * d           + 3.0 * m          );

		// Störungen durch Planeten
		dl += 3958 * MMath.Sin(a1);
		dl += 1962 * MMath.Sin(l - f);
		dl +=  318 * MMath.Sin(a2);

		// Terme anwenden und Länge normalisieren
		return MMath.ToRad((l + dl / 1000000.0).Mod(360.0));
	}

	// MMoon.RadiusLow(double)
	/// <summary>
	/// Liefert den geozentrisch-ekliptikalen Radiusvektor des Mondes zur julianischen Tageszahl in geringer Genauigkeit.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Geozentrisch-ekliptikalen Radiusvektor des Mondes zur julianischen Tageszahl.</returns>
	private static double RadiusLow(double jd)
	{
		// Lokale Felder einrichten
		double t = (jd - MCalendar.Jdn20000101) / 36525.0;
		double p = 0.9508;

		// Terme aufsummieren
		p += 0.0518 * (MMath.ToRad(134.9 + 477198.85 * t)).Cos();
		p += 0.0095 * (MMath.ToRad(259.2 - 413335.38 * t)).Cos();
		p += 0.0078 * (MMath.ToRad(235.7 + 890534.23 * t)).Cos();
		p += 0.0028 * (MMath.ToRad(269.9 + 954397.70 * t)).Cos();
		return (1.0 / MMath.Sin(MMath.ToRad(p))) * (6378.140 / 149597870.700);
	}
}
