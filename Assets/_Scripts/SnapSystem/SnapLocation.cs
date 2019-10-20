using System;
using UnityEngine;

namespace SnapSystem {
    public class SnapLocation : MonoBehaviour {

        [SerializeField] private GameObject highlightGameObject;

        private bool _isLit = false;

        public bool IsLit {
            get { return _isLit; }
            set {
                _isLit = value;
                OnHighlightStateChange();
            }
        }


        private void Start() {
            OnHighlightStateChange();
        }


        /// <summary>
        /// Internal event logic for state change
        /// </summary>
        private void OnHighlightStateChange() { highlightGameObject.SetActive(_isLit); }


        /// <summary>
        /// Simple box rendered in EditorMode to easily know where Tiles are located
        /// </summary>
        private void OnDrawGizmos() { Gizmos.DrawWireCube(transform.position + Vector3.one / 2, new Vector3(1, 0.5f, 1)); }

    }
}