using UnityEngine;
using UnityEngine.Events;
using Utils;
using SplineMesh;

public class WaveManager : MonoBehaviour
{
    [SerializeField] int _maxWaves;
    [SerializeField] int _maxUnitsPerWave;

    GameObject _spawner;
    GameObject _spline;
    public UnityEvent onWaveEnd;
    public UnityEvent onRoundEnd;

    private int _waveNumber;
    private int _unitsSpawned;
    bool waveStopped;

    void Start()
    {
        _spawner = GameObject.Find("Spawner");
        _spline = GameObject.Find("Spline");
        InvokeRepeating("SpawnEnemy", 0f, 1f);
        _waveNumber = 1;
        waveStopped = false;
    }

    void Update()
    {
        if(!waveStopped) {
            if(_unitsSpawned >= _maxUnitsPerWave) {
                CancelInvoke("SpawnEnemy");
                onWaveEnd?.Invoke();
                waveStopped = true;
            }
        }

        // if no enemies left on the map, end the round -> go to market phase
        if(waveStopped) {
            if((_spline.transform.childCount - 1) == 0) {
                onRoundEnd?.Invoke();
            }
        }
    }

    public void SpawnEnemy() {
        _spawner.GetComponent<Spawner>().SpawnOne();
        _unitsSpawned++;
    }

    public void StartWave() {
        _waveNumber++;
        _maxUnitsPerWave += 5;
        waveStopped = false;
        InvokeRepeating("SpawnEnemy", 0f, 1f);
    }
}
