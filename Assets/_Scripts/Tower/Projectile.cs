using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed;
    private GameObject enemy;

    public void setEnemy(GameObject go) { enemy = go; }


    void Update() {
        if ( enemy == null ) return;
        MoveTowardsEnemy();
    }
    private void MoveTowardsEnemy() {
//        float step =  speed * Time.deltaTime; // calculate distance to move
        float step = 0.1f;
        transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, step);
    }

}
