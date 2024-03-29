﻿using Acamat.LCore;
using Acamat.LMath;
using Acamat.LMath.Geometry;
using System;
using System.Globalization;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt Berechnungen zum Mond.
/// </summary>
public static partial class MMoon
{
	// ------------------- //
	// Felder und Methoden //
	// ------------------- //
	// MMoon.Age(double)             » MMoon.Phase.cs
	// MMoon.Age(double, ref double) » MMoon.Phase.cs

	// MMoon.AnomalisticMonth()
	/// <summary>
	/// Liefert die Dauer des anomalistischen Monats.
	/// </summary>
	/// <returns> Dauer des anomalistischen Monats.</returns>
	public static double AnomalisticMonth(){ return 27.55455; }

	// MMoon.AppendEvent(List<CEvent>, double, double)
	/// <summary>
	/// Fügt die mondbezogenen Eregnisse zum Zeitraum an die Liste an.
	/// </summary>
	/// <param name="list">Liste.</param>
	/// <param name="jdMin">Beginn des Zeitraums.</param>
	/// <param name="jdMax">Ende des Zeitraums.</param>
	public static void AppendEvent(List<CEvent> list, double jdMin, double jdMax)
	{
		// Lokale Felder
		double       jdn = 0.0;
		double       d   = 0.0;
		int          r   = 0;
		EEclipseType t;

		// ------ //
		// Apogee //
		// ------ //

		// Berechnungsschleife
		jdn = jdMin;
		while(true)
		{
			// Nächstes Ereignis berechnen und Lage im Intervall bestimmen
			(jdn, d) = MMoon.Apogee(jdn);
			r        = jdn.CompareTo(jdMin, jdMax);

			// Ereignisse vor dem Intervall verarbeiten
			if(r == -1)
				continue;

			// Ereignisse im Intervall verarbeiten
			if(r == 0)
			{
				// Ereignis an Liste anfügen und zum nächsten Durchlauf springen
				list.Add(new(jdn, string.Format("Mond: Durchgang durch das Apogäum – Horizontparallaxe: {0}", d.ToString("N3", new CultureInfo("de-DE")))));
				continue;
			}

			// Ereignis liegt nach dem Intervall --> Abbruch
			break;
		}

		// ------------- //
		// AscendingNode //
		// ------------- //

		// Berechnungsschleife
		jdn = jdMin;
		while(true)
		{
			// Nächste Ereignis berechnen und Lage im Intervall bestimmen
			jdn = MMoon.AscendingNode(jdn);
			r   = jdn.CompareTo(jdMin, jdMax);

			// Ereignisse vor dem Intervall verarbeiten
			if(r == -1)
				continue;

			// Ereignisse im Intervall verarbeiten
			if(r == 0)
			{
				// Ereignis an Liste anfügen und zum nächsten Durchlauf springen
				list.Add(new(jdn, "Mond: Durchgang durch den aufsteigenden Knoten"));
				continue;
			}

			// Ereignis liegt nach dem Intervall --> Abbruch
			break;
		}

		// -------------- //
		// DescendingNode //
		// -------------- //

		// Berechnunsschleife
		jdn = jdMin;
		while(true)
		{
			// Nächsts Ereignis berechnen und Lage im Intervall bestimmen
			jdn = MMoon.DescendingNode(jdn);
			r   = jdn.CompareTo(jdMin, jdMax);

			// Ereignisse vor dem Intervall verarbeiten
			if(r == -1)
				continue;

			// Ereignisse im Intervall verarbeiten
			if(r == 0)
			{
				// Ereignis an Liste anfügen und zum nächsten Durchlauf springen
				list.Add(new(jdn, "Mond: Durchgang durch den absteigenden Knoten"));
				continue;
			}

			// Ereignis liegt nach dem Intervall --> Abbruch
			break;
		}

		// ------------ //
		// FirstQuarter //
		// ------------ //

		// Berechnungsschleife
		jdn = jdMin;
		while(true)
		{
			// Nächstes Ereignis berechnen und Lage im Intervall bestimmen
			jdn = MMoon.FirstQuarter(jdn);
			r   = jdn.CompareTo(jdMin, jdMax);

			// Ereignisse vor dem Intervall verarbeiten
			if(r == -1)
				continue;

			// Ereignisse im Intervall verarbeiten
			if(r == 0)
			{
				// Ereignis an Liste anfügen und zum nächsten Durchlauf springen
				list.Add(new(jdn, "Mond: Erstes Viertel"));
				continue;
			}

			// Ereignis liegt nach dem Intervall --> Abbruch
			break;
		}

		// -------- //
		// FullMoon //
		// -------- //

		// Berechnungsschleife
		jdn = jdMin;
		while(true)
		{
			// Nächstes Ereignis berechnen und Lage im Intervall bestimmen
			(jdn, t) = MMoon.FullMoon(jdn);
			r        = jdn.CompareTo(jdMin, jdMax);

			// Ereignisse vor dem Intervall verarbeiten
			if(r == -1)
				continue;
		
			// Ereignisse im Intervall verarbeiten
			if(r == 0)
			{
				// Ereignis an Liste anfügen und zum nächsten Durchlauf springen
				list.Add(new(jdn, string.Format("Mond: Vollmond – Finsternisabschätzung: {0}", t.ToString())));
				continue;
			}

			// Ereignis liegt nach dem Intervall --> Abbruch
			break;
		}

		// -------------------------- //
		// GreatesNorthernDeclination //
		// -------------------------- //

		// Berechnungsschleife
		jdn = jdMin;
		while(true)
		{
			// Nächstes Ereignis berechnen und Lage im Intervall bestimmen
			(jdn, d) = MMoon.GreatestNorthernDeclination(jdn);
			r        = jdn.CompareTo(jdMin, jdMax);

			// Ereignisse vor dem Intervall verarbeiten
			if(r == -1)
				continue;

			// Ereignisse im Intervall verarbeiten
			if(r == 0)
			{
				// Ereignis an Liste anfügen und zum nächsten Durchlauf springen
				list.Add(new(jdn, string.Format("Mond: Nördliche Mondwende – Deklination: {0}", d.ToString("N3", new CultureInfo("de-DE")))));
				continue;
			}

			// Ereignis liegt nach dem Intervall --> Abbruch
			break;
		}

		// --------------------------- //
		// GreatestSouthernDeclination //
		// --------------------------- //

		// Berechnungsschleife
		jdn = jdMin;
		while(true)
		{
			// Nächstes Ereignis berechnen und Lage im Intervall bestimmen
			(jdn, d) = MMoon.GreatestSouthernDeclination(jdn);
			r        = jdn.CompareTo(jdMin, jdMax);

			// Ereignisse vor dem Intervall verarbeiten
			if(r == -1)
				continue;
			
			// Ereignisse im Intervall verarbeiten
			if(r == 0)
			{
				// Ereignis an Liste anfügen und zum nächsten Durchlauf springen
				list.Add(new(jdn, string.Format("Mond: Südliche Mondwende – Deklination: {0}", d.ToString("N3", new CultureInfo("de-DE")))));
				continue;
			}

			// Ereignis liegt nach dem Intervall --> Abbruch
			break;
		}

		// ----------- //
		// LastQuarter //
		// ----------- //

		// Berechnungsschleife
		jdn = jdMin;
		while(true)
		{
			// Nächstes Ereignis berechnen und Lage im Intervall bestimmen
			jdn = MMoon.LastQuarter(jdn);
			r   = jdn.CompareTo(jdMin, jdMax);

			// Ereignisse vor dem Intervall verarbeiten
			if(r == -1)
				continue;

			// Ereignusse im Intervall verarbeiten
			if(r == 0)
			{
				// Ereignis an Liste anfügen und zum nächsten Durchlauf springen
				list.Add(new(jdn, "Mond: Letztes Viertel"));
				continue;
			}

			// Ereignis liegt nach dem Intervall --> Abbruch
			break;
		}

		// ------- //
		// NewMoon //
		// ------- //

		// Berechnungsschleife
		jdn = jdMin;
		while(true)
		{
			// Nächstes Ereignis berechnen und Lage im Intervall bestimmen
			(jdn, t) = MMoon.NewMoon(jdn);
			r        = jdn.CompareTo(jdMin, jdMax);

			// Ereignisse vor dem Intervall verarbeiten
			if(r == -1)
				continue;

			// Ereignisse im Intervall verarbeiten
			if(r == 0)
			{
				// Ereignis an Liste anfügen und zum nächsten Durchlauf springen
				list.Add(new(jdn, string.Format("Mond: Neumond – Finsternisabschätzung: {0}", t.ToString())));
				continue;
			}

			// Ereignis liegt nach dem Intervall --> Abbruch
			break;
		}

		// ------- //
		// Perigee //
		// ------- //

		// Berechnungsschleife
		jdn = jdMin;
		while(true)
		{
			// Nächstes Ereignis berechnen und Lage im Intervall bestimmen
			(jdn, d) = MMoon.Perigee(jdn);
			r        = jdn.CompareTo(jdMin, jdMax);

			// Ereignisse vor dem Intervall verarbeiten
			if(r == -1)
				continue;

			// Ereignisse im Intervall verarbeiten
			if(r == 0)
			{
				// Ereignis an Liste anfügen und zum nächsten Durchlauf springen
				list.Add(new(jdn, string.Format("Mond: Durchgang durch das Perigäum – Horizontparallaxe: {0}", d.ToString("N3", new CultureInfo("de-DE")))));
				continue;
			}

			// Ereignis liegt nach dem Intervall --> Abbruch
			break;
		}
	}

	// MMoon.Apogee()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch das Apogäum und die Horizontparalaxxe nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch das Apogäum und die Horizontparallaxe nach der aktuellen Systemzeit.</returns>
	public static (double jd, double parallax) Apogee(){ return MMoon.Apogee(DateTime.Now.ToJdn()); }

	// MMoon.Apogee(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch das Apogäum und die Horizontparallaxe nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch das Apogäum und die Horizontparallaxe nach der julianischen Tageszahl.</returns>
	public static (double jd, double parallax) Apogee(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor(13.2555 * (y - 1999.97)) - 0.5;
		double t = 0.0;
		double j = 0.0;
		double d = 0.0;
		double m = 0.0;
		double f = 0.0;
		double h = 0.0;

		// Berechnungsschleife
		while(j <= jd)
		{
			// Lunation inkrementieren und Näherung berechnen
			k += 1.0;
			t = k / 1325.55;
			j = MMath.Polynome(t, 2451534.6698 + 27.55454989 * k, 0.0, -0.0006691, -0.000001098, 0.0000000052);

			// Hilfsfelder einrichten
			d = (MMath.ToRad(MMath.Polynome(t, 171.9179 + 335.9106046 * k, 0.0, -0.0100383, -0.00001156, 0.000000055))).Mod(MMath.Pi2);
			m = (MMath.ToRad(MMath.Polynome(t, 347.3477 +  27.1577721 * k, 0.0, -0.0008130, -0.00000100             ))).Mod(MMath.Pi2);
			f = (MMath.ToRad(MMath.Polynome(t, 316.6109 + 364.5287911 * k, 0.0, -0.0125053, -0.00001480             ))).Mod(MMath.Pi2);

			// ---------------------- //
			// Ereigniszeit berechnen //
			// ---------------------- //

			// Korrektur berechnen und anwenden
			h  =  0.4392 * MMath.Sin( 2.0 * d                    );
			h +=  0.0684 * MMath.Sin( 4.0 * d                    );
			h +=  0.0456 * MMath.Sin(                 m          ) - 0.00011 * t;
			h +=  0.0426 * MMath.Sin( 2.0 * d -       m          ) - 0.00011 * t;
			h +=  0.0212 * MMath.Sin(                   + 2.0 * f);
			h += -0.0189 * MMath.Sin(       d                    );
			h +=  0.0144 * MMath.Sin( 6.0 * d                    );
			h +=  0.0113 * MMath.Sin( 4.0 * d -       m          );
			h +=  0.0047 * MMath.Sin( 2.0 * d           + 2.0 * f);
			h +=  0.0036 * MMath.Sin(       d +       m          );
			h +=  0.0035 * MMath.Sin( 8.0 * d                    );
			h +=  0.0034 * MMath.Sin( 6.0 * d -       m          );
			h += -0.0034 * MMath.Sin( 2.0 * d           - 2.0 * f);
			h +=  0.0022 * MMath.Sin( 2.0 * d - 2.0 * m          );
			h += -0.0017 * MMath.Sin( 3.0 * d                    );
			h +=  0.0013 * MMath.Sin( 4.0 * d           + 2.0 * f);
			h +=  0.0011 * MMath.Sin( 8.0 * d -       m          );
			h +=  0.0010 * MMath.Sin( 4.0 * d - 2.0 * m          );
			h +=  0.0009 * MMath.Sin(10.0 * d                    );
			h +=  0.0007 * MMath.Sin( 3.0 * d +       m          );
			h +=  0.0006 * MMath.Sin(           2.0 * m          );
			h +=  0.0005 * MMath.Sin( 2.0 * d +       m          );
			h +=  0.0005 * MMath.Sin( 2.0 * d + 2.0 * m          );
			h +=  0.0004 * MMath.Sin( 6.0 * d           + 2.0 * f);
			h +=  0.0004 * MMath.Sin( 6.0 * d - 2.0 * m          );
			h +=  0.0004 * MMath.Sin(10.0 * d -       m          );
			h += -0.0004 * MMath.Sin( 5.0 * d                    );
			h += -0.0004 * MMath.Sin( 4.0 * d           - 2.0 * f);
			h +=  0.0003 * MMath.Sin(                 m + 2.0 * f);
			h +=  0.0003 * MMath.Sin(12.0 * d                    );
			h +=  0.0003 * MMath.Sin( 2.0 * d -       m + 2.0 * f);
			h += -0.0003 * MMath.Sin(       d -       m          );
			j += h;
		}

		// --------------------------- //
		// Horizontparallaxe berechnen //
		// --------------------------- //

		// Korrektur berechnen
		h  = 3245.251;
		h +=   -9.147 * (2.0 * d                    ).Cos();
		h +=   -0.841 * (      d                    ).Cos();
		h +=    0.697 * (                    2.0 * f).Cos();
		h +=   -0.656 * (                m          ).Cos() + 0.0016 * t;
		h +=    0.355 * (4.0 * d                    ).Cos();
		h +=    0.159 * (2.0 * d -       m          ).Cos();
		h +=    0.127 * (      d +       m          ).Cos();
		h +=    0.065 * (4.0 * d -       m          ).Cos();
		h +=    0.052 * (6.0 * d                    ).Cos();
		h +=    0.043 * (2.0 * d +       m          ).Cos();
		h +=    0.031 * (2.0 * d           + 2.0 * f).Cos();
		h +=   -0.023 * (2.0 * d           - 2.0 * f).Cos();
		h +=    0.022 * (2.0 * d - 2.0 * m          ).Cos();
		h +=    0.019 * (2.0 * d + 2.0 * m          ).Cos();
		h +=   -0.016 * (        + 2.0 * m          ).Cos();
		h +=    0.014 * (6.0 * d -       m          ).Cos();
		h +=    0.010 * (8.0 * d                    ).Cos();

		// Rückgabe
		return(j, h / 3600.0);
	}

	// MMoon.AscendingNode()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch den aufsteigenden Knoten nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch den aufsteigenden Knoten nach der aktuellen Systemzeit.</returns>
	public static double AscendingNode(){ return MMoon.AscendingNode(DateTime.Now.ToJdn()); }

	// MMoon.AscendingNode(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch den aufsteigenden Knoten nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch den aufsteigenden Knoten nach der julianischen Tageszahl.</returns>
	public static double AscendingNode(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor(13.4233 * (y - 2000.05)) - 1.0;
		double j = 0.0;

		// Berechnungschleife
		while(j <= jd)
		{
			// Lunation inkrementieren und lokale Felder einrichten
					 k += 1.0;
			double t  = k / 1342.23;

			// Näherung berechnen und Hilfsfelder einrichten
			j = MMath.Polynome(t, 2451565.1619 + 27.212220817 * k, 0.0, 0.0002762, 0.000000021, -0.000000000088);
			double e =  MMath.Polynome(t, 1.0, -0.002516, -0.0000074);
			double d = (MMath.ToRad(MMath.Polynome(t, 183.6380 + 331.73735682 * k, 0.0, 0.0014852, 0.000002090, -0.000000010))).Mod(MMath.Pi2);
			double m = (MMath.ToRad(MMath.Polynome(t,  17.4006 +  26.82037250 * k, 0.0, 0.0001186, 0.000000060              ))).Mod(MMath.Pi2);
			double a = (MMath.ToRad(MMath.Polynome(t,  38.3776 + 355.52747313 * k, 0.0, 0.0123499, 0.000014627, -0.000000069))).Mod(MMath.Pi2);
			double o = (MMath.ToRad(MMath.Polynome(t, 123.9767 -   1.44098956 * k, 0.0, 0.0020608, 0.000002140, -0.000000016))).Mod(MMath.Pi2);
			double v = (MMath.ToRad(MMath.Polynome(t, 299.75, 132.85, -0.009173))).Mod(MMath.Pi2);
			double p = (MMath.ToRad(o + 272.75 - 2.3 * t)).Mod(MMath.Pi2);
			double h;

			// Korrektur berechnen und anwenden
			h  = -0.4721     * MMath.Sin(                          a);
			h += -0.1649     * MMath.Sin(2.0 * d                    );
			h += -0.0868     * MMath.Sin(2.0 * d           -       a);
			h +=  0.0084     * MMath.Sin(2.0 * d           +       a);
			h += -0.0083     * MMath.Sin(2.0 * d -       m          );
			h += -0.0039     * MMath.Sin(2.0 * d -       m -       a);
			h +=  0.0034     * MMath.Sin(                    2.0 * a);
			h += -0.0031     * MMath.Sin(2.0 * d           - 2.0 * a);
			h +=  0.0030 * e * MMath.Sin(2.0 * d +       m          );
			h +=  0.0028 * e * MMath.Sin(                m -       a);
			h +=  0.0026 * e * MMath.Sin(                m          );
			h +=  0.0025     * MMath.Sin(4.0 * d                    );
			h +=  0.0024     * MMath.Sin(      d                    );
			h +=  0.0022 * e * MMath.Sin(                m +       a);
			h +=  0.0017     * MMath.Sin(                          o);
			h +=  0.0014     * MMath.Sin(4.0 * d           -       a);
			h +=  0.0005 * e * MMath.Sin(2.0 * d +       m -       a);
			h +=  0.0004 * e * MMath.Sin(2.0 * d -       m +       a);
			h += -0.0003 * e * MMath.Sin(2.0 * d - 2.0 * m          );
			h +=  0.0003 * e * MMath.Sin(4.0 * d -       m          );
			h +=  0.0003     * MMath.Sin(                          v);
			h +=  0.0003     * MMath.Sin(                          p);
			j += h;
		}
		return j;
	}

	// MMoon.DescendingNode()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch den absteigenden Knoten nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch den absteigenden Knoten nach der aktuellen Systemzeit.</returns>
	public static double DescendingNode(){ return MMoon.DescendingNode(DateTime.Now.ToJdn());	}

	// MMoon.DescendingNode(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch den absteigenden Knoten nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch den absteigenden Knoten nach der julianischen Tageszahl.</returns>
	public static double DescendingNode(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor(13.4233 * (y - 2000.05)) - 1.5;
		double j = 0.0;

		// Berechnungschleife
		while(j <= jd)
		{
			// Lunation inkrementieren und lokale Felder einrichten
						k += 1.0;
			double t = k / 1342.23;

			// Näherung berechnen und Hilfsfelder einrichten
			j = MMath.Polynome(t, 2451565.1619 + 27.212220817 * k, 0.0, 0.0002762, 0.000000021, -0.000000000088);
			double e = MMath.Polynome(t, 1.0, -0.002516, -0.0000074);
			double d = (MMath.ToRad(MMath.Polynome(t, 183.6380 + 331.73735682 * k, 0.0, 0.0014852, 0.000002090, -0.000000010))).Mod(MMath.Pi2);
			double m = (MMath.ToRad(MMath.Polynome(t,  17.4006 +  26.82037250 * k, 0.0, 0.0001186, 0.000000060              ))).Mod(MMath.Pi2);
			double a = (MMath.ToRad(MMath.Polynome(t,  38.3776 + 355.52747313 * k, 0.0, 0.0123499, 0.000014627, -0.000000069))).Mod(MMath.Pi2);
			double o = (MMath.ToRad(MMath.Polynome(t, 123.9767 -   1.44098956 * k, 0.0, 0.0020608, 0.000002140, -0.000000016))).Mod(MMath.Pi2);
			double v = (MMath.ToRad(MMath.Polynome(t, 299.75, 132.85, -0.009173))).Mod(MMath.Pi2);
			double p = (MMath.ToRad(o + 272.75 - 2.3 * t)).Mod(MMath.Pi2);
			double h;

			// Korrektur berechnen und anwenden
			h  = -0.4721     * MMath.Sin(                          a);
			h += -0.1649     * MMath.Sin(2.0 * d                    );
			h += -0.0868     * MMath.Sin(2.0 * d           -       a);
			h +=  0.0084     * MMath.Sin(2.0 * d           +       a);
			h += -0.0083     * MMath.Sin(2.0 * d -       m          );
			h += -0.0039     * MMath.Sin(2.0 * d -       m -       a);
			h +=  0.0034     * MMath.Sin(                    2.0 * a);
			h += -0.0031     * MMath.Sin(2.0 * d           - 2.0 * a);
			h +=  0.0030 * e * MMath.Sin(2.0 * d +       m          );
			h +=  0.0028 * e * MMath.Sin(                m -       a);
			h +=  0.0026 * e * MMath.Sin(                m          );
			h +=  0.0025     * MMath.Sin(4.0 * d                    );
			h +=  0.0024     * MMath.Sin(      d                    );
			h +=  0.0022 * e * MMath.Sin(                m +       a);
			h +=  0.0017     * MMath.Sin(                          o);
			h +=  0.0014     * MMath.Sin(4.0 * d           -       a);
			h +=  0.0005 * e * MMath.Sin(2.0 * d +       m -       a);
			h +=  0.0004 * e * MMath.Sin(2.0 * d -       m +       a);
			h += -0.0003 * e * MMath.Sin(2.0 * d - 2.0 * m          );
			h +=  0.0003 * e * MMath.Sin(4.0 * d -       m          );
			h +=  0.0003     * MMath.Sin(                          v);
			h +=  0.0003     * MMath.Sin(                          p);
			j += h;
		}
		return j;
	}

	// MMoon.DraconicMonth()
	/// <summary>
	/// Liefert die Dauer des drakonischen Monats.
	/// </summary>
	/// <returns> Dauer des drakonischen Monats.</returns>
	public static double DraconicMonth(){ return 27.21222; }

	// MMoon.FirstQuarter()                     » MMoon.Phase.cs
	// MMoon.FirstQuarter(double)               » MMoon.Phase.cs
	// MMoon.FullMoon()                         » MMoon.Phase.cs
	// MMoon.FullMoon(double)                   » MMoon.Phase.cs

	// MMoon.GreatestNorthernDeclination()
	/// <summary>
	/// Liefert die julianische Tageszahl der nächsten nördlichen Mondwende nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl der nächsten nördlichen Mondwende nach der aktuellen Systemzeit.</returns>
	public static (double jd, double declination) GreatestNorthernDeclination(){ return MMoon.GreatestNorthernDeclination(DateTime.Now.ToJdn()); }

	// MMoon.GreatesNorthernDeclination(double)
	/// <summary>
	/// Liefert die julianische Tageszahl der nächsten nördlichen Mondwende und die Deklination nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl der nächsten nördlichen Mondwende und die Deklination nach der julianischen Tageszahl.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static (double jd, double declination) GreatestNorthernDeclination(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor(13.3686 * (y - 2000.03)) - 1.0;
		double t = 0.0;
		double j = 0.0;
		double e = 0.0;
		double d = 0.0;
		double m = 0.0;
		double a = 0.0;
		double f = 0.0;
		double h = 0.0;

		// Berechnungschleife
		while(j <= jd)
		{
			// Lunation inkrementieren und Näherung berechnen
			k += 1.0;
			t = k / 1336.86;
			j = MMath.Polynome(t, 2451562.5897 + 27.321582247 * k, 0.0, 0.000119804, -0.000000141);

			// Hilfsfelder einrichten
			e = MMath.Polynome(t, 1.0, -0.002516, -0.0000074);
			d = (MMath.ToRad(MMath.Polynome(t, 152.2029 + 333.0705546 * k, 0.0, -0.0004214,  0.00000011))).Mod(MMath.Pi2);
			m = (MMath.ToRad(MMath.Polynome(t,  14.8591 +  26.9281592 * k, 0.0, -0.0000355, -0.00000010))).Mod(MMath.Pi2);
			a = (MMath.ToRad(MMath.Polynome(t,   4.6881 + 356.9562794 * k, 0.0,  0.0103066,  0.00001251))).Mod(MMath.Pi2);
			f = (MMath.ToRad(MMath.Polynome(t, 325.8867 +   1.4467807 * k, 0.0, -0.0020690, -0.00000215))).Mod(MMath.Pi2);

			// ---------------------- //
			// Ereigniszeit berechnen //
			// ---------------------- //

			// Korrektur berechnen und anwenden
			h  =  0.8975     * (                                    f).Cos();
			h += -0.4726     * MMath.Sin(                          a          );
			h += -0.1030     * MMath.Sin(                              2.0 * f);
			h += -0.0976     * MMath.Sin(2.0 * d           -       a          );
			h += -0.0462     * (                          a -       f).Cos();
			h += -0.0461     * (                          a +       f).Cos();
			h += -0.0438     * MMath.Sin(2.0 * d                              );
			h +=  0.0162 * e * MMath.Sin(                m                    );
			h += -0.0157     * (                              3.0 * f).Cos();
			h +=  0.0145     * MMath.Sin(                          a + 2.0 * f);
			h +=  0.0136     * (2.0 * d                     -       f).Cos();
			h += -0.0095     * (2.0 * d           -       a -       f).Cos();
			h += -0.0091     * (2.0 * d           -       a +       f).Cos();
			h += -0.0089     * (2.0 * d                     +       f).Cos();
			h +=  0.0075     * MMath.Sin(                    2.0 * a          );
			h += -0.0068     * MMath.Sin(                          a - 2.0 * f);
			h +=  0.0061     * (                    2.0 * a -       f).Cos();
			h += -0.0047     * MMath.Sin(                          a + 3.0 * f);
			h += -0.0043 * e * MMath.Sin(2.0 * d -       m -       a          );
			h += -0.0040     * (                          a - 2.0 * f).Cos();
			h += -0.0037     * MMath.Sin(2.0 * d           - 2.0 * a          );
			h +=  0.0031     * MMath.Sin(                                    f);
			h +=  0.0030     * MMath.Sin(2.0 * d           +       a          );
			h += -0.0029     * (                          a + 2.0 * f).Cos();
			h += -0.0029 * e * MMath.Sin(2.0 * d -       m                    );
			h += -0.0027     * MMath.Sin(                          a +       f);
			h +=  0.0024 * e * MMath.Sin(                m -       a          );
			h += -0.0021     * MMath.Sin(                          a - 3.0 * f);
			h +=  0.0019     * MMath.Sin(                    2.0 * a +       f);
			h +=  0.0018     * (2.0 * d           - 2.0 * a -       f).Cos();
			h +=  0.0018     * MMath.Sin(                              3.0 * f);
			h +=  0.0017     * (                          a + 3.0 * f).Cos();
			h +=  0.0017     * (                    2.0 * a          ).Cos();
			h += -0.0014     * (2.0 * d           -       a          ).Cos();
			h +=  0.0013     * (2.0 * d           +       a +       f).Cos();
			h +=  0.0013     * (                          a          ).Cos();
			h +=  0.0012     * MMath.Sin(                    3.0 * a +       f);
			h +=  0.0011     * MMath.Sin(2.0 * d           -       a +       f);
			h += -0.0011     * (2.0 * d           - 2.0 * a          ).Cos();
			h +=  0.0010     * (      d                     +       f).Cos();
			h +=  0.0010 * e * MMath.Sin(                m +       a          );
			h += -0.0009     * MMath.Sin(2.0 * d                     - 2.0 * f);
			h +=  0.0007     * (                    2.0 * a +       f).Cos();
			h += -0.0007     * (                    3.0 * a +       f).Cos();
			j += h;
		}

		// --------------------- //
		// Deklination berechnen //
		// --------------------- //

		// Korrektur berechnen
		h  =  5.1093     * MMath.Sin(                                     f);
		h +=  0.2658     * (                               2.0 * f).Cos();
		h +=  0.1448     * MMath.Sin(2.0 * d                      -       f);
		h += -0.0322     * MMath.Sin(                               3.0 * f);
		h +=  0.0133     * (2.0 * d                      - 2.0 * f).Cos();
		h +=  0.0125     * (2.0 * d                               ).Cos();
		h += -0.0124     * MMath.Sin(                           a -       f);
		h += -0.0101     * MMath.Sin(                           a + 2.0 * f);
		h +=  0.0097     * (                                     f).Cos();
		h += -0.0087 * e * MMath.Sin(2.0 * d +        m -                 f);
		h +=  0.0074     * MMath.Sin(                           a + 3.0 * f);
		h +=  0.0067     * MMath.Sin(      d                      +       f);
		h +=  0.0063     * MMath.Sin(                           a - 2.0 * f);
		h +=  0.0060 * e * MMath.Sin(2.0 * d -        m           -       f);
		h += -0.0057     * MMath.Sin(2.0 * d            -       a -       f);
		h += -0.0056     * (                           a +       f).Cos();
		h +=  0.0052     * (                           a + 2.0 * f).Cos();
		h +=  0.0041     * (                     2.0 * a +       f).Cos();
		h += -0.0040     * (                           a - 3.0 * f).Cos();
		h +=  0.0038     * (                     2.0 * a -       f).Cos();
		h += -0.0034     * (                           a - 2.0 * f).Cos();
		h += -0.0029     * MMath.Sin(                     2.0 * a          );
		h +=  0.0029     * MMath.Sin(                     3.0 * a +       f);
		h += -0.0028 * e * (2.0 * d +        m           -       f).Cos();
		h += -0.0028     * (                           a -       f).Cos();
		h += -0.0023     * (                               3.0 * f).Cos();
		h += -0.0021     * MMath.Sin(2.0 * d                      +       f);
		h +=  0.0019     * (                           a + 3.0 * f).Cos();
		h +=  0.0018     * (      d                      +       f).Cos();
		h +=  0.0017     * MMath.Sin(                     2.0 * a -       f);
		h +=  0.0015     * (                     3.0 * a +       f).Cos();
		h +=  0.0014     * (2.0 * d            + 2.0 * a +       f).Cos();
		h += -0.0012     * MMath.Sin(2.0 * d            - 2.0 * a -       f);
		h += -0.0012     * (                     2.0 * a          ).Cos();
		h += -0.0010     * (                           a          ).Cos();
		h += -0.0010     * MMath.Sin(                               2.0 * f);
		h +=  0.0006     * MMath.Sin(                           a +       f);

		// Rückgabe
		return(j, 23.6961 - 0.013004 * t + h);
	}

	// MMoon.GreatestSouthernDeclination()
	/// <summary>
	/// Liefert die julianische Tageszahl der nächsten südlichen Mondwende nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl der nächsten südlichen Mondwende nach der aktuellen Systemzeit.</returns>
	public static (double jd, double declination) GreatestSouthernDeclination(){ return MMoon.GreatestSouthernDeclination(DateTime.Now.ToJdn()); }

	// MMoon.GreatestSouthernDeclination(double)
	/// <summary>
	/// Liefert die julianische Tageszahl der nächsten südlichen Mondwende und die Deklination nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl der nächsten südlichen Mondwende und die Deklination nach der julianischen Tageszahl.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static (double jd, double declination) GreatestSouthernDeclination(double jd)
	{
		// Lokale Felder einrichten
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor(13.3686 * (y- 2000.03)) - 1.0;
		double t = 0.0;
		double j = 0.0;
		double e = 0.0;
		double d = 0.0;
		double m = 0.0;
		double a = 0.0;
		double f = 0.0;
		double h = 0.0;

		// Berechnungschleife
		while(j <= jd)
		{
			// Lunation inkrementieren und Näherung berechnen
			k += 1.0;
			t = k / 1336.86;
			j = MMath.Polynome(t, 2451548.9289 + 27.321582247 * k, 0.0, 0.000119804, -0.000000141);

			// Hilfsfelder einrichten
			e = MMath.Polynome(t, 1.0, -0.002516, -0.0000074);
			d = (MMath.ToRad(MMath.Polynome(t, 345.6676 + 333.0705546 * k, 0.0, -0.0004214,  0.00000011))).Mod(MMath.Pi2);
			m = (MMath.ToRad(MMath.Polynome(t,   1.3851 +  26.9281592 * k, 0.0, -0.0000355, -0.00000010))).Mod(MMath.Pi2);
			a = (MMath.ToRad(MMath.Polynome(t, 186.2100 + 356.9562794 * k, 0.0,  0.0103066,  0.00001251))).Mod(MMath.Pi2);
			f = (MMath.ToRad(MMath.Polynome(t, 145.1633 +   1.4467807 * k, 0.0, -0.0020690, -0.00000215))).Mod(MMath.Pi2);

			// ---------------------- //
			// Ereigniszeit berechnen //
			// ---------------------- //

			// Korrektur berechnen und anwenden
			h  = -0.8975     * (                                    f).Cos();
			h += -0.4726     * MMath.Sin(                          a          );
			h += -0.1030     * MMath.Sin(                              2.0 * f);
			h += -0.0976     * MMath.Sin(2.0 * d           -       a          );
			h +=  0.0541     * (                          a -       f).Cos();
			h +=  0.0516     * (                          a +       f).Cos();
			h += -0.0438     * MMath.Sin(2.0 * d                              );
			h +=  0.0112 * e * MMath.Sin(                m                    );
			h += +0.0157     * (                              3.0 * f).Cos();
			h +=  0.0023     * MMath.Sin(                          a + 2.0 * f);
			h += -0.0136     * (2.0 * d                     -       f).Cos();
			h +=  0.0110     * (2.0 * d           -       a -       f).Cos();
			h +=  0.0091     * (2.0 * d           -       a +       f).Cos();
			h +=  0.0089     * (2.0 * d                     +       f).Cos();
			h +=  0.0075     * MMath.Sin(                    2.0 * a          );
			h += -0.0030     * MMath.Sin(                          a - 2.0 * f);
			h += -0.0061     * (                    2.0 * a -       f).Cos();
			h += -0.0047     * MMath.Sin(                          a + 3.0 * f);
			h += -0.0043 * e * MMath.Sin(2.0 * d -       m -       a          );
			h +=  0.0040     * (                          a - 2.0 * f).Cos();
			h += -0.0037     * MMath.Sin(2.0 * d           - 2.0 * a          );
			h += -0.0031     * MMath.Sin(                                    f);
			h +=  0.0030     * MMath.Sin(2.0 * d           +       a          );
			h +=  0.0029     * (                          a + 2.0 * f).Cos();
			h += -0.0029 * e * MMath.Sin(2.0 * d -       m                    );
			h += -0.0027     * MMath.Sin(                          a +       f);
			h +=  0.0024 * e * MMath.Sin(                m -       a          );
			h += -0.0021     * MMath.Sin(                          a - 3.0 * f);
			h += -0.0019     * MMath.Sin(                    2.0 * a +       f);
			h += -0.0006     * (2.0 * d           - 2.0 * a -       f).Cos();
			h += -0.0018     * MMath.Sin(                              3.0 * f);
			h += -0.0017     * (                          a + 3.0 * f).Cos();
			h +=  0.0017     * (                    2.0 * a          ).Cos();
			h +=  0.0014     * (2.0 * d           -       a          ).Cos();
			h += -0.0013     * (2.0 * d           +       a +       f).Cos();
			h += -0.0013     * (                          a          ).Cos();
			h +=  0.0012     * MMath.Sin(                    3.0 * a +       f);
			h +=  0.0011     * MMath.Sin(2.0 * d           -       a +       f);
			h +=  0.0011     * (2.0 * d           - 2.0 * a          ).Cos();
			h +=  0.0010     * (      d                     +       f).Cos();
			h +=  0.0010 * e * MMath.Sin(                m +       a          );
			h += -0.0009     * MMath.Sin(2.0 * d                     - 2.0 * f);
			h += -0.0007     * (                    2.0 * a +       f).Cos();
			h += -0.0007     * (                    3.0 * a +       f).Cos();
			j += h;
		}

		// --------------------- //
		// Deklination berechnen //
		// --------------------- //

		// Korrektur berechnen
		h  = -5.1093     * MMath.Sin(                                     f);
		h +=  0.2658     * (                               2.0 * f).Cos();
		h += -0.1448     * MMath.Sin(2.0 * d                      -       f);
		h +=  0.0322     * MMath.Sin(                               3.0 * f);
		h +=  0.0133     * (2.0 * d                      - 2.0 * f).Cos();
		h +=  0.0125     * (2.0 * d                               ).Cos();
		h += -0.0015     * MMath.Sin(                           a -       f);
		h +=  0.0101     * MMath.Sin(                           a + 2.0 * f);
		h += -0.0097     * (                                     f).Cos();
		h += +0.0087 * e * MMath.Sin(2.0 * d +        m -                 f);
		h +=  0.0074     * MMath.Sin(                           a + 3.0 * f);
		h +=  0.0067     * MMath.Sin(      d                      +       f);
		h += -0.0063     * MMath.Sin(                           a - 2.0 * f);
		h += -0.0060 * e * MMath.Sin(2.0 * d -        m           -       f);
		h +=  0.0057     * MMath.Sin(2.0 * d            -       a -       f);
		h += -0.0056     * (                           a +       f).Cos();
		h += -0.0052     * (                           a + 2.0 * f).Cos();
		h += -0.0041     * (                     2.0 * a +       f).Cos();
		h += -0.0040     * (                           a - 3.0 * f).Cos();
		h += -0.0038     * (                     2.0 * a -       f).Cos();
		h +=  0.0034     * (                           a - 2.0 * f).Cos();
		h += -0.0029     * MMath.Sin(                     2.0 * a          );
		h +=  0.0029     * MMath.Sin(                     3.0 * a +       f);
		h +=  0.0028 * e * (2.0 * d +        m           -       f).Cos();
		h += -0.0028     * (                           a -       f).Cos();
		h +=  0.0023     * (                               3.0 * f).Cos();
		h +=  0.0021     * MMath.Sin(2.0 * d                      +       f);
		h +=  0.0019     * (                           a + 3.0 * f).Cos();
		h +=  0.0018     * (      d                      +       f).Cos();
		h += -0.0017     * MMath.Sin(                     2.0 * a -       f);
		h +=  0.0015     * (                     3.0 * a +       f).Cos();
		h +=  0.0014     * (2.0 * d            + 2.0 * a +       f).Cos();
		h +=  0.0012     * MMath.Sin(2.0 * d            - 2.0 * a -       f);
		h += -0.0012     * (                     2.0 * a          ).Cos();
		h +=  0.0010     * (                           a          ).Cos();
		h += -0.0010     * MMath.Sin(                               2.0 * f);
		h +=  0.0037     * MMath.Sin(                           a +       f);

		// Rückgabe
		return(j, -23.6961 - 0.013004 * t - h);
	}

	// MMoon.Latitude(EPrecision)
	/// <summary>
	/// Liefert die geozentrisch-ekliptikale Breite zur aktuellen Systemzeit.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <returns>Geozentrisch-ekliptikale Breite zur aktuellen Systemzeit.</returns>
	public static double Latitude(EPrecision value){ return MMoon.Latitude(value, DateTime.Now.ToJdn()); }

	// MMoon.Latitude(EPrecision, double)
	/// <summary>
	/// Liefert die geozentrisch-ekliptikale Breite zur julianischen Tageszahl.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Geozentrisch-ekliptikale Breite zur julianischen Tageszahl.</returns>
	public static double Latitude(EPrecision value, double jd)
	{
		// Nach Genauigkeitskennung unterscheiden
		if(value == EPrecision.Low)    return MMoon.LatitudeLow(jd);
		if(value == EPrecision.Medium) return MMoon.LongitudeMedium(jd);
		throw new NotImplementedException("Die Methode ist für diese Genauigkeitskennung nicht implementiert.");
	}

	// MMoon.LastQuarter()       » MMoon.Phase.cs
	// MMoon.LastQuarter(double) » MMoon.Phase.cs

	// MMoon.Longitude(EPrecisionList)
	/// <summary>
	/// Liefert die geozentrisch-ekliptikale Breite zur aktuellen Systemzeit.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <returns>Geozentrisch-ekliptikale Breite zur aktuellen Systemzeit.</returns>
	public static double Longitude(EPrecision value){ return MMoon.Longitude(value, DateTime.Now.ToJdn()); }

	// MMoon.Longitude(EPrecision, double)
	/// <summary>
	/// Liefert die geozentrisch-ekliptikale Breite zur julianischen Tageszahl.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Geozentrisch-ekliptikale Breite zur julianischen Tageszahl.</returns>
	public static double Longitude(EPrecision value, double jd)
	{
		// Nach Genauigkeitskennung unterscheiden
		if(value == EPrecision.Low)    return MMoon.LongitudeLow(jd);
		if(value == EPrecision.Medium) return MMoon.LongitudeMedium(jd);
		throw new NotImplementedException("Die Methode ist für diese Genauigkeitskennung nicht implementiert.");
	}

	// MMoon.NewMoon()       » MMoon.Phase.cs
	// MMoon.NewMoon(double) » MMoon.Phase.cs

	// MMoon.Perigee()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgang durch das Perigäum und die Horizontparallaxe nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgang durch das Perigäum und die Horizontparallaxe nach der aktuellen Systemzeit.</returns>
	public static (double jd, double parallax) Perigee(){ return MMoon.Perigee(DateTime.Now.ToJdn()); }

	// MMoon.Perigee(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgang durch das Perigäum und die Horizontparallaxe nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgang durch das Perigäum nach der julianischen Tageszahl.</returns>
	public static (double jd, double parallax) Perigee(double jd)
	{
		// Deklaration der lokalen Felder
		double y = (double)MCalendar.GregorianYear(jd) + MCalendar.YearFragment(jd);
		double k = MMath.Floor(13.2555 * (y - 1999.97)) - 1.0;
		double t = 0.0;
		double j = 0.0;
		double d = 0.0;
		double m = 0.0;
		double f = 0.0;
		double h = 0.0;

		// Berechnungschleife
		while(j <= jd)
		{
			// Lunation inkrementieren und Näherung berechnen
			k += 1.0;
			t = k / 1325.55;
			j = MMath.Polynome(t, 2451534.6698 + 27.55454989 * k, 0.0, -0.0006691, -0.000001098, 0.0000000052);

			// Hilfsfelder einrichten
			d = (MMath.ToRad(MMath.Polynome(t, 171.9179 + 335.9106046 * k, 0.0, -0.0100383, -0.00001156, 0.000000055))).Mod(MMath.Pi2);
			m = (MMath.ToRad(MMath.Polynome(t, 347.3477 +  27.1577721 * k, 0.0, -0.0008130, -0.00000100             ))).Mod(MMath.Pi2);
			f = (MMath.ToRad(MMath.Polynome(t, 316.6109 + 364.5287911 * k, 0.0, -0.0125053, -0.00001480             ))).Mod(MMath.Pi2);

			// ---------------------- //
			// Ereigniszeit berechnen //
			// ---------------------- //

			// Korrektur berechnen und anwenden
			h  = -1.6769 * MMath.Sin( 2.0 * d                    );
			h +=  0.4589 * MMath.Sin( 4.0 * d                    );
			h += -0.1856 * MMath.Sin( 6.0 * d                    );
			h +=  0.0883 * MMath.Sin( 8.0 * d                    );
			h += -0.0773 * MMath.Sin( 2.0 * d -       m          ) + 0.00019 * t;
			h +=  0.0502 * MMath.Sin(                 m          ) - 0.00013 * t;
			h += -0.0460 * MMath.Sin(10.0 * d                    );
			h +=  0.0422 * MMath.Sin( 4.0 * d -       m          ) - 0.00011 * t;
			h += -0.0256 * MMath.Sin( 6.0 * d -       m          );
			h +=  0.0253 * MMath.Sin(12.0 * d                    );
			h +=  0.0237 * MMath.Sin(       d                    );
			h +=  0.0162 * MMath.Sin( 8.0 * d -       m          );
			h += -0.0145 * MMath.Sin(14.0 * d                    );
			h +=  0.0129 * MMath.Sin(                   + 2.0 * f);
			h += -0.0112 * MMath.Sin( 3.0 * d                    );
			h += -0.0104 * MMath.Sin(10.0 * d -       m          );
			h +=  0.0086 * MMath.Sin(16.0 * d                    );
			h +=  0.0069 * MMath.Sin(12.0 * d -       m          );
			h +=  0.0066 * MMath.Sin( 5.0 * d                    );
			h += -0.0053 * MMath.Sin( 2.0 * d           + 2.0 * f);
			h += -0.0052 * MMath.Sin(18.0 * d                    );
			h += -0.0046 * MMath.Sin(14.0 * d -       m          );
			h += -0.0041 * MMath.Sin( 7.0 * d                    );
			h +=  0.0040 * MMath.Sin( 2.0 * d +       m          );
			h +=  0.0032 * MMath.Sin(20.0 * d                    );
			h += -0.0032 * MMath.Sin(       d +       m          );
			h +=  0.0031 * MMath.Sin(16.0 * d -       m          );
			h += -0.0029 * MMath.Sin( 4.0 * d +       m          );
			h +=  0.0027 * MMath.Sin( 9.0 * d                    );
			h +=  0.0027 * MMath.Sin( 4.0 * d           + 2.0 * f);
			h += -0.0027 * MMath.Sin( 2.0 * d - 2.0 * m          );
			h +=  0.0024 * MMath.Sin( 4.0 * d - 2.0 * m          );
			h += -0.0021 * MMath.Sin( 6.0 * d - 2.0 * m          );
			h += -0.0021 * MMath.Sin(22.0 * d                    );
			h += -0.0021 * MMath.Sin(18.0 * d -       m          );
			h +=  0.0019 * MMath.Sin( 6.0 * d +       m          );
			h += -0.0018 * MMath.Sin(11.0 * d                    );
			h += -0.0014 * MMath.Sin( 8.0 * d +       m          );
			h += -0.0014 * MMath.Sin( 4.0 * d           - 2.0 * f);
			h += -0.0014 * MMath.Sin( 6.0 * d           + 2.0 * f);
			h +=  0.0014 * MMath.Sin( 3.0 * d +       m          );
			h += -0.0014 * MMath.Sin( 5.0 * d +       m          );
			h +=  0.0013 * MMath.Sin(13.0 * d                    );
			h +=  0.0013 * MMath.Sin(20.0 * d -       m          );
			h +=  0.0011 * MMath.Sin( 3.0 * d + 2.0 * m          );
			h += -0.0011 * MMath.Sin( 4.0 * d - 2.0 * m + 2.0 * f);
			h += -0.0010 * MMath.Sin(       d + 2.0 * m          );
			h += -0.0009 * MMath.Sin(22.0 * d -       m          );
			h += -0.0008 * MMath.Sin(                   + 4.0 * f);
			h +=  0.0008 * MMath.Sin( 6.0 * d           - 2.0 * f);
			h +=  0.0008 * MMath.Sin( 2.0 * d +       m - 2.0 * f);
			h +=  0.0007 * MMath.Sin(         + 2.0 * m          );
			h +=  0.0007 * MMath.Sin(         -       m + 2.0 * f);
			h +=  0.0007 * MMath.Sin( 2.0 * d           + 4.0 * f);
			h += -0.0006 * MMath.Sin(         - 2.0 * m + 2.0 * f);
			h += -0.0006 * MMath.Sin( 2.0 * d + 2.0 * m - 2.0 * f);
			h +=  0.0006 * MMath.Sin(24.0 * d                    );
			h +=  0.0005 * MMath.Sin( 4.0 * d           - 4.0 * f);
			h +=  0.0005 * MMath.Sin( 2.0 * d + 2.0 * m          );
			h += -0.0004 * MMath.Sin(       d -       m          );
			j += h;
		}

		// --------------------------- //
		// Horizontparallaxe berechnen //
		// --------------------------- //

		// Korrektur berechnen
		h  = 3629.215;
		h +=   63.224 * ( 2.0 * d                    ).Cos();
		h +=   -6.990 * ( 4.0 * d                    ).Cos();
		h +=    2.834 * ( 2.0 * d -       m          ).Cos() - 0.0071 * t;
		h +=    1.927 * ( 6.0 * d                    ).Cos();
		h +=   -1.263 * (       d                    ).Cos();
		h +=   -0.702 * ( 8.0 * d                    ).Cos();
		h +=    0.696 * (                 m          ).Cos() - 0.0017 * t;
		h +=   -0.690 * (                   + 2.0 * f).Cos();
		h +=   -0.629 * ( 4.0 * d -       m          ).Cos() + 0.0016 * t;
		h +=   -0.392 * ( 2.0 * d           - 2.0 * f).Cos();
		h +=    0.297 * (10.0 * d                    ).Cos();
		h +=    0.260 * ( 6.0 * d -       m          ).Cos();
		h +=    0.201 * ( 3.0 * d                    ).Cos();
		h +=   -0.161 * ( 2.0 * d +       m          ).Cos();
		h +=    0.157 * (       d +       m          ).Cos();
		h +=   -0.138 * (12.0 * d                    ).Cos();
		h +=   -0.127 * ( 8.0 * d -       m          ).Cos();
		h +=    0.104 * ( 2.0 * d           + 2.0 * f).Cos();
		h +=    0.104 * ( 2.0 * d - 2.0 * m          ).Cos();
		h +=   -0.079 * ( 5.0 * d                    ).Cos();
		h +=    0.068 * (14.0 * d                    ).Cos();
		h +=    0.067 * (10.0 * d -       m          ).Cos();
		h +=    0.054 * ( 4.0 * d +       m          ).Cos();
		h +=   -0.038 * (12.0 * d -       m          ).Cos();
		h +=   -0.038 * ( 4.0 * d - 2.0 * m          ).Cos();
		h +=    0.037 * ( 7.0 * d                    ).Cos();
		h +=   -0.037 * ( 4.0 * d           + 2.0 * f).Cos();
		h +=   -0.035 * (16.0 * d                    ).Cos();
		h +=   -0.030 * ( 3.0 * d +       m          ).Cos();
		h +=    0.029 * (       d -       m          ).Cos();
		h +=   -0.025 * ( 6.0 * d +       m          ).Cos();
		h +=    0.023 * (         + 2.0 * m          ).Cos();
		h +=    0.023 * (14.0 * d -       m          ).Cos();
		h +=   -0.023 * ( 2.0 * d + 2.0 * m          ).Cos();
		h +=    0.022 * ( 6.0 * d - 2.0 * m          ).Cos();
		h +=   -0.021 * ( 2.0 * d -       m - 2.0 * f).Cos();
		h +=   -0.020 * ( 9.0 * d                    ).Cos();
		h +=    0.019 * (18.0 * d                    ).Cos();
		h +=    0.017 * ( 6.0 * d           + 2.0 * f).Cos();
		h +=    0.014 * (         -       m + 2.0 * f).Cos();
		h +=   -0.014 * (16.0 * d -       m          ).Cos();
		h +=    0.013 * ( 4.0 * d           - 2.0 * f).Cos();
		h +=    0.012 * ( 8.0 * d +       m          ).Cos();
		h +=    0.011 * (11.0 * d                    ).Cos();
		h +=    0.010 * ( 5.0 * d +       m          ).Cos();
		h +=   -0.010 * (20.0 * d                    ).Cos();

		// Rückgabe
		return(j, h / 3600.0);
	}

	// MMoon.Position(EPrecision)
	/// <summary>
	/// Liefert die geozentrisch-ekliptikale Position zur aktuellen Systemzeit.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <returns>Geozentrisch-ekliptikale Position zur aktuellen Systemzeit.</returns>
	public static CPolar Position(EPrecision value){ return MMoon.Position(value, DateTime.Now.ToJdn()); }

	// MMoon.Position(EPrecision, double)
	/// <summary>
	/// Liefert die geozentrisch-ekliptikale Position zur julianischen Tageszahl.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Geozentrisch-ekliptikale Position zur julianischen Tageszahl.</returns>
	public static CPolar Position(EPrecision value, double jd)
	{
		// Lokale Felder einrichten
		CPolar rtn = new CPolar();

		// Nach Genauigkeitskennung unterscheiden
		if(value == EPrecision.Low)
		{
			rtn.Latitude  = MMoon.LatitudeLow (jd);
			rtn.Longitude = MMoon.LongitudeLow(jd);
			rtn.Radius    = MMoon.RadiusLow   (jd);
			return rtn;
		}
		throw new NotImplementedException("Die Methode ist für diese Genauigkeitskennung nicht implementiert.");
	}

	// MMoon.Radius(EPrecision)
	/// <summary>
	/// Liefert die geozentrisch-ekliptikalen Radiusvektor zur aktuellen Systemzeit.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <returns>Geozentrisch-ekliptikalen Radiusvektor zur aktuellen Systemzeit.</returns>
	public static double Radius(EPrecision value){ return MMoon.Radius(value, DateTime.Now.ToJdn()); }

	// MMoon.Radius(EPrecision, double)
	/// <summary>
	/// Liefert die geozentrisch-ekliptikalen Radiusvektor zur julianischen Tageszahl.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Geozentrisch-ekliptikalen Radiusvektor zur julianischen Tageszahl.</returns>
	public static double Radius(EPrecision value, double jd)
	{
		// Nach Genauigkeitskennung unterscheiden
		if(value == EPrecision.Low) return MMoon.RadiusLow(jd);
		throw new NotImplementedException("Die Methode ist für diese Genauigkeitskennung nicht implementiert.");
	}

	// MMoon.SiderealMonth()
	/// <summary>
	/// Liefert die Dauer des siderischen Monats.
	/// </summary>
	/// <returns> Dauer des siderischen Monats.</returns>
	public static double SiderealMonth() { return 27.32166; }

	// MMoon.SynodicMonth()
	/// <summary>
	/// Liefert die Dauer des synodischen Monats zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Dauer des synodischen Monats zur aktuellen Systemzeit.</returns>
	public static double SynodicMonth(){ return MMoon.SynodicMonth(DateTime.Now.ToJdn()); }

	// MMoon.SynodicMonth(double)
	/// <summary>
	/// Liefert die Dauer des synodischen Monats zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julinische Tageszahl.</param>
	/// <returns>Dauer des synodischen Monats zur julianischen Tageszahl.</returns>
	public static double SynodicMonth(double jd)
	{
		// Lokale Felder einrichten und Länge berechnen
		double t = (jd - MCalendar.Jdn20000101) / 36525.0;
		return MMath.Polynome(t, 29.5305888531, +0.00000021621, -0.000000000364);
	}

	// MMoon.TropicalMonth()
	/// <summary>
	/// Liefert die Dauer des tropischen Monats.
	/// </summary>
	/// <returns> Dauer des tropischen Monats.</returns>
	public static double TropicalMonth(){ return 27.32158; }
}
