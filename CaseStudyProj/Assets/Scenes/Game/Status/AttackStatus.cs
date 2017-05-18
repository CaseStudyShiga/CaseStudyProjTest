using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class AttackStatus : StatusBase
{
	void Awake()
	{
		this.Name = "Attack Character";
		this.Attack = 5;
		this.HpMax = this.Hp = 9;
		this.Move = 3;
		this.Range = 2;
		this.CharSp = Resources.Load<Sprite>("Sprites/Char/char0");
	}
}