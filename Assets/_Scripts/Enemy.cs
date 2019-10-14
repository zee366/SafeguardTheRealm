using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	[SerializeField]
	int health;
	[SerializeField]
	int moveSpeed;
	[SerializeField]
	int attackDamage;
    [SerializeField]
    int xpValue;
    [SerializeField]
    int goldValue;
	
	GameObject _castle;
    GameObject _player;
    SplineFollower _splineFollower;
	
	const float EPSILON = 0.0001f;
	
	void Start() {
		_castle = GameObject.Find("Castle");
        _player = GameObject.Find("Player");
		_splineFollower = GetComponent<SplineFollower>();
	}
	
	void Update() {
		CheckProgress();
		CheckHealth();
	}
	
	public void TakeDamage(int value) {
		health -= value;
	}
	
	void CheckProgress() {
		if(Mathf.Abs(_splineFollower.GetProgress() - 1.0f) < EPSILON) {
			_castle.GetComponent<Castle>().TakeDamage(attackDamage);
			health = 0;
		}
	}
	
	void CheckHealth() {
        if(health <= 0) {
            Die();
            Destroy(this.gameObject);
        }
	}
	
	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Castle") {
            other.GetComponent<Castle>().TakeDamage(attackDamage);
        }
    }

    void Die() {
        _player.GetComponent<Player>().GainXP(xpValue);
        _player.GetComponent<Player>().GainGold(goldValue);
    }
}