using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Partner
{
	public GameObject PartnerObj;
	public int Dir;

	public Partner(GameObject obj, int dir)
	{
		PartnerObj = obj;
		Dir = dir;
	}
}

public class StatusBase : MonoBehaviour
{
	public enum Type
	{
		eEd,
		eLucy,
		eShinya,
		eYukina,
	};

	Color32 DEFAULT_COL = Color.white;
	Color32 SELECT_COL;
	Color32 BETWEEN_COL;
	Color32 MOVED_COL = new Color32(150, 150, 150, 255);

	GameObject _stage;
	public GameObject Stage { get { return _stage; } set { _stage = value; } }

	int _index;
	EffectManager.eEffect _effect;
	string _name;					// キャラクターの名前
	string _subName;				// サブネーム
	int _attack;					// 攻撃力
	int _hp;						// 体力
	int _hpMax;						// 最大体力
	int _move;						// 移動量
	int _x;							// 現在位置
	int _y;							// 現在位置
	bool _isSelect;                 // 選択されているかどうか
	bool _isSelectFrame;			// 選択されているやつにフレームを出すフラグ
	bool _isBetween;				// 挟まれているかどうか
	bool _isPlayer;					// プレイヤーかどうか
	int _range;						// 射程
	bool _isMoved;					// 行動済みかどうか
	Sprite _charSp;					// キャライメージ
	Sprite _charTopSp;				// キャラのTopUIに出すイメージ
	int _damage;					// 受けるダメージ値
	int _attackNum;					// 攻撃回数
	List<Partner> _partnerList;		// 挟んでいる相方
	Type _type;						// キャラタイプ

	public int Index { get { return _index; } set { _index = value; } }
	public EffectManager.eEffect Effect { get { return _effect; } set { _effect = value; } }
	public string Name { get { return _name; } set { _name = value; } }
	public string SubName { get { return _subName; } set { _subName = value; } }
	public int Attack { get { return _attack; } set { _attack = value; } }
	public int Hp { get { return _hp; } set { _hp = value; } }
	public int HpMax { get { return _hpMax; } set { _hpMax = value; } }
	public int Move { get { return _move; } set { _move = value; } }
	public int X { get { return _x; } set { _x = value; } }
	public int Y { get { return _y; } set { _y = value; } }
	public int Range { get { return _range; } set { _range = value; } }
	public bool IsPlayer { get { return _isPlayer; } set { _isPlayer = value; } }
	public bool IsSelect { get { return _isSelect; } }
	public bool IsSelectFrame { get { return _isSelectFrame; } set { _isSelectFrame = value; } }
	public bool IsBetween { get { return _isBetween; } }
	public bool IsMoved { get { return _isMoved; } }
	public Sprite CharSp { get { return _charSp; } set { _charSp = value; } }
	public Sprite CharTopSp { get { return _charTopSp; }  set { _charTopSp = value; } }
	public int Damage { get { return _damage; } set { _damage = value; } }
	public int AttackNum { get { return _attackNum; } set { _attackNum = value; } }
	public List<Partner> PartnerList { get { return _partnerList; } set { _partnerList = value; } }
	public Type CharType { get { return _type; } set { _type = value; } }

	void Update()
	{
		if (this._isMoved)
		{
			this.GetComponent<Image>().color = MOVED_COL;
		}

		if (this._hp <= 0)
		{
			var stage = _stage.GetComponent<StageBase>();
			stage.GetPanelData(_x, _y).DataReset();
			Destroy(this.gameObject);
		}
	}

	public void InitField()
	{
		this._index = 0;
		this._isSelect = false;
		this._isBetween = false;
		this._isPlayer = false;
		this._isMoved = false;
		//this._dir = new List<int>() { };
		this._damage = 0;
		//this._partnerList = new List<GameObject>() { };
		this._partnerList = new List<Partner>() { };
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
}