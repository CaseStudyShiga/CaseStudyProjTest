using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// LoadSceneを使うために必要！！！！！
using UnityEngine.SceneManagement;
	

public class Title : MonoBehaviour {

	GameObject _nextSceneBtn;
	GameObject _nextSceneBtn2;

	// Use this for initialization
	void Start () {
		this.InitField();
		this.InitAction();

		Fader.instance.BlackIn();
	}

	// Update is called once per frame
	void Update () {
		
	}

	void InitField() {
		_nextSceneBtn = this.transform.Find("Canvas/Buttons/NextButton").gameObject;
		_nextSceneBtn2 = this.transform.Find("Canvas/Buttons/NextButton2").gameObject;
	}

	void InitAction() {
		ButtonManager.Instance.SetAction(this._nextSceneBtn, () => {
			//SoundManager.Instance.PlaySe("Decision");
			Fader.instance.BlackOut();
			StartCoroutine(DelayMethod(Fader.instance.FadeTime, () => { CSVDataReader.Instance.Load("stage00"); }));			// 1.2秒後に実行する
		});

		ButtonManager.Instance.SetAction(this._nextSceneBtn2, () => {
			//SoundManager.Instance.PlaySe("Decision");
			Fader.instance.BlackOut();
			StartCoroutine(DelayMethod(Fader.instance.FadeTime, ()=> { CSVDataReader.Instance.Load("stage01"); }));          // 1.2秒後に実行する
		});
	}

	private IEnumerator DelayMethod(float waitTime, System.Action ac) {
		yield return new WaitForSeconds(waitTime);      // waitTime後に実行する
		ac();
		SceneManager.LoadScene("Game");					// シーン切り替え
	}
}
