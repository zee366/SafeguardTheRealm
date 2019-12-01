using UnityEngine;
using UnityEngine.Events;
using Behavioral;
using System;

public class Enemy : MonoBehaviour {
    // For Enemy health
    public static event Action<Enemy> OnHealthAdded = delegate { };
    public static event Action<Enemy> OnHealtRemoved = delegate { };

    [SerializeField] int _maxHealth;
    public int _currentHealth { get; private set; }

    public event Action<float> OnHealthChangedPercentage = delegate { };



    [SerializeField] int _attackDamage;
    [SerializeField] int _xpValue;
    [SerializeField] int _goldValue;
    [SerializeField] bool _golden;

    Castle _castle;
    Player _player;
    SplineFollower _splineFollower;
    
    public UnityEvent onDeath;
    public UnityEvent onCastleHit;

    Rigidbody _rigidbody;
    Animator _animator;

    bool isKilledByTowerProjectile;

    private const float EPSILON = 0.0001f;
	
    void Awake() {
        _castle = GameObject.Find("Castle").GetComponent<Castle>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _splineFollower = GetComponent<SplineFollower>();
        _rigidbody = GetComponentInChildren<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    void LateUpdate() {
        CheckProgress();
        _animator.SetBool("isKilledByTower", isKilledByTowerProjectile);
    }

    private void OnEnable()
    {
        _currentHealth = _maxHealth;
        OnHealthAdded(this);
    }

    private void OnDisable()
    {
        OnHealtRemoved(this);
    }

    public void TakeDamage(int value) {
        //_health -= value;
        _currentHealth -= value;

        float _currentHealthPercentage = (float)_currentHealth / (float)_maxHealth;
        OnHealthAdded(this);
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
        //if(_health <= 0) {
        if(_currentHealth <= 0) {
            isKilledByTowerProjectile = true;
            _splineFollower.speedInUnitsPerSecond = 0.0f;
            _rigidbody.transform.position = new Vector3(_rigidbody.transform.position.x, -30.0f, _rigidbody.transform.position.z);
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
