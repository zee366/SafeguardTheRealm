using System;
using System.Collections.Generic;
using SnapSystem;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct TowerUpgradeLink {

    public string     sourceTagName;
    public GameObject yield;

}

public class TowerUpgrader : MonoBehaviour {

    public  UnityEvent             onUpgrade;
    public  List<TowerUpgradeLink> upgrades;
    private Inventory              _inventory;
    private List<string>           _toProcess;
    private SnapManager            _snapManager;
    private WaveManager            _waveManager;


    void Awake() {
        _toProcess = new List<string>();
        _snapManager = FindObjectOfType<SnapManager>();
        _waveManager = FindObjectOfType<WaveManager>();
        _waveManager.onRoundEnd.AddListener(ProcessBuffer);

        _inventory = GetComponent<Inventory>();
        _inventory.onAdd.AddListener(OnInventoryAdded);
    }


    public GameObject GetYieldFor(string tag) {
        foreach ( TowerUpgradeLink upgrade in upgrades ) {
            if ( upgrade.sourceTagName == tag ) return upgrade.yield;
        }

        return null;
    }


    public void ProcessBuffer() {
        foreach ( string tagToProcess in _toProcess ) UpgradeWithTag(tagToProcess);

        _toProcess.Clear();
    }


    void UpgradeWithTag(string tag) {
        GameObject[] list = GameObject.FindGameObjectsWithTag(tag);

        if ( list.Length >= 3 ) {
            GameObject yield = GetYieldFor(tag);
            if ( yield == null ) {
                Debug.LogWarning("Could not get yield for tag: " + tag);
                return;
            }

            for ( int i = 1; i <= list.Length; i++ ) {
                GameObject   towerObjects = list[i - 1];
                SnapLocation location     = towerObjects.GetComponentInParent<SnapLocation>();
                location.Clear();

                if ( i % 3 == 0 ) {
                    GameObject upgradedInstance = Instantiate(yield, Vector3.zero, Quaternion.identity);
                    _inventory.Add(upgradedInstance);

                    onUpgrade.Invoke();
                }
            }
        }
    }


    void OnInventoryAdded(GameObject gameObject) {
        if ( _snapManager.IsLocked() ) {
            // To process later (After unlocked)
            _toProcess.Add(gameObject.tag);
            return;
        }

        UpgradeWithTag(gameObject.tag);
    }

}