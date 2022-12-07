using Acamat.LCore;
using Acamat.LMath;
using Acamat.LMath.Geometry;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt Berechnungen zum Neptun.
/// </summary>
public partial class MNeptune
{
	// ------------------- //
	// Felder und Methoden //
	// ------------------- //
	// MNeptune.Aphelion()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch das Aphel nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch das Aphel nach der aktuellen Systemzeit.</returns>
	public static double Aphelion(){ return MNeptune.Aphelion(DateTime.Now.ToJdn()); }

	// MNeptune.Aphelion(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch das Aphel nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch das Aphel nach der julianischen Tageszahl.</returns>
	public static double Aphelion(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor(0.00607 * (y - 2047.50)) - 0.5;
		double j = 0.0;

		// Berechnungsschleife
		while(j <= jd)
		{
			// Tageszahl berechnen
			k += 1.0;
			j  = MMath.Polynome(k, 2468895.100, 60190.33, 0.03429);
		}
		return j;
	}

	// MNeptune.Conjunction()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die Konjunktion mit der Sonne nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die Konjunktion mit der Sonne nach der aktuellen Systemzeit.</returns>
	public static double Conjunction(){ return MNeptune.Conjunction(DateTime.Now.ToJdn()); }

	// MNeptune.Conjuction(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die Konjunktion mit der Sonne nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die Konjunktion mit der Sonne nach der julianischen Tageszahl.</returns>
	public static double Conjunction(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor((365.2425 * y + 1721060.0 - 2451569.379) / 367.486703) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while (j <= jd)
		{
			// Näherung berechnen
			k += 1.0;
			j  = 2451569.379 + k * 367.486703;

			// Hilfsfelder berechnen
			double m = MMod.Mod(MMath.ToRad(21.5569 + k * 2.194998), MMath.Pi2);
			double t = (j - MCalendar.Jdn20000101) / 36525.0;
			double a = MMod.Mod(MMath.ToRad(207.83 +   8.51 * t), MMath.Pi2);
			double b = MMod.Mod(MMath.ToRad(276.74 + 209.98 * t), MMath.Pi2);
			double h;

			// Korrektur berechnen und anwenden
			h  =                    0.0168                                         ;
			h += MMath.Polynome(t, -2.5606,  0.0088,  0.00002) * MMath.Sin(      m);
			h += MMath.Polynome(t, -0.8611, -0.0037,  0.00002) * MMath.Cos(      m);
			h += MMath.Polynome(t,  0.0118, -0.0004,  0.00001) * MMath.Sin(2.0 * m);
			h += MMath.Polynome(t,  0.0307, -0.0003          ) * MMath.Cos(2.0 * m);
			h +=                   -0.5964                     * MMath.Cos(      a);
			h +=                    0.0728                     * MMath.Cos(      b);
			j += h;
		}
		return j;
	}

	// MNeptune.Eccentricity()
	/// <summary>
	/// Liefert die Exzentrizität der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Exzentrizität der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	public static double Eccentricity(){ return MNeptune.Eccentricity(DateTime.Now.ToJdn()); }

	// MNeptune.Eccentricity(double)
	/// <summary>
	/// Liefert die Exzentrizität der mittleren Planetenbahn zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Exzentrizität der mittleren Planetenbahn zur julianischen Tageszahl.</returns>
	public static double Eccentricity(double jd)
	{
		// Lokale Felder einrichten und Exzentrizität berechnen
		double t = (jd - MCalendar.Jdn20000101) / 36525.0;
		return MMath.Polynome(t, 0.00945575, 0.000006033, 0.0, -0.00000000005);
	}

	// MNeptune.Inclination()
	/// <summary>
	/// Liefert die Neigung der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Neigung der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double Inclination(){ return MNeptune.Inclination(DateTime.Now.ToJdn()); }

	// MNeptune.Inclination(double)
	/// <summary>
	/// Liefert die Neigung der mittleren Planetenbahn zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Neigung der mittleren Planetenbahn zur julianischen Tageszahl.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double Inclination(double jd)
	{
		// Bahnneigung berechnen
		double t = (jd - MCalendar.Jdn20000101) / 36525.0;
		return MMath.Polynome(t, 1.769953, -0.0093082, -0.00000708, 0.000000027);
	}

	// MNeptune.Latitude(EPrecision)          » MNeptune.Latitude.cs
	// MNeptune.Latitude(EPrecision, double)  » MNeptune.Latitude.cs
	// MNeptune.Longitude(EPrecision)         » MNeptune.Longitude.cs
	// MNeptune.Longitude(EPrecision, double) » MNeptune.Longitude.cs

	// MNeptune.LongitudeOfAscendingNode()
	/// <summary>
	/// Liefert die Länge des aufsteigenden Knotens der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Länge des aufsteigenden Knotens der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double LongitudeOfAscendingNode(){ return MNeptune.LongitudeOfAscendingNode(DateTime.Now.ToJdn()); }

	// MNeptune.LongitudeOfAscendingNode(double)
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
		return MMath.Polynome(t, 131.784057, 1.1022039, 0.00025952, -0.000000637);
	}

	// MNeptune.LongitudeOfPerihelion()
	/// <summary>
	/// Liefert die Länge des Perihels der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Länge des Perihels der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double LongitudeOfPerihelion(){ return MNeptune.LongitudeOfPerihelion(DateTime.Now.ToJdn()); }

	// MNeptune.LongitudeOfPerihelion(double)
	/// <summary>
	/// Liefert die Länge des Perihels der mittleren Planetenbahn zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Länge des Perihels der mittleren Planetenbahn zur julianischen Tageszahl.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double LongitudeOfPerihelion(double jd)
	{
		// Länge berechnen
		double t = (jd - MCalendar.Jdn20000101) / 36525.0;
		return MMath.Polynome(t, 48.120276, 1.4262957, 0.00038434, 0.000000020);
	}

	// MNeptune.MeanAnomaly()
	/// <summary>
	/// Liefert die mittlere Anomalie der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Mittlere Anomalie der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double MeanAnomaly(){ return MNeptune.MeanAnomaly(DateTime.Now.ToJdn()); }

	// MNeptune.MeanAnomaly(double)
	/// <summary>
	/// Liefert die mittlere Anomalie der mittleren Planetenbahn zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Mittlere Anomalie der mittleren Planetenbahn zur julianischen Tageszahl.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double MeanAnomaly(double jd){ return MMod.Mod(MNeptune.MeanLongitude(jd) + MNeptune.LongitudeOfPerihelion(jd), 360.0); }

	// MNeptune.MeanLongitude()
	/// <summary>
	/// Liefert die mittlere Länge der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Mittlere Länge der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double MeanLongitude(){ return MNeptune.MeanLongitude(DateTime.Now.ToJdn()); }

	// MNeptune.MeanLongitude(double)
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
		return MMod.Mod(MMath.Polynome(t, 304.348665, 219.8833092, 0.00030882, 0.000000018), 360.0);
	}

	// MNeptune.Opposition()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die Opposition mit der Sonne nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die Opposition mit der Sonne nach der aktuellen Systemzeit.</returns>
	public static double Opposition(){ return MNeptune.Opposition(DateTime.Now.ToJdn()); }

	// MNeptune.Opposition(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die Opposition mit der Sonne nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die Opposition mit der Sonne nach der aktuellen Systemzeit.</returns>
	public static double Opposition(double jd)
	{
		// Deklaration der lokalen Felder
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor((365.2425 * y + 1721060.0 - 2451753.122) / 367.486703) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while (j <= jd)
		{
			// Näherung berechnen
			k += 1.0;
			j  = 2451753.122 + k * 367.486703;

			// Hilfsfelder berechnen
			double m = MMod.Mod(MMath.ToRad(202.6544 + k * 2.194998), MMath.Pi2);
			double t = (j - MCalendar.Jdn20000101) / 36525.0;
			double a = MMod.Mod(MMath.ToRad(207.83 +   8.51 * t), MMath.Pi2);
			double b = MMod.Mod(MMath.ToRad(276.74 + 209.98 * t), MMath.Pi2);
			double h;

			// Korrektur berechnen und anwenden
			h  = MMath.Polynome(t, -0.0140,  0.0000,  0.00001);
			h += MMath.Polynome(t, -1.3486,  0.0010,  0.00001) * MMath.Sin(      m);
			h += MMath.Polynome(t,  0.8597,  0.0037          ) * MMath.Cos(      m);
			h += MMath.Polynome(t, -0.0082, -0.0002,  0.00001) * MMath.Sin(2.0 * m);
			h += MMath.Polynome(t,  0.0037, -0.0003          ) * MMath.Cos(2.0 * m);
			h +=                   -0.5964                     * MMath.Cos(      a);
			h +=                    0.0728                     * MMath.Cos(      b);
			j += h;
		}
		return j;
	}

	// MNeptune.Perihelion()
	/// <summary>
	/// Liefert die julianische Tageszahl der nächsten Durchgangs durch das Perihel nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl der nächsten Durchgangs durch das Perihel nach der aktuellen Systemzeit.</returns>
	public static double Perihelion(){ return MNeptune.Perihelion(DateTime.Now.ToJdn()); }

	// MNeptune.Perihelion(double)
	/// <summary>
	/// Liefert die julianische Tageszahl der nächsten Durchgangs durch das Perihel nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl der nächsten Durchgangs durch das Perihel nach der julianischen Tageszahl.</returns>
	public static double Perihelion(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor(0.00607 * (y - 2047.50)) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while(j <= jd)
		{
			// Tageszahl berechnen
			k += 1.0;
			j  = MMath.Polynome(k, 2468895.100, 60190.33, 0.03429);
		}
		return j;
	}

	// MNeptune.PositionEcliptical(EPrecision)
	/// <summary>
	/// Liefert die heliozentrisch-ekliptikale Position zur aktuellen Systemzeit.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <returns>Heliozentrisch-ekliptikale Position zur aktuellen Systemzeit.</returns>
	public static CPolar PositionEcliptical(EPrecision value){ return MNeptune.PositionEcliptical(value, DateTime.Now.ToJdn()); }

	// MNeptune.PositionEcliptical(EPrecision, double)
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
		rtn.Latitude  = MNeptune.Latitude (value, jd);
		rtn.Longitude = MNeptune.Longitude(value, jd);
		rtn.Radius    = MNeptune.Radius   (value, jd);
		return rtn;
	}

	// MNeptune.PositionEquatorial()
	/// <summary>
	/// Liefert die (scheinbare) geozentrisch-äquatoriale Position zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Geozentrisch-äquatoriale Position zur aktuellen Systemzeit.</returns>
	public static CPolar PositionEquatorial(){ return MNeptune.PositionEquatorial(DateTime.Now.ToJdn()); }

	// MNeptune.PositionEquatorial(double)
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
			bH = MNeptune.Latitude (EPrecision.High, jdn);
			lH = MNeptune.Longitude(EPrecision.High, jdn);
			rH = MNeptune.Radius   (EPrecision.High, jdn);

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

	// MNeptune.Radius(EPrecision)         » MNeptune.Radius.cs
	// MNeptune.Radius(EPrecision, double) » MNeptune.Radius.cs

	// MNeptune.Rise(CPolar)
	/// <summary>
	/// Liefert die Ereigniskennung, die julianische Tageszahl des Aufgangs und die Morgenweite am geographischen Ort und zum aktuellen Systemdatum.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <returns>Ereigniskennung, julianische Tageszahl des Aufgangs und die Morgenweite am geographischen Ort und zum aktuellen Systemdatum.</returns>
	public static (EEventType type, double? jd, double? azimuth) Rise(CPolar position){ return MNeptune.Rise(position.Longitude, position.Latitude, DateTime.Now.ToJdn()); }

	// MNeptune.Rise(CPolar, double)
	/// <summary>
	/// Liefert die Ereigniskennung, die julianische Tageszahl des Aufgangs und die Morgenweite am geographischen Ort und zum julianischen Tagesdatum.
	/// </summary>
	/// <param name="pos">Geographische Position.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Ereigniskennung, die julianische Tageszahl des Aufgangs und die Morgenweite am geographischen Ort und zum julianischen Tagesdatum.</returns>
	public static (EEventType type, double? jd, double? azimuth) Rise(CPolar position, double jd){ return MNeptune.Rise(position.Longitude, position.Latitude, jd); }

	// MNeptune.Rise(double, double)
	/// <summary>
	/// Liefert die Ereigniskennung, die julianische Tageszahl des Aufgangs und die Morgenweite am geographischen Ort und zum aktuellen Systemdatum.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <returns>Ereigniskennung, die julianische Tageszahl des Aufgangs und die Morgenweite am geographischen Ort und zum aktuellen Systemdatum.</returns>
	public static (EEventType type, double? jd, double? azimuth) Rise(double lambda, double phi){ return MNeptune.Rise(lambda, phi, DateTime.Now.ToJdn()); }

	// MNeptune.Rise(double, double, double)
	/// <summary>
	/// Liefert die Ereigniskennung, die julianische Tageszahl des Aufgangs und die Morgenweite am geographischen Ort und zum julianischen Tagesdatum.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Ereigniskennung, die julianische Tageszahl des Aufgangs und die Morgenweite am geographischen Ort und zum julianischen Tagesdatum.</returns>
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
		l = MNeptune.Longitude(EPrecision.Low, jdn + 1.0);
		b = MNeptune.Latitude (EPrecision.Low, jdn + 1.0);
		double aP = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jdn + 1.0);
		double dP = MEphemerides.ToDelta(l, b, EObliquity.Mean, jdn + 1.0);

		// Position für gegebenen Tag berechnen
		l = MNeptune.Longitude(EPrecision.Low, jdn);
		b = MNeptune.Latitude (EPrecision.Low, jdn);
		double a0 = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jdn);
		if(MMath.Abs(aP - a0) > 1.0)
			a0 += MMath.Sgn(aP - a0) * MMath.Pi2;
		double d0 = MEphemerides.ToDelta(l, b, EObliquity.Mean, jdn);

		// Position für vorhergehenden Tag berechnen
		l = MNeptune.Longitude(EPrecision.Low, jdn - 1.0);
		b = MNeptune.Latitude (EPrecision.Low, jdn - 1.0);
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

	// MNeptune.SemimajorAxis()
	/// <summary>
	/// Liefert die große Halbachse der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Große Halbachse der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	public static double SemimajorAxis(){ return MNeptune.SemimajorAxis(DateTime.Now.ToJdn()); }

	// MNeptune.SemimajorAxis(double)
	/// <summary>
	/// Liefert die große Halbachse der mittleren Planetenbahn zur julianischen Tageszahl.
	/// </summary>
	/// <returns>Große Halbachse der mittleren Planetenbahn zur julianischen Tageszahl.</returns>
	public static double SemimajorAxis(double jd)
	{
		// Lokale Felder einrichten und Halbachse berechnen
		double t = (jd - MCalendar.Jdn20000101) / 36525.0;
		return MMath.Polynome(t, 30.110386869, -0.0000001663, 0.00000000069);
	}

	// MNeptune.Set(CPolar)
	/// <summary>
	/// Liefert die Ereigniskennung, die julianische Tageszahl des Untergangs und die Abendweite am geographischen Ort und zum aktuellen Systemdatum.
	/// </summary>
	/// <param name="position">Geographisches Position.</param>
	/// <returns>Ereigniskennung, die julianische Tageszahl des Untergangs und die Abendweite am geographischen Ort und zum aktuellen Systemdatum.</returns>
	public static (EEventType type, double? jd, double? azimuth) Set(CPolar position){ return MNeptune.Set(position.Longitude, position.Latitude, DateTime.Now.ToJdn()); }

	// MNeptune.Set(CPolar, double)
	/// <summary>
	/// Liefert die Ereigniskennung, die julianische Tageszahl des Untergangs und die Abendweite am geographischen Ort und zum julianischen Tagesdatum.
	/// </summary>
	/// <param name="position">Geographisches Position.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Ereigniskennung, die julianische Tageszahl des Untergangs und die Abendweite am geographischen Ort und zum julianischen Tagesdatum.</returns>
	public static (EEventType type, double? jd, double? azimuth) Set(CPolar position, double jd){ return MNeptune.Set(position.Longitude, position.Latitude, jd); }

	// MNeptune.Set(double, double)
	/// <summary>
	/// Liefert die Ereigniskennung, die julianische Tageszahl des Untergangs und die Abendweite am geographischen Ort und zum aktuellen Systemdatum.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <returns>Ereigniskennung, die julianische Tageszahl des Untergangs und die Abendweite am geographischen Ort und zum aktuellen Systemdatum.</returns>
	public static (EEventType type, double? jd, double? azimuth) Set(double lambda, double phi){ return MNeptune.Set(lambda, phi, DateTime.Now.ToJdn()); }

	// MNeptune.Set(double, double, double)
	/// <summary>
	/// Liefert die Ereigniskennung, die julianische Tageszahl des Untergangs und die Abendweite am geographischen Ort und zum julianischen Tagesdatum.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Ereigniskennung, die julianische Tageszahl des Untergangs und die Abendweite am geographischen Ort und zum julianischen Tagesdatum.</returns>
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
		l = MNeptune.Longitude(EPrecision.Low, jdn + 1.0);
		b = MNeptune.Latitude (EPrecision.Low, jdn + 1.0);
		double aP = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jd + 1.0);
		double dP = MEphemerides.ToDelta(l, b, EObliquity.Mean, jd + 1.0);

		// Position für gegebenen Tag berechnen
		l = MNeptune.Longitude(EPrecision.Low, jdn);
		b = MNeptune.Latitude (EPrecision.Low, jdn);
		double a0 = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jdn);
		if(MMath.Abs(aP - a0) > 1.0)
			a0 += MMath.Sgn(aP - a0) * MMath.Pi2;
		double d0 = MEphemerides.ToDelta(l, b, EObliquity.Mean, jdn);

		// Position für vorhergehenden Tag berechnen
		l = MNeptune.Longitude(EPrecision.Low, jdn - 1.0);
		b = MNeptune.Latitude (EPrecision.Low, jdn - 1.0);
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

	// MNeptune.SynodicPeriod()
	/// <summary>
	/// Liefert die mittlere synodische Periode zur mittleren Planetenbahn.
	/// </summary>
	/// <returns>Mittlere synodische Periode zur mittleren Planetenbahn.</returns>
	public static double SynodicPeriod(){ return 367.504368; }

	// MNeptune.Transit(CPolar)
	/// <summary>
	/// Liefert die Ereigniskennung, die julianische Tageszahl des Meridiandurchgangs und die horizontale Höhe am geographischen Ort und zum aktuellen Systemdatum.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <returns>Ereigniskennung, die julianische Tageszahl des Meridiandurchgangs und die horizontale Höhe am geographischen Ort und zum aktuellen Systemdatum.</returns>
	public static (EEventType type, double? jd, double? height) Transit(CPolar position){ return MNeptune.Transit(position.Longitude, position.Latitude, DateTime.Now.ToJdn()); }

	// MNeptune.Transit(CPolar, double)
	/// <summary>
	/// Liefert die Ereigniskennung, die julianische Tageszahl des Meridiandurchgangs und die horizontale Höhe am geographischen Ort und zum julianischen Tagesdatum.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Ereigniskennung, die julianische Tageszahl des Meridiandurchgangs und die horizontale Höhe am geographischen Ort und zum julianischen Tagesdatum.</returns>
	public static (EEventType type, double? jd, double? height) Transit(CPolar position, double jd){ return MNeptune.Transit(position.Longitude, position.Latitude, jd); }

	// MNeptune.Transit(double, double)
	/// <summary>
	/// Liefert die Ereigniskennung, die julianische Tageszahl des Meridiandurchgangs und die horizontale Höhe am geographischen Ort und zum aktuellen Systemdatum.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <returns>Ereigniskennung, die julianische Tageszahl des Meridiandurchgangs und die horizontale Höhe am geographischen Ort und zum aktuellen Systemdatum.</returns>
	public static (EEventType type, double? jd, double? height) Transit(double lambda, double phi){ return MNeptune.Transit(lambda, phi, DateTime.Now.ToJdn()); }

	// MNeptune.Transit(double, double, double)
	/// <summary>
	/// Liefert die Ereigniskennung, die juliansiche Tageszahl der Meridiandurchgangs und die horizontale Höhe am geographischen Ort und zum julianischen Tagesdatum.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Ereigniskennung, die juliansiche Tageszahl der Meridiandurchgangs und die horizontale Höhe am geographischen Ort und zum julianischen Tagesdatum.</returns>
	public static (EEventType type, double? jd, double? height) Transit(double lambda, double phi, double jd)
	{
		// Lokale Felder einrichten
		double jdn = MMath.Floor(jd - 0.5) + 0.5; // Tageszahl um Mitternacht
		double l   = 0.0;                         // Geozentrische Länge
		double b   = 0.0;                         // Geozentrische Breite
		double a   = 0.0;                         // Rektaszension
		double dm  = 1.0;                         // Korrekturglied

		// Position für nachfolgenden Tag berechnen
		l = MNeptune.Longitude(EPrecision.Low, jdn + 1.0);
		b = MNeptune.Latitude (EPrecision.Low, jdn + 1.0);
		double aP = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jdn + 1.0);
		double dP = MEphemerides.ToDelta(l, b, EObliquity.Mean, jdn + 1.0);

		// Position für gegebenen Tag berechnen
		l = MNeptune.Longitude(EPrecision.Low, jdn);
		b = MNeptune.Latitude (EPrecision.Low, jdn);
		double a0 = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jdn);
		if(MMath.Abs(aP - a0) > 1.0)
			a0 += MMath.Sgn(aP - a0) * MMath.Pi2;
		double d0 = MEphemerides.ToDelta(l, b, EObliquity.Mean, jdn);

		// Position für vorhergehenden Tag berechnen
		l = MNeptune.Longitude(EPrecision.Low, jdn - 1.0);
		b = MNeptune.Latitude (EPrecision.Low, jdn - 1.0);
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

	// MNeptune.TropicalPeriod()
	/// <summary>
	/// Liefert die mittlere tropische Periode zur mittleren Planetenbahn.
	/// </summary>
	/// <returns>Mittlere tropische Periode zur mittleren Planetenbahn.</returns>
	public static double TropicalPeriod(){ return 59799.900444; }
}
