using System.Collections.Generic;
using SnapSystem;
using UnityEngine;
using UnityEngine.Events;
using Utils;

public class WaveManager : MonoBehaviour {

    [SerializeField] private int _maxWaves;
    [SerializeField] private int _maxUnitsPerWave;
    [SerializeField] private int unitsPerWaveIncrement = 5;

    GameObject[]      _spawners;
    GameObject[]      _splines;

    [SerializeField]
    public List<Wave> waves;

    public UnityEvent onWaveStart;
    public UnityEvent onWaveEnd;
    public UnityEvent onRoundEnd;

    private int  _waveNumber = 1;
    private int  _unitsSpawned;
    bool         waveStopped;

    private bool _roundEnded = true;
    private SnapManager _snapManager;

    private int numOfStoppedWaves = 0;


    void Start() {
        _snapManager = FindObjectOfType<SnapManager>();
        _spawners = GameObject.FindGameObjectsWithTag("Spawner");
        _splines  = GameObject.FindGameObjectsWithTag("Spline");

        foreach(GameObject spawner in _spawners) {
            Wave w = spawner.GetComponent<Wave>();
            w._maxWaves = _maxWaves;
            w._maxUnitsPerWave = _maxUnitsPerWave;
            w._unitsPerWaveIncrement = unitsPerWaveIncrement;
            w._unitsSpawned = 0;
            w._waveStopped = true;
            
            waves.Add(w);
        }
    }


    void Update() {
        foreach(Wave w in waves) {
            if(!w._waveStopped) {
                if(w._unitsSpawned >= w._maxUnitsPerWave) {
                    w.CancelSpawn();
                    onWaveEnd?.Invoke();
                    w._waveStopped = true;
                }
            }
        }

        // if no enemies left on the map, end the round -> go to market phase
        if(!_roundEnded) {
            foreach(Wave w in waves) {
                if(!w._waveStopped && (w._spline.transform.childCount - 1) != 0)
                    break;
                else
                    numOfStoppedWaves++;
            }

            if(numOfStoppedWaves == waves.Count) {
                _roundEnded = true;
                onRoundEnd?.Invoke();
                _snapManager.UnlockGrid();
            }
            else {
                numOfStoppedWaves = 0;
            }
        }
    }


    public int GetWaveNumber() { return _waveNumber; }

    public void StartWave() {
        int offset = 0;
        foreach(Wave w in waves) {
            w._maxUnitsPerWave += w._unitsPerWaveIncrement;
            w._waveStopped = false;
            w.InvokeSpawn(0 + offset, waves.Count + offset);
            offset++;
        }
        _waveNumber++;
        onWaveStart?.Invoke();
        _snapManager.LockGrid();
    }
}