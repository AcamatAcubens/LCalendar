using Acamat.LCore;
using Acamat.LMath;
using Acamat.LMath.Geometry;
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
	public static double Aphelion(){ return MJupiter.Aphelion(DateTime.Now.ToJdn()); }

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
	public static double Conjunction(){ return MJupiter.Conjunction(DateTime.Now.ToJdn()); }

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
			double m = (MMath.ToRad(121.8980 + k * 33.140229)).Mod(MMath.Pi2);
			double t = (j - MCalendar.Jdn20000101) / 36525.0;
			double a = (MMath.ToRad(82.74 + 40.76 * t)).Mod(MMath.Pi2);
			double h;

			// Korrektur berechnen und anwenden
			h  = MMath.Polynome(t,  0.1027,  0.0002, -0.00009);
			h += MMath.Polynome(t, -2.2637,  0.0163, -0.00003) * MMath.Sin(      m);
			h += MMath.Polynome(t, -6.1540, -0.0210,  0.00008) * (      m).Cos();
			h += MMath.Polynome(t, -0.2021, -0.0017,  0.00001) * MMath.Sin(2.0 * m);
			h += MMath.Polynome(t,  0.1319, -0.0008          ) * (2.0 * m).Cos();
			h +=                    0.0086                     * MMath.Sin(3.0 * m);
			h += MMath.Polynome(t,  0.0087,  0.0002          ) * (3.0 * m).Cos();
			h += MMath.Polynome(t,  0.0000,  0.0144, -0.00008) * MMath.Sin(      m);
			h += MMath.Polynome(t,  0.3642, -0.0019, -0.00029) * (      m).Cos();
			j += h;
		}
		return j;
	}

	// MJupiter.Eccentricity()
	/// <summary>
	/// Liefert die Exzentrizität der mittleren Planetenbahn zum aktuellen Systemdatum.
	/// </summary>
	/// <returns>Exzentrizität der mittleren Planetenbahn zum aktuellen Systemdatum.</returns>
	public static double Eccentricity(){ return MJupiter.Eccentricity(DateTime.Now.ToJdn()); }

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
	/// Liefert die Neigung der mittleren Planetenbahn zum aktuellen Systemdatum.
	/// </summary>
	/// <returns>Neigung der mittleren Planetenbahn zum aktuellen Systemdatum.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double Inclination(){ return MJupiter.Inclination(DateTime.Now.ToJdn()); }

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
	/// Liefert die Länge des aufsteigenden Knotens der mittleren Planetenbahn zum aktuellen Systemdatum.
	/// </summary>
	/// <returns>Länge des aufsteigenden Knotens der mittleren Planetenbahn zum aktuellen Systemdatum.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double LongitudeOfAscendingNode(){ return MJupiter.LongitudeOfAscendingNode(DateTime.Now.ToJdn()); }

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
	/// Liefert die Länge des Perihels der mittleren Planetenbahn zum aktuellen Systemdatum.
	/// </summary>
	/// <returns>Länge des Perihels der mittleren Planetenbahn zum aktuellen Systemdatum.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double LongitudeOfPerihelion(){ return MJupiter.LongitudeOfPerihelion(DateTime.Now.ToJdn()); }

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
	/// Liefert die mittlere Anomalie der mittleren Planetenbahn zum aktuellen Systemdatum.
	/// </summary>
	/// <returns>Mittlere Anomalie der mittleren Planetenbahn zum aktuellen Systemdatum.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double MeanAnomaly(){ return MJupiter.MeanAnomaly(DateTime.Now.ToJdn());	}

	// MJupiter.MeanAnomaly(double)
	/// <summary>
	/// Liefert die mittlere Anomalie der mittleren Planetenbahn zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Mittlere Anomalie der mittleren Planetenbahn zur julianischen Tageszahl.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double MeanAnomaly(double jd){ return (MJupiter.MeanLongitude(jd) + MJupiter.LongitudeOfPerihelion(jd)).Mod(360.0); }

	// MJupiter.MeanLongitude()
	/// <summary>
	/// Liefert die mittlere Länge der mittleren Planetenbahn zum aktuellen Systemdatum.
	/// </summary>
	/// <returns>Mittlere Länge der mittleren Planetenbahn zum aktuellen Systemdatum.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double MeanLongitude(){ return MJupiter.MeanLongitude(DateTime.Now.ToJdn()); }

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
		return (MMath.Polynome(t, 34.351519, 3036.3027748, 0.00023330, 0.000000037)).Mod(360.0);
	}

	// MJupiter.Opposition()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die Opposition mit der Sonne nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die Opposition mit der Sonne nach der aktuellen Systemzeit.</returns>
	public static double Opposition(){ return MJupiter.Opposition(DateTime.Now.ToJdn()); }

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
			double m = (MMath.ToRad(318.4681 + k * 33.140229)).Mod(MMath.Pi2);
			double t = (j - MCalendar.Jdn20000101) / 36525.0;
			double a = (MMath.ToRad(82.74 + 40.76 * t)).Mod(MMath.Pi2);
			double h;

			// Korrektur berechnen und anwenden
			h  = MMath.Polynome(t, -0.1029,  0.0000, -0.00009);
			h += MMath.Polynome(t, -1.9658, -0.0056,  0.00007) * MMath.Sin(      m);
			h += MMath.Polynome(t,  6.1537,  0.0210, -0.00006) * (      m).Cos();
			h += MMath.Polynome(t, -0.2081, -0.0013          ) * MMath.Sin(2.0 * m);
			h += MMath.Polynome(t, -0.1116, -0.0010          ) * (2.0 * m).Cos();
			h += MMath.Polynome(t,  0.0074,  0.0001          ) * MMath.Sin(3.0 * m);
			h += MMath.Polynome(t, -0.0097, -0.0001          ) * (3.0 * m).Cos();
			h += MMath.Polynome(t,  0.0000,  0.0144, -0.00008) * MMath.Sin(      a);
			h += MMath.Polynome(t,  0.3642, -0.0019, -0.00029) * (      a).Cos();
			j += h;
		}
		return j;
	}

	// MJupiter.Perihelion()
	/// <summary>
	/// Liefert die julianische Tageszahl der nächsten Durchgangs durch das Perihel nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl der nächsten Durchgangs durch das Perihel nach der aktuellen Systemzeit.</returns>
	public static double Perihelion(){ return MJupiter.Perihelion(DateTime.Now.ToJdn()); }

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
	/// Liefert die heliozentrisch-ekliptikale Position zum aktuellen Systemdatum.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <returns>Heliozentrisch-ekliptikale Position zum aktuellen Systemdatum.</returns>
	public static CPolar PositionEcliptical(EPrecision value){ return MJupiter.PositionEcliptical(value, DateTime.Now.ToJdn()); }

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
	/// Liefert die (scheinbare) geozentrisch-äquatoriale Position zum aktuellen Systemdatum.
	/// </summary>
	/// <returns>Geozentrisch-äquatoriale Position zum aktuellen Systemdatum.</returns>
	public static CPolar PositionEquatorial(){ return MJupiter.PositionEquatorial(DateTime.Now.ToJdn()); }

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
			if((tau - tmp).Abs() < 0.00005) break; // Ausreichende Genauigkeit sicherstellen
			if(tau != 0.0 && tmp >= tau)    break; // Abbruch bei Schwingung sicherstellen

			// Wert anwenden und nächsten Iterationsschritt vorbereiten
			jdn += tmp;
			tau  = tmp;
		}

		// ----------------------- //
		// Aberration und Nutation //
		// ----------------------- //

		// Aberation und Nutation anwenden
		(lG, bG) = MEphemerides.AberrationEcliptical(lG, bG, jdn);
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
	public static double RegressionEnd(){ return MJupiter.RegressionEnd(DateTime.Now.ToJdn()); }

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
			double m = (MMath.ToRad(318.4681 + k * 33.140229)).Mod(MMath.Pi2);
			double t = (j - MCalendar.Jdn20000101) / 36525.0;
			double a = (MMath.ToRad(82.74 + 40.76 * t)).Mod(MMath.Pi2);
			double h;

			// Korrektur berechnen und anwenden
			h  = MMath.Polynome(t, 60.3023,  0.0002, -0.00009);
			h += MMath.Polynome(t,  0.3506, -0.0034,  0.00004) * MMath.Sin(      m);
			h += MMath.Polynome(t,  5.3635,  0.0247, -0.00007) * (      m).Cos();
			h += MMath.Polynome(t, -0.1872, -0.0016          ) * MMath.Sin(2.0 * m);
			h += MMath.Polynome(t, -0.0037, -0.0005          ) * (2.0 * m).Cos();
			h += MMath.Polynome(t,  0.0012,  0.0001          ) * MMath.Sin(3.0 * m);
			h += MMath.Polynome(t, -0.0096, -0.0001          ) * (3.0 * m).Cos();
			h += MMath.Polynome(t,  0.0000,  0.0144, -0.00008) * MMath.Sin(      a);
			h += MMath.Polynome(t,  0.3642, -0.0019, -0.00029) * (      a).Cos();
			j += h;
		}
		return j;
	}

	// MJupiter.RegressionStart()
	/// <summary>
	/// Liefert die julianische Tageszahl der Rückläufigkeitsanfangs nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl der Rückläufigkeitsanfangs nach der aktuellen Systemzeit.</returns>
	public static double RegressionStart(){ return MJupiter.RegressionStart(DateTime.Now.ToJdn()); }

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
			double m = (MMath.ToRad(318.4681 + k * 33.140229)).Mod(MMath.Pi2);
			double t = (j - MCalendar.Jdn20000101) / 36525.0;
			double a = (MMath.ToRad(82.74 + 40.76 * t)).Mod(MMath.Pi2);
			double h;

			// Korrektur berechnen und anwenden
			h  = MMath.Polynome(t, -60.3670, -0.0001, -0.00009);
			h += MMath.Polynome(t,  -2.3144, -0.0124,  0.00007) * MMath.Sin(      m);
			h += MMath.Polynome(t,   6.7439,  0.0166, -0.00006) * (      m).Cos();
			h += MMath.Polynome(t,  -0.2259, -0.0010          ) * MMath.Sin(2.0 * m);
			h += MMath.Polynome(t,  -0.1497, -0.0014          ) * (2.0 * m).Cos();
			h += MMath.Polynome(t,   0.0105,  0.0001          ) * MMath.Sin(3.0 * m);
			h +=                    -0.0098                     * (3.0 * m).Cos();
			h += MMath.Polynome(t,   0.0000,  0.0144, -0.00008) * MMath.Sin(      a);
			h += MMath.Polynome(t,   0.3642, -0.0019, -0.00029) * (      a).Cos();
			j += h;
		}
		return j;
	}

	// MJupiter.SemimajorAxis()
	/// <summary>
	/// Liefert die große Halbachse der mittleren Planetenbahn zum aktuellen Systemdatum.
	/// </summary>
	/// <returns>Große Halbachse der mittleren Planetenbahn zum aktuellen Systemdatum.</returns>
	public static double SemimajorAxis(){ return MJupiter.SemimajorAxis(DateTime.Now.ToJdn()); }

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

	// MJupiter.SiderealPeriod()
	/// <summary>
	/// Liefert die mittlere siderische Periode zur mittleren Planetenbahn.
	/// </summary>
	/// <returns>Mittlere siderische Periode zur mittleren Planetenbahn.</returns>
	public static double SiderealPeriod(){ return 4330.595764; }

	// MJupiter.SynodicPeriod()
	/// <summary>
	/// Liefert die mittlere synodische Periode zur mittleren Planetenbahn.
	/// </summary>
	/// <returns>Mittlere synodische Periode zur mittleren Planetenbahn.</returns>
	public static double SynodicPeriod(){ return 398.884047; }
}
