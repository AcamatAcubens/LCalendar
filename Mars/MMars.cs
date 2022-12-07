﻿using Acamat.LCore;
using Acamat.LMath;
using Acamat.LMath.Geometry;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt Berechnungen zum Mars.
/// </summary>
public partial class MMars
{
	// ------------------- //
	// Felder und Methoden //
	// ------------------- //
	// MMars.Aphelion()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch das Aphel nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch das Aphel nach der aktuellen Systemzeit.</returns>
	public static double Aphelion(){ return MMars.Aphelion(DateTime.Now.ToJdn()); }

	// MMars.Aphelion(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch das Aphel nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch das Aphel nach der julianischen Tageszahl.</returns>
	public static double Aphelion(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor(0.53166 * (y - 2001.78)) - 0.5;
		double j = 0.0;

		// Berechnungsschleife
		while(j <= jd)
		{
			// Tageszahl berechnen
			k += 1.0;
			j  = MMath.Polynome(k, 2452195.026, 686.9957857, -0.0000001187);
		}
		return j;
	}

	// MMars.Conjunction()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die obere Konjunktion mit der Sonne nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die obere Konjunktion mit der Sonne nach der aktuellen Systemzeit.</returns>
	public static double Conjunction(){ return MMars.Conjunction(DateTime.Now.ToJdn()); }

	// MMars.Conjuction(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die Konjunktion mit der Sonne nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die Konjunktion mit der Sonne nach der julianischen Tageszahl.</returns>
	public static double Conjunction(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor((365.2425 * y + 1721060.0 - 2451707.414) / 779.936104) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while (j <= jd)
		{
			// Näherung berechnen
			k += 1.0;
			j  = 2451707.414 + k * 779.936104;

			// Hilfsfelder berechnen
			double m = MMod.Mod(MMath.ToRad(157.6047 + k * 48.705244), MMath.Pi2);
			double t = (j - MCalendar.Jdn20000101) / 36525.0;
			double h;

			// Korrektur berechnen und anwenden
			h  = MMath.Polynome(t,   0.3102, -0.0001,  0.00001);
			h += MMath.Polynome(t,   9.7273, -0.0156,  0.00001) * MMath.Sin(      m);
			h += MMath.Polynome(t, -18.3195, -0.0467,  0.00009) * MMath.Cos(      m);
			h += MMath.Polynome(t,  -1.6488, -0.0133,  0.00001) * MMath.Sin(2.0 * m);
			h += MMath.Polynome(t,  -2.6117, -0.0020,  0.00004) * MMath.Cos(2.0 * m);
			h += MMath.Polynome(t,  -0.6827, -0.0026,  0.00001) * MMath.Sin(3.0 * m);
			h += MMath.Polynome(t,   0.0281,  0.0035,  0.00001) * MMath.Cos(3.0 * m);
			h += MMath.Polynome(t,  -0.0823,  0.0006,  0.00001) * MMath.Sin(4.0 * m);
			h += MMath.Polynome(t,   0.1584,  0.0013          ) * MMath.Cos(4.0 * m);
			h += MMath.Polynome(t,   0.0270,  0.0005          ) * MMath.Sin(5.0 * m);
			h +=                     0.0433                     * MMath.Cos(5.0 * m);
			j += h;
		}
		return j;
	}

	// MMars.Eccentricity()
	/// <summary>
	/// Liefert die Exzentrizität der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Exzentrizität der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	public static double Eccentricity(){ return MMars.Eccentricity(DateTime.Now.ToJdn()); }

	// MMars.Eccentricity(double)
	/// <summary>
	/// Liefert die Exzentrizität der mittleren Planetenbahn zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Exzentrizität der mittleren Planetenbahn zur julianischen Tageszahl.</returns>
	public static double Eccentricity(double jd)
	{
		// Lokale Felder einrichten und Extrentrizität berechnen
		double t = (jd - MCalendar.Jdn20000101) / 36525.0;
		return MMath.Polynome(t, 0.09340065, 0.000090484, -0.0000000806, -0.00000000025);
	}

	// MMars.Inclination()
	/// <summary>
	/// Liefert die Neigung der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Neigung der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double Inclination(){ return MMars.Inclination(DateTime.Now.ToJdn()); }

	// MMars.Inclination(double)
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
		return MMath.Polynome(t, 1.849726, -0.0006011, 0.00001276, -0.000000007);
	}

	// MMars.Latitude(EPrecision)          » MMars.Latitude.cs
	// MMars.Latitude(EPrecision, double)  » MMars.Latitude.cs
	// MMars.Longitude(EPrecision)         » MMars.Longitude.cs
	// MMars.Longitude(EPrecision, double) » MMars.Longitude.cs

	// MMars.LongitudeOfAscendingNode()
	/// <summary>
	/// Liefert die Länge des aufsteigenden Knotens der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Länge des aufsteigenden Knotens der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double LongitudeOfAscendingNode(){ return MMars.LongitudeOfAscendingNode(DateTime.Now.ToJdn()); }

	// MMars.LongitudeOfAscendingNode(double)
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
		return MMath.Polynome(t, 49.558093, 0.7720959, 0.00001557, 0.000002267);
	}

	// MMars.LongitudeOfPerihelion()
	/// <summary>
	/// Liefert die Länge des Perihels der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Länge des Perihels der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double LongitudeOfPerihelion(){ return MMars.LongitudeOfPerihelion(DateTime.Now.ToJdn()); }

	// MMars.LongitudeOfPerihelion(double)
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
		return MMath.Polynome(t, 336.060234, 1.8410449, 0.00013477, 0.000000536);
	}

	// MMars.MeanAnomaly()
	/// <summary>
	/// Liefert die mittlere Anomalie der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Mittlere Anomalie der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double MeanAnomaly(){ return MMars.MeanAnomaly(DateTime.Now.ToJdn()); }

	// MMars.MeanAnomaly(double)
	/// <summary>
	/// Liefert die mittlere Anomalie der mittleren Planetenbahn zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Mittlere Anomalie der mittleren Planetenbahn zur julianischen Tageszahl.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double MeanAnomaly(double jd){ return MMod.Mod(MMars.MeanLongitude(jd) + MMars.LongitudeOfPerihelion(jd), 360.0); }

	// MMars.MeanLongitude()
	/// <summary>
	/// Liefert die mittlere Länge der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Mittlere Länge der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double MeanLongitude(){ return MMars.MeanLongitude(DateTime.Now.ToJdn()); }

	// MMars.MeanLongitude(double)
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
		return MMod.Mod(MMath.Polynome(t, 355.433000, 19141.6964471, 0.00031052, 0.000000016), 360.0);
	}

	// MMars.Opposition()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die Opposition mit der Sonne nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die Opposition mit der Sonne nach der aktuellen Systemzeit.</returns>
	public static double Opposition(){ return MMars.Opposition(DateTime.Now.ToJdn()); }

	// MMars.Opposition(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die Opposition mit der Sonne nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die Opposition mit der Sonne nach der aktuellen Systemzeit.</returns>
	public static double Opposition(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor((365.2425 * y + 1721060.0 - 2452097.382) / 779.936104) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while (j <= jd)
		{
			// Näherung berechnen
			k += 1.0;
			j  = 2452097.382 + k * 779.936104;

			// Hilfsfelder berechnen
			double m = MMod.Mod(MMath.ToRad(181.9573 + k * 48.705244), MMath.Pi2);
			double t = (j - MCalendar.Jdn20000101) / 36525.0;
			double h;

			// Korrektur berechnen und anwenden
			h  = MMath.Polynome(t,  -0.3088,  0.0000,  0.00002);
			h += MMath.Polynome(t, -17.6965,  0.0363,  0.00005) * MMath.Sin(      m);
			h += MMath.Polynome(t,  18.3131,  0.0467, -0.00006) * MMath.Cos(      m);
			h += MMath.Polynome(t,  -0.2162, -0.0198, -0.00001) * MMath.Sin(2.0 * m);
			h += MMath.Polynome(t,  -4.5028, -0.0019,  0.00007) * MMath.Cos(2.0 * m);
			h += MMath.Polynome(t,   0.8987,  0.0058, -0.00002) * MMath.Sin(3.0 * m);
			h += MMath.Polynome(t,   0.7666, -0.0050, -0.00003) * MMath.Cos(3.0 * m);
			h += MMath.Polynome(t,  -0.3636, -0.0001,  0.00002) * MMath.Sin(4.0 * m);
			h += MMath.Polynome(t,   0.0402,  0.0023          ) * MMath.Cos(4.0 * m);
			h += MMath.Polynome(t,   0.0737, -0.0008          ) * MMath.Sin(5.0 * m);
			h += MMath.Polynome(t,  -0.0980, -0.0011          ) * MMath.Cos(5.0 * m);
			j += h;
		}
		return j;
	}

	// MMars.Perihelion()
	/// <summary>
	/// Liefert die julianische Tageszahl der nächsten Durchgangs durch das Perihel nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl der nächsten Durchgangs durch das Perihel nach der aktuellen Systemzeit.</returns>
	public static double Perihelion(){ return MMars.Perihelion(DateTime.Now.ToJdn()); }

	// MMars.Perihelion(double)
	/// <summary>
	/// Liefert die julianische Tageszahl der nächsten Durchgangs durch das Perihel nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl der nächsten Durchgangs durch das Perihel nach der julianischen Tageszahl.</returns>
	public static double Perihelion(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor(0.53166 * (y - 2001.78)) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while(j <= jd)
		{
			// Tageszahl berechnen
			k += 1.0;
			j  = MMath.Polynome(k, 2452195.026, 686.9957857, -0.0000001187);
		}
		return j;
	}

	// MMars.PositionEcliptical(EPrecision)
	/// <summary>
	/// Liefert die heliozentrisch-ekliptikale Position zur aktuellen Systemzeit.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <returns>Heliozentrisch-ekliptikale Position zur aktuellen Systemzeit.</returns>
	public static CPolar PositionEcliptical(EPrecision value){ return MMars.PositionEcliptical(value, DateTime.Now.ToJdn()); }

	// MMars.PositionEcliptical(EPrecision, double)
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
		rtn.Latitude  = MMars.Latitude (value, jd);
		rtn.Longitude = MMars.Longitude(value, jd);
		rtn.Radius    = MMars.Radius   (value, jd);
		return rtn;
	}

	// MMars.PositionEquatorial()
	/// <summary>
	/// Liefert die (scheinbare) geozentrisch-äquatoriale Position zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Geozentrisch-äquatoriale Position zur aktuellen Systemzeit.</returns>
	public static CPolar PositionEquatorial(){ return MMars.PositionEquatorial(DateTime.Now.ToJdn()); }

	// MMars.PositionEquatorial(double)
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
			bH = MMars.Latitude (EPrecision.High, jdn);
			lH = MMars.Longitude(EPrecision.High, jdn);
			rH = MMars.Radius   (EPrecision.High, jdn);

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

	// MMars.Radius(EPrecision)         » MMars.Radius.cs
	// MMars.Radius(EPrecision, double) » MMars.Radius.cs

	// MMars.RegressionEnd()
	/// <summary>
	/// Liefert die julianische Tageszahl des Rückläufigkeitsendes nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des Rückläufigkeitsendes nach der aktuellen Systemzeit.</returns>
	public static double RegressionEnd(){ return MMars.RegressionEnd(DateTime.Now.ToJdn()); }

	// MMars.RegressionEnd(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des Rückläufigkeitsendes nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des Rückläufigkeitsendes nach der julianischen Tageszahl.</returns>
	public static double RegressionEnd(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor((365.2425 * y + 1721060.0 - 2452097.382) / 779.936104) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while (j <= jd)
		{
			// Näherung berechnen
			k += 1.0;
			j  = 2452097.382 + k * 779.936104;

			// Hilfsfelder berechnen
			double m = MMod.Mod(MMath.ToRad(181.9573 + k * 48.705244), MMath.Pi2);
			double t = (j - MCalendar.Jdn20000101) / 36525.0;
			double h;

			// Korrektur berechnen und anwenden
			h  = MMath.Polynome(t,  36.7191,  0.0016,  0.00003);
			h += MMath.Polynome(t, -12.6163,  0.0417, -0.00001) * MMath.Sin(      m);
			h += MMath.Polynome(t,  20.1218,  0.0379, -0.00006) * MMath.Cos(      m);
			h += MMath.Polynome(t,  -1.6360, -0.0190          ) * MMath.Sin(2.0 * m);
			h += MMath.Polynome(t,  -3.9657,  0.0045,  0.00007) * MMath.Cos(2.0 * m);
			h += MMath.Polynome(t,   1.1546,  0.0029, -0.00003) * MMath.Sin(3.0 * m);
			h += MMath.Polynome(t,   0.2888, -0.0073, -0.00002) * MMath.Cos(3.0 * m);
			h += MMath.Polynome(t,  -0.3128,  0.0017,  0.00002) * MMath.Sin(4.0 * m);
			h += MMath.Polynome(t,   0.2513,  0.0026, -0.00002) * MMath.Cos(4.0 * m);
			h += MMath.Polynome(t,  -0.0021, -0.0016          ) * MMath.Sin(5.0 * m);
			h += MMath.Polynome(t,  -0.1497, -0.0006          ) * MMath.Cos(5.0 * m);
			j += h;
		}
		return j;
	}

	// MMars.RegressionStart()
	/// <summary>
	/// Liefert die julianische Tageszahl der Rückläufigkeitsanfangs nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl der Rückläufigkeitsanfangs nach der aktuellen Systemzeit.</returns>
	public static double RegressionStart(){ return MMars.RegressionStart(DateTime.Now.ToJdn()); }

	// MMars.RegressionStart(double)
	/// <summary>
	/// Liefert die julianische Tageszahl der Rückläufigkeitsanfangs nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl der Rückläufigkeitsanfangs nach der julianischen Tageszahl.</returns>
	public static double RegressionStart(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor((365.2425 * y + 1721060.0 - 2452097.382) / 779.936104) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while (j <= jd)
		{
			// Näherung berechnen
			k += 1.0;
			j  = 2452097.382 + k * 779.936104;

			// Hilfsfelder berechnen
			double m = MMod.Mod(MMath.ToRad(181.9573 + k * 48.705244), MMath.Pi2);
			double t = (j - MCalendar.Jdn20000101) / 36525.0;
			double h;

			// Korrektur berechnen und anwenden
			h  = MMath.Polynome(t, -37.0790, -0.0009,  0.00002);
			h += MMath.Polynome(t, -20.0651,  0.0228,  0.00004) * MMath.Sin(      m);
			h += MMath.Polynome(t,  14.5205,  0.0504, -0.00001) * MMath.Cos(      m);
			h += MMath.Polynome(t,   1.1737, -0.0169          ) * MMath.Sin(2.0 * m);
			h += MMath.Polynome(t,  -4.2550, -0.0075,  0.00008) * MMath.Cos(2.0 * m);
			h += MMath.Polynome(t,   0.4897,  0.0074, -0.00001) * MMath.Sin(3.0 * m);
			h += MMath.Polynome(t,   1.1151, -0.0021, -0.00005) * MMath.Cos(3.0 * m);
			h += MMath.Polynome(t,  -0.3636, -0.0020,  0.00001) * MMath.Sin(4.0 * m);
			h += MMath.Polynome(t,  -0.1769,  0.0028,  0.00002) * MMath.Cos(4.0 * m);
			h += MMath.Polynome(t,   0.1437, -0.0004          ) * MMath.Sin(5.0 * m);
			h += MMath.Polynome(t,  -0.0383, -0.0016          ) * MMath.Cos(5.0 * m);
			j += h;
		}
		return j;
	}

	// MMars.Rise(CPolar)
	/// <summary>
	/// Liefert die Ereigniskennung, die julianische Tageszahl des Aufgangs und die Morgenweite am geographischen Ort und zum aktuellen Systemdatum.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <returns>Ereigniskennung, die julianische Tageszahl des Aufgangs und die Morgenweite am geographischen Ort und zum aktuellen Systemdatum.</returns>
	public static (EEventType type, double? jd, double? azimuth) Rise(CPolar position){ return MMars.Rise(position.Longitude, position.Latitude, DateTime.Now.ToJdn()); }

	// MMars.Rise(CPolar, double)
	/// <summary>
	/// Liefert die Ereigniskennung, die julianische Tageszahl des Aufgangs und die Morgenweite am geographischen Ort und zum julianischen Tagedatum.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <param name="jd">Julianisches Tagesdatum.</param>
	/// <returns>Ereigniskennung, die julianische Tageszahl des Aufgangs und die Morgenweite am geographischen Ort und zum julianischen Tagedatum.</returns>
	public static (EEventType type, double? jd, double? azimuth) Rise(CPolar position, double jd){ return MMars.Rise(position.Longitude, position.Latitude, jd); }

	// MMars.Rise(double, double)
	/// <summary>
	/// Liefert die Ereigniskennung, die julianische Tageszahl des Aufgangs und die Morgenweite am geographischen Ort und zum aktuellen Systemdatum.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <returns>Ereigniskennung, die julianische Tageszahl des Aufgangs und die Morgenweite am geographischen Ort und zum aktuellen Systemdatum.</returns>
	public static (EEventType type, double? jd, double? azimuth) Rise(double lambda, double phi){ return MMars.Rise(lambda, phi, DateTime.Now.ToJdn()); }

	// MMars.Rise(double, double, double)
	/// <summary>
	/// Liefert die Ereigniskennung, die julianische Tageszahl der Aufgangs und die Morgenweite am geographischen Ort und zum julianischen Tagedatum.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <param name="jd">Julianisches Tagesdatum.</param>
	/// <returns>Ereigniskennung, die julianische Tageszahl der Aufgangs und die Morgenweite am geographischen Ort und zum julianischen Tagedatum.</returns>
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
		double H    = 0.0;                                //
		double sinP = MMath.Sin(phi);                     // Breitensinus
		double cosP = MMath.Cos(phi);                     // Breitencosinus

		// Position für nachfolgenden Tag berechnen
		l = MMars.Longitude(EPrecision.Low, jdn + 1.0);
		b = MMars.Latitude (EPrecision.Low, jdn + 1.0);
		double aP = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jdn + 1.0);
		double dP = MEphemerides.ToDelta(l, b, EObliquity.Mean, jdn + 1.0);

		// Position für gegebenen Tag berechnen
		l = MMars.Longitude(EPrecision.Low, jdn);
		b = MMars.Latitude (EPrecision.Low, jdn);
		double a0 = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jdn);
		if(MMath.Abs(aP - a0) > 1.0)
			a0 += MMath.Sgn(aP - a0) * MMath.Pi2;
		double d0 = MEphemerides.ToDelta(l, b, EObliquity.Mean, jdn);

		// Position für vorhergehenden Tag berechnen
		l = MMars.Longitude(EPrecision.Low, jdn - 1.0);
		b = MMars.Latitude (EPrecision.Low, jdn - 1.0);
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
		double m = MMath.Div((a0 + lambda - t0 - H) / MMath.Pi2);
		if(m < 0.0)
			m += 1.0;

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

	// MMars.SemimajorAxis()
	/// <summary>
	/// Liefert die große Halbachse der mittleren Planetenbahn.
	/// </summary>
	/// <returns>Große Halbachse der mittleren Planetenbahn.</returns>
	public static double SemimajorAxis(){ return 1.523679342; }

	// MMars.Set(CPolar)
	/// <summary>
	/// Liefert die Ereigniskennung, die juliansiche Tageszahl des Untergangs und die Abendweite am geographischen Ort und zum aktuellen Systemdatum.
	/// </summary>
	/// <param name="position">Geographisches Position.</param>
	/// <returns>Ereigniskennung, die juliansiche Tageszahl des Untergangs und die Abendweite am geographischen Ort und zum aktuellen Systemdatum.</returns>
	public static (EEventType type, double? jd, double? azimuth) Set(CPolar position){ return MMars.Set(position.Longitude, position.Latitude, DateTime.Now.ToJdn()); }

	// MMars.Set(CPolar, double)
	/// <summary>
	/// Liefert die Ereigniskennung, die juliansiche Tageszahl des Untergangs und die Abendweite am geographischen Ort und zum julianischen Tagesdatum.
	/// </summary>
	/// <param name="position">Geographisches Position.</param>
	/// <param name="jd">Julianisches Tagesdatum.</param>
	/// <returns>Ereigniskennung, die juliansiche Tageszahl des Untergangs und die Abendweite am geographischen Ort und zum julianischen Tagesdatum.</returns>
	public static (EEventType type, double? jd, double? azimuth) Set(CPolar position, double jd){ return MMars.Set(position.Longitude, position.Latitude, jd); }

	// MMars.Set(double, double)
	/// <summary>
	/// Liefert die Ereigniskennung, die julianische Tageszahl des Untergangs und die Abendweite am geographischen Ort und zum aktuellen Systemdatum.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <returns>Ereigniskennung, die julianische Tageszahl des Untergangs und die Abendweite am geographischen Ort und zum aktuellen Systemdatum.</returns>
	public static (EEventType type, double? jd, double? azimuth) Set(double lambda, double phi){ return MMars.Set(lambda, phi, DateTime.Now.ToJdn()); }

	// MMars.Set(double, double, double)
	/// <summary>
	/// Liefert die Ereigniskennung, die julianische Tageszahl des Untergangs und die Abendweite am geographischen Ort und zum julianischen Tagedatum.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <param name="jd">Julianisches Tagesdatum.</param>
	/// <returns>Ereigniskennung, die julianische Tageszahl des Untergangs und die Abendweite am geographischen Ort und zum julianischen Tagedatum.</returns>
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
		l = MMars.Longitude(EPrecision.Low, jdn + 1.0);
		b = MMars.Latitude (EPrecision.Low, jdn + 1.0);
		double aP = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jd + 1.0);
		double dP = MEphemerides.ToDelta(l, b, EObliquity.Mean, jd + 1.0);

		// Position für gegebenen Tag berechnen
		l = MMars.Longitude(EPrecision.Low, jdn);
		b = MMars.Latitude (EPrecision.Low, jdn);
		double a0 = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jdn);
		if(MMath.Abs(aP - a0) > 1.0)
			a0 += MMath.Sgn(aP - a0) * MMath.Pi2;
		double d0 = MEphemerides.ToDelta(l, b, EObliquity.Mean, jdn);

		// Position für vorhergehenden Tag berechnen
		l = MMars.Longitude(EPrecision.Low, jdn - 1.0);
		b = MMars.Latitude (EPrecision.Low, jdn - 1.0);
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

		// Kein Ereignis verarbeiten
		if(m < 0.0 | m >= 1.0)
			return(EEventType.NoEvent, null, null);

		// Rückgabe
		return(EEventType.Normal, jd + m, MEphemerides.ToAzimuth(H, d, phi));
	}

	// MMars.SynodicPeriod()
	/// <summary>
	/// Liefert die mittlere synodische Periode zur mittleren Planetenbahn.
	/// </summary>
	/// <returns>Mittlere synodische Periode zur mittleren Planetenbahn.</returns>
	public static double SynodicPeriod(){ return 779.936105; }

	// MMars.Transit(CPolar)
	/// <summary>
	/// Liefert die Ereigniskennung, die julianische Tageszahl des Meridiandurchgangs und die horizontale Höhe am geographischen Ort und zum aktuellen Systemdatum.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <returns>Ereigniskennunt, die julianische Tageszahl des Meridiandurchgangs und die horizontale Höhe am geographischen Ort und zum aktuellen Systemdatum.</returns>
	public static (EEventType type, double? jd, double? height) Transit(CPolar position){ return MMars.Transit(position.Longitude, position.Latitude, DateTime.Now.ToJdn()); }

	// MMars.Transit(CPolar, double)
	/// <summary>
	/// Liefert die Ereigniskennung, die julianische Tageszahl des Meridiandurchgangs und die horizontale Höhe am geographischen Ort und zum julianischen Tagedatum.
	/// </summary>
	/// <param name="pos">Geographische Position.</param>
	/// <param name="jd">Julianisches Tagesdatum.</param>
	/// <returns>Ereigniskennung, die julianische Tageszahl des Meridiandurchgangs und die horizontale Höhe am geographischen Ort und zum julianischen Tagedatum.</returns>
	public static (EEventType type, double? jd, double? height) Transit(CPolar position, double jd){ return MMars.Transit(position.Longitude, position.Latitude, jd); }

	// MMars.Transit(double, double)
	/// <summary>
	/// Liefert Ereigniskennung, die julianische Tageszahl des Meridiandurchgangs und die horizontale Höhe am geographischen Ort und zum aktuellen Systemdatum.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <returns>Ereigniskennung, die julianische Tageszahl des Meridiandurchgangs und die horizontale Höhe am geographischen Ort und zum aktuellen Systemdatum.</returns>
	public static (EEventType type, double? jd, double? height) Transit(double lambda, double phi){ return MMars.Transit(lambda, phi, DateTime.Now.ToJdn()); }

	// MMars.Transit(double, double, double)
	/// <summary>
	/// Liefert die julianische Tageszahl des Meridiandurchgangs und die horizontale Höe am geographischen Ort und zum julianischen Tagedatum.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <param name="jd">Julianisches Tagesdatum.</param>
	/// <returns>Julianische Tageszahl des Meridiandurchgangs und die horizontale Höe am geographischen Ort und zum julianischen Tagedatum.</returns>
	public static (EEventType type, double? jd, double? height) Transit(double lambda, double phi, double jd)
	{
		// Lokale Felder einrichten
		double jdn = MMath.Floor(jd - 0.5) + 0.5; // Tageszahl um Mitternacht
		double l   = 0.0;                         // Geozentrische Länge
		double b   = 0.0;                         // Geozentrische Breite
		double a   = 0.0;                         // Rektaszension
		double dm  = 1.0;                         // Korrekturglied

		// Position für nachfolgenden Tag berechnen
		l = MMars.Longitude(EPrecision.Low, jdn + 1.0);
		b = MMars.Latitude (EPrecision.Low, jdn + 1.0);
		double aP = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jdn + 1.0);
		double dP = MEphemerides.ToDelta(l, b, EObliquity.Mean, jdn + 1.0);

		// Position für gegebenen Tag berechnen
		l = MMars.Longitude(EPrecision.Low, jdn);
		b = MMars.Latitude (EPrecision.Low, jdn);
		double a0 = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jdn);
		if(MMath.Abs(aP - a0) > 1.0)
			a0 += MMath.Sgn(aP - a0) * MMath.Pi2;
		double d0 = MEphemerides.ToDelta(l, b, EObliquity.Mean, jdn);

		// Position für vorhergehenden Tag berechnen
		l = MMars.Longitude(EPrecision.Low, jdn - 1.0);
		b = MMars.Latitude (EPrecision.Low, jdn - 1.0);
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
			if(MMath.Abs(dm) > 0.5)
				dm -= MMath.Sgn(dm);
			m += dm;
		}

		// Kein Ereignis verarbeiten
		if(m < 0.0 | m >= 1.0)
			return(EEventType.NoEvent, null, null);

		// Rückgabe
		return(EEventType.Normal, jd + m, MEphemerides.ToHeight(0.0, MMath.Bessel(m, dM, d0, dP), phi));
	}

	// MMars.TropicalPeriod()
	/// <summary>
	/// Liefert die mittlere tropische Periode zur mittleren Planetenbahn.
	/// </summary>
	/// <returns>Mittlere tropische Periode zur mittleren Planetenbahn.</returns>
	public static double TropicalPeriod(){ return 686.929711; }
}
