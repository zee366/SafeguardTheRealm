using SnapSystem;
using UnityEngine;
using UnityEngine.Events;
using Utils;

public class WaveManager : MonoBehaviour {

    [SerializeField] private int _maxWaves;
    [SerializeField] private int _maxUnitsPerWave;
    [SerializeField] private int unitsPerWaveIncrement = 5;

    GameObject        _spawner;
    GameObject        _spline;
    public UnityEvent onWaveStart;
    public UnityEvent onWaveEnd;
    public UnityEvent onRoundEnd;

    private int _waveNumber = 1;
    private int _unitsSpawned;
    private bool        waveStopped;
    private SnapManager _snapManager;


    void Start() {
        _snapManager = FindObjectOfType<SnapManager>();
        _spawner = GameObject.Find("Spawner");
        _spline  = GameObject.Find("Spline");
    }


    void Update() {
        if ( !waveStopped ) {
            if ( _unitsSpawned >= _maxUnitsPerWave ) {
                CancelInvoke("SpawnEnemy");
                onWaveEnd?.Invoke();
                waveStopped = true;
            }
        }

        // if no enemies left on the map, end the round -> go to market phase
        if ( waveStopped ) {
            if ( _spline.transform.childCount == 0 ) {
                onRoundEnd?.Invoke();
                _snapManager.UnlockGrid();
            }
        }
    }


    public int GetWaveNumber() { return _waveNumber; }


    public void SpawnEnemy() {
        _spawner.GetComponent<Spawner>().SpawnOne();
        _unitsSpawned++;
    }


    public void StartWave() {
        _unitsSpawned = 0;
        _waveNumber++;
        _maxUnitsPerWave += unitsPerWaveIncrement;
        waveStopped      =  false;
        onWaveStart?.Invoke();
        _snapManager.LockGrid();
        InvokeRepeating("SpawnEnemy", 0f, 1f);
    }

}