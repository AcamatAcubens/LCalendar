using System;

namespace Acamat.LCalendar;

/// <summary>
/// Schnittstelle für observierbare Elemente.
/// </summary>
public interface IEcliptical
{
   double Latitude (EPrecision precision, double jd);
   double Longitude(EPrecision precision, double jd);
   double Radius   (EPrecision precision, double jd);
   double SiderealPeriod{ get; }
}
