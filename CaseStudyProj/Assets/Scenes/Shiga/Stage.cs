using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage : StageBase
{
	// 定数
	const int PANEL_SIZE = 70;

	// 変数
	Player _player1;
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



	void Start() {
		this.CreateStageBase(_stageData, PANEL_SIZE);
		this.InitField();
	}

	void Update() {
	}

	void InitField()
	{
		// players
		_player1 = new Player();
		_player1.Create(this.gameObject, CharBase.eType.eAttacker, 2, 2);

		// enemys
		_enemy1 = new Enemy();
		_enemy1.Create(this.gameObject, CharBase.eType.eAttacker, 4, 7);
	}
}
