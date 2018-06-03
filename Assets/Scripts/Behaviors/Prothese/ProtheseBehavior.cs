using UnityEngine;

public class ProtheseBehavior : MonoBehaviour
{

    public Prothese prothese;
    private float healthBarBackgroundLength;
    private float healthBarForegroundLength;
    //private GUIStyle healthBarStyle;

    protected virtual void Start()
    {
        healthBarBackgroundLength = Screen.width / 10;
        healthBarForegroundLength = healthBarBackgroundLength;
    }

    protected virtual void Update()
    {
        if (this.prothese.currentHealth <= 0)
        {
            Destroy(this.gameObject);
            return;
        }

        healthBarForegroundLength = healthBarBackgroundLength * (float)((float)this.prothese.currentHealth / (float)this.prothese.maxHealth);
    }

    protected void OnGUI()
    {
        var screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        //GUI.Box(new Rect(screenPos.x - (healthBarLength / 2), Screen.height - screenPos.y, healthBarLength, 20), this.shroom.currentHealth + "/" + this.shroom.maxHealth);

        var healthBarStyle = new GUIStyle(GUI.skin.box);
        healthBarStyle.normal.background = Texture2D.whiteTexture;
        healthBarStyle.normal.textColor = Color.white;
        var oldguicolor = GUI.backgroundColor;
        GUI.backgroundColor = Color.black;
        GUI.Box(new Rect(screenPos.x - (healthBarBackgroundLength / 2), Screen.height - screenPos.y, healthBarBackgroundLength, 1), "", healthBarStyle);
        GUI.backgroundColor = Color.green;
        GUI.Box(new Rect(screenPos.x - (healthBarBackgroundLength / 2), Screen.height - screenPos.y, healthBarForegroundLength, 1), "", healthBarStyle);
        GUI.backgroundColor = oldguicolor;


    }

}