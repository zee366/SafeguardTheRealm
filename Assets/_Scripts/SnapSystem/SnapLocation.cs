using System;
using System.Security.Cryptography;
using UnityEngine;

namespace SnapSystem {
    public class SnapLocation : MonoBehaviour {

        public float selectedObjectHeight        = 2f;
        public float selectedObjectRotationSpeed = 90f;

        [SerializeField] private GameObject highlightGameObject;
        [SerializeField] private Transform  container;

        private bool _isLit      = false;
        private bool _isSelected = false;

        private Transform _innerObjectTransform;

        public bool IsLit {
            get { return _isLit; }
            set {
                _isLit = value;
                OnHighlightStateChange();
            }
        }

        public bool IsSelected {
            get { return _isSelected; }
            set {
                _isSelected = value;
                OnSelectChange();
            }
        }

        public bool IsEmpty {
            get { return container.childCount == 0; }
        }


        private void Start() {
            if ( container.childCount > 0 )
                _innerObjectTransform = container.GetChild(0);

            OnHighlightStateChange();
        }


        private void Update() {
            // Animate if selected tile
            if ( !IsEmpty && IsSelected ) {
                _innerObjectTransform.position = container.position + Vector3.up * selectedObjectHeight; Vector3 prevEuler = _innerObjectTransform.rotation.eulerAngles;
                _innerObjectTransform.rotation = Quaternion.Euler(prevEuler.x,
                                                                  prevEuler.y + selectedObjectRotationSpeed * Time.deltaTime,
                                                                  prevEuler.z);
            }
        }


        /// <summary>
        /// Internal event logic for state change
        /// </summary>
        private void OnHighlightStateChange() { highlightGameObject.SetActive(_isLit); }


        private void OnSelectChange() {
            if ( !IsSelected && !IsEmpty ) {
                // Reset pos and rotation
                _innerObjectTransform.position = container.position;
                _innerObjectTransform.rotation = Quaternion.identity;
            }
        }


        /// <summary>
        /// Replace current object in container if one, deleting the other one.
        /// </summary>
        /// <param name="toAdd"></param>
        public void ReplaceObject(GameObject toAdd) {
            // Remove
            if ( !IsEmpty )
                Destroy(_innerObjectTransform);

            _innerObjectTransform          = toAdd.transform;
            _innerObjectTransform.position = container.position;
            _innerObjectTransform.rotation = Quaternion.identity;
            _innerObjectTransform.parent   = container;
        }


        /// <summary>
        /// Get current object in location container
        /// </summary>
        /// <returns></returns>
        public GameObject GetObject() { return !IsEmpty ? _innerObjectTransform.gameObject : null; }


        /// <summary>
        /// Simple box rendered in EditorMode to easily know where Tiles are located
        /// </summary>
        private void OnDrawGizmos() { Gizmos.DrawWireCube(transform.position + Vector3.one / 2, new Vector3(1, 0.5f, 1)); }

    }
}