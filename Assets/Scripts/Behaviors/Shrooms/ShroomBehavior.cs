using UnityEngine;

public class ShroomBehavior : MonoBehaviour {

	public Shroom shroom;

	protected virtual void Start () {
	}

	protected virtual void Update () {
		if (this.shroom.health <= 0) {
			Destroy (this.gameObject);
		}
	}

}