using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] int _health;
    [SerializeField] int _attackDamage;
    [SerializeField] int _xpValue;
    [SerializeField] int _goldValue;
    [SerializeField] bool _golden;

    GameObject _castle;
    GameObject _player;
    SplineFollower _splineFollower;
	
    const float EPSILON = 0.0001f;
	
    void Start() {
        _castle = GameObject.Find("Castle");
        _player = GameObject.Find("Player");
        _splineFollower = GetComponent<SplineFollower>();
    }

    void LateUpdate() {
        CheckProgress();
        CheckHealth();
    }

    public void TakeDamage(int value) {
        _health -= value;
    }

    void CheckProgress() {
        if(Mathf.Abs(_splineFollower.GetPercentageOfSplineProgress() - 1.0f) < EPSILON) {
           _castle.GetComponent<Castle>().TakeDamage(_attackDamage);
            Destroy(this.GameObject);
        }
    }

    void CheckHealth() {
        if(_health <= 0) {
            Die();
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Castle") {
            other.GetComponent<Castle>().TakeDamage(_attackDamage);
        }
    }

    void Die() {
        if(_golden)
            _player.GetComponent<Player>().GainGold(_goldValue);
    }
}
