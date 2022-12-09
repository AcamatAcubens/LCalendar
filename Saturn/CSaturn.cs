using System;

namespace Acamat.LCalendar;

/// <summary>
/// Stellt den Planeten Saturn dar.
/// </summary>
public class CSaturn : CPlanet
{
   // ------------- //
   // Konstruktoren //
   // ------------- //
   // Keine

   // ------------------- //
   // Felder und Methoden //
   // ------------------- //
   // CSaturn.Beta(EPrecision, double)
   /// <summary>
   /// Liefert die ekliptikale Breite zur Präzisionskennung und zur julianischer Tageszahl.
   /// </summary>
   /// <param name="precision">Präzisionskennung.</param>
   /// <param name="jd">Julianische Tageszahl</param>
   /// <returns>Ekliptikale Breite zur Präzisionskennung und zur julianischer Tageszahl.</returns>
   public override double Beta(EPrecision precision, double jd){ return MSaturn.Longitude(precision, jd); }

   // CSaturn.Lambda(EPrecision, double)
   /// <summary>
   /// Liefert die ekliptikale Länge zur Präzessionskennung und zur julianischer Tageszahl.
   /// </summary>
   /// <param name="precision">Präzisionskennung.</param>
   /// <param name="jd">Julianische Tageszahl</param>
   /// <returns>ekliptikale Länge zur Präzessionskennung und zur julianischer Tageszahl.</returns>
   public override double Lambda(EPrecision precision, double jd){ return MSaturn.Latitude(precision, jd); }

   // CSaturn.SiderealPeriod
   /// <summary>
   /// Liefert die siderische Periode.
   /// </summary>
   public override double SiderealPeriod{ get{ return MSaturn.SiderealPeriod();} }
}
