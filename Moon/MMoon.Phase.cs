using Acamat.LCore;
using Acamat.LMath;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt Berechnungen zum Mond.
/// </summary>
public static partial class MMoon
{
	// ------------------- //
	// Felder und Methoden //
	// ------------------- //
	// MMoon.FirstQuarter()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten ersten Viertels nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten ersten Viertels nach der aktuellen Systemzeit.</returns>
	public static double FirstQuarter()
	{
		// Lokale Felder einrichten und Ereigniszeit berechnen
		double jd = DateTime.Now.ToJdn();
		return MMoon.FirstQuarter(jd);
	}

	// MMoon.FirstQuarter(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten ersten Viertels nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des nächsten ersten Viertels nach der julianischen Tageszahl.</returns>
	public static double FirstQuarter(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor(12.3685 * (y - 2000.0)) - 0.75;
		double j = 0.0;

		// Berechnungsschleife
		while(j <= jd)
		{
			// Lunation inkrementieren und lokale Felder einrichten
					 k += 1.0;
			double t  = k / 1236.85;

			// Näherung berechnen und Hilfsfelder einrichten
			j = MMath.Polynome(t, 2451550.09766 + 29.530588861 * k, 0.0, 0.00015437, -0.000000150, 0.00000000073);
			double e1 = MMath.Polynome(t, 1.0, -0.002516, -0.0000074);
			double e2 = e1 * e1;
			double m  = MMod.Mod(MMath.ToRad(MMath.Polynome(t,   2.5534 +  29.10535670 * k, 0.0, -0.0000014, -0.00000011              )), MMath.Pi2);
			double a  = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 201.5643 + 385.81693528 * k, 0.0,  0.0107582,  0.00001238, -0.000000058)), MMath.Pi2);
			double f  = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 160.7108 + 390.67050284 * k, 0.0, -0.0016118, -0.00000227,  0.000000011)), MMath.Pi2);
			double o  = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 124.7746 -   1.56375588 * k, 0.0,  0.0020672,  0.00000215              )), MMath.Pi2);
			double h  = 0.0;

			// Korrektur berechnen
			h  = -0.62801      * MMath.Sin(      a                    );
			h +=  0.17172 * e1 * MMath.Sin(                m          );
			h += -0.01183 * e1 * MMath.Sin(      a +       m          );
			h +=  0.00862      * MMath.Sin(2.0 * a                    );
			h +=  0.00804      * MMath.Sin(                    2.0 * f);
			h +=  0.00454 * e1 * MMath.Sin(      a -       m          );
			h +=  0.00204 * e2 * MMath.Sin(          2.0 * m          );
			h += -0.00180      * MMath.Sin(      a           - 2.0 * f);
			h += -0.00070      * MMath.Sin(      a           + 2.0 * f);
			h += -0.00040      * MMath.Sin(3.0 * a                    );
			h += -0.00034 * e1 * MMath.Sin(2.0 * a -       m          );
			h +=  0.00032 * e1 * MMath.Sin(                m + 2.0 * f);
			h +=  0.00032 * e1 * MMath.Sin(                m - 2.0 * f);
			h += -0.00028 * e2 * MMath.Sin(      a + 2.0 * m          );
			h +=  0.00027 * e1 * MMath.Sin(2.0 * a +       m          );
			h += -0.00017      * MMath.Sin(                          o);
			h += -0.00005      * MMath.Sin(      a -       m - 2.0 * f);
			h +=  0.00004      * MMath.Sin(2.0 * a           + 2.0 * f);
			h += -0.00004      * MMath.Sin(      a +       m + 2.0 * f);
			h +=  0.00004      * MMath.Sin(      a - 2.0 * m          );
			h +=  0.00003      * MMath.Sin(      a +       m - 2.0 * f);
			h +=  0.00003      * MMath.Sin(          3.0 * m          );
			h +=  0.00002      * MMath.Sin(2.0 * a           - 2.0 * f);
			h +=  0.00002      * MMath.Sin(      a -       m + 2.0 * f);
			h += -0.00002      * MMath.Sin(3.0 * a +       m          );

			// Viertelkorrektur berechnen
			h +=  0.00306;
			h += -0.00038 * e1 * MMath.Cos(m);
			h +=  0.00026      * MMath.Cos(a);
			h += -0.00002      * MMath.Cos(a - m);
			h +=  0.00002      * MMath.Cos(a + m);
			h +=  0.00002      * MMath.Cos(2.0 * f);

			// Störungen durch Planeten berechnen
			h += 0.000325 * MMath.Sin(MMath.ToRad(299.77 +  0.107408 * k - 0.009173 * t * t));
			h += 0.000165 * MMath.Sin(MMath.ToRad(251.88 +  0.016321 * k));
			h += 0.000164 * MMath.Sin(MMath.ToRad(251.83 + 26.651886 * k));
			h += 0.000126 * MMath.Sin(MMath.ToRad(349.42 + 36.412478 * k));
			h += 0.000110 * MMath.Sin(MMath.ToRad( 84.66 + 18.206239 * k));
			h += 0.000062 * MMath.Sin(MMath.ToRad(141.74 + 53.303771 * k));
			h += 0.000060 * MMath.Sin(MMath.ToRad(207.14 +  2.453732 * k));
			h += 0.000056 * MMath.Sin(MMath.ToRad(154.84 +  7.306860 * k));
			h += 0.000047 * MMath.Sin(MMath.ToRad( 34.52 + 27.261239 * k));
			h += 0.000042 * MMath.Sin(MMath.ToRad(207.19 +  0.121824 * k));
			h += 0.000040 * MMath.Sin(MMath.ToRad(291.34 +  1.844379 * k));
			h += 0.000037 * MMath.Sin(MMath.ToRad(161.72 + 24.198154 * k));
			h += 0.000035 * MMath.Sin(MMath.ToRad(239.56 + 25.513099 * k));
			h += 0.000023 * MMath.Sin(MMath.ToRad(331.55 +  3.592518 * k));

			// Korrekturen anwenden
			j += h;
		}
		return j;
	}

	// MMoon.FullMoon()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Vollmondes nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Liefert die julianische Tageszahl des nächsten Vollmondes nach der aktuellen Systemzeit.</returns>
	public static double FullMoon()
	{
		// Private Felder einrichten und Ereigniszeit berechnen
		EEclipseType t  = EEclipseType.None;
		double       jd = DateTime.Now.ToJdn();
		return MMoon.FullMoon(jd, ref t);
	}

	// MMoon.FullMoon(ref EEclipseType)
	/// <summary>
	/// Setzt die Kennung der Finsternisabschätzung und liefert die julianische Tageszahl des nächsten Vollmondes nach der aktuellen Systemzeit.
	/// </summary>
	/// <param name="type">Kennung der Finsternisabschätzung.</param>
	/// <returns>Julianische Tageszahl des nächsten Vollmondes nach der julianischen Tageszahl.</returns>
	public static double FullMoon(ref EEclipseType type)
	{
		// Private Felder einrichten und Eregniszeit berechnen
		double jd = DateTime.Now.ToJdn();
		return MMoon.FullMoon(jd, ref type);
	}

	// MMoon.FullMoon(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Vollmondes nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des nächsten Vollmondes nach der julianischen Tageszahl.</returns>
	public static double FullMoon(double jd)
	{
		// Private Felder einrichten und Ereigniszeit berechnen
		EEclipseType t = EEclipseType.None;
		return MMoon.FullMoon(jd, ref t);
	}

	// MMoon.FullMoon(double, ref EEclipseType)
	/// <summary>
	/// Setzt die Kennung der Finsternisabschätzung und liefert die julianische Tageszahl des nächsten Vollmondes nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="type">Kennung der Finsternisabschätzung.</param>
	/// <returns>Julianische Tageszahl des nächsten Vollmondes nach der julianischen Tageszahl.</returns>
	public static double FullMoon(double jd, ref EEclipseType type)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor(12.3685 * (y - 2000.0)) - 1.5;
		double j = 0.0;

		// Berechnungschleife
		while(j <= jd)
		{
			// Lunation inkrementieren und lokale Felder einrichten
						k += 1.0;
			double t  = k / 1236.85;

			// Näherung berechnen und Hilfsfelder einrichten
			j = MMath.Polynome(t, 2451550.09766 + 29.530588861 * k, 0.0, 0.00015437, -0.000000150, 0.00000000073);
			double e1 = MMath.Polynome(t, 1.0, -0.002516, -0.0000074);
			double e2 = e1 * e1;
			double m  = MMod.Mod(MMath.ToRad(MMath.Polynome(t,   2.5534 +  29.10535670 * k, 0.0, -0.0000014, -0.00000011              )), MMath.Pi2);
			double a  = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 201.5643 + 385.81693528 * k, 0.0,  0.0107582,  0.00001238, -0.000000058)), MMath.Pi2);
			double f  = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 160.7108 + 390.67050284 * k, 0.0, -0.0016118, -0.00000227,  0.000000011)), MMath.Pi2);
			double o  = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 124.7746 -   1.56375588 * k, 0.0,  0.0020672,  0.00000215              )), MMath.Pi2);
			double h;

			// Korrektur berechnen
			h  = -0.40614      * MMath.Sin(      a                    );
			h +=  0.17302 * e1 * MMath.Sin(                m          );
			h +=  0.01614      * MMath.Sin(2.0 * a                    );
			h +=  0.01043      * MMath.Sin(                    2.0 * f);
			h +=  0.00734 * e1 * MMath.Sin(      a -       m          );
			h += -0.00515 * e1 * MMath.Sin(      a +       m          );
			h +=  0.00209 * e2 * MMath.Sin(          2.0 * m          );
			h += -0.00111      * MMath.Sin(      a           - 2.0 * f);
			h += -0.00057      * MMath.Sin(      a           + 2.0 * f);
			h +=  0.00056 * e1 * MMath.Sin(2.0 * a +       m          );
			h += -0.00042      * MMath.Sin(3.0 * a                    );
			h +=  0.00042 * e1 * MMath.Sin(                m + 2.0 * f);
			h +=  0.00038 * e1 * MMath.Sin(                m - 2.0 * f);
			h += -0.00024 * e1 * MMath.Sin(2.0 * a -       m          );
			h += -0.00017      * MMath.Sin(                          o);
			h += -0.00007      * MMath.Sin(      a + 2.0 * m          );
			h +=  0.00004      * MMath.Sin(2.0 * a - 2.0 * f          );
			h +=  0.00004      * MMath.Sin(          3.0 * m          );
			h +=  0.00003      * MMath.Sin(      a +       m - 2.0 * f);
			h +=  0.00003      * MMath.Sin(2.0 * a           + 2.0 * f);
			h += -0.00003      * MMath.Sin(      a +       m + 2.0 * f);
			h +=  0.00003      * MMath.Sin(      a -       m + 2.0 * f);
			h += -0.00002      * MMath.Sin(      a -       m - 2.0 * f);
			h += -0.00002      * MMath.Sin(3.0 * a +       m          );
			h +=  0.00002      * MMath.Sin(4.0 * a                    );

			// Störungen durch Planeten berechnen
			h += 0.000325 * MMath.Sin(MMath.ToRad(299.77 +  0.107408 * k - 0.009173 * t * t));
			h += 0.000165 * MMath.Sin(MMath.ToRad(251.88 +  0.016321 * k));
			h += 0.000164 * MMath.Sin(MMath.ToRad(251.83 + 26.651886 * k));
			h += 0.000126 * MMath.Sin(MMath.ToRad(349.42 + 36.412478 * k));
			h += 0.000110 * MMath.Sin(MMath.ToRad( 84.66 + 18.206239 * k));
			h += 0.000062 * MMath.Sin(MMath.ToRad(141.74 + 53.303771 * k));
			h += 0.000060 * MMath.Sin(MMath.ToRad(207.14 +  2.453732 * k));
			h += 0.000056 * MMath.Sin(MMath.ToRad(154.84 +  7.306860 * k));
			h += 0.000047 * MMath.Sin(MMath.ToRad( 34.52 + 27.261239 * k));
			h += 0.000042 * MMath.Sin(MMath.ToRad(207.19 +  0.121824 * k));
			h += 0.000040 * MMath.Sin(MMath.ToRad(291.34 +  1.844379 * k));
			h += 0.000037 * MMath.Sin(MMath.ToRad(161.72 + 24.198154 * k));
			h += 0.000035 * MMath.Sin(MMath.ToRad(239.56 + 25.513099 * k));
			h += 0.000023 * MMath.Sin(MMath.ToRad(331.55 +  3.592518 * k));

			// Korrekturen anwenden
			j += h;
		}

		// Ekliptikale Breite berechnen und Finsterniseinschätzung bestimmen
		double  b = MMath.Abs(MMoon.Latitude(EPrecision.Medium, j));
		if     (b < 0.006351) type = EEclipseType.MoonTotalDefinite;
		else if(b < 0.009376) type = EEclipseType.MoonTotalPotential;
		else if(b < 0.015533) type = EEclipseType.MoonPartialDefinite;
		else if(b < 0.018568) type = EEclipseType.MoonPartialPotential;
		else if(b < 0.025089) type = EEclipseType.MoonPenumbralDefinite;
		else if(b < 0.028134) type = EEclipseType.MoonPenumbralPotential;
		else                  type = EEclipseType.MoonNoEclipse;
		return j;
	}

	// MMoon.LastQuarter()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten letzten Viertels nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Liefert die julianische Tageszahl des nächsten letzten Viertels nach der aktuellen Systemzeit.</returns>
	public static double LastQuarter()
	{
		// Lokale Felder einrichten und Ereigniszeit berechnen
		double jd = DateTime.Now.ToJdn();
		return MMoon.LastQuarter(jd);
	}

	// MMoon.LastQuarter(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten letzten Viertels nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Liefert die julianische Tageszahl des nächsten letzten Viertels nach der julianischen Tageszahl.</returns>
	public static double LastQuarter(double jd)
	{
		// Lokale Felder einrichten und Ereigniszeit berechnen
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor(12.3685 * (y - 2000.0)) - 1.25;
		double j = 0.0;

		// Berechnungschleife
		while(j <= jd)
		{
			// Lunation inkrementieren und lokale Felder einrichten
						k += 1.0;
			double t  = k / 1236.85;

			// Näherung berechnen und Hilfsfelder einrichten
			j = MMath.Polynome(t, 2451550.09766 + 29.530588861 * k, 0.0, 0.00015437, -0.000000150, 0.00000000073);
			double e1 = MMath.Polynome(t, 1.0, -0.002516, -0.0000074);
			double e2 = e1 * e1;
			double m  = MMod.Mod(MMath.ToRad(MMath.Polynome(t,   2.5534 +  29.10535670 * k, 0.0, -0.0000014, -0.00000011              )), MMath.Pi2);
			double a  = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 201.5643 + 385.81693528 * k, 0.0,  0.0107582,  0.00001238, -0.000000058)), MMath.Pi2);
			double f  = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 160.7108 + 390.67050284 * k, 0.0, -0.0016118, -0.00000227,  0.000000011)), MMath.Pi2);
			double o  = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 124.7746 -   1.56375588 * k, 0.0,  0.0020672,  0.00000215              )), MMath.Pi2);
			double h;

			// Korrektur berechnen
			h  = -0.62801      * MMath.Sin(      a                    );
			h +=  0.17172 * e1 * MMath.Sin(                m          );
			h += -0.01183 * e1 * MMath.Sin(      a +       m          );
			h +=  0.00862      * MMath.Sin(                    2.0 * a);
			h +=  0.00804      * MMath.Sin(                    2.0 * f);
			h +=  0.00454 * e1 * MMath.Sin(      a -       m          );
			h +=  0.00204 * e2 * MMath.Sin(          2.0 * m          );
			h += -0.00180      * MMath.Sin(      a           - 2.0 * f);
			h += -0.00070      * MMath.Sin(      a           + 2.0 * f);
			h += -0.00040      * MMath.Sin(3.0 * a                    );
			h += -0.00034 * e1 * MMath.Sin(2.0 * a -       m          );
			h +=  0.00032 * e1 * MMath.Sin(                m + 2.0 * f);
			h +=  0.00032 * e1 * MMath.Sin(                m - 2.0 * f);
			h += -0.00028 * e2 * MMath.Sin(      a + 2.0 * m          );
			h +=  0.00027 * e1 * MMath.Sin(2.0 * a +       m          );
			h += -0.00017      * MMath.Sin(                          o);
			h += -0.00005      * MMath.Sin(      a -       m - 2.0 * f);
			h +=  0.00004      * MMath.Sin(2.0 * a           + 2.0 * f);
			h += -0.00004      * MMath.Sin(      a +       m + 2.0 * f);
			h +=  0.00004      * MMath.Sin(      a - 2.0 * m          );
			h +=  0.00003      * MMath.Sin(      a +       m - 2.0 * f);
			h +=  0.00003      * MMath.Sin(          3.0 * m          );
			h +=  0.00002      * MMath.Sin(2.0 * a           - 2.0 * f);
			h +=  0.00002      * MMath.Sin(      a -       m + 2.0 * f);
			h += -0.00002      * MMath.Sin(3.0 * a +       m          );

			// Viertelkorrektur berechnen
			h -=  0.00306;
			h -= -0.00038 * e1 * MMath.Cos(m);
			h -=  0.00026      * MMath.Cos(a);
			h -= -0.00002      * MMath.Cos(a - m);
			h -=  0.00002      * MMath.Cos(a + m);
			h -=  0.00002      * MMath.Cos(2.0 * f);

			// Störungen durch Planeten berechnen
			h += 0.000325 * MMath.Sin(MMath.ToRad(299.77 +  0.107408 * k - 0.009173 * t * t));
			h += 0.000165 * MMath.Sin(MMath.ToRad(251.88 +  0.016321 * k));
			h += 0.000164 * MMath.Sin(MMath.ToRad(251.83 + 26.651886 * k));
			h += 0.000126 * MMath.Sin(MMath.ToRad(349.42 + 36.412478 * k));
			h += 0.000110 * MMath.Sin(MMath.ToRad( 84.66 + 18.206239 * k));
			h += 0.000062 * MMath.Sin(MMath.ToRad(141.74 + 53.303771 * k));
			h += 0.000060 * MMath.Sin(MMath.ToRad(207.14 +  2.453732 * k));
			h += 0.000056 * MMath.Sin(MMath.ToRad(154.84 +  7.306860 * k));
			h += 0.000047 * MMath.Sin(MMath.ToRad( 34.52 + 27.261239 * k));
			h += 0.000042 * MMath.Sin(MMath.ToRad(207.19 +  0.121824 * k));
			h += 0.000040 * MMath.Sin(MMath.ToRad(291.34 +  1.844379 * k));
			h += 0.000037 * MMath.Sin(MMath.ToRad(161.72 + 24.198154 * k));
			h += 0.000035 * MMath.Sin(MMath.ToRad(239.56 + 25.513099 * k));
			h += 0.000023 * MMath.Sin(MMath.ToRad(331.55 +  3.592518 * k));

			// Korrekturen anwenden
			j += h;
		}

		// Rückgabe
		return j;
	}

	// MMoon.NewMoon()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Neumondes nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Neumondes nach der aktuellen Systemzeit.</returns>
	public static double NewMoon()
	{
		// Lokale Felder einrichten und Ereigniszeit berechnen
		EEclipseType t  = EEclipseType.None;
		double       jd = DateTime.Now.ToJdn();
		return MMoon.NewMoon(jd, ref t);
	}

	// MMoon.NewMoon(ref EEclipseType)
	/// <summary>
	/// Setzt die Kennung der Finsternisabschätzung und liefert die julianische Tageszahl des nächsten Neumondes nach der aktuellen Systemzeit.
	/// </summary>
	/// <param name="type">Kennung der Finsternisabschätzung.</param>
	/// <returns>Julianische Tageszahl des nächsten Neumondes nach der julianischen Tageszahl.</returns>
	public static double NewMoon(ref EEclipseType type)
	{
		// Lokale Felder einrichten und Ereigniszeit berechnen
		double jd = DateTime.Now.ToJdn();
		return MMoon.NewMoon(jd, ref type);
	}

	// MMoon.NewMoon(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Neumondes nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des nächsten Neumondes nach der julianischen Tageszahl.</returns>
	public static double NewMoon(double jd)
	{
		// Lokale Felder einrichten und Ereigniszeit berechnen
		EEclipseType t = EEclipseType.None;
		return MMoon.NewMoon(jd, ref t);
	}

	// MMoon.NewMoon(double, ref EEclipseType)
	/// <summary>
	/// Setzt die Kennung der Finsternisabschätzung und liefert die julianische Tageszahl des nächsten Neumondes nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="type">Kennung der Finsternisabschätzung.</param>
	/// <returns>Julianische Tageszahl des nächsten Neumondes nach der julianischen Tageszahl.</returns>
	public static double NewMoon(double jd, ref EEclipseType type)
	{
		// Lokale Felder einrichten und Ereigniszeit berechen
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor(12.3685 * (y - 2000.0)) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while(j <= jd)
		{
			// Lunation inkrementieren und lokale Felder einrichten
						k += 1.0;
			double t  = k / 1236.85;

			// Näherung berechnen und Hilfsfelder einrichten
			j = MMath.Polynome(t, 2451550.09766 + 29.530588861 * k, 0.0, 0.00015437, -0.000000150, 0.00000000073);
			double e1 = MMath.Polynome(t, 1.0, -0.002516, -0.0000074);
			double e2 = e1 * e1;
			double m  = MMod.Mod(MMath.ToRad(MMath.Polynome(t,   2.5534 +  29.10535670 * k, 0.0, -0.0000014, -0.00000011              )), MMath.Pi2);
			double a  = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 201.5643 + 385.81693528 * k, 0.0,  0.0107582,  0.00001238, -0.000000058)), MMath.Pi2);
			double f  = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 160.7108 + 390.67050284 * k, 0.0, -0.0016118, -0.00000227,  0.000000011)), MMath.Pi2);
			double o  = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 124.7746 -   1.56375588 * k, 0.0,  0.0020672,  0.00000215              )), MMath.Pi2);
			double h;

			// Korrektur berechnen
			h  = -0.40720      * MMath.Sin(      a                    );
			h +=  0.17241 * e1 * MMath.Sin(                m          );
			h +=  0.01608      * MMath.Sin(2.0 * a                    );
			h +=  0.01039      * MMath.Sin(                    2.0 * f);
			h +=  0.00739 * e1 * MMath.Sin(      a -       m          );
			h += -0.00514 * e1 * MMath.Sin(      a +       m          );
			h +=  0.00208 * e2 * MMath.Sin(          2.0 * m          );
			h += -0.00111      * MMath.Sin(      a           - 2.0 * f);
			h += -0.00057      * MMath.Sin(      a           + 2.0 * f);
			h +=  0.00056 * e1 * MMath.Sin(2.0 * a +       m          );
			h += -0.00042      * MMath.Sin(3.0 * a                    );
			h +=  0.00042 * e1 * MMath.Sin(                m + 2.0 * f);
			h +=  0.00038 * e1 * MMath.Sin(                m - 2.0 * f);
			h += -0.00024 * e1 * MMath.Sin(2.0 * a -       m          );
			h += -0.00017      * MMath.Sin(                          o);
			h += -0.00007      * MMath.Sin(      a + 2.0 * m          );
			h +=  0.00004      * MMath.Sin(2.0 * a           - 2.0 * f);
			h +=  0.00004      * MMath.Sin(          3.0 * m          );
			h +=  0.00003      * MMath.Sin(      a +       m - 2.0 * f);
			h +=  0.00003      * MMath.Sin(2.0 * a           + 2.0 * f);
			h += -0.00003      * MMath.Sin(      a +       m + 2.0 * f);
			h +=  0.00003      * MMath.Sin(      a -       m + 2.0 * f);
			h += -0.00002      * MMath.Sin(      a -       m - 2.0 * f);
			h += -0.00002      * MMath.Sin(3.0 * a +       m          );
			h +=  0.00002      * MMath.Sin(4.0 * a                    );

			// Störungen durch Planeten berechnen
			h += 0.000325 * MMath.Sin(MMath.ToRad(299.77 +  0.107408 * k - 0.009173 * t * t));
			h += 0.000165 * MMath.Sin(MMath.ToRad(251.88 +  0.016321 * k));
			h += 0.000164 * MMath.Sin(MMath.ToRad(251.83 + 26.651886 * k));
			h += 0.000126 * MMath.Sin(MMath.ToRad(349.42 + 36.412478 * k));
			h += 0.000110 * MMath.Sin(MMath.ToRad( 84.66 + 18.206239 * k));
			h += 0.000062 * MMath.Sin(MMath.ToRad(141.74 + 53.303771 * k));
			h += 0.000060 * MMath.Sin(MMath.ToRad(207.14 +  2.453732 * k));
			h += 0.000056 * MMath.Sin(MMath.ToRad(154.84 +  7.306860 * k));
			h += 0.000047 * MMath.Sin(MMath.ToRad( 34.52 + 27.261239 * k));
			h += 0.000042 * MMath.Sin(MMath.ToRad(207.19 +  0.121824 * k));
			h += 0.000040 * MMath.Sin(MMath.ToRad(291.34 +  1.844379 * k));
			h += 0.000037 * MMath.Sin(MMath.ToRad(161.72 + 24.198154 * k));
			h += 0.000035 * MMath.Sin(MMath.ToRad(239.56 + 25.513099 * k));
			h += 0.000023 * MMath.Sin(MMath.ToRad(331.55 +  3.592518 * k));

			// Korrekturen anwenden
			j += h;
		}

		// Ekliptikale Breite berechnen und Finsternisabschätzung bestimmen
		double  b = MMath.Abs(MMoon.Latitude(EPrecision.Medium, j));
		if     (b < 0.015223) type = EEclipseType.SunCentralDefinite;
		else if(b < 0.018210) type = EEclipseType.SunCentralPotential;
		else if(b < 0.024595) type = EEclipseType.SunPartialDefinite;
		else if(b < 0.027586) type = EEclipseType.SunPartialPotential;
		else                  type = EEclipseType.SunNoEclipse;
		return j;
	}

	// MMoon.PhaseAngle()
	/// <summary>
	/// Liefert den Phasenwinkel zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Phasenwinkel zur aktuellen Systemzeit.</returns>
	public static double PhaseAngle()
	{
		// Phasenwinkel berechnen
		double jdn = DateTime.Now.ToJdn();
		return MMoon.PhaseAngle(jdn);
	}

	// MMoon.PhaseAngle(double)
	/// <summary>
	/// Liefert den Phasenwinkel zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Phasenwinkel zur julianischen Tageszahl.</returns>
	public static double PhaseAngle(double jd)
	{
		// Lokale Felder einrichten
		double betM = MMoon.Latitude (EPrecision.Low, jd);
		double lamM = MMoon.Longitude(EPrecision.Low, jd);
		double lamS = MSun .Longitude(EPrecision.Low, jd);

		// Geozentrische Elongation berechnen
		double cosPsi = MMath.Cos(betM) * MMath.Cos(lamM - lamS);
		double sinPsi = MMath.Sin(MMath.ArcCos(cosPsi));

		// Radii berechnen
		double delM = MMoon.Radius(EPrecision.Low, jd);
		double delS = MSun .Radius(EPrecision.Low, jd);

		// Phasenwinkel berechnen
		double i = MMath.ToDeg(MMath.ArcTan((delS * sinPsi) / (delM - delS * cosPsi)));
		if(lamM - lamS < 0.0) i *= 1.0;
		return i;
	}
}
