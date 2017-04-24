using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	static GameManager _instance;
	public static GameManager Instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject obj = new GameObject("GameManager");
				_instance = obj.AddComponent<GameManager>();
			}
			return _instance;
		}
	}

	int _score;
	public int Score { get { return _score; } set { _score = value; } }

	int _time;
	public int Time { get { return _time; } set { _time = value; } }

	bool[] canSkill = new bool[5];
	public Slider[] _slider;

	public bool StartSkillGage(int n)
	{
		if (canSkill[n])
		{
			StartCoroutine(SkillCount(n));
			return true;
		}
		else
		{
			return false;
		}
	}

	IEnumerator SkillCount(int n)
	{
		canSkill[n] = false;
		float coolTime = 10.0f; //現在選択しているスキルから取得する
		float skillGage = 0;
		while (skillGage < 1.0)
		{
			yield return null;
			skillGage += UnityEngine.Time.deltaTime / coolTime;
			_slider[n].value = skillGage;
		}
		canSkill[n] = true;
	}
}
