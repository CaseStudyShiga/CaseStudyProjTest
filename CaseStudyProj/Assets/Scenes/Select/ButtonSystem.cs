using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// LoadSceneを使うために必要！！！！！
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ButtonSystem : MonoBehaviour
{
	int _stageID;
	public int StageID { get { return this._stageID; } set { this._stageID = value; } }

	void Awake()
	{
		_stageID = 0;
	}

	//// Use this for initialization
	void Start () {
		//------ クリックした際にオブジェクト名取得 ------
		GetGameObjName();
        //------ オブジェクトの変形アクション ------
        OpenButtonAction();
    }

    // Update is called once per frame
    void Update()
    {
    }

    //-----------------------------------------------------
    //オブジェクト名取得
    //-----------------------------------------------------
    public void GetGameObjName()
    {
        this.transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.Log(this.gameObject.name);
			SelectManager.Instance.StageID = _stageID;
		});
    }

    //-----------------------------------------------------
    //オブジェクトのアクション（動き）
    //-----------------------------------------------------
     void OpenButtonAction()
    {
        this.transform.localScale = new Vector3(0, 0, 0);

        transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 1f);
    }
}
