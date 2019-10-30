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
            if ( !_isOk ) return;

            _posOnSpline += Time.deltaTime * (speedInUnitsPerSecond / _splineRef.Length);

            // Don't query for position outside curve bounds
            _posOnSpline = Mathf.Clamp(_posOnSpline, 0.0f, 1.0f);

            CurveSample sample = _splineRef.GetSampleAtDistance(_posOnSpline * _splineRef.Length);

            // Add Spline object position offset
            transform.position = sample.location + _splineRef.transform.position;
        }


        public float GetPercentageOfSplineProgress() { return _posOnSpline; }

    }
}