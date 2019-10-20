using UnityEngine;

namespace SnapSystem {
    public class SnapManager : MonoBehaviour {

        [SerializeField] private LayerMask selectableLayerMask;

        private Camera       _cam;
        private SnapLocation _currentTarget;
        private SnapLocation _lastSelected;

        private void Awake() { _cam = Camera.main; }


        private void Update() {
            // Check Mouse position for available action on Grid
            CheckCurrentTarget(Input.mousePosition);

            // Left click
            if ( Input.GetMouseButtonDown(0) ) {

                CheckLocationSelection();
            }

        }


        /// <summary>
        /// Mouse movement check for target.
        /// Used for location highlighting and potential tile information gathering
        /// </summary>
        private void CheckCurrentTarget(Vector3 mousePos) {
            RaycastHit hit;
            Ray        ray = _cam.ScreenPointToRay(mousePos);

            if ( Physics.Raycast(ray, out hit, Mathf.Infinity, selectableLayerMask) ) {
                // Make sure the selectable has associated script
                SnapLocation newRef = hit.transform.GetComponent<SnapLocation>();

                // Change state of older one
                if ( _currentTarget != null && _currentTarget != newRef )
                    _currentTarget.IsLit = false;

                _currentTarget = newRef;

                // Make sure the new reference was indeed a SnapLocation.. maybe not
                if ( _currentTarget == null )
                    return;

                _currentTarget.IsLit = true;

            } else {
                // We didn't hit this layer
                if ( _currentTarget != null ) {
                    _currentTarget.IsLit = false;
                    _currentTarget = null;
                }
            }
        }


        /// <summary>
        /// Selectable selection logic
        /// </summary>
        private void CheckLocationSelection() { _lastSelected = _currentTarget; }

    }
}