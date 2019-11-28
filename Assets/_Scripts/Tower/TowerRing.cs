using UnityEngine;

public class TowerRing : MonoBehaviour {

    [SerializeField] private GameObject ringObj;

    void OnSnapLocationStateChange(bool state) {
        ringObj.SetActive(state);
    }
}
