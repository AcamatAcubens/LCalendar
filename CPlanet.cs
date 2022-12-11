using System;

namespace Acamat.LCalendar;

/// <summary>
/// Stellt einen Planeten dar
/// </summary>
public abstract class CPlanet : IEcliptical
{
   // ------------- //
   // Konstruktoren //
   // ------------- //
   // Keine

   // ----------- //
   // IEcliptical //
   // ----------- //
   // IEcliptical.Latitude(EPrecision, double)
   double IEcliptical.Latitude(EPrecision precision, double jd){ return this.Latitude(precision, jd); }

   // IEcliptical.Longitude(EPrecision, double)
   double IEcliptical.Longitude(EPrecision precision, double jd){ return this.Longitude(precision, jd); }

   // IEcliptical
   double IEcliptical.Radius(EPrecision precision, double jd) { return this.Radius(precision, jd); }

   // IEcliptical.SiderealPeriod
   double IEcliptical.SiderealPeriod{ get{return this.SiderealPeriod;} }

   // ------------------- //
   // Felder und Methoden //
   // ------------------- //
   // CPlanet.Latitude(EPrecision, double)
   /// <summary>
   /// Liefert die ekliptikale Breite zur Präzisionskennung und zur julianischen Tageszahl.
   /// </summary>
   /// <param name="precision">Präzisionskennung.</param>
   /// <param name="jd">Julianische Tageszahl.</param>
   /// <returns>Ekliptikale Breite zur Präzessionskennung und zur julianischen Tageszahl.</returns>
   public abstract double Latitude(EPrecision precision, double jd);

   // CPlanet.Longitude(EPrecision, double)
   /// <summary>
   /// Liefert die ekliptikale Länge zur Präzisionskennung und zur julianischen Tageszahl.
   /// </summary>
   /// <param name="precision">Präzisionskennung.</param>
   /// <param name="jd">Juliansiche Tageszahl.</param>
   /// <returns>Ekliptikale Länge zur Präzessionskennung und zur julianischen Tageszahl.</returns>
   public abstract double Longitude(EPrecision precision, double jd);

   // CPlanet.Radius(EPrecision, double)
   /// <summary>
   /// Liefert den ekliptikalen Radius zur Präzisionskennung und zur julianischen Tageszahl.
   /// </summary>
   /// <param name="precision">Präzisionskennung.</param>
   /// <param name="jd">Juliansiche Tageszahl.</param>
   /// <returns>ekliptikalen Radius zur Präzisionskennung und zur julianischen Tageszahl.</returns>
   public abstract double Radius(EPrecision precision, double jd);

   // CPlanet.SiderealPeriod
   /// <summary>
   /// Liefert die siderische Periode.
   /// </summary>
   /// <value></value>
   public abstract double SiderealPeriod{ get; }
}
