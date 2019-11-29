using UnityEngine;
using Behavioral;

public class Boomerang : MonoBehaviour
{
    public float speed;
    public int damage = 1;
    float rotationSpeed = 1080;

    private float _angle;
    private Enemy _enemy;
    private SplineFollower _splineFollower;

    private const float EPSILON = 0.0001f;

    void Start() {
        _angle = 0f;
        _splineFollower = GetComponent<SplineFollower>();
    }

    void Update() {
        _angle += rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0f, _angle, 0f);

        CheckProgress();
    }

    private void OnTriggerEnter(Collider other) {
        Enemy enemy = other.gameObject.transform.parent.GetComponent<Enemy>();
        if(enemy) {
            EnemyHit(other.gameObject.transform);
        }
    }

    public void EnemyHit(Transform t) {
        Enemy enemy = t.parent.GetComponent<Enemy>();
        enemy.TakeDamage(damage);
    }

    void CheckProgress() {
        if(Mathf.Abs(_splineFollower.GetPercentageOfSplineProgress() - 1.0f) < EPSILON)
            Destroy(this.gameObject);
    }

    public void SetEnemy(Enemy go) { _enemy = go; }
}
