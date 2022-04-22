using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour {

	private bool loadScene = false;
	public string LoadingSceneName;
	public Slider sliderBar;

	void Start () {
		sliderBar.gameObject.SetActive(false);
	}
		
	void Update () {
		if (loadScene == false)
		{
			loadScene = true;
			sliderBar.gameObject.SetActive(true);
			StartCoroutine(LoadNewScene(LoadingSceneName));
		}
	}
		
	IEnumerator LoadNewScene(string sceneName) {
		AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
		while (!async.isDone)
		{
			float progress = Mathf.Clamp01(async.progress / 0.9f);
			sliderBar.value = progress;
			yield return null;

		}

	}

}
