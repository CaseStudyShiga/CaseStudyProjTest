using UnityEngine;
using System.Collections;

class SkillBase : MonoBehaviour
{
	string _name;		// スキルの名前
	string _content;	// スキルの内容　※説明文
	int _type;			// スキルの種類 ※攻撃スキルなのか回復スキルなのか　など
	int _value;			// 各種値　※　ダメージ値や回復値

	public string Name { get { return _name; } set { _name = value; } }
	public string Content { get { return _content; } set { _content = value; } }
	public int Type { get { return _type; } set { _type = value; } }
	public int Value { get { return _value; } set { _value = value; } }
}
	