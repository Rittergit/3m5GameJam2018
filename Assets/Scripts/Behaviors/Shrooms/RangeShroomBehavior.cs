using UnityEngine;

public class RangeShroomBehavior : ShroomBehavior {

	private new Collider collider;
	//private Animator animator;
	private GameObject hittedGameObject;
	private float attackTimer;

	protected override void Start () {
		base.Start ();

		this.collider = GetComponentInChildren<Collider> ();
		//this.animator = GetComponent<Animator> ();
		this.attackTimer = this.shroom.attackspeed;
	}

	protected override void Update () {
		base.Update ();

        this.attackTimer -= Time.deltaTime;
        if (this.attackTimer < 0)
        {
            // Reset Timer
            this.attackTimer = this.shroom.attackspeed;
            
            float nearestDist = 99999f;
            Transform target = null;

            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in enemies)
            {
                float dist = Vector3.Distance(enemy.transform.position, this.transform.position);
                if (dist < nearestDist)
                {
                    nearestDist = dist;
                    target = enemy.transform;
                }
            }

            if (target == null || this.shroom.attackrange < nearestDist)
            {
                return;
            }

            // Set Attack Animation
            //this.animator.speed = 1 / this.shroom.attackspeed;
            //this.animator.SetBool("Attack", true);

            // Should Attack
            var centerPos = this.collider.bounds.center;
            var bulletGameObj = Instantiate(this.shroom.bullet.model, centerPos, Quaternion.identity).gameObject;
            var bulletScript = bulletGameObj.GetComponent(typeof(BulletBehavior)) as BulletBehavior;
            bulletScript.bullet = this.shroom.bullet;
            bulletScript.target = target;
        }
    }

}