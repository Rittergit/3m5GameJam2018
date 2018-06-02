using UnityEngine;

[System.Serializable]
public struct Bullet {
	public Transform model;
	public AudioClip sound;
	public int damage;
	public float speed;
	public float range;
}