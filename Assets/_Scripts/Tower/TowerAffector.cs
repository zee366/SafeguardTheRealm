using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class TowerAffector : MonoBehaviour {

    [Range(2f, 20f)] public float radius = 1;
    public Sprite thumbnail;

    public float speedModifier  = 1f;
    public float damageModifier = 1f;

    private List<Transform> _affected;
    private GameObject      _radiusObjet;


    void Awake() {
        _radiusObjet                      = transform.Find("Range").gameObject;
        _radiusObjet.transform.localScale = new Vector3(radius, 0.1f, radius);
    }


    public void StartAffectingTower(Transform t) {
        if ( t.parent == null ) return;

        TowerModifier param = new TowerModifier(speedModifier, damageModifier);
        t.parent.BroadcastMessage("ApplyAttackerModifier", param, SendMessageOptions.DontRequireReceiver);
    }


    public void StopAffectingTower(Transform t) {
        if ( t.parent == null ) return;

        TowerModifier param = new TowerModifier(1-(speedModifier-1), 1-(damageModifier-1));
        t.parent.BroadcastMessage("ApplyAttackerModifier", param, SendMessageOptions.DontRequireReceiver);
    }

}