using Acamat.LCore;
using Acamat.LMath;
using Acamat.LMath.Geometry;
using System;
using System.Text;

// HeliacalRise
// HeliacalSet
// HeliacalConjunction
// HeliacalOpposition
// IsCirumpolar
//    δ + ϕ ≥ 90°

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt ephemeridale Funktionen.
/// </summary>
public static partial class MEphemerides
{
	// ---------- //
	// Konstanten //
	// ---------- //
	/// <summary>
	/// Geozentrische Höhe des Mondes beim Horizontdurchgang.
	/// </summary>
	public static double GeocentricHeight_Moon = -0.002327106;

	/// <summary>
	/// Geozentrische Höhe eines Sternes oder Planeten beim Horizontdurchgang.
	/// </summary>
	public static double GeocentricHeight_Star = -0.009890199;

	/// <summary>
	/// Geozentrische Höhe der Sonne beim Horizontdurchgang.
	/// </summary>
	public static double GeocentricHeight_Sun = -0.014543829;

	/// <summary>
	/// Geozentrische Höhe der Sonne für die astronomische Dämmerung.
	/// </summary>
	public static double GeocentricHeight_TwilightAstronomical = -0.314159265;

	/// <summary>
	/// Geozentrische Höhe der Sonne für die bürgerliche Dämmerung.
	/// </summary>
	public static double GeocentricHeight_TwilightCivil = -0.104719755;

	/// <summary>
	/// Geozentrische Höhe der Sonne für die nautische Dämmerung.
	/// </summary>
	public static double GeocentricHeight_TwilightNautical = -0.209439510;

	/// <summary>
	/// Schiefe der Ekliptik zur Standardepoche 2000.
	/// </summary>
	public static double Obliquity_2000 =  0.409092804222;

	// ------------------- //
	// Felder und Methoden //
	// ------------------- //
	// MEphemerides.AberrationEcliptical(CPolar)
	/// <summary>
	/// Liefert die um die Aberration korrigierte Position zur ekliptikalen Position und aktuellen Systemzeit.
	/// </summary>
	/// <param name="position">Ekliptikale Position.</param>
	/// <returns>Um die Aberration korrigierte Position zur ekliptikalen Position und aktuellen Systemzeit.</returns>
	public static CPolar AberrationEcliptical(CPolar position)
	{
		// Lokale Felder einrichten und Aberration anwenden
		double jd     = DateTime.Now.ToJdn();
		double lambda = position.Longitude;
		double beta   = position.Latitude;
		MEphemerides.AberrationEcliptical(ref lambda, ref beta, jd);
		return new CPolar(lambda, beta, 1.0);
	}

	// MEphemerides.AberrationEcliptical(ref double, ref double)
	/// <summary>
	/// Wendet die Aberration auf die ekliptikale Position zur aktuellen Systemzeit an.
	/// </summary>
	/// <param name="lambda">Ekliptikale Länge.</param>
	/// <param name="beta">Ekliptikale Breite.</param>
	public static void AberrationEcliptical(ref double lambda, ref double beta)
	{
		// Lokale Felder einrichten und Aberration anwenden
		double jd = DateTime.Now.ToJdn();
		MEphemerides.AberrationEcliptical(ref lambda, ref beta, jd);
	}

	// MEphemerides.AberrationEcliptical(CPolar, double)
	/// <summary>
	/// Liefert die um die Aberration korrigierte Position zur ekliptikalen Position und julianischer Tageszahl.
	/// </summary>
	/// <param name="position">Ekliptikale Position.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Um die Aberration korrigierte Position zur ekliptikalen Position und julianischer Tageszahl.</returns>
	public static CPolar AberrationEcliptical(CPolar position, double jd)
	{
		// Lokale Felder einrichten und Aberration anwenden
		double lambda = position.Longitude;
		double beta   = position.Latitude;
		MEphemerides.AberrationEcliptical(ref lambda, ref beta, jd);
		return new CPolar(lambda, beta, 1.0);
	}

	// MEphemerides.AberrationEcliptical(ref double, ref double, double)
	/// <summary>
	/// Wendet die Aberration auf die ekliptikale Position zur julianischen Tageszahl an.
	/// </summary>
	/// <param name="lambda">Ekliptikale Länge.</param>
	/// <param name="beta">Ekliptikale Breite.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	public static void AberrationEcliptical(ref double lambda, ref double beta, double jd)
	{
		// Lokalen Felder einrichten
		double t = (jd - MCalendar.Jdn20000101) / 36525.0;
		double k = MMath.ToRad(20.49552 / 3600.0);
		double e = MMath.Polynome(t, 0.016708634, -0.000042037, -0.0000001267);
		double p = MMath.ToRad(MMath.Polynome(t, 102.93735, 1.71946, 0.00046));
		double l = MSun.Longitude(EPrecision.High, jd);

		// Aberration berechnen und anwenden
		lambda += (-k * MMath.Cos(l - lambda) + e * k * MMath.Cos(p - lambda)) / MMath.Cos(beta);
		beta   +=  -k * MMath.Sin(beta) * (MMath.Sin(l - lambda) - e * MMath.Sin(p - lambda));;
	}

	// MEphemerides.AberrationEquatorial(CPolar)
	/// <summary>
	/// Liefert die um die Aberration korrigierte Position zur äquatorialen Position und aktuellen Systemzeit.
	/// </summary>
	/// <param name="position">Äquatoriale Position.</param>
	/// <returns>Um die Aberration korrigierte Position zur äquatorialen Position und aktuellen Systemzeit.</returns>
	public static CPolar AberrationEquatorial(CPolar position)
	{
		// Lokale Felder einrichten und Aberration anwenden
		double jd    = DateTime.Now.ToJdn();
		double alpha = position.Longitude;
		double delta = position.Latitude;
		MEphemerides.AberrationEquatorial(ref alpha, ref delta, jd);
		return new CPolar(alpha, delta, 1.0);
	}

	// MEphemerides.AberrationEquatorial(ref double, ref double)
	/// <summary>
	/// Wendet die Aberration auf die äquatoriale Position zur aktuellen Systemzeit an.
	/// </summary>
	/// <param name="alpha">Rektaszension.</param>
	/// <param name="delta">Deklination.</param>
	public static void AberrationEquatorial(ref double alpha, ref double delta)
	{
		// Lokale Felder einrichten und Aberration anwenden
		double jd = DateTime.Now.ToJdn();
		MEphemerides.AberrationEquatorial(ref alpha, ref delta, jd);
	}

	// MEphemerides.AberrationEquatorial(CPolar, double)
	/// <summary>
	/// Liefert die um die Aberration korrigierte Position zur äquatorialen Position und julianischer Tageszahl.
	/// </summary>
	/// <param name="position">Äquatoriale Position.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Um die Aberration korrigierte Position zur äquatorialen Position und julianischer Tageszahl.</returns>
	public static CPolar AberrationEquatorial(CPolar position, double jd)
	{
		// Lokale Felder einrichten und Aberration anwenden
		double alpha = position.Longitude;
		double delta = position.Latitude;
		MEphemerides.AberrationEquatorial(ref alpha, ref delta, jd);
		return new CPolar(alpha, delta, 1.0);
	}

	// MEphemerides.AberrationEquatorial(ref double, ref double, double)
	/// <summary>
	/// Wendet die Aberration auf die äquatoriale Position zur julianischen Tageszahl an.
	/// </summary>
	/// <param name="alpha">Rektaszension.</param>
	/// <param name="delta">Deklination.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	public static void AberrationEquatorial(ref double alpha, ref double delta, double jd)
	{
		// Lokalen Felder einrichten
		double t = (jd - MCalendar.Jdn20000101) / 36525.0;
		double k = MMath.ToRad(20.49552 / 3600.0);
		double e = MMath.Polynome(t, 0.016708634, -0.000042037, -0.0000001267);
		double p = MMath.ToRad(MMath.Polynome(t, 102.93735, 1.71946, 0.00046));
		double l = MSun.Longitude(EPrecision.High, jd);

		// Hilfsfelder einrichten
		double cosA = MMath.Cos(alpha);
		double cosD = MMath.Cos(delta);
		double cosE = MMath.Cos(e);
		double cosL = MMath.Cos(l);
		double cosP = MMath.Cos(p);
		double sinA = MMath.Sin(alpha);
		double sinD = MMath.Sin(delta);
		double sinL = MMath.Sin(l);
		double sinP = MMath.Sin(p);
		double tanE = MMath.Tan(e);
		double h    = tanE * cosD - sinA * sinD;

		// Aberration berechnen und anwenden
		double dA  =     -k * ((cosA * cosL * cosE + sinA * sinL) / cosD);
				 dA +=  e * k * ((cosA * cosP * cosE + sinA * sinP) / cosD);
		alpha     += dA;
		double dD  =     -k * (cosL * cosE * h + cosA * sinD * sinL);
				 dD +=  e * k * (cosP * cosE * h + cosA * sinD * sinP);
		delta     += dD;
	}

	// MEphmerides.AngleOfDiurnalPath(double, double)
	/// <summary>
	/// Liefert den Winkel des Tagbogens zur Deklination und geographischer Breite zum Zeitpunkt des Horizontdurchganges.
	/// </summary>
	/// <param name="delta">Deklination.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <returns>Winkel des Tagbogens zur Deklination und geographischer Breite zum Zeitpunkt des Horizontdurchganges.</returns>
	public static double AngleOfDiurnalPath(double delta, double phi)
	{
		// Lokalen Felder einrichten und Winkel berechnen
		double c = MMath.Sqr(1.0 - MMath.Pow(MMath.Tan(delta) * MMath.Tan(phi), 2.0));
		return MMath.ArcTan(c * MMath.Cos(delta), MMath.Tan(phi));
	}

	// MEphemerides.AngularSeparation(CPolar, CPolar)
	/// <summary>
	/// Liefert den Winkelabstand zweier äquatorialer Positionen.
	/// </summary>
	/// <param name="positionA">Position A.</param>
	/// <param name="positionB">Position B.</param>
	/// <returns>Winkelabstand zweier äquatorialer Positionen.</returns>
	public static double AngularSeparation(CPolar positionA, CPolar positionB)
	{
		// Lokale Felder einrichten und Winkelabstand berechnen
		double angle = 0.0;
		return MEphemerides.AngularSeparation(positionA.Longitude, positionA.Latitude, positionB.Longitude, positionB.Latitude, ref angle);
	}

	// MEphemerides.AngularSeparation(CPolar, CPolar, ref double)
	/// <summary>
	/// Setzt den Positionswinkel und liefert den Winkelabstand zweier äquatorialer Positionen.
	/// </summary>
	/// <param name="positionA">Position A.</param>
	/// <param name="positionB">Position B.</param>
	/// <param name="angle">Positionswinkel.</param>
	/// <returns>Winkelabstand zweier äquatorialer Positionen.</returns>
	public static double AngularSeparation(CPolar positionA, CPolar positionB, ref double angle){ return MEphemerides.AngularSeparation(positionA.Longitude, positionA.Latitude, positionB.Longitude, positionB.Latitude, ref angle); }

	// MEphemerides.AngularSeparation(double, double, double, double)
	/// <summary>
	/// Liefert den Winkelabstand zweier äquatorialer Positionen.
	/// </summary>
	/// <param name="alphaA">Rektaszension zur Position A.</param>
	/// <param name="deltaA">Deklination zur Position A.</param>
	/// <param name="alphaB">Rektaszension zur Position B.</param>
	/// <param name="deltaB">Deklination zur Position B.</param>
	/// <returns>Winkelabstand zweier äquatorialer Positionen.</returns>
	public static double AngularSeparation(double alphaA, double deltaA, double alphaB, double deltaB)
	{
		// Lokalen Felder einrichten und Winkelanstand berechnen
		double angle = 0.0;
		return MEphemerides.AngularSeparation(alphaA, deltaA, alphaB, deltaB, ref angle);
	}

	// MEphemerides.AngularSeparation(double, double, double, double, ref double)
	/// <summary>
	/// Setzt den Positionswinkel und liefert den Winkelabstand zweier äquatorialer Positionen.
	/// </summary>
	/// <param name="alphaA">Rektaszension zur Position A.</param>
	/// <param name="deltaA">Deklination zur Position A.</param>
	/// <param name="alphaB">Rektaszension zur Position B.</param>
	/// <param name="deltaB">Deklination zur Position B.</param>
	/// <param name="angle">Positionswinkel.</param>
	/// <returns>Winkelabstand zweier äquatorialer Positionen.</returns>
	public static double AngularSeparation(double alphaA, double deltaA, double alphaB, double deltaB, ref double angle)
	{
		// Lokalen Felder einrichten
		// TODO: MEphemerides.AngularSeparation(double, double, double, double, ref double): Verwendung der Winkelwandelungsfunktionen prüfen.
		double cosdA = MMath.Cos(alphaB - alphaA);
		double sindA = MMath.Sin(alphaB - alphaA);
		double cosD1 = MMath.Cos(deltaA);
		double sinD1 = MMath.Sin(deltaA);
		double tanD1 = MMath.Tan(deltaA);
		double cosD2 = MMath.Cos(deltaB);
		double sinD2 = MMath.Sin(deltaB);

		// Rechtwinklige Koordinaten berechnen
		double x = cosD1 * sinD2 - sinD1 * cosD2 * cosdA;
		double y = cosD2 * sindA;
		double z = sinD1 * sinD2 + cosD1 * cosD2 * cosdA;

		// Winkel berechnen
		angle = MMath.ArcTan(MMath.Sin(alphaA - alphaB), cosD2 * tanD1 - sinD2 * MMath.Cos(alphaA - alphaB));
		return MMath.ArcTan(MMath.Sqr(x * x + y * y), z);
	}

	// MEphemerids.ApparentPosition(CPolar)
	/// <summary>
	/// Liefert die scheinbare Position zur äquatorialen Position und aktueller Systemzeit.
	/// </summary>
	/// <param name="pos">Äquatoriale Position.</param>
	/// <returns>Scheinbare Position zur äquatorialen Position und aktueller Systemzeit.</returns>
	public static CPolar ApparentPosition(CPolar pos)
	{
		// Lokale Felder einrichten
		double jd    = DateTime.Now.ToJdn();
		double alpha = pos.Longitude;
		double delta = pos.Latitude;
		MEphemerides.ApparentPosition(ref alpha, ref delta, jd);
		return new CPolar(alpha, delta, 1.0);
	}

	// MEphemerids.ApparentPosition(CPolar, double)
	/// <summary>
	/// Liefert die scheinbare Position zur äquatorialen Position und julianischer Tageszahl.
	/// </summary>
	/// <param name="position">Äquatoriale Position.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Scheinbare Position zur äquatorialen Position und julianischer Tageszahl.</returns>
	public static CPolar ApparentPosition(CPolar postion, double jd)
	{
		// Lokale Felder einrichten
		double alpha = postion.Longitude;
		double delta = postion.Latitude;
		MEphemerides.ApparentPosition(ref alpha, ref delta, jd);
		return new CPolar(alpha, delta, 1.0);
	}

	// MEphemerides.ApparentPosition(ref double, ref double)
	/// <summary>
	/// Setzt die scheinbare Position der äquatorialen Position zur aktuellen Systemzeit.
	/// </summary>
	/// <param name="alpha">Rektaszension.</param>
	/// <param name="delta">Deklination.</param>
	public static void ApparentPosition(ref double alpha, ref double delta)
	{
		// Lokalen Felder einrichten und Position wandeln
		double jd = DateTime.Now.ToJdn();
		MEphemerides.ApparentPosition(ref alpha, ref delta, jd);
	}

	// MEphemerides.ApparentPosition(ref double, ref double, double)
	/// <summary>
	/// Setzt die scheinbare Position der äquatorialen Position zur julianischen Tageszahl.
	/// </summary>
	/// <param name="alpha">Rektaszension.</param>
	/// <param name="delta">Deklination.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	public static void ApparentPosition(ref double alpha, ref double delta, double jd)
	{
		// TODO: MEphemerides.ApparentPosition(ref double, ref double, double): Implemenation vervollständigen.
		throw new NotImplementedException("Methode ist nicht implementiert.");
	}

	// MEphemerides.EquationOfTime()
	/// <summary>
	/// Liefert die Zeitdifferenz zwischen mittleren und scheinbaren Sonnentransit zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Zeitdifferenz zwischen mittleren und scheinbaren Sonnentransit zur aktuellen Systemzeit.</returns>
	public static double EquationOfTime()
	{
		// Lokalen Felder einrichten und Zeitdifferenz berechnen
		double jd = DateTime.Now.ToJdn();
		return MEphemerides.EquationOfTime(jd);
	}

	// MEphemerides.EquationOfTime(double)
	/// <summary>
	/// Liefert die Zeitdifferenz zwischen mittleren und scheinbaren Sonnentransit zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Zeitdifferenz zwischen mittleren und scheinbaren Sonnentransit zur julianischen Tageszahl.</returns>
	public static double EquationOfTime(double jd)
	{
		// Lokalen Felder einrichten
		double l = MSun.Longitude(EPrecision.High, jd);
		double b = MSun.Latitude (EPrecision.High, jd);
		double a = MEphemerides.ToAlpha(l, b, EObliquity.Mean, jd);

		// Hilfsfelder einrichten
		double t  = (jd - MCalendar.Jdn20000101) / 365250.0;
		double l0 = MMod.Mod(MMath.ToRad(MMath.Polynome(t, 280.4664567, 360007.6982779, 0.03032028, 1/49931, -1/15300, -1/2000000)), MMath.Pi2);
		if(l0 < 0.0) l0 += MMath.Pi2;

		// Werte für Nutation berechnen
		double p   = MEphemerides.NutationInLongitude(jd);
		double eps = MEphemerides.ObliquityTrue(jd);
		double e   = MMod.Mod(l0 - 0.000099803 - a + p * MMath.Cos(eps), MMath.Pi2);

		// Winkel normalisieren
		if(e < 0.0) e += MMath.Pi2;
		return e;
	}

	// MEphemerides.Gast()
	/// <summary>
	/// Liefert die scheinbare Sternzeit zum Nullmeridian und zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Scheinbare Sternzeit zum Nullmeridian und zur aktuellen Systemzeit.</returns>
	public static double Gast()
	{
		// Lokalen Felder einrichten und Sternzeit berechnen
		double jd = DateTime.Now.ToJdn();
		return MEphemerides.Gast(jd);
	}

	// MEphemerides.Gast(double)
	/// <summary>
	/// Liefert die scheinbare Sternzeit zum Nullmeridian und zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Scheinbare Sternzeit zum Nullmeridian und zur julianischen Tageszahl.</returns>
	public static double Gast(double jd)
	{
		// Lokalen Felder einrichten
		double gmst = MEphemerides.Gmst(jd);
		double dPsi = MEphemerides.NutationInLongitude(jd);
		double cosE = MMath.Cos(MEphemerides.ObliquityTrue(jd));
		
		// Sternzeit berechnen
		return gmst + dPsi * cosE;
	}

	// MEphemerides.Gmst()
	/// <summary>
	/// Liefert die mittlere Sternzeit zum Nullmeridian und zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Mittlere Sternzeit zum Nullmeridian und zur aktuellen Systemzeit.</returns>
	public static double Gmst()
	{
		// Lokalen Felder einrichten und Sternzeit berechnen
		double jd = DateTime.Now.ToJdn();
		return MEphemerides.Gmst(jd);
	}

	// MEphemerides.Gmst(double)
	/// <summary>
	/// Liefert die mittlere Sternzeit zum Nullmeridian und zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianischen Tageszahl.</param>
	/// <returns>Mittlere Sternzeit zum Nullmeridian und zur julianischen Tageszahl.</returns>
	public static double Gmst(double jd)
	{
		// Lokalen Felder einrichten
		double jd0 = MMath.Floor(jd + 0.5) + 0.5;
		double t0  = MCalendar.CenturyFragment(jd0);
		double th0 = 360.0 * MMath.Polynome(t0, 24110.548410, 8640184.812866, 0.093104, -0.000006) / MCalendar.SecondsPerDay;

		// Sternzeit berechnen
		return MMath.ToRad(MMod.Mod(th0 + 1.00273790935 * 360.0 * (jd - jd0), 360.0));
	}

	// MEphemerides.HeightOfEcliptic(CPolar)
	/// <summary>
	/// Liefert den Winkel zwischen Ekliptik und Horizont zum geographischen Ort und aktueller Systemzeit.
	/// </summary>
	/// <param name="position">Geographischer Ort.</param>
	/// <returns>Winkel zwischen Ekliptik und Horizont zum geographischen Ort und aktueller Systemzeit.</returns>
	public static double HeightOfEcliptic(CPolar position)
	{
		// Lokalen Felder einrichten und Winkel berechnen
		double jd = DateTime.Now.ToJdn();
		return MEphemerides.HeightOfEcliptic(position.Longitude, position.Latitude, jd);
	}

	// MEphemerides.HeightOfEcliptic(double, double)
	/// <summary>
	/// Liefert den Winkel zwischen Ekliptik und Horizont zum geographischen Ort und aktueller Systemzeit.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Länge.</param>
	/// <returns>Winkel zwischen Ekliptik und Horizont zum geographischen Ort und aktueller Systemzeit.</returns>
	public static double HeightOfEcliptic(double lambda, double phi)
	{
		// Lokalen Felder einrichten und Winkel berechnen
		double jd = DateTime.Now.ToJdn();
		return MEphemerides.HeightOfEcliptic(lambda, phi, jd);
	}

	// MEphemerides.HeightOfEcliptic(CPolar, double)
	/// <summary>
	/// Liefert den Winkel zwischen Ekliptik und Horizont zum geographischen Ort und julianischer Tageszahl.
	/// </summary>
	/// <param name="position">Geographischer Ort.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Winkel zwischen Ekliptik und Horizont zum geographischen Ort und julianischer Tageszahl.</returns>
	public static double HeightOfEcliptic(CPolar position, double jd){ return MEphemerides.HeightOfEcliptic(position.Longitude, position.Latitude, jd); }

	// MEphemerides.HeightOfEcliptic(double, double, double)
	/// <summary>
	/// Liefert den Winkel zwischen Ekliptik und Horizont zum geographischen Ort und julianischer Tageszahl.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Länge.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Winkel zwischen Ekliptik und Horizont zum geographischen Ort und julianischer Tageszahl.</returns>
	public static double HeightOfEcliptic(double lambda, double phi, double jd)
	{
		// Lokalen Felder einrichten
		double lha = MEphemerides.LocalHourAngle(0.0, lambda, jd);
		double eps = MEphemerides.ObliquityMean(jd);

		// Winkel berechnen
		return MMath.ArcCos(MMath.Cos(eps) * MMath.Sin(phi) - MMath.Sin(eps) * MMath.Cos(phi) * MMath.Sin(lha));
	}

	// MEphemerides.Last(double, double)
	/// <summary>
	/// Liefert die scheinbare Sternzeit zur julianischen Tageszahl und geographischer Länge.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="lambda">Geographische Breite.</param>
	/// <returns>Scheinbare Sternzeit zur julianischen Tageszahl und geographischer Länge.</returns>
	public static double Last(double jd, double lambda){ return MEphemerides.Gast(jd) + lambda; }

	// MEphemerides.Lmst(double, double)
	/// <summary>
	/// Liefert die mittlere Sternzeit zur julianischen Tageszahl und geographischer Länge.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="lambda">Geographische Breite.</param>
	/// <returns>Mittlere Sternzeit zur julianischen Tageszahl und geographischer Länge.</returns>
	public static double Lmst(double jd, double lambda){ return MEphemerides.Gmst(jd) + lambda; }

	// MEphemerides.LocalHourAngle(double)
	/// <summary>
	/// Liefert den lokalen Stundenwinkel zur Rektaszension, Nullmeridian und aktueller Systemzeit.
	/// </summary>
	/// <param name="alpha">Rektaszension.</param>
	/// <returns>Lokaler Stundenwinkel zur Rektaszension, Nullmeridian und aktueller Systemzeit.</returns>
	public static double LocalHourAngle(double alpha)
	{
		// Lokalen Felder einrichten und Stundenwinkel berechnen
		double jd = DateTime.Now.ToJdn();
		return MEphemerides.LocalHourAngle(alpha, 0.0, jd);
	}

	// MEphemerides.LocalHourAngle(double, double)
	/// <summary>
	/// Liefert den lokalen Stundenwinkel zur Rektaszension, geographischer Länge und aktueller Systemzeit.
	/// </summary>
	/// <param name="alpha">Rektaszension.</param>
	/// <param name="lambda">Geographische Länge.</param>
	/// <returns>Lokaler Stundenwinkel zur Rektaszension, geographischer Länge und aktueller Systemzeit.</returns>
	public static double LocalHourAngle(double alpha, double lambda)
	{
		// Lokalen Felder einrichten und Stundenwinkel berechnen
		double jd = DateTime.Now.ToJdn();
		return MEphemerides.LocalHourAngle(alpha, lambda, jd);
	}

	// MEphemerides.LocalHourAngle(double, double, double)
	/// <summary>
	/// Liefert den lokalen Stundenwinkel zur Rektaszension, geographischer Länge und julianischer Tageszahl.
	/// </summary>
	/// <param name="alpha">Rektaszension.</param>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Lokaler Stundenwinkel zur Rektaszension, geographischer Länge und julianischer Tageszahl.</returns>
	public static double LocalHourAngle(double alpha, double lambda, double jd)
	{
		// Lokalen Felder einrichten und Winkel berechnen
		double t   = MEphemerides.Gmst(jd);
		double lha = MMod.Mod(t - lambda - alpha, MMath.Pi2);

		// Winkel normalisieren
		if(lha < 0.0) lha += MMath.Pi2;
		return lha;
	}

	// MEphemerides.LongitudeOfEcliptic(CPolar)
	/// <summary>
	/// Liefert die ekliptikale Länge des Horizontdurchgangs zur geopraphischen Ort und aktueller Systemzeit.
	/// </summary>
	/// <param name="position">Geographischer Ort.</param>
	/// <returns>Ekliptikale Länge des Horizontdurchgangs zur geopraphischen Ort und aktueller Systemzeit.</returns>
	public static double LongitudeOfEcliptic(CPolar position)
	{
		// Lokale Felder einrichten und Länge berechnen
		double jd = DateTime.Now.ToJdn();
		return MEphemerides.LongitudeOfEcliptic(position.Longitude, position.Latitude, jd);
	}

	// MEphemerides.LongitudeOfEcliptic(CPolar, double)
	/// <summary>
	/// Liefert die ekliptikale Länge des Horizontdurchgangs zur geopraphischen Ort und julianischer Tageszahl.
	/// </summary>
	/// <param name="position">Geographischer Ort.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Ekliptikale Länge des Horizontdurchgangs zur geopraphischen Ort und julianischer Tageszahl.</returns>
	public static double LongitudeOfEcliptic(CPolar position, double jd) { return MEphemerides.LongitudeOfEcliptic(position.Longitude, position.Latitude, jd); }

	// MEphemerides.LongitudeOfEcliptic(double, double, double)
	/// <summary>
	/// Liefert die ekliptikale Länge des Horizontdurchgangs zur geopraphischen Ort und aktueller Systemzeit.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <returns>Ekliptikale Länge des Horizontdurchgangs zur geopraphischen Ort und aktueller Systemzeit</returns>
	public static double LongitudeOfEcliptic(double lambda, double phi)
	{
		// Lokalen Felder einrichten und Länge berechnen
		double jd = DateTime.Now.ToJdn();
		return MEphemerides.LongitudeOfEcliptic(lambda, phi, jd);
	}

	// MEphemerides.LongitudeOfEcliptic(double, double, double)
	/// <summary>
	/// Liefert die ekliptikale Länge des Horizontdurchgangs zur geopraphischen Ort und julianischer Tageszahl.
	/// </summary>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Ekliptikale Länge des Horizontdurchgangs zur geopraphischen Ort und julianischer Tageszahl.</returns>
	public static double LongitudeOfEcliptic(double lambda, double phi, double jd)
	{
		// Lokalen Felder einrichten
		double lha = MEphemerides.LocalHourAngle(0.0, phi, jd);
		double eps = MEphemerides.ObliquityMean(jd);

		// Winkel berechnen
		return MMath.ArcTan(-MMath.Cos(lha), MMath.Sin(eps) * MMath.Tan(phi) + MMath.Cos(eps) * MMath.Sin(lha));
	}

	// MEphemerides.NutationInLongitude()       --> MEphemerides.Nutation.cs
	// MEphemerides.NutationInLongitude(double) --> MEphemerides.Nutation.cs
	// MEphemerides.NutationInObliquity()       --> MEphemerides.Nutation.cs
	// MEphemerides.NutationInObliquity(double) --> MEphemerides.Nutation.cs

	// MEphemerides.ObliquityMean()
	/// <summary>
	/// Liefert die mittlere Ekliptikschiefe zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Mittlere Ekliptikschiefe zur aktuellen Systemzeit.</returns>
	public static double ObliquityMean()
	{
		// Lokalen Felder einrichten und Ekliptikschiefe berechnen
		double jd = DateTime.Now.ToJdn();
		return MEphemerides.ObliquityMean(jd);
	}

	// MEphemerides.ObliquityMean(double)
	/// <summary>
	/// Liefert die mittlere Ekliptikschiefe zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Mittlere Ekliptikschiefe zur julianischen Tageszahl.</returns>
	public static double ObliquityMean(double jd)
	{
		// Lokalen Felder einrichten und Ekliptikschiefe berechnen
		double t = (jd - MCalendar.Jdn20000101) / 36525.0;
		return MMath.ToRad(23.43929111 - (((46.815 + (0.00059 - 0.001813 * t) * t) * t) / 3600.0));
	}

	// MEphemerides.ObliquityTrue()
	/// <summary>
	/// Liefert die wahre Ekliptikschiefe zur aktuellen Systemzeit.
	/// </summary>
	/// <returns>Wahre Exkliptikschiefe zur aktuellen Systemzeit.</returns>
	public static double ObliquityTrue()
	{
		// Lokalen Felder einrichten und Ekliptikschiefe berechnen
		double jd = DateTime.Now.ToJdn();
		return MEphemerides.ObliquityTrue(jd);
	}

	// MEphemerides.ObliquityTrue(double)
	/// <summary>
	/// Liefert die wahre Ekliptikschiefe zur julianischen Tageszahl.
	/// </summary>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Wahre Exkliptikschiefe zur julianischen Tageszahl.</returns>
	public static double ObliquityTrue(double jd){ return MEphemerides.ObliquityMean(jd) + MEphemerides.NutationInObliquity(jd); }

	// MEphemerides.ParallacticAngle(CPolar, CPolar)
	/// <summary>
	/// Liefert den parallaktischen Winkel zur geozentrischen Position, zum geographischen Ort und aktueller Systemzeit.
	/// </summary>
	/// <param name="positionGeocentric">Geozentrische Position.</param>
	/// <param name="positionGeographic">Geographischer Ort.</param>
	/// <returns>Parallaktischer Winkel zur geozentrischen Position, zum geographischen Ort und aktueller Systemzeit.</returns>
	public static double ParallacticAngle(CPolar positionGeocentric, CPolar positionGeographic)
	{
		// Lokalen Felder einrichten und Winkel berechnen
		double jd = DateTime.Now.ToJdn();
		return MEphemerides.ParallacticAngle(positionGeocentric.Longitude, positionGeocentric.Latitude, positionGeographic.Longitude, positionGeographic.Latitude, jd);
	}

	// MEphemerides.ParallacticAngle(CPolar, CPolar, double)
	/// <summary>
	/// Liefert den parallaktischen Winkel zur geozentrischen Position, zum geographischen Ort und julianischer Tageszahl.
	/// </summary>
	/// <param name="positionGeocentric">Geozentrische Position.</param>
	/// <param name="positionGeographic">Geographische Ort.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Parallaktischer Winkel zur geozentrischen Position, zum geographischen Ort und julianischer Tageszahl.</returns>
	public static double ParallacticAngle(CPolar positionGeocentric, CPolar positionGeographic, double jd){ return MEphemerides.ParallacticAngle(positionGeocentric.Longitude, positionGeocentric.Latitude, positionGeographic.Longitude, positionGeographic.Latitude); }

	// MEphemerides.ParallacticAngle(double, double, double, double)
	/// <summary>
	/// Liefert den parallaktischen Winkel zur geozentrischen Position, zum geographischen Ort und aktueller Systemzeit.
	/// </summary>
	/// <param name="alpha">Rektaszension.</param>
	/// <param name="delta">Deklination.</param>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <returns>Parallaktischer Winkel zur geozentrischen Position, zum geographischen Ort und aktueller Systemzeit.</returns>
	public static double ParallacticAngle(double alpha, double delta, double lambda, double phi)
	{
		// Lokalen Felder einrichten und Winkel berechnen
		double jd = DateTime.Now.ToJdn();
		return MEphemerides.ParallacticAngle(alpha, delta, lambda, phi, jd);
	}

	// MEphemerides.ParallacticAngle(double, double, double, double, double)
	/// <summary>
	/// Liefert den parallaktischen Winkel zur geozentrischen Position, zum geographischen Ort und julianischer Tageszahl.
	/// </summary>
	/// <param name="alpha">Rektaszension.</param>
	/// <param name="delta">Deklination.</param>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <param name="jd">Julianische Tageszhal</param>
	/// <returns>Parallaktischer Winkel zur geozentrischen Position, zum geographischen Ort und julianischer Tageszahl.</returns>
	public static double ParallacticAngle(double alpha, double delta, double lambda, double phi, double jd)
	{
		// Lokalen Felder einrichten und Winkel berechnen
		double lha = MEphemerides.LocalHourAngle(alpha, lambda, jd);
		return MMath.ArcTan(MMath.Sin(lha), MMath.Tan(phi) * MMath.Cos(delta) - MMath.Sin(delta) * MMath.Cos(lha));
	}

	// MEphemerides.PrecessionEcliptical(CPolar)
	/// <summary>
	/// Liefert die um die Präzession korrigierte Position zur ekliptikalen Position und aktuellen Systemzeit.
	/// </summary>
	/// <param name="pos">Ekliptikale Position.</param>
	/// <returns>Um die Präzession korrigierte Position zur ekliptikalen Position und aktuellen Systemzeit.</returns>
	public static CPolar PrecessionEcliptical(CPolar position)
	{
		// Lokale Felder einrichten und Präzession anwenden
		double jd     = DateTime.Now.ToJdn();
		double lambda = position.Longitude;
		double beta   = position.Latitude;
		MEphemerides.PrecessionEcliptical(ref lambda, ref beta, jd);
		return new CPolar(lambda, beta, 1.0);
	}

	// MEphemerides.PrecessionEcliptical(CPolar, double)
	/// <summary>
	/// Liefert die um die Präzession korrigierte Position zur ekliptikalen Position und julianischer Tageszahl.
	/// </summary>
	/// <param name="position">Ekliptikale Position.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Um die Präzession korrigierte Position zur ekliptikalen Position und julianischer Tageszahl.</returns>
	public static CPolar PrecessionEcliptical(CPolar position, double jd)
	{
		// Lokale Felder einrichten und Präzession anwenden
		double lambda = position.Longitude;
		double beta   = position.Latitude;
		MEphemerides.PrecessionEcliptical(ref lambda, ref beta, jd);
		return new CPolar(lambda, beta, 1.0);
	}

	// MEphemerides.PrecessionEcliptical(ref double, ref double)
	/// <summary>
	/// Wendet die Präzession auf die ekliptikale Position zur aktueller Systemzeit an.
	/// </summary>
	/// <param name="lambda">Ekliptikale Länge.</param>
	/// <param name="beta">Ekliptikale Breite.</param>
	public static void PrecessionEcliptical(ref double lambda, ref double beta)
	{
		// Lokalen Felder einrichten und Präzession anwenden
		double jd = DateTime.Now.ToJdn();
		MEphemerides.PrecessionEcliptical(ref lambda, ref beta, jd);
	}

	// MEphemerides.PrecessionEcliptical(ref double, ref double, double)
	/// <summary>
	/// Wendet die Präzession auf die ekliptikale Position zur julianischen Tageszahl an.
	/// </summary>
	/// <param name="lambda">Ekliptikale Länge.</param>
	/// <param name="beta">Ekliptikale Breite.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	public static void PrecessionEcliptical(ref double lambda, ref double beta, double jd)
	{
		// Lokalen Felder einrichten
		double t = MCalendar.CenturyFragment(jd);
		double x = MMath.ToRad(MMath.Polynome(t,   0.0,   47.0029, -0.03302,  0.000060) / 3600.0);
		double y = MMath.ToRad(MMath.Polynome(t, 174.876384, -869.8089 / 3600.0,  0.03536 / 3600.0));
		double z = MMath.ToRad(MMath.Polynome(t,   0.0, 5029.0966,  1.11113, -0.000006) / 3600.0);

		// Lokalen Hilsfelder einrichten
		// TODO: MEphemerides.PrecessionEcliptical(ref double, ref double, double): Verwendung der Winkelwandelungsfunktionen prüfen.
		double cosL = MMath.Cos(y - lambda);
		double sinL = MMath.Sin(y - lambda);
		double cosB = MMath.Cos(beta);
		double sinB = MMath.Sin(beta);
		double cosX = MMath.Cos(x);
		double sinX = MMath.Sin(x);

		// Bestimmung der Drehmatrix
		double a = cosX * cosB * sinL - sinX * sinB;
		double b = cosB * cosL;
		double c = cosX * sinB + sinX * cosB * sinL;

		// Position korrigieren
		lambda = z + y - MMath.ArcTan(a, b);
		beta   = MMath.ArcSin(c);
	}

	// MEphemerides.PrecessionEquatorial(CPolar)
	/// <summary>
	/// Liefert die um die Präzession korrigierte Position zur äquatorialen Position und aktuellen Systemzeit.
	/// </summary>
	/// <param name="position">Äquatorialen Position.</param>
	/// <returns>Um die Präzession korrigierte Position zur äquatorialen Position und aktuellen Systemzeit.</returns>
	public static CPolar PrecessionEquatorial(CPolar position)
	{
		// Lokale Felder einrichten und Präzession anwenden
		double jd    = DateTime.Now.ToJdn();
		double alpha = position.Longitude;
		double delta = position.Latitude;
		MEphemerides.PrecessionEquatorial(ref alpha, ref delta, jd);
		return new CPolar(alpha, delta, 1.0);
	}

	// MEphemerides.PrecessionEquatorial(CPolar, double)
	/// <summary>
	/// Liefert die um die Präzession korrigierte Position zur äquatorialen Position und julianischer Tageszahl.
	/// </summary>
	/// <param name="position">Äquatorialen Position.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Um die Präzession korrigierte Position zur äquatorialen Position und julianischer Tageszahl.</returns>
	public static CPolar PrecessionEquatorial(CPolar position, double jd)
	{
		// Lokale Felder einrichten und Präzession anwenden
		double alpha = position.Longitude;
		double delta = position.Latitude;
		MEphemerides.PrecessionEquatorial(ref alpha, ref delta, jd);
		return new CPolar(alpha, delta, 1.0);
	}

	// MEphemerides.PrecessionEquatorial(ref double, ref double)
	/// <summary>
	/// Wendet die Präzession auf die äquatoriale Position zur aktuellen Systemzeit an.
	/// </summary>
	/// <param name="alpha">Rektaszension.</param>
	/// <param name="delta">Deklination.</param>
	public static void PrecessionEquatorial(ref double alpha, ref double delta)
	{
		// Lokalen Felder einrichten und Präzession anwenden
		double jd = DateTime.Now.ToJdn();
		MEphemerides.PrecessionEquatorial(ref alpha, ref delta, jd);
	}

	// MEphemerides.PrecessionEquatorial(ref double, ref double, double)
	/// <summary>
	/// Wendet die Präzession auf die äquatoriale Position zur julianischen Tageszahl an.
	/// </summary>
	/// <param name="alpha">Rektaszension.</param>
	/// <param name="delta">Deklination.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	public static void PrecessionEquatorial(ref double alpha, ref double delta, double jd)
	{
		// Lokalen Felder einrichten
		double t = MCalendar.CenturyFragment(jd);
		double x = MMath.ToRad(MMath.Polynome(t, 2306.2181,  0.30188,  0.017998) / 3600.0);
		double y = MMath.ToRad(MMath.Polynome(t, 2306.2181,  1.09468,  0.018203) / 3600.0);
		double z = MMath.ToRad(MMath.Polynome(t, 2004.3109, -0.42665, -0.041833) / 3600.0);

		// Lokalen Hilfsfelder einrichten
		// TODO: MEphemerides.PrecessionEquatorial(ref double, ref double, double): Verwendung der Winkelwandelungsfunktionen prüfen.
		double cosA = MMath.Cos(alpha + x);
		double sinA = MMath.Sin(alpha + x);
		double cosD = MMath.Cos(delta);
		double sinD = MMath.Sin(delta);
		double cosZ = MMath.Cos(z);
		double sinZ = MMath.Sin(z);

		// Bestimmung der Drehmatrix
		double a = cosD * sinA;
		double b = cosZ * cosD * cosA - sinZ * sinD;
		double c = sinZ * cosD * cosA + cosZ * sinD;

		// Position korrigieren
		alpha = MMath.ArcTan(a, b) + y;
		delta = MMath.ArcSin(c);
	}

	// MEphemerides.ProperMotion(CPolar, double, double)
	/// <summary>
	/// Liefert die um die Eigenbewegung korrigierte Position zur äquatorialen Position und aktueller Systemzeit.
	/// </summary>
	/// <param name="position">Äquatoriale Position.</param>
	/// <param name="muAlpha">Eigenbewegung in Rektaszension.</param>
	/// <param name="muDelta">Eigenbewegung in Deklination.</param>
	/// <returns>Um die Eigenbewegung korrigierte Position zur äquatorialen Position und aktueller Systemzeit.</returns>
	public static CPolar ProperMotion(CPolar position, double muAlpha, double muDelta)
	{
		// Lokale Felder einrichten und Eigenbewegung anwenden
		double jd    = DateTime.Now.ToJdn();
		double alpha = position.Longitude;
		double delta = position.Latitude;
		MEphemerides.ProperMotion(ref alpha, ref delta, muAlpha, muDelta, jd);
		return new CPolar(alpha, delta, 1.0);
	}

	// MEphemerides.ProperMotion(CPolar, double, double, double)
	/// <summary>
	/// Liefert die um die Eigenbewegung korrigierte Position zur äquatorialen Position und julianischer Tageszahl.
	/// </summary>
	/// <param name="position">Äquatoriale Position.</param>
	/// <param name="muAlpha">Eigenbewegung in Rektaszension.</param>
	/// <param name="muDelta">Eigenbewegung in Deklination.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Um die Eigenbewegung korrigierte Position zur äquatorialen Position und julianischer Tageszahl.</returns>
	public static CPolar ProperMotion(CPolar position, double muAlpha, double muDelta, double jd)
	{
		// Lokale Felder einrichten und Eigenbewegung anwenden
		double alpha = position.Longitude;
		double delta = position.Latitude;
		MEphemerides.ProperMotion(ref alpha, ref delta, muAlpha, muDelta, jd);
		return new CPolar(alpha, delta, 1.0);
	}

	// MEphemerides.ProperMotion(CPolar, double, double, double, double, double)
	/// <summary>
	/// Liefert die um die Eigenbewegung korrigierte Position zur äquatorialen Position und julianischer Tageszahl.
	/// </summary>
	/// <param name="position">Äquatoriale Position.</param>
	/// <param name="muAlpha">Eigenbewegung in Rektaszension.</param>
	/// <param name="muDelta">Eigenbewegung in Deklination.</param>
	/// <param name="distance">Entfernung [pc].</param>
	/// <param name="radialVelocity">Radialgeschwindigkeit.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Um die Eigenbewegung korrigierte Position zur äquatorialen Position und julianischer Tageszahl.</returns>
	public static CPolar ProperMotion(CPolar position, double muAlpha, double muDelta, double distance, double radialVelocity, double jd)
	{
		// Lokale Felder einrichten und Eigenbewegung anwenden
		double alpha = position.Longitude;
		double delta = position.Latitude;
		MEphemerides.ProperMotion(ref alpha, ref delta, muAlpha, muDelta, distance, radialVelocity, jd);
		return new CPolar(alpha, delta, 1.0);
	}

	// MEphemerides.ProperMotion(ref double, ref double, double, double)
	/// <summary>
	/// Wendet die Eigenbewegung auf die äquatoriale Position zur aktuellen Systemzeit an.
	/// </summary>
	/// <param name="alpha">Rektaszension.</param>
	/// <param name="delta">Deklination.</param>
	/// <param name="muAlpha">Eigenbewegung in Rektaszention.</param>
	/// <param name="muDelta">Eigenbewegung in Deklination.</param>
	public static void ProperMotion(ref double alpha, ref double delta, double muAlpha, double muDelta)
	{
		// Lokalen Felder einrichten und Eigenbewegung anwenden
		double jd = DateTime.Now.ToJdn();
		MEphemerides.ProperMotion(ref alpha, ref delta, muAlpha, muDelta, jd);
	}

	// MEphemerides.ProperMotion(ref double, ref double, double, double, double)
	/// <summary>
	/// Wendet die Eigenbewegung auf die äquatoriale Position zur julianischen Tageszahl an.
	/// </summary>
	/// <param name="alpha">Rektaszension.</param>
	/// <param name="delta">Deklination.</param>
	/// <param name="muAlpha">Eigenbewegung in Rektaszention.</param>
	/// <param name="muDelta">Eigenbewegung in Deklination.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	public static void ProperMotion(ref double alpha, ref double delta, double muAlpha, double muDelta, double jd)
	{
		// Lokalen Felder und Eigenbewegung anwenden
		double t = (jd - MCalendar.Jdn20000101) / 365.25;
		alpha += t * MMath.ToRad(15.0 * muAlpha / 3600.0);
		delta += t * MMath.ToRad(muDelta / 3600.0);
	}

	// MEphemerides.ProperMotion(ref double, ref double, double, double, double, double)
	/// <summary>
	/// Wendet die Eigenbewegung auf die äquatoriale Position zur julianischen Tageszahl an.
	/// </summary>
	/// <param name="alpha">Rektaszenion.</param>
	/// <param name="delta">Deklination.</param>
	/// <param name="muAlpha">Eigenbewegung in Rektaszension.</param>
	/// <param name="muDelta">Eigenbewegung in Deklination.</param>
	/// <param name="distance">Entfernung [pc].</param>
	/// <param name="radialVelocity">Radialgeschwindigkeit.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	public static void ProperMotion(ref double alpha, ref double delta, double muAlpha, double muDelta, double distance, double radialVelocity, double jd)
	{
		// TODO: MEphemerides.ProperMotion(ref double, ref double, double, double, double, double): Implemenation vervollständigen
		throw new NotImplementedException("Methode ist nicht implementiert.");
	}

	// MEphemerides.Rise(CPolar, CPolar, ref double, double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Aufgangs der äquatorialen Position am geographischen Ort und nach der julianischen Tageszahl und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="positionEquatorial">Äquatoriale Position.</param>
	/// <param name="positionGeographic">Geographischer Ort.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Ereignisses.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Rise(CPolar positionEquatorial, CPolar positionGeographic, ref double jdEvent, double jd)
	{
		// Lokale Felder einrichten und Ereigniskennung bestimmen
		double azimuth = 0.0;
		return MEphemerides.Rise(positionEquatorial.Longitude, positionEquatorial.Latitude, positionGeographic.Longitude, positionGeographic.Latitude, ref jdEvent, jd, ref azimuth);
	}

	// MEphemerides.Rise(CPolar, CPolar, ref double, double, ref double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Aufgangs und die Morgenweite der äquatorialen Position am geographischen Ort und nach der julianischen Tageszahl und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="positionEquatorial">Äquatoriale Position.</param>
	/// <param name="positionGeographic">Geographischer Ort.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Ereignisses.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="azimuth">Morgenweite.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Rise(CPolar positionEquatorial, CPolar positionGeographic, ref double jdEvent, double jd, ref double azimuth){ return MEphemerides.Rise(positionEquatorial.Longitude, positionEquatorial.Latitude, positionGeographic.Longitude, positionGeographic.Latitude, ref jdEvent, jd, ref azimuth); }

	// MEphemerides.Rise(double, double, double, double, ref double, double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Aufgangs der äquatorialen Position am geographischen Ort und nach der julianischen Tageszahl und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="alpha">Rektaszension.</param>
	/// <param name="delta">Deklination.</param>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Ereignisses.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Rise(double alpha, double delta, double lambda, double phi, ref double jdEvent, double jd)
	{
		// Lokale Felder einrichten und Ereigniskennung bestimmen
		double azimuth = 0.0;
		return MEphemerides.Rise(alpha, delta, lambda, phi, ref jdEvent, jd, ref azimuth);
	}

	// MEphemerides.Rise(double, double, double, double, ref double, double, ref double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Aufgangs und die Morgenweite der äquatorialen Position am geographischen Ort und nach der julianischen Tageszahl und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="alpha">Rektaszension.</param>
	/// <param name="delta">Deklination.</param>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Ereignisses.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="azimuth">Morgenweite.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Rise(double alpha, double delta, double lambda, double phi, ref double jdEvent, double jd, ref double azimuth)
	{
		// TODO: MEphemerides.Rise(double, double, double, double, ref double, double, ref double): Implementation vervollständigen.
		throw new NotImplementedException("Methode ist nicht implementiert.");
	}

	// MEphemerides.SemidialArc(double, double)
	/// <summary>
	/// Liefert den halben Tagbogen der Deklination zur geographischen Breite.
	/// </summary>
	/// <param name="delta">Deklination.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <returns>Halber Tagbogen der Deklination zur geographischen Breite.</returns>
	public static double SemidialArc(double delta, double phi)
	{
		// Lokalen Felder einrichten
		double x = MMath.Sin(delta) * MMath.Sin(phi);
		double y = MMath.Cos(delta) * MMath.Cos(phi);

		// Tagesbogen berechnen
		if(y == 0.0) return 0.0;
		return 0.997269571 * MMath.ArcCos(x / y);
	}

	// MEphemerides.Set(CPolar, CPolar, ref double, double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Untergangs der äquatorialen Position am geographischen Ort und zur julianischen Tageszahl und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="positionEquatorial">Äquatoriale Position.</param>
	/// <param name="positionGeographic">Geographischer Ort.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Ereignisses.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Set(CPolar positionEquatorial, CPolar positionGeographic, ref double jdEvent, double jd)
	{
		// Lokale Felder einrichten und Ereigniskennung bestimmen
		double azimuth = 0.0;
		return MEphemerides.Set(positionEquatorial.Longitude, positionEquatorial.Latitude, positionGeographic.Longitude, positionGeographic.Latitude, ref jdEvent, jd, ref azimuth);
	}

	// MEphemerides.Set(CPolar, CPolar, ref double, double, ref double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Untergangs und die Abendweite der äquatorialen Position am geographischen Ort und zur julianischen Tageszahl und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="positionEquatorial">Äquatoriale Position.</param>
	/// <param name="positionGeographic">Geographischer Ort.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Ereignisses.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="azimuth">Abendweite.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Set(CPolar positionEquatorial, CPolar positionGeographic, ref double jdEvent, double jd, ref double azimuth){ return MEphemerides.Set(positionEquatorial.Longitude, positionEquatorial.Latitude, positionGeographic.Longitude, positionGeographic.Latitude, ref jdEvent, jd, ref azimuth); }

	// MEphemerides.Set(double, double, double, double, ref double, double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Untergangs der äquatorialen Position am geographischen Ort und zur julianischen Tageszahl und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="alpha">Rektaszension.</param>
	/// <param name="delta">Deklination.</param>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Ereignisses.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Set(double alpha, double delta, double lambda, double phi, ref double jdEvent, double jd)
	{
		// Lokale Felder einrichten und Ereigniskennung bestimmen
		double azimuth = 0.0;
		return MEphemerides.Set(alpha, delta, lambda, phi, ref jdEvent, jd, ref azimuth);
	}

	// MEphemerides.Set(double, double, double, double, ref double, double, ref double)
	/// <summary>
	/// Setzt die julianische Tageszahl des Untergangs und die Abendweite der äquatorialen Position am geographischen Ort und zur julianischen Tageszahl und liefert die Ereigniskennung.
	/// </summary>
	/// <param name="alpha">Rektaszension.</param>
	/// <param name="delta">Deklination.</param>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <param name="jdEvent">Julianische Tageszahl des Ereignisses.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="azimuth">Abendweite.</param>
	/// <returns>Ereigniskennung.</returns>
	public static EEventType Set(double alpha, double delta, double lambda, double phi, ref double jdEvent, double jd, ref double azimuth)
	{
		// TODO: MEphemerides.Set(double, double, double, double, ref double, double, ref double): Implementation vervollständigen.
		throw new NotImplementedException("Methode ist nicht implementiert.");
	}

	// MEphemerides.ToAlpha(CPolar, EObliquity)
	/// <summary>
	/// Liefert die (mittlere bzw. scheinbare) Rektaszension der geozentrisch-ekliptikalen Position zum Äquinoktium J2000.
	/// </summary>
	/// <param name="position">Ekliptikale Position.</param>
	/// <param name="obliquity">Kennung der Ekliptikschiefe.</param>
	/// <returns>(Mittlere bzw. scheinbare) Rektaszension der geozentrisch-ekliptikalen Position zum Äquinoktium J2000.</returns>
	public static double ToAlpha(CPolar position, EObliquity obliquity)
	{
		// Lokale Felder einrichten und Rektaszension berechnen
		double jd = MCalendar.Jdn20000101;
		return MEphemerides.ToAlpha(position.Longitude, position.Latitude, obliquity, jd);
	}

	// MEpemerides.ToAlpha(CPolar, EObliquity, double)
	/// <summary>
	/// Liefert die (mittlere bzw. scheinbare) Rektaszension der geozentrisch-ekliptikalen Position zum Äquinoktium der julianischen Tageszahl.
	/// </summary>
	/// <param name="position">Ekliptikale Position.</param>
	/// <param name="obliquity">Kennung der Ekliptikschiefe.</param>
	/// <param name="jd">Julianische Tageszahl zum Äquinoktium.</param>
	/// <returns>(Mittlere bzw. scheinbare) Rektaszension der geozentrisch-ekliptikalen Position zum Äquinoktium der julianischen Tageszahl.</returns>
	public static double ToAlpha(CPolar position, EObliquity obliquity, double jd){ return MEphemerides.ToAlpha(position.Longitude, position.Latitude, obliquity, jd); }

	// MEphemerides.ToAlpha(double, double, EObliquity)
	/// <summary>
	/// Liefert die (mittlere bzw. scheinbare) Rektaszension der geozentrisch-ekliptikalen Position zum Äquinoktium J2000.
	/// </summary>
	/// <param name="lambda">Ekliptikale Länge.</param>
	/// <param name="beta">Ekliptikale Breite.</param>
	/// <param name="obliquity">Kennung der Ekliptikschiefe.</param>
	/// <returns>(Mittlere bzw. scheinbare) Rektaszension der geozentrisch-ekliptikalen Position zum Äquinoktium J2000.</returns>
	public static double ToAlpha(double lambda, double beta, EObliquity obliquity)
	{
		// Lokale Felder einrichten und Rektaszension berechnen
		double jd = MCalendar.Jdn20000101;
		return MEphemerides.ToAlpha(lambda, beta, obliquity, jd);
	}

	// MEphemerides.ToAlpha(double, double, EObliquity, double)
	/// <summary>
	/// Liefert die (mittlere bzw. scheinbare) Rektaszension der geozentrisch-ekliptikalen Position zum Äquinoktium der julianischen Tageszahl.
	/// </summary>
	/// <param name="lambda">Ekliptikale Länge.</param>
	/// <param name="beta">Ekliptikale Breite.</param>
	/// <param name="obliquity">Kennung der Ekliptikschiefe.</param>
	/// <param name="jd">Julianische Tageszahl zum Äquinoktium.</param>
	/// <returns>(Mittlere bzw. scheinbare) Rektaszension der geozentrisch-ekliptikalen Position zum Äquinoktium der julianischen Tageszahl.</returns>
	public static double ToAlpha(double lambda, double beta, EObliquity obliquity, double jd)
	{
		// Sonderfälle abfangen
		if(lambda == MMath.Rad090) return MMath.Rad090;
		if(lambda == MMath.Rad270) return MMath.Rad270;

		// Lokalen Felder einrichten
		double eps  = 0.409092804222;
		if(jd == MCalendar.Jdn20000101) eps = obliquity == EObliquity.Mean ? eps : eps + MEphemerides.NutationInObliquity(jd);
		else eps = obliquity == EObliquity.Mean ? MEphemerides.ObliquityMean(jd) : MEphemerides.ObliquityTrue(jd);

		// Rektaszension berechnen
		double t = MMath.ArcTan(MMath.Sin(lambda) * MMath.Cos(eps) - MMath.Tan(beta) * MMath.Sin(eps), MMath.Cos(lambda));
		if(t < 0.0) t += MMath.Pi2;
		return t;
	}

	// MEphemerides.ToAzimuth(double, double, double)
	/// <summary>
	/// Liefert den horizontalen Azimut der geozentrisch-äquatorialen Position zur geographischen Breite.
	/// </summary>
	/// <param name="localHourAngle">Lokaler Stundenwinkel.</param>
	/// <param name="delta">Deklination.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <returns>Horizontaler Azimut der geozentrisch-äquatorialen Position zur geographischen Breite.</returns>
	public static double ToAzimuth(double localHourAngle, double delta, double phi)
	{
		// Azimut berechnen
		double h = MMath.Cos(localHourAngle) * MMath.Sin(phi) - MMath.Tan(delta) * MMath.Cos(phi);

		// Sonderfälle abfangen
		if(h == MMath.Rad090) return h;
		if(h == MMath.Rad270) return h;

		// Horizontalen Azimut berechnen
		double t = MMath.ArcTan(MMath.Sin(localHourAngle), h) - MMath.Pi;
		if(t < 0.0) t += MMath.Pi2;
		return t;
	}

	// MEphemerides.ToBeta(CPolar)
	/// <summary>
	/// Liefert die ekliptikale Breite zur (scheinbaren) geozentrisch-äquatorialen Position und zum Äquinoktium J2000.
	/// </summary>
	/// <param name="position">Äquatoriale Position.</param>
	/// <returns>Ekliptikale Breite zur (scheinbaren) geozentrisch-äquatorialen Position und zum Äquinoktium J2000.</returns>
	public static double ToBeta(CPolar position)
	{
		// Lokale Felder einrichten und Breite berechnen
		double jd = MCalendar.Jdn20000101;
		return MEphemerides.ToBeta(position.Longitude, position.Latitude, jd);
	}

	// MEphemerides.ToBeta(CPolar, double)
	/// <summary>
	/// Liefert die ekliptikale Breite zur (scheinbaren) geozentrisch-äquatorialen Position und zum Äquinoktium der julianischen Tageszahl.
	/// </summary>
	/// <param name="position">Äquatoriale Position.</param>
	/// <param name="jd">Julianische Tageszahl des Äquinoktiums.</param>
	/// <returns>Ekliptikale Breite zur (scheinbaren) geozentrisch-äquatorialen Position und zum Äquinoktium der julianischen Tageszahl.</returns>
	public static double ToBeta(CPolar position, double jd){ return MEphemerides.ToBeta(position.Longitude, position.Latitude, jd); }

	// MEphemerides.ToBeta(double, double)
	/// <summary>
	/// Liefert die ekliptikale Breite zur (scheinbaren) geozentrisch-äquatorialen Position und zum Äquinoktium J2000.
	/// </summary>
	/// <param name="alpha">Rektaszension.</param>
	/// <param name="delta">Deklination.</param>
	/// <returns>Ekliptikale Breite zur (scheinbaren) geozentrisch-äquatorialen Position und zum Äquinoktium J2000.</returns>
	public static double ToBeta(double alpha, double delta)
	{
		// Lokalen Felder einrichten und Breite berechnen
		double jd = DateTime.Now.ToJdn();
		return MEphemerides.ToBeta(alpha, delta, jd);
	}

	// MEphemerides.ToBeta(double, double, double)
	/// <summary>
	/// Liefert die ekliptikale Breite zur (scheinbaren) geozentrisch-äquatorialen Position und zum Äquinoktium der julianischen Tageszahl.
	/// </summary>
	/// <param name="alpha">Rektaszension.</param>
	/// <param name="delta">Deklination.</param>
	/// <param name="jd">Julianische Tageszahl des Äquinoktiums.</param>
	/// <returns>Ekliptikale Breite zur (scheinbaren) geozentrisch-äquatorialen Position und zum Äquinoktium der julianischen Tageszahl.</returns>
	public static double ToBeta(double alpha, double delta, double jd)
	{
		// Lokalen Felder einrichten
		double eps = 0.0;
		if(jd == MCalendar.Jdn20000101) eps = 0.409092804222;
		else eps = MEphemerides.ObliquityTrue(jd);

		// Breite berechnen
		return MMath.ArcSin(MMath.Sin(delta) * MMath.Cos(eps) - MMath.Cos(delta) * MMath.Sin(eps) * MMath.Sin(alpha));
	}

	// MEphemerides.ToDelta(CPolar, EObliquity)
	/// <summary>
	/// Liefert die (mittlere bzw. scheinbare) Deklination der geozentrisch-ekliptikalen Position zum Äquinoktium J2000.
	/// </summary>
	/// <param name="position">Ekliptikale Position.</param>
	/// <param name="obliquity">Kennung der Ekliptikschiefe.</param>
	/// <returns>(Mittlere bzw. scheinbare) Deklination der geozentrisch-ekliptikalen Position zum Äquinoktium J2000.</returns>
	public static double ToDelta(CPolar position, EObliquity obliquity)
	{
		// Lokale Felder einrichten und Deklination berechnen
		double jd = MCalendar.Jdn20000101;
		return MEphemerides.ToDelta(position.Longitude, position.Latitude, obliquity, jd);
	}

	// MEpemerides.ToDelta(CPolar, EObliquity, double)
	/// <summary>
	/// Liefert die (mittlere bzw. scheinbare) Deklination der geozentrisch-ekliptikalen Position zum Äquinoktium der julianischen Tageszahl.
	/// </summary>
	/// <param name="position">Ekliptikale Position.</param>
	/// <param name="obliquity">Kennung der Ekliptikschiefe.</param>
	/// <param name="jd">Julianische Tageszahl zum Äquinoktium.</param>
	/// <returns>(Mittlere bzw. scheinbare) Deklination der geozentrisch-ekliptikalen Position zum Äquinoktium der julianischen Tageszahl.</returns>
	public static double ToDelta(CPolar position, EObliquity obliquity, double jd){ return MEphemerides.ToDelta(position.Longitude, position.Latitude, obliquity, jd); } 

	// MEphemerides.ToDelta(double, double, EObliquity)
	/// <summary>
	/// Liefert die (mittlere bzw. scheinbare) Deklination der geozentrisch-ekliptikalen Position zum Äquinoktium J2000.
	/// </summary>
	/// <param name="lambda">Ekliptikale Länge.</param>
	/// <param name="beta">Ekliptikale Breite.</param>
	/// <param name="obliquity">Kennung der Ekliptikschiefe.</param>
	/// <returns>(Mittlere bzw. scheinbare) Deklination der geozentrisch-ekliptikalen Position zum Äquinoktium J2000.</returns>
	public static double ToDelta(double lambda, double beta, EObliquity obliquity)
	{
		// Lokale Felder einrichten und Deklination berechnen
		double jd = MCalendar.Jdn20000101;
		return MEphemerides.ToDelta(lambda, beta, obliquity, jd);
	}

	// MEphemerides.ToDelta(double, double, EObliquity, double)
	/// <summary>
	/// Liefert die (mittlere bzw. scheinbare) Deklination der geozentrisch-ekliptikalen Position zum Äquinoktium der julianischen Tageszahl.
	/// </summary>
	/// <param name="lambda">Ekliptikale Länge.</param>
	/// <param name="beta">Ekliptikale Breite.</param>
	/// <param name="obliquity">Kennung der Ekliptikschiefe.</param>
	/// <param name="jd">Julianische Tageszahl zum Äquinoktium.</param>
	/// <returns>(Mittlere bzw. scheinbare) Deklination der geozentrisch-ekliptikalen Position zum Äquinoktium der julianischen Tageszahl.</returns>
	public static double ToDelta(double lambda, double beta, EObliquity obliquity, double jd)
	{
		// Lokalen Felder einrichten
		double eps  = Obliquity_2000;
		if(jd == MCalendar.Jdn20000101) eps = obliquity == EObliquity.Mean ? eps : eps + MEphemerides.NutationInObliquity(jd);
		else eps = obliquity == EObliquity.Mean ? MEphemerides.ObliquityMean(jd) : MEphemerides.ObliquityTrue(jd);

		// Deklination berechnen
		double t = MMath.ArcSin(MMath.Sin(beta) * MMath.Cos(eps) + MMath.Cos(beta) * MMath.Sin(eps) * MMath.Sin(lambda));
		return t;
	}

	// MEphemerides.ToGeocentric(CPolar, double, EPrecision)
	/// <summary>
	/// Wandelt die heliozentrisch-ekliptikale Position in die geozentrisch-ekliptikalte Position zur julianischen Tageszahl und liefert den resultierenden Polar.
	/// </summary>
	/// <param name="position">Heliozentrisch-ekliptikale Position.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="precision">Genauigkeitskennung.</param>
	/// <returns>Resultierender Polar.</returns>
	public static CPolar ToGeocentric(CPolar position, double jd, EPrecision precision)
	{
		// Lokale Felder einrichten
		double l = 0.0;
		double b = 0.0;
		double r = 0.0;

		// Geozentrische Position bestimmen und wandeln
		MEphemerides.ToGeocentric(position.Longitude, position.Longitude, position.Radius, jd, ref l, ref b, ref r, precision);
		return new CPolar(l, b, r);
	}

	// MEphemerides.ToGeocenric(double, double, double, double, ref double, ref double, ref double, EPrecision)
	/// <summary>
	/// Setzt die geozentrisch-ekliptikale Position zur heliozentrisch-ekliptikalen Position und julianischer Tageszahl und liefert die Lichtlaufzeit.
	/// </summary>
	/// <param name="lambdaHeliocentric">Heliozentrische Länge.</param>
	/// <param name="betaHeliocentric">Heliozentrische Breite.</param>
	/// <param name="radiusHeliocentric">Heliozentrischer Radius.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="lambdaGeocentric">Geozentrische Länge.</param>
	/// <param name="betaGeocentric">Geozentrische Breite.</param>
	/// <param name="radiusGeocentric">Geozentrischer Radius.</param>
	/// <param name="precision">Genauigkeitskennung.</param>
	/// <returns>Lichtlaufzeit.</returns>
	public static double ToGeocentric(double lambdaHeliocentric, double betaHeliocentric, double radiusHeliocentric, double jd, ref double lambdaGeocentric, ref double betaGeocentric, ref double radiusGeocentric, EPrecision precision)
	{
		// Position der Erde berechnen
		double l = MEarth.Longitude(precision, jd);
		double b = MEarth.Latitude (precision, jd);
		double r = MEarth.Radius   (precision, jd);

		// Kartesische Koordinaten berechnen
		double x = radiusHeliocentric * MMath.Cos(betaHeliocentric) * MMath.Cos(lambdaHeliocentric) - r * MMath.Cos(b) * MMath.Cos(l);
		double y = radiusHeliocentric * MMath.Cos(betaHeliocentric) * MMath.Sin(lambdaHeliocentric) - r * MMath.Cos(b) * MMath.Sin(l);
		double z = radiusHeliocentric * MMath.Sin(betaHeliocentric)                                 - r * MMath.Sin(b)               ;

		// Polarkoordianten und Lichtlaufzeit bestimmen
		lambdaGeocentric = MMath.ArcTan(y, x);
		betaGeocentric   = MMath.ArcTan(z, MMath.Sqr(x * x + y * y));
		radiusGeocentric = MMath.Sqr(x * x + y * y + z * z);
		return 0.0057755183 * radiusGeocentric;
	}

	// MEphemerides.ToHeight(double, double, double)
	/// <summary>
	/// Liefert die horizontale Höhe der geozentrisch-äquatorialen Position zur geographischen Breite.
	/// </summary>
	/// <param name="localHourAngle">Lokaler Stundenwinkel.</param>
	/// <param name="delta">Deklination.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <returns></returns>
	public static double ToHeight(double localHourAngle, double delta, double phi)
	{
		// RÜckgabe
		return MMath.ArcSin(MMath.Sin(phi) * MMath.Sin(delta) + MMath.Cos(phi) * MMath.Cos(delta) * MMath.Cos(localHourAngle));
	}

	// MEphemerides.ToLambda(CPolar)
	/// <summary>
	/// Liefert die ekliptikale Länge zur (scheinbaren) geozentrisch-äquatorialen Position und zum Äquinoktium J2000.
	/// </summary>
	/// <param name="position">Äquatoriale Position.</param>
	/// <returns>Ekliptikale Länge zur (scheinbaren) geozentrisch-äquatorialen Position und zum Äquinoktium J2000.</returns>
	public static double ToLambda(CPolar position)
	{
		// Lokale Felder einrichten und Breite berechnen
		double jd = MCalendar.Jdn20000101;
		return MEphemerides.ToLambda(position.Longitude, position.Latitude, jd);
	}

	// MEphemerides.ToLambda(CPolar, double)
	/// <summary>
	/// Liefert die ekliptikale Länge zur (scheinbaren) geozentrisch-äquatorialen Position und zum Äquinoktium der julianischen Tageszahl.
	/// </summary>
	/// <param name="position">Äquatoriale Position.</param>
	/// <param name="jd">Julianische Tageszahl des Äquinoktiums.</param>
	/// <returns>Ekliptikale Länge zur (scheinbaren) geozentrisch-äquatorialen Position und zum Äquinoktium der julianischen Tageszahl.</returns>
	public static double ToLambda(CPolar position, double jd){ return MEphemerides.ToLambda(position.Longitude, position.Latitude, jd); }

	// MEphemerides.ToLambda(double, double)
	/// <summary>
	/// Liefert die ekliptikale Länge zur (scheinbaren) geozentrisch-äquatorialen Position und zum Äquinoktium J2000.
	/// </summary>
	/// <param name="alpha">Rektaszension.</param>
	/// <param name="delta">Deklination.</param>
	/// <returns>Ekliptikale Länge zur (scheinbaren) geozentrisch-äquatorialen Position und zum Äquinoktium J2000.</returns>
	public static double ToLambda(double alpha, double delta)
	{
		// Lokale Felder einrichten und Breite berechnen
		double jd = DateTime.Now.ToJdn();
		return MEphemerides.ToLambda(alpha, delta, jd);
	}

	// MEphemerides.ToLambda(double, double, double)
	/// <summary>
	/// Liefert die ekliptikale Länge zur (scheinbaren) geozentrisch-äquatorialen Position und zum Äquinoktium der julianischen Tageszahl.
	/// </summary>
	/// <param name="alpha">Rektaszension.</param>
	/// <param name="delta">Deklination.</param>
	/// <param name="jd">Julianische Tageszahl des Äquinoktiums.</param>
	/// <returns>Ekliptikale Länge zur (scheinbaren) geozentrisch-äquatorialen Position und zum Äquinoktium der julianischen Tageszahl.</returns>
	public static double ToLambda(double alpha, double delta, double jd)
	{
		// Lokale Felder einrichten
		double eps = 0.0;
		if(jd == MCalendar.Jdn20000101) eps = Obliquity_2000;
		else eps = MEphemerides.ObliquityTrue(jd);

		// Sonderfälle abfangen
		if(alpha == MMath.Rad090) return  eps;
		if(alpha == MMath.Rad270) return -eps;
		
		// Länge berechnen
		return MMath.ArcTan(MMath.Sin(alpha) * MMath.Cos(eps) + MMath.Tan(delta) * MMath.Sin(eps), MMath.Cos(alpha));
	}

	// MEphemerides.ToSideralTime(double)
	/// <summary>
	/// Liefert die (mittlere) Sternzeit zur (mittleren) Sonnenzeit.
	/// </summary>
	/// <returns>(Mittlere) Sternzeit zur (mittleren) Sonnenzeit.</returns>
	public static double ToSideralTime(double solarTime){ return 0.99726956633 * solarTime; }

	// MEphemerides.ToSolarTime(double)
	/// <summary>
	/// Liefert die (mittlere) Sonnenzeit zur (mittleren) Sternzeit.
	/// </summary>
	/// <returns>(Mittlere) Sonnenzeit zur (mittleren) Sternzeit.</returns>
	public static double ToSolarTime(double sideralTime){ return 1.00273790935 * sideralTime; }

	// MEphemerides.ToZodiac(double)
	/// <summary>
	/// Liefert die ekliptikale Länge in Tierkreisnotation.
	/// </summary>
	/// <param name="lambda">Ekliptikale Länge.</param>
	/// <returns>Ekliptikale Länge in Tierkreisnotation.</returns>
	public static string ToZodiac(double lambda)
	{
		// Lokale Felder einrichten
		double        lng = MMath.ToDeg(lambda);
		int           sng = (int)MMath.Int(lng / 30.0) + 1;
		int           dgr = (int)MMod .Mod(lng , 30.0);
		int           mnt = (int)MMath.Round(MMod.Mod(lng * 60.0, 60.0));
		StringBuilder rtn = new StringBuilder();

		// Überlauf verarbeiten
		if(mnt > 59){mnt -= 60; dgr += 1;}
		if(dgr > 29){dgr -= 30; sng += 1;}
		if(sng > 12){sng  =  1;}

		// Nach Tierkreiszeichen unterscheiden
		switch(sng)
		{
			case  1: rtn.AppendFormat("{0:00}ARI{1:00}", dgr, mnt); break;
			case  2: rtn.AppendFormat("{0:00}TAU{1:00}", dgr, mnt); break;
			case  3: rtn.AppendFormat("{0:00}GEM{1:00}", dgr, mnt); break;
			case  4: rtn.AppendFormat("{0:00}CNC{1:00}", dgr, mnt); break;
			case  5: rtn.AppendFormat("{0:00}LEO{1:00}", dgr, mnt); break;
			case  6: rtn.AppendFormat("{0:00}VIR{1:00}", dgr, mnt); break;
			case  7: rtn.AppendFormat("{0:00}LIB{1:00}", dgr, mnt); break;
			case  8: rtn.AppendFormat("{0:00}SCO{1:00}", dgr, mnt); break;
			case  9: rtn.AppendFormat("{0:00}SGR{1:00}", dgr, mnt); break;
			case 10: rtn.AppendFormat("{0:00}CAP{1:00}", dgr, mnt); break;
			case 11: rtn.AppendFormat("{0:00}AQR{1:00}", dgr, mnt); break;
			case 12: rtn.AppendFormat("{0:00}PSC{1:00}", dgr, mnt); break;
		}
		return rtn.ToString();
	}

	// MEphemerides.Transit(CPolar, CPolar, double)
	/// <summary>
	/// Liefert die julianische Tageszahl des Meridiandurchgang der äquatorialen Position am geographischen Ort und zur julianischen Tageszahl.
	/// </summary>
	/// <param name="positionGeocentric">Geozentrische Position.</param>
	/// <param name="positionGeographic">Geographischer Ort.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Liefert die julianische Tageszahl des Meridiandurchgang der äquatorialen Position am geographischen Ort und zur julianischen Tageszahl.</returns>
	public static double Transit(CPolar positionGeocentric, CPolar positionGeographic, double jd)
	{
		// Lokalen Felder einrichten und Ereigniszeit berechnen
		double h = 0.0;
		return MEphemerides.Transit(positionGeocentric.Longitude, positionGeocentric.Latitude, positionGeocentric.Longitude, positionGeographic.Longitude, jd, ref h);
	}

	// MEphemerides.Transit(CPolar, CPolar, double, ref double)
	/// <summary>
	/// Setzt die horizontale Höhe und liefert die julianische Tageszahl des Meridiandurchgang der äquatorialen Position am geographischen Ort und zur julianischen Tageszahl.
	/// </summary>
	/// <param name="positionGeocentric">Geozentrische Position.</param>
	/// <param name="positionGeographic">Geographischer Ort.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="height">Horizontale Höhe.</param>
	/// <returns>Liefert die julianische Tageszahl des Meridiandurchgang der äquatorialen Position am geographischen Ort und zur julianischen Tageszahl.</returns>
	public static double Transit(CPolar positionGeocentric, CPolar positionGeographic, double jd, ref double height){ return MEphemerides.Transit(positionGeocentric.Longitude, positionGeocentric.Latitude, positionGeocentric.Longitude, positionGeographic.Longitude, jd, ref height); }

	// MEphemerides.Transit(double, double, double, double, double)
	/// <summary>
	/// Liefert die julianische Tageszahl des Meridiandurchgang der äquatorialen Position am geographischen Ort und zur julianischen Tageszahl.
	/// </summary>
	/// <param name="alpha">Rektaszension.</param>
	/// <param name="delta">Deklination.</param>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Liefert die julianische Tageszahl des Meridiandurchgang der äquatorialen Position am geographischen Ort und zur julianischen Tageszahl.</returns>
	public static double Transit(double alpha, double delta, double lambda, double phi, double jd)
	{
		// Lokalen Felder einrichten und Ereigniszeit berechnen
		double h = 0.0;
		return MEphemerides.Transit(alpha, delta, lambda, phi, jd, ref h);
	}

	// MEphemerides.Transit(double, double, double, double, double, ref double)
	/// <summary>
	/// Setzt die horizontale Höhe und liefert die julianische Tageszahl des Meridiandurchgang der äquatorialen Position am geographischen Ort und zur julianischen Tageszahl.
	/// </summary>
	/// <param name="alpha">Rektaszension.</param>
	/// <param name="delta">Deklination.</param>
	/// <param name="lambda">Geographische Länge.</param>
	/// <param name="phi">Geographische Breite.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <param name="height">Horizontale Höhe.</param>
	/// <returns>Liefert die julianische Tageszahl des Meridiandurchgang der äquatorialen Position am geographischen Ort und zur julianischen Tageszahl.</returns>
	public static double Transit(double alpha, double delta, double lambda, double phi, double jd, ref double height)
	{
		// Lokalen Felder einrichten
		double jdn = MMath.Floor(jd - 0.5) + 0.5;
		double t0  = MEphemerides.Gmst(jdn);
		double m   = MMath.Div((alpha + lambda - t0) / MMath.Pi2);

		// Ereigniszeit prüfen
		if(m < 0.0) m = MEphemerides.Transit(alpha, delta, lambda, phi, jdn + 1.0, ref height);

		// Höhe und Ereigniszeit berechnen
		height = MEphemerides.ToHeight(0.0, delta, phi);
		return jd + m;
	}
}
