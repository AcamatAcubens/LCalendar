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
	// MSun.AppendEvent(List<CEvent>, double, double)
	/// <summary>
	/// Fügt die sonnenbezogenen Eregnisse zum Zeitraum an die Liste an.
	/// </summary>
	/// <param name="list">Liste.</param>
	/// <param name="jdMin">Beginn des Zeitraums.</param>
	/// <param name="jdMax">Ende des Zeitraums.</param>
	public static void AppendEvent(List<CEvent> list, double jdMin, double jdMax)
	{
		// Lokale Felder
		double jdn = 0.0;
		int    r   = 0;
		int    y   = 0;

		// --------------- //
		// EquinoxOfAutumn //
		// --------------- //

		// Berechnungsschleife
		jdn = jdMin;
		y   = MCalendar.GregorianYear(jdn);
		while(true)
		{
			// Nächstes Ereignis berechnen und Lage im Intervall bestimmen
			jdn = MSun.EquinoxOfAutumn(y++);
			r   = jdn.CompareTo(jdMin, jdMax);

			// Ereignisse vor dem Intervall verarbeiten
			if(r == -1)
				continue;

			// Ereignisse im Intervall verarbeiten
			if(r == 0)
			{
				// Ereignis an Liste anfügen und zum nächsten Durchlauf springen
				list.Add(new(jdn, "Sonne: Herbstäquinoktium"));
				continue;
			}

			// Ereignis liegt nach dem Intervall --> Abbruch
			break;
		}

		// --------------- //
		// EquinoxOfSpring //
		// --------------- //

		// Berechnungsschleife
		jdn = jdMin;
		y   = MCalendar.GregorianYear(jdn);
		while(true)
		{
			// Nächstes Ereignis berechnen und Lage im Intervall bestimmen
			jdn = MSun.EquinoxOfSpring(y++);
			r   = jdn.CompareTo(jdMin, jdMax);

			// Ereignisse vor dem Intervall verarbeiten
			if(r == -1)
				continue;
			
			// Ereignisse im Intervall verarbeiten
			if(r == 0)
			{
				// Ereignis an Liste anfügen und zum nächsten Durchlauf springen
				list.Add(new(jdn, "Sonne: Frühlingsäquinoktium"));
				continue;
			}

			// Ereignis liegt nach dem Intervall --> Abbruch
			break;
		}

		// ---------------- //
		// SolsticeOfSummer //
		// ---------------- //

		// Berechnungsschleife
		jdn = jdMin;
		y   = MCalendar.GregorianYear(jdn);
		while(true)
		{
			// Nächstes Ereignis berechnen und Lage im Intervall bestimmen
			jdn = MSun.SolsticeOfSummer(y++);
			r   = jdn.CompareTo(jdMin, jdMax);

			// Ereignisse vor dem Intervall verabeiten
			if(r == -1)
				continue;

			// Ereignisse im Intervall verarbeiten
			if(r == 0)
			{
				// Ereignis an Liste anfügen und zum nächsten Durchlauf springen
				list.Add(new(jdn, "Sonne: Sommersonnenwende"));
				continue;
			}

			// Ereignis liegt nach dem Intervall --> Abbruch
			break;
		}

		// ---------------- //
		// SolsticeOfWinter //
		// ---------------- //

		// Berechnungsschleife
		jdn = jdMin;
		y   = MCalendar.GregorianYear(jdn);
		while(true)
		{
			// Nächstes Ereignis berechnen und Lage im Intervall bestimmen
			jdn = MSun.SolsticeOfWinter(y++);
			r   = jdn.CompareTo(jdMin, jdMax);

			// Ereignisse vor dem Intervall verarbeiten
			if(r == -1)
				continue;

			// Ereignisse im Intervall verarbeiten
			if(r == 0)
			{
				// Ereignis an Liste anfügen und zum nächsten Durchlauf springen
				list.Add(new(jdn, "Sonne: Wintersonnenwende"));
				continue;
			}

			// Ereignis liegt nach dem Intervall --> Abbruch
			break;
		}
	}

	// MSun.Dawn(CPolar)
	/// <summary>
	/// Liefert die Ereigniskennung und die julianische Tageszahl der bürgerlichen Morgendämmerung am geographischen Ort und zum aktuellen Systemdatum.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <returns>Ereigniskennung und die julianische Tageszahl der bürgerlichen Morgendämmerung am geographischen Ort und zum aktuellen Systemdatum.</returns>
	public static (EEventType type, double? jd) Dawn(CPolar position){ return MSun.Dawn(position.Longitude, position.Latitude, DateTime.Now.ToJdn(), MEphemerides.GeocentricHeight_TwilightCivil); }

	// MSun.Dawn(CPolar, double)
	/// <summary>
	/// Liefert die Ereigniskennung und die julianische Tageszahl der bürgerlichen Morgendämmerung am geographischen Ort und zum julianischen Tagesdatum.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <param name="jd">Julianisches Tagesdatum.</param>
	/// <returns>Liefert die Ereigniskennung und die julianische Tageszahl der bürgerlichen Morgendämmerung am geographischen Ort und zum julianischen Tagesdatum.</returns>
	public static (EEventType type, double? jd) Dawn(CPolar position, double jd){ return MSun.Dawn(position.Longitude, position.Latitude, jd, MEphemerides.GeocentricHeight_TwilightCivil); }

	// MSun.Dawn(double, double)
	/// <summary>
	/// Liefert die Ereigniskennung und die julianische Tageszahl der bürgerlichen Morgendämmerung am geographischen Ort und zum aktuellen Systemdatum.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite</param>
	/// <returns>Ereigniskennung und die julianische Tageszahl der bürgerlichen Morgendämmerung am geographischen Ort und zum aktuellen Systemdatum.</returns>
	public static (EEventType type, double? jd) Dawn(double lambda, double phi){ return MSun.Dawn(lambda, phi, DateTime.Now.ToJdn(), MEphemerides.GeocentricHeight_TwilightCivil); }

	// MSun.Dawn(double, double, double)
	/// <summary>
	/// Liefert die Ereigniskennung und die julianische Tageszahl der bürgerlichen Morgendämmerung am geographischen Ort und zum julianischen Tagesdatum.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite</param>
	/// <param name="jd">Julianisches Tagesdatum.</param>
	/// <returns>Ereigniskennung und die julianische Tageszahl der bürgerlichen Morgendämmerung am geographischen Ort und zum julianischen Tagesdatum.</returns>
	public static (EEventType type, double? jd) Dawn(double lambda, double phi, double jd){ return MSun.Dawn(lambda, phi, jd, MEphemerides.GeocentricHeight_TwilightCivil); }

	// MSun.Dawn(double, double, double, double)
	/// <summary>
	/// Liefert die Ereigniskennung und die julianische Tageszahl der Morgendämmerung am geographischen Ort, zum julianischen Tagesdatum und zur geozentrischen Höhe.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="height">Geozentrische Höhe.</param>
	/// <returns>Ereigniskennung und die julianische Tageszahl der Morgendämmerung am geographischen Ort, zum julianischen Tagesdatum und zur geozentrischen Höhe.</returns>
	public static (EEventType type, double? jd) Dawn(double lambda, double phi, double jd, double height)
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
		if(MMath.Abs(aP - a0) > 1.0)
			a0 += MMath.Sgn(aP - a0) * MMath.Pi2;
		double d0 = MEphemerides.ToDelta(l, 0.0, EObliquity.Mean, jd);

		// Position für vorhergehenden Tag berechnen
		l = MSun.Longitude(EPrecision.Low, jdn - 1.0);
		double aM = MEphemerides.ToAlpha(l, 0.0, EObliquity.Mean, jdn - 1.0);
		if(MMath.Abs(a0 - aM) > 1.0)
			aM += MMath.Sgn(a0 - aM) * MMath.Pi2;
		double dM = MEphemerides.ToDelta(l, 0.0, EObliquity.Mean, jdn - 1.0);

		// Stundenwinkel berechnen und prüfen
		double cosH = (MMath.Sin(height) - sinP * MMath.Sin(dP)) / (cosP * MMath.Cos(dP));
		if(MMath.Abs(cosH) > 1.0)
			return(cosH < 1.0 ? EEventType.AlwaysAboveHorizon : EEventType.AlwaysBeneathHorizon, null);
		H = MMath.ArcCos(cosH);

		// -------------//
		// Ereigniszeit //
		// -------------//

		// Sternzeit und Stundenwinkel zum gegebenen Zeitpunkt bestimmen
		double t0 = MEphemerides.Gmst(jdn);
		double m = ((a0 + lambda - t0 - H) / MMath.Pi2).Div();

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

		// Kein Ereignis verarbeiten
		if(m < 0.0 | m >= 1.0)
			return(EEventType.NoEvent, null);

		// Rückgabe
		return(EEventType.Normal, jd + m);
	}

	// MSun.Dusk(CPolar)
	/// <summary>
	/// Liefert die Ereigniskennung und die julianische Tageszahl der bürgerlichen Abenddämmerung am geographischen Ort und zum aktuellen Systemdatum.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <returns>Ereigniskennung und die julianische Tageszahl der bürgerlichen Abenddämmerung am geographischen Ort und zum aktuellen Systemdatum.</returns>
	public static (EEventType type, double? jd) Dusk(CPolar position){ return MSun.Dusk(position.Longitude, position.Latitude, DateTime.Now.ToJdn(), MEphemerides.GeocentricHeight_TwilightCivil); }

	// MSun.Dusk(CPolar, double)
	/// <summary>
	/// Liefert die Ereigniskennung und die julianische Tageszahl der bürgerlichen Abenddämmerung am geographischen Ort und zum julianischen Tagesdatum.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <param name="jd">Julianisches Tagesdatum.</param>
	/// <returns>Ereigniskennung und die julianische Tageszahl der bürgerlichen Abenddämmerung am geographischen Ort und zum julianischen Tagesdatum.</returns>
	public static (EEventType type, double? jd) Dusk(CPolar position, double jd){ return MSun.Dusk(position.Longitude, position.Latitude, jd, MEphemerides.GeocentricHeight_TwilightCivil); }

	// MSun.Dusk(double, double)
	/// <summary>
	/// Liefert die Ereigniskennung und die julianische Tageszahl der bürgerlichen Abenddämmerung am geographischen Ort und zum aktuellen Systemdatum.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite</param>
	/// <returns>Ereigniskennung und die julianische Tageszahl der bürgerlichen Abenddämmerung am geographischen Ort und zum aktuellen Systemdatum.</returns>
	public static (EEventType type, double? jd) Dusk(double lambda, double phi){ return MSun.Dusk(lambda, phi, DateTime.Now.ToJdn(), MEphemerides.GeocentricHeight_TwilightCivil); }

	// MSun.Dusk(double, double, double)
	/// <summary>
	/// Liefert die Ereigniskennung und die julianische Tageszahl der bürgerlichen Abenddämmerung am geographischen Ort und zum julianischen Tagesdatum.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Georgraphische Breite.</param>
	/// <param name="jd">Julianisches Tagesdatum.</param>
	/// <returns>Ereigniskennung und die julianische Tageszahl der bürgerlichen Abenddämmerung am geographischen Ort und zum julianischen Tagesdatum.</returns>
	public static (EEventType type, double? jd) Dusk(double lambda, double phi, double jd){ return MSun.Dusk(lambda, phi, jd, MEphemerides.GeocentricHeight_TwilightCivil); }

	// MSun.Dusk(double, double, double, double)
	/// <summary>
	/// Liefert die Ereigniskennung und die julianische Tageszahl der Abenddämmerung am geographischen Ort, zum julianischen Tagesdatum und zur geozentrischen Höhe.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Georgraphische Breite.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="height">Geozentrische Höhe.</param>
	/// <returns>Ereigniskennung und die julianische Tageszahl der Abenddämmerung am geographischen Ort, zum julianischen Tagesdatum und zur geozentrischen Höhe.</returns>
	public static (EEventType type, double? jd) Dusk(double lambda, double phi, double jd, double height)
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
		if(MMath.Abs(aP - a0) > 1.0)
			a0 += MMath.Sgn(aP - a0) * MMath.Pi2;
		double d0 = MEphemerides.ToDelta(l, 0.0, EObliquity.Mean, jdn);

		// Position für vorhergehenden Tag berechnen
		l = MSun.Longitude(EPrecision.Low, jdn + 1.0);
		double aM = MEphemerides.ToAlpha(l, 0.0, EObliquity.Mean, jdn - 1.0);
		if(MMath.Abs(a0 - aM) > 1.0)
			aM += MMath.Sgn(a0 - aM) * MMath.Pi2;
		double dM = MEphemerides.ToDelta(l, 0.0, EObliquity.Mean, jdn - 1.0);

		// Stundenwinkel berechnen und prüfen
		double cosH = (MMath.Sin(height) - sinP * MMath.Sin(dP)) / (cosP * MMath.Cos(dP));
		if(MMath.Abs(cosH) > 1.0)
			return(cosH < 1.0 ? EEventType.AlwaysAboveHorizon : EEventType.AlwaysBeneathHorizon, null);
		H = MMath.ArcCos(cosH);

		// ------------------- //
		// Ereigniszeit nähern //
		// ------------------- //

		// Sternzeit und Stundenwinkel zum gegebenen Zeitpunkt bestimmen
		double t0 = MEphemerides.Gmst(jdn);
		double m  = ((a0 + lambda - t0 + H) / MMath.Pi2).Div();

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

		// Kein Ereignis verarbeiten
		if(m < 0.0 | m >= 1.0)
			return(EEventType.NoEvent, null);

		// Rückgabe
		return(EEventType.Normal, jd + m);
	}

	// MSun.EquinoxOfAutumn()
	/// <summary>
	/// Liefert die julianische Tageszahl des Herbstäquinoktiums zum aktuellen Jahr.
	/// </summary>
	/// <returns>Julianische Tageszahl des Herbstäquinoktiums zum aktuellen Jahr.</returns>
	public static double EquinoxOfAutumn(){ return MSun.EquinoxOfAutumn(MCalendar.GregorianYear(DateTime.Now.ToJdn())); }

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
	public static double EquinoxOfSpring(){ return MSun.EquinoxOfSpring(MCalendar.GregorianYear(DateTime.Now.ToJdn())); }

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
	public static double Latitude(EPrecision value){ return MSun.Latitude(value, DateTime.Now.ToJdn()); }

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
	public static double Longitude(EPrecision value){ return MSun.Longitude(value, DateTime.Now.ToJdn()); }

	// MSun.Longitude(EPrecision, double)
	/// <summary>
	/// Liefert die geozentrisch-ekliptikale Länge zur julianischen Tageszahl.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Geozentrisch-ekliptikale Länge zur julianischen Tageszahl.</returns>
	public static double Longitude(EPrecision value, double jd){ return (MMath.Pi + MEarth.Longitude(value, jd)).Mod(MMath.Pi2); }

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
	public static double Radius(EPrecision value){ return MSun.Radius(value, DateTime.Now.ToJdn()); }

	// MSun.Radius(EPrecision, double)
	/// <summary>
	/// Liefert den geozentrisch-ekliptikalen Radius zur julianischen Tageszahl.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Geozentrisch-ekliptikalen Radius zur julianischen Tageszahl.</returns>
	public static double Radius(EPrecision value, double jd){ return MEarth.Radius(value, jd); }

	// MSun.SolsticeOfSummer()
	/// <summary>
	/// /// Liefert die julianische Tageszahl der Sommersonnenwende zur aktuellen Jahreszahl.
	/// </summary>
	/// <returns>Julianische Tageszahl der Sommersonnenwende zur aktuellen Jahreszahl.</returns>
	public static double SolsticeOfSummer(){ return MSun.SolsticeOfSummer(MCalendar.GregorianYear(DateTime.Now.ToJdn())); }

	// MSun.SolsticeOfSummer(int)
	/// <summary>
	/// /// Liefert die julianische Tageszahl der Sommersonnenwende zur Jahreszahl.
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
	public static double SolsticeOfWinter(){ return MSun.SolsticeOfWinter(MCalendar.GregorianYear(DateTime.Now.ToJdn())); }

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
}
