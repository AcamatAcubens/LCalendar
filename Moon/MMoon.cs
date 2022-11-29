using Acamat.LCore;
using Acamat.LMath;
using System;

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

	// MMoon.Apogee()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch das Apogäum nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch das Apogäum nach der aktuellen Systemzeit.</returns>
	public static double Apogee()
	{
		// Lokale Felder einrichten und Ereigniszeit berechnen
		double  p = 0.0;
		double jd = DateTime.Now.ToJdn();
		return MMoon.Apogee(jd, ref p);
	}

	// MMoon.Apogee(ref double)
	/// <summary>
	/// Setzt die Horizontparallaxe und liefert die julianische Tageszahl des nächsten Durchgangs durch das Apogäum nach der aktuellen Systemzeit.
	/// </summary>
	/// <param name="parallax">Horizontparallaxe.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch das Apogäum nach der aktuellen Systemzeit.</returns>
	public static double Apogee(ref double parallax)
	{
		// Lokale Felder einrichten und Ereigniszeit berechnen
		double jd = DateTime.Now.ToJdn();
		return MMoon.Apogee(jd, ref parallax);
	}

	// MMoon.Apogee(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch das Apogäum nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch das Apogäum nach der julianischen Tageszahl.</returns>
	public static double Apogee(double jd)
	{
		// Lokale Felder einrichten und Ereigniszeit berechnen
		double p = 0.0;
		return MMoon.Apogee(jd, ref p);
	}

	// MMoon.Apogee(double, ref double)
	/// <summary>
	/// Setzt die Horizontparallaxe und liefert die julianische Tageszahl des nächsten Durchgangs durch das Apogäum nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="parallax">Horizontparallaxe.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch das Apogäum nach der julianischen Tageszahl.</returns>
	public static double Apogee(double jd, ref double parallax)
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
			d = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 171.9179 + 335.9106046 * k, 0.0, -0.0100383, -0.00001156, 0.000000055)), MMath.Pi2);
			m = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 347.3477 +  27.1577721 * k, 0.0, -0.0008130, -0.00000100             )), MMath.Pi2);
			f = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 316.6109 + 364.5287911 * k, 0.0, -0.0125053, -0.00001480             )), MMath.Pi2);

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
		h +=   -9.147 * MMath.Cos(2.0 * d                    );
		h +=   -0.841 * MMath.Cos(      d                    );
		h +=    0.697 * MMath.Cos(                    2.0 * f);
		h +=   -0.656 * MMath.Cos(                m          )  + 0.0016 * t;
		h +=    0.355 * MMath.Cos(4.0 * d                    );
		h +=    0.159 * MMath.Cos(2.0 * d -       m          );
		h +=    0.127 * MMath.Cos(      d +       m          );
		h +=    0.065 * MMath.Cos(4.0 * d -       m          );
		h +=    0.052 * MMath.Cos(6.0 * d                    );
		h +=    0.043 * MMath.Cos(2.0 * d +       m          );
		h +=    0.031 * MMath.Cos(2.0 * d           + 2.0 * f);
		h +=   -0.023 * MMath.Cos(2.0 * d           - 2.0 * f);
		h +=    0.022 * MMath.Cos(2.0 * d - 2.0 * m          );
		h +=    0.019 * MMath.Cos(2.0 * d + 2.0 * m          );
		h +=   -0.016 * MMath.Cos(        + 2.0 * m          );
		h +=    0.014 * MMath.Cos(6.0 * d -       m          );
		h +=    0.010 * MMath.Cos(8.0 * d                    );

		// Parallaxe berechnen und Rückgabewert setzen
		parallax = h / 3600.0;
		return j;
	}

	// MMoon.AscendingNode()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgangs durch den aufsteigenden Knoten nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgangs durch den aufsteigenden Knoten nach der aktuellen Systemzeit.</returns>
	public static double AscendingNode()
	{
		// Lokale Felder einrichten und Ereigniszeit berechnen
		double jd = DateTime.Now.ToJdn();
		return MMoon.AscendingNode(jd);
	}

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
			double e = MMath.Polynome(t, 1.0, -0.002516, -0.0000074);
			double d = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 183.6380 + 331.73735682 * k, 0.0, 0.0014852, 0.000002090, -0.000000010)), MMath.Pi2);
			double m = MMod.Mod(MMath.ToRad(MMath.Polynome(t,  17.4006 +  26.82037250 * k, 0.0, 0.0001186, 0.000000060              )), MMath.Pi2);
			double a = MMod.Mod(MMath.ToRad(MMath.Polynome(t,  38.3776 + 355.52747313 * k, 0.0, 0.0123499, 0.000014627, -0.000000069)), MMath.Pi2);
			double o = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 123.9767 -   1.44098956 * k, 0.0, 0.0020608, 0.000002140, -0.000000016)), MMath.Pi2);
			double v = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 299.75, 132.85, -0.009173)), MMath.Pi2);
			double p = MMod.Mod(MMath.ToRad(o + 272.75 - 2.3 * t), MMath.Pi2);
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
	public static double DescendingNode()
	{
		// Lokale Felder einrichten und Ereigniszeit berechnen
		double jd = DateTime.Now.ToJdn();
		return MMoon.DescendingNode(jd);
	}

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
			double d = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 183.6380 + 331.73735682 * k, 0.0, 0.0014852, 0.000002090, -0.000000010)), MMath.Pi2);
			double m = MMod.Mod(MMath.ToRad(MMath.Polynome(t,  17.4006 +  26.82037250 * k, 0.0, 0.0001186, 0.000000060              )), MMath.Pi2);
			double a = MMod.Mod(MMath.ToRad(MMath.Polynome(t,  38.3776 + 355.52747313 * k, 0.0, 0.0123499, 0.000014627, -0.000000069)), MMath.Pi2);
			double o = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 123.9767 -   1.44098956 * k, 0.0, 0.0020608, 0.000002140, -0.000000016)), MMath.Pi2);
			double v = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 299.75, 132.85, -0.009173)), MMath.Pi2);
			double p = MMod.Mod(MMath.ToRad(o + 272.75 - 2.3 * t), MMath.Pi2);
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
	// MMoon.FullMoon(double, ref EEclipseType) » MMoon.Phase.cs

	// MMoon.GreatestNorthernDeclination()
	/// <summary>
	/// Liefert die julianische Tageszahl der nächsten nördlichen Mondwende nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl der nächsten nördlichen Mondwende nach der aktuellen Systemzeit.</returns>
	public static double GreatestNorthernDeclination()
	{
		// Lokale Felder einrichten und Ereigniszeit berechnen
		double  d = 0.0;
		double jd = DateTime.Now.ToJdn();
		return MMoon.GreatestNorthernDeclination(jd, ref d);
	}

	// MMoon.GreatesNorthernDeclination(ref double)
	/// <summary>
	/// Setzt die Deklination und liefert die julianische Tageszahl der nächsten nördlichen Mondwende nach der aktuellen Systemzeit.
	/// </summary>
	/// <param name="declination">Deklination.</param>
	/// <returns>Julianische Tageszahl der nächsten nördlichen Mondwende nach der aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double GreatestNorthernDeclination(ref double declination)
	{
		// Lokale Felder einrichten und Ereigniszeit berechnen
		double jd = DateTime.Now.ToJdn();
		return MMoon.GreatestNorthernDeclination(jd, ref declination);
	}

	// MMoon.GreatestNorthernDeclination(double)
	/// <summary>
	/// Liefert die julianische Tageszahl der nächsten nördlichen Mondwende nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl der nächsten nördlichen Mondwende nach der julianischen Tageszahl.</returns>
	public static double GreatestNorthernDeclination(double jd)
	{
		// Lokale Felder einrichten und Ereigniszeit berechnen
		double d = 0.0;
		return MMoon.GreatestNorthernDeclination(jd, ref d);
	}

	// MMoon.GreatesNorthernDeclination(double, ref double)
	/// <summary>
	/// Setzt die Deklination und liefert die julianische Tageszahl der nächsten nördlichen Mondwende nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="declination">Deklination.</param>
	/// <returns>Julianische Tageszahl der nächsten nördlichen Mondwende nach der julianischen Tageszahl.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double GreatestNorthernDeclination(double jd, ref double declination)
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
			d = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 152.2029 + 333.0705546 * k, 0.0, -0.0004214,  0.00000011)), MMath.Pi2);
			m = MMod.Mod(MMath.ToRad(MMath.Polynome(t,  14.8591 +  26.9281592 * k, 0.0, -0.0000355, -0.00000010)), MMath.Pi2);
			a = MMod.Mod(MMath.ToRad(MMath.Polynome(t,   4.6881 + 356.9562794 * k, 0.0,  0.0103066,  0.00001251)), MMath.Pi2);
			f = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 325.8867 +   1.4467807 * k, 0.0, -0.0020690, -0.00000215)), MMath.Pi2);

			// ---------------------- //
			// Ereigniszeit berechnen //
			// ---------------------- //

			// Korrektur berechnen und anwenden
			h  =  0.8975     * MMath.Cos(                                    f);
			h += -0.4726     * MMath.Sin(                          a          );
			h += -0.1030     * MMath.Sin(                              2.0 * f);
			h += -0.0976     * MMath.Sin(2.0 * d           -       a          );
			h += -0.0462     * MMath.Cos(                          a -       f);
			h += -0.0461     * MMath.Cos(                          a +       f);
			h += -0.0438     * MMath.Sin(2.0 * d                              );
			h +=  0.0162 * e * MMath.Sin(                m                    );
			h += -0.0157     * MMath.Cos(                              3.0 * f);
			h +=  0.0145     * MMath.Sin(                          a + 2.0 * f);
			h +=  0.0136     * MMath.Cos(2.0 * d                     -       f);
			h += -0.0095     * MMath.Cos(2.0 * d           -       a -       f);
			h += -0.0091     * MMath.Cos(2.0 * d           -       a +       f);
			h += -0.0089     * MMath.Cos(2.0 * d                     +       f);
			h +=  0.0075     * MMath.Sin(                    2.0 * a          );
			h += -0.0068     * MMath.Sin(                          a - 2.0 * f);
			h +=  0.0061     * MMath.Cos(                    2.0 * a -       f);
			h += -0.0047     * MMath.Sin(                          a + 3.0 * f);
			h += -0.0043 * e * MMath.Sin(2.0 * d -       m -       a          );
			h += -0.0040     * MMath.Cos(                          a - 2.0 * f);
			h += -0.0037     * MMath.Sin(2.0 * d           - 2.0 * a          );
			h +=  0.0031     * MMath.Sin(                                    f);
			h +=  0.0030     * MMath.Sin(2.0 * d           +       a          );
			h += -0.0029     * MMath.Cos(                          a + 2.0 * f);
			h += -0.0029 * e * MMath.Sin(2.0 * d -       m                    );
			h += -0.0027     * MMath.Sin(                          a +       f);
			h +=  0.0024 * e * MMath.Sin(                m -       a          );
			h += -0.0021     * MMath.Sin(                          a - 3.0 * f);
			h +=  0.0019     * MMath.Sin(                    2.0 * a +       f);
			h +=  0.0018     * MMath.Cos(2.0 * d           - 2.0 * a -       f);
			h +=  0.0018     * MMath.Sin(                              3.0 * f);
			h +=  0.0017     * MMath.Cos(                          a + 3.0 * f);
			h +=  0.0017     * MMath.Cos(                    2.0 * a          );
			h += -0.0014     * MMath.Cos(2.0 * d           -       a          );
			h +=  0.0013     * MMath.Cos(2.0 * d           +       a +       f);
			h +=  0.0013     * MMath.Cos(                          a          );
			h +=  0.0012     * MMath.Sin(                    3.0 * a +       f);
			h +=  0.0011     * MMath.Sin(2.0 * d           -       a +       f);
			h += -0.0011     * MMath.Cos(2.0 * d           - 2.0 * a          );
			h +=  0.0010     * MMath.Cos(      d                     +       f);
			h +=  0.0010 * e * MMath.Sin(                m +       a          );
			h += -0.0009     * MMath.Sin(2.0 * d                     - 2.0 * f);
			h +=  0.0007     * MMath.Cos(                    2.0 * a +       f);
			h += -0.0007     * MMath.Cos(                    3.0 * a +       f);
			j += h;
		}

		// --------------------- //
		// Deklination berechnen //
		// --------------------- //

		// Korrektur berechnen
		h  =  5.1093     * MMath.Sin(                                     f);
		h +=  0.2658     * MMath.Cos(                               2.0 * f);
		h +=  0.1448     * MMath.Sin(2.0 * d                      -       f);
		h += -0.0322     * MMath.Sin(                               3.0 * f);
		h +=  0.0133     * MMath.Cos(2.0 * d                      - 2.0 * f);
		h +=  0.0125     * MMath.Cos(2.0 * d                               );
		h += -0.0124     * MMath.Sin(                           a -       f);
		h += -0.0101     * MMath.Sin(                           a + 2.0 * f);
		h +=  0.0097     * MMath.Cos(                                     f);
		h += -0.0087 * e * MMath.Sin(2.0 * d +        m -                 f);
		h +=  0.0074     * MMath.Sin(                           a + 3.0 * f);
		h +=  0.0067     * MMath.Sin(      d                      +       f);
		h +=  0.0063     * MMath.Sin(                           a - 2.0 * f);
		h +=  0.0060 * e * MMath.Sin(2.0 * d -        m           -       f);
		h += -0.0057     * MMath.Sin(2.0 * d            -       a -       f);
		h += -0.0056     * MMath.Cos(                           a +       f);
		h +=  0.0052     * MMath.Cos(                           a + 2.0 * f);
		h +=  0.0041     * MMath.Cos(                     2.0 * a +       f);
		h += -0.0040     * MMath.Cos(                           a - 3.0 * f);
		h +=  0.0038     * MMath.Cos(                     2.0 * a -       f);
		h += -0.0034     * MMath.Cos(                           a - 2.0 * f);
		h += -0.0029     * MMath.Sin(                     2.0 * a          );
		h +=  0.0029     * MMath.Sin(                     3.0 * a +       f);
		h += -0.0028 * e * MMath.Cos(2.0 * d +        m           -       f);
		h += -0.0028     * MMath.Cos(                           a -       f);
		h += -0.0023     * MMath.Cos(                               3.0 * f);
		h += -0.0021     * MMath.Sin(2.0 * d                      +       f);
		h +=  0.0019     * MMath.Cos(                           a + 3.0 * f);
		h +=  0.0018     * MMath.Cos(      d                      +       f);
		h +=  0.0017     * MMath.Sin(                     2.0 * a -       f);
		h +=  0.0015     * MMath.Cos(                     3.0 * a +       f);
		h +=  0.0014     * MMath.Cos(2.0 * d            + 2.0 * a +       f);
		h += -0.0012     * MMath.Sin(2.0 * d            - 2.0 * a -       f);
		h += -0.0012     * MMath.Cos(                     2.0 * a          );
		h += -0.0010     * MMath.Cos(                           a          );
		h += -0.0010     * MMath.Sin(                               2.0 * f);
		h +=  0.0006     * MMath.Sin(                           a +       f);

		// Deklination berechnen und Rückgabewert setzen
		declination = 23.6961 - 0.013004 * t + h;
		return j;
	}

	// MMoon.GreatestSouthernDeclination()
	/// <summary>
	/// Liefert die julianische Tageszahl der nächsten südlichen Mondwende nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl der nächsten südlichen Mondwende nach der aktuellen Systemzeit.</returns>
	public static double GreatestSouthernDeclination()
	{
		// Lokale Felder einrichten und Ereigniszeit berechnen
		double  d = 0.0;
		double jd = DateTime.Now.ToJdn();
		return MMoon.GreatestSouthernDeclination(jd, ref d);
	}

	// MMoon.GreatestSouthernDeclination(ref double)
	/// <summary>
	/// Setzt die Deklination und liefert die julianische Tageszahl der nächsten südlichen Mondwende nach der aktuellen Systemzeit.
	/// </summary>
	/// <param name="declination">Deklination.</param>
	/// <returns>Julianische Tageszahl der nächsten südlichen Mondwende nach der aktuellen Systemzeit.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double GreatestSouthernDeclination(ref double declination)
	{
		// Lokale Felder einrichten und Ereigniszeit berechnen
		double jd = DateTime.Now.ToJdn();
		return MMoon.GreatestSouthernDeclination(jd, ref declination);
	}
	// MMoon.GreatestSouthernDeclination(double)
	/// <summary>
	/// Liefert die julianische Tageszahl der nächsten südlichen Mondwende nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl der nächsten südlichen Mondwende nach der julianischen Tageszahl.</returns>
	public static double GreatestSouthernDeclination(double jd)
	{
		// Lokale Felder einrichten und Ereigniszeit berechnen
		double d = 0.0;
		return MMoon.GreatestSouthernDeclination(jd, ref d);
	}

	// MMoon.GreatestSouthernDeclination(double, ref double)
	/// <summary>
	/// Setzt die Deklination und liefert die julianische Tageszahl der nächsten südlichen Mondwende nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="declination">Deklination.</param>
	/// <returns>Julianische Tageszahl der nächsten südlichen Mondwende nach der julianischen Tageszahl.</returns>
	/// <remarks>Die Winkelangabe erfolgt in Gradmaß.</remarks>
	public static double GreatestSouthernDeclination(double jd, ref double declination)
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
			d = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 345.6676 + 333.0705546 * k, 0.0, -0.0004214,  0.00000011)), MMath.Pi2);
			m = MMod.Mod(MMath.ToRad(MMath.Polynome(t,   1.3851 +  26.9281592 * k, 0.0, -0.0000355, -0.00000010)), MMath.Pi2);
			a = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 186.2100 + 356.9562794 * k, 0.0,  0.0103066,  0.00001251)), MMath.Pi2);
			f = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 145.1633 +   1.4467807 * k, 0.0, -0.0020690, -0.00000215)), MMath.Pi2);

			// ---------------------- //
			// Ereigniszeit berechnen //
			// ---------------------- //

			// Korrektur berechnen und anwenden
			h  = -0.8975     * MMath.Cos(                                    f);
			h += -0.4726     * MMath.Sin(                          a          );
			h += -0.1030     * MMath.Sin(                              2.0 * f);
			h += -0.0976     * MMath.Sin(2.0 * d           -       a          );
			h +=  0.0541     * MMath.Cos(                          a -       f);
			h +=  0.0516     * MMath.Cos(                          a +       f);
			h += -0.0438     * MMath.Sin(2.0 * d                              );
			h +=  0.0112 * e * MMath.Sin(                m                    );
			h += +0.0157     * MMath.Cos(                              3.0 * f);
			h +=  0.0023     * MMath.Sin(                          a + 2.0 * f);
			h += -0.0136     * MMath.Cos(2.0 * d                     -       f);
			h +=  0.0110     * MMath.Cos(2.0 * d           -       a -       f);
			h +=  0.0091     * MMath.Cos(2.0 * d           -       a +       f);
			h +=  0.0089     * MMath.Cos(2.0 * d                     +       f);
			h +=  0.0075     * MMath.Sin(                    2.0 * a          );
			h += -0.0030     * MMath.Sin(                          a - 2.0 * f);
			h += -0.0061     * MMath.Cos(                    2.0 * a -       f);
			h += -0.0047     * MMath.Sin(                          a + 3.0 * f);
			h += -0.0043 * e * MMath.Sin(2.0 * d -       m -       a          );
			h +=  0.0040     * MMath.Cos(                          a - 2.0 * f);
			h += -0.0037     * MMath.Sin(2.0 * d           - 2.0 * a          );
			h += -0.0031     * MMath.Sin(                                    f);
			h +=  0.0030     * MMath.Sin(2.0 * d           +       a          );
			h +=  0.0029     * MMath.Cos(                          a + 2.0 * f);
			h += -0.0029 * e * MMath.Sin(2.0 * d -       m                    );
			h += -0.0027     * MMath.Sin(                          a +       f);
			h +=  0.0024 * e * MMath.Sin(                m -       a          );
			h += -0.0021     * MMath.Sin(                          a - 3.0 * f);
			h += -0.0019     * MMath.Sin(                    2.0 * a +       f);
			h += -0.0006     * MMath.Cos(2.0 * d           - 2.0 * a -       f);
			h += -0.0018     * MMath.Sin(                              3.0 * f);
			h += -0.0017     * MMath.Cos(                          a + 3.0 * f);
			h +=  0.0017     * MMath.Cos(                    2.0 * a          );
			h +=  0.0014     * MMath.Cos(2.0 * d           -       a          );
			h += -0.0013     * MMath.Cos(2.0 * d           +       a +       f);
			h += -0.0013     * MMath.Cos(                          a          );
			h +=  0.0012     * MMath.Sin(                    3.0 * a +       f);
			h +=  0.0011     * MMath.Sin(2.0 * d           -       a +       f);
			h +=  0.0011     * MMath.Cos(2.0 * d           - 2.0 * a          );
			h +=  0.0010     * MMath.Cos(      d                     +       f);
			h +=  0.0010 * e * MMath.Sin(                m +       a          );
			h += -0.0009     * MMath.Sin(2.0 * d                     - 2.0 * f);
			h += -0.0007     * MMath.Cos(                    2.0 * a +       f);
			h += -0.0007     * MMath.Cos(                    3.0 * a +       f);
			j += h;
		}

		// --------------------- //
		// Deklination berechnen //
		// --------------------- //

		// Korrektur berechnen
		h  = -5.1093     * MMath.Sin(                                     f);
		h +=  0.2658     * MMath.Cos(                               2.0 * f);
		h += -0.1448     * MMath.Sin(2.0 * d                      -       f);
		h +=  0.0322     * MMath.Sin(                               3.0 * f);
		h +=  0.0133     * MMath.Cos(2.0 * d                      - 2.0 * f);
		h +=  0.0125     * MMath.Cos(2.0 * d                               );
		h += -0.0015     * MMath.Sin(                           a -       f);
		h +=  0.0101     * MMath.Sin(                           a + 2.0 * f);
		h += -0.0097     * MMath.Cos(                                     f);
		h += +0.0087 * e * MMath.Sin(2.0 * d +        m -                 f);
		h +=  0.0074     * MMath.Sin(                           a + 3.0 * f);
		h +=  0.0067     * MMath.Sin(      d                      +       f);
		h += -0.0063     * MMath.Sin(                           a - 2.0 * f);
		h += -0.0060 * e * MMath.Sin(2.0 * d -        m           -       f);
		h +=  0.0057     * MMath.Sin(2.0 * d            -       a -       f);
		h += -0.0056     * MMath.Cos(                           a +       f);
		h += -0.0052     * MMath.Cos(                           a + 2.0 * f);
		h += -0.0041     * MMath.Cos(                     2.0 * a +       f);
		h += -0.0040     * MMath.Cos(                           a - 3.0 * f);
		h += -0.0038     * MMath.Cos(                     2.0 * a -       f);
		h +=  0.0034     * MMath.Cos(                           a - 2.0 * f);
		h += -0.0029     * MMath.Sin(                     2.0 * a          );
		h +=  0.0029     * MMath.Sin(                     3.0 * a +       f);
		h +=  0.0028 * e * MMath.Cos(2.0 * d +        m           -       f);
		h += -0.0028     * MMath.Cos(                           a -       f);
		h +=  0.0023     * MMath.Cos(                               3.0 * f);
		h +=  0.0021     * MMath.Sin(2.0 * d                      +       f);
		h +=  0.0019     * MMath.Cos(                           a + 3.0 * f);
		h +=  0.0018     * MMath.Cos(      d                      +       f);
		h += -0.0017     * MMath.Sin(                     2.0 * a -       f);
		h +=  0.0015     * MMath.Cos(                     3.0 * a +       f);
		h +=  0.0014     * MMath.Cos(2.0 * d            + 2.0 * a +       f);
		h +=  0.0012     * MMath.Sin(2.0 * d            - 2.0 * a -       f);
		h += -0.0012     * MMath.Cos(                     2.0 * a          );
		h +=  0.0010     * MMath.Cos(                           a          );
		h += -0.0010     * MMath.Sin(                               2.0 * f);
		h +=  0.0037     * MMath.Sin(                           a +       f);

		// Deklination berechnen und Rückgabewert setzen
		declination = -23.6961 - 0.013004 * t - h;
		return j;
	}

	// MMoon.Latitude(EPrecision)
	/// <summary>
	/// Liefert die geozentrisch-ekliptikale Breite zur aktuellen Systemzeit.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <returns>Geozentrisch-ekliptikale Breite zur aktuellen Systemzeit.</returns>
	public static double Latitude(EPrecision value)
	{
		// Lokale Felder einrichten
		double jd = DateTime.Now.ToJdn();
		return MMoon.Latitude(value, jd);
	}

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
	public static double Longitude(EPrecision value)
	{
		// Lokale Felder einrichten
		double jd = DateTime.Now.ToJdn();
		return MMoon.Longitude(value, jd);
	}

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

	// MMoon.NewMoon()                     » MMoon.Phase.cs
	// MMoon.NewMoon(double)               » MMoon.Phase.cs
	// MMoon.NewMoon(double, EEclipseType) » MMoon.Phase.cs

	// MMoon.Perigee()
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgang durch das Perigäum nach der aktuellen Systemzeit.
	/// </summary>
	/// <returns>Julianische Tageszahl des nächsten Durchgang durch das Perigäum nach der aktuellen Systemzeit.</returns>
	public static double Perigee()
	{
		// Lokale Felder einrichten und Ereigniszeit berechnen
		double  p = 0.0;
		double jd = DateTime.Now.ToJdn();
		return MMoon.Perigee(jd, ref p);
	}

	// MMoon.Perigee(ref double)
	/// <summary>
	/// Setzt die Horizontparallaxe und liefert die julianische Tageszahl des nächsten Durchgang durch das Perigäum nach der aktuellen Systemzeit.
	/// </summary>
	/// <param name="parallax">Horizontparallaxe.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgang durch das Perigäum nach der aktuellen Systemzeit.</returns>
	public static double Perigee(ref double parallax)
	{
		// Lokale Felder einrichten und Ereigniszeit berechnen
		double jd = DateTime.Now.ToJdn();
		return MMoon.Perigee(jd, ref parallax);
	}

	// MMoon.Perigee(double)
	/// <summary>
	/// Liefert die julianische Tageszahl des nächsten Durchgang durch das Perigäum nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgang durch das Perigäum nach der julianischen Tageszahl.</returns>
	public static double Perigee(double jd)
	{
		// Lokale Felder einrichten und Ereigniszeit berechnen
		double p = 0.0;
		return MMoon.Perigee(jd, ref p);
	}

	// MMoon.Perigee(double, ref double)
	/// <summary>
	/// Setzt die Horizontparallaxe und liefert die julianische Tageszahl des nächsten Durchgang durch das Perigäum nach der julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="parallax">Horizontparallaxe.</param>
	/// <returns>Julianische Tageszahl des nächsten Durchgang durch das Perigäum nach der julianischen Tageszahl.</returns>
	public static double Perigee(double jd, ref double parallax)
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
			d = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 171.9179 + 335.9106046 * k, 0.0, -0.0100383, -0.00001156, 0.000000055)), MMath.Pi2);
			m = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 347.3477 +  27.1577721 * k, 0.0, -0.0008130, -0.00000100             )), MMath.Pi2);
			f = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 316.6109 + 364.5287911 * k, 0.0, -0.0125053, -0.00001480             )), MMath.Pi2);

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
		h +=   63.224 * MMath.Cos( 2.0 * d                    );
		h +=   -6.990 * MMath.Cos( 4.0 * d                    );
		h +=    2.834 * MMath.Cos( 2.0 * d -       m          ) - 0.0071 * t;
		h +=    1.927 * MMath.Cos( 6.0 * d                    );
		h +=   -1.263 * MMath.Cos(       d                    );
		h +=   -0.702 * MMath.Cos( 8.0 * d                    );
		h +=    0.696 * MMath.Cos(                 m          ) - 0.0017 * t;
		h +=   -0.690 * MMath.Cos(                   + 2.0 * f);
		h +=   -0.629 * MMath.Cos( 4.0 * d -       m          ) + 0.0016 * t;
		h +=   -0.392 * MMath.Cos( 2.0 * d           - 2.0 * f);
		h +=    0.297 * MMath.Cos(10.0 * d                    );
		h +=    0.260 * MMath.Cos( 6.0 * d -       m          );
		h +=    0.201 * MMath.Cos( 3.0 * d                    );
		h +=   -0.161 * MMath.Cos( 2.0 * d +       m          );
		h +=    0.157 * MMath.Cos(       d +       m          );
		h +=   -0.138 * MMath.Cos(12.0 * d                    );
		h +=   -0.127 * MMath.Cos( 8.0 * d -       m          );
		h +=    0.104 * MMath.Cos( 2.0 * d           + 2.0 * f);
		h +=    0.104 * MMath.Cos( 2.0 * d - 2.0 * m          );
		h +=   -0.079 * MMath.Cos( 5.0 * d                    );
		h +=    0.068 * MMath.Cos(14.0 * d                    );
		h +=    0.067 * MMath.Cos(10.0 * d -       m          );
		h +=    0.054 * MMath.Cos( 4.0 * d +       m          );
		h +=   -0.038 * MMath.Cos(12.0 * d -       m          );
		h +=   -0.038 * MMath.Cos( 4.0 * d - 2.0 * m          );
		h +=    0.037 * MMath.Cos( 7.0 * d                    );
		h +=   -0.037 * MMath.Cos( 4.0 * d           + 2.0 * f);
		h +=   -0.035 * MMath.Cos(16.0 * d                    );
		h +=   -0.030 * MMath.Cos( 3.0 * d +       m          );
		h +=    0.029 * MMath.Cos(       d -       m          );
		h +=   -0.025 * MMath.Cos( 6.0 * d +       m          );
		h +=    0.023 * MMath.Cos(         + 2.0 * m          );
		h +=    0.023 * MMath.Cos(14.0 * d -       m          );
		h +=   -0.023 * MMath.Cos( 2.0 * d + 2.0 * m          );
		h +=    0.022 * MMath.Cos( 6.0 * d - 2.0 * m          );
		h +=   -0.021 * MMath.Cos( 2.0 * d -       m - 2.0 * f);
		h +=   -0.020 * MMath.Cos( 9.0 * d                    );
		h +=    0.019 * MMath.Cos(18.0 * d                    );
		h +=    0.017 * MMath.Cos( 6.0 * d           + 2.0 * f);
		h +=    0.014 * MMath.Cos(         -       m + 2.0 * f);
		h +=   -0.014 * MMath.Cos(16.0 * d -       m          );
		h +=    0.013 * MMath.Cos( 4.0 * d           - 2.0 * f);
		h +=    0.012 * MMath.Cos( 8.0 * d +       m          );
		h +=    0.011 * MMath.Cos(11.0 * d                    );
		h +=    0.010 * MMath.Cos( 5.0 * d +       m          );
		h +=   -0.010 * MMath.Cos(20.0 * d                    );

		// Parallaxe berechnen und Rückgabewert setzen
		parallax = h / 3600.0;
		return j;
	}

	// MMoon.Position(EPrecision)
	/// <summary>
	/// Liefert die geozentrisch-ekliptikale Position zur aktuellen Systemzeit.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <returns>Geozentrisch-ekliptikale Position zur aktuellen Systemzeit.</returns>
	public static CPolar Position(EPrecision value)
	{
		// Lokale Felder einrichten
		double jd = DateTime.Now.ToJdn();
		return MMoon.Position(value, jd);
	}

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
	public static double Radius(EPrecision value)
	{
		// Lokale Felder einrichten
		double jd = DateTime.Now.ToJdn();
		return MMoon.Radius(value, jd);
	}

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

	// MMoon.Rise(CPolar, ref double, double)
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
		return MMoon.Rise(position.Longitude, position.Latitude, ref jdEvent, jd, ref azm);
	}

	// MMoon.Rise(CPolar, ref double, double, ref double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Aufgangs und die Morgenweite am geographischen Ort und zur aktuekllen Systemzeit und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Aufgangs.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="azimuth">Morgenweite.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Rise(CPolar position, ref double jdEvent, double jd, ref double azimuth){ return MMoon.Rise(position.Longitude, position.Latitude, ref jdEvent, jd, ref azimuth); }

	// MMoon.Rise(double, double, ref double)
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
		return MMoon.Rise(lambda, phi, ref jdEvent, jd, ref azm);
	}

	// MMoon.Rise(double, double, ref double, double)
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
		return MMoon.Rise(lambda, phi, ref jdEvent, jd, ref azm);
	}

	// MMoon.Rise(double, double, ref double, double, ref double)
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
		double l    =  0.0;                               // Geozentrische Länge
		double a    =  0.0;                               // Rektaszension
		double d    =  0.0;                               // Deklination
		double dm   =  1.0;                               // Korrekturglied
		double h    =  0.0;                               //
		double h0   = MEphemerides.GeocentricHeight_Moon; // Refraktionswinkel
		double H    =  0.0;                               //
		double sinP = MMath.Sin(phi);                     // Breitensinus
		double cosP = MMath.Cos(phi);                     // Breitencosinus

		// Position für nachfolgenden Tag berechnen
		l = MMoon.Longitude(EPrecision.Low, jdn + 1.0);
		double aP = MEphemerides.ToAlpha(l, 0.0, EObliquity.Mean, jdn + 1.0);
		double dP = MEphemerides.ToDelta(l, 0.0, EObliquity.Mean, jdn + 1.0);

		// Position für gegebenen Tag berechnen
		l = MMoon.Longitude(EPrecision.Low, jdn);
		double a0 = MEphemerides.ToAlpha(l, 0.0, EObliquity.Mean, jdn);
		if(MMath.Abs(aP - a0) > 1.0) a0 += MMath.Sgn(aP - a0) * MMath.Pi2;
		double d0 = MEphemerides.ToDelta(l, 0.0, EObliquity.Mean, jdn);

		// Position für vorhergehenden Tag berechnen
		l = MMoon.Longitude(EPrecision.Low, jdn + 1.0);
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

		// Ereigniszeit prüfen
		if(m < 0.0 || m >= 1.0) return EEventType.NoEvent;

		// Iteration anwenden, Azimut berechnen und Rückgabewert setzen
		jdEvent = jd + m;
		azimuth = MEphemerides.ToAzimuth(H, d, phi);
		return EEventType.Normal;
	}

	// MMoon.Set(CPolar, ref double)
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
		return MMoon.Set(position.Longitude, position.Latitude, ref jdEvent, jd, ref azm);
	}

	// MMoon.Set(CPolar, ref double, double)
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
		return MMoon.Set(position.Longitude, position.Latitude, ref jdEvent, jd, ref azm);
	}

	// MMoon.Set(CPolar, ref double, double, ref double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Untergangs und die Abendweite am geographischen Ort und zur julianischen Tageszahl und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="position">Geographische Position.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Untergangs.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="azimuth">Abendweite.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Set(CPolar position, ref double jdEvent, double jd, ref double azimuth)
	{
		// Rückgabe
		return MMoon.Set(position.Longitude, position.Latitude, ref jdEvent, jd, ref azimuth);}

	// MMoon.Set(double, double, ref double)
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
		return MMoon.Set(lambda, phi, ref jdEvent, jd, ref azm);
	}

	// MMoon.Set(double, double, ref double, double)
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
		return MMoon.Set(lambda, phi, ref jdEvent, jd, ref azm);
	}

	// MMoon.Set(double, double, ref double, double, ref double)
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
		double l    =  0.0;                               // Geozentrische Länge
		double a    =  0.0;                               // Rektaszension
		double d    =  0.0;                               // Deklination
		double dm   =  1.0;                               // Korrekturglied
		double h    =  0.0;                               //
		double h0   = MEphemerides.GeocentricHeight_Moon; // Refraktionswinkel
		double H    =  0.0;                               //
		double sinP = MMath.Sin(phi);                     // Breitensinus
		double cosP = MMath.Cos(phi);                     // Breitencosinus

		// Position für nachfolgenden Tag berechnen
		l = MMoon.Longitude(EPrecision.Low, jdn + 1.0);
		double aP = MEphemerides.ToAlpha(l, 0.0, EObliquity.Mean, jd + 1.0);
		double dP = MEphemerides.ToDelta(l, 0.0, EObliquity.Mean, jd + 1.0);

		// Position für gegebenen Tag berechnen
		l = MMoon.Longitude(EPrecision.Low, jdn);
		double a0 = MEphemerides.ToAlpha(l, 0.0, EObliquity.Mean, jdn);
		if(MMath.Abs(aP - a0) > 1.0) a0 += MMath.Sgn(aP - a0) * MMath.Pi2;
		double d0 = MEphemerides.ToDelta(l, 0.0, EObliquity.Mean, jdn);

		// Position für vorhergehenden Tag berechnen
		l = MMoon.Longitude(EPrecision.Low, jdn - 1.0);
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
	public static double SynodicMonth()
	{
		// Lokale Felder einrichten und Länge berechnen
		double jd = DateTime.Now.ToJdn();
		return MMoon.SynodicMonth(jd);
	}

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

	// MMoon.Transit(CPolar, ref double)
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
		return MMoon.Transit(position.Longitude, position.Latitude, ref jdEvent, jd, ref h);
	}

	// MMoon.Transit(CPolar, ref double, double)
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
		return MMoon.Transit(position.Longitude, position.Latitude, ref jdEvent, jd, ref h);
	}

	// MMoon.Transit(CPolar, ref double, double, ref double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Meridiandurchgangs und die Höhe am geographischen Ort und zur aktuekllen Systemzeit und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="pos">Geographische Position.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Meridiandurchgangs.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="height">Höhe.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Transit(CPolar position, ref double jdEvent, double jd, ref double height){ return MMoon.Transit(position.Longitude, position.Latitude, ref jdEvent, jd, ref height); }

	// MMoon.Transit(double, double, ref double)
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
		return MMoon.Transit(lambda, phi, ref jdEvent, jd, ref h);
	}

	// MMoon.Transit(double, double, ref double, double)
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
		return MMoon.Transit(lambda, phi, ref jdEvent, jd, ref h);
	}

	// MMoon.Transit(double, double, ref double, double, ref double)
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
		l = MMoon.Longitude(EPrecision.Low, jdn + 1.0);
		double aP = MEphemerides.ToAlpha(l, 0.0, EObliquity.Mean, jdn + 1.0);
		double dP = MEphemerides.ToDelta(l, 0.0, EObliquity.Mean, jdn + 1.0);

		// Position für gegebenen Tag berechnen
		l = MMoon.Longitude(EPrecision.Low, jdn);
		double a0 = MEphemerides.ToAlpha(l, 0.0, EObliquity.Mean, jdn);
		if(MMath.Abs(aP - a0) > 1.0) a0 += MMath.Sgn(aP - a0) * MMath.Pi2;
		double d0 = MEphemerides.ToDelta(l, 0.0, EObliquity.Mean, jdn);

		// Position für vorhergehenden Tag berechnen
		l = MMoon.Longitude(EPrecision.Low, jdn - 1.0);
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

	// MMoon.TropicalMonth()
	/// <summary>
	/// Liefert die Dauer des tropischen Monats.
	/// </summary>
	/// <returns> Dauer des tropischen Monats.</returns>
	public static double TropicalMonth(){ return 27.32158; }
}
