﻿using UnityEngine;

[System.Serializable]
public struct Enemy {
	public Transform model;
	public AudioClip sound;
    public Bullet bullet;
	public int health;
    public int attack;
    public float attackspeed;
	public float movementSpeed;
    public float movementRange;
}