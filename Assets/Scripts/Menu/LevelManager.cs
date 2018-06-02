using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public Transform levelSelectionButton;

	void Start() {
		if (GlobalStats.levels == null) {
			return;
		}

		foreach (var level in GlobalStats.levels) {
			var btn = Instantiate (levelSelectionButton, this.transform);

			// set button text
			var btnText = btn.GetComponentInChildren (typeof(Text)) as Text;
			btnText.text = level;

			// set level load on click function
			var am = btn.GetComponent (typeof(ApplicationManager)) as ApplicationManager;
			am.sceneName = level;
		}
	}

}