using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamAttacker : Attacker
{
    [SerializeField]
    private Beam beam;
    public Transform spawnParent;

    public const int MAX_BEAMS = 1;
    public int activeBeams { get; set; }

    private void Start() {
        activeBeams = 0;
    }

    protected override void SendProjectile() {
        if(_currentEnemy == null || activeBeams >= MAX_BEAMS)
            return;

        Vector3 midPoint = _currentEnemy.transform.position - transform.position;
        

        Beam b = Instantiate(beam, transform.position + midPoint, Quaternion.identity, spawnParent);
        b.transform.LookAt(_currentEnemy.transform.position);
        b.SetEnemy(_currentEnemy);
        b.damage = Mathf.FloorToInt(damage * modifier.damageModifier);
        activeBeams++;
        
    }

    protected override IEnumerator ProjectileCoroutine() {
        _projectileCoroutineStarted = true;
        SendProjectile();
        yield return new WaitForSeconds(1);
        /*
        while(true) {
            float speed = attackSpeed * modifier.speedModifier;
            if(Mathf.Approximately(speed, 0.0f))
                yield return new WaitForSeconds(1);
            else
                yield return new WaitForSeconds(1 / speed);

            SendProjectile();
        }
        */
    }
}
