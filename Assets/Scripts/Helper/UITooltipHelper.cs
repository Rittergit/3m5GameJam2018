using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITooltipHelper : MonoBehaviour {

	public GUISkin skin;
	public bool visible = true;

	private string labelText;
	private Color labelColor = Color.white;
	private int labelFontSize = 150;
	private float labelFadingSpeed = 0.02f;
	private float labelFadingRate = 0.1f;
	private float labelAlpha;

	void OnGUI() {
		if (!this.visible) {
			return;
		}

		this.labelColor.a = this.labelAlpha;
		GUIStyle style = new GUIStyle(skin.label);
		style.font = skin.font;
		style.normal.textColor = this.labelColor;
		style.alignment = TextAnchor.MiddleCenter;
		style.fontSize = this.labelFontSize;
		GUI.Label (new Rect (0, 0, Screen.width, Screen.height), this.labelText, style);
	}

	public void Draw() {
		StartCoroutine ("Fade");
	}

	public void SetText (string text) {
		this.labelText = text;
	}

	public void SetColor (Color color) {
		this.labelColor = color;
	}

	public void SetFontSize (int fontSize) {
		this.labelFontSize = fontSize;
	}

	public void SetFadingSpeed (float fadingSpeed) {
		this.labelFadingSpeed = fadingSpeed;
	}

	public void SetFadingRate (float fadingRate) {
		this.labelFadingRate = fadingRate;
	}

	IEnumerator Fade() {
		for (var f = 1f; f >= 0f; f -= this.labelFadingSpeed) {
			this.labelAlpha = f;
			yield return new WaitForSeconds (this.labelFadingRate);
		}
		this.labelAlpha = 0f;
	}
}
