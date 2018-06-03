using UnityEngine;

public class ProtheseBehavior : MonoBehaviour {

	public Prothese prothese;

	protected virtual void Start () {
	}

	protected virtual void Update () {
		if (this.prothese.health <= 0) {
			Destroy (this.gameObject);
		}
	}

}