# ToDo

## ENationalHoliday
[Siehe](https://de.wikipedia.org/wiki/Liste_von_Nationalfeiertagen)


## MCalendar

`public static double ToJdn(this DateOnly item)`  
Verlagerung in Acamat.LCore unter Verwendung von `public int DayNumber{ get; }`

`public static double ToJdn(this DateTime item)`  
Verlagerung in Acamat.LCore unter Verwendung von `public double ToOADate();`

## MEphemerides

// HeliacalRise
// HeliacalSet
// HeliacalConjunction
// HeliacalOpposition
// IsCirumpolar
//    δ + ϕ ≥ 90°

// Aus Planetenmodule einlagern:
// · CPolar PositionEcliptical(EPrecision value)
// · CPolar PositionEcliptical(EPrecision value, double jd)
// · CPolar PositionEquatorial()
// · CPolar PositionEquatorial(double)

`public static (double distance, double angle) AngularSeparation(double alphaA, double deltaA, double alphaB, double deltaB)`   
Siehe: Jean Meeus – Astronomical Algorithms – Chapter 17 – Angular Separation  
[Dokumentation](https://en.wikipedia.org/wiki/Angular_distance)  
[Dokumentation](https://handwiki.org/wiki/Astronomy:Angular_distance)  
Verwendung der Winkelwandelungsfunktionen prüfen.  
Verlagerung nach Acamat.LMath.

## MGregorianHoliday

· Überladungsline für die oster-bezogenen Feiertage mit einem Osterdatum
· Überladungsline für die advent-bezogenen Feiertage mit einem Weihnachtsdatum

`public static double NewYear(int year)`  
Liefert Neujahr. Ist der 01.01. des Kalenderjahres.

`public static double Epiphany(int year)`  
Liefert Heilige Drei Könige. Ist der 06.01. des Kalenderjahres.

`public static double FridayThe13th(double d)`  
Liefert den nächsten Freitag, den 13.

`public static double WorkersDay(int year)`  
Liefert den Maifeiertag. Ist der 01.05. des Kalenderjahres.
