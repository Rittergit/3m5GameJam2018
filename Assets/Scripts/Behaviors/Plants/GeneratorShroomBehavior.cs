using UnityEngine;

public class GeneratorShroomBehavior : ShroomBehavior {

	private float sporeSpawnTimer;
	private new Collider collider;
	//private Animator animator;
	
	protected override void Start () {
		base.Start ();
		this.collider = GetComponentInChildren<Collider> ();
		//this.animator = GetComponent<Animator> ();
		this.sporeSpawnTimer = this.shroom.sporeSpawnRate;
	}

	protected override void Update () {
		base.Update ();

		this.sporeSpawnTimer -= Time.deltaTime;

		// Reset Generate Animation
		//this.animator.SetBool ("Generate", false);

		if (this.sporeSpawnTimer < 0) {
			Vector3 pos = this.collider.bounds.center;
			pos.x += this.collider.bounds.extents.x;
			var sporeGameObj = Instantiate(this.shroom.spore.model, pos, Quaternion.identity).gameObject;
			var sporeScript = sporeGameObj.GetComponent(typeof(SporeBehavior)) as SporeBehavior;
            sporeScript.spore = this.shroom.spore;

			// Generate Animation
			//this.animator.SetBool ("Generate", true);

			// Reset Timer
			this.sporeSpawnTimer = this.shroom.sporeSpawnRate;
		}
	}
}
