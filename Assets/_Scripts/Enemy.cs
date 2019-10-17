using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField]
    int health;
    [SerializeField]
    int moveSpeed;
    [SerializeField]
    float angularSpeed;
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

    void LateUpdate() {
        Move();
        CheckProgress();
        CheckHealth();
    }

    public void TakeDamage(int value) {
        health -= value;
    }

    void Move() {
        Vector3 target = _splineFollower.transform.position - transform.position;
	float step = angularSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, target, step, 0.0f);
	
	transform.rotation = Quaternion.LookRotation(newDir);
        transform.Translate(target.x, target.y, target.z);
    }

    void CheckProgress() {
        if(Mathf.Abs(_splineFollower.GetPercentageOfSplineProgress() - 1.0f) < EPSILON) {
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

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Castle") {
            other.GetComponent<Castle>().TakeDamage(attackDamage);
        }
    }

    void Die() {
        _player.GetComponent<Player>().GainXP(xpValue);
        _player.GetComponent<Player>().GainGold(goldValue);
    }
}
