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
		SoundManager.Instance.LoadBgm("Title", "bgm1");
		SoundManager.Instance.LoadBgm("Stage1", "bgm2");
		SoundManager.Instance.LoadBgm("Stage2", "bgm3");
	}
}
