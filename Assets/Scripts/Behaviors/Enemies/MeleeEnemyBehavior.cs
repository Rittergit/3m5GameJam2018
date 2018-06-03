using UnityEngine;

public class MeleeEnemyBehavior : EnemyBehavior
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

    private Vector3 getTarget()
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

        if (nearestdist > this.enemy.movementRange)
        {
            target = null;
        }

        if (target == null)
        {
            return new Vector3(0, 0, 0);
        }
        else
        {
            return target.position;
        }
    }

    protected override void Update()
    {
        base.Update();

        var target = getTarget();

        this.transform.position = Vector3.MoveTowards(this.transform.position, target, this.enemy.movementSpeed);
    }

    void OnCollisionEnter(Collision col)
    {
        var colGameObj = col.gameObject;
        if (colGameObj.tag == "Shroom")
        {
            var shroomScript = colGameObj.GetComponentInParent(typeof(ShroomBehavior)) as ShroomBehavior;
            shroomScript.shroom.currentHealth -= this.enemy.attack;
            Destroy(this.gameObject);
        }
        else if (colGameObj.tag == "Prothese")
        {
            var protheseScript = colGameObj.GetComponentInParent(typeof(ProtheseBehavior)) as ProtheseBehavior;
            protheseScript.prothese.currentHealth -= this.enemy.attack;
            Destroy(this.gameObject);
        }
    }

}