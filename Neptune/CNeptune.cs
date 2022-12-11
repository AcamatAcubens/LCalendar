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
   // CNeptune.Latitude(EPrecision, double)
   /// <summary>
   /// Liefert die ekliptikale Breite zur Präzisionskennung und zur julianischen Tageszahl.
   /// </summary>
   /// <param name="precision">Präzisionskennung.</param>
   /// <param name="jd">Julianische Tageszahl.</param>
   /// <returns>Ekliptikale Breite zur Präzisionskennung und zur julianischen Tageszahl.</returns>
   public override double Latitude(EPrecision precision, double jd){ return MNeptune.Latitude(precision, jd); }

   // CNeptune.Longitude(EPrecision, double)
   /// <summary>
   /// Liefert die ekliptikale Länge zur Präzessionskennung und zur julianischen Tageszahl.
   /// </summary>
   /// <param name="precision">Präzisionskennung.</param>
   /// <param name="jd">Julianische Tageszahl.</param>
   /// <returns>Ekliptikale Länge zur Präzessionskennung und zur julianischen Tageszahl.</returns>
   public override double Longitude(EPrecision precision, double jd){ return MNeptune.Longitude(precision, jd); }

   // CNeptune.Radius(EPrecision, double)
   /// <summary>
   /// Liefert den ekliptikalen Radius zur Präzessionskennung und zur julianischen Tageszahl.
   /// </summary>
   /// <param name="precision">Präzisionskennung.</param>
   /// <param name="jd">Julianische Tageszahl.</param>
   /// <returns>Ekliptikaleb Radius zur Präzessionskennung und zur julianischen Tageszahl.</returns>
   public override double Radius(EPrecision precision, double jd){ return MNeptune.Radius(precision, jd); }

   // CNeptune.SiderealPeriod
   /// <summary>
   /// Liefert die siderische Periode.
   /// </summary>
   public override double SiderealPeriod{ get{ return MNeptune.SiderealPeriod();} }
}
