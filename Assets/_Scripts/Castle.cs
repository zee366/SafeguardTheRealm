using UnityEngine;
using UnityEngine.Events;

public class Castle : MonoBehaviour
{
    [SerializeField] int _health;

    public UnityEvent onCastleHit;

    void TakeDamage(int value) {
        _health -= value;
        onCastleHit?.Invoke();
    }
}
