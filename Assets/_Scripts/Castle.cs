using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Castle state, health and events.
/// </summary>
public class Castle : MonoBehaviour {

    [SerializeField] int _health;

    private Slider hpSlider;

    public UnityEvent onCastleHit;
    public UnityEvent onCastleDie;


    private void Start() {
        hpSlider          = GameObject.Find("HealthBar(Castle)").GetComponent<Slider>();
        hpSlider.maxValue = _health;
        hpSlider.value    = hpSlider.maxValue;
    }

    /// <summary>
    /// Entry point for dealing damage
    /// </summary>
    /// <param name="value"></param>
    public void TakeDamage(int value) {
        _health -= value;
        onCastleHit?.Invoke();

        // Set the health bar's value to the current health.
        hpSlider.value = _health;

        if ( _health <= 0 )
            onCastleDie?.Invoke();
    }


    // Get health
    public int GetHealth() { return _health; }

}