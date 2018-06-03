using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehavior : MonoBehaviour
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
        if (colGameObj.tag == "Shroom")
        {
            var shroomScript = colGameObj.GetComponentInParent(typeof(ShroomBehavior)) as ShroomBehavior;
            shroomScript.shroom.currentHealth -= this.bullet.damage;
            Destroy(this.gameObject);
        }
        else if (colGameObj.tag == "Prothese")
        {
            var protheseScript = colGameObj.GetComponentInParent(typeof(ProtheseBehavior)) as ProtheseBehavior;
            protheseScript.prothese.currentHealth -= this.bullet.damage;
            Destroy(this.gameObject);
        }
    }
}
