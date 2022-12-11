using Acamat.LCore;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt Unterstützungsmethoden.
/// </summary>
public static class MHelper
{
   // MHelper.ToString(this EEclipseType)
   /// <summary>
   /// Liefert die Textrepräsentation zur Finsterniskennung.
   /// </summary>
   /// <param name="value">Finsterniskennung.</param>
   /// <returns>Textrepräsentation zur Finsterniskennung.</returns>
   public static string ToString(this EEclipseType value)
   {
      // Nach Typ unterscheiden
      switch(value)
      {
         case EEclipseType.MoonNoEclipse:          return "Eine Mondfinsternis ist nicht möglich.";
         case EEclipseType.MoonPartialDefinite:    return "Eine partielle Mondfinsternis ist sicher.";
         case EEclipseType.MoonPartialPotential:   return "Eine partielle Mondfinsternis ist möglich.";
         case EEclipseType.MoonPenumbralDefinite:  return "Eine penumbrale Mondfinsternis ist sicher.";
         case EEclipseType.MoonPenumbralPotential: return "Eine penumbrale Mondfinsternis ist möglich.";
         case EEclipseType.MoonTotalDefinite:      return "Eine totale Mondfinsternis ist sicher.";
         case EEclipseType.MoonTotalPotential:     return "Eine totale Mondfinsternis ist möglich.";
         case EEclipseType.SunCentralDefinite:     return "Eine totale Sonnenfinsternis ist sicher.";
         case EEclipseType.SunCentralPotential:    return "Eine totale Sonnenfinsternis ist möglich.";
         case EEclipseType.SunNoEclipse:           return "Eine Sonnenfinsternis ist nicht möglich.";
         case EEclipseType.SunPartialDefinite:     return "Eine partielle Sonnenfinsternis ist sicher.";
         case EEclipseType.SunPartialPotential:    return "Eine partielle Sonnenfinsternis ist möglich.";
      }

      // Ausnahme auslösen
      throw new UnexpectedCodePathException();
   }
}
