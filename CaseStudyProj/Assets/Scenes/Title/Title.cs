using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// LoadSceneを使うために必要！！！！！
using UnityEngine.SceneManagement;
	

public class Title : MonoBehaviour {

	private GameObject _nextSceneBtn;

	// Use this for initialization
	void Start () {
		Global.SetButtonManager();
		Global.SetSoundManager();
		Global.SetGameManager();

		this.InitField();
		this.InitAction();

		Fader.instance.BlackIn();
	}

	// Update is called once per frame
	void Update () {
		
	}

	void InitField() {
		_nextSceneBtn = this.transform.Find("Canvas/Buttons/NextButton").gameObject;
	}

	void InitAction() {
		Global.ButtonMng.SetAction(this._nextSceneBtn, () => {
			//Global.SoundMng.PlaySe("Decision");		// BGM再生開始
			Fader.instance.BlackOut();					// フェードアウト
			StartCoroutine(DelayMethod(1.2f));			// 1.2秒後に実行する
		});
	}

	private IEnumerator DelayMethod(float waitTime) {
		yield return new WaitForSeconds(waitTime);		// waitTime後に実行する
		SceneManager.LoadScene("Game");					// シーン切り替え
	}
}
