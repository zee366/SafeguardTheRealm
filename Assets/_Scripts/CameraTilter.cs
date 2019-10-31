using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraTilter : MonoBehaviour {

    public float   transitionTime = 1.5f;
    public Vector3 posOnMarket;
    public Vector3 rotationOnMarket;
    public Vector3 posOnBattle;
    public Vector3 rotationOnBattle;

    private Vector3    _targetPos;
    private Quaternion _targetRot;
    private Coroutine  _coroutine;
    private float      _time;


    void Start() {
        // Market on start
        GoToMarketPosition();
    }


    private IEnumerator Transition() {
        _time = 0f;
        while ( _time < transitionTime ) {
            _time              += Time.deltaTime;
            transform.position =  Vector3.Lerp(transform.position, _targetPos, _time / transitionTime);
            transform.rotation =  Quaternion.Lerp(transform.rotation, _targetRot, _time / transitionTime);

            // Skip a frame
            yield return null;
        }

        transform.position = _targetPos;
        transform.rotation = _targetRot;
    }


    public void GoToMarketPosition() {
        if ( _coroutine != null )
            StopCoroutine(_coroutine);

        _targetPos = posOnMarket;
        _targetRot = Quaternion.Euler(rotationOnMarket);
        _coroutine = StartCoroutine(Transition());
    }


    public void GoToBattlePosition() {
        if ( _coroutine != null )
            StopCoroutine(_coroutine);

        _targetPos = posOnBattle;
        _targetRot = Quaternion.Euler(rotationOnBattle);
        _coroutine = StartCoroutine(Transition());
    }

}