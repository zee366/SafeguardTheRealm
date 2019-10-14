using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Tower : MonoBehaviour {

    [Range(2f, 20f)]
    public float radius;
    private GameObject _radiusObjet;
    private Dictionary< int , GameObject> _enemyInRange;
    private List<Transform> _towerWeaponVisuals;

    void Start() {
        _radiusObjet = transform.Find("Range").gameObject;
        _radiusObjet.transform.localScale = new Vector3(radius, 0.1f, radius);
        _enemyInRange = new Dictionary<int, GameObject>();
        
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
        if ( _enemyInRange.Count > 0 ) LookAtEnemy();
    }

    public void AddEnemyToMemory(Transform t) {
        _enemyInRange.Add(t.gameObject.GetInstanceID(), t.gameObject);
    }


    public void RemoveEnemyFromMemory(Transform t) {
        _enemyInRange.Remove(t.gameObject.GetInstanceID()); 
    }
    

    void LookAtEnemy() {
        Vector3 target = Vector3.positiveInfinity;
        
        foreach ( KeyValuePair<int, GameObject> IdEnemyPair in _enemyInRange ) {
            target = IdEnemyPair.Value.transform.position;
            //TODO: FIND CLOSEST ON SPLINE
        }

        if ( target == Vector3.positiveInfinity ) return;
        
        foreach ( Transform weapon in _towerWeaponVisuals ) {
            Vector3 targetPostition = new Vector3( target.x, 
                                                   weapon.position.y, 
                                                   target.z ) ;
            weapon.LookAt( targetPostition ) ;
        }
    }
    
}
