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

	protected override void Update () {
		base.Update ();
        
        var Target = new Vector3(0, 0, 0);

        var actualpos = rigidbody.position;
        var actualspeed = rigidbody.velocity;
        
        var speedvec = actualpos - Target;
        var speed = Mathf.Sqrt(speedvec.x * speedvec.x + speedvec.y * speedvec.y);
        rigidbody.velocity = new Vector3(-actualpos.x / speed, actualspeed.y, -actualpos.z / speed);
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

}