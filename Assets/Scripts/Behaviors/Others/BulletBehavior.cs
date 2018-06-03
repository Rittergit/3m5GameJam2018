using UnityEngine;

public class BulletBehavior : MonoBehaviour
{

    public Bullet bullet;
    public Transform target;

    void Start()
    {
        var audio = this.gameObject.AddComponent<AudioSource>();
        if (this.bullet.sound != null)
        {
            audio.clip = this.bullet.sound;
            audio.Play();
        }
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }

        this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, this.bullet.speed);
    }

    void OnCollisionEnter(Collision col)
    {
        var colGameObj = col.gameObject;
        if (colGameObj.tag == "Enemy")
        {
            var enemyScript = colGameObj.GetComponentInParent(typeof(EnemyBehavior)) as EnemyBehavior;
            enemyScript.enemy.currentHealth -= this.bullet.damage;
            Destroy(this.gameObject);
        }
    }
}
