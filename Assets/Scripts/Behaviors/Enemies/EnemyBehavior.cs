using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

    public Enemy enemy;
    public bool isUnplaced;
    private bool isMouseOver;
    private float healthBarBackgroundLength;
    private float healthBarForegroundLength;

    protected virtual void Start()
    {
        healthBarBackgroundLength = Screen.width / 15;
        healthBarForegroundLength = healthBarBackgroundLength;
    }

    protected virtual void Update()
    {
        if (this.enemy.currentHealth <= 0)
        {
            Destroy(this.gameObject);
            return;
        }

        if (isUnplaced || !isMouseOver)
        {
            return;
        }

        healthBarForegroundLength = healthBarBackgroundLength * (float)((float)this.enemy.currentHealth / (float)this.enemy.maxHealth);
    }

    protected void OnGUI()
    {
        if (isUnplaced || !isMouseOver)
        {
            return;
        }

        var screenPos = Camera.main.WorldToScreenPoint(this.transform.position);

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

    protected void OnMouseOver()
    {
        this.isMouseOver = true;
    }

    protected void OnMouseExit()
    {
        this.isMouseOver = false;
    }

}