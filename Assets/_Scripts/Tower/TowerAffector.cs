using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

/// <summary>
/// This is main behaviour for support towers
/// Modify towers in range with modifier.
/// Using
/// </summary>
public class TowerAffector : Tower {

    public float speedModifier = 1f;
    public float damageModifier = 1f;

    private List<Transform> _affected;
    private GameObject _radiusObjet;


    void Awake() {
        _radiusObjet = transform.Find("Range").gameObject;
        _radiusObjet.transform.localScale = new Vector3(radius, 0.1f, radius);
    }

    /// <summary>
    /// Called from trigger events.
    /// Add modifier to target
    /// </summary>
    /// <param name="t"></param>
    public void StartAffectingTower(Transform t) {
        if(t.parent == null) return;

        TowerModifier param = new TowerModifier(speedModifier, damageModifier);
        t.parent.BroadcastMessage("ApplyAttackerModifier", param, SendMessageOptions.DontRequireReceiver);
    }

    /// <summary>
    /// Called from trigger events.
    /// Add reversed modifier to target to cancel effect
    /// </summary>
    /// <param name="t"></param>
    public void StopAffectingTower(Transform t) {
        if(t.parent == null) return;

        TowerModifier param = new TowerModifier(1 - (speedModifier - 1), 1 - (damageModifier - 1));
        t.parent.BroadcastMessage("ApplyAttackerModifier", param, SendMessageOptions.DontRequireReceiver);
    }

}