using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

	public Enemy enemy;

	protected virtual void Start () {
	}

	protected virtual void Update () {
		if(this.enemy.health <= 0) {
			Destroy(this.gameObject);
		}
	}

}