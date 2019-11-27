using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

namespace SnapSystem {
    [Serializable]
    public class SnapEvent : UnityEvent<SnapLocation> { }

    public class SnapLocation : MonoBehaviour {

        public float selectedObjectHeight        = 2f;
        public float selectedObjectRotationSpeed = 90f;

        [SerializeField] private GameObject highlightGameObject;
        [SerializeField] private Transform  container;

        public SnapEvent onContentChange;

        private bool _isLit      = false;
        private bool _isSelected = false;

        private Transform _innerObjectTransform;

        /// <summary>
        /// Defines if the location if currently highlighted
        /// </summary>
        public bool IsLit {
            get { return _isLit; }
            set {
                _isLit = value;
                OnHighlightStateChange();
            }
        }

        /// <summary>
        /// Defines if the location if selected for action
        /// </summary>
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
            if ( container.childCount > 0 ) _innerObjectTransform = container.GetChild(0);

            OnHighlightStateChange();
        }


        private void Update() {
            // Animate if selected tile
            if ( !IsEmpty && IsSelected ) {
                _innerObjectTransform.position = container.position + Vector3.up * selectedObjectHeight;
                Vector3 prevEuler = _innerObjectTransform.rotation.eulerAngles;
                _innerObjectTransform.rotation = Quaternion.Euler(prevEuler.x,
                                                                  prevEuler.y + selectedObjectRotationSpeed * Time.deltaTime,
                                                                  prevEuler.z);
            }
        }


        /// <summary>
        /// Internal event logic for state change
        /// </summary>
        private void OnHighlightStateChange() {
            highlightGameObject.SetActive(_isLit);
            BroadcastMessage("OnSnapLocationStateChange", _isLit, SendMessageOptions.DontRequireReceiver);
        }


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
            // Remove if one
            if ( !IsEmpty ) Destroy(_innerObjectTransform);

            _innerObjectTransform          = toAdd.transform;
            _innerObjectTransform.position = container.position;
            _innerObjectTransform.rotation = Quaternion.identity;
            _innerObjectTransform.parent   = container;

            onContentChange?.Invoke(this);
        }


        /// <summary>
        /// Remove objects that might be present in that location container
        /// </summary>
        public void Clear() {
            if ( !IsEmpty ) Destroy(_innerObjectTransform.gameObject);

            _innerObjectTransform = null;
            onContentChange?.Invoke(this);
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