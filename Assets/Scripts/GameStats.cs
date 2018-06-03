using System.Collections.Generic;
using UnityEngine;

public static class GameStats {

	public enum GameState {MainMenu, InGame, Pause, GameOver, LevelComplete};

	public static GameState state;

	public static int wave;

	public static int spores;

    public static Shroom selectedShroom;

    public static Prothese prothese;

	public static List<Wave> waves;

	public static List<Shroom> shrooms;

    public static List<Enemy> enemies;
    
}