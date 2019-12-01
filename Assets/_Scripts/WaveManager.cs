using System.Collections.Generic;
using SnapSystem;
using UnityEngine;
using UnityEngine.Events;
using Utils;

enum Phase {
    Market,
    Battle
};

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

    private bool _roundEnded = false;
    private SnapManager _snapManager;

    private int numOfStoppedWaves = 0;
    private int completedWaves = 0;
    private Phase phase;


    void Start() {
        _snapManager = FindObjectOfType<SnapManager>();
        _spawners = GameObject.FindGameObjectsWithTag("Spawner");
        _splines  = GameObject.FindGameObjectsWithTag("Spline");
        phase = Phase.Market;

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
        if(phase == Phase.Battle) {
            foreach(Wave w in waves) {
                if(!w._waveStopped) {
                    if(w._unitsSpawned >= w._maxUnitsPerWave) {
                        w.CancelSpawn();
                        onWaveEnd?.Invoke();
                        w._waveStopped = true;
                        completedWaves++;
                    }
                }
                else {
                    if(w._spline.transform.childCount == 0)
                        numOfStoppedWaves++;
                }
            }

            // if no enemies left on the map, end the round -> go to market phase
            if(!_roundEnded) {
                if(numOfStoppedWaves == waves.Count) {
                    numOfStoppedWaves = 0;
                    _roundEnded = true;
                    phase = Phase.Market;
                    onRoundEnd?.Invoke();
                    _snapManager.UnlockGrid();
                }
                else {
                    numOfStoppedWaves = 0;
                }
            }

            // transition to next level
            if(_roundEnded && completedWaves == _maxWaves) {
                Debug.Log("rdy for level transition");
            }
        }
    }


    public int GetWaveNumber() { return _waveNumber; }

    public void StartWave() {
        phase = Phase.Battle;
        int offset = 0;
        foreach(Wave w in waves) {
            w._unitsSpawned = 0;
            w._maxUnitsPerWave += w._unitsPerWaveIncrement;
            w._waveStopped = false;
            w.InvokeSpawn(0 + offset, waves.Count + offset);
            offset++;
        }
        _waveNumber++;
        onWaveStart?.Invoke();
        _snapManager.LockGrid();
        _roundEnded = false;
    }
}