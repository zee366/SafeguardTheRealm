using UnityEngine;

/// <summary>
/// Ensuring the health bar UI is always looking at the camera
/// </summary>
public class HealthView : MonoBehaviour
{
    private Transform target;

    private void Start()
    {
        target = Camera.main.transform;
    }
    void Update()
    {
        transform.LookAt(target);
        transform.forward = -transform.forward;
    }
}
