using UnityEngine;

public class EnemySpawnBehavior : MonoBehaviour {

	public void Spawn() {
		var enemies = GameStats.waves[GameStats.wave].enemies;
		if (enemies.Count == 0) {
			return;
		}

		var enemy = enemies[Random.Range(0, enemies.Count)];

		// Instantiate enemy
		Vector3 topPos = this.transform.position;
		topPos.y += 0.5f;
		var enemyGameObj = Instantiate(enemy.model, topPos, Quaternion.identity).gameObject;
		var enemyScript = enemyGameObj.GetComponent(typeof(EnemyBehavior)) as EnemyBehavior;
        enemyScript.enemy = enemy;
	}

}