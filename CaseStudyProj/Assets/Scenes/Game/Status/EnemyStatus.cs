using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class EnemyStatus01 : StatusBase
{
	void Awake()
	{
		this.Name = "デバウアー";
		this.SubName = "近接型";
		this.Attack = 2;
		this.HpMax = this.Hp = 8;
		this.Move = 2;
		this.Range = 1;
		this.AttackNum = 1;
		this.CharSp = Resources.Load<Sprite>("Sprites/Char/enemy0");
	}
}

class EnemyStatus02 : StatusBase
{
	void Awake()
	{
		this.Name = "デバウアー";
		this.SubName = "射撃型";
		this.Attack = 1;
		this.HpMax = this.Hp = 7;
		this.Move = 1;
		this.Range = 2;
		this.AttackNum = 1;
		this.CharSp = Resources.Load<Sprite>("Sprites/Char/enemy0");
	}
}

class EnemyStatus03 : StatusBase
{
	void Awake()
	{
		this.Name = "デバウアー";
		this.SubName = "偵察型";
		this.Attack = 1;
		this.HpMax = this.Hp = 6;
		this.Move = 4;
		this.Range = 1;
		this.AttackNum = 1;
		this.CharSp = Resources.Load<Sprite>("Sprites/Char/enemy0");
	}
}

class EnemyStatus04 : StatusBase
{
	// 範囲内全部攻撃
	void Awake()
	{
		this.Name = "デバウアー";
		this.SubName = "制圧型";
		this.Attack = 1;
		this.HpMax = this.Hp = 13;
		this.Move = 1;
		this.Range = 1;
		this.AttackNum = 4;
		this.CharSp = Resources.Load<Sprite>("Sprites/Char/enemy0");
	}
}