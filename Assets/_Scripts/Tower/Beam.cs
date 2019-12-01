using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    public int damage = 1;
    protected float mTickRate = 1.0f; // damage over time tick rate

    protected Enemy mEnemy;
    protected Vector3 mLastEnemyPosition;
    protected Vector3 mInitialScale;

    protected Tower mTower;
    protected BeamAttacker mBeamAttacker;

    protected void Init() {
        mTower = transform.GetComponentInParent<Tower>();
        if(!mTower)
            Debug.Log("Tower not found");
        mBeamAttacker = transform.GetComponentInParent<BeamAttacker>();
        mInitialScale = transform.localScale;
        StartCoroutine(LateStart(0.5f));
    }

    void FixedUpdate() {
        if(mEnemy) {
            AlignToEnemy();
        }
        else {
            Die();
        }
    }

    IEnumerator LateStart(float waitTime) {
        yield return new WaitForSeconds(waitTime);

        InvokeRepeating("DamageOverTime", 0.0f, mTickRate);
    }

    protected void AlignToEnemy() {
        Vector3 targetDirection = (mEnemy.transform.position - transform.parent.position);
        float distanceToEnemy = targetDirection.magnitude;


        if(distanceToEnemy < mTower.radius) {
            Vector3 midPoint = targetDirection / 2.0f;

            transform.position = transform.parent.position + midPoint;
            transform.LookAt(mEnemy.transform.position);

            //transform.localScale = Vector3.one * midPoint.magnitude;
            transform.localScale = mInitialScale * distanceToEnemy;
        }
        else {
            Die();
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
        if(enemy._health <= 0) {
            Die();
        }
    }

    public void SetTickRate(float rate) {
        mTickRate = rate;
    }

    public void SetEnemy(Enemy go) { mEnemy = go; }

    void Die() {
        mBeamAttacker.activeBeams--;
        Destroy(gameObject);
    }
}
