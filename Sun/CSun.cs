using System;

namespace Acamat.LCalendar;

/// <summary>
/// Stellt den Planeten Sonne dar.
/// </summary>
public class CSun : CPlanet
{
   // ------------- //
   // Konstruktoren //
   // ------------- //
   // Keine

   // ------------------- //
   // Felder und Methoden //
   // ------------------- //
   // CSun.Latitude(EPrecision, double)
   /// <summary>
   /// Liefert die ekliptikale Breite zur Präzisionskennung und zur julianischen Tageszahl.
   /// </summary>
   /// <param name="precision">Präzisionskennung.</param>
   /// <param name="jd">Julianische Tageszahl.</param>
   /// <returns>Ekliptikale Breite zur Präzisionskennung und zur julianischen Tageszahl.</returns>
   public override double Latitude(EPrecision precision, double jd){ return MSun.Latitude(precision, jd); }

   // CSun.Longitude(EPrecision, double)
   /// <summary>
   /// Liefert die ekliptikale Länge zur Präzessionskennung und zur julianischen Tageszahl.
   /// </summary>
   /// <param name="precision">Präzisionskennung.</param>
   /// <param name="jd">Julianische Tageszahl.</param>
   /// <returns>Ekliptikale Länge zur Präzessionskennung und zur julianischen Tageszahl.</returns>
   public override double Longitude(EPrecision precision, double jd){ return MSun.Latitude(precision, jd); }

   // CSun.Radius(EPrecision, double)
   /// <summary>
   /// Liefert den ekliptikalen Radius zur Präzessionskennung und zur julianischen Tageszahl.
   /// </summary>
   /// <param name="precision">Präzisionskennung.</param>
   /// <param name="jd">Julianische Tageszahl.</param>
   /// <returns>Ekliptikalee Radius zur Präzessionskennung und zur julianischen Tageszahl.</returns>
   public override double Radius(EPrecision precision, double jd){ return MSun.Radius(precision, jd); }

   // CSun.SiderealPeriod
   /// <summary>
   /// Liefert die siderische Periode.
   /// </summary>
   public override double SiderealPeriod{ get{ return MEarth.SiderealYear();} }
}
