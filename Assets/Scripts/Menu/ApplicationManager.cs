using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationManager : MonoBehaviour {

	public string sceneName;

	public void LoadSceneByName(string name) {
		SceneManager.LoadScene (name);
	}

	public void LoadScene() {
		SceneManager.LoadScene (this.sceneName);
	}

	public void ApplicationExit() {
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}

}