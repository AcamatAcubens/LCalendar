using Acamat.LCore;
using Acamat.LMath;
using Acamat.LMath.Geometry;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt Berechnungen zum Saturn.
/// </summary>
public partial class MSaturn
{
	// ------------------- //
	// Felder und Methoden //
	// ------------------- //
	// MSaturn.Aphelion()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch das Aphel nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch das Aphel nach der aktuellen Systemzeit.</returns>
	public static double Aphelion(){ return MSaturn.Aphelion(DateTime.Now.ToJdn()); }

	// MSaturn.Aphelion(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch das Aphel nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch das Aphel nach der julianischen Tageszahl.</returns>
	public static double Aphelion(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor(0.03393 * (y - 2003.52)) - 0.5;
		double j = 0.0;

		// Berechnungsschleife
		while(j <= jd)
		{
			// Tageszahl berechnen
			k += 1.0;
			j  = MMath.Polynome(k, 2452830.12, 10764.21676, 0.000827);
		}
		return j;
	}

	// MSaturn.Conjunction()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die obere Konjunktion mit der Sonne nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die obere Konjunktion mit der Sonne nach der aktuellen Systemzeit.</returns>
	public static double Conjunction(){ return MSaturn.Conjunction(DateTime.Now.ToJdn()); }

	// MSaturn.Conjuction(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die Konjunktion mit der Sonne nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die Konjunktion mit der Sonne nach der julianischen Tageszahl.</returns>
	public static double Conjunction(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor((365.2425 * y + 1721060.0 - 2451681.124) / 378.091904) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while (j <= jd)
		{
			// Näherung berechnen
			k += 1.0;
			j  = 2451681.124 + k * 378.091904;

			// Hilfsfelder berechnen
			double m = MMod.Mod(MMath.ToRad(131.6934 + k * 12.647487), MMath.Pi2);
			double t = (j - MCalendar.Jdn20000101) / 36525.0;
			double a = MMod.Mod(MMath.ToRad( 82.74 +   40.76 * t), MMath.Pi2);
			double b = MMod.Mod(MMath.ToRad( 29.86 + 1181.36 * t), MMath.Pi2);
			double c = MMod.Mod(MMath.ToRad( 14.13 +  590.68 * t), MMath.Pi2);
			double d = MMod.Mod(MMath.ToRad(220.02 + 1262.87 * t), MMath.Pi2);
			double h;

			// Korrektur berechnen und anwenden
			h  = MMath.Polynome(t,  0.0172, -0.0006,  0.00023);
			h += MMath.Polynome(t, -8.5885,  0.0411,  0.00020) * MMath.Sin(      m);
			h += MMath.Polynome(t, -1.1470,  0.0352, -0.00011) * MMath.Cos(      m);
			h += MMath.Polynome(t,  0.3331, -0.0034, -0.00001) * MMath.Sin(2.0 * m);
			h += MMath.Polynome(t,  0.1145, -0.0045,  0.00002) * MMath.Cos(2.0 * m);
			h += MMath.Polynome(t, -0.0169,  0.0002          ) * MMath.Sin(3.0 * m);
			h += MMath.Polynome(t, -0.0109,  0.0004          ) * MMath.Cos(3.0 * m);
			h += MMath.Polynome(t,  0.0000, -0.0337,  0.00018) * MMath.Sin(      a);
			h += MMath.Polynome(t, -0.8510,  0.0044,  0.00068) * MMath.Cos(      a);
			h += MMath.Polynome(t,  0.0000, -0.0064,  0.00004) * MMath.Sin(      b);
			h += MMath.Polynome(t,  0.2397, -0.0012, -0.00008) * MMath.Cos(      b);
			h += MMath.Polynome(t,  0.0000, -0.0010          ) * MMath.Sin(      c);
			h += MMath.Polynome(t,  0.1245,  0.0006          ) * MMath.Cos(      c);
			h += MMath.Polynome(t,  0.0000,  0.0024, -0.00003) * MMath.Sin(      d);
			h += MMath.Polynome(t,  0.0477, -0.0005, -0.00006) * MMath.Cos(      d);
			j += h;
		}
		return j;
	}

	// MSaturn.Eccentricity()
	/// <summary>
	/// Liefert die Exzentrizität der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Exzentrizität der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	public static double Eccentricity(){ return MSaturn.Eccentricity(DateTime.Now.ToJdn()); }

	// MSaturn.Eccentricity(double)
	/// <summary>
	/// Liefert die Exzentrizität der mittleren Planetenbahn zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Exzentrizität der mittleren Planetenbahn zur julianischen Tageszahl.</returns>
	public static double Eccentricity(double jd)
	{
		// Lokale Felder einrichten und Exzentrizität berechnen
		double t = (jd - MCalendar.Jdn20000101) / 36525.0;
		return MMath.Polynome(t, 0.05554814, -0.000346641, -0.0000006436, -0.00000000340);
	}

	// MSaturn.Inclination()
	/// <summary>
	/// Liefert die Neigung der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Neigung der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double Inclination(){ return MSaturn.Inclination(DateTime.Now.ToJdn());}

	// MSaturn.Inclination(double)
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
		return MMath.Polynome(t, 2.488879, -0.0037362, -0.00001519, 0.000000087);
	}

	// MSaturn.Latitude(EPrecision)          » MSaturn.Latitude.cs
	// MSaturn.Latitude(EPrecision, double)  » MSaturn.Latitude.cs
	// MSaturn.Longitude(EPrecision)         » MSaturn.Longitude.cs
	// MSaturn.Longitude(EPrecision, double) » MSaturn.Longitude.cs

	// MSaturn.LongitudeOfAscendingNode()
	/// <summary>
	/// Liefert die Länge des aufsteigenden Knotens der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Länge des aufsteigenden Knotens der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double LongitudeOfAscendingNode(){ return MSaturn.LongitudeOfAscendingNode(DateTime.Now.ToJdn()); }

	// MSaturn.LongitudeOfAscendingNode(double)
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
		return MMath.Polynome(t, 113.665503, 0.8770880, -0.00012176, -0.000002249);
	}

	// MSaturn.LongitudeOfPerihelion()
	/// <summary>
	/// Liefert die Länge des Perihels der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Länge des Perihels der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double LongitudeOfPerihelion(){ return MSaturn.LongitudeOfPerihelion(DateTime.Now.ToJdn()); }

	// MSaturn.LongitudeOfPerihelion(double)
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
		return MMath.Polynome(t, 93.057237, 1.9637613, 0.00083753, 0.000004928);
	}

	// MSaturn.MeanAnomaly()
	/// <summary>
	/// Liefert die mittlere Anomalie der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Mittlere Anomalie der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double MeanAnomaly(){ return MSaturn.MeanAnomaly(DateTime.Now.ToJdn()); }

	// MSaturn.MeanAnomaly(double)
	/// <summary>
	/// Liefert die mittlere Anomalie der mittleren Planetenbahn zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Mittlere Anomalie der mittleren Planetenbahn zur julianischen Tageszahl.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double MeanAnomaly(double jd){ return MMod.Mod(MSaturn.MeanLongitude(jd) + MSaturn.LongitudeOfPerihelion(jd), 360.0); }

	// MSaturn.MeanLongitude()
	/// <summary>
	/// Liefert die mittlere Länge der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Mittlere Länge der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double MeanLongitude(){ return MSaturn.MeanLongitude(DateTime.Now.ToJdn()); }

	// MSaturn.MeanLongitude(double)
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
		return MMod.Mod(MMath.Polynome(t, 50.077444, 1223.5110686, 0.00051908, -0.000000030), 360.0);
	}

	// MSaturn.Opposition()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die Opposition mit der Sonne nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die Opposition mit der Sonne nach der aktuellen Systemzeit.</returns>
	public static double Opposition(){ return MSaturn.Opposition(DateTime.Now.ToJdn()); }

	// MSaturn.Opposition(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die Opposition mit der Sonne nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die Opposition mit der Sonne nach der aktuellen Systemzeit.</returns>
	public static double Opposition(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor((365.2425 * y + 1721060.0 - 2451870.170) / 378.091904) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while (j <= jd)
		{
			// Näherung berechnen
			k += 1.0;
			j  = 2451870.170 + k * 378.091904;

			// Hilfsfelder berechnen
			double m = MMod.Mod(MMath.ToRad(318.0172 + k * 12.647487), MMath.Pi2);
			double t = (j - MCalendar.Jdn20000101) / 36525.0;
			double a = MMod.Mod(MMath.ToRad( 82.74 +   40.76 * t), MMath.Pi2);
			double b = MMod.Mod(MMath.ToRad( 29.86 + 1181.36 * t), MMath.Pi2);
			double c = MMod.Mod(MMath.ToRad( 14.13 +  590.68 * t), MMath.Pi2);
			double d = MMod.Mod(MMath.ToRad(220.02 + 1262.87 * t), MMath.Pi2);
			double h;

			// Korrektur berechnen und anwenden
			h  = MMath.Polynome(t, -0.0209,  0.0006,  0.00023);
			h += MMath.Polynome(t,  4.5795, -0.0312, -0.00017) * MMath.Sin(      m);
			h += MMath.Polynome(t,  1.1462, -0.0351,  0.00011) * MMath.Cos(      m);
			h += MMath.Polynome(t,  0.0985, -0.0015          ) * MMath.Sin(2.0 * m);
			h += MMath.Polynome(t,  0.0733, -0.0031,  0.00001) * MMath.Cos(2.0 * m);
			h += MMath.Polynome(t,  0.0025, -0.0001          ) * MMath.Sin(3.0 * m);
			h += MMath.Polynome(t,  0.0050, -0.0002          ) * MMath.Cos(3.0 * m);
			h += MMath.Polynome(t,  0.0000, -0.0337,  0.00018) * MMath.Sin(      a);
			h += MMath.Polynome(t, -0.8510,  0.0044,  0.00068) * MMath.Cos(      a);
			h += MMath.Polynome(t,  0.0000, -0.0064,  0.00004) * MMath.Sin(      b);
			h += MMath.Polynome(t,  0.2397, -0.0012, -0.00008) * MMath.Cos(      b);
			h += MMath.Polynome(t,  0.0000, -0.0010          ) * MMath.Sin(      c);
			h += MMath.Polynome(t,  0.1245,  0.0006          ) * MMath.Cos(      c);
			h += MMath.Polynome(t,  0.0000,  0.0024, -0.00003) * MMath.Sin(      d);
			h += MMath.Polynome(t,  0.0477, -0.0005, -0.00006) * MMath.Cos(      d);
			j += h;
		}
		return j;
	}

	// MSaturn.Perihelion()
	/// <summary>
	/// Liefert die julianische Tageszahl der nächsten Durchgangs durch das Perihel nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl der nächsten Durchgangs durch das Perihel nach der aktuellen Systemzeit.</returns>
	public static double Perihelion(){ return MSaturn.Perihelion(DateTime.Now.ToJdn()); }

	// MSaturn.Perihelion(double)
	/// <summary>
	/// Liefert die julianische Tageszahl der nächsten Durchgangs durch das Perihel nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl der nächsten Durchgangs durch das Perihel nach der julianischen Tageszahl.</returns>
	public static double Perihelion(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor(0.03393 * (y - 2003.52)) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while(j <= jd)
		{
			// Tageszahl berechnen
			k += 1.0;
			j  = MMath.Polynome(k, 2452830.12, 10764.21676, 0.000827);
		}
		return j;
	}

	// MSaturn.PositionEcliptical(EPrecision)
	/// <summary>
	/// Liefert die heliozentrisch-ekliptikale Position zur aktuellen Systemzeit.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <returns>Heliozentrisch-ekliptikale Position zur aktuellen Systemzeit.</returns>
	public static CPolar PositionEcliptical(EPrecision value){ return MSaturn.PositionEcliptical(value, DateTime.Now.ToJdn()); }

	// MSaturn.PositionEcliptical(EPrecisionList, double)
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
		rtn.Latitude  = MSaturn.Latitude (value, jd);
		rtn.Longitude = MSaturn.Longitude(value, jd);
		rtn.Radius    = MSaturn.Radius   (value, jd);
		return rtn;
	}

	// MSaturn.PositionEquatorial()
	/// <summary>
	/// Liefert die (scheinbare) geozentrisch-äquatoriale Position zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Geozentrisch-äquatoriale Position zur aktuellen Systemzeit.</returns>
	public static CPolar PositionEquatorial(){ return MSaturn.PositionEquatorial(DateTime.Now.ToJdn()); }

	// MSaturn.PositionEquatorial(double)
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
			bH = MSaturn.Latitude (EPrecision.High, jdn);
			lH = MSaturn.Longitude(EPrecision.High, jdn);
			rH = MSaturn.Radius   (EPrecision.High, jdn);

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

	// MSaturn.Radius(EPrecision)         » MSaturn.Radius.cs
	// MSaturn.Radius(EPrecision, double) » MSaturn.Radius.cs

	// MSaturn.RegressionEnd()
	/// <summary>
	/// Liefert die julianische Tageszahl des Rückläufigkeitsendes nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des Rückläufigkeitsendes nach der aktuellen Systemzeit.</returns>
	public static double RegressionEnd(){ return MSaturn.RegressionEnd(DateTime.Now.ToJdn()); }

	// MSaturn.RegressionEnd(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des Rückläufigkeitsendes nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des Rückläufigkeitsendes nach der julianischen Tageszahl.</returns>
	public static double RegressionEnd(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor((365.2425 * y + 1721060.0 - 2451870.170) / 378.091904) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while (j <= jd)
		{
			// Näherung berechnen
			k += 1.0;
			j  = 2451870.170 + k * 378.091904;

			// Hilfsfelder berechnen
			double m = MMod.Mod(MMath.ToRad(318.0172 + k * 12.647487), MMath.Pi2);
			double t = (j - MCalendar.Jdn20000101) / 36525.0;
			double a = MMod.Mod(MMath.ToRad( 82.74 +   40.76 * t), MMath.Pi2);
			double b = MMod.Mod(MMath.ToRad( 29.86 + 1181.36 * t), MMath.Pi2);
			double c = MMod.Mod(MMath.ToRad( 14.13 +  590.68 * t), MMath.Pi2);
			double d = MMod.Mod(MMath.ToRad(220.02 + 1262.87 * t), MMath.Pi2);
			double h;

			// Korrektur berechnen und anwenden
			h  = MMath.Polynome(t, 68.8720, -0.0007,  0.00023);
			h += MMath.Polynome(t,  5.9399, -0.0400, -0.00015) * MMath.Sin(      m);
			h += MMath.Polynome(t, -0.7998, -0.0266,  0.00014) * MMath.Cos(      m);
			h += MMath.Polynome(t,  0.1738, -0.0032          ) * MMath.Sin(2.0 * m);
			h += MMath.Polynome(t, -0.0039, -0.0024,  0.00001) * MMath.Cos(2.0 * m);
			h += MMath.Polynome(t,  0.0073, -0.0002          ) * MMath.Sin(3.0 * m);
			h += MMath.Polynome(t,  0.0020, -0.0002          ) * MMath.Cos(3.0 * m);
			h += MMath.Polynome(t,  0.0000, -0.0337,  0.00018) * MMath.Sin(      a);
			h += MMath.Polynome(t, -0.8510,  0.0044,  0.00068) * MMath.Cos(      a);
			h += MMath.Polynome(t,  0.0000, -0.0064,  0.00004) * MMath.Sin(      b);
			h += MMath.Polynome(t,  0.2397, -0.0012, -0.00008) * MMath.Cos(      b);
			h += MMath.Polynome(t,  0.0000, -0.0010          ) * MMath.Sin(      c);
			h += MMath.Polynome(t,  0.1245,  0.0006          ) * MMath.Cos(      c);
			h += MMath.Polynome(t,  0.0000,  0.0024, -0.00003) * MMath.Sin(      d);
			h += MMath.Polynome(t,  0.0477, -0.0005, -0.00006) * MMath.Cos(      d);
			j += h;
		}
		return j;
	}

	// MSaturn.RegressionStart()
	/// <summary>
	/// Liefert die julianische Tageszahl der Rückläufigkeitsanfangs nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl der Rückläufigkeitsanfangs nach der aktuellen Systemzeit.</returns>
	public static double RegressionStart(){ return MSaturn.RegressionStart(DateTime.Now.ToJdn()); }

	// MSaturn.RegressionStart(double)
	/// <summary>
	/// Liefert die julianische Tageszahl der Rückläufigkeitsanfangs nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl der Rückläufigkeitsanfangs nach der julianischen Tageszahl.</returns>
	public static double RegressionStart(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor((365.2425 * y + 1721060.0 - 2451870.170) / 378.091904) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while (j <= jd)
		{
			// Näherung berechnen
			k += 1.0;
			j  = 2451870.170 + k * 378.091904;

			// Hilfsfelder berechnen
			double m = MMod.Mod(MMath.ToRad(318.0172 + k * 12.647487), MMath.Pi2);
			double t = (j - MCalendar.Jdn20000101) / 36525.0;
			double a = MMod.Mod(MMath.ToRad( 82.74 +   40.76 * t), MMath.Pi2);
			double b = MMod.Mod(MMath.ToRad( 29.86 + 1181.36 * t), MMath.Pi2);
			double c = MMod.Mod(MMath.ToRad( 14.13 +  590.68 * t), MMath.Pi2);
			double d = MMod.Mod(MMath.ToRad(220.02 + 1262.87 * t), MMath.Pi2);
			double h;

			// Korrektur berechnen und anwenden
			h  = MMath.Polynome(t, -68.8840,  0.0009,  0.00023);
			h += MMath.Polynome(t,   5.5452, -0.0279, -0.00020) * MMath.Sin(      m);
			h += MMath.Polynome(t,   3.0727, -0.0430,  0.00007) * MMath.Cos(      m);
			h += MMath.Polynome(t,   0.1101, -0.0006, -0.00001) * MMath.Sin(2.0 * m);
			h += MMath.Polynome(t,   0.1654, -0.0043,  0.00001) * MMath.Cos(2.0 * m);
			h += MMath.Polynome(t,   0.0010,  0.0001          ) * MMath.Sin(3.0 * m);
			h += MMath.Polynome(t,   0.0095, -0.0003          ) * MMath.Cos(3.0 * m);
			h += MMath.Polynome(t,   0.0000, -0.0337,  0.00018) * MMath.Sin(      a);
			h += MMath.Polynome(t,  -0.8510,  0.0044,  0.00068) * MMath.Cos(      a);
			h += MMath.Polynome(t,   0.0000, -0.0064,  0.00004) * MMath.Sin(      b);
			h += MMath.Polynome(t,   0.2397, -0.0012, -0.00008) * MMath.Cos(      b);
			h += MMath.Polynome(t,   0.0000, -0.0010          ) * MMath.Sin(      c);
			h += MMath.Polynome(t,   0.1245,  0.0006          ) * MMath.Cos(      c);
			h += MMath.Polynome(t,   0.0000,  0.0024, -0.00003) * MMath.Sin(      d);
			h += MMath.Polynome(t,   0.0477, -0.0005, -0.00006) * MMath.Cos(      d);
			j += h;
		}
		return j;
	}

	// MSaturn.SemimajorAxis()
	/// <summary>
	/// Liefert die große Halbachse der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Große Halbachse der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	public static double SemimajorAxis(){ return MSaturn.SemimajorAxis(DateTime.Now.ToJdn()); }

	// MSaturn.SemimajorAxis(double)
	/// <summary>
	/// Liefert die große Halbachse der mittleren Planetenbahn zur julianischen Tageszahl.
	/// </summary>
	/// <returns>Große Halbachse der mittleren Planetenbahn zur julianischen Tageszahl.</returns>
	public static double SemimajorAxis(double jd)
	{
		// Lokale Felder einrichten und große Halbachse berechnen
		double t = (jd - MCalendar.Jdn20000101) / 36525.0;
		return MMath.Polynome(t, 9.554909192, -0.0000021390, 0.000000004);
	}

	// MSaturn.SiderealPeriod()
	/// <summary>
	/// Liefert die mittlere siderische Periode zur mittleren Planetenbahn.
	/// </summary>
	/// <returns>Mittlere siderische Periode zur mittleren Planetenbahn.</returns>
	public static double SiderealPeriod(){ return 10746.940443; }

	// MSaturn.SynodicPeriod()
	/// <summary>
	/// Liefert die mittlere synodische Periode zur mittleren Planetenbahn.
	/// </summary>
	/// <returns>Mittlere synodische Periode zur mittleren Planetenbahn.</returns>
	public static double SynodicPeriod(){ return 378.091904; }
}
