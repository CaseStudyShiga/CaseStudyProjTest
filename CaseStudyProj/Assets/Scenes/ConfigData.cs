using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ConfigData
{
	const string SPEED_UP_KEY = "speedup";
	const string TURN_END_CHK_KEY = "turnendchk";

	static ConfigData _instance;
	public static ConfigData Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new ConfigData();
			}
			return _instance;
		}
	}

	public void SaveConfigData(int speed, bool endchk)
	{
		PlayerPrefs.SetInt(SPEED_UP_KEY, speed);
		this.SetBool(TURN_END_CHK_KEY, endchk);
		PlayerPrefs.Save();
	}

	public int LoadSpeedUp()
	{
		return PlayerPrefs.GetInt(SPEED_UP_KEY, 0);
	}

	public bool LoadEndChk()
	{
		return this.GetBool(TURN_END_CHK_KEY, true);
	}

	bool GetBool(string key, bool defalutValue)
	{
		var value = PlayerPrefs.GetInt(key, defalutValue ? 1 : 0);
		return value == 1;
	}

	void SetBool(string key, bool value)
	{
		PlayerPrefs.SetInt(key, value ? 1 : 0);
	}
}
