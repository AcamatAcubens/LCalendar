using System;

namespace Acamat.LCalendar;

/// <summary>
/// Listet Ereignisarten auf.
/// </summary>
public enum EEventType
{
	// EEventType.Normal
	/// <summary>
	/// Das Ereignis findet statt.
	/// </summary>
	Normal               = 0,

	// EEventType.NoEvent
	/// <summary>
	/// Das Ereignis findet nicht statt, weil der Mond an diesem Tag nicht auf- bzw. untergeht.
	/// </summary>
	NoEvent              = 1,

	// EEventType.AlwaysAboveHorizon
	/// <summary>
	/// Das Ereignis findet nicht statt, weil das Objekt sich immer oberhalb des Horizonts befindet.
	/// </summary>
	AlwaysAboveHorizon   = 2,

	// EEventType.AlwaysBeneathHorizon
	/// <summary>
	/// Das Ereignis findet nicht statt, weil das Objekt sich immer unterhalb des Horizonts befindet.
	/// </summary>
	AlwaysBeneathHorizon = 3,

	// EEventType.DualEvent
	/// <summary>
	/// Das Ereignis findet doppelt statt, weil der Stern oder der Planet an diesem Tag zweimal auf- bzw. untergeht.
	/// </summary>
	DualEvent = 4
}
