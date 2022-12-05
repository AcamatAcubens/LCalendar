using Acamat.LCore;
using Acamat.LMath;
using Acamat.LMath.Geometry;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt Berechnungen zur Sonne.
/// </summary>
public static partial class MSun
{
	// ------------------- //
	// Felder und Methoden //
	// ------------------- //
	// MSun.Dawn(CPolar, ref double)
	/// <summary>
	/// Setzt die julianische Tageszahl der bürgerlichen Morgendämmerung am geographischen Ort und zur aktuellen Systemzeit und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <param name="jdEvent">Julianische Tageszahl der Morgendämmerung.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Dawn(CPolar position, ref double jdEvent)
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double jd = DateTime.Now.ToJdn();
		return MSun.Dawn(position.Longitude, position.Latitude, ref jdEvent, jd, MEphemerides.GeocentricHeight_TwilightCivil);
	}

	// MSun.Dawn(CPolar, ref double, double)
	/// <summary>
	/// Setzt die julianische Tageszahl der bürgerlichen Morgendämmerung am geographischen Ort und zur julianischen Tageszahl und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <param name="jdEvent">Julianische Tageszahl der Morgendämmerung.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Dawn(CPolar position, ref double jdEvent, double jd){ return MSun.Dawn(position.Longitude, position.Latitude, ref jdEvent, jd, MEphemerides.GeocentricHeight_TwilightCivil); }

	// MSun.Dawn(CPolar, ref double, double)
	/// <summary>
	/// Setzt die julianische Tageszahl der bürgerlichen Morgendämmerung am geographischen Ort und zur julianischen Tageszahl und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <param name="jdEvent">Julianische Tageszahl der Morgendämmerung.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="height">Geozentrische Höhe.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Dawn(CPolar position, ref double jdEvent, double jd, double height){ return MSun.Dawn(position.Longitude, position.Latitude, ref jdEvent, jd, height); }

	// MSun.Dawn(double, double, ref double)
	/// <summary>
	/// Setzt die julianische Tageszahl der bürgerlichen Morgendämmerung am geographischen Ort und zur aktuellen Systemzeit und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite</param>
	/// <param name="jdEvent">Julianische Tageszahl der Morgendämmerung.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Dawn(double lambda, double phi, ref double jdEvent)
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double jd = DateTime.Now.ToJdn();
		return MSun.Dawn(lambda, phi, ref jdEvent, jd, MEphemerides.GeocentricHeight_TwilightCivil);
	}

	// MSun.Dawn(double, double, ref double, double)
	/// <summary>
	/// Setzt die julianische Tageszahl der bürgerlichen Morgendämmerung am geographischen Ort und zur julianischen Tageszahl und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite</param>
	/// <param name="jdEvent">Julianische Tageszahl der Morgendämmerung.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Dawn(double lambda, double phi, ref double jdEvent, double jd){ return MSun.Dawn(lambda, phi, ref jdEvent, jd, MEphemerides.GeocentricHeight_TwilightCivil); }

	// MSun.Dawn(double, double, ref double, double, double)
	/// <summary>
	/// Setzt die julianische Tageszahl der Morgendämmerung am geographischen Ort und zur julianischen Tageszahl und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite</param>
	/// <param name="jdEvent">Julianische Tageszahl der Morgendämmerung.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="height">Geozentrische Höhe.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Dawn(double lambda, double phi, ref double jdEvent, double jd, double height)
	{
		// Lokale Felder einrichten
		double jdn  = MMath.Floor(jd - 0.5) + 0.5; // Tageszahl um Mitternacht
		double l    = 0.0;                         // Geozentrische Länge
		double a    = 0.0;                         // Rektaszension
		double d    = 0.0;                         // Deklination
		double dm   = 1.0;                         // Korrekturglied
		double h    = 0.0;                         //
		double H    = 0.0;                         //
		double sinP = MMath.Sin(phi);              // Breitensinus
		double cosP = MMath.Cos(phi);              // Breitencosinus

		// Position für nachfolgenden Tag berechnen
		l = MSun.Longitude(EPrecision.Low, jdn + 1.0);
		double aP = MEphemerides.ToAlpha(l, 0.0, EObliquity.Mean, jdn + 1.0);
		double dP = MEphemerides.ToDelta(l, 0.0, EObliquity.Mean, jdn + 1.0);

		// Position für gegebenen Tag berechnen
		l = MSun.Longitude(EPrecision.Low, jdn);
		double a0 = MEphemerides.ToAlpha(l, 0.0, EObliquity.Mean, jdn);
		if(MMath.Abs(aP - a0) > 1.0) a0 += MMath.Sgn(aP - a0) * MMath.Pi2;
		double d0 = MEphemerides.ToDelta(l, 0.0, EObliquity.Mean, jd);

		// Position für vorhergehenden Tag berechnen
		l = MSun.Longitude(EPrecision.Low, jdn - 1.0);
		double aM = MEphemerides.ToAlpha(l, 0.0, EObliquity.Mean, jdn - 1.0);
		if(MMath.Abs(a0 - aM) > 1.0) aM += MMath.Sgn(a0 - aM) * MMath.Pi2;
		double dM = MEphemerides.ToDelta(l, 0.0, EObliquity.Mean, jdn - 1.0);

		// Stundenwinkel berechnen und prüfen
		double cosH = (MMath.Sin(height) - sinP * MMath.Sin(dP)) / (cosP * MMath.Cos(dP));
		if(MMath.Abs(cosH) > 1.0) return cosH < 1.0 ? EEventType.AlwaysAboveHorizon : EEventType.AlwaysBeneathHorizon;
		H = MMath.ArcCos(cosH);

		// -------------//
		// Ereigniszeit //
		// -------------//

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
			dm = (h - height) / (MMath.Pi2 * MMath.Cos(d) * cosP * MMath.Sin(H));
			m += dm;
		}

		// Ereigniszeit prüfen
		if(m < 0.0 || m >= 1.0) return EEventType.NoEvent;

		// Iteration anwenden und Rückgabewert setzen
		jdEvent = jd + m;
		return EEventType.Normal;
	}

	// MSun.Dusk(CPolar, ref double)
	/// <summary>
	/// Setzt die julianische Tageszahl der bürgerlichen Abenddämmerung am geographischen Ort und zur aktuellen Systemzeit und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <param name="jdEvent">Julianische Tageszahl der Abenddämmerung.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Dusk(CPolar position, ref double jdEvent)
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double jd = DateTime.Now.ToJdn();
		return MSun.Dusk(position.Longitude, position.Latitude, ref jdEvent, jd, MEphemerides.GeocentricHeight_TwilightCivil);
	}

	// MSun.Dusk(CPolar, ref double, double)
	/// <summary>
	/// Setzt die julianische Tageszahl der bürgerlichen Abenddämmerung am geographischen Ort und zur julianischen Tageszahl und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <param name="jdEvent">Julianische Tageszahl der Abenddämmerung.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Dusk(CPolar position, ref double jdEvent, double jd){ return MSun.Dusk(position.Longitude, position.Latitude, ref jdEvent, jd, MEphemerides.GeocentricHeight_TwilightCivil); }

	// MSun.Dusk(CPolar, ref double, double)
	/// <summary>
	/// Setzt die julianische Tageszahl der bürgerlichen Abenddämmerung am geographischen Ort und zur julianischen Tageszahl und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <param name="jdEvent">Julianische Tageszahl der Abenddämmerung.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="height">Geozentrische Höhe.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Dusk(CPolar position, ref double jdEvent, double jd, double height){ return MSun.Dusk(position.Longitude, position.Latitude, ref jdEvent, jd, height); }

	// MSun.Dusk(double, double, ref double)
	/// <summary>
	/// Setzt die julianische Tageszahl der bürgerlichen Abenddämmerung am geographischen Ort und zur aktuellen Systemzeit und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite</param>
	/// <param name="jdEvent">Julianische Tageszahl der Abenddämmerung.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Dusk(double lambda, double phi, ref double jdEvent)
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double jd = DateTime.Now.ToJdn();
		return MSun.Dusk(lambda, phi, ref jdEvent, jd, MEphemerides.GeocentricHeight_TwilightCivil);
	}

	// MSun.Dusk(double, double, ref double, double)
	/// <summary>
	/// Setzt die julianische Tageszahl der bürgerlichen Abenddämmerung am geographischen Ort und zur julianischen Tageszahl und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Georgraphische Breite.</param>
	/// <param name="jdEvent">Julianische Tageszahl der Abenddämmerung.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Dusk(double lambda, double phi, ref double jdEvent, double jd){ return MSun.Dusk(lambda, phi, ref jdEvent, jd, MEphemerides.GeocentricHeight_TwilightCivil); }

	// MSun.Dusk(double, double, ref double, double, double)
	/// <summary>
	/// Setzt die julianische Tageszahl der Abenddämmerung am geographischen Ort und zur julianischen Tageszahl und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Georgraphische Breite.</param>
	/// <param name="jdEvent">Julianische Tageszahl der Abenddämmerung.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="height">Geozentrische Höhe.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Dusk(double lambda, double phi, ref double jdEvent, double jd, double height)
	{
		// Lokale Felder einrichten
		double jdn  = MMath.Floor(jd - 0.5) + 0.5; // Tageszahl um Mitternacht
		double l    = 0.0;                         // Geozentrische Länge
		double a    = 0.0;                         // Rektaszension
		double d    = 0.0;                         // Deklination
		double dm   = 1.0;                         // Korrekturglied
		double h    = 0.0;                         //
		double H    = 0.0;                         //
		double sinP = MMath.Sin(phi);              // Breitensinus
		double cosP = MMath.Cos(phi);              // Breitencosinus

		// Position für nachfolgenden Tag berechnen
		l = MSun.Longitude(EPrecision.Low, jdn + 1.0);
		double aP = MEphemerides.ToAlpha(l, 0.0, EObliquity.Mean, jdn + 1.0);
		double dP = MEphemerides.ToDelta(l, 0.0, EObliquity.Mean, jdn + 1.0);

		// Position für gegebenen Tag berechnen
		l = MSun.Longitude(EPrecision.Low, jdn);
		double a0 = MEphemerides.ToAlpha(l, 0.0, EObliquity.Mean, jdn);
		if(MMath.Abs(aP - a0) > 1.0) a0 += MMath.Sgn(aP - a0) * MMath.Pi2;
		double d0 = MEphemerides.ToDelta(l, 0.0, EObliquity.Mean, jdn);

		// Position für vorhergehenden Tag berechnen
		l = MSun.Longitude(EPrecision.Low, jdn + 1.0);
		double aM = MEphemerides.ToAlpha(l, 0.0, EObliquity.Mean, jdn - 1.0);
		if(MMath.Abs(a0 - aM) > 1.0) aM += MMath.Sgn(a0 - aM) * MMath.Pi2;
		double dM = MEphemerides.ToDelta(l, 0.0, EObliquity.Mean, jdn - 1.0);

		// Stundenwinkel berechnen und prüfen
		double cosH = (MMath.Sin(height) - sinP * MMath.Sin(dP)) / (cosP * MMath.Cos(dP));
		if(MMath.Abs(cosH) > 1.0) return cosH < 1.0 ? EEventType.AlwaysAboveHorizon : EEventType.AlwaysBeneathHorizon;
		H = MMath.ArcCos(cosH);

		// ------------------- //
		// Ereigniszeit nähern //
		// ------------------- //

		// Sternzeit und Stundenwinkel zum gegebenen Zeitpunkt bestimmen
		double t0 = MEphemerides.Gmst(jdn);
		double m  = MMath.Div((a0 + lambda - t0 + H) / MMath.Pi2);
		if(m < 0.0) m += 1.0;

		// Ereigniszeit iterieren
		while(MMath.Abs(dm) >= 0.0001)
		{
			// Iteration durchführen und nächsten Iterationsschritt vorbereiten
			a  = MMath.Bessel(m, aM, a0, aP);
			d  = MMath.Bessel(m, dM, d0, dP);
			H  = t0 + 6.300388093 * m - lambda - a;
			h  = MMath.ArcSin(sinP * MMath.Sin(d) + cosP * MMath.Cos(d) * MMath.Cos(H));
			dm = (h - height) / (MMath.Pi2 * MMath.Cos(d) * cosP * MMath.Sin(H));
			m  += dm;
		}

		// Ereigniszeit prüfen
		if(m < 0.0 || m >= 1.0) return EEventType.NoEvent;

		// Iteration anwenden und Rückgabewert setzen
		jdEvent = jd + m;
		return EEventType.Normal;
	}

	// MSun.EquinoxOfAutumn()
	/// <summary>
	/// Liefert die julianische Tageszahl des Herbstäquinoktiums zum aktuellen Jahr.
	/// </summary>
	/// <returns>Julianische Tageszahl des Herbstäquinoktiums zum aktuellen Jahr.</returns>
	public static double EquinoxOfAutumn()
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		int y = MCalendar.GregorianYear(DateTime.Now.ToJdn());
		return MSun.EquinoxOfAutumn(y);
	}

	// MSun.EquinoxOfAutumn(int)
	/// <summary>
	/// Liefert die julianische Tageszahl des Herbstäquinoktiums zur Jahreszahl.
	/// </summary>
	/// <param name="year">Jahreszahl.</param>
	/// <returns>Julianische Tageszahl des Herbstäquinoktiums zur Jahreszahl.</returns>
	public static double EquinoxOfAutumn(int year)
	{
		// Lokale Felder einrichten
		double jd = 0.0; // Tageszahl der Näherung

		// Nach Jahreszahl unterscheiden
		if(year < 1000)
		{
			// Näherung berechnen
			double y = (double)year / 1000.0;
			jd = MMath.Polynome(y, 1721325.70455, 365242.49558, -0.11677, -0.00297,  0.00074);
		}
		else
		{
			// Näherung berechnen
			double y = ((double)year - 2000.0) / 1000.0;
			jd = MMath.Polynome(y, 2451810.21715, 365242.01767, -0.11575,  0.00337,  0.00078);
		}

		// Näherung korregieren
		return jd + MSun.T27C(jd);
	}

	// MSun.EquinoxOfSpring()
	/// <summary>
	/// Liefert die julianische Tageszahl des Frühlingsäquinoktiums zur aktuellen Jahreszahl.
	/// </summary>
	/// <returns>Julianische Tageszahl des Frühlingsäquinoktiums zur aktuellen Jahreszahl.</returns>
	public static double EquinoxOfSpring()
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		int y = MCalendar.GregorianYear(DateTime.Now.ToJdn());
		return MSun.EquinoxOfSpring(y);
	}

	// MSun.EquinoxOfSpring(int)
	/// <summary>
	/// Liefert die julianische Tageszahl des Frühlingsäquinoktiums zur Jahreszahl.
	/// </summary>
	/// <param name="year">Jahreszahl.</param>
	/// <returns>Julianische Tageszahl des Frühlingsäquinoktiums zur Jahreszahl.</returns>
	public static double EquinoxOfSpring(int year)
	{
		// Lokale Felder einrichten
		double jd = 0.0; // Tageszahl der Näherung

		// Nach Jahreszahl unterscheiden
		if(year < 1000)
		{
			// Näherung berechnen
			double y = (double)year / 1000.0;
			jd = MMath.Polynome(y, 1721139.29189, 365242.13740,  0.06134,  0.00111, -0.00071);
		}
		else
		{
			// Näherung berechnen
			double y = ((double)year - 2000.0) / 1000.0;
			jd = MMath.Polynome(y, 2451623.80984, 365242.37404,  0.05169, -0.00411, -0.00057);
		}

		// Näherung korregieren
		return jd + MSun.T27C(jd);
	}

	// MSun.Latitude(EPrecision)
	/// <summary>
	/// Liefert die heliozentrisch-ekliptikale Breite zur aktuellen Systemzeit.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <returns>Heliozentrisch-ekliptikale Breite zur aktuellen Systemzeit.</returns>
	public static double Latitude(EPrecision value)
	{
		// Lokale Felder einrichten
		double jd = DateTime.Now.ToJdn();
		return MSun.Latitude(value, jd);
	}

	// MSun.Latitude(EPrecision, double)
	/// <summary>
	/// Liefert die heliozentrisch-ekliptikale Breite zur julianischen Tageszahl.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Heliozentrisch-ekliptikale Breite zur julianischen Tageszahl.</returns>
	public static double Latitude(EPrecision value, double jd) { return -MEarth.Latitude(value, jd); }

	// MSun.Longitude(EPrecision)
	/// <summary>
	/// Liefert die geozentrisch-ekliptikale Länge zur aktuellen Systemzeit.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <returns>Geozentrisch-ekliptikale Länge zur aktuellen Systemzeit.</returns>
	public static double Longitude(EPrecision value)
	{
		// Lokale Felder einrichten
		double jd = DateTime.Now.ToJdn();
		return MSun.Longitude(value, jd);
	}

	// MSun.Longitude(EPrecision, double)
	/// <summary>
	/// Liefert die geozentrisch-ekliptikale Länge zur julianischen Tageszahl.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Geozentrisch-ekliptikale Länge zur julianischen Tageszahl.</returns>
	public static double Longitude(EPrecision value, double jd){ return MMod.Mod(MMath.Pi + MEarth.Longitude(value, jd), MMath.Pi2); }

	// MSun.T27C(double)
	/// <summary>
	/// Liefert die Korrektur für Jahreszeitenberechnungen zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Korrektur für Jahreszeitenberechnungen zur julianischen Tageszahl.</returns>
	private static double T27C(double jd)
	{
		// Lokale Felder einrichten
		double t  = (jd - MCalendar.Jdn20000101) / 36525.0;
		double w  = MMath.ToRad(35999.373 * t - 2.47);
		double dL = 1.0 + 0.0334 * MMath.Cos(w) + 0.0007 * MMath.Cos(2.0 * w);
		double s  = 0.0;

		// Korrektur berechnen und anwenden
		s += 485.0 * MMath.Cos(MMath.ToRad(324.96 +   1934.136 * t));
		s += 203.0 * MMath.Cos(MMath.ToRad(337.23 +  32964.467 * t));
		s += 199.0 * MMath.Cos(MMath.ToRad(342.08 +     20.186 * t));
		s += 182.0 * MMath.Cos(MMath.ToRad( 27.85 + 445267.112 * t));
		s += 156.0 * MMath.Cos(MMath.ToRad( 73.14 +  45036.886 * t));
		s += 136.0 * MMath.Cos(MMath.ToRad(171.52 +  22518.443 * t));
		s +=  77.0 * MMath.Cos(MMath.ToRad(222.54 +  65928.934 * t));
		s +=  74.0 * MMath.Cos(MMath.ToRad(296.72 +   3034.906 * t));
		s +=  70.0 * MMath.Cos(MMath.ToRad(243.58 +   9037.513 * t));
		s +=  58.0 * MMath.Cos(MMath.ToRad(119.81 +  33718.147 * t));
		s +=  52.0 * MMath.Cos(MMath.ToRad(297.17 +    150.678 * t));
		s +=  50.0 * MMath.Cos(MMath.ToRad( 21.02 +   2281.226 * t));
		s +=  45.0 * MMath.Cos(MMath.ToRad(247.54 +  29929.562 * t));
		s +=  44.0 * MMath.Cos(MMath.ToRad(325.15 +  31555.956 * t));
		s +=  29.0 * MMath.Cos(MMath.ToRad( 60.93 +   4443.417 * t));
		s +=  18.0 * MMath.Cos(MMath.ToRad(155.12 +  67555.328 * t));
		s +=  17.0 * MMath.Cos(MMath.ToRad(288.79 +   4562.452 * t));
		s +=  16.0 * MMath.Cos(MMath.ToRad(198.04 +  62894.029 * t));
		s +=  14.0 * MMath.Cos(MMath.ToRad(199.76 +  31436.921 * t));
		s +=  12.0 * MMath.Cos(MMath.ToRad( 95.39 +  14577.848 * t));
		s +=  12.0 * MMath.Cos(MMath.ToRad(287.11 +  31931.756 * t));
		s +=  12.0 * MMath.Cos(MMath.ToRad(320.81 +  34777.259 * t));
		s +=   9.0 * MMath.Cos(MMath.ToRad(227.73 +   1222.114 * t));
		s +=   8.0 * MMath.Cos(MMath.ToRad( 15.45 +  16859.074 * t));
		return 0.00001 * s / dL;
	}

	// MSun.Position(EPrecision)
	/// <summary>
	/// Liefert die geozentrisch-ekliptikale Position zur aktuellen Systemzeit.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <returns>Heliozentrisch-ekliptikale Position zur aktuellen Systemzeit.</returns>
	public static CPolar Position(EPrecision value)
	{
		// Lokale Felder einrichten
		CPolar rtn = new CPolar();
		rtn.Latitude  = MSun.Latitude (value);
		rtn.Longitude = MSun.Longitude(value);
		rtn.Radius    = MSun.Radius   (value);
		return rtn;
	}

	// MSun.Position(EPrecision, double)
	/// <summary>
	/// Liefert die geozentrisch-ekliptikale Position zur julianischen Tageszahl.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Heliozentrisch-ekliptikale Position zur julianischen Tageszahl.</returns>
	public static CPolar Position(EPrecision value, double jd)
	{
		// Lokale Felder einrichten
		CPolar rtn = new CPolar();
		rtn.Latitude  = MSun.Latitude (value, jd);
		rtn.Longitude = MSun.Longitude(value, jd);
		rtn.Radius    = MSun.Radius   (value, jd);
		return rtn;
	}

	// MSun.Radius(EPrecision)
	/// <summary>
	/// Liefert den geozentrisch-ekliptikalen Radius zur aktuellen Systemzeit.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <returns>Geozentrisch-ekliptikalen Radius zur aktuellen Systemzeit.</returns>
	public static double Radius(EPrecision value)
	{
		// Lokale Felder einrichten
		double jd = DateTime.Now.ToJdn();
		return MSun.Radius(value, jd);
	}

	// MSun.Radius(EPrecision, double)
	/// <summary>
	/// Liefert den geozentrisch-ekliptikalen Radius zur julianischen Tageszahl.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Geozentrisch-ekliptikalen Radius zur julianischen Tageszahl.</returns>
	public static double Radius(EPrecision value, double jd){ return MEarth.Radius(value, jd); }

	// MSun.Rise(CPolar, ref double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Aufgangs am geographischen Ort und zur aktuellen Systemdatum und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Aufgangs.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Rise(CPolar position, ref double jdEvent)
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double jd  = DateTime.Now.ToJdn();
		double azm = 0.0;
		return MSun.Rise(position.Longitude, position.Latitude, ref jdEvent, jd, ref azm);
	}

	// MSun.Rise(CPolar, ref double, double)
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
		return MSun.Rise(position.Longitude, position.Latitude, ref jdEvent, jd, ref azm);
	}

	// MSun.Rise(CPolar, ref double, double, ref double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Aufgangs und die Morgenweite am geographischen Ort und zur aktuekllen Systemzeit und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Aufgangs.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="azimuth">Morgenweite.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Rise(CPolar position, ref double jdEvent, double jd, ref double azimuth){ return MSun.Rise(position.Longitude, position.Latitude, ref jdEvent, jd, ref azimuth); }

	// MSun.Rise(double, double, ref double)
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
		return MSun.Rise(lambda, phi, ref jdEvent, jd, ref azm);
	}

	// MSun.Rise(double, double, ref double, double)
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
		return MSun.Rise(lambda, phi, ref jdEvent, jd, ref azm);
	}

	// MSun.Rise(double, double, ref double, double, ref double)
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
		double jdn  = MMath.Floor(jd - 0.5) + 0.5;       // Tageszahl um Mitternacht
		double l    =  0.0;                              // Geozentrische Länge
		double a    =  0.0;                              // Rektaszension
		double d    =  0.0;                              // Deklination
		double dm   =  1.0;                              // Korrekturglied
		double h    =  0.0;                              //
		double h0   = MEphemerides.GeocentricHeight_Sun; // Refraktionswinkel
		double H    =  0.0;                              //
		double sinP = MMath.Sin(phi);                    // Breitensinus
		double cosP = MMath.Cos(phi);                    // Breitencosinus

		// Position für nachfolgenden Tag berechnen
		l = MSun.Longitude(EPrecision.Low, jdn + 1.0);
		double aP = MEphemerides.ToAlpha(l, 0.0, EObliquity.Mean, jdn + 1.0);
		double dP = MEphemerides.ToDelta(l, 0.0, EObliquity.Mean, jdn + 1.0);

		// Position für gegebenen Tag berechnen
		l = MSun.Longitude(EPrecision.Low, jdn);
		double a0 = MEphemerides.ToAlpha(l, 0.0, EObliquity.Mean, jdn);
		if(MMath.Abs(aP - a0) > 1.0) a0 += MMath.Sgn(aP - a0) * MMath.Pi2;
		double d0 = MEphemerides.ToDelta(l, 0.0, EObliquity.Mean, jdn);

		// Position für vorhergehenden Tag berechnen
		l = MSun.Longitude(EPrecision.Low, jdn + 1.0);
		double aM = MEphemerides.ToAlpha(l, 0.0, EObliquity.Mean, jdn - 1.0);
		if(MMath.Abs(a0 - aM) > 1.0) aM += MMath.Sgn(a0 - aM) * MMath.Pi2;
		double dM = MEphemerides.ToDelta(l, 0.0, EObliquity.Mean, jdn - 1.0);

		// Stundenwinkel berechnen und prüfen
		double cosH = (MMath.Sin(h0) - sinP * MMath.Sin(dP)) / (cosP * MMath.Cos(dP));
		if(MMath.Abs(cosH) > 1.0) return cosH < 1.0 ? EEventType.AlwaysAboveHorizon : EEventType.AlwaysBeneathHorizon;
		H = MMath.ArcCos(cosH);

		// ------------------- //
		// Ereigniszeit nähern //
		// ------------------- //

		// Sternzeit und Stundenwinkel zum gegebenen Zeitpunkt bestimmen
		double t0 = MEphemerides.Gmst(jdn);
		double m  = MMath.Div((a0 + lambda - t0 - H) / MMath.Pi2);
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

		// Ereigniszeit prüfen
		if(m < 0.0 || m >= 1.0) return EEventType.NoEvent;

		// Iteration anwenden, Azimut berechnen und Rückgabewert setzen
		jdEvent = jd + m;
		azimuth = MEphemerides.ToAzimuth(H, d, phi);
		return EEventType.Normal;
	}

	// MSun.Set(CPolar, ref double)
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
		return MSun.Set(position.Longitude, position.Latitude, ref jdEvent, jd, ref azm);
	}

	// MSun.Set(CPolar, ref double, double)
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
		return MSun.Set(position.Longitude, position.Latitude, ref jdEvent, jd, ref azm);
	}

	// MSun.Set(CPolar, ref double, double, ref double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Untergangs und die Abendweite am geographischen Ort und zur julianischen Tageszahl und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Untergangs.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="azimuth">Abendweite.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Set(CPolar position, ref double jdEvent, double jd, ref double azimuth){ return MSun.Set(position.Longitude, position.Latitude, ref jdEvent, jd, ref azimuth); }
	
	// MSun.Set(double, double, ref double)
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
		return MSun.Set(lambda, phi, ref jdEvent, jd, ref azm);
	}

	// MSun.Set(double, double, ref double, double)
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
		return MSun.Set(lambda, phi, ref jdEvent, jd, ref azm);
	}

	// MSun.Set(double, double, ref double, double, ref double)
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
		double jdn  = MMath.Floor(jd - 0.5) + 0.5;       // Tageszahl um Mitternacht
		double l    =  0.0;                              // Geozentrische Länge
		double a    =  0.0;                              // Rektaszension
		double d    =  0.0;                              // Deklination
		double dm   =  1.0;                              // Korrekturglied
		double h    =  0.0;                              //
		double h0   = MEphemerides.GeocentricHeight_Sun; // Refraktionswinkel
		double H    =  0.0;                              //
		double sinP = MMath.Sin(phi);                    // Breitensinus
		double cosP = MMath.Cos(phi);                    // Breitencosinus

		// Position für nachfolgenden Tag berechnen
		l = MSun.Longitude(EPrecision.Low, jdn + 1.0);
		double aP = MEphemerides.ToAlpha(l, 0.0, EObliquity.Mean, jd + 1.0);
		double dP = MEphemerides.ToDelta(l, 0.0, EObliquity.Mean, jd + 1.0);

		// Position für gegebenen Tag berechnen
		l = MSun.Longitude(EPrecision.Low, jdn);
		double a0 = MEphemerides.ToAlpha(l, 0.0, EObliquity.Mean, jdn);
		if(MMath.Abs(aP - a0) > 1.0) a0 += MMath.Sgn(aP - a0) * MMath.Pi2;
		double d0 = MEphemerides.ToDelta(l, 0.0, EObliquity.Mean, jdn);

		// Position für vorhergehenden Tag berechnen
		l = MSun.Longitude(EPrecision.Low, jdn - 1.0);
		double aM = MEphemerides.ToAlpha(l, 0.0, EObliquity.Mean, jd - 1.0);
		if(MMath.Abs(a0 - aM) > 1.0) aM += MMath.Sgn(a0 - aM) * MMath.Pi2;
		double dM = MEphemerides.ToDelta(l, 0.0, EObliquity.Mean, jd - 1.0);

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

		// Ereigniszeit prüfen
		if(m < 0.0 || m >= 1.0) return EEventType.NoEvent;

		// Iteration anwenden, Azimut berechnen und Rückgabewert setzen
		jdEvent = jd + m;
		azimuth = MEphemerides.ToAzimuth(H, d, phi);
		return EEventType.Normal;
	}

	// MSun.SolsticeOfSummer()
	/// <summary>
	/// Liefert die julianische Tageszahl der Sommersonnenwende zur aktuellen Jahreszahl.
	/// </summary>
	/// <returns>Julianische Tageszahl der Sommersonnenwende zur aktuellen Jahreszahl.</returns>
	public static double SolsticeOfSummer()
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		int y = MCalendar.GregorianYear(DateTime.Now.ToJdn());
		return MSun.SolsticeOfSummer(y);
	}

	// MSun.SolsticeOfSummer(int)
	/// <summary>
	/// Liefert die julianische Tageszahl der Sommersonnenwende zur Jahreszahl.
	/// </summary>
	/// <param name="year">Jahreszahl.</param>
	/// <returns>Julianische Tageszahl der Sommersonnenwende zur Jahreszahl.</returns>
	public static double SolsticeOfSummer(int year)
	{
		// Lokalen Felder einrichten
		double jd = 0; // Tageszahl der Näherung

		// Nach Jahreszahl unterscheiden
		if(year < 1000)
		{
			// Näherung berechnen
			double y = (double)year / 1000.0;
			jd = MMath.Polynome(y, 1721233.25401, 365241.72562, -0.05323,  0.00907,  0.00025);
		}
		else
		{
			// Näherung berechnen
			double y = ((double)year - 2000.0) / 1000.0;
			jd = MMath.Polynome(y, 2451716.56767, 365241.62603,  0.00325,  0.00888, -0.00030);
		}

		// Näherung korregieren
		return jd + MSun.T27C(jd);
	}

	// MSun.SolsticeOfWinter()
	/// <summary>
	/// Liefert die julianische Tageszahl der Wintersonnenwende zur aktuellen Jahreszahl.
	/// </summary>
	/// <returns>Julianische Tageszahl der Wintersonnenwende zur aktuellen Jahreszahl.</returns>
	public static double SolsticeOfWinter()
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		int y = MCalendar.GregorianYear(DateTime.Now.ToJdn());
		return MSun.SolsticeOfWinter(y);
	}

	// MSun.SolsticeOfWinter(int)
	/// <summary>
	/// Liefert die julianische Tageszahl der Wintersonnenwende zur Jahreszahl.
	/// </summary>
	/// <param name="year">Jahreszahl.</param>
	/// <returns>Julianische Tageszahl der Wintersonnenwende zur Jahreszahl.</returns>
	public static double SolsticeOfWinter(int year)
	{
		// Lokale Felder einrichten
		double jd = 0; // Tageszahl der Näherung

		// Nach Jahreszahl unterscheiden
		if(year < 1000)
		{
			// Näherung berechnen
			double y = (double)year / 1000.0;
			jd = MMath.Polynome(y, 1721414.39987, 365242.88257, -0.00769, -0.00933, -0.00006);
		}
		else
		{
			// Näherung berechnen
			double y = ((double)year - 2000.0) / 1000.0;
			jd = MMath.Polynome(y, 2451900.05952, 365242.74049, -0.06223, -0.00823,  0.00032);
		}

		// Näherung korregieren
		return jd + MSun.T27C(jd);
	}

	// MSun.Transit(CPolar, ref double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Meridiandurchgangs am geographischen Ort und zum aktuellen Systemdatum und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Meridiandurchgangs.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Transit(CPolar position, ref double jdEvent)
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double jd = DateTime.Now.ToJdn();
		double h  = 0.0;
		return MSun.Transit(position.Longitude, position.Latitude, ref jdEvent, jd, ref h);
	}

	// MSun.Transit(CPolar, ref double, double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Meridiandurchgangs am geographischen Ort und zur julianischen Tageszahl und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Meridiandurchgangs.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Transit(CPolar position, ref double jdEvent, double jd)
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double h = 0.0;
		return MSun.Transit(position.Longitude, position.Latitude, ref jdEvent, jd, ref h);
	}

	// MSun.Transit(CPolar, ref double, double, ref double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Meridiandurchgangs und die Höhe am geographischen Ort und zur aktuekllen Systemzeit und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Meridiandurchgangs.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="height">Höhe.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Transit(CPolar position, ref double jdEvent, double jd, ref double height){ return MSun.Transit(position.Longitude, position.Latitude, ref jdEvent, jd, ref height); }

	// MSun.Transit(double, double, ref double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Aufgangs am geographischen Ort und zur aktuellen Systemzeit und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Meridiandurchgangs.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Transit(double lambda, double phi, ref double jdEvent)
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double jd = DateTime.Now.ToJdn();
		double h  = 0.0;
		return MSun.Transit(lambda, phi, ref jdEvent, jd, ref h);
	}

	// MSun.Transit(double, double, ref double, double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Meridiandurchgangs am geographischen Ort und zur julianischen Tageszahl und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Meridiandurchgangs.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Transit(double lambda, double phi, ref double jdEvent, double jd)
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double h = 0.0;
		return MSun.Transit(lambda, phi, ref jdEvent, jd, ref h);
	}

	// MSun.Transit(double, double, ref double, double, ref double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Meridiandurchgangs und die Höhe am geographischen Ort und zur julianischen Tageszahl und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Meridiandurchgangs.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="height">Höhe.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Transit(double lambda, double phi, ref double jdEvent, double jd, ref double height)
	{
		// Lokale Felder einrichten
		double jdn = MMath.Floor(jd - 0.5) + 0.5; // Tageszahl um Mitternacht
		double l   = 0.0;                         // Geozentrische Länge
		double a   = 0.0;                         // Rektaszension
		double d   = 0.0;                         // Deklination
		double dm  = 1.0;                         // Korrekturglied

		// Position für nachfolgenden Tag berechnen
		l = MSun.Longitude(EPrecision.Low, jdn + 1.0);
		double aP = MEphemerides.ToAlpha(l, 0.0, EObliquity.Mean, jdn + 1.0);
		double dP = MEphemerides.ToDelta(l, 0.0, EObliquity.Mean, jdn + 1.0);

		// Position für gegebenen Tag berechnen
		l = MSun.Longitude(EPrecision.Low, jdn);
		double a0 = MEphemerides.ToAlpha(l, 0.0, EObliquity.Mean, jdn);
		if(MMath.Abs(aP - a0) > 1.0) a0 += MMath.Sgn(aP - a0) * MMath.Pi2;
		double d0 = MEphemerides.ToDelta(l, 0.0, EObliquity.Mean, jdn);

		// Position für vorhergehenden Tag berechnen
		l = MSun.Longitude(EPrecision.Low, jdn - 1.0);
		double aM = MEphemerides.ToAlpha(l, 0.0, EObliquity.Mean, jdn - 1.0);
		if(MMath.Abs(a0 - aM) > 1.0) aM += MMath.Sgn(a0 - aM) * MMath.Pi2;
		double dM = MEphemerides.ToDelta(l, 0.0, EObliquity.Mean, jdn - 1.0);

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

		// Ereigniszeit prüfen
		if(m < 0.0 || m >= 1.0) return EEventType.NoEvent;

		// Höhe berechnen
		d = MMath.Bessel(m, dM, d0, dP);
		height = MEphemerides.ToHeight(0.0, d, phi);

		// Iteration anwenden und Rückgabewert setzen
		jdEvent = jd + m;
		return EEventType.Normal;
	}
}
