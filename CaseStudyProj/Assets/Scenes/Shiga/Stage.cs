using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage : StageBase
{
	// 定数
	const int PANEL_SIZE = 70;

	// 変数
	Player _player1, _player2;
	Enemy _enemy1;
	int[,] _stageData = {
		{1,1,1,1,1,1,1,0},
		{1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,0},
		{1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1},
	};

	// 外部読み取り用　関数
	public int[,] StageData { get { return _stageData; } }

	void Start()
	{
		this.CreateStageBase(_stageData, PANEL_SIZE);
		this.InitField();
	}

	void Update()
	{
	}

	void InitField()
	{
		_player1 = new Player();
		_player2 = new Player();

		_player1.Create(this.transform, StatusBase.eType.eAttacker, 4, 4);
		_player2.Create(this.transform, StatusBase.eType.eAttacker, 4, 8);

		_enemy1 = new Enemy();
		_enemy1.Create(this.transform, StatusBase.eType.eAttacker, 4, 7);
	}
}
