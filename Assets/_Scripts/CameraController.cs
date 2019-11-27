using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    public float translationSpeed;
    [SerializeField]
    public float rotationSpeed;

    private Vector3 mViewTarget;
    private float t;
    private float transitionTime = 1.5f;
    private bool mRotating;

    // Start is called before the first frame update
    void Start()
    {
        mViewTarget = Vector3.zero;
        transform.LookAt(mViewTarget);
        mRotating = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)) {
            mRotating = true;
            t = 0.0f;
        }
        if(mRotating) {
            t += 90.0f * Time.deltaTime;
            if(t <90.0f) {
                transform.Rotate(Vector3.up, t, Space.World);
            }
            else {
                mRotating = false;
            }
        }
    }
}
