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

	int _totalTurnNum = 0;
	Dictionary<int, Vector2> _dirTable;
	bool _enemyTurn;

	public int TotalTurnNum { get { return _totalTurnNum; } set { this._totalTurnNum = value; } }
	public Dictionary<int, Vector2> DirTable { get { return _dirTable; } }
	public bool isEnemyTurn { get { return _enemyTurn; } set { _enemyTurn = value; } }

	void Awake()
	{
		this.InitField();
	}

	void Start()
	{
	}

	void Update()
	{
		if (this._totalTurnNum >= 999) this._totalTurnNum = 999;
	}

	public void Reset()
	{
		_totalTurnNum = 0;
		_enemyTurn = false;
	}

	void InitField()
	{
		this._dirTable = new Dictionary<int, Vector2> {
			{0, new Vector2(0, -1) },
			{1, new Vector2(1, -1) },
			{2, new Vector2(1,  0) },
			{3, new Vector2(1,  1) },
			{4, new Vector2(0,  1) },
			{5, new Vector2(-1, 1) },
			{6, new Vector2(-1, 0) },
			{7, new Vector2(-1, -1) },
		};
	}
}