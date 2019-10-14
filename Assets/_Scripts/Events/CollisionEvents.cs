using UnityEngine;

namespace Events {
    public class CollisionEvents : MonoBehaviour {

        public LayerMask layerMask;
        public TUnityEvent onEnter2D;
        public TUnityEvent onStay2D;
        public TUnityEvent onExit2D;


        private void OnCollisionEnter(Collision other) {
            if ( ((1 << other.gameObject.layer) & layerMask) != 0 ) {
                onEnter2D?.Invoke(other.gameObject.transform);
            }
        }


        private void OnCollisionExit(Collision other) {
            if ( ((1 << other.gameObject.layer) & layerMask) != 0 ) {
                onExit2D?.Invoke(other.gameObject.transform);
            }
        }


        private void OnCollisionStay(Collision other) {
            if ( ((1 << other.gameObject.layer) & layerMask) != 0 ) {
                onStay2D?.Invoke(other.gameObject.transform);
            }
        }

    }
}