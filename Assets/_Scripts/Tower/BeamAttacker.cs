using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Specific attacker for the laser tower and his beam projectile
/// </summary>
public class BeamAttacker : Attacker
{
    [SerializeField]
    private Beam beam;
    public Transform spawnParent;

    [SerializeField]
    public int maxBeams = 1;
    public int activeBeams { get; set; }

    private void Start() {
        activeBeams = 0;
    }

    /// <summary>
    /// Sends a beam instead
    /// </summary>
    protected override void SendProjectile() {
        if(_currentEnemy == null || activeBeams >= maxBeams)
            return;

        Vector3 midPoint = _currentEnemy.transform.position - transform.position;
        

        Beam b = Instantiate(beam, transform.position + midPoint, Quaternion.identity, spawnParent);
        b.SetEnemy(_currentEnemy);
        b.transform.LookAt(_currentEnemy.transform.position);
        b.transform.localScale /= transform.parent.transform.localScale.x;
        b.damage = Mathf.FloorToInt(damage * modifier.damageModifier);
        activeBeams++;
        
    }

    protected override IEnumerator ProjectileCoroutine() {
        _projectileCoroutineStarted = true;
        SendProjectile();
        yield return new WaitForSeconds(1);
    }
}
