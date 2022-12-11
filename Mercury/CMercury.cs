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
   // CMercury.Latitude(EPrecision, double)
   /// <summary>
   /// Liefert die ekliptikale Breite zur Präzisionskennung und zur julianischen Tageszahl.
   /// </summary>
   /// <param name="precision">Präzisionskennung.</param>
   /// <param name="jd">Julianische Tageszahl.</param>
   /// <returns>Ekliptikale Breite zur Präzisionskennung und zur julianischen Tageszahl.</returns>
   public override double Latitude(EPrecision precision, double jd){ return MMercury.Latitude(precision, jd); }

   // CMercury.Longitude(EPrecision, double)
   /// <summary>
   /// Liefert die ekliptikale Länge zur Präzessionskennung und zur julianischen Tageszahl.
   /// </summary>
   /// <param name="precision">Präzisionskennung.</param>
   /// <param name="jd">Julianische Tageszahl.</param>
   /// <returns>Ekliptikale Länge zur Präzessionskennung und zur julianischen Tageszahl.</returns>
   public override double Longitude(EPrecision precision, double jd){ return MMercury.Longitude(precision, jd); }

   // CMercury.Radius(EPrecision, double)
   /// <summary>
   /// Liefert den ekliptikalen Radius zur Präzessionskennung und zur julianischen Tageszahl.
   /// </summary>
   /// <param name="precision">Präzisionskennung.</param>
   /// <param name="jd">Julianische Tageszahl.</param>
   /// <returns>Ekliptikaler Radius zur Präzessionskennung und zur julianischen Tageszahl.</returns>
   public override double Radius(EPrecision precision, double jd){ return MMercury.Radius(precision, jd); }

   // CMercury.SiderealPeriod
   /// <summary>
   /// Liefert die siderische Periode.
   /// </summary>
   public override double SiderealPeriod{ get{ return MMercury.SiderealPeriod();} }
}
