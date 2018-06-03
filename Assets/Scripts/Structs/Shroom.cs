using UnityEngine;

[System.Serializable]
public struct Shroom {
	public Transform model;
    public Bullet bullet;
    public Spore spore;
	public string name;
	public int cost;
	public int health;
	public float attackspeed;
    public float attackrange;
	public float movementspeed;
	public float sporeSpawnRate;
}