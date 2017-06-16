using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Runtime : MonoBehaviour
{
	[RuntimeInitializeOnLoadMethod]
	static void OnRunTime()
	{
		Application.targetFrameRate = 60;
	}
}
