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


    void Awake() {
        _inventory = GetComponent<Inventory>();
        _inventory.onAdd.AddListener(OnInventoryAdded);
    }


    public GameObject GetYieldFor(string tag) {
        foreach ( TowerUpgradeLink upgrade in upgrades ) {
            if ( upgrade.sourceTagName == tag )
                return upgrade.yield;
        }

        return null;
    }


    void OnInventoryAdded(GameObject gameObject) {
        GameObject[] list = GameObject.FindGameObjectsWithTag(gameObject.tag);
        if ( list.Length >= 3 ) {
            GameObject yield = GetYieldFor(gameObject.tag);
            if ( yield == null ) {
                Debug.LogWarning("Could not get yield for tag: " + gameObject.tag);
                return;
            }

            Debug.Log("Would upgrade");
            foreach ( GameObject towerObjects in list ) {
                SnapLocation location = towerObjects.GetComponentInParent<SnapLocation>();
                location.Clear();
            }

            GameObject upgradedInstance = Instantiate(yield, Vector3.zero, Quaternion.identity);
            _inventory.Add(upgradedInstance);

            onUpgrade.Invoke();
        }
    }

}