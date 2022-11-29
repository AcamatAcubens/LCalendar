﻿using Acamat.LMath;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt die Elp2100-Theorie.
/// </summary>
internal partial class CElp
{
	// CElp.Elp21[]
	/// <summary>
	/// Datenvektor für Elp21 (Planetary perturbations – Table 2 Distance/t).
	/// </summary>
	private TElpC[] Elp21 = new TElpC[]
	{
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 0, 0, 0},270.00000,0.00149,99999.999),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 0, 1,-2}, 90.00000,0.00010,    0.074),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 0, 1, 0},270.00000,0.00174,    0.075),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 0, 2, 0},270.00000,0.00029,    0.038),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 0, 3, 0},270.00000,0.00003,    0.025),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1,-4, 0}, 90.00000,0.00007,    0.019),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1,-3, 0}, 90.00000,0.00106,    0.026),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1,-2, 0}, 90.00000,0.01764,    0.039),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1,-2, 2}, 90.00000,0.00013,    0.750),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1,-1,-2}, 90.00000,0.00002,    0.026),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1,-1, 0}, 90.00000,0.32654,    0.082),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1,-1, 2},270.00000,0.00084,    0.069),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1, 0,-2}, 90.00000,0.00047,    0.039),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1, 0, 0},270.00000,0.12302,    1.000),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1, 0, 2}, 90.00000,0.00040,    0.036),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1, 1,-2}, 90.00000,0.00062,    0.079),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1, 1, 0},270.00000,0.26396,    0.070),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1, 2,-2},270.00000,0.00008,    1.500),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1, 2, 0},270.00000,0.01449,    0.036),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1, 3, 0},270.00000,0.00089,    0.025),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1, 4, 0},270.00000,0.00006,    0.019),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 2,-3, 0}, 90.00000,0.00005,    0.026),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 2,-2, 0}, 90.00000,0.00069,    0.041),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 2,-1, 0}, 90.00000,0.01066,    0.089),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 2,-1, 2},270.00000,0.00002,    0.064),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 2, 0,-2}, 90.00000,0.00003,    0.040),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 2, 0, 0},270.00000,0.00536,    0.500),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 2, 1, 0},270.00000,0.00587,    0.066),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 2, 2, 0},270.00000,0.00026,    0.035),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 3,-2, 0}, 90.00000,0.00002,    0.043),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 3,-1, 0}, 90.00000,0.00029,    0.098),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 3, 0, 0},270.00000,0.00016,    0.333),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 3, 1, 0},270.00000,0.00014,    0.062),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1,-2,-1, 0},270.00000,0.00004,    0.346),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1,-2, 0, 0}, 90.00000,0.00015,    0.096),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1,-1,-1, 0},270.00000,0.00029,    0.530),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1,-1, 0,-2}, 90.00000,0.00003,    0.065),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1,-1, 0, 0},270.00000,0.00126,    0.088),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1,-1, 1, 0},270.00000,0.00028,    0.041),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1,-1, 2, 0},270.00000,0.00003,    0.026),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1, 0,-2, 0}, 90.00000,0.00002,    0.071),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1, 0,-1, 0}, 90.00000,0.00004,    1.127),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1, 0, 0, 0},270.00000,0.00008,    0.081),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1, 0, 1, 0}, 90.00000,0.00002,    0.039),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1, 1,-3, 0},270.00000,0.00004,    0.038),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1, 1,-2, 0},270.00000,0.00084,    0.076),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1, 1,-1, 0},270.00000,0.00214,    8.850),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1, 1, 0,-2},270.00000,0.00008,    0.074),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1, 1, 0, 0}, 90.00000,0.04194,    0.075),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1, 1, 1, 0}, 90.00000,0.00235,    0.038),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1, 1, 2, 0}, 90.00000,0.00015,    0.025),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1, 2,-1, 0},270.00000,0.00002,    0.898),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1, 2, 0, 0},270.00000,0.00019,    0.070),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1, 2, 1, 0},270.00000,0.00003,    0.036),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-4,-1, 0}, 90.00000,0.00004,    0.134),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-4, 0, 0}, 90.00000,0.00016,    0.048),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-3,-2, 0},270.00000,0.00008,    0.209),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-3,-1, 0}, 90.00000,0.00112,    0.118),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-3, 0, 0}, 90.00000,0.00310,    0.046),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-3, 1, 0}, 90.00000,0.00022,    0.029),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-2,-3, 0},270.00000,0.00008,    0.059),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-2,-2, 0},270.00000,0.00173,    0.265),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-2,-1,-2},270.00000,0.00005,    0.058),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-2,-1, 0}, 90.00000,0.02490,    0.105),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-2, 0,-2},270.00000,0.00014,    0.243),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-2, 0, 0}, 90.00000,0.04970,    0.044),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-2, 1,-2},270.00000,0.00004,    0.109),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-2, 1, 0}, 90.00000,0.00331,    0.028),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-2, 2, 0}, 90.00000,0.00023,    0.020),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1,-4, 0},270.00000,0.00006,    0.034),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1,-3, 0},270.00000,0.00125,    0.062),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1,-2,-2},270.00000,0.00004,    0.034),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1,-2, 0},270.00000,0.02529,    0.360),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1,-2, 2},270.00000,0.00008,    0.042),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1,-1,-2},270.00000,0.00081,    0.061),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1,-1, 0}, 90.00000,0.38245,    0.095),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1,-1, 2},270.00000,0.00009,    0.027),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1, 0,-2},270.00000,0.00165,    0.322),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1, 0, 0}, 90.00000,0.51395,    0.042),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1, 1,-2},270.00000,0.00053,    0.099),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1, 1, 0}, 90.00000,0.03222,    0.027),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1, 2,-2},270.00000,0.00005,    0.043),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1, 2, 0}, 90.00000,0.00213,    0.020),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1, 3, 0}, 90.00000,0.00015,    0.016),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 0,-3, 0},270.00000,0.00002,    0.067),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 0,-2, 0}, 90.00000,0.00004,    0.564),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 0,-1, 0},270.00000,0.00633,    0.087),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 0, 0,-2}, 90.00000,0.00005,    0.474),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 0, 0, 0},270.00000,0.01356,    0.040),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 0, 1,-2}, 90.00000,0.00003,    0.090),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 0, 1, 0},270.00000,0.00114,    0.026),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 0, 2, 0},270.00000,0.00009,    0.020),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 1,-4, 0},270.00000,0.00004,    0.037),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 1,-3, 0},270.00000,0.00043,    0.071),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 1,-2, 0},270.00000,0.00039,    1.292),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 1,-1,-2}, 90.00000,0.00012,    0.070),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 1,-1, 0},270.00000,0.06068,    0.080),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 1, 0,-2}, 90.00000,0.00034,    0.903),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 1, 0, 0},270.00000,0.07754,    0.039),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 1, 1,-2}, 90.00000,0.00014,    0.082),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 1, 1, 0},270.00000,0.00658,    0.026),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 1, 2, 0},270.00000,0.00053,    0.019),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 1, 3, 0},270.00000,0.00004,    0.015),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 2,-3, 0}, 90.00000,0.00005,    0.077),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 2,-2, 0}, 90.00000,0.00055,    4.425),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 2,-1,-2}, 90.00000,0.00005,    0.075),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 2,-1, 0},270.00000,0.01186,    0.074),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 2, 0, 0},270.00000,0.00074,    0.037),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 2, 1, 0},270.00000,0.00005,    0.025),
		new TElpC(new int[]{0,0,0,0,0,0,0, 3,-2,-1, 0},270.00000,0.00007,    0.046),
		new TElpC(new int[]{0,0,0,0,0,0,0, 3,-2, 0, 0}, 90.00000,0.00005,    0.028),
		new TElpC(new int[]{0,0,0,0,0,0,0, 3,-1,-2, 0},270.00000,0.00013,    0.104),
		new TElpC(new int[]{0,0,0,0,0,0,0, 3,-1,-1, 0},270.00000,0.00064,    0.044),
		new TElpC(new int[]{0,0,0,0,0,0,0, 3,-1, 0,-2},270.00000,0.00004,    0.108),
		new TElpC(new int[]{0,0,0,0,0,0,0, 3,-1, 0, 0}, 90.00000,0.00040,    0.028),
		new TElpC(new int[]{0,0,0,0,0,0,0, 3,-1, 1, 0}, 90.00000,0.00002,    0.020),
		new TElpC(new int[]{0,0,0,0,0,0,0, 3, 0,-1, 0}, 90.00000,0.00006,    0.042),
		new TElpC(new int[]{0,0,0,0,0,0,0, 3, 0, 0, 0}, 90.00000,0.00003,    0.027),
		new TElpC(new int[]{0,0,0,0,0,0,0, 3, 1,-2, 0},270.00000,0.00010,    0.086),
		new TElpC(new int[]{0,0,0,0,0,0,0, 3, 1,-1, 0}, 90.00000,0.00053,    0.040),
		new TElpC(new int[]{0,0,0,0,0,0,0, 3, 1, 0,-2}, 90.00000,0.00002,    0.089),
		new TElpC(new int[]{0,0,0,0,0,0,0, 3, 1, 0, 0}, 90.00000,0.00027,    0.026),
		new TElpC(new int[]{0,0,0,0,0,0,0, 3, 1, 1, 0}, 90.00000,0.00004,    0.019),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-3,-2, 0}, 90.00000,0.00004,    0.050),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-3,-1, 0}, 90.00000,0.00012,    0.030),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-3, 0, 0}, 90.00000,0.00006,    0.022),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-2,-2, 0}, 90.00000,0.00054,    0.048),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-2,-1, 0}, 90.00000,0.00140,    0.029),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-2, 0,-2}, 90.00000,0.00002,    0.048),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-2, 0, 0}, 90.00000,0.00064,    0.021),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-2, 1, 0}, 90.00000,0.00009,    0.016),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-1,-3, 0}, 90.00000,0.00011,    0.115),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-1,-2, 0}, 90.00000,0.00476,    0.046),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-1,-1,-2}, 90.00000,0.00007,    0.119),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-1,-1, 0}, 90.00000,0.00993,    0.028),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-1, 0,-2}, 90.00000,0.00014,    0.046),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-1, 0, 0}, 90.00000,0.00394,    0.021),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-1, 1, 0}, 90.00000,0.00051,    0.016),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-1, 2, 0}, 90.00000,0.00005,    0.013),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4, 0,-2, 0},270.00000,0.00012,    0.044),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4, 0,-1, 0},270.00000,0.00038,    0.028),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4, 0, 0, 0},270.00000,0.00019,    0.020),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4, 0, 1, 0},270.00000,0.00003,    0.016),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4, 1,-3, 0}, 90.00000,0.00006,    0.093),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4, 1,-2, 0},270.00000,0.00060,    0.042),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4, 1,-1,-2},270.00000,0.00004,    0.096),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4, 1,-1, 0},270.00000,0.00146,    0.027),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4, 1, 0,-2},270.00000,0.00005,    0.042),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4, 1, 0, 0},270.00000,0.00061,    0.020),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4, 1, 1, 0},270.00000,0.00009,    0.016),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4, 2,-2, 0},270.00000,0.00013,    0.040),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4, 2,-1, 0},270.00000,0.00009,    0.026),
		new TElpC(new int[]{0,0,0,0,0,0,0, 6,-2,-2, 0}, 90.00000,0.00003,    0.022),
		new TElpC(new int[]{0,0,0,0,0,0,0, 6,-2,-1, 0}, 90.00000,0.00003,    0.017),
		new TElpC(new int[]{0,0,0,0,0,0,0, 6,-1,-3, 0}, 90.00000,0.00006,    0.030),
		new TElpC(new int[]{0,0,0,0,0,0,0, 6,-1,-2, 0}, 90.00000,0.00017,    0.021),
		new TElpC(new int[]{0,0,0,0,0,0,0, 6,-1,-1, 0}, 90.00000,0.00013,    0.017),
		new TElpC(new int[]{0,0,0,0,0,0,0, 6,-1, 0, 0}, 90.00000,0.00004,    0.014),
		new TElpC(new int[]{0,0,0,0,0,0,0, 6, 1,-2, 0},270.00000,0.00002,    0.021),
		new TElpC(new int[]{0,0,0,0,0,0,0, 6, 1,-1, 0},270.00000,0.00002,    0.016),
		new TElpC(new int[]{0,0,1,0,0,0,0,-1, 0, 0,-1},284.81311,0.00003,    0.040),
		new TElpC(new int[]{0,0,1,0,0,0,0,-1, 0, 1,-1},104.81311,0.00005,    0.087),
		new TElpC(new int[]{0,0,1,0,0,0,0, 1, 0,-2,-1},284.81311,0.00003,    0.038),
		new TElpC(new int[]{0,0,1,0,0,0,0, 1, 0,-1,-1},284.81311,0.00049,    0.075),
		new TElpC(new int[]{0,0,1,0,0,0,0, 1, 0,-1, 1},104.81311,0.00012,    0.074),
		new TElpC(new int[]{0,0,1,0,0,0,0, 1, 0, 1,-1},104.81311,0.00049,    0.076),
		new TElpC(new int[]{0,0,1,0,0,0,0, 1, 0, 2,-1},104.81311,0.00003,    0.038),
		new TElpC(new int[]{0,0,1,0,0,0,0, 3, 0,-1,-1},284.81311,0.00003,    0.088),
		new TElpC(new int[]{0,0,1,0,0,0,0, 3, 0, 0,-1},104.81311,0.00004,    0.041)
	};

	// CElp.Elp21Size
	/// <summary>
	/// Größe des Datenvektor für Elp21 (Planetary perturbations – Table 2 Distance/t).
	/// </summary>
	private const int Elp21Size = 169;

	// CElp.SumElp21(double[])
	/// <summary>
	/// Liefert das Ergebnis für Elp21 (Planetary perturbations – Table 2 Distance/t) zum Jahrhundertbruchteil.
	/// </summary>
	/// <param name="t">Jahrhundertbruchteil.</param>
	/// <returns>Ergebnis für Elp21 (Planetary perturbations – Table 2 Distance/t) zum Jahrhundertbruchteil.</returns>
	private double SumElp21(double[] t)
	{
		// TODO: CElp.SumElp21(double[]): Implementation vervollständigen.
		throw new NotImplementedException("Methode ist nicht implementiert.");
	}
}
