using System;

namespace Acamat.LCalendar;

/// <summary>
/// Schnittsetelle für observierbare Elemente.
/// </summary>
public interface IObservable
{
   double Beta  (EPrecision precision, double jd);
   double Lambda(EPrecision precision, double jd);
   double SiderealPeriod{ get; }
}
