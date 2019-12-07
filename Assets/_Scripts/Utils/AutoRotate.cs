using UnityEngine;

/// <summary>
/// Basic generic rotation around one/many axis in euler format
/// Often used for visual assets
/// </summary>
public class AutoRotate : MonoBehaviour {

    public float speed;
    public bool x;
    public bool y;
    public bool z;

    private void Update() {
        transform.rotation *= Quaternion.Euler(speed * (x?1:0) * Time.deltaTime, speed * (y?1:0) * Time.deltaTime, speed * (z?1:0) * Time.deltaTime);
    }

}
