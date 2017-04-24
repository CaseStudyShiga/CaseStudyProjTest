using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// LoadSceneを使うために必要！！！！！
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

	private GameObject _nextSceneBtn;

	// Use this for initialization
	void Start() {
		this.InitField();
		this.InitAction();
		Fader.instance.BlackIn();
	}

	// Update is called once per frame
	void Update() {
	}

	void InitField() {
		_nextSceneBtn = this.transform.Find("Canvas/Buttons/NextButton").gameObject;
	}

	void InitAction() {
		Global.ButtonMng.SetAction(this._nextSceneBtn, () => {
			//Global.SoundMng.PlaySe("Decision");			// BGM再生開始
			Fader.instance.BlackOut();						// フェードアウト
			StartCoroutine(DelayMethod(1.2f));				// 1.2秒後に実行する
		});
	}

	private IEnumerator DelayMethod(float waitTime) {
		yield return new WaitForSeconds(waitTime);			// waitTime後に実行する
		SceneManager.LoadScene("Title");					// シーン切り替え
	}

}
