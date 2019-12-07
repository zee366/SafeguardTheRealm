using UnityEngine;

/// <summary>
/// Toggle visual cues based on current modifier of the tower's attacker.
/// </summary>
public class ModifierDisplayToggle : MonoBehaviour {

    private                  Attacker   _attacker;
    [SerializeField] private GameObject p_speed;
    [SerializeField] private GameObject p_damage;


    private void Start() {
        if ( transform.parent == null ) return;

        _attacker = transform.parent.GetComponentInChildren<Attacker>();
    }


    private void Update() {
        if ( _attacker == null ) return;

        p_speed.SetActive(!Mathf.Approximately(_attacker.GetModifier().speedModifier, 1f));
        p_damage.SetActive(!Mathf.Approximately(_attacker.GetModifier().damageModifier, 1f));
    }

}