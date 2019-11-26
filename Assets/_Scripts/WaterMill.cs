using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMill : MonoBehaviour
{
    void Update()
    {
        transform.rotation *= Quaternion.Euler(90.0f * Time.deltaTime, 0.0f, 0.0f);
    }
}
