using Acamat.LCore;
using Acamat.LMath;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt Berechnungen zur Venus.
/// </summary>
public partial class MVenus
{
	// ------------------- //
	// Felder und Methoden //
	// ------------------- //
	// MVenus.Aphelion()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch das Aphel nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch das Aphel nach der aktuellen Systemzeit.</returns>
	public static double Aphelion()
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double jd = DateTime.Now.ToJdn();
		return MVenus.Aphelion(jd);
	}

	// MVenus.Aphelion(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch das Aphel nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch das Aphel nach der julianischen Tageszahl.</returns>
	public static double Aphelion(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor(1.62549 * (y - 2000.53)) - 0.5;
		double j = 0.0;

		// Berechnungsschleife
		while(j <= jd)
		{
			// Tageszahl berechnen
			k += 1.0;
			j  = MMath.Polynome(k, 2451738.233, 224.7008188, -0.0000000327);
		}
		return j;
	}

	// MVenus.ConjunctionInferior()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die untere Konjunktion mit der Sonne nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die untere Konjunktion mit der Sonne nach der aktuellen Systemzeit.</returns>
	public static double ConjunctionInferior()
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double jd = DateTime.Now.ToJdn();
		return MVenus.ConjunctionInferior(jd);
	}

	// MVenus.ConjunctionInferior(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die untere Konjunktion mit der Sonne nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die untere Konjunktion mit der Sonne nach der julianischen Tageszahl.</returns>
	public static double ConjunctionInferior(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor((365.2425 * y + 1721060.0 - 2451996.706) / 583.921361) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while(j <= jd)
		{
			// Näherung berechnen
			k += 1.0;
			j  = 2451996.706 + k * 583.921361;

			// Hilfsfelder berechnen
			double m = MMod.Mod(MMath.ToRad(82.7311 + k * 215.513058), MMath.Pi2);
			double t = (j - MCalendar.Jdn20000101) / 36525.0;
			double h;

			// Korrektur berechnen und anwenden
			h  = MMath.Polynome(t, -0.0096,  0.0002, -0.00001);
			h += MMath.Polynome(t,  2.0009, -0.0033, -0.00001) * MMath.Sin(      m);
			h += MMath.Polynome(t,  0.5980, -0.0104,  0.00001) * MMath.Cos(      m);
			h += MMath.Polynome(t,  0.0967, -0.0018, -0.00003) * MMath.Sin(2.0 * m);
			h += MMath.Polynome(t,  0.0913,  0.0019, -0.00002) * MMath.Cos(2.0 * m);
			h += MMath.Polynome(t,  0.0046, -0.0002          ) * MMath.Sin(3.0 * m);
			h += MMath.Polynome(t,  0.0079,  0.0001          ) * MMath.Cos(3.0 * m);
			j += h;
		}
		return j;
	}

	// MVenus.ConjunctionSuperior()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die obere Konjunktion mit der Sonne nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die obere Konjunktion mit der Sonne nach der aktuellen Systemzeit.</returns>
	public static double ConjunctionSuperior()
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double jd = DateTime.Now.ToJdn();
		return MVenus.ConjunctionSuperior(jd);
	}

	// MVenus.ConjunctionSuperior(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die obere Konjunktion mit der Sonne nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die obere Konjunktion mit der Sonne nach der julianischen Tageszahl.</returns>
	public static double ConjunctionSuperior(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor((365.2425 * y + 1721060.0 - 2451704.746) / 583.921361) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while(j <= jd)
		{
			// Näherung berechnen
			k += 1.0;
			j  = 2451704.746 + k * 583.921361;

			// Hilfsfelder berechnen
			double m = MMod.Mod(MMath.ToRad(154.9745 + k * 215.513058), MMath.Pi2);
			double t = (j - MCalendar.Jdn20000101) / 36525.0;
			double h;

			// Korrektur berechnen und anwenden
			h  = MMath.Polynome(t,  0.0099, -0.0002, -0.00001);
			h += MMath.Polynome(t,  4.1991, -0.0121, -0.00003) * MMath.Sin(      m);
			h += MMath.Polynome(t, -0.6095,  0.0102, -0.00002) * MMath.Cos(      m);
			h += MMath.Polynome(t,  0.2500, -0.0028, -0.00003) * MMath.Sin(2.0 * m);
			h += MMath.Polynome(t,  0.0063,  0.0025, -0.00002) * MMath.Cos(2.0 * m);
			h += MMath.Polynome(t,  0.0232, -0.0005, -0.00001) * MMath.Sin(3.0 * m);
			h += MMath.Polynome(t,  0.0031,  0.0004          ) * MMath.Cos(3.0 * m);
			j += h;
		}
		return j;
	}

	// MVenus.Eccentricity()
	/// <summary>
	/// Liefert die Exzentrizität der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Exzentrizität der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	public static double Eccentricity()
	{
		// Lokale Felder einrichten und Exzentrizität berechnen
		double jd = DateTime.Now.ToJdn();
		return MVenus.Eccentricity(jd);
	}

	// MVenus.Eccentricity(double)
	/// <summary>
	/// Liefert die Exzentrizität der mittleren Planetenbahn zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Exzentrizität der mittleren Planetenbahn zur julianischen Tageszahl.</returns>
	public static double Eccentricity(double jd)
	{
		// Lokale Felder einrichten und Extentrizität berechnen
		double t = (jd - MCalendar.Jdn20000101) / 36525.0;
		return MMath.Polynome(t, 0.00677192, -0.000047765, 0.0000000981, 0.00000000046);
	}

	// MVenus.GreatesEasternElongation()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die Ostelongation nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die Ostelongation nach der aktuellen Systemzeit.</returns>
	public static double GreatestEasternElongation()
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double  e = 0.0;
		double jd = DateTime.Now.ToJdn();
		return MVenus.GreatestEasternElongation(jd, ref e);
	}

	// MVenus.GreatestEasternElongation(ref double)
	/// <summary>
	/// Setzt den Elongationswinkel und liefert die julianische Tageszahl des nächsten Durchgangs durch die Ostelongation nach der aktuellen Systemzeit.
	/// </summary>
	/// <param name="elongation">Elongationswinkel.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die Ostelongation nach der aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double GreatestEasternElongation(ref double elongation)
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double jd = DateTime.Now.ToJdn();
		return MVenus.GreatestEasternElongation(jd, ref elongation);
	}

	// MVenus.GreatestEasternElongation(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die Ostelongation nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die Ostelongation nach der julianischen Tageszahl.</returns>
	public static double GreatestEasternElongation(double jd)
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double e = 0.0;
		return MVenus.GreatestEasternElongation(jd, ref e);
	}

	// MVenus.GreatestEasternElongation(double, ref double)
	/// <summary>
	/// Setzt den Elongationswinkel und liefert die julianische Tageszahl des nächsten Durchgangs durch die Ostelongation nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="elongation">Elongationswinkel.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die Ostelongation nach der julianischen Tageszahl.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double GreatestEasternElongation(double jd, ref double elongation)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor((365.2425 * y + 1721060.0 - 2451996.706) / 583.921361) - 1.0;
		double j = 0.0;
		double m = 0.0;
		double t = 0.0;
		double h;

		// ------------------- //
		// Tageszahl berechnen //
		// ------------------- //
		
		// Berechnungsschleife
		while(j <= jd)
		{
			// Näherung berechnen
			k += 1.0;
			j  = 2451996.706 + k * 583.921361;

			// Hilfsfelder berechnen
			m = MMod.Mod(MMath.ToRad(82.7311 + k * 215.513058), MMath.Pi2);
			t = (j - MCalendar.Jdn20000101) / 36525.0;

			// Korrektur berechnen und anwenden
			h  = MMath.Polynome(t, -70.7600,  0.0002, -0.00001);
			h += MMath.Polynome(t,   1.0282, -0.0010, -0.00001) * MMath.Sin(      m);
			h += MMath.Polynome(t,   0.2761, -0.0060          ) * MMath.Cos(      m);
			h += MMath.Polynome(t,  -0.0438, -0.0023,  0.00002) * MMath.Sin(2.0 * m);
			h += MMath.Polynome(t,   0.1660, -0.0037, -0.00004) * MMath.Cos(2.0 * m);
			h += MMath.Polynome(t,   0.0036,  0.0001          ) * MMath.Sin(3.0 * m);
			h += MMath.Polynome(t,  -0.0011,  0.0000,  0.00001) * MMath.Cos(3.0 * m);
			j += h;
		}

		// ----------------- //
		// Elongationswinkel //
		// ----------------- //

		// Elongationswinkel berechnen
		h  = MMath.Polynome(t, 46.3173,  0.0001);
		h += MMath.Polynome(t,  0.6916, -0.0024) * MMath.Sin(      m);
		h += MMath.Polynome(t,  0.6676, -0.0045) * MMath.Cos(      m);
		h += MMath.Polynome(t,  0.0309, -0.0002) * MMath.Sin(2.0 * m);
		h += MMath.Polynome(t,  0.0036, -0.0001) * MMath.Cos(2.0 * m);

		// Rückgabewerte setzen
		elongation = h;
		return j;
	}

	// MVenus.GreatesWesternElongation()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die Westelongation nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die Westelongation nach der aktuellen Systemzeit.</returns>
	public static double GreatestWesternElongation()
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double  e = 0.0;
		double jd = DateTime.Now.ToJdn();
		return MVenus.GreatestWesternElongation(jd, ref e);
	}

	// MVenus.GreatestWesternElongation(ref double)
	/// <summary>
	/// Setzt den Elongationswinkel und liefert die julianische Tageszahl des nächsten Durchgangs durch die Westelongation nach der aktuellen Systemzeit.
	/// </summary>
	/// <param name="elongation">Elongationswinkel.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die Westelongation nach der aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double GreatestWesternElongation(ref double elongation)
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double jd = DateTime.Now.ToJdn();
		return MVenus.GreatestWesternElongation(jd, ref elongation);
	}

	// MVenus.GreatestWesternElongation(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die Westelongation nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die Westelongation nach der julianischen Tageszahl.</returns>
	public static double GreatestWesternElongation(double jd)
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double e = 0.0;
		return MVenus.GreatestWesternElongation(jd, ref e);
	}

	// MVenus.GreatestWesternElongation(double, ref double)
	/// <summary>
	/// Setzt den Elongationswinkel und liefert die julianische Tageszahl des nächsten Durchgangs durch die Westelongation nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="elongation">Elongationswinkel.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die Westelongation nach der julianischen Tageszahl.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double GreatestWesternElongation(double jd, ref double elongation)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor((365.2425 * y + 1721060.0 - 2451996.706) / 583.921361) - 1.0;
		double j = 0.0;
		double m = 0.0;
		double t = 0.0;
		double h;

		// ------------------- //
		// Tageszahl berechnen //
		// ------------------- //
		
		// Berechnungsschleife
		while(j <= jd)
		{
			// Näherung berechnen
			k += 1.0;
			j  = 2451996.706 + k * 583.921361;

			// Hilfsfelder berechnen
			m = MMod.Mod(MMath.ToRad(82.7311 + k * 215.513058), MMath.Pi2);
			t = (j - MCalendar.Jdn20000101) / 36525.0;

			// Korrektur berechnen und anwenden
			h  = MMath.Polynome(t, 70.7462,  0.0000, -0.00001);
			h += MMath.Polynome(t,  1.1218, -0.0025, -0.00001) * MMath.Sin(      m);
			h += MMath.Polynome(t,  0.4538, -0.0066          ) * MMath.Cos(      m);
			h += MMath.Polynome(t,  0.1320,  0.0020, -0.00003) * MMath.Sin(2.0 * m);
			h += MMath.Polynome(t, -0.0702,  0.0022,  0.00004) * MMath.Cos(2.0 * m);
			h += MMath.Polynome(t,  0.0062, -0.0001          ) * MMath.Sin(3.0 * m);
			h += MMath.Polynome(t,  0.0015,  0.0000, -0.00001) * MMath.Cos(3.0 * m);
			j += h;
		}

		// ----------------- //
		// Elongationswinkel //
		// ----------------- //

		// Elongationswinkel berechnen
		h  =                   46.3245;
		h += MMath.Polynome(t, -0.5366, -0.0003,  0.00001) * MMath.Sin(      m);
		h += MMath.Polynome(t,  0.3097,  0.0016, -0.00001) * MMath.Cos(      m);
		h +=                   -0.0163                     * MMath.Sin(2.0 * m);
		h += MMath.Polynome(t, -0.0075,  0.0001          ) * MMath.Cos(2.0 * m);

		// Rückgabewerte setzen
		elongation = h;
		return j;
	}

	// MVenus.Inclination()
	/// <summary>
	/// Liefert die Neigung der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Neigung der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double Inclination()
	{
		// Lokale Felder einrichten und Bahnneigung berechnen
		double jd = DateTime.Now.ToJdn();
		return MVenus.Inclination(jd);
	}

	// MVenus.Inclination(double)
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
		return MMath.Polynome(t, 3.394662, 0.0010037, -0.00000088, -0.000000007);
	}

	// MVenus.Latitude(EPrecision)          » MVenus.Latitude.cs
	// MVenus.Latitude(EPrecision, double)  » MVenus.Latitude.cs
	// MVenus.Longitude(EPrecision)         » MVenus.Longitude.cs
	// MVenus.Longitude(EPrecision, double) » MVenus.Longitude.cs

	// MVenus.LongitudeOfAscendingNode()
	/// <summary>
	/// Liefert die Länge des aufsteigenden Knotens der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Länge des aufsteigenden Knotens der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double LongitudeOfAscendingNode()
	{
		// Lokale Felder einrichten und Länge berechnen
		double jd = DateTime.Now.ToJdn();
		return MVenus.LongitudeOfAscendingNode(jd);
	}

	// MVenus.LongitudeOfAscendingNode(double)
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
		return MMath.Polynome(t, 76.679920, 0.9011206, 0.00040618, -0.000000093);
	}

	// MVenus.LongitudeOfPerihelion()
	/// <summary>
	/// Liefert die Länge des Perihels der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Länge des Perihels der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double LongitudeOfPerihelion()
	{
		// Lokale Felder einrichten und Länge berechnen
		double jd = DateTime.Now.ToJdn();
		return MVenus.LongitudeOfPerihelion(jd);
	}

	// MVenus.LongitudeOfPerihelion(double)
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
		return MMath.Polynome(t, 131.563703, 1.4022288, -0.00107618, -0.000005678);
	}

	// MVenus.MeanAnomaly()
	/// <summary>
	/// Liefert die mittlere Anomalie der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Mittlere Anomalie der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double MeanAnomaly()
	{
		// Lokale Felder einrichten und Anomalie berechnen
		double jd = DateTime.Now.ToJdn();
		return MVenus.MeanAnomaly(jd);
	}

	// MVenus.MeanAnomaly(double)
	/// <summary>
	/// Liefert die mittlere Anomalie der mittleren Planetenbahn zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Mittlere Anomlie der mittleren Planetenbahn zur julianischen Tageszahl.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double MeanAnomaly(double jd){ return MMod.Mod(MVenus.MeanLongitude(jd) + MVenus.LongitudeOfPerihelion(jd), 360.0); }

	// MVenus.MeanLongitude()
	/// <summary>
	/// Liefert die mittlere Länge der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Mittlere Länge der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double MeanLongitude()
	{
		// Lokale Felder einrichten und Länge berechnen
		double jd = DateTime.Now.ToJdn();
		return MVenus.MeanLongitude(jd);
	}

	// MVenus.MeanLongitude(double)
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
		return MMod.Mod(MMath.Polynome(t, 181.979801, 58519.2130302, 0.00031014, 0.000000015), 360.0);
	}

	// MVenus.Perihelion()
	/// <summary>
	/// Liefert die julianische Tageszahl der nächsten Durchgangs durch das Perihel nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl der nächsten Durchgangs durch das Perihel nach der aktuellen Systemzeit.</returns>
	public static double Perihelion()
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double jd = DateTime.Now.ToJdn();
		return MVenus.Perihelion(jd);
	}

	// MVenus.Perihelion(double)
	/// <summary>
	/// Liefert die julianische Tageszahl der nächsten Durchgangs durch das Perihel nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl der nächsten Durchgangs durch das Perihel nach der julianischen Tageszahl.</returns>
	public static double Perihelion(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor(1.62549 * (y - 2000.53)) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while(j <= jd)
		{
			// Tageszahl berechnen
			k += 1.0;
			j  = MMath.Polynome(k, 2451738.233, 224.7008188, -0.0000000327);
		}
		return j;
	}

	// MVenus.PositionEcliptical(EPrecisionList)
	/// <summary>
	/// Liefert die heliozentrisch-ekliptikale Position zur aktuellen Systemzeit.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <returns>Heliozentrisch-ekliptikale Position zur aktuellen Systemzeit.</returns>
	public static CPolar PositionEcliptical(EPrecision value)
	{
		// Lokale Felder einrichten und Position berechnen
		double jd = DateTime.Now.ToJdn();
		return MVenus.PositionEcliptical(value, jd);
	}

	// MVenus.PositionEcliptical(EPrecisionList, double)
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
		rtn.Latitude  = MVenus.Latitude (value, jd);
		rtn.Longitude = MVenus.Longitude(value, jd);
		rtn.Radius    = MVenus.Radius   (value, jd);
		return rtn;
	}

	// MVenus.PositionEquatorial()
	/// <summary>
	/// Liefert die (scheinbare) geozentrisch-äquatoriale Position zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Geozentrisch-äquatoriale Position zur aktuellen Systemzeit.</returns>
	public static CPolar PositionEquatorial()
	{
		// Lokale Felder einrichten und Position berechnen
		double jd = DateTime.Now.ToJdn();
		return MVenus.PositionEquatorial(jd);
	}

	// MVenus.PositionEquatorial(double)
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
			bH = MVenus.Latitude (EPrecision.High, jdn);
			lH = MVenus.Longitude(EPrecision.High, jdn);
			rH = MVenus.Radius   (EPrecision.High, jdn);

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

	// MVenus.Radius(EPrecision)         » MVenus.Radius.cs
	// MVenus.Radius(EPrecision, double) » MVenus.Radius.cs

	// MVenus.RegressionEnd()
	/// <summary>
	/// Liefert die julianische Tageszahl des Rückläufigkeitsendes nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des Rückläufigkeitsendes nach der aktuellen Systemzeit.</returns>
	public static double RegressionEnd()
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double jd = DateTime.Now.ToJdn();
		return MVenus.RegressionEnd(jd);
	}

	// MVenus.RegressionEnd(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des Rückläufigkeitsendes nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des Rückläufigkeitsendes nach der julianischen Tageszahl.</returns>
	public static double RegressionEnd(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor((365.2425 * y + 1721060.0 - 2451996.706) / 583.921361) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while(j <= jd)
		{
			// Näherung berechnen
			k += 1.0;
			j  = 2451996.706 + k * 583.921361;

			// Hilfsfelder berechnen
			double m = MMod.Mod(MMath.ToRad(82.7311 + k * 215.513058), MMath.Pi2);
			double t = (j - MCalendar.Jdn20000101) / 36525.0;
			double h;

			// Korrektur berechnen und anwenden
			h  = MMath.Polynome(t, 21.0623,  0.0000, -0.00001);
			h += MMath.Polynome(t,  1.9913, -0.0040, -0.00001) * MMath.Sin(      m);
			h += MMath.Polynome(t, -0.0407, -0.0077          ) * MMath.Cos(      m);
			h += MMath.Polynome(t,  0.1351, -0.0009, -0.00004) * MMath.Sin(2.0 * m);
			h += MMath.Polynome(t,  0.0303,  0.0019          ) * MMath.Cos(2.0 * m);
			h += MMath.Polynome(t,  0.0089, -0.0002          ) * MMath.Sin(3.0 * m);
			h += MMath.Polynome(t,  0.0043,  0.0001          ) * MMath.Cos(3.0 * m);
			j += h;
		}
		return j;
	}

	// MVenus.RegressionStart()
	/// <summary>
	/// Liefert die julianische Tageszahl der Rückläufigkeitsanfangs nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl der Rückläufigkeitsanfangs nach der aktuellen Systemzeit.</returns>
	public static double RegressionStart()
	{
		// Lokale Felder einrichten und Ereigniszeit bestimmen
		double jd = DateTime.Now.ToJdn();
		return MVenus.RegressionStart(jd);
	}

	// MVenus.RegressionStart(double)
	/// <summary>
	/// Liefert die julianische Tageszahl der Rückläufigkeitsanfangs nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl der Rückläufigkeitsanfangs nach der julianischen Tageszahl.</returns>
	public static double RegressionStart(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor((365.2425 * y + 1721060.0 - 2451996.706) / 583.921361) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while(j <= jd)
		{
			// Näherung berechnen
			k += 1.0;
			j  = 2451996.706 + k * 583.921361;

			// Hilfsfelder berechnen
			double m = MMod.Mod(MMath.ToRad(82.7311 + k * 215.513058), MMath.Pi2);
			double t = (j - MCalendar.Jdn20000101) / 36525.0;
			double h;

			// Korrektur berechnen und anwenden
			h  = MMath.Polynome(t,-21.0672,  0.0002, -0.00001);
			h += MMath.Polynome(t,  1.9396, -0.0029, -0.00001) * MMath.Sin(      m);
			h += MMath.Polynome(t,  1.0727, -0.0102          ) * MMath.Cos(      m);
			h += MMath.Polynome(t,  0.0404, -0.0023, -0.00001) * MMath.Sin(2.0 * m);
			h += MMath.Polynome(t,  0.1305, -0.0004, -0.00003) * MMath.Cos(2.0 * m);
			h += MMath.Polynome(t, -0.0007, -0.0002          ) * MMath.Sin(3.0 * m);
			h +=                    0.0098                     * MMath.Cos(3.0 * m);
			j += h;
		}
		return j;
	}

	// MVenus.Rise(CPolar, ref double)
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
		return MVenus.Rise(position.Longitude, position.Latitude, ref jdEvent, jd, ref azm);
	}

	// MVenus.Rise(CPolar, ref double, double)
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
		return MVenus.Rise(position.Longitude, position.Latitude, ref jdEvent, jd, ref azm);
	}

	// MVenus.Rise(CPolar, ref double, double, ref double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Aufgangs und die Morgenweite am geographischen Ort und zur aktuekllen Systemzeit und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Aufgangs.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="azimuth">Morgenweite.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Rise(CPolar position, ref double jdEvent, double jd, ref double azimuth){ return MVenus.Rise(position.Longitude, position.Latitude, ref jdEvent, jd, ref azimuth); }

	// MVenus.Rise(double, double, ref double)
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
		return MVenus.Rise(lambda, phi, ref jdEvent, jd, ref azm);
	}

	// MVenus.Rise(double, double, ref double, double)
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
		return MVenus.Rise(lambda, phi, ref jdEvent, jd, ref azm);
	}

	// MVenus.Rise(double, double, ref double, double, ref double)
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
		l = MVenus.Longitude(EPrecision.Low, jdn + 1.0);
		b = MVenus.Latitude (EPrecision.Low, jdn + 1.0);
		double aP = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jdn + 1.0);
		double dP = MEphemerides.ToDelta(l, b, EObliquity.Mean, jdn + 1.0);

		// Position für gegebenen Tag berechnen
		l = MVenus.Longitude(EPrecision.Low, jdn);
		b = MVenus.Latitude (EPrecision.Low, jdn);
		double a0 = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jdn);
		if(MMath.Abs(aP - a0) > 1.0) a0 += MMath.Sgn(aP - a0) * MMath.Pi2;
		double d0 = MEphemerides.ToDelta(l, b, EObliquity.Mean, jdn);

		// Position für vorhergehenden Tag berechnen
		l = MVenus.Longitude(EPrecision.Low, jdn - 1.0);
		b = MVenus.Latitude (EPrecision.Low, jdn - 1.0);
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

	// MVenus.SemimajorAxis()
	/// <summary>
	/// Liefert die große Halbachse der mittleren Planetenbahn.
	/// </summary>
	/// <returns>Große Halbachse der mittleren Planetenbahn.</returns>
	public static double SemimajorAxis(){ return 0.723329820; }

	// MVenus.Set(CPolar, ref double)
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
		return MVenus.Set(position.Longitude, position.Latitude, ref jdEvent, jd, ref azm);
	}

	// MVenus.Set(CPolar, ref double, double)
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
		return MVenus.Set(position.Longitude, position.Latitude, ref jdEvent, jd, ref azm);
	}

	// MVenus.Set(CPolar, ref double, double, ref double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Untergangs und die Abendweite am geographischen Ort und zur julianischen Tageszahl und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Untergangs.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="azimuth">Abendweite.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Set(CPolar position, ref double jdEvent, double jd, ref double azimuth){ return MVenus.Set(position.Longitude, position.Latitude, ref jdEvent, jd, ref azimuth); }

	// MVenus.Set(double, double, ref double)
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
		return MVenus.Set(lambda, phi, ref jdEvent, jd, ref azm);
	}

	// MVenus.Set(double, double, ref double, double)
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
		return MVenus.Set(lambda, phi, ref jdEvent, jd, ref azm);
	}

	// MVenus.Set(double, double, ref double, double, ref double)
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
		l = MVenus.Longitude(EPrecision.Low, jdn + 1.0);
		b = MVenus.Latitude (EPrecision.Low, jdn + 1.0);
		double aP = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jd + 1.0);
		double dP = MEphemerides.ToDelta(l, b, EObliquity.Mean, jd + 1.0);

		// Position für gegebenen Tag berechnen
		l = MVenus.Longitude(EPrecision.Low, jdn);
		b = MVenus.Latitude (EPrecision.Low, jdn);
		double a0 = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jdn);
		if(MMath.Abs(aP - a0) > 1.0) a0 += MMath.Sgn(aP - a0) * MMath.Pi2;
		double d0 = MEphemerides.ToDelta(l, b, EObliquity.Mean, jdn);

		// Position für vorhergehenden Tag berechnen
		l = MVenus.Longitude(EPrecision.Low, jdn - 1.0);
		b = MVenus.Latitude (EPrecision.Low, jdn - 1.0);
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

	// MVenus.SynodicPeriod()
	/// <summary>
	/// Liefert die mittlere synodische Periode zur mittleren Planetenbahn.
	/// </summary>
	/// <returns>Mittlere synodische Periode zur mittleren Planetenbahn.</returns>
	public static double SynodicPeriod(){ return 583.921354; }

	// MVenus.Transit(CPolar)
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
		return MVenus.Transit(position.Longitude, position.Latitude, jd, ref h);
	}

	// MVenus.Transit(CPolar, double)
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
		return MVenus.Transit(position.Longitude, position.Latitude, jd, ref h);
	}

	// MVenus.Transit(CPolar, double, ref double)
	/// <summary>
	/// Setzt die Höhe und liefert die julianische Tageszahl des Meridiandurchgangs am geographischen Ort und zur julianischen Tageszahl.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="height">Höhe.</param>
	/// <returns>Julianische Tageszahl des Meridiandurchgangs am geographischen Ort und zur julianischen Tageszahl.</returns>
	public static double Transit(CPolar position, double jd, ref double height){ return MVenus.Transit(position.Longitude, position.Latitude, jd, ref height); }

	// MVenus.Transit(double, double)
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
		return MVenus.Transit(lambda, phi, jd, ref h);
	}

	// MVenus.Transit(double, double, double)
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
		return MVenus.Transit(lambda, phi, jd, ref h);
	}

	// MVenus.Transit(double, double, double, ref double)
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
		l = MVenus.Longitude(EPrecision.Low, jdn + 1.0);
		b = MVenus.Latitude (EPrecision.Low, jdn + 1.0);
		double aP = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jdn + 1.0);
		double dP = MEphemerides.ToDelta(l, b, EObliquity.Mean, jdn + 1.0);

		// Position für gegebenen Tag berechnen
		l = MVenus.Longitude(EPrecision.Low, jdn);
		b = MVenus.Latitude (EPrecision.Low, jdn);
		double a0 = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jdn);
		if(MMath.Abs(aP - a0) > 1.0) a0 += MMath.Sgn(aP - a0) * MMath.Pi2;
		double d0 = MEphemerides.ToDelta(l, b, EObliquity.Mean, jdn);

		// Position für vorhergehenden Tag berechnen
		l = MVenus.Longitude(EPrecision.Low, jdn - 1.0);
		b = MVenus.Latitude (EPrecision.Low, jdn - 1.0);
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

		// Iteration anwenden, Höhe berechnen und Rückgabewert setzen
		d = MMath.Bessel(m, dM, d0, dP);
		height = MEphemerides.ToHeight(0.0, d, phi);
		return jd + m;
	}

	// MVenus.TropicalPeriod()
	/// <summary>
	/// Liefert die mittlere tropische Periode zur mittleren Planetenbahn.
	/// </summary>
	/// <returns>Mittlere tropische Periode zur mittleren Planetenbahn.</returns>
	public static double TropicalPeriod(){ return 224.695434; }
}
