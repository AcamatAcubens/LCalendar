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
