using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusBase : MonoBehaviour
{
	private Color32 DEFAULT_COL;
	private Color32 SELECT_COL;
	private Color32 BETWEEN_COL;

	GameObject _stage;
	public GameObject Stage { get { return _stage; } set { _stage = value; } }

	// デバッグ用にいまだけpublic 
	public string _name;	// キャラクターの名前
	public int _attack;		// 攻撃力
	public int _hp;			// 体力
	public int _move;		// 移動量
	public bool _isDead;	// 死んでるかどうか ※ 生存していたら:false 死んでいたら:true
	public int _x;			// 現在位置
	public int _y;			// 現在位置
	public bool _isSelect;	// 選択されているかどうか
	public bool _isBetween; // 挟まれているかどうか
	public bool _isPlayer;	// プレイヤーかどうか

	public string Name { get { return _name; } set { _name = value; } }
	public int Attack { get { return _attack; } set { _attack = value; } }
	public int Hp { get { return _hp; } set { _hp = value; } }
	public int Move { get { return _move; } set { _move = value; } }
	public bool IsDead { get { return _isDead; } set { _isDead = value; } }
	public int X { get { return _x; } set { _x = value; } }
	public int Y { get { return _y; } set { _y = value; } }
	public bool IsSelect { get { return _isSelect; } }
	public bool IsBetween { get { return _isBetween; } set { _isBetween = value; } }
	public bool IsPlayer { get { return _isPlayer; } set { _isPlayer = value; } }

	public enum eType
	{
		eAttacker = 0,
		eDefender,
	}

	void Update()
	{
		Panel panel = _stage.GetComponent<StageBase>().GetPanelData(_x, _y);
		if (panel.IsOnObj == false)
		{
			panel.IsOnObj = true;
		}
	}

	public void SetColor(Color32 d, Color32 s, Color32 b)
	{
		DEFAULT_COL = d;
		SELECT_COL = s;
		BETWEEN_COL = b;

		this.GetComponent<Image>().color = DEFAULT_COL;
	}

	public void SetPos(int x, int y)
	{
		_x = x;
		_y = y;
		_stage.GetComponent<StageBase>().GetPanelData(_x, _y).OnObj = this.transform;
		_stage.GetComponent<StageBase>().GetPanelData(_x, _y).IsOnObj = true;
		this.transform.localPosition = _stage.GetComponent<StageBase>().GetPanelLocalPosition(_x, _y);
	}

	public void SelectOn()
	{
		this._isSelect = true;
		this.transform.GetComponent<Image>().color = SELECT_COL;
	}

	public void SelectOff()
	{
		this._isSelect = false;
		this.transform.GetComponent<Image>().color = DEFAULT_COL;
	}

	public void BetweenOn()
	{
		this._isBetween = true;
		this.transform.GetComponent<Image>().color = BETWEEN_COL;
	}

	public void BetweenOff()
	{
		this._isBetween = false;
		this.transform.GetComponent<Image>().color = DEFAULT_COL;
	}
}