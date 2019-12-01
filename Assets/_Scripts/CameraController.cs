using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float translationSpeed = 12;
    public float rotationSpeed = 1;
    public float zoomSpeed = 3.0f;
    public float minZoom = 3.0f;
    public float maxZoom = 3.0f;



    private float mRotationMax = 90.0f;
    private float t;
    private Quaternion mTargetRot;
    private Coroutine mCoroutine;
    private float transitionTime = 1f;
    private bool mRotating;

    private GameObject mCameraObj;
    private Camera mCameraRef;

    private Vector3 mRight;
    private Vector3 mForward;
    private Vector3 mZoomDirection;

    private const float MIN_X = -10f;
    private const float MAX_X = 10f;
    private const float MIN_Z = -10f;
    private const float MAX_Z = 10f;

    void Start() {
        mRotating = false;
        mRight = transform.GetChild(0).transform.right;
        mForward = Vector3.Cross(mRight, Vector3.up);
        mCameraRef = Camera.main;
        mCameraObj = Camera.main.gameObject;
        mZoomDirection = (transform.position - mCameraObj.transform.position).normalized;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.R)) {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if(!mRotating) {
            if(Input.GetKeyDown(KeyCode.Q)) {
                mRotating = true;
                float currentRotation = transform.rotation.eulerAngles.y;
                mTargetRot = Quaternion.Euler(0, currentRotation + mRotationMax, 0);
                mCoroutine = StartCoroutine(Rotate());
            }
            else if(Input.GetKeyDown(KeyCode.E)) {
                mRotating = true;
                float currentRotation = transform.rotation.eulerAngles.y;
                mTargetRot = Quaternion.Euler(0, currentRotation - mRotationMax, 0);
                mCoroutine = StartCoroutine(Rotate());
            }
        }
        Translate();
    }

    private IEnumerator Rotate() {
        t = 0.0f;
        while(t < transitionTime) {
            t += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, mTargetRot, t / transitionTime);

            yield return null;
        }
        transform.rotation = mTargetRot;
        mRotating = false;
    }

    private void Translate() {
        Vector3 move = Vector3.zero;
        if(Input.GetKey(KeyCode.W))
            move += mForward;
        if(Input.GetKey(KeyCode.A))
            move += -mRight;
        if(Input.GetKey(KeyCode.S))
            move += -mForward;
        if(Input.GetKey(KeyCode.D))
            move += mRight;

        // Zooming for orthographic Cam now
        if ( Input.GetAxis("Mouse ScrollWheel") > 0f && mCameraRef.orthographicSize >= minZoom )
            mCameraRef.orthographicSize -= zoomSpeed;
        if(Input.GetAxis("Mouse ScrollWheel") < 0f && mCameraRef.orthographicSize <= maxZoom)
            mCameraRef.orthographicSize += zoomSpeed;

        transform.Translate(translationSpeed * move * Time.deltaTime);
    }
}