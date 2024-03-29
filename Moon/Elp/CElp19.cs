﻿using Acamat.LMath;
using System;

namespace Acamat.LCalendar;

/// <summary>
/// Bündelt die ELP2000-Theorie.
/// </summary>
internal partial class CElp
{
	// CElp.Elp19[]
	/// <summary>
	/// Datenvektor für Elp19 (Planetary perturbations – Table 2 Longitude/t).
	/// </summary>
	private TElpC[] Elp19 = new TElpC[]
	{
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 0, 0, 2},180.00000,0.00002, 0.037),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 0, 1,-2},  0.00000,0.00011, 0.074),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 0, 1, 0},  0.00000,0.00097, 0.075),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 0, 2, 0},  0.00000,0.00035, 0.038),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 0, 3, 0},  0.00000,0.00004, 0.025),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1,-4, 0},  0.00000,0.00012, 0.019),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1,-3,-2},180.00000,0.00002, 0.015),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1,-3, 0},  0.00000,0.00169, 0.026),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1,-2,-2},180.00000,0.00013, 0.019),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1,-2, 0},  0.00000,0.02438, 0.039),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1,-2, 2},180.00000,0.00006, 0.750),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1,-1,-2},180.00000,0.00076, 0.026),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1,-1, 0},  0.00000,0.37115, 0.082),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1,-1, 2},  0.00000,0.00020, 0.069),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1, 0,-2},180.00000,0.00019, 0.039),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1, 0, 0},  0.00000,1.67680, 1.000),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1, 0, 2},180.00000,0.00104, 0.036),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1, 1,-2},  0.00000,0.00021, 0.079),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1, 1, 0},  0.00000,0.27560, 0.070),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1, 1, 2},180.00000,0.00066, 0.024),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1, 2,-2},180.00000,0.00007, 1.500),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1, 2, 0},  0.00000,0.01923, 0.036),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1, 2, 2},180.00000,0.00011, 0.018),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1, 3, 0},  0.00000,0.00138, 0.025),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1, 3, 2},180.00000,0.00001, 0.015),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 1, 4, 0},  0.00000,0.00010, 0.019),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 2,-3, 0},  0.00000,0.00008, 0.026),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 2,-2, 0},  0.00000,0.00097, 0.041),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 2,-1,-2},180.00000,0.00002, 0.026),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 2,-1, 0},  0.00000,0.01293, 0.089),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 2, 0,-2},  0.00000,0.00002, 0.040),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 2, 0, 0},  0.00000,0.03747, 0.500),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 2, 0, 2},180.00000,0.00003, 0.035),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 2, 1, 0},  0.00000,0.00585, 0.066),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 2, 1, 2},180.00000,0.00001, 0.024),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 2, 2, 0},  0.00000,0.00033, 0.035),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 2, 3, 0},  0.00000,0.00002, 0.024),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 3,-2, 0},  0.00000,0.00003, 0.043),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 3,-1, 0},  0.00000,0.00039, 0.098),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 3, 0, 0},  0.00000,0.00076, 0.333),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 3, 1, 0},  0.00000,0.00014, 0.062),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 4,-1, 0},  0.00000,0.00001, 0.108),
		new TElpC(new int[]{0,0,0,0,0,0,0, 0, 4, 0, 0},  0.00000,0.00001, 0.250),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1,-3, 0, 0},180.00000,0.00002, 0.107),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1,-2,-1, 0},180.00000,0.00001, 0.346),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1,-2, 0, 0},180.00000,0.00022, 0.096),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1,-1,-2, 0},  0.00000,0.00002, 0.066),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1,-1,-1, 0},  0.00000,0.00034, 0.530),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1,-1, 0,-2},  0.00000,0.00003, 0.065),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1,-1, 0, 0},  0.00000,0.00141, 0.088),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1,-1, 1, 0},  0.00000,0.00030, 0.041),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1,-1, 2, 0},  0.00000,0.00004, 0.026),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1, 0,-2, 0},  0.00000,0.00002, 0.071),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1, 0,-1, 0},  0.00000,0.00004, 1.127),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1, 0, 0, 0},  0.00000,0.00011, 0.081),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1, 0, 1, 0},180.00000,0.00003, 0.039),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1, 1,-3, 0},180.00000,0.00006, 0.038),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1, 1,-2, 0},180.00000,0.00088, 0.076),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1, 1,-1, 0},180.00000,0.00270, 8.850),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1, 1, 0, 0},180.00000,0.04516, 0.075),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1, 1, 0, 2},  0.00000,0.00009, 0.025),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1, 1, 1, 0},180.00000,0.00317, 0.038),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1, 1, 1, 2},  0.00000,0.00002, 0.019),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1, 1, 2, 0},180.00000,0.00023, 0.025),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1, 1, 3, 0},180.00000,0.00002, 0.019),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1, 2,-2, 0},180.00000,0.00002, 0.082),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1, 2,-1, 0},  0.00000,0.00002, 0.898),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1, 2, 0, 0},  0.00000,0.00020, 0.070),
		new TElpC(new int[]{0,0,0,0,0,0,0, 1, 2, 1, 0},  0.00000,0.00004, 0.036),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-4,-1, 0},180.00000,0.00008, 0.134),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-4, 0, 0},180.00000,0.00013, 0.048),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-4, 1, 0},180.00000,0.00001, 0.029),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-3,-2, 0},180.00000,0.00007, 0.209),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-3,-1, 0},180.00000,0.00186, 0.118),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-3, 0,-2},180.00000,0.00001, 0.196),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-3, 0, 0},180.00000,0.00256, 0.046),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-3, 1, 0},180.00000,0.00025, 0.029),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-3, 2, 0},180.00000,0.00002, 0.021),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-2,-3, 0},180.00000,0.00008, 0.059),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-2,-2, 0},180.00000,0.00148, 0.265),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-2,-1,-2},  0.00000,0.00002, 0.058),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-2,-1, 0},180.00000,0.03707, 0.105),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-2,-1, 2},  0.00000,0.00008, 0.028),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-2, 0,-2},180.00000,0.00032, 0.243),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-2, 0, 0},180.00000,0.04048, 0.044),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-2, 0, 2},  0.00000,0.00009, 0.020),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-2, 1,-2},  0.00000,0.00008, 0.109),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-2, 1, 0},180.00000,0.00378, 0.028),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-2, 1, 2},  0.00000,0.00002, 0.016),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-2, 2, 0},180.00000,0.00032, 0.020),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-2, 3, 0},180.00000,0.00003, 0.016),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1,-4, 0},180.00000,0.00007, 0.034),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1,-3, 0},180.00000,0.00120, 0.062),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1,-2,-2},  0.00000,0.00007, 0.034),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1,-2, 0},180.00000,0.02165, 0.360),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1,-2, 2},  0.00000,0.00005, 0.042),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1,-1,-2},  0.00000,0.00015, 0.061),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1,-1, 0},180.00000,0.51642, 0.095),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1,-1, 2},  0.00000,0.00107, 0.027),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1, 0,-2},180.00000,0.00539, 0.322),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1, 0, 0},180.00000,0.41383, 0.042),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1, 0, 2},  0.00000,0.00096, 0.020),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1, 1,-2},  0.00000,0.00094, 0.099),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1, 1, 0},180.00000,0.03649, 0.027),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1, 1, 2},  0.00000,0.00019, 0.016),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1, 2,-2},  0.00000,0.00007, 0.043),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1, 2, 0},180.00000,0.00295, 0.020),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1, 2, 2},  0.00000,0.00003, 0.013),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1, 3, 0},180.00000,0.00023, 0.016),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2,-1, 4, 0},180.00000,0.00002, 0.013),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 0,-3, 0},180.00000,0.00003, 0.067),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 0,-2, 0},180.00000,0.00031, 0.564),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 0,-1, 0},  0.00000,0.00819, 0.087),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 0,-1, 2},180.00000,0.00002, 0.026),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 0, 0,-2},  0.00000,0.00029, 0.474),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 0, 0, 0},  0.00000,0.01072, 0.040),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 0, 0, 2},180.00000,0.00003, 0.019),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 0, 1,-2},180.00000,0.00005, 0.090),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 0, 1, 0},  0.00000,0.00128, 0.026),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 0, 2, 0},  0.00000,0.00013, 0.020),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 0, 3, 0},  0.00000,0.00001, 0.016),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 1,-4, 0},180.00000,0.00005, 0.037),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 1,-3, 0},180.00000,0.00045, 0.071),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 1,-2, 0},180.00000,0.00631, 1.292),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 1,-1, 0},  0.00000,0.07118, 0.080),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 1,-1, 2},180.00000,0.00017, 0.025),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 1, 0,-2},  0.00000,0.00362, 0.903),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 1, 0, 0},  0.00000,0.06128, 0.039),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 1, 0, 2},180.00000,0.00016, 0.019),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 1, 1,-2},180.00000,0.00021, 0.082),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 1, 1, 0},  0.00000,0.00734, 0.026),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 1, 1, 2},180.00000,0.00004, 0.015),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 1, 2,-2},180.00000,0.00002, 0.039),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 1, 2, 0},  0.00000,0.00073, 0.019),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 1, 3, 0},  0.00000,0.00007, 0.015),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 2,-3, 0},  0.00000,0.00005, 0.077),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 2,-2, 0},  0.00000,0.00126, 4.425),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 2,-1,-2},  0.00000,0.00003, 0.075),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 2,-1, 0},  0.00000,0.01270, 0.074),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 2,-1, 2},180.00000,0.00003, 0.025),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 2, 0,-2},  0.00000,0.00012, 9.300),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 2, 0, 0},  0.00000,0.00094, 0.037),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 2, 1, 0},  0.00000,0.00007, 0.025),
		new TElpC(new int[]{0,0,0,0,0,0,0, 2, 3,-1, 0},  0.00000,0.00001, 0.069),
		new TElpC(new int[]{0,0,0,0,0,0,0, 3,-2,-2, 0},  0.00000,0.00002, 0.116),
		new TElpC(new int[]{0,0,0,0,0,0,0, 3,-2,-1, 0},  0.00000,0.00006, 0.046),
		new TElpC(new int[]{0,0,0,0,0,0,0, 3,-2, 0, 0},180.00000,0.00003, 0.028),
		new TElpC(new int[]{0,0,0,0,0,0,0, 3,-1,-2, 0},  0.00000,0.00021, 0.104),
		new TElpC(new int[]{0,0,0,0,0,0,0, 3,-1,-1, 0},  0.00000,0.00058, 0.044),
		new TElpC(new int[]{0,0,0,0,0,0,0, 3,-1, 0,-2},  0.00000,0.00005, 0.108),
		new TElpC(new int[]{0,0,0,0,0,0,0, 3,-1, 0, 0},180.00000,0.00017, 0.028),
		new TElpC(new int[]{0,0,0,0,0,0,0, 3, 0,-1, 0},180.00000,0.00007, 0.042),
		new TElpC(new int[]{0,0,0,0,0,0,0, 3, 0, 0, 0},180.00000,0.00004, 0.027),
		new TElpC(new int[]{0,0,0,0,0,0,0, 3, 1,-3, 0},  0.00000,0.00002, 0.602),
		new TElpC(new int[]{0,0,0,0,0,0,0, 3, 1,-2, 0},  0.00000,0.00011, 0.086),
		new TElpC(new int[]{0,0,0,0,0,0,0, 3, 1,-1, 0},180.00000,0.00069, 0.040),
		new TElpC(new int[]{0,0,0,0,0,0,0, 3, 1, 0,-2},180.00000,0.00002, 0.089),
		new TElpC(new int[]{0,0,0,0,0,0,0, 3, 1, 0, 0},180.00000,0.00038, 0.026),
		new TElpC(new int[]{0,0,0,0,0,0,0, 3, 1, 1, 0},180.00000,0.00006, 0.019),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-3,-2, 0},180.00000,0.00006, 0.050),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-3,-1, 0},180.00000,0.00013, 0.030),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-3, 0, 0},180.00000,0.00007, 0.022),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-3, 1, 0},180.00000,0.00001, 0.017),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-2,-3, 0},180.00000,0.00003, 0.130),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-2,-2, 0},180.00000,0.00079, 0.048),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-2,-1, 0},180.00000,0.00155, 0.029),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-2, 0, 0},180.00000,0.00076, 0.021),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-2, 1, 0},180.00000,0.00012, 0.016),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-2, 2, 0},180.00000,0.00001, 0.014),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-1,-3, 0},180.00000,0.00025, 0.115),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-1,-2, 0},180.00000,0.00686, 0.046),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-1,-2, 2},  0.00000,0.00004, 0.020),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-1,-1,-2},180.00000,0.00006, 0.119),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-1,-1, 0},180.00000,0.01097, 0.028),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-1,-1, 2},  0.00000,0.00006, 0.016),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-1, 0,-2},180.00000,0.00002, 0.046),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-1, 0, 0},180.00000,0.00469, 0.021),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-1, 0, 2},  0.00000,0.00003, 0.013),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-1, 1,-2},  0.00000,0.00002, 0.029),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-1, 1, 0},180.00000,0.00071, 0.016),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4,-1, 2, 0},180.00000,0.00008, 0.013),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4, 0,-2, 0},  0.00000,0.00018, 0.044),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4, 0,-1, 0},  0.00000,0.00041, 0.028),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4, 0, 0, 0},  0.00000,0.00022, 0.020),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4, 0, 1, 0},  0.00000,0.00004, 0.016),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4, 1,-3, 0},180.00000,0.00007, 0.093),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4, 1,-2, 0},  0.00000,0.00090, 0.042),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4, 1,-1,-2},  0.00000,0.00005, 0.096),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4, 1,-1, 0},  0.00000,0.00160, 0.027),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4, 1, 0, 0},  0.00000,0.00073, 0.020),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4, 1, 1, 0},  0.00000,0.00013, 0.016),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4, 1, 2, 0},  0.00000,0.00002, 0.013),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4, 2,-3, 0},  0.00000,0.00002, 0.085),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4, 2,-2, 0},  0.00000,0.00018, 0.040),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4, 2,-1, 0},  0.00000,0.00011, 0.026),
		new TElpC(new int[]{0,0,0,0,0,0,0, 4, 2, 0, 0},  0.00000,0.00001, 0.019),
		new TElpC(new int[]{0,0,0,0,0,0,0, 5,-1,-2, 0},  0.00000,0.00002, 0.029),
		new TElpC(new int[]{0,0,0,0,0,0,0, 5, 1,-1, 0},180.00000,0.00001, 0.020),
		new TElpC(new int[]{0,0,0,0,0,0,0, 6,-2,-3, 0},180.00000,0.00002, 0.031),
		new TElpC(new int[]{0,0,0,0,0,0,0, 6,-2,-2, 0},180.00000,0.00004, 0.022),
		new TElpC(new int[]{0,0,0,0,0,0,0, 6,-2,-1, 0},180.00000,0.00004, 0.017),
		new TElpC(new int[]{0,0,0,0,0,0,0, 6,-2, 0, 0},180.00000,0.00001, 0.014),
		new TElpC(new int[]{0,0,0,0,0,0,0, 6,-1,-3, 0},180.00000,0.00010, 0.030),
		new TElpC(new int[]{0,0,0,0,0,0,0, 6,-1,-2, 0},180.00000,0.00023, 0.021),
		new TElpC(new int[]{0,0,0,0,0,0,0, 6,-1,-1, 0},180.00000,0.00018, 0.017),
		new TElpC(new int[]{0,0,0,0,0,0,0, 6,-1, 0, 0},180.00000,0.00006, 0.014),
		new TElpC(new int[]{0,0,0,0,0,0,0, 6,-1, 1, 0},180.00000,0.00001, 0.012),
		new TElpC(new int[]{0,0,0,0,0,0,0, 6, 0,-2, 0},  0.00000,0.00001, 0.021),
		new TElpC(new int[]{0,0,0,0,0,0,0, 6, 0,-1, 0},  0.00000,0.00001, 0.016),
		new TElpC(new int[]{0,0,0,0,0,0,0, 6, 1,-3, 0},  0.00000,0.00001, 0.028),
		new TElpC(new int[]{0,0,0,0,0,0,0, 6, 1,-2, 0},  0.00000,0.00003, 0.021),
		new TElpC(new int[]{0,0,0,0,0,0,0, 6, 1,-1, 0},  0.00000,0.00003, 0.016),
		new TElpC(new int[]{0,0,1,0,0,0,0,-1, 0,-1, 1}, 14.81311,0.00001, 0.089),
		new TElpC(new int[]{0,0,1,0,0,0,0,-1, 0, 0, 1},194.81311,0.00007, 0.487),
		new TElpC(new int[]{0,0,1,0,0,0,0,-1, 0, 1,-1}, 14.81311,0.00006, 0.087),
		new TElpC(new int[]{0,0,1,0,0,0,0, 1, 0,-2,-1},194.81311,0.00004, 0.038),
		new TElpC(new int[]{0,0,1,0,0,0,0, 1, 0,-1,-1},194.81311,0.00053, 0.075),
		new TElpC(new int[]{0,0,1,0,0,0,0, 1, 0,-1, 1},194.81311,0.00007, 0.074),
		new TElpC(new int[]{0,0,1,0,0,0,0, 1, 0, 0,-1},194.81311,0.00247,18.600),
		new TElpC(new int[]{0,0,1,0,0,0,0, 1, 0, 0, 1},194.81311,0.00052, 0.037),
		new TElpC(new int[]{0,0,1,0,0,0,0, 1, 0, 1,-1},194.81311,0.00053, 0.076),
		new TElpC(new int[]{0,0,1,0,0,0,0, 1, 0, 1, 1},194.81311,0.00006, 0.025),
		new TElpC(new int[]{0,0,1,0,0,0,0, 1, 0, 2,-1},194.81311,0.00004, 0.038),
		new TElpC(new int[]{0,0,1,0,0,0,0, 3, 0,-1,-1}, 14.81311,0.00004, 0.088),
		new TElpC(new int[]{0,0,1,0,0,0,0, 3, 0,-1, 1},194.81311,0.00001, 0.026),
		new TElpC(new int[]{0,0,1,0,0,0,0, 3, 0, 0,-1},194.81311,0.00005, 0.041)
	};

	// CElp.Elp19Size
	/// <summary>
	/// Größe des Datenvektor für Elp19 (Planetary perturbations – Table 2 Longitude/t).
	/// </summary>
	private const int Elp19Size = 226;

	// CElp.SumElp19(double[])
	/// <summary>
	/// Liefert das Ergebnis für Elp19 (Planetary perturbations – Table 2 Longitude/t) zum Jahrhundertbruchteil.
	/// </summary>
	/// <param name="t">Jahrhundertbruchteil.</param>
	/// <returns>Ergebnis für Elp19 (Planetary perturbations – Table 2 Longitude/t) zum Jahrhundertbruchteil.</returns>
	private double SumElp19(double[] t)
	{
		// TODO: CElp.SumElp19(double[]): Implementation vervollständigen.
		throw new NotImplementedException("Methode ist nicht implementiert.");
	}
}
