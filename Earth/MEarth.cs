using Acamat.LCore;
using Acamat.LMath;
using Acamat.LMath.Geometry;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt Berechnungen zur Erde.
/// </summary>
public static partial class MEarth
{
	// ------------------- //
	// Felder und Methoden //
	// ------------------- //
	// MEarth.AnomalisticYear()
	/// <summary>
	/// Liefert die Dauer des anomalistischen Jahres.
	/// </summary>
	/// <returns>Dauer des anomalistischen Jahres.</returns>
	public static double AnomalisticYear(){ return 365.25964; }

	// MEarth.AppendEvent(List<CEvent>, double, double)
	/// <summary>
	/// Fügt die erdbezogenen Eregnisse zum Zeitraum an die Liste an.
	/// </summary>
	/// <param name="list">Liste.</param>
	/// <param name="jdMin">Beginn des Zeitraums.</param>
	/// <param name="jdMax">Ende des Zeitraums.</param>
	public static void AppendEvent(List<CEvent> list, double jdMin, double jdMax)
	{
		// Lokale Felder
		double jdn = 0.0;
		int    r   = 0;

		// -------- //
		// Aphelion //
		// -------- //

		// Berechnungsschleife
		jdn = jdMin;
		while(true)
		{
			// Nächstes Ereignis berechnen und Lage im Intervall bestimmen
			jdn = MEarth.Aphelion(jdn);
			r   = jdn.CompareTo(jdMin, jdMax);

			// Ereignisse vor dem Intervall verarbeiten
			if(r == -1)
				continue;

			// Ereignisse im Intervall verarbeiten
			if(r == 0)
			{
				// Ereignis an Liste anfügen und zum nächsten Durchlauf springen
				list.Add(new(jdn, "Erde: Durchgang durch das Aphel"));
				continue;
			}

			// Ereignis liegt nach dem Intervall --> Abbruch
			break; 
		}

		// ---------- //
		// Perihelion //
		// ---------- //

		// Berechnungsschleife
		jdn = jdMin;
		while(true)
		{
			// Nächstes Ereignis berechnen und Lage im Intervall bestimmen
			jdn = MEarth.Perihelion(jdn);
			r   = jdn.CompareTo(jdMin, jdMax);

			// Ereignisse vor dem Intervall verarbeiten
			if(r == -1)
				continue;

			// Ereignisse im Intervall verabeiten
			if(r == 0)
			{
				// Ereignis an Liste anfügen und zum nächsten Durchlauf springen
				list.Add(new(jdn, "Erde: Durchgang durch das Perihel"));
				continue;
			}

			// Ereignis liegt nach dem Intervall --> Abbruch
			break;
		}
	}

	// MEarth.Aphelion()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch das Aphel nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch das Aphel nach der aktuellen Systemzeit.</returns>
	public static double Aphelion()
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double jd = DateTime.Now.ToJdn();
		return MEarth.Aphelion(jd);
	}

	// MEarth.Aphelion(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch das Aphel nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch das Aphel nach der julianischen Tageszahl.</returns>
	public static double Aphelion(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor(0.99997 * (y - 2000.01)) - 0.5;
		double j = 0.0;

		// Berechnungsschleife
		while(j <= jd)
		{
			// Näherung berechnen
			k += 1.0;
			j  = MMath.Polynome(k, 2451547.507, 365.2596358, 0.0000000156);

			// Näherung korregieren
			j += -1.352 * MMath.Sin(MMath.ToRad(328.41 + 132.788585 * k));
			j +=  0.061 * MMath.Sin(MMath.ToRad(316.13 + 584.903153 * k));
			j +=  0.062 * MMath.Sin(MMath.ToRad(346.20 + 450.380738 * k));
			j +=  0.029 * MMath.Sin(MMath.ToRad(136.95 + 659.306737 * k));
			j +=  0.031 * MMath.Sin(MMath.ToRad(249.52 + 329.653368 * k));
		}
		return j;
	}

	// MEarth.Direction(CPolar, CPolar)
	/// <summary>
	/// Liefert die geographische Richtung zweier Orte auf der Erdoberfläche.
	/// </summary>
	/// <param name="positionA">Position des Ortes A.</param>
	/// <param name="positionB">Position des Ortes B.</param>
	/// <returns>Geographische Richtung zweier Orte auf der Erdoberfläche.</returns>
	public static double Direction(CPolar positionA, CPolar positionB){ return MEarth.Direction(positionA.Longitude, positionA.Latitude, positionB.Longitude, positionB.Latitude); }

	// MEarth.Direction(double, double, double, double)
	/// <summary>
	/// Liefert die geographische Richtung zweier Orte auf der Erdoberfläche.
	/// </summary>
	/// <param name="lamdaA">Geographische Länge des Ortes A.</param>
	/// <param name="phiA">Geographische Breite des Ortes A.</param>
	/// <param name="lamdaB">Geographische Länge des Ortes B.</param>
	/// <param name="phiB">Geographische Breite des Ortes B.</param>
	/// <returns>Geographische Richtung zweier Orte auf der Erdoberfläche.</returns>
	public static double Direction(double lamdaA, double phiA, double lamdaB, double phiB)
	{
		// Lokale Felder einrichten
		double d = MMath.Cos(phiA) * MMath.Tan(phiB) - MMath.Sin(phiA) * MMath.Cos(lamdaA - lamdaB);

		// Winkel berechnen und liefern
		if(d == 0.0) return 0.0;
		return MMath.Sin(lamdaA - lamdaB).ArcTan(d).Mod(MMath.Pi2);
	}

	// MEarth.Distance(CPolar, CPolar)
	/// <summary>
	/// Liefert die geographische Entfernung zweier Orte auf der Erdoberfläche.
	/// </summary>
	/// <param name="positionA">Position des Ortes A.</param>
	/// <param name="positionB">Position des Ortes B.</param>
	/// <returns>Geographische Entfernung zweier Orte auf der Erdoberfläche.</returns>
	public static double Distance(CPolar positionA, CPolar positionB){ return MEarth.Distance(positionA.Longitude, positionA.Latitude, positionB.Longitude, positionB.Latitude); }

	// MEarth.Distance(double, double, double, double)
	/// <summary>
	/// Liefert die geographische Entfernung zweier Orte auf der Erdoberfläche.
	/// </summary>
	/// <param name="lamdaA">Geographische Länge des Ortes A.</param>
	/// <param name="phiA">Geographische Breite des Ortes A.</param>
	/// <param name="lamdaB">Geographische Länge des Ortes B.</param>
	/// <param name="phiB">Geographische Breite des Ortes B.</param>
	/// <returns>Geographische Entfernung zweier Orte auf der Erdoberfläche.</returns>
	public static double Distance(double lamdaA, double phiA, double lamdaB, double phiB)
	{
		// Lokale Felder einrichten
		double f = (phiA   + phiB  ) / 2.0;
		double g = (phiA   - phiB  ) / 2.0;
		double l = (lamdaA - lamdaB) / 2.0;

		// Lokale Hilfsfelder einrichten
		double sinF = MMath.Sin(f);
		double sinG = MMath.Sin(g);
		double sinL = MMath.Sin(l);
		double cosF = MMath.Cos(f);
		double cosG = MMath.Cos(g);
		double cosL = MMath.Cos(l);

		// Koeffizienten berechnen
		double s = sinG * sinG * cosL * cosL + cosF * cosF * sinL * sinL;
		double c = cosG * cosG * cosL * cosL + sinF * sinF * sinL * sinL;

		// Kartesische Koordinaten berechnen
		double o  = (MMath.Sqr(s / c)).ArcTan();
		double r  = MMath.Sqr(s * c) / o;
		double d  = 2.0 * 6378.14 * o;
		double h1 = (3.0 * r - 1.0) / (2.0 * c);
		double h2 = (3.0 * r + 1.0) / (2.0 * s);

		// Entfernung berechnen
		return d * (1.0 + 0.003352858 * (h1 * sinF * sinF * cosG * cosG - h2 * cosF * cosF * sinG * sinG));
	}

	// MEarth.DistanceFromCenter(double)
	/// <summary>
	/// Liefert die Entfernung der geographischen Breite vom Erdmittelpunkt auf Meeresniveau.
	/// </summary>
	/// <param name="phi">Geographische Breite.</param>
	/// <returns>Entfernung der geographischen Breite vom Erdmittelpunkt auf Meeresniveau.</returns>
	public static double DistanceFromCenter(double phi)
	{
		// Rückgabe
		return 6378.14 * (0.9983271 + 0.0016764 * MMath.Cos(2.0 * phi) - 0.0000035 * MMath.Cos(4.0 * phi));
	}

	// MEarth.DraconicYear()
	/// <summary>
	/// Liefert die Dauer des drakonsischen Jahres.
	/// </summary>
	/// <returns>Dauer des drakonsischen Jahres.</returns>
	public static double DraconicYear(){ return 346.62005; }

	// MEarth.Eccentricity()
	/// <summary>
	/// Liefert die Exzentrizität der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Exzentrizität der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	public static double Eccentricity()
	{
		// Lokale Felder einrichten und Exzentrizität berechnen
		double jd = DateTime.Now.ToJdn();
		return MEarth.Eccentricity(jd);
	}

	// MEarth.Eccentricity(double)
	/// <summary>
	/// Liefert die Exzentrizität der mittleren Planetenbahn zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Exzentrizität der mittleren Planetenbahn zur julianischen Tageszahl.</returns>
	public static double Eccentricity(double jd)
	{
		// Lokale Felder einrichten und Exzentrizität berechnen
		double t = (jd - MCalendar.Jdn20000101) / 36525.0;
		return MMath.Polynome(t, 0.01670863, -0.000042037, -0.0000001267, 0.00000000014);
	}

	// MEarth.GeocentricLatitude(double)
	/// <summary>
	/// Liefert die geozentrische Breite zur geographischen Breite.
	/// </summary>
	/// <param name="phi">Geographische Breite.</param>
	/// <returns>Geozentrische Breite zur geographischen Breite.</returns>
	public static double GeocentricLatitude(double phi)
	{
		// Rückgabe
		return phi + (-692.73 * MMath.Sin(2.0 * phi) + 1.16 * MMath.Sin(4.0 * phi)) / 1296000.0;
	}

	// MEarth.Latitude(EPrecision)         » MEarth.Latitude.cs
	// MEarth.Latitude(EPrecision, double) » MEarth.Latitude.cs

	// MEarth.LengthOfLatitude(double)
	/// <summary>
	/// Liefert die Länge des Breitengrades zur geographischen Breite.
	/// </summary>
	/// <param name="phi">Geographische Breite.</param>
	/// <returns>Länge des Breitengrades zur geographischen Breite.</returns>
	public static double LengthOfLatitude(double phi)
	{
		// Rückgabe
		return MMath.ToRad(MEarth.RadiusOfCurvature(phi));
	}

	// MEarth.LengthOfLongitude(double)
	/// <summary>
	/// Liefert die Länge des Längengrades zur geographischen Breite.
	/// </summary>
	/// <param name="phi">Geographische Breite.</param>
	/// <returns>Länge des Längengrades zur geographischen Breite.</returns>
	public static double LengthOfLongitude(double phi)
	{
		// Rückgabe
		return MMath.ToRad(MEarth.ParallelOfLatitude(phi));
	}

	// MEarth.LinearVelocity(double)
	/// <summary>
	/// Liefert die Lineargeschwindigkeit zur geographischen Breite.
	/// </summary>
	/// <param name="phi">Geographische Breite.</param>
	/// <returns>Lineargeschwindigkeit zur geographischen Breite.</returns>
	public static double LinearVelocity(double phi)
	{
		// Rückgabe
		return 0.00007292114992 * MEarth.ParallelOfLatitude(phi);
	}

	// MEarth.Longitude(EPrecision)         » MEarth.Longitude.cs
	// MEarth.Longitude(EPrecision, double) » MEarth.Longitude.cs

	// MEarth.LongitudeOfPerihelion()
	/// <summary>
	/// Liefert die Länge des Perihels der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Länge des Perihels der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double LongitudeOfPerihelion()
	{
		// Lokale Felder einrichten und Länge berechnen
		double jd = DateTime.Now.ToJdn();
		return MEarth.LongitudeOfPerihelion(jd);
	}

	// MEarth.LongitudeOfPerihelion(double)
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
		return MMath.Polynome(t, 102.937348, 1.7195366, 0.00045688, -0.000000018);
	}

	// MEarth.MeanAnomaly()
	/// <summary>
	/// Liefert die mittlere Anomalie der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Mittlere Anomalie der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double MeanAnomaly()
	{
		// Lokale Felder einrichten und Anomalie berechnen
		double jd = DateTime.Now.ToJdn();
		return MEarth.MeanAnomaly(jd);
	}

	// MEarth.MeanAnomaly(double)
	/// <summary>
	/// Liefert die mittlere Anomalie der mittleren Planetenbahn zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Mittlere Anomalie der mittleren Planetenbahn zur julianischen Tageszahl.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double MeanAnomaly(double jd)
	{
		// Rückgabe
		return MEarth.MeanLongitude(jd) + MEarth.LongitudeOfPerihelion(jd).Mod(360.0);
	}

	// MEarth.MeanLongitude()
	/// <summary>
	/// Liefert die mittlere Länge der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Mittlere Länge der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double MeanLongitude()
	{
		// Lokale Felder einrichten und Länge berechnen
		double jd = DateTime.Now.ToJdn();
		return MEarth.MeanLongitude(jd);
	}

	// MEarth.MeanLongitude(double)
	/// <summary>
	/// Liefert die mittlere Länge der mittleren Planetenbahn zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Mittlere Länge der mittleren Planetenbahn zur julianischen Tageszahl.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double MeanLongitude(double jd)
	{
		// Lokale Felder einrichten und Länge berechnen
		double t = (jd - MCalendar.Jdn20000101) / 36525.0;
		return MMath.Polynome(t, 100.466457, 36000.7698278, 0.00030322, 0.000000020).Mod(360.0);
	}

	// MEarth.ParallelOfLatitude(double)
	/// <summary>
	/// Liefert den Radius des Breitenkreises zur geographischen Breite.
	/// </summary>
	/// <param name="phi">Geographische Breite.</param>
	/// <returns>Radius des Breitenkreises zur geographischen Breite.</returns>
	public static double ParallelOfLatitude(double phi)
	{
		// Lokale Felder einrichten
		double e = 0.081819221;
		double n = 6378.14 * MMath.Cos(phi);
		double h = e * MMath.Sin(phi);
		double d = MMath.Sqr(1.0 - h * h);

		// Längenparallele berechnen
		return n / d;
	}

	// MEarth.Perihelion
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch das Perihel nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch das Perihel nach der aktuellen Systemzeit.</returns>
	public static double Perihelion()
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double jd = DateTime.Now.ToJdn();
		return MEarth.Perihelion(jd);
	}

	// MEarth.Perihelion(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch das Perihel nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch das Perihel nach der julianischen Tageszahl.</returns>
	public static double Perihelion(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor(0.99997 * (y - 2000.01)) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while(j <= jd)
		{
			// Näherung berechnen
			k += 1.0;
			j  = MMath.Polynome(k, 2451547.507, 365.2596358, 0.0000000156);

			// Näherung korregieren
			j +=  1.278 * MMath.Sin(MMath.ToRad(328.41 + 132.788585 * k));
			j += -0.055 * MMath.Sin(MMath.ToRad(316.13 + 584.903153 * k));
			j += -0.091 * MMath.Sin(MMath.ToRad(346.20 + 450.380738 * k));
			j += -0.056 * MMath.Sin(MMath.ToRad(136.95 + 659.306737 * k));
			j += -0.045 * MMath.Sin(MMath.ToRad(249.52 + 329.653368 * k));
		}
		return j;
	}

	// MEarth.PositionEcliptical(EPrecision)
	/// <summary>
	/// Liefert die heliozentrisch-ekliptikale Position zur aktuellen Systemzeit.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <returns>Heliozentrisch-ekliptikale Position zur aktuellen Systemzeit.</returns>
	public static CPolar PositionEcliptical(EPrecision value)
	{
		// Lokale Felder einrichten
		CPolar rtn = new CPolar();
		rtn.Latitude  = MEarth.Latitude (value);
		rtn.Longitude = MEarth.Longitude(value);
		rtn.Radius    = MEarth.Radius   (value);
		return rtn;
	}

	// MEarth.PositionEcliptical(EPrecision, double)
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
		rtn.Latitude  = MEarth.Latitude (value, jd);
		rtn.Longitude = MEarth.Longitude(value, jd);
		rtn.Radius    = MEarth.Radius   (value, jd);
		return rtn;
	}

	// MEarth.Radius(EPrecision)         » MEarth.Radius.cs
	// MEarth.Radius(EPrecision, double) » MEarth.Radius.cs

	// MEarth.RadiusOfCurvature(double)
	/// <summary>
	/// Liefert den Radius des Längenkreises zur geographischen Breite.
	/// </summary>
	/// <param name="phi">Geographische Breite.</param>
	/// <returns>Radius des Längenkreises zur geographischen Breite.</returns>
	public static double RadiusOfCurvature(double phi)
	{
		// Lokale Felder einrichten
		double e = 0.081819221;
		double h = e * MMath.Sin(phi);

		// Krümmungsradius berechnen
		return 6335.4423 / MMath.Pow((1.0 - h * h), 1.5);
	}

	// MEarth.SemimajorAxis()
	/// <summary>
	/// Liefert die große Halbachse der mittleren Planetenbahn.
	/// </summary>
	/// <returns>Große Halbachse der mittleren Planetenbahn.</returns>
	public static double SemimajorAxis(){ return 1.000001018; }

	// MEarth.SiderealYear()
	/// <summary>
	/// Liefert die Dauer des siderischen Jahres.
	/// </summary>
	/// <returns>Dauer des siderischen Jahres.</returns>
	public static double SiderealYear(){ return 365.25636; }

	// MEarth.TropicalYear()
	/// <summary>
	/// Liefert die Dauer des tropischen Jahres zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Dauer des tropischen Jahres zur aktuellen Systemzeit.</returns>
	public static double TropicalYear()
	{
		// Lokale Felder einrichten und Länge berechnen
		double jd = DateTime.Now.ToJdn();
		return MEarth.TropicalYear(jd);
	}

	// MEarth.TropicalYear(double)
	/// <summary>
	/// Liefert die Dauer des tropischen Jahres zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Dauer des tropischen Jahres zur julianischen Tageszahl.</returns>
	public static double TropicalYear(double jd)
	{
		// Lokale Felder einrichten und Länge berechnen
		double t = (jd - MCalendar.Jdn20000101) / 36525.0;
		return MMath.Polynome(t, 365.2421896698, -0.00000615359, -0.000000000729, 0.000000000264);
	}
}
