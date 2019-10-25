using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Tower : MonoBehaviour {

    [Range(2f, 20f)]
    public float radius;
    
    private List<Attacker> _attackers;
    private GameObject _enemyBeingTargetted;
    private GameObject _radiusObjet;
    private Dictionary< int , GameObject> _enemyInRange;
    private List<Transform> _towerWeaponVisuals;
    
    private Timer _timer;


    void Awake() {
        
        _attackers = new List<Attacker>(GetComponentsInChildren<Attacker>());
        _radiusObjet                      = transform.Find("Range").gameObject;
        _radiusObjet.transform.localScale = new Vector3(radius, 0.1f, radius);
        _enemyInRange                     = new Dictionary<int, GameObject>();
        
        //Find all the tower's weapon
        _towerWeaponVisuals = new List<Transform>();
        foreach ( Transform child in GetComponentsInChildren<Transform>() ) {
            if ( child.transform.CompareTag("weapon") ) {
                _towerWeaponVisuals.Add(child);
            }
        }
    }
    

    // Update is called once per frame
    void Update() {
        if ( _enemyInRange.Count <= 0 ) return; // if there is no enemy

        GameObject enemy = FindClosestEnemy();
        if ( enemy != _enemyBeingTargetted ) {
            AlertAttacker(enemy);
            _enemyBeingTargetted = enemy;
        }
        LookAtEnemy();
    }
    
    public void AddEnemyToMemory(Transform t) {
        _enemyInRange.Add(t.gameObject.GetInstanceID(), t.gameObject);
    }


    public void RemoveEnemyFromMemory(Transform t) {
        StopAttacker(t.gameObject);
        _enemyInRange.Remove(t.gameObject.GetInstanceID()); 
    }


    GameObject FindClosestEnemy() {
        GameObject target = null;
        
        foreach ( KeyValuePair<int, GameObject> idEnemyPair in _enemyInRange ) {
            target = idEnemyPair.Value;
            break;

            //TODO: FIND CLOSEST ON SPLINE
        }
        
        return target;
    }

    private void AlertAttacker(GameObject enemy) {
        foreach (Attacker attacker in _attackers ) {
            attacker.Attack(enemy);
        }
    }


    private void StopAttacker(GameObject enemy) {
        foreach (Attacker attacker in _attackers ) {
            attacker.Stop(enemy);
        }
    }
    
    private void LookAtEnemy() {
        Vector3 enemyPosition = _enemyBeingTargetted.transform.position;
        foreach ( Transform weapon in _towerWeaponVisuals ) {
            Vector3 targetPostition = new Vector3( enemyPosition.x, 
                                                   weapon.position.y, 
                                                   enemyPosition.z ) ;
            weapon.LookAt( targetPostition ) ;
        }
    }
    
}
