using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class StatusBase : MonoBehaviour
{
	private Color32 DEFAULT_COL;
	private Color32 SELECT_COL;
	private Color32 BETWEEN_COL;
	private Color32 MOVED_COL;

	GameObject _stage;
	public GameObject Stage { get { return _stage; } set { _stage = value; } }

	// デバッグ用にいまだけpublic 
	public string _name;	// キャラクターの名前
	public int _attack;		// 攻撃力
	public int _hp;         // 体力
	public int _hpMax;      // 最大体力
	public int _move;		// 移動量
	public bool _isDead;	// 死んでるかどうか ※ 生存していたら:false 死んでいたら:true
	public int _x;			// 現在位置
	public int _y;          // 現在位置
	public int _oldX;       // 前回位置
	public int _oldY;       // 前回位置
	public bool _isSelect;	// 選択されているかどうか
	public bool _isBetween; // 挟まれているかどうか
	public bool _isPlayer;  // プレイヤーかどうか
	public int _range;		// 射程

	public string Name { get { return _name; } set { _name = value; } }
	public int Attack { get { return _attack; } set { _attack = value; } }
	public int Hp { get { return _hp; } set { _hp = value; } }
	public int HpMax { get { return _hpMax; } set { _hpMax = value; } }
	public int Move { get { return _move; } set { _move = value; } }
	public bool IsDead { get { return _isDead; } set { _isDead = value; } }
	public int X { get { return _x; } set { _x = value; } }
	public int Y { get { return _y; } set { _y = value; } }
	public int OldX { get { return _oldX; } set { _oldX = value; } }
	public int OldY { get { return _oldY; } set { _oldY = value; } }
	public bool IsSelect { get { return _isSelect; } }
	public bool IsBetween { get { return _isBetween; } set { _isBetween = value; } }
	public bool IsPlayer { get { return _isPlayer; } set { _isPlayer = value; } }
	public int Range { get { return _range; } set { _range = value; } }

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

	public void SetColor(Color32 d, Color32 s, Color32 b, Color32 moved)
	{
		DEFAULT_COL = d;
		SELECT_COL = s;
		BETWEEN_COL = b;
		MOVED_COL = moved;

		this.GetComponent<Image>().color = DEFAULT_COL;
	}

	public void SetPos(int x, int y)
	{
		_oldX = _x;
		_oldY = _y;
		_x = x;
		_y = y;
		_stage.GetComponent<StageBase>().GetPanelData(_x, _y).OnObj = this.transform;
		_stage.GetComponent<StageBase>().GetPanelData(_x, _y).IsOnObj = true;

		// Tween Plugins Use
		Vector3 nextpos = _stage.GetComponent<StageBase>().GetPanelLocalPosition(_x, _y);
		this.transform.DOLocalMove(nextpos, 0.1f).SetEase(Ease.InOutQuart);
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

		// Imageの色変更
		var image = this.transform.GetComponent<Image>();
		DOTween.To(
			() => image.color,				// 何を対象にするのか
			color => image.color = color,	// 値の更新
			BETWEEN_COL,					// 最終的な値
			0.15f							// アニメーション時間
		);
	}

	public void BetweenOff()
	{
		this._isBetween = false;

		// Imageの色変更
		var image = this.transform.GetComponent<Image>();
		DOTween.To(
			() => image.color,              // 何を対象にするのか
			color => image.color = color,   // 値の更新
			DEFAULT_COL,                    // 最終的な値
			0.15f                            // アニメーション時間
		);
	}
}