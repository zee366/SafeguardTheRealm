using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    [SerializeField] int _health;

    private Slider hpSlider;

    public UnityEvent onCastleHit;
    public UnityEvent onCastleDie;

    private void Start()
    {
        hpSlider = GameObject.Find("HealthBar(Castle)").GetComponent<Slider>();
        hpSlider.maxValue = _health;
        hpSlider.value = hpSlider.maxValue;
    }
    public void TakeDamage(int value)
    {
        _health -= value;
        onCastleHit?.Invoke();

        // Set the health bar's value to the current health.
        hpSlider.value = _health;

        if (_health <= 0)
            onCastleDie?.Invoke();
    }
}
