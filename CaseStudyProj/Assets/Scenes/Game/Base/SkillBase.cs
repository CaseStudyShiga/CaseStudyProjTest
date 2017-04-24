using UnityEngine;
using System.Collections;

class SkillBase
{
	string _name;
	string _content;
	int _type;
	int _value;

	public string Name { get { return _name; } set { _name = value; } }
	public string Content { get { return _content; } set { _content = value; } }
	public int Type { get { return _type; } set { _type = value; } }
	public int Value { get { return _value; } set { _value = value; } }
}
	