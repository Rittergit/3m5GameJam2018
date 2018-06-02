using UnityEngine;

public class SporeBehavior : MonoBehaviour {

	public Spore spore;

	void Start () {
		GameStats.spores += this.spore.spores;

		var audio = this.gameObject.AddComponent<AudioSource> ();
		if (this.spore.sound != null) {
			audio.clip = this.spore.sound;
			audio.Play ();
		}

		Destroy (this.gameObject, this.spore.lifeTime);
	}

}