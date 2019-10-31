using UnityEngine;
using UnityEngine.Events;

public class Castle : MonoBehaviour
{
    [SerializeField] int _health;

    public UnityEvent onCastleHit;
    public UnityEvent onCastleDie;

    public void TakeDamage(int value) {
        _health -= value;
        onCastleHit?.Invoke();

        if(_health <= 0)
            onCastleDie?.Invoke();
    }
}
