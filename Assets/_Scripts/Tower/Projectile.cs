using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed;
    public int damage = 1;
    private Enemy _enemy;
    private Vector3 _lastEnemyPosition;
    public void SetEnemy(Enemy go) { _enemy = go; }


    void FixedUpdate() {
        if ( _enemy == null ) {
            MoveTowardsPosition(_lastEnemyPosition);
            if ( transform.position == _lastEnemyPosition ) {
                Destroy(gameObject);
            }

            return;
        }
        
        MoveTowardsEnemy();
    }
    private void MoveTowardsEnemy() {
        float step = 0.1f;
        _lastEnemyPosition = _enemy.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, _enemy.transform.position, step * speed);
    }
    
    private void MoveTowardsPosition(Vector3 _lastEnemyPosition) {
        float step = 0.1f;
        transform.position = Vector3.MoveTowards(transform.position, _lastEnemyPosition, step);
    }


    public void EnemyHit(Transform t) {
        Enemy enemy = t.parent.GetComponent<Enemy>();
        if ( enemy ) {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

}
