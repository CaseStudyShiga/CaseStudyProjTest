using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class AttackStatus : StatusBase
{
	void Awake()
	{
		this.Name = "Attack Character";
		this.Attack = 5;
		this.HpMax = this.Hp = 1;
		this.Move = 3;
		this.Range = 2;
	}

	public void SetData(string name, int attack, int hp, int move)
	{
		this.Name = name;
		this.Attack = attack;
		this.HpMax = this.Hp = 1;
		this.Move = move;
	}
}