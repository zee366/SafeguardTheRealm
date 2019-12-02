using UnityEngine;

/// <summary>
/// Simply for testing purpose
/// </summary>
public class dummyscript : MonoBehaviour
{

    // Update is called once per frame
    void Update() { transform.position = transform.position - new Vector3(0, 0, 0.01f); }
}
