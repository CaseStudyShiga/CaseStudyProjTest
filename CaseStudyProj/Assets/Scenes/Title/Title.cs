using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// LoadSceneを使うために必要！！！！！
using UnityEngine.SceneManagement;
	

public class Title : MonoBehaviour
{
	void Start ()
	{
		Application.targetFrameRate = 60;

		this.InitField();
		this.InitAction();

		Fader.instance.BlackIn();
	}

	void Update ()
	{
	}

	void InitField()
	{
	}

	void InitAction()
	{
	}

	private IEnumerator DelayMethod(float waitTime, System.Action ac)
	{
		yield return new WaitForSeconds(waitTime);      // waitTime後に実行する
		ac();
		SceneManager.LoadScene("Game");					// シーン切り替え
	}
}
