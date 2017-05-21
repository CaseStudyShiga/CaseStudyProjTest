using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage : StageBase
{
	// 変数
	Player[] _player;
	Enemy[] _enemy;
	int[,] _stageData = {
		{1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1},
	};
	int[,] _playerPos = {
			{ 4,3 },
			{ 4,8 },
			{ 2,1 },
		};
	int[,] _enemyPos = {
			{ 4,7 },
			{ 4,6 },
			{ 4,5 },
			{ 5,5 },

			{ 2,7 },
			{ 2,8 },
			{ 1,8 },

		};

	// 外部読み取り用　関数
	public int[,] StageData { get { return _stageData; } }

	void Start()
	{
		this.CreateStageBase(_stageData);
		this.InitField();
		this.AllCheckBetween();
	}

	void Update()
	{
	}

	void InitField()
	{
		_player = new Player[_playerPos.GetLength(0)];
		for(int i = 0; i < _playerPos.GetLength(0); i ++)
		{
			_player[i] = new Player();
			_player[i].Create(this.transform, StatusBase.eType.eAttacker, _playerPos[i,0], _playerPos[i, 1]);
		}

		_enemy = new Enemy[_enemyPos.GetLength(0)];
		for (int i = 0; i < _enemyPos.GetLength(0); i++)
		{
			_enemy[i] = new Enemy();
			_enemy[i].Create(this.transform, StatusBase.eType.eEnemy0, _enemyPos[i,0], _enemyPos[i,1]);
		}
	}
}
