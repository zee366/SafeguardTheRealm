using Behavioral;
using UnityEngine;
using UnityEngine.Events;

using Behavioral;

public class Enemy : MonoBehaviour {
    [SerializeField] int _health;
    [SerializeField] int _attackDamage;
    [SerializeField] int _xpValue;
    [SerializeField] int _goldValue;
    [SerializeField] bool _golden;

    Castle _castle;
    Player _player;
    SplineFollower _splineFollower;
    
    public UnityEvent onDeath;
    public UnityEvent onCastleHit;

    Animator _animator;

    bool isKilledByTowerProjectile;

    private const float EPSILON = 0.0001f;
	
    void Awake() {
        _castle = GameObject.Find("Castle").GetComponent<Castle>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _splineFollower = GetComponent<SplineFollower>();
        _animator = GetComponent<Animator>();
    }

    void LateUpdate() {
        CheckProgress();
        _animator.SetBool("isKilledByTower", isKilledByTowerProjectile);
    }


    public void TakeDamage(int value) {
        _health -= value;
        CheckHealth();
    }

    void CheckProgress() {
        if(Mathf.Abs(_splineFollower.GetPercentageOfSplineProgress() - 1.0f) < EPSILON) {
            _castle.TakeDamage(_attackDamage);
            onCastleHit?.Invoke();
            Destroy(this.gameObject);
        }
    }


    public float GetProgress() {
        return Mathf.Abs(_splineFollower.GetPercentageOfSplineProgress() - 1.0f);
    }

    void CheckHealth() {
        if(_health <= 0) {
            isKilledByTowerProjectile = true;
            _splineFollower.speedInUnitsPerSecond = 0.0f;
        }
    }

    void OnTriggerEnter(Collider other) {
        if ( other.gameObject.CompareTag("Castle") ) {
            _castle.TakeDamage(_attackDamage);
        }
    }

    public void Die() {
        if(_golden)
            _player.GainGold(_goldValue);
        onDeath?.Invoke();
        Destroy(gameObject);
    }
}
