using System;

namespace Acamat.LCalendar;

/// <summary>
/// Listet Genauigkeiten auf.
/// </summary>
public enum EPrecision
{
	// EPrecision.None
	/// <summary>
	/// Die Genauigkeit ist nicht genannt.
	/// </summary>
	None   = 0,

	// EPrecision.Low
	/// <summary>
	/// Die Berechnung erfolgt mit niedriger Genauigkeit.
	/// </summary>
	Low    = 1,

	// EPrecision.Medium
	/// <summary>
	/// Die Berechnung erfolgt mit mittlerer Genauigkeit.
	/// </summary>
	Medium = 2,

	// EPrecision.High
	/// <summary>
	/// Die Berechnung erfolgt mit hoher Genauigkeit.
	/// </summary>
	High   = 3
}
