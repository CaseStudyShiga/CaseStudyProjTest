using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class AttackStatus : StatusBase
{
	void Awake()
	{
		this.CharType = Type.eYukina;
		this.Effect = EffectManager.eEffect.ePunchAttack;
		this.Name = "ユキナ";
		this.SubName = "player1";
		this.Attack = 3;
		this.HpMax = this.Hp = 3;
		this.Move = 3;
		this.Range = 1;
		this.AttackNum = 1;
		this.CharSp = Resources.Load<Sprite>("Sprites/Char/s2");
		this.CharTopSp = Resources.Load<Sprite>("Sprites/Char/on_Yukina");
	}
}

class Ed : StatusBase
{
	void Awake()
	{
		this.CharType = Type.eEd;
		this.Effect = EffectManager.eEffect.eSlash;
		this.Name = "エド";
		this.SubName = "Player2";
		this.Attack = 5;
		this.HpMax = this.Hp = 3;
		this.Move = 2;
		this.Range = 1;
		this.AttackNum = 1;
		this.CharSp = Resources.Load<Sprite>("Sprites/Char/s4");
		this.CharTopSp = Resources.Load<Sprite>("Sprites/Char/on_Ed");
	}
}

class Lucy : StatusBase
{
	void Awake()
	{
		this.CharType = Type.eLucy;
		this.Effect = EffectManager.eEffect.eExplosionPurple;
		this.Name = "ルーシー";
		this.SubName = "player3";
		this.Attack = 3;
		this.HpMax = this.Hp = 2;
		this.Move = 2;
		this.Range = 3;
		this.AttackNum = 1;
		this.CharSp = Resources.Load<Sprite>("Sprites/Char/s3");
		this.CharTopSp = Resources.Load<Sprite>("Sprites/Char/on_Lucy");
	}
}

class Shinya : StatusBase
{
	void Awake()
	{
		this.CharType = Type.eShinya;
		this.Effect = EffectManager.eEffect.eGunAttack;
		this.Name = "シンヤ";
		this.SubName = "Player4";
		this.Attack = 4;
		this.HpMax = this.Hp = 3;
		this.Move = 2;
		this.Range = 2;
		this.AttackNum = 1;
		this.CharSp = Resources.Load<Sprite>("Sprites/Char/s1");
		this.CharTopSp = Resources.Load<Sprite>("Sprites/Char/on_Shinya");
	}
}