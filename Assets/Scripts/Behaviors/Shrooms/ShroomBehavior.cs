using UnityEngine;

public class ShroomBehavior : MonoBehaviour {

	public Shroom shroom;
    private float healthBarLength;

    protected virtual void Start ()
    {
        healthBarLength = Screen.width / 15;
    }

	protected virtual void Update () {
		if (this.shroom.currentHealth <= 0) {
			Destroy (this.gameObject);
            return;
		}
    }
    
    void OnGUI()
    {
        var screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        GUI.Box(new Rect(screenPos.x - (healthBarLength / 2), Screen.height - screenPos.y, healthBarLength, 20), this.shroom.currentHealth + "/" + this.shroom.maxHealth);
    }

}