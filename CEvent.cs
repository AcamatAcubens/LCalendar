using Acamat.LCore;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Stellt ein Ereignis dar.
/// </summary>
public class CEvent : IEquatable<CEvent>, IComparable
{
   // ------------- //
   // Konstruktoren //
   // ------------- //
   // CEvent()
   /// <summary>
   /// Standardkonstuktor.
   /// </summary>
   public CEvent()
   {
      // Felder einrichten
      this.Name  = string.Empty;
      this.Value = 0.0;
   }

   // CEvent(CEvent)
   /// <summary>
   /// Kopierkonstruktor.
   /// </summary>
   /// <param name="item">Element.</param>
   public CEvent(CEvent item)
   {
      // Argument anwenden
      this.Name  = item.Name;
      this.Value = item.Value;
   }

   // CEvent(double, string)
   /// <summary>
   /// Konstruktor mit einer Angabe zur julianische Tageszahl und zum Namen.
   /// </summary>
   /// <param name="jd">Julianische Tageszahl.</param>
   /// <param name="name">Name.</param>
   public CEvent(double jd, string name)
   {
      // Argumente anwenden
      this.Name  = name;
      this.Value = jd;
   }

   // ------------------ //
   // IEquatable<CEvent> //
   // ------------------ //
   // IEquatabl<CEvent>.Equals(CEvent?)
   /// <summary>
   /// Liefert TRUE, falls die Elemente gleich sind.
   /// </summary>
   /// <param name="item">Elemente.</param>
   /// <returns>TRUE, falls die Elemente gleich sind.</returns>
   bool IEquatable<CEvent>.Equals(CEvent? item){ return this.Equals(item); }

   // ----------- //
   // IComparable //
   // ----------- //
   // IComparable.CompareTo(object?)
   /// <summary>
   /// Liefert -1, falls diese Instanz kleiner als das Element ist, 0, falls diese Instanz gleich dem Element ist oder 1, falls diese Instanz größer als das Element ist.
   /// </summary>
   /// <param name="item">Element.</param>
   /// <returns>Liefert -1, falls diese Instanz kleiner als das Element ist, 0, falls diese Instanz gleich dem Element ist oder 1, falls diese Instanz größer als das Element ist.</returns>
   int IComparable.CompareTo(object? item){ return this.CompareTo(item); }

   // ---------- //
   // Operatoren //
   // ---------- //
   // CEvent == (CEvent?, CEvent?)
	/// <summary>
	/// Liefert das Ergebnis zum Gleichheitsoperator.
	/// </summary>
	/// <param name="itemA">Element A.</param>
	/// <param name="itemB">Element B.</param>
	/// <returns>Ergebnis zum Gleichheitsoperator.</returns>
   public static bool operator == (CEvent? itemA, CEvent? itemB)
   { 
      // Vergleiche durchführen
      if(itemA == null) return false;
      if(itemB == null) return false;
      return itemA.GetHashCode == itemB.GetHashCode;
   }

   // CEvent != (CEvent, CEvent)
	/// <summary>
	/// Liefert das Ergebnis zum Ungleichheitsoperator.
	/// </summary>
	/// <param name="itemA">Vektor A.</param>
	/// <param name="itemB">Vektor B.</param>
	/// <returns>Ergebnis zum Ungleichheitsoperator.</returns>
	public static bool operator != (CEvent itemA, CEvent itemB){ return itemA == itemB ? false : true; }

   // ------------------- //
   // Felder und Methoden //
   // ------------------- //
   // CEvent.CompareTo(object?)
   /// <summary>
   /// Liefert -1, falls diese Instanz kleiner als das Element ist, 0, falls diese Instanz gleich dem Element ist oder 1, falls diese Instanz größer als das Element ist.
   /// </summary>
   /// <param name="item">Element.</param>
   /// <returns>Liefert -1, falls diese Instanz kleiner als das Element ist, 0, falls diese Instanz gleich dem Element ist oder 1, falls diese Instanz größer als das Element ist.</returns>
   public int CompareTo(object? item)
   { 
      // Vergleiche durchführen
      if(item == null)            return +1;
      if(item is CEvent == false) return +1;
      
      // Tageszahlen vergleichen
      if(this.Value < ((CEvent)item).Value) return +1;
      if(this.Value > ((CEvent)item).Value) return -1;

      // Textvergleich
      return this.Name.CompareTo(((CEvent)item).Name);
   }

   // CEvent.Equals(CEvent?)
   /// <summary>
   /// Liefert TRUE, falls die ELemente gleich sind.
   /// </summary>
   /// <param name="item">Element.</param>
   /// <returns>TRUE, falls die ELemente gleich sind.</returns>
   public bool Equals(CEvent? item)
   {
      // Vergleiche durchführen
      if((object)item == null) return false;
      return this.GetHashCode() == item.GetHashCode();
   }

   // CEvent.Equals(Object?)
   /// <summary>
   /// Liefert TRUE, falls die Elemente gleich sind.
   /// </summary>
   /// <param name="obj">Element.</param>
   /// <returns>TRUE, falls die Elemente gleich sind.</returns>
   public override bool Equals(object? item)
   {
      // Vergleiche durchführen
      if(item == null)            return false;
      if(item is CEvent == false) return false;
      return this.GetHashCode() == ((CEvent)item).GetHashCode();
   }

   // CEvent.GetHashCode()
   /// <summary>
   /// Liefert den Hash zum Element.
   /// </summary>
   /// <returns>Hash zum Element.</returns>
   public override int GetHashCode(){ return this.ToString().GetHashCode(); }

   // CEvent.Name
   /// <summary>
   /// Liefert oder übernimmt den Namen.
   /// </summary>
   public string Name{ get; set; }

   // CEvent.ToString()
   /// <summary>
   /// Liefert die Zeichenkettenrepräsentation zum Element.
   /// </summary>
   /// <returns>Zeichenkettenrepräsentation zum Element.</returns>
   public override string ToString(){ return this.Value.ToDateTime().ToString("yyyy-MM-dd HH:mm") + " " + this.Name; }

   // CEvent.Value
   /// <summary>
   /// Liefert oder übernimmt die julianische Tageszahl.
   /// </summary>
   public double Value{ get; set; }
}
