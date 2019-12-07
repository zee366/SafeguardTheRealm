using System.Collections;
using UnityEngine;

/// <summary>
/// A kind of projectile with different behavior that regular projectiles.
/// </summary>
public class Beam : MonoBehaviour
{
    public int damage = 1;
    protected float mTickRate = 1.0f; // damage over time tick rate

    protected Enemy mEnemy;
    protected Vector3 mInitialScale;

    protected Tower mTower;
    protected BeamAttacker mBeamAttacker;


    protected void Init() {
        mTower = transform.GetComponentInParent<Tower>();
        if(!mTower)
            Debug.LogWarning("Tower not found");
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

    /// <summary>
    /// Based on tick rate, with be called multiple time as long as enemy target exist and in range
    /// </summary>
    protected void DamageOverTime() {
        if(mEnemy)
            EnemyHit(mEnemy.transform);
    }

    /// <summary>
    /// Deal damage to enemy target
    /// </summary>
    /// <param name="t"></param>
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

    /// <summary>
    /// Destroys the beam correctly
    /// </summary>
    void Die() {
        mBeamAttacker.activeBeams--;
        Destroy(gameObject);
    }
}
