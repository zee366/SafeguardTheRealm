using UnityEngine;

/// <summary>
/// Simple visual ring toggle
/// Received by broadcast
/// </summary>
public class TowerRing : MonoBehaviour {

    [SerializeField] private GameObject ringObj;

    void OnSnapLocationStateChange(bool state) {
        ringObj.SetActive(state);
    }
}
