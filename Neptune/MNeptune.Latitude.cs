﻿using Acamat.LCore;
using Acamat.LMath;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt Berechnungen zum Neptun.
/// </summary>
public static partial class MNeptune
{
	// ------------------- //
	// Felder und Methoden //
	// ------------------- //
	// MNeptune.B0(EPrecision, double)
	/// <summary>
	/// Liefert die Summe der Terme 0. Ordnung zur Genauigkeitskennung und Jahrhundertbruchteil.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <param name="t">Jahrhundertbruchteil.</param>
	/// <returns>Liefert die Summe der Terme 0. Ordnung.</returns>
	private static double B0(EPrecision value, double t)
	{
		// Lokale Felder einrichten
		double rtn = 0.0;

		// Terme aufsummieren
		rtn += 0.03088622933 * MMath.Cos(1.44104372626 +    38.13303563780 * t);
		if(value == EPrecision.Low) return rtn;
		rtn += 0.00027780087 * MMath.Cos(5.91271882843 +    76.26607127560 * t);
		rtn += 0.00027623609 * MMath.Cos(0.00000000000 +     0.00000000000 * t);
		rtn += 0.00015448133 * MMath.Cos(3.50877080888 +    39.61750834610 * t);
		rtn += 0.00015355490 * MMath.Cos(2.52123799481 +    36.64856292950 * t);
		rtn += 0.00001999919 * MMath.Cos(1.50998669505 +    74.78159856730 * t);
		rtn += 0.00001967540 * MMath.Cos(4.37778195768 +     1.48447270830 * t);
		rtn += 0.00001015137 * MMath.Cos(3.21561035875 +    35.16409022120 * t);
		rtn += 0.00000605767 * MMath.Cos(2.80246601405 +    73.29712585900 * t);
		rtn += 0.00000594878 * MMath.Cos(2.12892708114 +    41.10198105440 * t);
		rtn += 0.00000588805 * MMath.Cos(3.18655882497 +     2.96894541660 * t);
		rtn += 0.00000401830 * MMath.Cos(4.16883287237 +   114.39910691340 * t);
		rtn += 0.00000279964 * MMath.Cos(1.68165309699 +    77.75054398390 * t);
		rtn += 0.00000261647 * MMath.Cos(3.76722704749 +   213.29909543800 * t);
		rtn += 0.00000254333 * MMath.Cos(3.27120499438 +   453.42489381900 * t);
		rtn += 0.00000205590 * MMath.Cos(4.25652348864 +   529.69096509460 * t);
		rtn += 0.00000140455 * MMath.Cos(3.52969556376 +   137.03302416240 * t);
		rtn += 0.00000098530 * MMath.Cos(4.16774829927 +    33.67961751290 * t);
		rtn += 0.00000067971 * MMath.Cos(4.66970781659 +    71.81265315070 * t);
		rtn += 0.00000051257 * MMath.Cos(1.95121181203 +     4.45341812490 * t);
		if(value == EPrecision.Medium) return rtn;
		rtn += 0.00000041931 * MMath.Cos(5.41783694467 +   111.43016149680 * t);
		rtn += 0.00000041822 * MMath.Cos(5.94832001477 +   112.91463420510 * t);
		rtn += 0.00000030637 * MMath.Cos(0.93620571932 +    42.58645376270 * t);
		rtn += 0.00000011084 * MMath.Cos(5.88898793049 +   108.46121608020 * t);
		rtn += 0.00000009728 * MMath.Cos(5.30069593532 +    32.19514480460 * t);
		rtn += 0.00000009664 * MMath.Cos(0.22455797403 +    79.23501669220 * t);
		rtn += 0.00000009620 * MMath.Cos(0.03944255108 +    70.32818044240 * t);
		rtn += 0.00000007386 * MMath.Cos(3.00684933642 +   426.59819087600 * t);
		rtn += 0.00000007087 * MMath.Cos(0.12535040656 +   109.94568878850 * t);
		rtn += 0.00000006391 * MMath.Cos(5.84646101060 +   148.07872442630 * t);
		rtn += 0.00000006251 * MMath.Cos(2.41678769385 +   152.53214255120 * t);
		rtn += 0.00000006169 * MMath.Cos(3.62098109648 +   983.11585891360 * t);
		rtn += 0.00000006021 * MMath.Cos(6.20514068152 +   115.88357962170 * t);
		rtn += 0.00000005795 * MMath.Cos(5.07516716087 +   415.29185818120 * t);
		rtn += 0.00000005006 * MMath.Cos(4.60815664851 +  1059.38193018920 * t);
		rtn += 0.00000004777 * MMath.Cos(0.75210194972 +     5.93789083320 * t);
		rtn += 0.00000004749 * MMath.Cos(2.51605725604 +    37.61177077600 * t);
		rtn += 0.00000004710 * MMath.Cos(3.50929350767 +    38.65430049960 * t);
		rtn += 0.00000004539 * MMath.Cos(5.58182098700 +   175.16605980020 * t);
		rtn += 0.00000004440 * MMath.Cos(4.78977105547 +    38.08485152800 * t);
		rtn += 0.00000004433 * MMath.Cos(1.23386935925 +    38.18121974760 * t);
		rtn += 0.00000004429 * MMath.Cos(5.65995321659 +    98.89998852460 * t);
		rtn += 0.00000004289 * MMath.Cos(4.19647392821 +    47.69426319340 * t);
		rtn += 0.00000004131 * MMath.Cos(4.40682554313 +    37.16982779130 * t);
		rtn += 0.00000004119 * MMath.Cos(1.72779509865 +    28.57180808220 * t);
		rtn += 0.00000004091 * MMath.Cos(1.61787956945 +    39.09624348430 * t);
		rtn += 0.00000004076 * MMath.Cos(6.00252170354 +   145.10977900970 * t);
		rtn += 0.00000003950 * MMath.Cos(2.74104636753 +   350.33211960040 * t);
		rtn += 0.00000003762 * MMath.Cos(4.83940791709 +   491.55792945680 * t);
		rtn += 0.00000002606 * MMath.Cos(1.20956732792 +   451.94042111070 * t);
		rtn += 0.00000002537 * MMath.Cos(2.18628045751 +   454.90936652730 * t);
		rtn += 0.00000002502 * MMath.Cos(0.85987904350 +   106.97674337190 * t);
		rtn += 0.00000002342 * MMath.Cos(0.81387240947 +     4.19278569400 * t);
		rtn += 0.00000002328 * MMath.Cos(5.19779918719 +    72.07328558160 * t);
		rtn += 0.00000002180 * MMath.Cos(0.70099749844 +   206.18554843720 * t);
		rtn += 0.00000001981 * MMath.Cos(0.46617960831 +   184.72728735580 * t);
		rtn += 0.00000001963 * MMath.Cos(6.01909114576 +    44.07092647100 * t);
		rtn += 0.00000001855 * MMath.Cos(5.61635630213 +    35.68535508300 * t);
		rtn += 0.00000001814 * MMath.Cos(3.64699555185 +   220.41264243880 * t);
		rtn += 0.00000001811 * MMath.Cos(0.40456996647 +    40.58071619260 * t);
		rtn += 0.00000001785 * MMath.Cos(2.42154818096 +   388.46515523820 * t);
		rtn += 0.00000001705 * MMath.Cos(6.13551142362 +   181.75834193920 * t);
		rtn += 0.00000001595 * MMath.Cos(3.05266110075 +    38.39366806870 * t);
		rtn += 0.00000001595 * MMath.Cos(2.97147156093 +    37.87240320690 * t);
		rtn += 0.00000001575 * MMath.Cos(3.58964541604 +    38.02116105320 * t);
		rtn += 0.00000001569 * MMath.Cos(2.43405967107 +    38.24491022240 * t);
		rtn += 0.00000001504 * MMath.Cos(5.80298577327 +    46.20979048510 * t);
		rtn += 0.00000001487 * MMath.Cos(0.20211121607 +    30.05628079050 * t);
		rtn += 0.00000001437 * MMath.Cos(1.48678704605 +   135.54855145410 * t);
		rtn += 0.00000001387 * MMath.Cos(2.46149266117 +   138.51749687070 * t);
		rtn += 0.00000001366 * MMath.Cos(1.52026779665 +    68.84370773410 * t);
		rtn += 0.00000001297 * MMath.Cos(5.06156596196 +    33.94024994380 * t);
		rtn += 0.00000001207 * MMath.Cos(1.84658687853 +   251.43213107580 * t);
		rtn += 0.00000001192 * MMath.Cos(0.87275514483 +    42.32582133180 * t);
		rtn += 0.00000001111 * MMath.Cos(0.65175024456 +   146.59425171800 * t);
		rtn += 0.00000001020 * MMath.Cos(0.98226686775 +   143.62530630140 * t);
		rtn += 0.00000001015 * MMath.Cos(0.53439848924 +   129.91947716160 * t);
		rtn += 0.00000000999 * MMath.Cos(2.47463873948 +   312.19908396260 * t);
		rtn += 0.00000000990 * MMath.Cos(3.41514319052 +   144.14657116320 * t);
		rtn += 0.00000000963 * MMath.Cos(4.31733242907 +   151.04766984290 * t);
		rtn += 0.00000000941 * MMath.Cos(1.02993053785 +   221.37585028530 * t);
		rtn += 0.00000000938 * MMath.Cos(2.43648356625 +   567.82400073240 * t);
		rtn += 0.00000000895 * MMath.Cos(0.25123869620 +    30.71067209630 * t);
		rtn += 0.00000000795 * MMath.Cos(5.80519741659 +   149.56319713460 * t);
		rtn += 0.00000000777 * MMath.Cos(0.00175975222 +   218.40690486870 * t);
		rtn += 0.00000000766 * MMath.Cos(4.03399506246 +   522.57741809380 * t);
		rtn += 0.00000000737 * MMath.Cos(3.40060492866 +   446.31134681820 * t);
		rtn += 0.00000000720 * MMath.Cos(0.00651007550 +   460.53844081980 * t);
		rtn += 0.00000000719 * MMath.Cos(1.43795191278 +     8.07675484730 * t);
		rtn += 0.00000000666 * MMath.Cos(1.39457824982 +    84.34282612290 * t);
		rtn += 0.00000000598 * MMath.Cos(5.39946724188 +    41.05379694460 * t);
		rtn += 0.00000000596 * MMath.Cos(0.62390100715 +    35.21227433100 * t);
		rtn += 0.00000000584 * MMath.Cos(1.01405548136 +   536.80451209540 * t);
		rtn += 0.00000000510 * MMath.Cos(1.34478579740 +   258.02441321480 * t);
		rtn += 0.00000000475 * MMath.Cos(5.80072248338 +     7.42236354150 * t);
		rtn += 0.00000000471 * MMath.Cos(0.92632922375 +    44.72531777680 * t);
		rtn += 0.00000000458 * MMath.Cos(5.25325523118 +    80.71948940050 * t);
		rtn += 0.00000000446 * MMath.Cos(1.19167306357 +   180.27386923090 * t);
		rtn += 0.00000000427 * MMath.Cos(5.15774894584 +    31.54075349880 * t);
		rtn += 0.00000000421 * MMath.Cos(3.24496387889 +   416.77633088950 * t);
		rtn += 0.00000000387 * MMath.Cos(1.68488418788 +   183.24281464750 * t);
		rtn += 0.00000000379 * MMath.Cos(2.16947487177 +   105.49227066360 * t);
		rtn += 0.00000000375 * MMath.Cos(0.15223869165 +   255.05546779820 * t);
		rtn += 0.00000000354 * MMath.Cos(4.21526988674 +     0.96320784650 * t);
		rtn += 0.00000000341 * MMath.Cos(4.79194051680 +   110.20632121940 * t);
		rtn += 0.00000000320 * MMath.Cos(3.58085653166 +    45.24658263860 * t);
		rtn += 0.00000000302 * MMath.Cos(3.45706306280 +   100.38446123290 * t);
		rtn += 0.00000000298 * MMath.Cos(2.26790695187 +   639.89728631400 * t);
		rtn += 0.00000000279 * MMath.Cos(1.62614865256 +   294.67297614430 * t);
		rtn += 0.00000000279 * MMath.Cos(0.25689162963 +    39.50563376150 * t);
		rtn += 0.00000000269 * MMath.Cos(5.72024180826 +    36.76043751410 * t);
		rtn += 0.00000000262 * MMath.Cos(3.08384135298 +     6.59228213900 * t);
		rtn += 0.00000000247 * MMath.Cos(0.61040148804 +   186.21176006410 * t);
		rtn += 0.00000000245 * MMath.Cos(0.64173616273 +   419.48464387520 * t);
		rtn += 0.00000000240 * MMath.Cos(4.13447692727 +     0.52126486180 * t);
		rtn += 0.00000000238 * MMath.Cos(2.18528916550 +   219.89137757700 * t);
		rtn += 0.00000000236 * MMath.Cos(3.12464145036 +   563.63121503840 * t);
		rtn += 0.00000000235 * MMath.Cos(0.73189197665 + 10213.28554621100 * t);
		rtn += 0.00000000232 * MMath.Cos(3.92583619589 +  1512.80682400820 * t);
		rtn += 0.00000000232 * MMath.Cos(0.37399822852 +   490.07345674850 * t);
		rtn += 0.00000000230 * MMath.Cos(5.76570492457 +    12.53017297220 * t);
		rtn += 0.00000000223 * MMath.Cos(5.52392277606 +   187.69623277240 * t);
		rtn += 0.00000000219 * MMath.Cos(1.35212712560 +   216.92243216040 * t);
		rtn += 0.00000000217 * MMath.Cos(4.69210602828 +   406.10313764110 * t);
		rtn += 0.00000000217 * MMath.Cos(2.93214905312 +    27.08733537390 * t);
		rtn += 0.00000000215 * MMath.Cos(3.78391259001 +   103.09277421860 * t);
		rtn += 0.00000000200 * MMath.Cos(2.35215465744 +   605.95703637020 * t);
		rtn += 0.00000000193 * MMath.Cos(0.53675942386 +    60.76695288680 * t);
		rtn += 0.00000000191 * MMath.Cos(0.37651439206 +    31.01948863700 * t);
		rtn += 0.00000000190 * MMath.Cos(0.29169556516 +   291.70403072770 * t);
		rtn += 0.00000000189 * MMath.Cos(4.80791039970 +   641.12112659140 * t);
		rtn += 0.00000000187 * MMath.Cos(4.96121139073 +  1589.07289528380 * t);
		rtn += 0.00000000178 * MMath.Cos(6.24797160202 +   316.39186965660 * t);
		rtn += 0.00000000172 * MMath.Cos(5.63262770743 +     7.11354700080 * t);
		rtn += 0.00000000166 * MMath.Cos(5.50438043692 +   662.53120356300 * t);
		rtn += 0.00000000164 * MMath.Cos(4.14700645532 +    77.22927912210 * t);
		rtn += 0.00000000162 * MMath.Cos(0.72021213236 +    11.04570026390 * t);
		rtn += 0.00000000161 * MMath.Cos(5.65988283675 +   343.21857259960 * t);
		rtn += 0.00000000160 * MMath.Cos(4.23490438166 +   487.36514376280 * t);
		rtn += 0.00000000157 * MMath.Cos(4.42530429545 +  6206.80977871580 * t);
		rtn += 0.00000000157 * MMath.Cos(1.02419759383 +  6283.07584999140 * t);
		rtn += 0.00000000156 * MMath.Cos(2.19452173251 +   274.06604832480 * t);
		rtn += 0.00000000155 * MMath.Cos(2.28260574227 +   142.14083359310 * t);
		rtn += 0.00000000154 * MMath.Cos(1.86865302773 +   331.32153907380 * t);
		rtn += 0.00000000153 * MMath.Cos(5.58405022784 +   252.08652238160 * t);
		rtn += 0.00000000148 * MMath.Cos(4.85696640135 +   442.75170057060 * t);
		rtn += 0.00000000146 * MMath.Cos(5.08949604858 +   286.59622129700 * t);
		rtn += 0.00000000146 * MMath.Cos(2.53359213478 +   256.53994050650 * t);
		rtn += 0.00000000145 * MMath.Cos(2.13015521881 +  2042.49778910280 * t);
		rtn += 0.00000000144 * MMath.Cos(5.52229258454 +    14.01464568050 * t);
		rtn += 0.00000000140 * MMath.Cos(1.57962199954 +    75.74480641380 * t);
		rtn += 0.00000000138 * MMath.Cos(2.80728175526 +    82.85835341460 * t);
		rtn += 0.00000000134 * MMath.Cos(1.29277093566 +   456.39383923560 * t);
		rtn += 0.00000000126 * MMath.Cos(5.59769497652 +   179.35884549420 * t);
		rtn += 0.00000000123 * MMath.Cos(0.05442220184 +   944.98282327580 * t);
		rtn += 0.00000000122 * MMath.Cos(1.90676379802 +   418.26080359780 * t);
		rtn += 0.00000000114 * MMath.Cos(1.48894980280 +   253.57099508990 * t);
		rtn += 0.00000000110 * MMath.Cos(5.32587573069 +   240.12579838100 * t);
		rtn += 0.00000000107 * MMath.Cos(0.66995358132 +   190.66517818900 * t);
		rtn += 0.00000000105 * MMath.Cos(0.65548440578 +   173.68158709190 * t);
		rtn += 0.00000000102 * MMath.Cos(2.58735617801 +   450.45594840240 * t);
		rtn += 0.00000000101 * MMath.Cos(4.71267656829 +   117.36805233000 * t);
		rtn += 0.00000000098 * MMath.Cos(0.44044795266 +   328.35259365720 * t);
		rtn += 0.00000000095 * MMath.Cos(2.17636214523 +   101.86893394120 * t);
		rtn += 0.00000000094 * MMath.Cos(1.79320597168 +   493.04240216510 * t);
		rtn += 0.00000000094 * MMath.Cos(0.54938580474 +   293.18850343600 * t);
		rtn += 0.00000000093 * MMath.Cos(0.63687810471 +   377.15882254340 * t);
		rtn += 0.00000000091 * MMath.Cos(5.84828809934 + 10137.01947493540 * t);
		rtn += 0.00000000089 * MMath.Cos(1.02830167997 +  1021.24889455140 * t);
		rtn += 0.00000000080 * MMath.Cos(1.58140274465 +    69.15252427480 * t);
		rtn += 0.00000000075 * MMath.Cos(0.23453373368 +    63.73589830340 * t);
		rtn += 0.00000000071 * MMath.Cos(1.51961989690 +   488.58898404020 * t);
		return rtn;
	}

	// MNeptune.B1(EPrecision, double)
	/// <summary>
	/// Liefert die Summe der Terme 1. Ordnung zur Genauigkeitskennung und Jahrhundertbruchteil.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <param name="t">Jahrhundertbruchteil.</param>
	/// <returns>Liefert die Summe der Terme 1. Ordnung.</returns>
	private static double B1(EPrecision value, double t)
	{
		// Lokale Felder einrichten
		double rtn = 0.0;

		// Terme aufsummieren
		rtn += 0.00227279214 * MMath.Cos(3.80793089870 +   38.13303563780 * t);
		if(value == EPrecision.Low) return rtn;
		rtn += 0.00001803120 * MMath.Cos(1.97576485377 +   76.26607127560 * t);
		rtn += 0.00001433300 * MMath.Cos(3.14159265359 +    0.00000000000 * t);
		rtn += 0.00001385733 * MMath.Cos(4.82555548018 +   36.64856292950 * t);
		rtn += 0.00001073298 * MMath.Cos(6.08054240712 +   39.61750834610 * t);
		rtn += 0.00000147903 * MMath.Cos(3.85766231348 +   74.78159856730 * t);
		rtn += 0.00000136448 * MMath.Cos(0.47764957338 +    1.48447270830 * t);
		rtn += 0.00000070285 * MMath.Cos(6.18782052139 +   35.16409022120 * t);
		rtn += 0.00000051899 * MMath.Cos(5.05221791891 +   73.29712585900 * t);
		if(value == EPrecision.Medium) return rtn;
		rtn += 0.00000042568 * MMath.Cos(0.30721737205 +  114.39910691340 * t);
		rtn += 0.00000037273 * MMath.Cos(4.89476629246 +   41.10198105440 * t);
		rtn += 0.00000037104 * MMath.Cos(5.75999349109 +    2.96894541660 * t);
		rtn += 0.00000026399 * MMath.Cos(5.21566335936 +  213.29909543800 * t);
		rtn += 0.00000018747 * MMath.Cos(0.90426522185 +  453.42489381900 * t);
		rtn += 0.00000016949 * MMath.Cos(4.26463671859 +   77.75054398390 * t);
		rtn += 0.00000012951 * MMath.Cos(6.17709713139 +  529.69096509460 * t);
		rtn += 0.00000010502 * MMath.Cos(1.20336443465 +  137.03302416240 * t);
		rtn += 0.00000004416 * MMath.Cos(1.25478204684 +  111.43016149680 * t);
		rtn += 0.00000004383 * MMath.Cos(6.14147099615 +   71.81265315070 * t);
		rtn += 0.00000003694 * MMath.Cos(0.94837702528 +   33.67961751290 * t);
		rtn += 0.00000002957 * MMath.Cos(4.77532871210 +    4.45341812490 * t);
		rtn += 0.00000002698 * MMath.Cos(1.92435531119 +  112.91463420510 * t);
		rtn += 0.00000001989 * MMath.Cos(3.96637567224 +   42.58645376270 * t);
		rtn += 0.00000001150 * MMath.Cos(4.30568700024 +   37.61177077600 * t);
		rtn += 0.00000000944 * MMath.Cos(2.21777772050 +  109.94568878850 * t);
		rtn += 0.00000000936 * MMath.Cos(1.17054983940 +  148.07872442630 * t);
		rtn += 0.00000000925 * MMath.Cos(2.40329074000 +  206.18554843720 * t);
		rtn += 0.00000000871 * MMath.Cos(4.81775882249 +  152.53214255120 * t);
		rtn += 0.00000000726 * MMath.Cos(4.13829519132 +   28.57180808220 * t);
		rtn += 0.00000000716 * MMath.Cos(0.55781847010 +  350.33211960040 * t);
		rtn += 0.00000000690 * MMath.Cos(1.57381082857 +   38.65430049960 * t);
		rtn += 0.00000000640 * MMath.Cos(2.46161252327 +  115.88357962170 * t);
		rtn += 0.00000000624 * MMath.Cos(2.79466003645 +   79.23501669220 * t);
		rtn += 0.00000000566 * MMath.Cos(1.80111775954 +  175.16605980020 * t);
		rtn += 0.00000000563 * MMath.Cos(1.84072805158 +  983.11585891360 * t);
		rtn += 0.00000000539 * MMath.Cos(2.06690307827 +   40.58071619260 * t);
		rtn += 0.00000000537 * MMath.Cos(1.95986772922 +  220.41264243880 * t);
		rtn += 0.00000000533 * MMath.Cos(1.34787677940 +   47.69426319340 * t);
		rtn += 0.00000000531 * MMath.Cos(2.96991530500 +   98.89998852460 * t);
		rtn += 0.00000000456 * MMath.Cos(3.19611413854 +  108.46121608020 * t);
		rtn += 0.00000000449 * MMath.Cos(1.62191691011 +  144.14657116320 * t);
		rtn += 0.00000000414 * MMath.Cos(1.03543720726 +   70.32818044240 * t);
		rtn += 0.00000000381 * MMath.Cos(6.11910193382 +  426.59819087600 * t);
		rtn += 0.00000000371 * MMath.Cos(2.74239666472 +  415.29185818120 * t);
		rtn += 0.00000000366 * MMath.Cos(2.39752585360 +  129.91947716160 * t);
		rtn += 0.00000000341 * MMath.Cos(3.87265469070 +   35.68535508300 * t);
		rtn += 0.00000000331 * MMath.Cos(4.48858774501 +  460.53844081980 * t);
		rtn += 0.00000000328 * MMath.Cos(0.89613145346 +   38.08485152800 * t);
		rtn += 0.00000000327 * MMath.Cos(3.62341506247 +   38.18121974760 * t);
		rtn += 0.00000000319 * MMath.Cos(1.34097217817 +  184.72728735580 * t);
		rtn += 0.00000000310 * MMath.Cos(0.51297445145 +   37.16982779130 * t);
		rtn += 0.00000000298 * MMath.Cos(4.00532631258 +   39.09624348430 * t);
		rtn += 0.00000000287 * MMath.Cos(2.18351651800 +  491.55792945680 * t);
		rtn += 0.00000000281 * MMath.Cos(3.81657117512 +    5.93789083320 * t);
		rtn += 0.00000000274 * MMath.Cos(6.11504724934 +  522.57741809380 * t);
		rtn += 0.00000000265 * MMath.Cos(5.26569823181 +  446.31134681820 * t);
		rtn += 0.00000000226 * MMath.Cos(6.17710997862 +  454.90936652730 * t);
		rtn += 0.00000000205 * MMath.Cos(5.53935732020 +  536.80451209540 * t);
		rtn += 0.00000000203 * MMath.Cos(6.02944475303 +  149.56319713460 * t);
		rtn += 0.00000000198 * MMath.Cos(2.30775852880 +  146.59425171800 * t);
		rtn += 0.00000000186 * MMath.Cos(3.24302117645 +    4.19278569400 * t);
		rtn += 0.00000000179 * MMath.Cos(4.91458426239 +  451.94042111070 * t);
		rtn += 0.00000000166 * MMath.Cos(1.16793600058 +   72.07328558160 * t);
		rtn += 0.00000000159 * MMath.Cos(3.46955908364 +  145.10977900970 * t);
		rtn += 0.00000000147 * MMath.Cos(2.10574339673 +   44.07092647100 * t);
		rtn += 0.00000000128 * MMath.Cos(1.51108932026 +  221.37585028530 * t);
		rtn += 0.00000000127 * MMath.Cos(0.17176461812 +  138.51749687070 * t);
		rtn += 0.00000000125 * MMath.Cos(3.42713474801 +  251.43213107580 * t);
		rtn += 0.00000000124 * MMath.Cos(5.85160407534 + 1059.38193018920 * t);
		rtn += 0.00000000123 * MMath.Cos(1.98250467171 +   46.20979048510 * t);
		rtn += 0.00000000118 * MMath.Cos(5.27114846878 +   37.87240320690 * t);
		rtn += 0.00000000117 * MMath.Cos(5.35267669439 +   38.39366806870 * t);
		rtn += 0.00000000116 * MMath.Cos(5.88971113590 +   38.02116105320 * t);
		rtn += 0.00000000115 * MMath.Cos(4.73412534395 +   38.24491022240 * t);
		rtn += 0.00000000114 * MMath.Cos(4.37452353441 +  388.46515523820 * t);
		rtn += 0.00000000111 * MMath.Cos(3.56226463770 +  181.75834193920 * t);
		rtn += 0.00000000099 * MMath.Cos(5.19920708255 +  135.54855145410 * t);
		rtn += 0.00000000093 * MMath.Cos(4.64183693718 +  106.97674337190 * t);
		rtn += 0.00000000091 * MMath.Cos(2.38273591235 +   30.05628079050 * t);
		rtn += 0.00000000084 * MMath.Cos(5.51669920239 +    8.07675484730 * t);
		rtn += 0.00000000084 * MMath.Cos(1.35269684746 +   33.94024994380 * t);
		rtn += 0.00000000082 * MMath.Cos(3.18401661435 +   42.32582133180 * t);
		return rtn;
	}

	// MNeptune.B2(EPrecision, double)
	/// <summary>
	/// Liefert die Summe der Terme 2. Ordnung zur Genauigkeitskennung und Jahrhundertbruchteil.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <param name="t">Jahrhundertbruchteil.</param>
	/// <returns>Liefert die Summe der Terme 2. Ordnung.</returns>
	private static double B2(EPrecision value, double t)
	{
		// Lokale Felder einrichten
		double rtn = 0.0;
		if(value == EPrecision.Low) return rtn;

		// Terme aufsummieren
		rtn += 0.00009690766 * MMath.Cos(5.57123750291 +  38.13303563780 * t);
		rtn += 0.00000078815 * MMath.Cos(3.62705474219 +  76.26607127560 * t);
		rtn += 0.00000071523 * MMath.Cos(0.45476688580 +  36.64856292950 * t);
		rtn += 0.00000058646 * MMath.Cos(3.14159265359 +   0.00000000000 * t);
		if(value == EPrecision.Medium) return rtn;
		rtn += 0.00000029915 * MMath.Cos(1.60671721861 +  39.61750834610 * t);
		rtn += 0.00000006472 * MMath.Cos(5.60736756575 +  74.78159856730 * t);
		rtn += 0.00000005800 * MMath.Cos(2.25341847151 +   1.48447270830 * t);
		rtn += 0.00000004309 * MMath.Cos(1.68126737666 +  35.16409022120 * t);
		rtn += 0.00000003502 * MMath.Cos(2.39142672984 + 114.39910691340 * t);
		rtn += 0.00000002649 * MMath.Cos(0.65061457644 +  73.29712585900 * t);
		rtn += 0.00000001518 * MMath.Cos(0.37600329684 + 213.29909543800 * t);
		rtn += 0.00000001223 * MMath.Cos(1.23116043030 +   2.96894541660 * t);
		rtn += 0.00000000779 * MMath.Cos(2.07081431472 + 529.69096509460 * t);
		rtn += 0.00000000766 * MMath.Cos(5.45279753249 + 453.42489381900 * t);
		rtn += 0.00000000496 * MMath.Cos(0.26552533921 +  41.10198105440 * t);
		rtn += 0.00000000482 * MMath.Cos(5.63056237954 + 137.03302416240 * t);
		rtn += 0.00000000469 * MMath.Cos(5.87866293959 +  77.75054398390 * t);
		rtn += 0.00000000345 * MMath.Cos(1.80085651594 +  71.81265315070 * t);
		rtn += 0.00000000274 * MMath.Cos(2.86650141006 +  33.67961751290 * t);
		rtn += 0.00000000166 * MMath.Cos(1.24877330835 + 220.41264243880 * t);
		rtn += 0.00000000158 * MMath.Cos(4.63868656467 + 206.18554843720 * t);
		rtn += 0.00000000153 * MMath.Cos(2.87376446497 + 111.43016149680 * t);
		rtn += 0.00000000116 * MMath.Cos(3.63838544843 + 112.91463420510 * t);
		rtn += 0.00000000104 * MMath.Cos(6.12597614674 + 144.14657116320 * t);
		rtn += 0.00000000085 * MMath.Cos(0.43712705655 +   4.45341812490 * t);
		return rtn;
	}

	// MNeptune.B3(EPrecision, double)
	/// <summary>
	/// Liefert die Summe der Terme 3. Ordnung zur Genauigkeitskennung und Jahrhundertbruchteil.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <param name="t">Jahrhundertbruchteil.</param>
	/// <returns>Liefert die Summe der Terme 3. Ordnung.</returns>
	private static double B3(EPrecision value, double t)
	{
		// Lokale Felder einrichten
		double rtn = 0.0;
		if(value == EPrecision.Low) return rtn;

		// Terme aufsummieren
		rtn += 0.00000273423 * MMath.Cos(1.01688979072 +  38.13303563780 * t);
		if(value == EPrecision.Medium) return rtn;
		rtn += 0.00000002393 * MMath.Cos(0.00000000000 +   0.00000000000 * t);
		rtn += 0.00000002274 * MMath.Cos(2.36805657126 +  36.64856292950 * t);
		rtn += 0.00000002029 * MMath.Cos(5.33364321342 +  76.26607127560 * t);
		rtn += 0.00000000538 * MMath.Cos(3.21934211365 +  39.61750834610 * t);
		rtn += 0.00000000242 * MMath.Cos(4.52650721578 + 114.39910691340 * t);
		rtn += 0.00000000185 * MMath.Cos(1.04913770083 +  74.78159856730 * t);
		rtn += 0.00000000157 * MMath.Cos(3.94195369610 +   1.48447270830 * t);
		rtn += 0.00000000155 * MMath.Cos(3.62376309338 +  35.16409022120 * t);
		return rtn;
	}

	// MNeptune.B4(EPrecision, double)
	/// <summary>
	/// Liefert die Summe der Terme 4. Ordnung zur Genauigkeitskennung und Jahrhundertbruchteil.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <param name="t">Jahrhundertbruchteil.</param>
	/// <returns>Liefert die Summe der Terme 4. Ordnung.</returns>
	private static double B4(EPrecision value, double t)
	{
		// Lokale Felder einrichten
		double rtn = 0.0;
		if(value == EPrecision.Low)    return rtn;
		if(value == EPrecision.Medium) return rtn;

		// Terme aufsummieren
		rtn += 0.00000005728 * MMath.Cos(2.66872693322 + 38.13303563780 * t);
		return rtn;
	}

	// MNeptune.B5(EPrecision, double)
	/// <summary>
	/// Liefert die Summe der Terme 5. Ordnung zur Genauigkeitskennung und Jahrhundertbruchteil.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <param name="t">Jahrhundertbruchteil.</param>
	/// <returns>Liefert die Summe der Terme 5. Ordnung.</returns>
	private static double B5(EPrecision value, double t)
	{
		// Lokale Felder einrichten
		double rtn = 0.0;
		if(value == EPrecision.Low)    return rtn;
		if(value == EPrecision.Medium) return rtn;

		// Terme aufsummieren
		rtn += 0.00000000113 * MMath.Cos(4.70646877989 + 38.13303563780 * t);
		return rtn;
	}

	// MNeptune.Latitude(EPrecision)
	/// <summary>
	/// Liefert die heliozentrisch-ekliptikale Breite zur aktuellen Systemzeit.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <returns>Heliozentrisch-ekliptikale Breite zur aktuellen Systemzeit.</returns>
	public static double Latitude(EPrecision value)
	{
		// Lokale Felder einrichten
		double jd = DateTime.Now.ToJdn();
		return MNeptune.Latitude(value, jd);
	}

	// MNeptune.Latitude(EPrecision, double)
	/// <summary>
	/// Liefert die heliozentrisch-ekliptikale Breite zur julianischen Tageszahl.
	/// </summary>
	/// <param name="value">Genauigkeitskennung.</param>
	/// <param name="jd">Julianische Tageszahl.</param>
	/// <returns>Heliozentrisch-ekliptikale Breite zur julianischen Tageszahl.</returns>
	public static double Latitude(EPrecision value, double jd)
	{
		// Lokale Felder einrichten
		double t  = (jd - MCalendar.Jdn20000101) / 365250.0; // Jahrhundertbruchteil
		double b0 = MNeptune.B0(value, t);
		double b1 = MNeptune.B1(value, t);
		double b2 = MNeptune.B2(value, t);
		double b3 = MNeptune.B3(value, t);
		double b4 = MNeptune.B4(value, t);
		double b5 = MNeptune.B5(value, t);

		// Breite berechnen
		return MMath.Polynome(t, b0, b1, b2, b3, b4, b5);
	}
}
