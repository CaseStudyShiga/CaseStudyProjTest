using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CutInManager : UIBase
{
	static CutInManager _instance;
	public static CutInManager Instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject obj = new GameObject("CutIn");
				_instance = obj.AddComponent<CutInManager>();
				_instance.transform.SetParent(GameObject.Find("Canvas/UI").transform);
				_instance.transform.localPosition = Vector3.zero;
			}
			return _instance;
		}
	}

	public class CutInData
	{
		GameObject _playerObj;
		GameObject _partnerObj;
		List<GameObject> _enemyList;
		List<int> _damageList;
		List<bool> _playerAttacked;
		List<bool> _partnerAttacked;

		public GameObject PlayerObj { get { return _playerObj; } set { _playerObj = value; } }
		public GameObject PartnerObj { get { return _partnerObj; } set { _partnerObj = value; } }
		public List<GameObject> EnemyList { get { return _enemyList; } set { _enemyList = value; } }
		public List<int> DamageList { get { return _damageList; } set { _damageList = value; } }
		public List<bool> PlayerAttackedList { get { return _playerAttacked; } set { _playerAttacked = value; } }
		public List<bool> PartnerAttackedList { get { return _partnerAttacked; } set { _partnerAttacked = value; } }

		public CutInData()
		{
			_enemyList = new List<GameObject>();
			_damageList = new List<int>();
		}
	}

	List<CutInData> _cutinDataList;
	GameObject _cutInFirstObj;
	GameObject _cutInSecondObj;

	void Awake()
	{
		this.InitField();
	}

	void Start()
	{
	}

	void Update()
	{
	}

	public IEnumerator CutInStart()
	{
		float delayTime = 0.15f;
		float cutInDuration = 1.0f;

		switch (GameManager.Instance.SpeedUpType)
		{
			case GameManager.SpeedUp.x1:
				delayTime = 0.15f;
				cutInDuration = 1.0f;
				break;
			case GameManager.SpeedUp.x2:
				delayTime = 0.10f;
				cutInDuration = 0.5f;
				break;
			case GameManager.SpeedUp.x3:
				delayTime = 0.05f;
				cutInDuration = 0.25f;
				break;
		}

		float nextDelayTime = (cutInDuration * 2) + delayTime;
		foreach (var data in _cutinDataList)
		{
			this.SetImage(true, data.PlayerObj.GetComponent<StatusBase>());
			this.SetImage(false, data.PartnerObj.GetComponent<StatusBase>());

			_cutInFirstObj.transform.localPosition = new Vector3(750, 300);
			_cutInSecondObj.transform.localPosition = new Vector3(-750, -400);

			// 敵に攻撃
			bool enemyNullChk = false;
			data.EnemyList.Select((enemy, idx) =>
			{
				if (enemy)
				{
					// 一つ目のカットイン
					_cutInFirstObj.transform.DOLocalMoveX(25f, cutInDuration).SetEase(Ease.InOutQuart).OnComplete(() =>
					{
						var enemyStatus = enemy.GetComponent<StatusBase>();
						var stage = enemyStatus.Stage.GetComponent<StageBase>();

						DamageUI.Instance.SetDamage(data.DamageList[idx], stage.GetPanelLocalPosition(enemyStatus.X, enemyStatus.Y));
						enemyStatus.Hp -= data.DamageList[idx];

						if (data.PlayerAttackedList[idx])
						{
							EffectManager.Instance.SetEffect(data.PlayerObj.GetComponent<StatusBase>().Effect, stage.GetPanelLocalPosition(enemyStatus.X, enemyStatus.Y));
						}

						if (data.PartnerAttackedList[idx])
						{
							EffectManager.Instance.SetEffect(data.PartnerObj.GetComponent<StatusBase>().Effect, stage.GetPanelLocalPosition(enemyStatus.X, enemyStatus.Y));
						}

						_cutInFirstObj.transform.DOLocalMoveX(-800f, cutInDuration).SetEase(Ease.InOutQuart).SetDelay(delayTime).OnComplete(() =>
						{
						});
					});

					// 二つ目のカットイン
					_cutInSecondObj.transform.DOLocalMoveX(-25f, cutInDuration).SetEase(Ease.InOutQuart).OnComplete(() =>
					{
						_cutInSecondObj.transform.DOLocalMoveX(800f, cutInDuration).SetEase(Ease.InOutQuart).SetDelay(delayTime);
					});
				}
				else
				{
					enemyNullChk = true;
				}
				return enemy;
			}).ToList();

			if (enemyNullChk)
			{
				continue;
			}

			yield return new WaitForSeconds(nextDelayTime + 0.05f);
		}

		_cutinDataList.Clear();
	}

	public void AddCutInData(GameObject player, GameObject partner, List<GameObject> enemyList, List<int> damageList, List<bool> playerAttacked, List<bool> partnerAttacked)
	{
		CutInData data = new CutInData();
		data.PlayerObj = player;
		data.PartnerObj = partner;
		data.EnemyList = enemyList;
		data.DamageList = damageList;
		data.PlayerAttackedList = playerAttacked;
		data.PartnerAttackedList = partnerAttacked;

		_cutinDataList.Add(data);
	}

	void SetImage(bool first, StatusBase status)
	{
		Image image = (first) ? _cutInFirstObj.GetComponent<Image>() : _cutInSecondObj.GetComponent<Image>();

		string path = null;
		switch (status.CharType)
		{
			case StatusBase.Type.eEd:
				path = (first) ? "Sprites/Char/cut_in_ed" : "Sprites/Char/cut_in_ed_reverse";
				break;
			case StatusBase.Type.eShinya:
				path = (first) ? "Sprites/Char/cut_in_shinya" : "Sprites/Char/cut_in_shinya_reverse";
				break;
			case StatusBase.Type.eYukina:
				path = (first) ? "Sprites/Char/cut_in_yukina" : "Sprites/Char/cut_in_yukina_reverse";
				break;
			case StatusBase.Type.eLucy:
				path = (first) ? "Sprites/Char/cut_in_lucy" : "Sprites/Char/cut_in_lucy_reverse";
				break;
		}

		image.sprite = Resources.Load<Sprite>(path);
	}

	void InitField()
	{
		_cutinDataList = new List<CutInData>();

		Vector2 size = new Vector2(750, 300);
		this._cutInFirstObj = this.CreateChild("CutInFirst", this.transform, size, new Vector3(750, 300));
		this._cutInSecondObj = this.CreateChild("CutInSecond", this.transform, size, new Vector3(-750, -400));
	}
}

