using UnityEngine;

namespace SnapSystem {
    public class SnapLocation : MonoBehaviour {

        private void OnDrawGizmos() {
            Gizmos.DrawWireCube(  transform.position + Vector3.one/2 , new Vector3(1,0.5f, 1) );
        }

    }
}