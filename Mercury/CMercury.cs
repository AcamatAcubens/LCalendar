using System;

namespace Acamat.LCalendar;

/// <summary>
/// Stellt den Planeten Merkur dar.
/// </summary>
public class CMercury : CPlanet
{
   // ------------- //
   // Konstruktoren //
   // ------------- //
   // Keine

   // ------------------- //
   // Felder und Methoden //
   // ------------------- //
   // CMercury.Beta(EPrecision, double)
   /// <summary>
   /// Liefert die ekliptikale Breite zur Präzisionskennung und zur julianischer Tageszahl.
   /// </summary>
   /// <param name="precision">Präzisionskennung.</param>
   /// <param name="jd">Julianische Tageszahl</param>
   /// <returns>Ekliptikale Breite zur Präzisionskennung und zur julianischer Tageszahl.</returns>
   public override double Beta(EPrecision precision, double jd){ return MMercury.Longitude(precision, jd); }

   // CMercury.Lambda(EPrecision, double)
   /// <summary>
   /// Liefert die ekliptikale Länge zur Präzessionskennung und zur julianischer Tageszahl.
   /// </summary>
   /// <param name="precision">Präzisionskennung.</param>
   /// <param name="jd">Julianische Tageszahl</param>
   /// <returns>ekliptikale Länge zur Präzessionskennung und zur julianischer Tageszahl.</returns>
   public override double Lambda(EPrecision precision, double jd){ return MMercury.Latitude(precision, jd); }

   // CMercury.SiderealPeriod
   /// <summary>
   /// Liefert die siderische Periode.
   /// </summary>
   public override double SiderealPeriod{ get{ return MMercury.SiderealPeriod();} }
}
