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

	public enum SpeedUp {
		x1 = 0,
		x2,
		x3
	}

	int _totalTurnNum;
	Dictionary<int, Vector2> _dirTable;
	Dictionary<string, int> _stageTable;
	bool _enemyTurn;
	bool _turnendChk;
	SpeedUp _speedupType = SpeedUp.x1;
	bool _complete = false;
	bool[] _isMission;

	public int TotalTurnNum { get { return _totalTurnNum; } }
	public Dictionary<int, Vector2> DirTable { get { return _dirTable; } }
	public Dictionary<string, int> StageTable { get { return _stageTable; } }
	public bool isEnemyTurn { get { return _enemyTurn; } set { _enemyTurn = value; } }
	public bool isTurnEndChk { get { return _turnendChk; } set { _turnendChk = value; } }
	public SpeedUp SpeedUpType { get { return _speedupType; } set { _speedupType = value; } }
	public bool isComplete { get { return _complete; } }
	public bool[] isMission { get { return this._isMission; } set { _isMission = value; } }

	void Awake()
	{
		Application.targetFrameRate = 60;
		this.InitField();
	}

	void Start()
	{
	}

	void Update()
	{
		if (this._totalTurnNum >= 999) this._totalTurnNum = 999;
	}

	public void SaveConfigData()
	{
		ConfigData.Instance.SaveConfigData((int)_speedupType, _turnendChk);
	}

	public void LoadConfigData()
	{
		var speed = ConfigData.Instance.LoadSpeedUp();
		var endchk = ConfigData.Instance.LoadEndChk();

		this._speedupType = (SpeedUp)speed;
		this._turnendChk = endchk;
	}

	public void GameComplete()
	{
		this._complete = true;
	}

	public void AddTurn()
	{
		if (_complete == false)
		{
			_totalTurnNum++;
		}
	}

	public void Reset()
	{
		this._totalTurnNum = 1;
		this._enemyTurn = false;
		this._complete = false;

		for (int i = 0; i < 3; i++)
		{
			this._isMission[i] = false;
		}
	}

	void InitField()
	{
		this._totalTurnNum = 1;

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

		this._stageTable = new Dictionary<string, int> {
			{"P1", 0},

			{"E1", 2},
		};

		this._isMission = new bool[3];
		for (int i = 0; i < 3; i++)
		{
			this._isMission[i] = false;
		}
	}
}