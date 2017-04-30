using UnityEngine;
using System.Collections;

public class CharBase : MonoBehaviour
{
	GameObject _satge;
	public GameObject Stage { get { return _satge; } set { _satge = value; } }

	// デバッグ用にいまだけpublic 
	public string _name;   // キャラクターの名前
	public int _attack;    // 攻撃力
	public int _hp;        // 体力
	public int _move;      // 移動量
	public bool _isDead;   // 死んでるかどうか ※ 生存していたら:false 死んでいたら:true
	public int _x;         // 現在位置
	public int _y;         // 現在位置

	public string Name { get { return _name; } set { _name = value; } }
	public int Attack { get { return _attack; } set { _attack = value; } }
	public int Hp { get { return _hp; } set { _hp = value; } }
	public int Move { get { return _move; } set { _move = value; } }
	public bool IsDead { get { return _isDead; } set { _isDead = value; } }
	public int X { get { return _x; } set { _x = value; } }
	public int Y { get { return _y; } set { _y = value; } }

	public enum eType
	{
		eAttacker = 0,
		eDefender,
	}

	void Update()
	{
	}

	public void SetPos(int x, int y)
	{
		_x = x;
		_y = y;
		this.transform.localPosition = _satge.GetComponent<StageBase>().GetPanelLocalPosition(_x, _y);
	}
}