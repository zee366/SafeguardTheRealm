using SplineMesh;
using UnityEngine;

namespace Behavioral {
    public class SplineFollower : MonoBehaviour {

        public float speedInUnitsPerSecond = 1.0f;


        private Spline _splineRef;
        private float  _posOnSpline = 0.0f;
        private bool   _isOk        = true;


        private void Awake() {
            _splineRef = transform.parent.GetComponent<Spline>();

            if ( !_splineRef ) {
                Debug.LogWarning("Not child of a Spline. Can't follow...'");
                _isOk = false;
            }
        }


        void Update() {
            _posOnSpline += Time.deltaTime * (speedInUnitsPerSecond / _splineRef.Length);
            CurveSample sample = _splineRef.GetSample(_posOnSpline * (_splineRef.nodes.Count-1));
            transform.position = sample.location;

            Debug.Log(_posOnSpline * (_splineRef.nodes.Count-1));
        }


        public float GetPercentageOfSplineProgress() { return _posOnSpline; }

    }
}