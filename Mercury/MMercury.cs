using Acamat.LCore;
using Acamat.LMath;
using Acamat.LMath.Geometry;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt Berechnungen zum Merkur.
/// </summary>
public static partial class MMercury
{
	// ------------------- //
	// Felder und Methoden //
	// ------------------- //
	// MMercury.AppendEvent(List<CEvent>, double, double)
	/// <summary>
	/// Fügt die merkurbezogenen Eregnisse zum Zeitraum an die Liste an.
	/// </summary>
	/// <param name="list">Liste.</param>
	/// <param name="jdMin">Beginn des Zeitraums.</param>
	/// <param name="jdMax">Ende des Zeitraums.</param>
	public static void AppendEvent(List<CEvent> list, double jdMin, double jdMax)
	{
		// Lokale Felder
		double jdn = 0.0;
		double d   = 0.0;
		int    r   = 0;

		// -------- //
		// Aphelion //
		// -------- //

		// Berechnungsschleife
		jdn = jdMin;
		while(true)
		{
			// Nächstes Ereignis berechnen und Lage im Intervall bestimmen
			jdn = MMercury.Aphelion(jdn);
			r   = jdn.CompareTo(jdMin, jdMax);

			// Ereignisse vor dem Intervall bearbeiten
			if(r == -1)
				continue;

			// Ereignisse im Intervall bearbeiten
			if(r == 0)
			{
				// Ereignis an Liste anfügen und zum nächsten Durchlauf springen
				list.Add(new(jdn, "Merkur: Durchgang durch das Aphel"));
				continue;
			}

			// Ereignis liegt nach dem Intervall --> Abbruch
			break;
		}

		// ------------------- //
		// ConjunctionInferior //
		// ------------------- //

		// Berechnungsschleife
		jdn = jdMin;
		while(true)
		{
			// Nächstes Ereignis berechnen und Lage im Intervall bestimmen
			jdn = MMercury.ConjunctionInferior(jdn);
			r   = jdn.CompareTo(jdMin, jdMax);

			// Ereignisse vor dem Intervall bearbeiten
			if(r == -1)
				continue;

			// Ereignisse im Intervall bearbeiten
			if(r == 0)
			{
				// Ereignis an Liste anfügen und zum nächsten Durchlauf springen
				list.Add(new(jdn, "Merkur: Durchgang durch die untere Konjunktion mit der Sonne"));
				continue;
			}

			// Ereignis liegt nach dem Intervall --> Abbruch
			break;
		}

		// ------------------- //
		// ConjunctionSuperior //
		// ------------------- //

		// Berechnungsschleife
		jdn = jdMin;
		while(true)
		{
			// Nächstes Ereignis berechnen und Lage im Intervall bestimmen
			jdn = MMercury.ConjunctionSuperior(jdn);
			r   = jdn.CompareTo(jdMin, jdMax);

			// Ereignisse vor dem Intervall bearbeiten
			if(r == -1)
				continue;

			// Ereignisse im Intervall bearbeiten
			if(r == 0)
			{
				// Ereignis an Liste anfügen und zum nächsten Durchlauf springen
				list.Add(new(jdn, "Merkur: Durchgang durch die obere Konjunktion mit der Sonne"));
				return;
			}

			// Ereignis liegt nach dem Intervall --> Abbruch
			break;
		}
	}

	// MMercury.Aphelion()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch das Aphel nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch das Aphel nach der aktuellen Systemzeit.</returns>
	public static double Aphelion(){ return MMercury.Aphelion(DateTime.Now.ToJdn()); }

	// MMercury.Aphelion(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch das Aphel nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch das Aphel nach der julianischen Tageszahl.</returns>
	public static double Aphelion(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor(4.15201 * (y - 2000.12)) - 0.5;
		double j = 0.0;

		// Berechnungsschleife
		while(j <= jd)
		{
			// Tageszahl berechnen
			k += 1.0;
			j  = MMath.Polynome(k, 2451590.257, 87.96934963);
		}
		return j;
	}

	// MMercury.ConjunctionInferior()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die untere Konjunktion mit der Sonne nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die untere Konjunktion mit der Sonne nach der aktuellen Systemzeit.</returns>
	public static double ConjunctionInferior(){ return MMercury.ConjunctionInferior(DateTime.Now.ToJdn()); }

	// MMercury.ConjunctionInferior(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die untere Konjunktion mit der Sonne nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die untere Konjunktion mit der Sonne nach der julianischen Tageszahl.</returns>
	public static double ConjunctionInferior(double jd)
	{
		// Deklaration der lokalen Felder
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor((365.2425 * y + 1721060.0 - 2451612.023) / 115.8774771) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while(j <= jd)
		{
			// Näherung berechnen
			k += 1.0;
			j  = 2451612.023 + k * 115.8774771;

			// Hilfsfelder berechnen
			double m = MMath.ToRad(63.5867 + k * 114.2088742).Mod(MMath.Pi2);
			double t = (j - MCalendar.Jdn20000101) / 36525.0;
			double h;

			// Korrektur berechnen und anwenden
			h  = MMath.Polynome(t,  0.0545,  0.0002          );
			h += MMath.Polynome(t, -6.2008,  0.0074,  0.00003) * MMath.Sin(      m);
			h += MMath.Polynome(t, -3.2750, -0.0197,  0.00001) * MMath.Cos(      m);
			h += MMath.Polynome(t,  0.4737, -0.0052, -0.00001) * MMath.Sin(2.0 * m);
			h += MMath.Polynome(t,  0.8111,  0.0033, -0.00002) * MMath.Cos(2.0 * m);
			h += MMath.Polynome(t,  0.0037,  0.0018          ) * MMath.Sin(3.0 * m);
			h += MMath.Polynome(t, -0.1768,  0.0000,  0.00001) * MMath.Cos(3.0 * m);
			h += MMath.Polynome(t, -0.0211, -0.0004          ) * MMath.Sin(4.0 * m);
			h += MMath.Polynome(t,  0.0326, -0.0003          ) * MMath.Cos(4.0 * m);
			h += MMath.Polynome(t,  0.0083,  0.0001          ) * MMath.Sin(5.0 * m);
			h += MMath.Polynome(t, -0.0040,  0.0001          ) * MMath.Cos(5.0 * m);
			j += h;
		}
		return j;
	}

	// MMercury.ConjunctionSuperior()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die obere Konjunktion mit der Sonne nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die obere Konjunktion mit der Sonne nach der aktuellen Systemzeit.</returns>
	public static double ConjunctionSuperior(){ return MMercury.ConjunctionSuperior(DateTime.Now.ToJdn()); }

	// MMercury.ConjunctionSuperior(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die obere Konjunktion mit der Sonne nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die obere Konjunktion mit der Sonne nach der julianischen Tageszahl.</returns>
	public static double ConjunctionSuperior(double jd)
	{
		// Deklaration der lokalen Felder
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor((365.2425 * y + 1721060.0 - 2451554.084) / 115.8774771) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while(j <= jd)
		{
			// Näherung berechnen
			k += 1.0;
			j  = 2451554.084 + k * 115.8774771;

			// Hilfsfelder berechnen
			double m = MMath.ToRad(6.4822 + k * 114.2088742).Mod(MMath.Pi2);
			double t = (j - MCalendar.Jdn20000101) / 36525.0;
			double h;

			// Korrektur berechnen und anwenden
			h  = MMath.Polynome(t, -0.0548, -0.0002          );
			h += MMath.Polynome(t,  7.3894, -0.0100, -0.00003) * MMath.Sin(      m);
			h += MMath.Polynome(t,  3.2200,  0.0197, -0.00001) * MMath.Cos(      m);
			h += MMath.Polynome(t,  0.8383, -0.0064, -0.00001) * MMath.Sin(2.0 * m);
			h += MMath.Polynome(t,  0.9666,  0.0039, -0.00003) * MMath.Cos(2.0 * m);
			h += MMath.Polynome(t,  0.0770, -0.0026          ) * MMath.Sin(3.0 * m);
			h += MMath.Polynome(t,  0.2758,  0.0002, -0.00002) * MMath.Cos(3.0 * m);
			h += MMath.Polynome(t, -0.0128, -0.0008          ) * MMath.Sin(4.0 * m);
			h += MMath.Polynome(t,  0.0734, -0.0004, -0.00001) * MMath.Cos(4.0 * m);
			h += MMath.Polynome(t, -0.0122, -0.0002          ) * MMath.Sin(5.0 * m);
			h += MMath.Polynome(t,  0.0173, -0.0002          ) * MMath.Cos(5.0 * m);
			j += h;
		}
		return j;
	}

	// MMercury.Eccentricity()
	/// <summary>
	/// Liefert die Exzentrizität der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Exzentrizität der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	public static double Eccentricity(){ return MMercury.Eccentricity(DateTime.Now.ToJdn()); }

	// MMercury.Eccentricity(double)
	/// <summary>
	/// Liefert die Exzentrizität der mittleren Planetenbahn zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Exzentrizität der mittleren Planetenbahn zur julianischen Tageszahl.</returns>
	public static double Eccentricity(double jd)
	{
		// Lokale Felder einrichten und Exzentrizität berechnen
		double t = (jd - MCalendar.Jdn20000101) / 36525.0;
		return MMath.Polynome(t, 0.20563175, 0.000020407, -0.0000000283, -0.00000000018);
	}

	// MMercury.GreatesEasternElongation()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die Ostelongation und den Elongationswinkel nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die Ostelongation nach der aktuellen Systemzeit.</returns>
	public static (double jd, double elongation) GreatestEasternElongation(){ return MMercury.GreatestEasternElongation(DateTime.Now.ToJdn()); }

	// MMercury.GreatestEasternElongation(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die Ostelongation und den Elongationswinkel nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die Ostelongation und den Elongationswinkel nach der julianischen Tageszahl.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static (double jd, double elongation) GreatestEasternElongation(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor((365.2425 * y + 1721060.0 - 2451612.023) / 115.8774771) - 1.0;
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
			j  = 2451612.023 + k * 115.8774771;
			
			// Hilfsfelder berechnen
			m = (MMath.ToRad(63.5867 + k * 114.2088742)).Mod(MMath.Pi2);
			t = (j - MCalendar.Jdn20000101) / 36525.0;

			// Korrektur berechnen und anwenden
			h  = MMath.Polynome(t, -21.6101,  0.0002          );
			h += MMath.Polynome(t,  -1.9803, -0.0060,  0.00001) * MMath.Sin(      m);
			h += MMath.Polynome(t,   1.4151, -0.0072, -0.00001) * MMath.Cos(      m);
			h += MMath.Polynome(t,   0.5528, -0.0005, -0.00001) * MMath.Sin(2.0 * m);
			h += MMath.Polynome(t,   0.2905,  0.0034,  0.00001) * MMath.Cos(2.0 * m);
			h += MMath.Polynome(t,  -0.1121, -0.0001,  0.00001) * MMath.Sin(3.0 * m);
			h += MMath.Polynome(t,  -0.0098, -0.0015          ) * MMath.Cos(3.0 * m);
			h += MMath.Polynome(t,   0.0192,  0.0000          ) * MMath.Sin(4.0 * m);
			h += MMath.Polynome(t,   0.0111,  0.0004          ) * MMath.Cos(4.0 * m);
			h += MMath.Polynome(t,  -0.0061,  0.0000          ) * MMath.Sin(5.0 * m);
			h += MMath.Polynome(t,  -0.0032, -0.0001          ) * MMath.Cos(5.0 * m);
			j += h;
		}

		// ----------------- //
		// Elongationswinkel //
		// ----------------- //

		// Elongationswinkel berechnen
		h  = 22.4697;
		h += MMath.Polynome(t, -4.2666,  0.0054,  0.00002) * MMath.Sin(      m);
		h += MMath.Polynome(t, -1.8537, -0.0137          ) * MMath.Cos(      m);
		h += MMath.Polynome(t,  0.3598,  0.0008, -0.00001) * MMath.Sin(2.0 * m);
		h += MMath.Polynome(t, -0.0680,  0.0026          ) * MMath.Cos(2.0 * m);
		h += MMath.Polynome(t, -0.0524, -0.0003          ) * MMath.Sin(3.0 * m);
		h += MMath.Polynome(t,  0.0052, -0.0006          ) * MMath.Cos(3.0 * m);
		h += MMath.Polynome(t,  0.0107,  0.0001          ) * MMath.Sin(4.0 * m);
		h += MMath.Polynome(t, -0.0013,  0.0001          ) * MMath.Cos(4.0 * m);
		h +=                   -0.0021                     * MMath.Sin(5.0 * m);
		h +=                    0.0003                     * MMath.Cos(5.0 * m);

		// Rückgabe
		return(j, h);
	}

	// MMercury.GreatesWesternElongation()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die Westelongation nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die Westelongation nach der aktuellen Systemzeit.</returns>
	public static (double jd, double elongation) GreatestWesternElongation(){ return MMercury.GreatestWesternElongation(DateTime.Now.ToJdn()); }

	// MMercury.GreatestWesternElongation(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch die Westelongation und den Elongationswinkel nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch die Westelongation und den Elongationswinkel nach der julianischen Tageszahl.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static (double jd, double elongation) GreatestWesternElongation(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor((365.2425 * y + 1721060.0 - 2451612.023) / 115.8774771) - 1.0;
		double j = 0.0;
		double t = 0.0;
		double m = 0.0;
		double h;

		// ------------------- //
		// Tageszahl berechnen //
		// ------------------- //
		
		// Berechnungsschleife
		while(j <= jd)
		{
			// Näherung berechnen
			k += 1.0;
			j  = 2451612.023 + k * 115.8774771;

			// Hilfsfelder berechnen
			m = (MMath.ToRad(63.5867 + k * 114.2088742)).Mod(MMath.Pi2);
			t = (j - MCalendar.Jdn20000101) / 36525.0;

			// Korrektur berechnen und anwenden
			h  = MMath.Polynome(t, 21.6249, -0.0002          );
			h += MMath.Polynome(t,  0.1306,  0.0065          ) * MMath.Sin(      m);
			h += MMath.Polynome(t, -2.7661, -0.0011,  0.00001) * MMath.Cos(      m);
			h += MMath.Polynome(t,  0.2438, -0.0024, -0.00001) * MMath.Sin(2.0 * m);
			h += MMath.Polynome(t,  0.5767,  0.0023          ) * MMath.Cos(2.0 * m);
			h +=                    0.1041                     * MMath.Sin(3.0 * m);
			h += MMath.Polynome(t, -0.0184,  0.0007          ) * MMath.Cos(3.0 * m);
			h += MMath.Polynome(t, -0.0051, -0.0001          ) * MMath.Sin(4.0 * m);
			h += MMath.Polynome(t,  0.0048,  0.0001          ) * MMath.Cos(4.0 * m);
			h +=                    0.0026                     * MMath.Sin(5.0 * m);
			h +=                    0.0037                     * MMath.Cos(5.0 * m);
			j += h;
		}

		// ----------------- //
		// Elongationswinkel //
		// ----------------- //

		// Elongationswinkel berechnen
		h  = MMath.Polynome(t, 22.4143, -0.0001          );
		h += MMath.Polynome(t,  4.3651, -0.0048, -0.00002) * MMath.Sin(      m);
		h += MMath.Polynome(t,  2.3787,  0.0121, -0.00001) * MMath.Cos(      m);
		h += MMath.Polynome(t,  0.2674,  0.0022          ) * MMath.Sin(2.0 * m);
		h += MMath.Polynome(t, -0.3873,  0.0008,  0.00001) * MMath.Cos(2.0 * m);
		h += MMath.Polynome(t, -0.0369, -0.0001          ) * MMath.Sin(3.0 * m);
		h += MMath.Polynome(t,  0.0017, -0.0001          ) * MMath.Cos(3.0 * m);
		h +=                    0.0059                     * MMath.Sin(4.0 * m);
		h += MMath.Polynome(t,  0.0061,  0.0001          ) * MMath.Cos(4.0 * m);
		h +=                    0.0007                     * MMath.Sin(5.0 * m);
		h +=                   -0.0011                     * MMath.Cos(5.0 * m);

		// Rückgabe
		return(j, h);
	}

	// MMercury.Inclination()
	/// <summary>
	/// Liefert die Neigung der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Neigung der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double Inclination(){ return MMercury.Inclination(DateTime.Now.ToJdn()); }

	// MMercury.Inclination(double)
	/// <summary>
	/// Liefert die Neigung der mittleren Planetenbahn zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Neigung der mittleren Planetenbahn zur julianischen Tageszahl.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double Inclination(double jd)
	{
		// Planetenbahnneigung berechnen
		double t = (jd - MCalendar.Jdn20000101) / 36525.0;
		return MMath.Polynome(t, 7.004986, 0.0018215, -0.00001810, 0.000000056);
	}

	// MMercury.Latitude(EPrecision)          » MMercury.Latitude.cs
	// MMercury.Latitude(EPrecision, double)  » MMercury.Latitude.cs
	// MMercury.Longitude(EPrecision)         » MMercury.Longitude.cs
	// MMercury.Longitude(EPrecision, double) » MMercury.Longitude.cs

	// MMercury.LongitudeOfAscendingNode()
	/// <summary>
	/// Liefert die Länge des aufsteigenden Knotens der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Länge des aufsteigenden Knotens der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double LongitudeOfAscendingNode(){ return MMercury.LongitudeOfAscendingNode(DateTime.Now.ToJdn()); }

	// MMercury.LongitudeOfAscendingNode(double)
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
		return MMath.Polynome(t, 48.330893, 1.1861883, 0.00017542, 0.000000215);
	}

	// MMercury.LongituedOfPerihelion()
	/// <summary>
	/// Liefert die Länge des Perihels der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Länge des Perihels der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double LongitudeOfPerihelion(){ return MMercury.LongitudeOfPerihelion(DateTime.Now.ToJdn()); }

	// MMercury.LongitudeOfPerihelion(double)
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
		return MMath.Polynome(t, 77.456119, 1.5564776, 0.00029544, 0.000000009);
	}

	// MMercury.MeanAnomaly()
	/// <summary>
	/// Liefert die mittlere Anomalie der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Mittlere Anomalie der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double MeanAnomaly(){ return MMercury.MeanAnomaly(DateTime.Now.ToJdn()); }

	// MMercury.MeanAnomaly(double)
	/// <summary>
	/// Liefert die mittlere Anomalie der mittleren Planetenbahn zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Mittlere Anomalie der mittleren Planetenbahn zur julianischen Tageszahl.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double MeanAnomaly(double jd)
	{
		// Rückgabe
		return (MMercury.MeanLongitude(jd) + MMercury.LongitudeOfPerihelion(jd)).Mod(360.0);
	}

	// MMercury.MeanLongitude()
	/// <summary>
	/// Liefert die mittlere Länge der mittleren Planetenbahn zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Mittlere Länge der mittleren Planetenbahn zur aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double MeanLongitude(){ return MMercury.MeanLongitude(DateTime.Now.ToJdn()); }

	// MMercury.MeanLongitude(double)
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
		return (MMath.Polynome(t, 252.250906, 149474.0722491, 0.0003035, 0.000000018)).Mod(360.0);
	}

	// MMercury.Perihelion()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch den Perihel nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch den Perihel nach der aktuellen Systemzeit.</returns>
	public static double Perihelion(){ return MMercury.Perihelion(DateTime.Now.ToJdn()); }

	// MMercury.Perihelion(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch den Perihel nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch den Perihel nach der julianischen Tageszahl.</returns>
	public static double Perihelion(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor(4.15201 * (y - 2000.12)) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while(j <= jd)
		{
			// Tageszahl berechnen
			k += 1.0;
			j  = MMath.Polynome(k, 2451590.257, 87.96934963);
		}
		return j;
	}

	// MMercury.PositionEcliptical(EPrecision)
	/// <summary>
	/// Liefert die heliozentrisch-ekliptikale Position zur aktuellen Systemzeit.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <returns>Heliozentrisch-ekliptikale Position zur aktuellen Systemzeit.</returns>
	public static CPolar PositionEcliptical(EPrecision value){ return MMercury.PositionEcliptical(value, DateTime.Now.ToJdn());
	}

	// MMercury.PositionEcliptical(EPrecision, double)
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
		rtn.Latitude  = MMercury.Latitude (value, jd);
		rtn.Longitude = MMercury.Longitude(value, jd);
		rtn.Radius    = MMercury.Radius   (value, jd);
		return rtn;
	}

	// MMercury.PositionEquatorial()
	/// <summary>
	/// Liefert die (scheinbare) geozentrisch-äquatoriale Position zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Geozentrisch-äquatoriale Position zur aktuellen Systemzeit.</returns>
	public static CPolar PositionEquatorial(){ return MMercury.PositionEquatorial(DateTime.Now.ToJdn()); }

	// MMercury.PositionEquatorial(double)
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
			bH = MMercury.Latitude (EPrecision.High, jdn);
			lH = MMercury.Longitude(EPrecision.High, jdn);
			rH = MMercury.Radius   (EPrecision.High, jdn);

			// Geozentrische Position berechnen und Abbruchbedinungen verarbeiten
			tmp = MEphemerides.ToGeocentric(lH, bH, rH, jdn, ref lG, ref bG, ref rG, EPrecision.High);
			if((tau - tmp).Abs() < 0.00005) break; // Ausreichende Genauigkeit sicherstellen
			if(tau != 0.0 && tmp >= tau)       break; // Abbruch bei Schwingung sicherstellen

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

	// MMercury.Radius(EPrecision)         » MMercury.Radius.cs
	// MMercury.Radius(EPrecision, double) » MMercury.Radius.cs

	// MMercury.RegressionEnd()
	/// <summary>
	/// Liefert die julianische Tageszahl des Rückläufigkeitsendes nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des Rückläufigkeitsendes nach der aktuellen Systemzeit.</returns>
	public static double RegressionEnd(){ return MMercury.RegressionEnd(DateTime.Now.ToJdn()); }

	// MMercury.RegressionEnd(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des Rückläufigkeitsendes nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des Rückläufigkeitsendes nach der julianischen Tageszahl.</returns>
	public static double RegressionEnd(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor((365.2425 * y + 1721060.0 - 2451612.023) / 115.8774771) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while(j <= jd)
		{
			// Näherung berechnen
			k += 1.0;
			j  = 2451612.023 + k * 115.8774771;

			// Hilfsfelder berechnen
			double m = (MMath.ToRad(63.5867 + k * 114.2088742)).Mod(MMath.Pi2);
			double t = (j - MCalendar.Jdn20000101) / 36525.0;
			double h;

			// Korrektur berechnen und anwenden
			h  = MMath.Polynome(t,  11.1343, -0.0001          );
			h += MMath.Polynome(t,  -3.9137,  0.0073,  0.00002) * MMath.Sin(      m);
			h += MMath.Polynome(t,  -3.3861, -0.0128,  0.00001) * MMath.Cos(      m);
			h += MMath.Polynome(t,   0.5222, -0.0040, -0.00002) * MMath.Sin(2.0 * m);
			h += MMath.Polynome(t,   0.5929,  0.0039, -0.00002) * MMath.Cos(2.0 * m);
			h += MMath.Polynome(t,  -0.0593,  0.0018          ) * MMath.Sin(3.0 * m);
			h += MMath.Polynome(t,  -0.1733, -0.0007,  0.00001) * MMath.Cos(3.0 * m);
			h += MMath.Polynome(t,  -0.0053, -0.0006          ) * MMath.Sin(4.0 * m);
			h += MMath.Polynome(t,   0.0476, -0.0001          ) * MMath.Cos(4.0 * m);
			h += MMath.Polynome(t,   0.0070,  0.0002          ) * MMath.Sin(5.0 * m);
			h += MMath.Polynome(t,  -0.0115,  0.0001          ) * MMath.Cos(5.0 * m);
			j += h;
		}
		return j;
	}

	// MMercury.RegressionStart()
	/// <summary>
	/// Liefert die julianische Tageszahl der Rückläufigkeitsanfangs nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl der Rückläufigkeitsanfangs nach der aktuellen Systemzeit.</returns>
	public static double RegressionStart(){ return MMercury.RegressionStart(DateTime.Now.ToJdn()); }

	// MMercury.RegressionStart(double)
	/// <summary>
	/// Liefert die julianische Tageszahl der Rückläufigkeitsanfangs nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl der Rückläufigkeitsanfangs nach der julianischen Tageszahl.</returns>
	public static double RegressionStart(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor((365.2425 * y + 1721060.0 - 2451612.023) / 115.8774771) - 1.0;
		double j = 0.0;

		// Berechnungsschleife
		while(j <= jd)
		{
			// Näherung berechnen
			k += 1.0;
			j  = 2451612.023 + k * 115.8774771;

			// Hilfsfelder berechnen
			double m = (MMath.ToRad(63.5867 + k * 114.2088742)).Mod(MMath.Pi2);
			double t = (j - MCalendar.Jdn20000101) / 36525.0;
			double h;

			// Korrektur berechnen und anwenden
			h  = MMath.Polynome(t, -11.0761,  0.0003          );
			h += MMath.Polynome(t,  -4.7321,  0.0023,  0.00002) * MMath.Sin(      m);
			h += MMath.Polynome(t,  -1.3230, -0.0156          ) * MMath.Cos(      m);
			h += MMath.Polynome(t,   0.2270, -0.0046          ) * MMath.Sin(2.0 * m);
			h += MMath.Polynome(t,   0.7184,  0.0013, -0.00002) * MMath.Cos(2.0 * m);
			h += MMath.Polynome(t,   0.0638,  0.0016          ) * MMath.Sin(3.0 * m);
			h += MMath.Polynome(t,  -0.1655,  0.0007          ) * MMath.Cos(3.0 * m);
			h += MMath.Polynome(t,  -0.0395, -0.0003          ) * MMath.Sin(4.0 * m);
			h += MMath.Polynome(t,   0.0247, -0.0006          ) * MMath.Cos(4.0 * m);
			h +=                     0.0131                     * MMath.Sin(5.0 * m);
			h += MMath.Polynome(t,   0.0008,  0.0002          ) * MMath.Cos(5.0 * m);
			j += h;
		}
		return j;
	}

	// MMercury.SemimajorAxis()
	/// <summary>
	/// Liefert die große Halbachse der mittleren Planetenbahn.
	/// </summary>
	/// <returns>Große Halbachse der mittleren Planetenbahn.</returns>
	public static double SemimajorAxis(){ return 0.387098310; }

	// MMercury.SiderealPeriod()
	/// <summary>
	/// Liefert die mittlere siderische Periode zur mittleren Planetenbahn.
	/// </summary>
	/// <returns>Mittlere siderische Periode zur mittleren Planetenbahn.</returns>
	public static double SiderealPeriod(){ return 87.968435; }

	// MMercury.SynodicPeriod()
	/// <summary>
	/// Liefert die mittlere synodische Periode zur mittleren Planetenbahn.
	/// </summary>
	/// <returns>Mittlere synodische Periode zur mittleren Planetenbahn.</returns>
	public static double SynodicPeriod(){ return 115.877480; }
}
