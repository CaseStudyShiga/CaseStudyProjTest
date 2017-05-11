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

	public int _totalTurnNum = 0;
	public int TotalTurnNum { get { return _totalTurnNum; } set { this._totalTurnNum = value; } }

	void Start()
	{

	}

	void Update()
	{
		if (_totalTurnNum >= 999) _totalTurnNum = 999;
	}
}
