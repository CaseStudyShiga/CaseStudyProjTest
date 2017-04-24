using UnityEngine;
using System.Collections;

public class CharBase
{
	string _name;
	int _attack;
	int _hp;
	int _move;

	public string Name { get { return _name; } set { _name = value; } }
	public int Attack { get { return _attack; } set { _attack = value; } }
	public int Hp { get { return _hp; } set { _hp = value; } }
	public int Move { get { return _move; } set { _move = value; } }
}