using Acamat.LMath;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt Berechnungen zu den Galileischen Monden.
/// </summary>
public static class MGalileanMoon
{
	// -------------- //
	// Private Felder //
	// -------------- //
	private static double m_d;   // Anzahl der Tage nach dem 10. Aug. 1976.
	private static double m_G0;  // Korrektur der Jupiterlänge.
	private static double m_G1;  // Mittlere Anomalie des Jupiter.
	private static double m_G2;  // Mittlere Anomalie des Saturn.
	private static double m_H1;  // Hilfswinkel 1.
	private static double m_LO1; // Knotenlänge für Io.
	private static double m_LO2; // Knotenlänge für Europa.
	private static double m_LO3; // Knotenlänge für Ganymed.
	private static double m_LO4; // Knotenlänge für Kallisto.
	private static double m_LP1; // Perijovenlänge für Io.
	private static double m_LP2; // Perijovenlänge für Europa.
	private static double m_LP3; // Perijovenlänge für Ganymed.
	private static double m_LP4; // Perijovenlänge für Kallisto.
	private static double m_ML1; // Mittlere Länge für Io.
	private static double m_ML2; // Mittlere Länge für Europa.
	private static double m_ML3; // Mittlere Länge für Ganymed.
	private static double m_ML4; // Mittlere Länge für Kallisto.
	private static double m_Phi; // Phase der freien Libration.
	private static double m_Pi;  // Perihellänge des Jupiters.
	private static double m_Psi; // Knotenlänge des Jupiteräquators.
	private static double m_TL1; // Wahre Länge für Io.
	private static double m_TL2; // Wahre Länge für Europa.
	private static double m_TL3; // Wahre Länge für Ganymede.
	private static double m_TL4; // Wahre Länge für Callisto.

	// ------------------- //
	// Felder und Methoden //
	// ------------------- //
	// MGalileanMoon.Calculate(double)
	/// <summary>
	/// Richtet die privaten Felder zur julianischen Tageszahl ein.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	private static void Calculate(double jd)
	{
		// Tageszahl berechnen
		m_d = jd - 2443000.5;

		// ---------- //
		// Basisdaten //
		// ---------- //

		// Mittlere Längen berechnen
		m_ML1 = MMath.ToRad(MMod.Mod(106.077187 + 203.48895579033 * m_d, 360.0));
		m_ML2 = MMath.ToRad(MMod.Mod(175.731615 + 101.37472473479 * m_d, 360.0));
		m_ML3 = MMath.ToRad(MMod.Mod(120.558829 +  50.31760920702 * m_d, 360.0));
		m_ML4 = MMath.ToRad(MMod.Mod( 84.444587 +  21.57107117668 * m_d, 360.0));

		// Perijovenlängen berechnen
		m_LP1 = MMath.ToRad(MMod.Mod( 97.088086 +   0.16138586144 * m_d, 360.0));
		m_LP2 = MMath.ToRad(MMod.Mod(154.866335 +   0.04726306609 * m_d, 360.0));
		m_LP3 = MMath.ToRad(MMod.Mod(188.184037 +   0.00712733949 * m_d, 360.0));
		m_LP4 = MMath.ToRad(MMod.Mod(335.286807 +   0.00183999637 * m_d, 360.0));

		// Knotenlängen berechnen
		m_LO1 = MMath.ToRad(MMod.Mod(312.334566 -   0.13279385940 * m_d, 360.0));
		m_LO2 = MMath.ToRad(MMod.Mod(100.441116 -   0.03263063731 * m_d, 360.0));
		m_LO3 = MMath.ToRad(MMod.Mod(119.194241 -   0.00717703155 * m_d, 360.0));
		m_LO4 = MMath.ToRad(MMod.Mod(322.618633 -   0.00175933880 * m_d, 360.0));

		// Korrektur der Jupiterlänge berechnen
		m_G0  = 0.33033 * MMath.Sin(MMath.ToRad(163.679 + 0.0010512 * m_d));
		m_G0 += 0.03439 * MMath.Sin(MMath.ToRad( 34.486 - 0.0161731 * m_d));

		// Phase der freien Libration berechnen
		m_Phi = MMath.ToRad(MMod.Mod(199.676608 + 0.17379190461 * m_d, 360.0));

		// Knotenlänge des Jupiteräquator berechnen
		m_Psi = MMath.ToRad(MMod.Mod(316.518203 + 0.00000208362 * m_d, 360.0));

		// Mittlere Anomalien berechnen
		m_G1  = MMath.ToRad(MMod.Mod( 30.237557 + 0.08309257010 * m_d + m_G0, 360.0));
		m_G2  = MMath.ToRad(MMod.Mod( 31.978528 + 0.03345973390 * m_d,        360.0));

		// Länge des Perihels berechnen
		m_Pi  = MMath.ToRad(13.469942);

		// ----------- //
		// Korrekturen //
		// ----------- //

		// Hilfswinkel einrichten
		m_H1 = MMath.ToRad(52.225);

		// Wahre Längen berechnen
		m_TL1 = m_ML1 + MGalileanMoon.m_dL1();
		m_TL2 = m_ML2 + MGalileanMoon.m_dL2();
		m_TL3 = m_ML3 + MGalileanMoon.m_dL3();
		m_TL4 = m_ML4 + MGalileanMoon.m_dL4();
	}

	// MGalileanMoon.m_dL1()
	/// <summary>
	/// Liefert die Längenkorrektur für Io.
	/// </summary>
	/// <returns>Längenkorrektur für Io.</returns>
	private static double m_dL1()
	{
		// Lokale Felder einrichten
		double rtn = 0.0;

		// Korrekturen aufsummieren
		rtn +=  0.47259 * MMath.Sin(2.0 * m_ML1 - 2.0 * m_ML2                            );
		rtn += -0.03478 * MMath.Sin(      m_LP3 -       m_LP4                            );
		rtn +=  0.01081 * MMath.Sin(      m_ML2 - 2.0 * m_ML3 +       m_LP3              );
		rtn +=  0.00738 * MMath.Sin(      m_Phi                                          );
		rtn +=  0.00713 * MMath.Sin(      m_ML2 - 2.0 * m_ML3 +       m_LP2              );
		rtn += -0.00674 * MMath.Sin(      m_LP1 +       m_LP3 - 2.0 * m_Pi  - 2.0 * m_G1 );
		rtn +=  0.00666 * MMath.Sin(      m_ML2 - 2.0 * m_ML3 +       m_LP4              );
		rtn +=  0.00445 * MMath.Sin(      m_ML1 -       m_LP3                            );
		rtn += -0.00354 * MMath.Sin(      m_ML1 -       m_ML2                            );
		rtn += -0.00317 * MMath.Sin(2.0 * m_Psi - 2.0 * m_Pi                             );
		rtn +=  0.00265 * MMath.Sin(      m_ML1 -       m_LP4                            );
		rtn += -0.00186 * MMath.Sin(      m_G1                                           );
		rtn +=  0.00162 * MMath.Sin(      m_LP2 -       m_LP3                            );
		rtn +=  0.00158 * MMath.Sin(4.0 * m_ML1 - 4.0 * m_ML2                            );
		rtn += -0.00155 * MMath.Sin(      m_ML1 -       m_ML3                            );
		rtn += -0.00138 * MMath.Sin(      m_Psi +       m_LO3 - 2.0 * m_Pi  - 2.0 * m_G1 );
		rtn += -0.00115 * MMath.Sin(2.0 * m_ML1 - 4.0 * m_ML2 + 2.0 * m_LO2              );
		rtn +=  0.00089 * MMath.Sin(      m_LP2 -       m_LP4                            );
		rtn +=  0.00085 * MMath.Sin(      m_ML1 +       m_LP3 - 2.0 * m_Pi  - 2.0 * m_G1 );
		rtn +=  0.00083 * MMath.Sin(      m_LO2 -       m_LO3                            );
		rtn +=  0.00053 * MMath.Sin(      m_Psi -       m_LO2                            );
		return MMath.ToRad(rtn);
	}

	// MGalileanMoon.m_dL2()
	/// <summary>
	/// Liefert die Längenkorrektur für Europa.
	/// </summary>
	/// <returns>Längenkorrektur für Europa.</returns>
	private static double m_dL2()
	{
		// Lokale Felder einrichten
		double rtn = 0.0;

		// Korrekturen aufsummieren
		rtn +=  1.06476 * MMath.Sin(2.0 * m_ML2 - 2.0 * m_ML3                            );
		rtn +=  0.04256 * MMath.Sin(      m_ML1 - 2.0 * m_ML2 +       m_LP3              );
		rtn +=  0.03581 * MMath.Sin(      m_ML2 -       m_LP3                            );
		rtn +=  0.02395 * MMath.Sin(      m_ML1 - 2.0 * m_ML2 +       m_LP4              );
		rtn +=  0.01984 * MMath.Sin(      m_ML2 -       m_LP4                            );
		rtn += -0.01778 * MMath.Sin(      m_Phi                                          );
		rtn +=  0.01654 * MMath.Sin(      m_ML2 -       m_LP2                            );
		rtn +=  0.01334 * MMath.Sin(      m_ML2 - 2.0 * m_ML3 +       m_LP2              );
		rtn +=  0.01294 * MMath.Sin(      m_LP3 -       m_LP4                            );
		rtn += -0.01142 * MMath.Sin(      m_ML2 -       m_ML3                            );
		rtn += -0.01057 * MMath.Sin(      m_G1                                           );
		rtn += -0.00775 * MMath.Sin(2.0 * m_Psi - 2.0 * m_Pi                             );
		rtn +=  0.00524 * MMath.Sin(2.0 * m_ML1 - 2.0 * m_ML2                            );
		rtn += -0.00460 * MMath.Sin(      m_ML1 -       m_ML3                            );
		rtn +=  0.00316 * MMath.Sin(      m_Psi - 2.0 * m_G1  +       m_LO3 - 2.0 * m_Pi );
		rtn += -0.00203 * MMath.Sin(      m_LP1 +       m_LP3 - 2.0 * m_Pi  - 2.0 * m_G1 );
		rtn +=  0.00146 * MMath.Sin(      m_Psi -       m_LO3                            );
		rtn += -0.00145 * MMath.Sin(2.0 * m_G1                                           );
		rtn +=  0.00125 * MMath.Sin(      m_Psi -       m_LO4                            );
		rtn += -0.00115 * MMath.Sin(      m_ML1 - 2.0 * m_ML3 +       m_LP3              );
		rtn += -0.00094 * MMath.Sin(2.0 * m_ML3 - 2.0 * m_LO2                            );
		rtn +=  0.00086 * MMath.Sin(2.0 * m_ML1 - 4.0 * m_ML2 + 2.0 * m_LO2              );
		rtn += -0.00086 * MMath.Sin(5.0 * m_G2  - 2.0 * m_G1  +       m_H1               );
		rtn += -0.00078 * MMath.Sin(      m_ML2 -       m_ML4                            );
		rtn += -0.00064 * MMath.Sin(3.0 * m_ML3 - 7.0 * m_ML4 + 4.0 * m_LP4              );
		rtn +=  0.00064 * MMath.Sin(      m_LP1 -       m_LP4                            );
		rtn += -0.00063 * MMath.Sin(      m_ML1 - 2.0 * m_ML3 +       m_LP4              );
		rtn +=  0.00058 * MMath.Sin(      m_LO3 -       m_LO4                            );
		rtn +=  0.00056 * MMath.Sin(2.0 * m_Psi - 2.0 * m_Pi  - 2.0 * m_G1               );
		rtn +=  0.00055 * MMath.Sin(2.0 * m_ML2 - 2.0 * m_ML4                            );
		rtn +=  0.00052 * MMath.Sin(3.0 * m_ML3 - 7.0 * m_ML4 +       m_LP3 + 3.0 * m_LP4);
		rtn += -0.00043 * MMath.Sin(      m_ML1 -       m_LP3                            );
		rtn +=  0.00041 * MMath.Sin(5.0 * m_ML2 - 5.0 * m_ML3                            );
		rtn +=  0.00041 * MMath.Sin(      m_LP4 -       m_Pi                             );
		rtn +=  0.00032 * MMath.Sin(      m_LO2 -       m_LO3                            );
		rtn +=  0.00032 * MMath.Sin(2.0 * m_ML3 - 2.0 * m_G1  - 2.0 * m_Pi               );
		return MMath.ToRad(rtn);
	}

	// MGalileanMoon.m_dL3()
	/// <summary>
	/// Liefert die Längenkorrektur für Ganymede.
	/// </summary>
	/// <returns>Längenkorrektur für Ganymede.</returns>
	private static double m_dL3()
	{
		// Lokale Felder einrichten
		double rtn = 0.0;

		// Korrekturen aufsummieren
		rtn +=  0.16490 * MMath.Sin(      m_ML3 -       m_LP3                            );
		rtn +=  0.09081 * MMath.Sin(      m_ML3 -       m_LP4                            );
		rtn += -0.06907 * MMath.Sin(      m_ML2 -       m_ML3                            );
		rtn +=  0.03784 * MMath.Sin(      m_LP3 -       m_LP4                            );
		rtn +=  0.01846 * MMath.Sin(2.0 * m_ML3 - 2.0 * m_ML4                            );
		rtn += -0.01340 * MMath.Sin(      m_G0                                           );
		rtn += -0.01014 * MMath.Sin(2.0 * m_Psi - 2.0 * m_Pi                             );
		rtn +=  0.00704 * MMath.Sin(      m_ML2 - 2.0 * m_ML3 +       m_LP3              );
		rtn += -0.00620 * MMath.Sin(      m_ML2 - 2.0 * m_ML3 +       m_LP2              );
		rtn += -0.00541 * MMath.Sin(      m_ML3 -       m_ML4                            );
		rtn +=  0.00381 * MMath.Sin(      m_ML2 - 2.0 * m_ML3 +       m_LP4              );
		rtn +=  0.00235 * MMath.Sin(      m_Psi -       m_LO3                            );
		rtn +=  0.00198 * MMath.Sin(      m_Psi -       m_LO4                            );
		rtn +=  0.00176 * MMath.Sin(      m_Phi                                          );
		rtn +=  0.00130 * MMath.Sin(3.0 * m_ML3 - 3.0 * m_ML4                            );
		rtn +=  0.00125 * MMath.Sin(      m_ML1 -       m_ML3                            );
		rtn += -0.00119 * MMath.Sin(5.0 * m_G1  - 2.0 * m_G2  +       m_H1               );
		rtn +=  0.00109 * MMath.Sin(      m_ML1 -       m_ML2                            );
		rtn += -0.00100 * MMath.Sin(3.0 * m_ML3 - 7.0 * m_ML4 + 4.0 * m_LP4              );
		rtn +=  0.00091 * MMath.Sin(      m_ML3 -       m_ML4                            );
		rtn +=  0.00080 * MMath.Sin(3.0 * m_ML3 - 7.0 * m_ML4 +       m_LP3 + 3.0 * m_LP4);
		rtn += -0.00075 * MMath.Sin(2.0 * m_ML2 - 3.0 * m_ML3 +       m_LP3              );
		rtn +=  0.00072 * MMath.Sin(      m_LP1 +       m_LP3 - 2.0 * m_Pi  - 2.0 * m_G1 );
		rtn +=  0.00058 * MMath.Sin(      m_LP4 -       m_Pi                             );
		rtn += -0.00058 * MMath.Sin(2.0 * m_ML3 - 3.0 * m_ML4 +       m_LP4              );
		rtn += -0.00057 * MMath.Sin(      m_ML3 - 2.0 * m_ML4 +       m_LP4              );
		rtn +=  0.00056 * MMath.Sin(      m_ML3 +       m_LP3 - 2.0 * m_Pi  - 2.0 * m_G1 );
		rtn += -0.00052 * MMath.Sin(      m_ML2 - 2.0 * m_ML3 +       m_LP1              );
		rtn += -0.00050 * MMath.Sin(      m_LP2 -       m_LP3                            );
		rtn +=  0.00048 * MMath.Sin(      m_ML3 - 2.0 * m_ML4 +       m_LP3              );
		rtn += -0.00045 * MMath.Sin(2.0 * m_ML2 - 3.0 * m_ML3 +       m_LP4              );
		rtn += -0.00041 * MMath.Sin(      m_LP2 -       m_LP4                            );
		rtn += -0.00038 * MMath.Sin(2.0 * m_G1                                           );
		rtn += -0.00037 * MMath.Sin(      m_LP3 -       m_LP4 +       m_LO3 -       m_LO4);
		rtn += -0.00032 * MMath.Sin(3.0 * m_ML3 - 7.0 * m_ML4 + 2.0 * m_LP3 + 2.0 * m_LP4);
		rtn +=  0.00030 * MMath.Sin(4.0 * m_ML3 - 4.0 * m_ML4                            );
		rtn +=  0.00029 * MMath.Sin(      m_ML3 +       m_LP4 - 2.0 * m_Pi  - 2.0 * m_G1 );
		rtn += -0.00028 * MMath.Sin(      m_LO3 +       m_Psi - 2.0 * m_Pi  - 2.0 * m_G1 );
		rtn +=  0.00026 * MMath.Sin(      m_ML3 -       m_Pi  -       m_G1               );
		rtn +=  0.00024 * MMath.Sin(      m_ML3 - 3.0 * m_ML3 + 2.0 * m_ML4              );
		rtn +=  0.00021 * MMath.Sin(2.0 * m_ML3 - 2.0 * m_Pi  - 2.0 * m_G1               );
		rtn += -0.00021 * MMath.Sin(      m_ML3 -       m_LP3                            );
		rtn +=  0.00017 * MMath.Sin(2.0 * m_ML3 - 2.0 * m_LP3                            );
		return MMath.ToRad(rtn);
	}

	// MGalileanMoon.m_dL4()
	/// <summary>
	/// Liefert die Längenkorrektur für Kallisto.
	/// </summary>
	/// <returns>Längenkorrektur für Kallisto.</returns>
	private static double m_dL4()
	{
		// Lokale Felder einrichten
		double rtn = 0.0;

		// Korrekturen aufsummieren
		return MMath.ToRad(rtn);
	}
}
