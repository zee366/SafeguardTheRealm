using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    public int damage = 1;
    protected float mTickRate = 1.0f; // damage over time tick rate

    protected Enemy mEnemy;
    protected Vector3 mLastEnemyPosition;

    protected Tower mTower;
    protected BeamAttacker mBeamAttacker;

    protected void Init() {
        mTower = transform.GetComponentInParent<Tower>();
        if(!mTower)
            Debug.Log("Tower not found");
        mBeamAttacker = transform.GetComponentInParent<BeamAttacker>();
        StartCoroutine(LateStart(0.5f));
    }

    void FixedUpdate() {
        if(mEnemy) {
            AlignToEnemy();
        }
        else {
            mBeamAttacker.activeBeams--;
            Destroy(gameObject);
        }
    }

    IEnumerator LateStart(float waitTime) {
        yield return new WaitForSeconds(waitTime);

        InvokeRepeating("DamageOverTime", 0.0f, mTickRate);
    }

    protected void AlignToEnemy() {
        Vector3 targetDirection = (mEnemy.transform.position - transform.parent.position);
        
        if(targetDirection.magnitude < mTower.radius) {
            Vector3 midPoint = targetDirection / 2.0f;

            transform.position = transform.parent.position + midPoint;
            transform.LookAt(mEnemy.transform.position);

            float scaleFactor = midPoint.magnitude;
            transform.localScale = Vector3.one * scaleFactor;
        }
        else {
            mBeamAttacker.activeBeams--;
            Destroy(gameObject);
        }
    }

    /*
    protected void OnTriggerEnter(Collider other) {
        mEnemyTransform = other.gameObject.transform;
        Enemy e = mEnemyTransform.parent.GetComponent<Enemy>();
        if(e) {
            InvokeRepeating("DamageOverTime", 0.0f, mTickRate);
        }
    }
    */

    protected void DamageOverTime() {
        if(mEnemy)
            EnemyHit(mEnemy.transform);
    }

    public void EnemyHit(Transform t) {
        Enemy enemy = t.gameObject.GetComponent<Enemy>();
        enemy.TakeDamage(damage);
    }

    public void SetTickRate(float rate) {
        mTickRate = rate;
    }

    public void SetEnemy(Enemy go) { mEnemy = go; }
}
