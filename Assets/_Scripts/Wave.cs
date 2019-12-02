using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Wave : MonoBehaviour{
    public int _maxWaves;
    public int _maxUnitsPerWave;
    public int _unitsPerWaveIncrement;

    public int _unitsSpawned;
    public bool _waveStopped { get; set; }

    public GameObject _spawner;
    public GameObject _spline;

    public void SpawnEnemy() {
        _spawner.GetComponent<Spawner>().SpawnOne();
        _unitsSpawned++;
    }

    public void InvokeSpawn(float start, float repeat) {
        InvokeRepeating("SpawnEnemy", start, repeat);
    }

    public void CancelSpawn() {
        CancelInvoke("SpawnEnemy");
    }

    public void SpawnBoss() {
        _spawner.GetComponent<Spawner>().SpawnBoss();
    }
};
