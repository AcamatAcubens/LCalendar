using Acamat.LCore;
using Acamat.LMath;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt Berechnungen zum Jupiter.
/// </summary>
public static partial class MJupiter
{
	// MJupiter.Aphelion()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch das Aphel nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch das Aphel nach der aktuellen Systemzeit.</returns>
	public static double Aphelion()
	{
		// Ereigniszeit bestimmen
		double jd = DateTime.Now.ToJdn();
		return MJupiter.Aphelion(jd);
	}

	// MJupiter.Aphelion(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch das Aphel nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch das Aphel nach der julianischen Tageszahl.</returns>
	public static double Aphelion(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor(0.08430 * (y - 2011.20)) - 0.5;
		double j = 0.0;

		// Berechnungsschleife
		while(j <= jd)
		{
			// Tageszahl berechnen
			k += 1.0;
			j  = MMath.Polynome(k, 2455636.936, 4332.897065, 0.0001367);
		}
		return j;
	}

	// MJupiter.Conjunction()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die obere Konjunktion mit der Sonne nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die obere Konjunktion mit der Sonne nach der aktuellen Systemzeit.</returns>
	public static double Conjunction()
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double jd = DateTime.Now.ToJdn();
		return MJupiter.Conjunction(jd);
	}

	// MJupiter.Conjuction(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die Konjunktion mit der Sonne nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die Konjunktion mit der Sonne nach der julianischen Tageszahl.</returns>
	public static double Conjunction(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor((365.2425 * y + 1721060.0 - 2451671.186) / 398.884046) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while (j <= jd)
		{
			// Näherung berechnen
			k += 1.0;
			j  = 2451671.186 + k * 398.884046;

			// Hilfsfelder berechnen
			double m = MMod.Mod(MMath.ToRad(121.8980 + k * 33.140229), MMath.Pi2);
			double t = (j - MCalendar.Jdn20000101) / 36525.0;
			double a = MMod.Mod(MMath.ToRad(82.74 + 40.76 * t), MMath.Pi2);
			double h;

			// Korrektur berechnen und anwenden
			h  = MMath.Polynome(t,  0.1027,  0.0002, -0.00009);
			h += MMath.Polynome(t, -2.2637,  0.0163, -0.00003) * MMath.Sin(      m);
			h += MMath.Polynome(t, -6.1540, -0.0210,  0.00008) * MMath.Cos(      m);
			h += MMath.Polynome(t, -0.2021, -0.0017,  0.00001) * MMath.Sin(2.0 * m);
			h += MMath.Polynome(t,  0.1319, -0.0008          ) * MMath.Cos(2.0 * m);
			h +=                    0.0086                     * MMath.Sin(3.0 * m);
			h += MMath.Polynome(t,  0.0087,  0.0002          ) * MMath.Cos(3.0 * m);
			h += MMath.Polynome(t,  0.0000,  0.0144, -0.00008) * MMath.Sin(      m);
			h += MMath.Polynome(t,  0.3642, -0.0019, -0.00029) * MMath.Cos(      m);
			j += h;
		}
		return j;
	}

	// MJupiter.Eccentricity()
	/// <summary>
	/// Liefert die Exzentrizität der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Exzentrizität der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	public static double Eccentricity()
	{
		// Lokale Felder einrichten und Exzentrizität berechnen
		double jd = DateTime.Now.ToJdn();
		return MJupiter.Eccentricity(jd);
	}

	// MJupiter.Eccentricity(double)
	/// <summary>
	/// Liefert die Exzentrizität der mittleren Planetenbahn zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Exzentrizität der mittleren Planetenbahn zur julianischen Tageszahl.</returns>
	public static double Eccentricity(double jd)
	{
		// Lokale Felder einrichten und Exzentrizität berechnen
		double t = (jd - MCalendar.Jdn20000101) / 36525.0;
		return MMath.Polynome(t, 0.04849793, 0.000163225, -0.0000004714, -0.00000000201);
	}

	// MJupiter.Inclination()
	/// <summary>
	/// Liefert die Neigung der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Neigung der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double Inclination()
	{
		// Lokale Felder einrichten und Bahnneigung berechnen
		double jd = DateTime.Now.ToJdn();
		return MJupiter.Inclination(jd);
	}

	// MJupiter.Inclination(double)
	/// <summary>
	/// Liefert die Neigung der mittleren Planetenbahn zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Neigung der mittleren Planetenbahn zur julianischen Tageszahl.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double Inclination(double jd)
	{
		// Lokale Felder einrichten und Bahnneigung berechnen
		double t = (jd - MCalendar.Jdn20000101) / 36525.0;
		return MMath.Polynome(t, 1.303267, -0.0054965, 0.00000466, -0.000000002);
	}

	// MJupiter.Latitude(EPrecision)          » MJupiter.Latitude.cs
	// MJupiter.Latitude(EPrecision, double)  » MJupiter.Latitude.cs
	// MJupiter.Longitude(EPrecision)         » MJupiter.Longitude.cs
	// MJupiter.Longitude(EPrecision, double) » MJupiter.Longitude.cs

	// MJupiter.LongitudeOfAscendingNode()
	/// <summary>
	/// Liefert die Länge des aufsteigenden Knotens der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Länge des aufsteigenden Knotens der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double LongitudeOfAscendingNode()
	{
		// Lokale Felder einrichten und Länge berechnen
		double jd = DateTime.Now.ToJdn();
		return MJupiter.LongitudeOfAscendingNode(jd);
	}

	// MJupiter.LongitudeOfAscendingNode(double)
	/// <summary>
	/// Liefert die Länge des aufsteigenden Knotens der mittleren Planetenbahn zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Länge des aufsteigenden Knotens der mittleren Planetenbahn zur julianischen Tageszahl.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double LongitudeOfAscendingNode(double jd)
	{
		// Lokale Felder einrichten und Länge berechnen
		double t = (jd - MCalendar.Jdn20000101) / 36525.0;
		return MMath.Polynome(t, 100.464407, 1.0209774, 0.00040315, 0.000000404);
	}

	// MJupiter.LongitudeOfPerihelion()
	/// <summary>
	/// Liefert die Länge des Perihels der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Länge des Perihels der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double LongitudeOfPerihelion()
	{
		// Lokale Felder einrichten und Länge berechnen
		double jd = DateTime.Now.ToJdn();
		return MJupiter.LongitudeOfPerihelion(jd);
	}

	// MJupiter.LongitudeOfPerihelion(double)
	/// <summary>
	/// Liefert die Länge des Perihels der mittleren Planetenbahn zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Länge des Perihels der mittleren Planetenbahn zur julianischen Tageszahl.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double LongitudeOfPerihelion(double jd)
	{
		// Lokale Felder berechnen und Länge berechnen
		double t = (jd - MCalendar.Jdn20000101) / 36525.0;
		return MMath.Polynome(t, 14.331207, 1.6126352, 0.00103042, -0.000004464);
	}

	// MJupiter.MeanAnomaly()
	/// <summary>
	/// Liefert die mittlere Anomalie der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Mittlere Anomalie der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double MeanAnomaly()
	{
		// Lokale Felder einrichten und Anomalie berechnen
		double jd = DateTime.Now.ToJdn();
		return MJupiter.MeanAnomaly(jd);
	}

	// MJupiter.MeanAnomaly(double)
	/// <summary>
	/// Liefert die mittlere Anomalie der mittleren Planetenbahn zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Mittlere Anomalie der mittleren Planetenbahn zur julianischen Tageszahl.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double MeanAnomaly(double jd){ return MMod.Mod(MJupiter.MeanLongitude(jd) + MJupiter.LongitudeOfPerihelion(jd), 360.0); }

	// MJupiter.MeanLongitude()
	/// <summary>
	/// Liefert die mittlere Länge der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Mittlere Länge der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double MeanLongitude()
	{
		// Lokale Felder einrichten und Länge berechnen
		double jd = DateTime.Now.ToJdn();
		return MJupiter.MeanLongitude(jd);
	}

	// MJupiter.MeanLongitude(double)
	/// <summary>
	/// Liefert die mittlere Länge der mittleren Planetenbahn zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianischen Tageszahl.</param>
	/// <returns>Mittlere Länge der mittleren Planetenbahn zur julianischen Tageszahl.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double MeanLongitude(double jd)
	{
		// Lokale Felder einrichten und Länge berechnen
		double t = (jd - MCalendar.Jdn20000101) / 36525.0;
		return MMod.Mod(MMath.Polynome(t, 34.351519, 3036.3027748, 0.00023330, 0.000000037), 360.0);
	}

	// MJupiter.Opposition()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die Opposition mit der Sonne nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die Opposition mit der Sonne nach der aktuellen Systemzeit.</returns>
	public static double Opposition()
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double jd = DateTime.Now.ToJdn();
		return MJupiter.Opposition(jd);
	}

	// MJupiter.Opposition(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die Opposition mit der Sonne nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die Opposition mit der Sonne nach der aktuellen Systemzeit.</returns>
	public static double Opposition(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor((365.2425 * y + 1721060.0 - 2451870.628) / 398.884046) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while (j <= jd)
		{
			// Näherung berechnen
			k += 1.0;
			j  = 2451870.628 + k * 398.884046;

			// Hilfsfelder berechnen
			double m = MMod.Mod(MMath.ToRad(318.4681 + k * 33.140229), MMath.Pi2);
			double t = (j - MCalendar.Jdn20000101) / 36525.0;
			double a = MMod.Mod(MMath.ToRad(82.74 + 40.76 * t), MMath.Pi2);
			double h;

			// Korrektur berechnen und anwenden
			h  = MMath.Polynome(t, -0.1029,  0.0000, -0.00009);
			h += MMath.Polynome(t, -1.9658, -0.0056,  0.00007) * MMath.Sin(      m);
			h += MMath.Polynome(t,  6.1537,  0.0210, -0.00006) * MMath.Cos(      m);
			h += MMath.Polynome(t, -0.2081, -0.0013          ) * MMath.Sin(2.0 * m);
			h += MMath.Polynome(t, -0.1116, -0.0010          ) * MMath.Cos(2.0 * m);
			h += MMath.Polynome(t,  0.0074,  0.0001          ) * MMath.Sin(3.0 * m);
			h += MMath.Polynome(t, -0.0097, -0.0001          ) * MMath.Cos(3.0 * m);
			h += MMath.Polynome(t,  0.0000,  0.0144, -0.00008) * MMath.Sin(      a);
			h += MMath.Polynome(t,  0.3642, -0.0019, -0.00029) * MMath.Cos(      a);
			j += h;
		}
		return j;
	}

	// MJupiter.Perihelion()
	/// <summary>
	/// Liefert die julianische Tageszahl der nächsten Durchgangs durch das Perihel nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl der nächsten Durchgangs durch das Perihel nach der aktuellen Systemzeit.</returns>
	public static double Perihelion()
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double jd = DateTime.Now.ToJdn();
		return MJupiter.Perihelion(jd);
	}

	// MJupiter.Perihelion(double)
	/// <summary>
	/// Liefert die julianische Tageszahl der nächsten Durchgangs durch das Perihel nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl der nächsten Durchgangs durch das Perihel nach der julianischen Tageszahl.</returns>
	public static double Perihelion(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor(0.08430 * (y - 2011.20)) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while(j <= jd)
		{
			// Tageszahl berechnen
			k += 1.0;
			j  = MMath.Polynome(k, 2455636.936, 4332.897065, 0.0001367);
		}
		return j;
	}

	// MJupiter.PositionEcliptical(EPrecision)
	/// <summary>
	/// Liefert die heliozentrisch-ekliptikale Position zur aktuellen Systemzeit.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <returns>Heliozentrisch-ekliptikale Position zur aktuellen Systemzeit.</returns>
	public static CPolar PositionEcliptical(EPrecision value)
	{
		// Lokale Felder einrichten und Position berechnen
		double jd = DateTime.Now.ToJdn();
		return MJupiter.PositionEcliptical(value, jd);
	}

	// MJupiter.PositionEcliptical(EPrecision, double)
	/// <summary>
	/// Liefert die heliozentrisch-ekliptikale Position zur julianischen Tageszahl.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Heliozentrisch-ekliptikale Position zur julianischen Tageszahl.</returns>
	public static CPolar PositionEcliptical(EPrecision value, double jd)
	{
		// Lokale Felder einrichten
		CPolar rtn = new CPolar();
		rtn.Latitude  = MJupiter.Latitude (value, jd);
		rtn.Longitude = MJupiter.Longitude(value, jd);
		rtn.Radius    = MJupiter.Radius   (value, jd);
		return rtn;
	}

	// MJupiter.PositionEquatorial()
	/// <summary>
	/// Liefert die (scheinbare) geozentrisch-äquatoriale Position zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Geozentrisch-äquatoriale Position zur aktuellen Systemzeit.</returns>
	public static CPolar PositionEquatorial()
	{
		// Lokale Felder einrichten und Position berechnen
		double jd = DateTime.Now.ToJdn();
		return MJupiter.PositionEquatorial(jd);
	}

	// MJupiter.PositionEquatorial(double)
	/// <summary>
	/// Liefert die (scheinbare) geozentrisch-äquatoriale Position zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Geozentrisch-äquatoriale Position zur julianischen Tageszahl.</returns>
	public static CPolar PositionEquatorial(double jd)
	{
		// Lokale Felder einrichten
		double bG  = 0.0; // Geozentrische Breite.
		double bH  = 0.0; // Heliozentrische Breite.
		double jdn = jd;  // Julianische Tageszahl.
		double lG  = 0.0; // Geozentrische Länge.
		double lH  = 0.0; // Heliozentrische Länge.
		double rG  = 0.0; // Geozentrischer Radius.
		double rH  = 0.0; // Heliozentrischer Radius.
		double tau = 0.0; // Lichtlaufzeit.
		double tmp = 0.0; // Temporärwert.

		// ------------- //
		// Lichtlaufzeit //
		// ------------- //

		// Lichtlaufzeit iterieren
		while(true)
		{
			// Heliozentrische Position bestimmen
			bH = MJupiter.Latitude (EPrecision.High, jdn);
			lH = MJupiter.Longitude(EPrecision.High, jdn);
			rH = MJupiter.Radius   (EPrecision.High, jdn);

			// Geozentrische Position berechnen und Abbruchbedinungen verarbeiten
			tmp = MEphemerides.ToGeocentric(lH, bH, rH, jdn, ref lG, ref bG, ref rG, EPrecision.High);
			if(MMath.Abs(tau - tmp) < 0.00005) break; // Ausreichende Genauigkeit sicherstellen
			if(tau != 0.0 && tmp >= tau)       break; // Abbruch bei Schwingung sicherstellen

			// Wert anwenden und nächsten Iterationsschritt vorbereiten
			jdn += tmp;
			tau  = tmp;
		}

		// ----------------------- //
		// Aberration und Nutation //
		// ----------------------- //

		// Aberation und Nutation anwenden
		MEphemerides.AberrationEcliptical(ref lG, ref bG, jdn);
		lG += MEphemerides.NutationInLongitude(jdn);
		bG += MEphemerides.NutationInObliquity(jdn);

		// ------------------------- //
		// Koordinatentransformation //
		// ------------------------- //

		// Äquatoriale Position berechnen und anwenden
		double a = MEphemerides.ToAlpha(lG, bG, EObliquity.True, jdn);
		double d = MEphemerides.ToDelta(lG, bG, EObliquity.True, jdn);
		return new CPolar(a, d, rG);
	}

	// MJupiter.Radius(EPrecision)         » MJupiter.Radius.cs
	// MJupiter.Radius(EPrecision, double) » MJupiter.Radius.cs

	// MJupiter.RegressionEnd()
	/// <summary>
	/// Liefert die julianische Tageszahl des Rückläufigkeitsendes nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des Rückläufigkeitsendes nach der aktuellen Systemzeit.</returns>
	public static double RegressionEnd()
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double jd = DateTime.Now.ToJdn();
		return MJupiter.RegressionEnd(jd);
	}

	// MJupiter.RegressionEnd(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des Rückläufigkeitsendes nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des Rückläufigkeitsendes nach der julianischen Tageszahl.</returns>
	public static double RegressionEnd(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor((365.2425 * y + 1721060.0 - 2451870.628) / 398.884046) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while (j <= jd)
		{
			// Näherung berechnen
			k += 1.0;
			j  = 2451870.628 + k * 398.884046;

			// Hilfsfelder berechnen
			double m = MMod.Mod(MMath.ToRad(318.4681 + k * 33.140229), MMath.Pi2);
			double t = (j - MCalendar.Jdn20000101) / 36525.0;
			double a = MMod.Mod(MMath.ToRad(82.74 + 40.76 * t), MMath.Pi2);
			double h;

			// Korrektur berechnen und anwenden
			h  = MMath.Polynome(t, 60.3023,  0.0002, -0.00009);
			h += MMath.Polynome(t,  0.3506, -0.0034,  0.00004) * MMath.Sin(      m);
			h += MMath.Polynome(t,  5.3635,  0.0247, -0.00007) * MMath.Cos(      m);
			h += MMath.Polynome(t, -0.1872, -0.0016          ) * MMath.Sin(2.0 * m);
			h += MMath.Polynome(t, -0.0037, -0.0005          ) * MMath.Cos(2.0 * m);
			h += MMath.Polynome(t,  0.0012,  0.0001          ) * MMath.Sin(3.0 * m);
			h += MMath.Polynome(t, -0.0096, -0.0001          ) * MMath.Cos(3.0 * m);
			h += MMath.Polynome(t,  0.0000,  0.0144, -0.00008) * MMath.Sin(      a);
			h += MMath.Polynome(t,  0.3642, -0.0019, -0.00029) * MMath.Cos(      a);
			j += h;
		}
		return j;
	}

	// MJupiter.RegressionStart()
	/// <summary>
	/// Liefert die julianische Tageszahl der Rückläufigkeitsanfangs nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl der Rückläufigkeitsanfangs nach der aktuellen Systemzeit.</returns>
	public static double RegressionStart()
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double jd = DateTime.Now.ToJdn();
		return MJupiter.RegressionStart(jd);
	}

	// MJupiter.RegressionStart(double)
	/// <summary>
	/// Liefert die julianische Tageszahl der Rückläufigkeitsanfangs nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl der Rückläufigkeitsanfangs nach der julianischen Tageszahl.</returns>
	public static double RegressionStart(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor((365.2425 * y + 1721060.0 - 2451870.628) / 398.884046) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while (j <= jd)
		{
			// Näherung berechnen
			k += 1.0;
			j  = 2451870.628 + k * 398.884046;

			// Hilfsfelder berechnen
			double m = MMod.Mod(MMath.ToRad(318.4681 + k * 33.140229), MMath.Pi2);
			double t = (j - MCalendar.Jdn20000101) / 36525.0;
			double a = MMod.Mod(MMath.ToRad(82.74 + 40.76 * t), MMath.Pi2);
			double h;

			// Korrektur berechnen und anwenden
			h  = MMath.Polynome(t, -60.3670, -0.0001, -0.00009);
			h += MMath.Polynome(t,  -2.3144, -0.0124,  0.00007) * MMath.Sin(      m);
			h += MMath.Polynome(t,   6.7439,  0.0166, -0.00006) * MMath.Cos(      m);
			h += MMath.Polynome(t,  -0.2259, -0.0010          ) * MMath.Sin(2.0 * m);
			h += MMath.Polynome(t,  -0.1497, -0.0014          ) * MMath.Cos(2.0 * m);
			h += MMath.Polynome(t,   0.0105,  0.0001          ) * MMath.Sin(3.0 * m);
			h +=                    -0.0098                     * MMath.Cos(3.0 * m);
			h += MMath.Polynome(t,   0.0000,  0.0144, -0.00008) * MMath.Sin(      a);
			h += MMath.Polynome(t,   0.3642, -0.0019, -0.00029) * MMath.Cos(      a);
			j += h;
		}
		return j;
	}

	// MJupiter.Rise(CPolar, ref double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Aufgangs am geographischen Ort und zur aktuellen Systemzeit und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Aufgangs.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Rise(CPolar position, ref double jdEvent)
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double jd  = DateTime.Now.ToJdn();
		double azm = 0.0;
		return MJupiter.Rise(position.Longitude, position.Latitude, ref jdEvent, jd, ref azm);
	}

	// MJupiter.Rise(CPolar, ref double, double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Aufgangs am geographischen Ort und zur julianischen Tageszahl und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Aufgangs.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Rise(CPolar position, ref double jdEvent, double jd)
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double azm = 0.0;
		return MJupiter.Rise(position.Longitude, position.Latitude, ref jdEvent, jd, ref azm);
	}

	// MJupiter.Rise(CPolar, ref double, double, ref double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Aufgangs und die Morgenweite am geographischen Ort und zur aktuekllen Systemzeit und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Aufgangs.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="azimuth">Morgenweite.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Rise(CPolar position, ref double jdEvent, double jd, ref double azimuth){ return MJupiter.Rise(position.Longitude, position.Latitude, ref jdEvent, jd, ref azimuth); }

	// MJupiter.Rise(double, double, ref double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Aufgangs am geographischen Ort und zur aktuellen Systemzeit und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Aufgangs.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Rise(double lambda, double phi, ref double jdEvent)
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double jd  = DateTime.Now.ToJdn();
		double azm = 0.0;
		return MJupiter.Rise(lambda, phi, ref jdEvent, jd, ref azm);
	}

	// MJupiter.Rise(double, double, ref double, double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Aufgangs am geographischen Ort und zur julianischen Tageszahl und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Aufgangs.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Rise(double lambda, double phi, ref double jdEvent, double jd)
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double azm = 0.0;
		return MJupiter.Rise(lambda, phi, ref jdEvent, jd, ref azm);
	}

	// MJupiter.Rise(double, double, ref double, double, ref double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Aufgangs und die Morgenweite am geographischen Ort und zur julianischen Tageszahl und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Aufgangs.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="azimuth">Morgenweite.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Rise(double lambda, double phi, ref double jdEvent, double jd, ref double azimuth)
	{
		// Lokale Felder einrichten
		double jdn  = MMath.Floor(jd - 0.5) + 0.5;        // Tageszahl um Mitternacht
		double l    = 0.0;                                // Geozentrische Länge
		double b    = 0.0;                                // Geozentrische Breite
		double a    = 0.0;                                // Rektaszension
		double d    = 0.0;                                // Deklination
		double dm   = 1.0;                                // Korrekturglied
		double h    = 0.0;                                //
		double h0   = MEphemerides.GeocentricHeight_Star; // Refraktionswinkel
		double H    = 0.0;                                //
		double sinP = MMath.Sin(phi);                     // Breitensinus
		double cosP = MMath.Cos(phi);                     // Breitencosinus

		// Position für nachfolgenden Tag berechnen
		l = MJupiter.Longitude(EPrecision.Low, jdn + 1.0);
		b = MJupiter.Latitude (EPrecision.Low, jdn + 1.0);
		double aP = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jdn + 1.0);
		double dP = MEphemerides.ToDelta(l, b, EObliquity.Mean, jdn + 1.0);

		// Position für gegebenen Tag berechnen
		l = MJupiter.Longitude(EPrecision.Low, jdn);
		b = MJupiter.Latitude (EPrecision.Low, jdn);
		double a0 = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jdn);
		if(MMath.Abs(aP - a0) > 1.0) a0 += MMath.Sgn(aP - a0) * MMath.Pi2;
		double d0 = MEphemerides.ToDelta(l, b, EObliquity.Mean, jdn);

		// Position für vorhergehenden Tag berechnen
		l = MJupiter.Longitude(EPrecision.Low, jdn - 1.0);
		b = MJupiter.Latitude (EPrecision.Low, jdn - 1.0);
		double aM = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jdn - 1.0);
		if(MMath.Abs(a0 - aM) > 1.0) aM += MMath.Sgn(a0 - aM) * MMath.Pi2;
		double dM = MEphemerides.ToDelta(l, b, EObliquity.Mean, jdn - 1.0);

		// Stundenwinkel berechnen und prüfen
		double cosH = (MMath.Sin(h0) - sinP * MMath.Sin(dP)) / (cosP * MMath.Cos(dP));
		if(MMath.Abs(cosH) > 1.0) return cosH < 1.0 ? EEventType.AlwaysAboveHorizon : EEventType.AlwaysBeneathHorizon;
		H = MMath.ArcCos(cosH);

		// ------------------- //
		// Ereigniszeit nähern //
		// ------------------- //

		// Sternzeit und Stundenwinkel zum gegebenen Zeitpunkt bestimmen
		double t0 = MEphemerides.Gmst(jdn);
		double m = MMath.Div((a0 + lambda - t0 - H) / MMath.Pi2);
		if(m < 0.0) m += 1.0;

		// Ereigniszeit iterieren
		while(MMath.Abs(dm) >= 0.0001)
		{
			// Iteration durchführen und nächsten Iterationsschritt vorbereiten
			a  = MMath.Bessel(m, aM, a0, aP);
			d  = MMath.Bessel(m, dM, d0, dP);
			H  = t0 + 6.300388093 * m - lambda - a;
			h  = MMath.ArcSin(sinP * MMath.Sin(d) + cosP * MMath.Cos(d) * MMath.Cos(H));
			dm = (h - h0) / (MMath.Pi2 * MMath.Cos(d) * cosP * MMath.Sin(H));
			m += dm;
		}

		// Iteration anwenden, Azimut berechnen und Rückgabewert setzen
		jdEvent = jd + m;
		azimuth = MEphemerides.ToAzimuth(H, d, phi);
		return EEventType.Normal;
	}

	// MJupiter.SemimajorAxis()
	/// <summary>
	/// Liefert die große Halbachse der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Große Halbachse der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	public static double SemimajorAxis()
	{
		// Lokale Felder einrichten und große Halbachse bestimmen
		double jd = DateTime.Now.ToJdn();
		return MJupiter.SemimajorAxis(jd);
	}

	// MJupiter.SemimajorAxis(double)
	/// <summary>
	/// Liefert die große Halbachse der mittleren Planetenbahn zur julianischen Tageszahl.
	/// </summary>
	/// <returns>Große Halbachse der mittleren Planetenbahn zur julianischen Tageszahl.</returns>
	public static double SemimajorAxis(double jd)
	{
		// Lokale Felder einrichten und große Halbachse berechnen
		double t = (jd - MCalendar.Jdn20000101) / 36525.0;
		return MMath.Polynome(t, 5.202603209, 0.0000001913);
	}

	// MJupiter.Set(CPolar, ref double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Untergangs am geographischen Ort und zur aktuellen Systemzeit und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="position">Geographisches Position.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Untergangs.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Set(CPolar position, ref double jdEvent)
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double jd  = DateTime.Now.ToJdn();
		double azm = 0.0;
		return MJupiter.Set(position.Longitude, position.Latitude, ref jdEvent, jd, ref azm);
	}

	// MJupiter.Set(CPolar, ref double, double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Untergangs am geographischen Ort und zur julianischen Tageszahl und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="position">Geographisches Position.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Untergangs.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Set(CPolar position, ref double jdEvent, double jd)
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double azm = 0.0;
		return MJupiter.Set(position.Longitude, position.Latitude, ref jdEvent, jd, ref azm);
	}

	// MJupiter.Set(CPolar, ref double, double, ref double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Untergangs und die Abendweite am geographischen Ort und zur julianischen Tageszahl und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Untergangs.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="azimuth">Abendweite.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Set(CPolar position, ref double jdEvent, double jd, ref double azimuth){ return MJupiter.Set(position.Longitude, position.Latitude, ref jdEvent, jd, ref azimuth); }

	// MJupiter.Set(double, double, ref double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Untergangs am geographischen Ort und zur aktuellen Systemzeit und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Untergangs.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Set(double lambda, double phi, ref double jdEvent)
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double jd  = DateTime.Now.ToJdn();
		double azm = 0.0;
		return MJupiter.Set(lambda, phi, ref jdEvent, jd, ref azm);
	}

	// MJupiter.Set(double, double, ref double, double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Untergangs am geographischen Ort und zur julianischen Tageszahl und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Untergangs.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Set(double lambda, double phi, ref double jdEvent, double jd)
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double azm = 0.0;
		return MJupiter.Set(lambda, phi, ref jdEvent, jd, ref azm);
	}

	// MJupiter.Set(double, double, ref double, double, ref double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Untergangs und die Abendweite am geographischen Ort und zur julianischen Tageszahl und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Untergangs.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="azimuth">Abendweite.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Set(double lambda, double phi, ref double jdEvent, double jd, ref double azimuth)
	{
		// Lokale Felder einrichten
		double jdn  = MMath.Floor(jd - 0.5) + 0.5;        // Tageszahl um Mitternacht
		double l    = 0.0;                                // Geozentrische Länge
		double b    = 0.0;                                // Geozentrische Breite
		double a    = 0.0;                                // Rektaszension
		double d    = 0.0;                                // Deklination
		double dm   = 1.0;                                // Korrekturglied
		double h    = 0.0;                                //
		double h0   = MEphemerides.GeocentricHeight_Star; // Refraktionswinkel
		double H    = 0.0;                                //
		double sinP = MMath.Sin(phi);                     // Breitensinus
		double cosP = MMath.Cos(phi);                     // Breitencosinus

		// Position für nachfolgenden Tag berechnen
		l = MJupiter.Longitude(EPrecision.Low, jdn + 1.0);
		b = MJupiter.Latitude (EPrecision.Low, jdn + 1.0);
		double aP = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jd + 1.0);
		double dP = MEphemerides.ToDelta(l, b, EObliquity.Mean, jd + 1.0);

		// Position für gegebenen Tag berechnen
		l = MJupiter.Longitude(EPrecision.Low, jdn);
		b = MJupiter.Latitude (EPrecision.Low, jdn);
		double a0 = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jdn);
		if(MMath.Abs(aP - a0) > 1.0) a0 += MMath.Sgn(aP - a0) * MMath.Pi2;
		double d0 = MEphemerides.ToDelta(l, b, EObliquity.Mean, jdn);

		// Position für vorhergehenden Tag berechnen
		l = MJupiter.Longitude(EPrecision.Low, jdn - 1.0);
		b = MJupiter.Latitude (EPrecision.Low, jdn - 1.0);
		double aM = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jd - 1.0);
		if(MMath.Abs(a0 - aM) > 1.0) aM += MMath.Sgn(a0 - aM) * MMath.Pi2;
		double dM = MEphemerides.ToDelta(l, b, EObliquity.Mean, jd - 1.0);

		// Stundenwinkel berechnen und prüfen
		double cosH = (MMath.Sin(h0) - sinP * MMath.Sin(dP)) / (cosP * MMath.Cos(dP));
		if(MMath.Abs(cosH) > 1.0) return cosH < 1.0 ? EEventType.AlwaysAboveHorizon : EEventType.AlwaysBeneathHorizon;
		H = MMath.ArcCos(cosH);

		// ------------------- //
		// Ereigniszeit nähern //
		// ------------------- //

		// Sternzeit und Stundenwinkel zum gegebenen Zeitpunkt bestimmen
		double t0 = MEphemerides.Gmst(jdn);
		double m = MMath.Div((a0 + lambda - t0 + H) / MMath.Pi2);
		if(m < 0.0) m += 1.0;

		// Ereigniszeit iterieren
		while(MMath.Abs(dm) >= 0.0001)
		{
			// Iteration durchführen und nächsten Iterationsschritt vorbereiten
			a  = MMath.Bessel(m, aM, a0, aP);
			d  = MMath.Bessel(m, dM, d0, dP);
			H  = t0 + 6.300388093 * m - lambda - a;
			h  = MMath.ArcSin(sinP * MMath.Sin(d) + cosP * MMath.Cos(d) * MMath.Cos(H));
			dm = (h - h0) / (MMath.Pi2 * MMath.Cos(d) * cosP * MMath.Sin(H));
			m += dm;
		}

		// Iteration anwenden, Azimut berechnen und Rückgabewert setzen
		jdEvent = jd + m;
		azimuth = MEphemerides.ToAzimuth(H, d, phi);
		return EEventType.Normal;
	}

	// MJupiter.SynodicPeriod()
	/// <summary>
	/// Liefert die mittlere synodische Periode zur mittleren Planetenbahn.
	/// </summary>
	/// <returns>Mittlere synodische Periode zur mittleren Planetenbahn.</returns>
	public static double SynodicPeriod(){ return 398.884047; }

	// MJupiter.Transit(CPolar)
	/// <summary>
	/// Liefert die julianische Tageszahl des Meridiandurchgangs am geographischen Ort und zur aktuellen Systemzeit.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <returns>Julianische Tageszahl des Meridiandurchgangs am geographischen Ort und zur aktuellen Systemzeit.</returns>
	public static double Transit(CPolar position)
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double h  = 0.0;
		double jd = DateTime.Now.ToJdn();
		return MJupiter.Transit(position.Longitude, position.Latitude, jd, ref h);
	}

	// MJupiter.Transit(CPolar, double)
	/// <summary>
	/// Liefert die julianische Tageszahl des Meridiandurchgangs am geographischen Ort und zur julianischen Tageszahl.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des Meridiandurchgangs am geographischen Ort und zur julianischen Tageszahl.</returns>
	public static double Transit(CPolar position, double jd)
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double h = 0.0;
		return MJupiter.Transit(position.Longitude, position.Latitude, jd, ref h);
	}

	// MJupiter.Transit(CPolar, double, ref double)
	/// <summary>
	/// Setzt die Höhe und liefert die julianische Tageszahl des Meridiandurchgangs am geographischen Ort und zur julianischen Tageszahl.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="height">Höhe.</param>
	/// <returns>Julianische Tageszahl des Meridiandurchgangs am geographischen Ort und zur julianischen Tageszahl.</returns>
	public static double Transit(CPolar position, double jd, ref double height){ return MJupiter.Transit(position.Longitude, position.Latitude, jd, ref height); }

	// MJupiter.Transit(double, double)
	/// <summary>
	/// Liefert die julianische Tageszahl des Meridiandurchgangs am geographischen Ort und zur aktuellen Systemzeit.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <returns>Liefert die julianische Tageszahl des Meridiandurchgangs am geographischen Ort und zur aktuellen Systemzeit.</returns>
	public static double Transit(double lambda, double phi)
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double h  = 0.0;
		double jd = DateTime.Now.ToJdn();
		return MJupiter.Transit(lambda, phi, jd, ref h);
	}

	// MJupiter.Transit(double, double, double)
	/// <summary>
	/// Liefert die julianische Tageszahl des Meridiandurchgangs am geographischen Ort und zur julianischen Tageszahl.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des Meridiandurchgangs am geographischen Ort und zur julianischen Tageszahl.</returns>
	public static double Transit(double lambda, double phi, double jd)
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double h = 0.0;
		return MJupiter.Transit(lambda, phi, jd, ref h);
	}

	// MJupiter.Transit(double, double, double, ref double)
	/// <summary>
	/// Setzt die Höhe und liefert die julianische Tageszahl des Meridiandurchgangs am geographischen Ort und zur julianischen Tageszahl.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="height">Höhe.</param>
	/// <returns>Julianische Tageszahl des Meridiandurchgangs am geographischen Ort und zur julianischen Tageszahl.</returns>
	public static double Transit(double lambda, double phi, double jd, ref double height)
	{
		// Lokale Felder einrichten
		double jdn = MMath.Floor(jd - 0.5) + 0.5; // Tageszahl um Mitternacht
		double l   = 0.0;                         // Geozentrische Länge
		double b   = 0.0;                         // Geozentrische Breite
		double a   = 0.0;                         // Rektaszension
		double d   = 0.0;                         // Deklination
		double dm  = 1.0;                         // Korrekturglied

		// Position für nachfolgenden Tag berechnen
		l = MJupiter.Longitude(EPrecision.Low, jdn + 1.0);
		b = MJupiter.Latitude (EPrecision.Low, jdn + 1.0);
		double aP = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jdn + 1.0);
		double dP = MEphemerides.ToDelta(l, b, EObliquity.Mean, jdn + 1.0);

		// Position für gegebenen Tag berechnen
		l = MJupiter.Longitude(EPrecision.Low, jdn);
		b = MJupiter.Latitude (EPrecision.Low, jdn);
		double a0 = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jdn);
		if(MMath.Abs(aP - a0) > 1.0) a0 += MMath.Sgn(aP - a0) * MMath.Pi2;
		double d0 = MEphemerides.ToDelta(l, b, EObliquity.Mean, jdn);

		// Position für vorhergehenden Tag berechnen
		l = MJupiter.Longitude(EPrecision.Low, jdn - 1.0);
		b = MJupiter.Latitude (EPrecision.Low, jdn - 1.0);
		double aM = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jdn - 1.0);
		if(MMath.Abs(a0 - aM) > 1.0) aM += MMath.Sgn(a0 - aM) * MMath.Pi2;
		double dM = MEphemerides.ToDelta(l, b, EObliquity.Mean, jdn - 1.0);

		// ------------------- //
		// Ereigniszeit nähern //
		// ------------------- //

		// Sternzeit und Stundenwinkel zum gegebenen Zeitpunkt bestimmen
		double t0 = MEphemerides.Gmst(jdn);
		double m = MMath.Div((aP + lambda - t0) / MMath.Pi2);
		if(m < 0.0) m += 1.0;

		// Ereigniszeit iterieren
		while(MMath.Abs(dm) >= 0.0001)
		{
			// Iteration durchführen und nächsten Iterationsschritt vorbereiten
			a  = MMath.Bessel(m, aM, a0, aP);
			dm = MMath.Div((a + lambda - t0 - 6.300388093 * m) / MMath.Pi2);
			if(MMath.Abs(dm) > 0.5) dm -= MMath.Sgn(dm);
			m += dm;
		}

		// Iteration anwenden, Höhe berechnen und Rückgabewert setzen
		d = MMath.Bessel(m, dM, d0, dP);
		height = MEphemerides.ToHeight(0.0, d, phi);
		return jd + m;
	}

	// MJupiter.TropicalPeriod()
	/// <summary>
	/// Liefert die mittlere tropische Periode zur mittleren Planetenbahn.
	/// </summary>
	/// <returns>Mittlere tropische Periode zur mittleren Planetenbahn.</returns>
	public static double TropicalPeriod(){ return 4330.595764; }
}
