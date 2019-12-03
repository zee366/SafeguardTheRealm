using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used specifically with watermill asset
/// TODO: Replace with Auto-Rotate
/// </summary>
public class WaterMill : MonoBehaviour {

    void Update() { transform.rotation *= Quaternion.Euler(90.0f * Time.deltaTime, 0.0f, 0.0f); }

}