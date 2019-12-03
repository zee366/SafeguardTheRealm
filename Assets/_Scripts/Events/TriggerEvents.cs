using UnityEngine;

namespace Events {

    /// <summary>
    /// Allow interface for collision event on trigger 3D collider
    /// </summary>
    public class TriggerEvents : MonoBehaviour {

        public LayerMask layerMask;
        public TUnityEvent onEnter2D;
        public TUnityEvent onStay2D;
        public TUnityEvent onExit2D;


        private void OnTriggerEnter(Collider other) {
            if ( ((1 << other.gameObject.layer) & layerMask) != 0 ) {
                onEnter2D?.Invoke(other.gameObject.transform);
            }
        }


        private void OnTriggerExit(Collider other) {
            if ( ((1 << other.gameObject.layer) & layerMask) != 0 ) {
                onExit2D?.Invoke(other.gameObject.transform);
            }
        }


        private void OnTriggerStay(Collider other) {
            if ( ((1 << other.gameObject.layer) & layerMask) != 0 ) {
                onStay2D?.Invoke(other.gameObject.transform);
            }
        }

    }
}