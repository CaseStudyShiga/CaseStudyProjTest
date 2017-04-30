using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class AttackChar : CharBase
{
	void Awake()
	{
		this.Name = "Attack Character";
		this.Attack = 5;
		this.Hp = 1;
		this.Move = 1;
	}

	public void SetData(string name, int attack, int hp, int move)
	{
		this.Name = name;
		this.Attack = attack;
		this.Hp = hp;
		this.Move = move;
	}
}