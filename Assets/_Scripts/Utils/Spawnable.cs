using System;
using UnityEngine;

[Serializable]
public struct Spawnable {

    public                 GameObject prefab;
    [Range(0, 100)] public float      probability;

}