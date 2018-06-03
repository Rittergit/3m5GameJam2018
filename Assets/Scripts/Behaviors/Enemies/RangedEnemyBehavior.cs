using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyBehavior : EnemyBehavior
{

    private new Collider collider;
    private Rigidbody rigidbody;
    //private Animator animator;
    private GameObject hittedGameObject;
    private float attackTimer;

    protected override void Start()
    {
        base.Start();

        this.collider = GetComponentInChildren<Collider>();
        this.rigidbody = GetComponentInChildren<Rigidbody>();
        //this.animator = GetComponent<Animator> ();
        this.attackTimer = this.enemy.attackspeed;
    }

    private Transform getTarget()
    {
        float nearestdist = 99999f;
        Transform target = null;
        var shrooms = GameObject.FindGameObjectsWithTag("Shroom");
        int len = shrooms.GetLength(0);
        foreach (var shroom in shrooms)
        {
            var posS = shroom.transform.position;
            var posE = this.transform.position;
            var actualdist = Vector3.Distance(posS, posE);
            if (actualdist < nearestdist)
            {
                nearestdist = actualdist;
                target = shroom.transform;
            }
        }

        if (nearestdist > 10)
        {
            target = null;
        }

        if (target == null)
        {
            var prothese = GameObject.FindGameObjectWithTag("Prothese");
            if (prothese != null)
            {
                return prothese.transform;
            } else
            {
                return null;
            }
        }

        return target;
    }

    protected override void Update()
    {
        base.Update();

        var target = getTarget();
        if (target == null)
        {
            Destroy(this.transform.gameObject);
            return;
        }

        if (Vector3.Distance(target.position, this.transform.position) < 6)
        {
            this.attackTimer -= Time.deltaTime;
            if (this.attackTimer < 0)
            {
                // Reset Timer
                this.attackTimer = this.enemy.attackspeed;
                var bulletGameObj = Instantiate(this.enemy.bullet.model, this.transform.position, Quaternion.identity).gameObject;
                var bulletScript = bulletGameObj.GetComponent(typeof(EnemyBulletBehavior)) as EnemyBulletBehavior;
                bulletScript.bullet = this.enemy.bullet;
                bulletScript.target = target;
            }
        }
        else
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, target.position, this.enemy.movementSpeed);
        }
    }

}
