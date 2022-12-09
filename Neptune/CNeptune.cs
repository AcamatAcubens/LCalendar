using System;

namespace Acamat.LCalendar;

/// <summary>
/// Stellt den Planeten Neptun dar.
/// </summary>
public class CNeptune : CPlanet
{
   // ------------- //
   // Konstruktoren //
   // ------------- //
   // Keine

   // ------------------- //
   // Felder und Methoden //
   // ------------------- //
   // CNeptune.Beta(EPrecision, double)
   /// <summary>
   /// Liefert die ekliptikale Breite zur Präzisionskennung und zur julianischer Tageszahl.
   /// </summary>
   /// <param name="precision">Präzisionskennung.</param>
   /// <param name="jd">Julianische Tageszahl</param>
   /// <returns>Ekliptikale Breite zur Präzisionskennung und zur julianischer Tageszahl.</returns>
   public override double Beta(EPrecision precision, double jd){ return MNeptune.Longitude(precision, jd); }

   // CNeptune.Lambda(EPrecision, double)
   /// <summary>
   /// Liefert die ekliptikale Länge zur Präzessionskennung und zur julianischer Tageszahl.
   /// </summary>
   /// <param name="precision">Präzisionskennung.</param>
   /// <param name="jd">Julianische Tageszahl</param>
   /// <returns>ekliptikale Länge zur Präzessionskennung und zur julianischer Tageszahl.</returns>
   public override double Lambda(EPrecision precision, double jd){ return MNeptune.Latitude(precision, jd); }

   // CNeptune.SiderealPeriod
   /// <summary>
   /// Liefert die siderische Periode.
   /// </summary>
   public override double SiderealPeriod{ get{ return MNeptune.SiderealPeriod();} }
}
