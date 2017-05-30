using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StatusBase : MonoBehaviour
{
	Color32 DEFAULT_COL = Color.white;
	Color32 SELECT_COL;
	Color32 BETWEEN_COL;
	Color32 MOVED_COL = new Color32(150, 150, 150, 255);

	GameObject _stage;
	public GameObject Stage { get { return _stage; } set { _stage = value; } }

	// デバッグ用にいまだけpublic 
	string _name;	// キャラクターの名前
	int _attack;		// 攻撃力
	int _hp;         // 体力
	int _hpMax;      // 最大体力
	int _move;		// 移動量
	int _x;			// 現在位置
	int _y;          // 現在位置
	bool _isSelect;	// 選択されているかどうか
	bool _isBetween; // 挟まれているかどうか
	bool _isPlayer;  // プレイヤーかどうか
	int _range;      // 射程
	bool _isMoved;   // 行動済みかどうか
	List<int> _dir;  // 攻撃すべき方向（敵がいる方向）
	Sprite _charSp;  // キャライメージ
	int _damage;     // 受けるダメージ値
	int _attackNum;	// 攻撃回数

	public string Name { get { return _name; } set { _name = value; } }
	public int Attack { get { return _attack; } set { _attack = value; } }
	public int Hp { get { return _hp; } set { _hp = value; } }
	public int HpMax { get { return _hpMax; } set { _hpMax = value; } }
	public int Move { get { return _move; } set { _move = value; } }
	public int X { get { return _x; } set { _x = value; } }
	public int Y { get { return _y; } set { _y = value; } }
	public int Range { get { return _range; } set { _range = value; } }
	public bool IsPlayer { get { return _isPlayer; } set { _isPlayer = value; } }
	public bool IsSelect { get { return _isSelect; } }
	public bool IsBetween { get { return _isBetween; } }
	public bool IsMoved { get { return _isMoved; } }
	public List<int> Dir { get { return _dir; } set { _dir = value; } }
	public Sprite CharSp { get { return _charSp; } set { _charSp = value; } }
	public int Damage { get { return _damage; } set { _damage = value; } }
	public int AttackNum { get { return _attackNum; } }

	void Update()
	{
		if (this._isMoved)
		{
			this.GetComponent<Image>().color = MOVED_COL;
		}

		if (this._hp <= 0)
		{
			Destroy(this.gameObject);
		}
	}

	public void InitField()
	{
		this._isSelect = false;
		this._isBetween = false;
		this._isPlayer = false;
		this._isMoved = false;
		this._dir = new List<int>() { };
		this._damage = 0;
		this._attackNum = 1;
	}

	void SettingDefaultCol()
	{
		// Imageの色変更
		var image = this.transform.GetComponent<Image>();
		DOTween.To(
			() => image.color,              // 何を対象にするのか
			color => image.color = color,   // 値の更新
			DEFAULT_COL,                    // 最終的な値
			0.15f                            // アニメーション時間
		);
	}

	public void SetColor(Color32 s, Color32 b)
	{
		SELECT_COL = s;
		BETWEEN_COL = b;

		this.GetComponent<Image>().color = DEFAULT_COL;
	}

	public void SetPos(int x, int y)
	{
		if (this._isMoved == false)
		{
			this._x = x;
			this._y = y;
			this._stage.GetComponent<StageBase>().GetPanelData(_x, _y).OnObj = this.transform;
			this._isMoved = true;

			// Tween Plugins Use
			Vector3 nextpos = _stage.GetComponent<StageBase>().GetPanelLocalPosition(_x, _y);
			this.transform.DOLocalMove(nextpos, 0.1f).SetEase(Ease.InOutQuart);
		}
	}

	public void SelectOn()
	{
		if (this._isMoved == false)	
		{
			this._isSelect = true;
			this.transform.GetComponent<Image>().color = SELECT_COL;
		}
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
		this.SettingDefaultCol();
	}

	public void MovedOff()
	{
		this._isMoved = false;

		if(this._isBetween == false)
			this.SettingDefaultCol();
	}

	public void SetEffect(Vector3 v)
	{
		var a = Resources.Load<GameObject>("Prehabs/Effect/effect_gunattack");

		GameObject gameObj = Instantiate(a) as GameObject;
		gameObj.transform.SetParent(this.Stage.transform.parent);
		gameObj.GetComponent<RectTransform>().localPosition = v;
		gameObj.GetComponent<RectTransform>().localScale = Vector3.one * 100;
	}
}