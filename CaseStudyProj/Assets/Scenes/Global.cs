//------------------------------------------------------------------------------
// 作成者     : 志賀 麻言
// 作成日     : 2016/07/26
//------------------------------------------------------------------------------
// 更新履歴	- 2016/07/26
//			-V0.01 Initial Version
//------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;

//------------------------------------------------------------------------------
// クラス
//------------------------------------------------------------------------------
public static class Global
{
	static ButtonManager _buttonMng;
	public static ButtonManager ButtonMng { get { return _buttonMng; } }

	static SoundManager _soundMng;
	public static SoundManager SoundMng { get { return _soundMng; } }

	static GameManager _gameMng;
	public static GameManager GameMng { get { return _gameMng; } }

	//前回のシーン保存用
	static List<string> _prevScenes = new List<string>(50);
	static string _token;
	static int _mode;
	public static List<string> PrevScenes { get { return _prevScenes; } }
	public static string Token { get { return _token; } set { _token = value; } }
	public static int Mode { get { return _mode; } set { _mode = value; } }

	public static void SetButtonManager()
	{
		_buttonMng = ButtonManager.Instance;
	}

	public static void SetSoundManager()
	{
		_soundMng = SoundManager.GetInstance();
	}

	public static void SetGameManager()
	{
		_gameMng = GameManager.Instance;
	}

	public static void AddPrevScenes(string addName)
	{
		if (_prevScenes.Count > 50)
		{//最大容量に達したため、先頭を削除
			_prevScenes.RemoveAt(0);
		}
		_prevScenes.Add(addName);
	}

	public static void GetToken()
	{
		Console.Write(_token);
	}
}
