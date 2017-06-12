using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

class AreaSelect : UIBase
{
	readonly Vector2 AreaBtnSize = new Vector2(600, 170);

	Transform _select;
	GameObject _background;
	List<GameObject> _area;

	void Start()
	{
		this.InitField();
	}

	void Update()
	{
	}

	void AreaBtn(Area area)
	{
		var s = this._select.GetComponent<Select>();
		s.SetBackgroundType(area.ID);
		s.SetStageSelectActive(true, area);
	}

	void InitField()
	{
		this._select = this.transform.parent;
		this._background = this.CreateChild("BackGround", this.transform, new Vector2(750, 1334), Vector3.zero, Resources.Load<Sprite>("Sprites/GUI/areaSelectUI"));

		this._area = new List<GameObject>();
		SelectManager.Instance.AreaList.Select((data, i) =>
		{
			GameObject area = this.CreateButton("Area" + i.ToString(), this.transform, AreaBtnSize, new Vector3(0, 330 - (i * 200)), Resources.Load<Sprite>("Sprites/GUI/areaSelectUI_panelButton"), () => 
			{
				this.AreaBtn(data);
			});

			GameObject text = this.CreateText("Text", data.Name, area.transform, new Vector3(0, 31), 60, false);
			text.GetComponent<RectTransform>().sizeDelta = new Vector2(500,100);

			string star = "";
			for (int c = 0; c < data.Difficulty; c++)
			{
				star += "★";
			}

			GameObject dificlut = this.CreateText("Dificlut", star, area.transform, new Vector3(0, -25), 40, false);
			this._area.Add(area);

			return data;
		}).ToList();

		this._background.transform.SetAsFirstSibling();
	}
}

