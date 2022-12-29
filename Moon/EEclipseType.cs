using Acamat.LCore;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Listet Finsternisabschätzungen auf.
/// </summary>
public enum EEclipseType
{
	// EEclipseType.SunNoEclipse
	/// <summary>
	/// Eine Sonnenfinsternis ist nicht möglich.
	/// </summary>
	SunNoEclipse = 1,

	// EEclipseType.SunPartialPotential
	/// <summary>
	/// Eine partielle Sonnenfinsternis ist möglich.
	/// </summary>
	SunPartialPotential = 2,

	// EEclipseType.SunPartialDefinite
	/// <summary>
	/// Eine partielle Sonnenfinsternis ist sicher.
	/// </summary>
	SunPartialDefinite = 3,

	// EEclipseType.SunCentralPotential
	/// <summary>
	/// Eine totale Sonnenfinsternis ist möglich.
	/// </summary>
	SunCentralPotential = 4,

	// EEclipseType.SunCentralDefinite
	/// <summary>
	/// Eine totale Sonnenfinsternis ist sicher.
	/// </summary>
	SunCentralDefinite = 5,

	// EEclipseType.MoonNoEclipse
	/// <summary>
	/// Eine Mondfinsternis ist nicht möglich.
	/// </summary>
	MoonNoEclipse = 6,

	// EEclipseType.MoonPenumbralPotential
	/// <summary>
	/// Eine penumbrale Mondfinsternis ist möglich.
	/// </summary>
	MoonPenumbralPotential = 7,

	// EEclipseType.MoonPenumbralDefinite
	/// <summary>
	/// Eine penumbrale Mondfinsternis ist sicher.
	/// </summary>
	MoonPenumbralDefinite = 8,

	// EEclipseType.MoonPartialPotential
	/// <summary>
	/// Eine partielle Mondfinsternis ist möglich.
	/// </summary>
	MoonPartialPotential = 9,

	// EEclipseType.MoonPartialDefinite
	/// <summary>
	/// Eine partielle Mondfinsternis ist sicher.
	/// </summary>
	MoonPartialDefinite = 10,

	// EEclipseType.MoonTotalPotential
	/// <summary>
	/// Eine totale Mondfinsternis ist möglich.
	/// </summary>
	MoonTotalPotential = 11,

	// EEclipseType.MoonTotalDefinte
	/// <summary>
	/// Eine totale Mondfinsternis ist sicher.
	/// </summary>
	MoonTotalDefinite = 12
}

/// <summary>
/// Bündelt Erweiterungsmethoden
/// </summary>
public static class MEclipseType
{
   /// <summary>
   /// Liefert die Textrepräsentation zur Finsterniskennung.
   /// </summary>
   /// <param name="value">Finsterniskennung.</param>
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
