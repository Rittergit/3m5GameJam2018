using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalBehavior : MonoBehaviour {
	
	public List<string> levels;

	void Start () {
		GlobalStats.levels = this.levels;
	}

}