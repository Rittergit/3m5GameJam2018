using UnityEngine;

public class MeleeEnemyBehavior : EnemyBehavior {

	private new Collider collider;
    private Rigidbody rigidbody;
	//private Animator animator;
	private GameObject hittedGameObject;
	private float attackTimer;

    protected override void Start () {
		base.Start ();

		this.collider = GetComponentInChildren<Collider>();
        this.rigidbody = GetComponentInChildren<Rigidbody>();
		//this.animator = GetComponent<Animator> ();
		this.attackTimer = this.enemy.attackspeed;
    }

    private Vector3 getTarget()
    {
        float nearestdist = 99999f;
        //Vector3 nearestpos = new Vector3(0,0,0);
        Transform target=null;
        var shrooms = GameObject.FindGameObjectsWithTag("Shroom");
        int len = shrooms.GetLength(0);
        foreach(var shroom in shrooms)
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

        if(target == null)
        {
            return new Vector3(0, 0, 0);
        }
        else
        {
            return target.position;
        }


        

    }

	protected override void Update () {
		base.Update ();

        var target = getTarget();

        this.transform.position = Vector3.MoveTowards(this.transform.position, target, 0.05f);

        //// Get Objects infront
        //var centerPos = this.collider.bounds.center;
        //var fwd = this.transform.forward;
        //RaycastHit hit;
        //if (Physics.Raycast (centerPos, fwd, out hit, this.gorilla.bullet.range) && (hit.collider.tag == "Plant")) {
        //	this.hittedGameObject = hit.collider.gameObject;
        //}

        //if (this.hittedGameObject == null) {
        //	// Movement
        //	if (this.gorilla.movementspeed != 0) {
        //		this.transform.Translate ((Vector3.forward * Time.deltaTime) * this.gorilla.movementspeed);
        //	}

        //	// Reset Attack Animation
        //	this.animator.SetBool ("Attack", false);
        //}

        //this.attackTimer -= Time.deltaTime;
        //if (this.attackTimer < 0) {
        //	// Reset Timer
        //	this.attackTimer = this.gorilla.attackspeed;

        //	if (this.hittedGameObject == null) {
        //		return;
        //	}

        //	// Set Attack Animation
        //	this.animator.speed = 1 / this.gorilla.attackspeed;
        //	this.animator.SetBool ("Attack", true);

        //	// Damage Calculation
        //	var plantScript = this.hittedGameObject.GetComponentInParent(typeof(PlantBehavior)) as PlantBehavior;
        //	plantScript.plant.health -= this.gorilla.bullet.damage;
        //}
    }

    void OnCollisionEnter(Collision col)
    {
        var colGameObj = col.gameObject;
        if ((colGameObj.tag == "Shroom"))
        {
            var shroomScript = colGameObj.GetComponentInParent(typeof(ShroomBehavior)) as ShroomBehavior;
            shroomScript.shroom.health -= this.enemy.attack;
            Destroy(this.gameObject);
        }
    }

}