using Acamat.LCore;
using Acamat.LMath;
using Acamat.LMath.Geometry;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt Berechnungen zum Uranus.
/// </summary>
public partial class MUranus
{
	// ------------------- //
	// Felder und Methoden //
	// ------------------- //
	// MUranus.Aphelion()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch das Aphel nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch das Aphel nach der aktuellen Systemzeit.</returns>
	public static double Aphelion(){ return MUranus.Aphelion(DateTime.Now.ToJdn()); }

	// MUranus.Aphelion(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch das Aphel nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch das Aphel nach der julianischen Tageszahl.</returns>
	public static double Aphelion(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor(0.01190 * (y - 2051.10)) - 0.5;
		double j = 0.0;

		// Berechnungsschleife
		while(j <= jd)
		{
			// Tageszahl berechnen
			k += 1.0;
			j  = MMath.Polynome(k, 2470213.500, 30694.87670, -0.00542);
		}
		return j;
	}

	// MUranus.Conjunction()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die obere Konjunktion mit der Sonne nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die obere Konjunktion mit der Sonne nach der aktuellen Systemzeit.</returns>
	public static double Conjunction(){ return MUranus.Conjunction(DateTime.Now.ToJdn()); }

	// MUranus.Conjuction(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die Konjunktion mit der Sonne nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die Konjunktion mit der Sonne nach der julianischen Tageszahl.</returns>
	public static double Conjunction(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor((365.2425 * y + 1721060.0 - 2451579.489) / 369.656035) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while (j <= jd)
		{
			// Näherung berechnen
			k += 1.0;
			j  = 2451579.489 + k * 369.656035;

			// Hilfsfelder berechnen
			double m = MMod.Mod(MMath.ToRad(31.5219 + k * 4.333093), MMath.Pi2);
			double t = (j - MCalendar.Jdn20000101) / 36525.0;
			double a = MMod.Mod(MMath.ToRad(207.83 +   8.51 * t), MMath.Pi2);
			double b = MMod.Mod(MMath.ToRad(108.84 + 419.96 * t), MMath.Pi2);
			double h;

			// Korrektur berechnen und anwenden
			h  = MMath.Polynome(t, -0.0859,  0.0003          );
			h += MMath.Polynome(t, -3.8179, -0.0148,  0.00003) * MMath.Sin(      m);
			h += MMath.Polynome(t,  5.1228, -0.0105, -0.00002) * MMath.Cos(      m);
			h += MMath.Polynome(t, -0.0803,  0.0011          ) * MMath.Sin(2.0 * m);
			h += MMath.Polynome(t, -0.1905, -0.0006          ) * MMath.Cos(2.0 * m);
			h += MMath.Polynome(t,  0.0088,  0.0001          ) * MMath.Sin(3.0 * m);
			h +=                    0.8850                     * MMath.Cos(      a);
			h +=                    0.2153                     * MMath.Cos(      b);
			j += h;
		}
		return j;
	}

	// MUranus.Eccentricity()
	/// <summary>
	/// Liefert die Exzentrizität der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Exzentrizität der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	public static double Eccentricity(){ return MUranus.Eccentricity(DateTime.Now.ToJdn()); }

	// MUranus.Eccentricity(double)
	/// <summary>
	/// Liefert die Exzentrizität der mittleren Planetenbahn zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Exzentrizität der mittleren Planetenbahn zur julianischen Tageszahl.</returns>
	public static double Eccentricity(double jd)
	{
		// Lokale Felder einrichten und Exzentrizität berechnen
		double t = (jd - MCalendar.Jdn20000101) / 36525.0;
		return MMath.Polynome(t, 0.04638122, -0.000027293, 0.0000000789, 0.00000000024);
	}

	// MUranus.Inclination()
	/// <summary>
	/// Liefert die Neigung der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Neigung der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double Inclination(){ return MUranus.Inclination(DateTime.Now.ToJdn()); }

	// MUranus.Inclination(double)
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
		return MMath.Polynome(t, 0.773197, -0.0007744, 0.00003749, -0.000000092);
	}

	// MUranus.Latitude(EPrecision)          » MUranus.Latitude.cs
	// MUranus.Latitude(EPrecision, double)  » MUranus.Latitude.cs
	// MUranus.Longitude(EPrecision)         » MUranus.Longitude.cs
	// MUranus.Longitude(EPrecision, double) » MUranus.Longitude.cs

	// MUranus.LongitudeOfAscendingNode()
	/// <summary>
	/// Liefert die Länge des aufsteigenden Knotens der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Länge des aufsteigenden Knotens der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double LongitudeOfAscendingNode(){ return MUranus.LongitudeOfAscendingNode(DateTime.Now.ToJdn()); }

	// MUranus.LongitudeOfAscendingNode(double)
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
		return MMath.Polynome(t, 74.005957, 0.5211278, 0.00133947, 0.000018484);
	}

	// MUranus.LongitudeOfPerihelion()
	/// <summary>
	/// Liefert die Länge des Perihels der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Länge des Perihels der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double LongitudeOfPerihelion(){ return MUranus.LongitudeOfPerihelion(DateTime.Now.ToJdn()); }

	// MUranus.LongitudeOfPerihelion(double)
	/// <summary>
	/// Liefert die Länge des Perihels der mittleren Planetenbahn zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Länge des Perihels der mittleren Planetenbahn zur julianischen Tageszahl.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double LongitudeOfPerihelion(double jd)
	{
		// Lokale Felder einrichten und Länge berechnen
		double t = (jd - MCalendar.Jdn20000101) / 36525.0;
		return MMath.Polynome(t, 173.005291, 1.486370, 0.00021406, 0.000000434);
	}

	// MUranus.MeanAnomaly()
	/// <summary>
	/// Liefert die mittlere Anomalie der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Mittlere Anomalie der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double MeanAnomaly(){ return MUranus.MeanAnomaly(DateTime.Now.ToJdn()); }

	// MUranus.MeanAnomaly(double)
	/// <summary>
	/// Liefert die mittlere Anomalie der mittleren Planetenbahn zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Mittlere Anomalie der mittleren Planetenbahn zur julianischen Tageszahl.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double MeanAnomaly(double jd){ return MMod.Mod(MUranus.MeanLongitude(jd) + MUranus.LongitudeOfPerihelion(jd), 360.0); }

	// MUranus.MeanLongitude()
	/// <summary>
	/// Liefert die mittlere Länge der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Mittlere Länge der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double MeanLongitude(){ return MUranus.MeanLongitude(DateTime.Now.ToJdn()); }

	// MUranus.MeanLongitude(double)
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
		return MMod.Mod(MMath.Polynome(t, 314.055005, 429.8640561, 0.00030390, 0.000000026), 360.0);
	}

	// MUranus.Opposition()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die Opposition mit der Sonne nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die Opposition mit der Sonne nach der aktuellen Systemzeit.</returns>
	public static double Opposition(){ return MUranus.Opposition(DateTime.Now.ToJdn()); }

	// MUranus.Opposition(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die Opposition mit der Sonne nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die Opposition mit der Sonne nach der aktuellen Systemzeit.</returns>
	public static double Opposition(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor((365.2425 * y + 1721060.0 - 2451764.317) / 369.656035) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while (j <= jd)
		{
			// Näherung berechnen
			k += 1.0;
			j  = 2451764.317 + k * 369.656035;

			// Hilfsfelder berechnen
			double m = MMod.Mod(MMath.ToRad(213.6884 + k * 4.333093), MMath.Pi2);
			double t = (j - MCalendar.Jdn20000101) / 36525.0;
			double a = MMod.Mod(MMath.ToRad(207.83 +   8.51 * t), MMath.Pi2);
			double b = MMod.Mod(MMath.ToRad(108.84 + 419.96 * t), MMath.Pi2);
			double h;

			// Korrektur berechnen und anwenden
			h  = MMath.Polynome(t,  0.0844, -0.0006          );
			h += MMath.Polynome(t, -0.1048,  0.0246          ) * MMath.Sin(      m);
			h += MMath.Polynome(t, -5.1221,  0.0104,  0.00003) * MMath.Cos(      m);
			h += MMath.Polynome(t, -0.1428,  0.0005          ) * MMath.Sin(2.0 * m);
			h += MMath.Polynome(t, -0.0148, -0.0013          ) * MMath.Cos(2.0 * m);
			h +=                    0.0055                     * MMath.Cos(3.0 * m);
			h +=                    0.8850                     * MMath.Cos(      a);
			h +=                    0.2153                     * MMath.Cos(      b);
			j += h;
		}
		return j;
	}

	// MUranus.Perihelion()
	/// <summary>
	/// Liefert die julianische Tageszahl der nächsten Durchgangs durch das Perihel nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl der nächsten Durchgangs durch das Perihel nach der aktuellen Systemzeit.</returns>
	public static double Perihelion(){ return MUranus.Perihelion(DateTime.Now.ToJdn()); }

	// MUranus.Perihelion(double)
	/// <summary>
	/// Liefert die julianische Tageszahl der nächsten Durchgangs durch das Perihel nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl der nächsten Durchgangs durch das Perihel nach der julianischen Tageszahl.</returns>
	public static double Perihelion(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor(0.01190 * (y - 2051.10)) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while(j <= jd)
		{
			// Tageszahl berechnen
			k += 1.0;
			j  = MMath.Polynome(k, 2470213.500, 30694.87670, -0.00542);
		}
		return j;
	}

	// MUranus.PositionEcliptical(EPrecision)
	/// <summary>
	/// Liefert die heliozentrisch-ekliptikale Position zur aktuellen Systemzeit.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <returns>Heliozentrisch-ekliptikale Position zur aktuellen Systemzeit.</returns>
	public static CPolar PositionEcliptical(EPrecision value){ return MUranus.PositionEcliptical(value, DateTime.Now.ToJdn()); }

	// MUranus.PositionEcliptical(EPrecision, double)
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
		rtn.Latitude  = MUranus.Latitude (value, jd);
		rtn.Longitude = MUranus.Longitude(value, jd);
		rtn.Radius    = MUranus.Radius   (value, jd);
		return rtn;
	}

	// MUranus.PositionEquatorial()
	/// <summary>
	/// Liefert die (scheinbare) geozentrisch-äquatoriale Position zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Geozentrisch-äquatoriale Position zur aktuellen Systemzeit.</returns>
	public static CPolar PositionEquatorial(){ return MUranus.PositionEquatorial(DateTime.Now.ToJdn()); }

	// MUranus.PositionEquatorial()
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
			bH = MUranus.Latitude (EPrecision.High, jdn);
			lH = MUranus.Longitude(EPrecision.High, jdn);
			rH = MUranus.Radius   (EPrecision.High, jdn);

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

	// MUranus.Radius(EPrecision)         » MUranus.Radius.cs
	// MUranus.Radius(EPrecision, double) » MUranus.Radius.cs

	// MUranus.Rise(CPolar)
	/// <summary>
	/// Liefert die Ereigniskennung, die julianische Tageszahl des Aufgangs und die Morgenweite am geographischen Ort und zum aktuellen Systemdatum.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <returns>Ereigniskennung, die julianische Tageszahl des Aufgangs und die Morgenweite am geographischen Ort und zum aktuellen Systemdatum.</returns>
	public static (EEventType type, double? jd, double? azimuth) Rise(CPolar position){ return MUranus.Rise(position.Longitude, position.Latitude, DateTime.Now.ToJdn()); }

	// MUranus.Rise(CPolar, double)
	/// <summary>
	/// Liefert die Ereigniskennung, die julianische Tageszahl des Aufgangs und die Morgenweite am geographischen Ort und zum julianischen Tagesdstum.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <param name="jd">Julianisches Tagesdatum.</param>
	/// <returns>Ereigniskennung, die julianische Tageszahl des Aufgangs und die Morgenweite am geographischen Ort und zum julianischen Tagesdstum.</returns>
	public static (EEventType type, double? jd, double? azimuth) Rise(CPolar position, double jd){ return MUranus.Rise(position.Longitude, position.Latitude, jd); }

	// MUranus.Rise(double, double)
	/// <summary>
	/// Liefert die Ereigniskennung, die julianische Tageszahl des Aufgangs und die Morgenweite am geographischen Ort und zum aktuellen Systemdatum.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <returns>Ereigniskennung, die julianische Tageszahl des Aufgangs und die Morgenweite am geographischen Ort und zum aktuellen Systemdatum.</returns>
	public static (EEventType type, double? jd, double? azimuth) Rise(double lambda, double phi){ return MUranus.Rise(lambda, phi, DateTime.Now.ToJdn()); }

	// MUranus.Rise(double, double, double)
	/// <summary>
	/// Liefert die Ereigniskennung, die julianische Tageszahl des Aufgangs und die Morgenweite am geographischen Ort und zum julianischen Tagesdatum.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <param name="jd">Julianisches Tagesdatum.</param>
	/// <returns>Liefert die Ereigniskennung, die julianische Tageszahl des Aufgangs und die Morgenweite am geographischen Ort und zum julianischen Tagesdatum.</returns>
	public static (EEventType type, double? jd, double? azimuth) Rise(double lambda, double phi, double jd)
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
		double H    =  0.0;                               //
		double sinP = MMath.Sin(phi);                     // Breitensinus
		double cosP = MMath.Cos(phi);                     // Breitencosinus

		// Position für nachfolgenden Tag berechnen
		l = MUranus.Longitude(EPrecision.Low, jdn + 1.0);
		b = MUranus.Latitude (EPrecision.Low, jdn + 1.0);
		double aP = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jdn + 1.0);
		double dP = MEphemerides.ToDelta(l, b, EObliquity.Mean, jdn + 1.0);

		// Position für gegebenen Tag berechnen
		l = MUranus.Longitude(EPrecision.Low, jdn);
		b = MUranus.Latitude (EPrecision.Low, jdn);
		double a0 = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jdn);
		if(MMath.Abs(aP - a0) > 1.0)
			a0 += MMath.Sgn(aP - a0) * MMath.Pi2;
		double d0 = MEphemerides.ToDelta(l, b, EObliquity.Mean, jdn);

		// Position für vorhergehenden Tag berechnen
		l = MUranus.Longitude(EPrecision.Low, jdn + 1.0);
		b = MUranus.Latitude (EPrecision.Low, jdn + 1.0);
		double aM = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jdn - 1.0);
		if(MMath.Abs(a0 - aM) > 1.0)
			aM += MMath.Sgn(a0 - aM) * MMath.Pi2;
		double dM = MEphemerides.ToDelta(l, b, EObliquity.Mean, jdn - 1.0);

		// Stundenwinkel berechnen und prüfen
		double cosH = (MMath.Sin(h0) - sinP * MMath.Sin(dP)) / (cosP * MMath.Cos(dP));
		if(MMath.Abs(cosH) > 1.0)
			return(cosH < 1.0 ? EEventType.AlwaysAboveHorizon : EEventType.AlwaysBeneathHorizon, null, null);
		H = MMath.ArcCos(cosH);

		// ------------------- //
		// Ereigniszeit nähern //
		// ------------------- //

		// Sternzeit und Stundenwinkel zum gegebenen Zeitpunkt bestimmen
		double t0 = MEphemerides.Gmst(jdn);
		double m  = MMath.Div((a0 + lambda - t0 - H) / MMath.Pi2);

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

		// Kein Ereignis verarbeiten
		if(m < 0.0 | m >= 1.0)
			return(EEventType.NoEvent, null, null);

		// Rückgabe
		return(EEventType.Normal, jd + m, MEphemerides.ToAzimuth(H, d, phi));
	}

	// MUranus.SemimajorAxis()
	/// <summary>
	/// Liefert die große Halbachse der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Große Halbachse der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	public static double SemimajorAxis(){ return MUranus.SemimajorAxis(DateTime.Now.ToJdn()); }

	// MUranus.SemimajorAxis(double)
	/// <summary>
	/// Liefert die große Halbachse der mittleren Planetenbahn zur julianischen Tageszahl.
	/// </summary>
	/// <returns>Große Halbachse der mittleren Planetenbahn zur julianischen Tageszahl.</returns>
	public static double SemimajorAxis(double jd)
	{
		// Lokale Felder einrichten und große Halbachse berechnen
		double t = (jd - MCalendar.Jdn20000101) / 36525.0;
		return MMath.Polynome(t, 19.218446062, -0.0000000372, 0.00000000098);
	}

	// MUranus.Set(CPolar)
	/// <summary>
	/// Liefert die Ereigniskennung, die julianische Tageszahl des Untergangs und die Abendweite am geographischen Ort und zum aktuellen Systemdatum.
	/// </summary>
	/// <param name="position">Geographisches Position.</param>
	/// <returns>Ereigniskennung, die julianische Tageszahl des Untergangs und die Abendweite am geographischen Ort und zum aktuellen Systemdatum.</returns>
	public static (EEventType type, double? jd, double? azimuth) Set(CPolar position){ return MUranus.Set(position.Longitude, position.Latitude, DateTime.Now.ToJdn()); }

	// MUranus.Set(CPolar, double)
	/// <summary>
	/// Liefert die Ereigniskennung, die julianische Tageszahl des Untergangs und die Abendweite am geographischen Ort und zum julianischen Tagesdatum.
	/// </summary>
	/// <param name="position">Geographisches Position.</param>
	/// <param name="jd">Julianisches Tagesdatum.</param>
	/// <returns>Ereigniskennung, die julianische Tageszahl des Untergangs und die Abendweite am geographischen Ort und zum julianischen Tagesdatum.</returns>
	public static (EEventType type, double? jd, double? azimuth) Set(CPolar position, double jd){ return MUranus.Set(position.Longitude, position.Latitude, jd); }

	// MUranus.Set(double, double)
	/// <summary>
	/// Liefert die Ereigniskennung, die julianische Tageszahl des Untergangs und die Abendweite am geographischen Ort und zum aktuellen Systemdatum.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <returns>Ereigniskennung, die julianische Tageszahl des Untergangs und die Abendweite am geographischen Ort und zum aktuellen Systemdatum.</returns>
	public static (EEventType type, double? jd, double? azimuth) Set(double lambda, double phi){ return MUranus.Set(lambda, phi, DateTime.Now.ToJdn()); }

	// MUranus.Set(double, double, double)
	/// <summary>
	/// Liefert die Ereignislennung, die julianische Tageszahl des Untergangs und die Abendweite am geographischen Ort und zum julianischen Tagesdatum.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <param name="jd">Julianisches Tagesdatum.</param>
	/// <returns>Ereignislennung, die julianische Tageszahl des Untergangs und die Abendweite am geographischen Ort und zum julianischen Tagesdatum.</returns>
	public static (EEventType type, double? jd, double? azimuth) Set(double lambda, double phi, double jd)
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
		l = MUranus.Longitude(EPrecision.Low, jdn + 1.0);
		b = MUranus.Latitude (EPrecision.Low, jdn + 1.0);
		double aP = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jd + 1.0);
		double dP = MEphemerides.ToDelta(l, b, EObliquity.Mean, jd + 1.0);

		// Position für gegebenen Tag berechnen
		l = MUranus.Longitude(EPrecision.Low, jdn);
		b = MUranus.Latitude (EPrecision.Low, jdn);
		double a0 = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jdn);
		if(MMath.Abs(aP - a0) > 1.0)
			a0 += MMath.Sgn(aP - a0) * MMath.Pi2;
		double d0 = MEphemerides.ToDelta(l, b, EObliquity.Mean, jdn);

		// Position für vorhergehenden Tag berechnen
		l = MUranus.Longitude(EPrecision.Low, jdn - 1.0);
		b = MUranus.Latitude (EPrecision.Low, jdn - 1.0);
		double aM = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jd - 1.0);
		if(MMath.Abs(a0 - aM) > 1.0)
			aM += MMath.Sgn(a0 - aM) * MMath.Pi2;
		double dM = MEphemerides.ToDelta(l, b, EObliquity.Mean, jd - 1.0);

		// Stundenwinkel berechnen und prüfen
		double cosH = (MMath.Sin(h0) - sinP * MMath.Sin(dP)) / (cosP * MMath.Cos(dP));
		if(MMath.Abs(cosH) > 1.0)
			return(cosH < 1.0 ? EEventType.AlwaysAboveHorizon : EEventType.AlwaysBeneathHorizon, null, null);
		H = MMath.ArcCos(cosH);

		// ------------------- //
		// Ereigniszeit nähern //
		// ------------------- //

		// Sternzeit und Stundenwinkel zum gegebenen Zeitpunkt bestimmen
		double t0 = MEphemerides.Gmst(jdn);
		double m = MMath.Div((a0 + lambda - t0 + H) / MMath.Pi2);

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

		// Kein Ereignis verarbeiten
		if(m < 0.0 | m >= 1.0)
			return(EEventType.NoEvent, null, null);

		// Rückgabe
		return(EEventType.Normal, jd + m, MEphemerides.ToAzimuth(H, d, phi));
	}

	// MUranus.SynodicPeriod()
	/// <summary>
	/// Liefert die mittlere synodische Periode zur mittleren Planetenbahn.
	/// </summary>
	/// <returns>Mittlere synodische Periode zur mittleren Planetenbahn.</returns>
	public static double SynodicPeriod(){ return 369.670550; }

	// MUranus.Transit(CPolar)
	/// <summary>
	/// Liefert die Ereignislkennung, die julianische Tageszahl des Meridiandurchgangs und die horizontale Höhe am geographischen Ort und zum aktuellen Systemdatum.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <returns>Ereignislkennung, die julianische Tageszahl des Meridiandurchgangs und die horizontale Höhe am geographischen Ort und zum aktuellen Systemdatum.</returns>
	public static (EEventType type, double? jd, double? height) Transit(CPolar position){ return MUranus.Transit(position.Longitude, position.Latitude, DateTime.Now.ToJdn()); }

	// MUranus.Transit(CPolar, double)
	/// <summary>
	/// Liefert die Ereigniskennung, die julianische Tageszahl des Meridiandurchgangs und die horizontale Höhe am geographischen Ort und zum julianischen Tagesdatum.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <returns>Julianische Tageszahl des Meridiandurchgangs am geographischen Ort und zur julianischen Tageszahl.</returns>
	public static (EEventType type, double? jd, double? height) Transit(CPolar position, double jd){ return MUranus.Transit(position.Longitude, position.Latitude, jd); }

	// MUranus.Transit(double, double)
	/// <summary>
	/// Liefert die Ereigniskennung, die julianische Tageszahl des Meridiandurchgangs und die horizontale Höhe am geographischen Ort und zum aktuellen Systemdatum.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <returns>Ereigniskennung, die julianische Tageszahl des Meridiandurchgangs und die horizontale Höhe am geographischen Ort und zum aktuellen Systemdatum.</returns>
	public static (EEventType type, double? jd, double? height) Transit(double lambda, double phi){ return MUranus.Transit(lambda, phi, DateTime.Now.ToJdn()); }

	// MUranus.Transit(double, double, double)
	/// <summary>
	/// Liefert die Ereigniskennung, die julianische Tageszahl des Meridiandurchgangs und die horizontale Höhe am geographischen Ort und zum julianischen Tagesdatum.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <param name="jd">Julianisches Tagesdatum.</param>
	/// <returns>Ereigniskennung, die julianische Tageszahl des Meridiandurchgangs und die horizontale Höhe am geographischen Ort und zum julianischen Tagesdatum.</returns>
	public static (EEventType type, double? jd, double? height) Transit(double lambda, double phi, double jd)
	{
		// Lokale Felder einrichten
		double jdn = MMath.Floor(jd - 0.5) + 0.5; // Tageszahl um Mitternacht
		double l   = 0.0;                         // Geozentrische Länge
		double b   = 0.0;                         // Geozentrische Breite
		double a   = 0.0;                         // Rektaszension
		double dm  = 1.0;                         // Korrekturglied

		// Position für nachfolgenden Tag berechnen
		l = MUranus.Longitude(EPrecision.Low, jdn + 1.0);
		b = MUranus.Latitude (EPrecision.Low, jdn + 1.0);
		double aP = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jdn + 1.0);
		double dP = MEphemerides.ToDelta(l, b, EObliquity.Mean, jdn + 1.0);

		// Position für gegebenen Tag berechnen
		l = MUranus.Longitude(EPrecision.Low, jdn);
		b = MUranus.Latitude (EPrecision.Low, jdn);
		double a0 = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jdn);
		if(MMath.Abs(aP - a0) > 1.0)
			a0 += MMath.Sgn(aP - a0) * MMath.Pi2;
		double d0 = MEphemerides.ToDelta(l, b, EObliquity.Mean, jdn);

		// Position für vorhergehenden Tag berechnen
		l = MUranus.Longitude(EPrecision.Low, jdn - 1.0);
		b = MUranus.Latitude (EPrecision.Low, jdn - 1.0);
		double aM = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jdn - 1.0);
		if(MMath.Abs(a0 - aM) > 1.0)
			aM += MMath.Sgn(a0 - aM) * MMath.Pi2;
		double dM = MEphemerides.ToDelta(l, b, EObliquity.Mean, jdn - 1.0);

		// ------------------- //
		// Ereigniszeit nähern //
		// ------------------- //

		// Sternzeit und Stundenwinkel zum gegebenen Zeitpunkt bestimmen
		double t0 = MEphemerides.Gmst(jdn);
		double m = MMath.Div((aP + lambda - t0) / MMath.Pi2);

		// Ereigniszeit iterieren
		while(MMath.Abs(dm) >= 0.0001)
		{
			// Iteration durchführen und nächsten Iterationsschritt vorbereiten
			a  = MMath.Bessel(m, aM, a0, aP);
			dm = MMath.Div((a + lambda - t0 - 6.300388093 * m) / MMath.Pi2);
			if(MMath.Abs(dm) > 0.5) dm -= MMath.Sgn(dm);
			m += dm;
		}

		// Kein Ereignis verarbeiten
		if(m < 0.0 | m >= 1.0)
			return(EEventType.NoEvent, null, null);

		// Rückgabe
		return(EEventType.Normal, jd + m, MEphemerides.ToHeight(0.0, MMath.Bessel(m, dM, d0, dP), phi));
	}

	// MUranus.TropicalPeriod()
	/// <summary>
	/// Liefert die mittlere tropische Periode zur mittleren Planetenbahn.
	/// </summary>
	/// <returns>Mittlere tropische Periode zur mittleren Planetenbahn.</returns>
	public static double TropicalPeriod(){ return 30588.740341; }
}
