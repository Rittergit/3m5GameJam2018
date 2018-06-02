using UnityEngine;

public class BulletBehavior : MonoBehaviour {

	public Bullet bullet;
	public string hitTag;
    public Enemy ememy;
    
	void Start () {
		var audio = this.gameObject.AddComponent<AudioSource>();
		if (this.bullet.sound != null) {
			audio.clip = this.bullet.sound;
			audio.Play ();
		}
	}

	void Update () {
		
	}

	void OnCollisionEnter (Collision col) {
		var colGameObj = col.gameObject;
		if ((colGameObj.tag == "Enemy") && (colGameObj.tag == this.hitTag)) {
			var enemyScript = colGameObj.GetComponentInParent(typeof(EnemyBehavior)) as EnemyBehavior;
            enemyScript.enemy.health -= this.bullet.damage;
			Destroy(this.gameObject);
		}
	}
}
