using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class EnemyStatus0 : StatusBase
{
	void Awake()
	{
		this.Name = "村瀬克磨";
		this.Attack = 1;
		this.HpMax = this.Hp = 9;
		this.Move = 2;
		this.Range = 1;
		this.CharSp = Resources.Load<Sprite>("Sprites/Char/enemy0");
	}
}