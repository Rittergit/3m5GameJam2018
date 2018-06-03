using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehavior : MonoBehaviour
{

    public int spores;
    public float waveStartDelay;
    public Prothese prothese;
    public List<Shroom> shrooms;
    public List<Wave> waves;
    private KeyCode shroomSpawnHotkey = KeyCode.B;

    private Transform currentShroom;
    private float mouseWheelRotation;
    private bool waitToNextWave;
    private float nextWaveTime;
    private float enemySpawnTimer;

    void Start()
    {
        GameStats.state = GameStats.GameState.InGame;
        GameStats.spores = this.spores;
        GameStats.wave = -1;
        var protheseGameObj = Instantiate(this.prothese.model);
        var protheseScript = protheseGameObj.GetComponent(typeof(ProtheseBehavior)) as ProtheseBehavior;
        protheseScript.prothese = this.prothese;
        GameStats.prothese = this.prothese;
        GameStats.shrooms = this.shrooms;
        GameStats.waves = this.waves;
        this.nextWaveTime = this.waveStartDelay;
        this.enemySpawnTimer = this.waveStartDelay;
    }

    void Update()
    {
        this.UpdateTimeScale();
        this.UpdateProthese();
        this.UpdateWaves();
        this.UpdateEnemySpawner();
        this.UpdateShroomSpawner();
    }

    void UpdateTimeScale()
    {
        switch (GameStats.state)
        {
            case GameStats.GameState.MainMenu:
                Time.timeScale = 1;
                break;
            case GameStats.GameState.InGame:
                Time.timeScale = 1;
                break;
            case GameStats.GameState.Pause:
                Time.timeScale = 0;
                break;
            case GameStats.GameState.GameOver:
                Time.timeScale = 0;
                break;
            case GameStats.GameState.LevelComplete:
                Time.timeScale = 0;
                break;
            default:
                break;
        }
    }

    void UpdateProthese()
    {
        var protheseGameObj = GameObject.FindGameObjectWithTag("Prothese");
        if(protheseGameObj == null)
        {
            return;
        }

        var protheseScript = protheseGameObj.GetComponent(typeof(ProtheseBehavior)) as ProtheseBehavior;
        GameStats.prothese = protheseScript.prothese;

        if(GameStats.prothese.currentHealth <= 0)
        {
            GameStats.state = GameStats.GameState.GameOver;
        }
    }

    void UpdateWaves()
    {
        if (this.waves.Count == 0)
        {
            return;
        }

        if (GameStats.wave == this.waves.Count)
        {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length == 0)
            {
                GameStats.state = GameStats.GameState.LevelComplete;
            }
            return;
        }

        if (this.waitToNextWave)
        {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length == 0)
            {
                // set wait flag
                this.waitToNextWave = false;

                // Next Wave
                ++GameStats.wave;

                // Do not do shit if last wave is done
                if (GameStats.wave == this.waves.Count)
                {
                    return;
                }

                // Reset Timer
                this.nextWaveTime = this.waves[GameStats.wave].time;
            }
            return;
        }

        this.nextWaveTime -= Time.deltaTime;
        if (this.nextWaveTime < 0)
        {
            // Set wait flag
            this.waitToNextWave = true;
        }
    }

    void UpdateEnemySpawner()
    {
        if (GameStats.waves == null || GameStats.waves.Count == 0)
        {
            return;
        }

        if (GameStats.wave == GameStats.waves.Count)
        {
            return;
        }

        if (this.waitToNextWave)
        {
            return;
        }

        this.enemySpawnTimer -= Time.deltaTime;
        if (this.enemySpawnTimer < 0)
        {
            var enemySpawns = GameObject.FindGameObjectsWithTag("EnemySpawn");
            if (enemySpawns.Length == 0)
            {
                return;
            }

            var enemySpawnObj = enemySpawns[Random.Range(0, enemySpawns.Length)];
            var enemySpawnScript = enemySpawnObj.GetComponent(typeof(EnemySpawnBehavior)) as EnemySpawnBehavior;

            enemySpawnScript.Spawn();

            var currentWave = GameStats.waves[GameStats.wave];
            this.enemySpawnTimer = Random.Range(currentWave.spawnRateMin, currentWave.spawnRateMax);
        }
    }

    private bool isLightSourceNearPosition(Vector3 position)
    {
        var protheseGameObj = GameObject.FindGameObjectWithTag("Prothese");
        var protheseScript = protheseGameObj.GetComponent(typeof(ProtheseBehavior)) as ProtheseBehavior;
        var protheseLightRange = protheseScript.prothese.lightRange;
        if (Vector3.Distance(position, protheseGameObj.transform.position) < protheseLightRange)
        {
            return true;
        }

        var shrooms = GameObject.FindGameObjectsWithTag("Shroom");
        foreach(var shroom in shrooms)
        {
            var shroomScript = shroom.GetComponent(typeof(ShroomBehavior)) as ShroomBehavior;
            var shroomLightRange = shroomScript.shroom.lightRange;
            if (Vector3.Distance(position, shroom.transform.position) < shroomLightRange)
            {
                return true;
            }
        }

        return false;
    }

    void UpdateShroomSpawner()
    {
        var selectedShroom = GameStats.selectedShroom;
        if (GameStats.spores < selectedShroom.cost)
        {
            return;
        }

        if (Input.GetKeyDown(shroomSpawnHotkey))
        {
            if (currentShroom == null)
            {
                if (shrooms.Count > 0)
                {
                    currentShroom = Instantiate(selectedShroom.model);
                    var shroomScript = currentShroom.GetComponent(typeof(ShroomBehavior)) as ShroomBehavior;
                    shroomScript.shroom = selectedShroom;
                    shroomScript.isUnplaced = true;

                    // Disable light, ignore raycast and disable collider
                    currentShroom.gameObject.layer = 2;
                    currentShroom.gameObject.tag = "UnplacedShroom";
                    var shroomLight = currentShroom.gameObject.GetComponentInChildren<Light>();
                    shroomLight.intensity = shroomLight.intensity / 5;
                    currentShroom.gameObject.GetComponent<Collider>().enabled = false;
                }
            }
            else
            {
                Destroy(currentShroom.gameObject);
            }
        }
        if (currentShroom != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Terrain"))
                {
                    currentShroom.transform.position = hitInfo.point;
                    currentShroom.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
                }
            }

            mouseWheelRotation = Input.mouseScrollDelta.y;
            currentShroom.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
            
            if (Input.GetMouseButtonDown(0))
            {
                if(!isLightSourceNearPosition(currentShroom.transform.position))
                {
                    return;
                }

                // Enable light, apply coorect layer and enable collider
                currentShroom.gameObject.layer = 0;
                currentShroom.gameObject.tag = "Shroom";
                var shroomLight = currentShroom.gameObject.GetComponentInChildren<Light>();
                shroomLight.intensity = shroomLight.intensity * 5;
                currentShroom.gameObject.GetComponent<Collider>().enabled = true;
                var shroomScript = currentShroom.GetComponent(typeof(ShroomBehavior)) as ShroomBehavior;
                shroomScript.isUnplaced = false;
                GameStats.spores -= selectedShroom.cost;
                currentShroom = null;
            }
        }
    }
}
