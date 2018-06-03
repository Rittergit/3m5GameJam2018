using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

	public Enemy enemy;

	protected virtual void Start () {
	}

	protected virtual void Update () {
		if(this.enemy.currentHealth <= 0) {
			Destroy(this.gameObject);
		}
	}

}