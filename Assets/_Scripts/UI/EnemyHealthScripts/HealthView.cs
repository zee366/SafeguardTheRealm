using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
