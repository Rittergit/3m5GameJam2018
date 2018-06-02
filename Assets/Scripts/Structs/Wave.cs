using System.Collections.Generic;

[System.Serializable]
public struct Wave {
	public List<Enemy> enemies;
	public float spawnRateMin;
	public float spawnRateMax;
	public float time;
}