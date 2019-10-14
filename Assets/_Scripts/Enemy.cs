using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	[SerializeField]
	int mHealth;
	[SerializeField]
	int mMoveSpeed;
	[SerializeField]
	int mAttackDamage;
	
	GameObject mCastle;
    SplineFollower mSplineFollower;
	
	const float EPSILON = 0.0001f;
	
	void Start() {
		mCastle = GameObject.Find("Castle");
		mSplineFollower = GetComponent<SplineFollower>();
	}
	
	void Update() {
		CheckProgress();
		CheckHealth();
	}
	
	public void TakeDamage(int value) {
		mHealth -= value;
	}
	
	void CheckProgress() {
		if(Mathf.Abs(mSplineFollower.GetProgress() - 1.0f) < EPSILON) {
			mCastle.GetComponent<Castle>().TakeDamage(mAttackDamage);
			mHealth = 0;
		}
	}
	
	void CheckHealth() {
		if(mHealth <= 0)
			Destroy(this.gameObject);
	}
	
	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Castle") {
            other.GetComponent<Castle>().TakeDamage(mAttackDamage);
        }
    }
}