using System;

namespace Acamat.LCalendar;

/// <summary>
/// Stellt einen Planeten dar
/// </summary>
public abstract class CPlanet : IObservable
{
   // ------------- //
   // Konstruktoren //
   // ------------- //
   // Keine

   // ------------ //
   // IObservalble //
   // ------------ //
   // IObservable.Beta(EPrecision, double)
   double IObservable.Beta(EPrecision precision, double jd){ return this.Beta(precision, jd); }

   // IObservable.Lambda(EPrecision, double)
   double IObservable.Lambda(EPrecision precision, double jd){ return this.Lambda(precision, jd); }

   // ------------------- //
   // Felder und Methoden //
   // ------------------- //
   // CPlanet.Beta(EPrecision, double)
   /// <summary>
   /// Liefert die ekliptikale Breite zur Präzisionskennung. und julianischer Tageszahl.
   /// </summary>
   /// <param name="precision">Präzisionskennung.</param>
   /// <param name="jd">Julianische Tageszahl.</param>
   /// <returns>Ekliptikale Breite zur Präzessionskennung und julianischer Tageszahl.</returns>
   public abstract double Beta(EPrecision precision, double jd);

   // CPlanet.Lambda(EPrecision, double)
   /// <summary>
   /// Liefert die ekliptikale Länge zur Präzisionskennung. und julianischer Tageszahl.
   /// </summary>
   /// <param name="precision">Präzisionskennung.</param>
   /// <param name="jd">Juliansiche Tageszahl</param>
   /// <returns>Ekliptikale Länge zur Präzessionskennung und julianischer Tageszahl.</returns>
   public abstract double Lambda(EPrecision precision, double jd);
}
