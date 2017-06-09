using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// LoadSceneを使うために必要！！！！！
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

	private GameObject _nextSceneBtn;

	void Start() {
		Application.targetFrameRate = 60;

		SaveManager.Instance.Load();
		this.InitField();
		this.InitAction();
		GameManager.Instance.LoadConfigData();
		Fader.instance.BlackIn();
	}

	void Update() {
	}

	void InitField() {
		_nextSceneBtn = this.transform.Find("Canvas/Buttons/NextButton").gameObject;
	}

	void InitAction() {
		ButtonManager.Instance.SetAction(this._nextSceneBtn, () => {
			//Global.SoundMng.PlaySe("Decision");			// BGM再生開始
			Fader.instance.BlackOut();						// フェードアウト
			StartCoroutine(DelayMethod(Fader.instance.FadeTime));				// 1.2秒後に実行する
		});
	}

	private IEnumerator DelayMethod(float waitTime) {
		yield return new WaitForSeconds(waitTime);			// waitTime後に実行する
		SceneManager.LoadScene("Title");					// シーン切り替え
	}
}