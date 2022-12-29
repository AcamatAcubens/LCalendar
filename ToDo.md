# ToDo

## MCalendar

`public static double ToJdn(this DateOnly item)`  
Verlagerung in Acamat.LCore unter Verwendung von `public int DayNumber{ get; }`

`public static double ToJdn(this DateTime item)`  
Verlagerung in Acamat.LCore unter Verwendung von `public double ToOADate();`

## MEphemerides

`public static (double distance, double angle) AngularSeparation(double alphaA, double deltaA, double alphaB, double deltaB)`   
Siehe: Jean Meeus – Astronomical Algorithms – Chapter 17 – Angular Separation  
[Dokumentation](https://en.wikipedia.org/wiki/Angular_distance)  
[Dokumentation](https://handwiki.org/wiki/Astronomy:Angular_distance)  
Verwendung der Winkelwandelungsfunktionen prüfen.  
Verlagerung nach Acamat.LMath.
