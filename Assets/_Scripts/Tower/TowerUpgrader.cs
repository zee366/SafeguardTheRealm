using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct TowerUpgradeLink {

    public string     sourceTagName;
    public GameObject yield;

}

public class TowerUpgrader : MonoBehaviour {

    public List<TowerUpgradeLink> upgrades;

    // private Dictionary<GameObject, int> _counters;
    private Inventory _inventory;


    void Awake() {
        _inventory = GetComponent<Inventory>();
        _inventory.onAdd.AddListener(OnInventoryAdded);
    }


    void OnInventoryAdded(GameObject gameObject) {
        GameObject[] list = GameObject.FindGameObjectsWithTag(gameObject.tag);
        if ( list.Length >= 3 )
            Debug.Log("Would upgrade");
    }

}