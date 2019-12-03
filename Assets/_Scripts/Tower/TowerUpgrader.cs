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

/// <summary>
/// Upgrade tower automatically on different moments and events.
/// Holds how the tower will upgrade and to what tower.
/// </summary>
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

    /// <summary>
    /// Upgrade buffer to check.
    /// Used for preventing upgrades during battle and process it after battle.
    /// </summary>
    public void ProcessBuffer() {
        lock ( _toProcess ) {
            foreach ( string tagToProcess in _toProcess ) UpgradeWithTag(tagToProcess);
            _toProcess.Clear();
        }
    }

    /// <summary>
    /// With an Gameobject tag, remove towers to replace with upgraded one.
    /// Check if upgrade possible first.
    /// </summary>
    /// <param name="tag"></param>
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

    /// <summary>
    /// Triggered when an item is added to the inventory
    /// Maybe trigger upgrade
    /// </summary>
    /// <param name="gameObject"></param>
    void OnInventoryAdded(GameObject gameObject) {
        lock ( _toProcess ) {
            if ( _snapManager.IsLocked() ) {
                // To process later (After unlocked)
                _toProcess.Add(gameObject.tag);
                return;
            }
        }

        UpgradeWithTag(gameObject.tag);
    }

}