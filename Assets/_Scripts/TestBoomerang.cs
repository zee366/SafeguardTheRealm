using UnityEngine;
using SplineMesh;

public class TestBoomerang : MonoBehaviour
{
    [SerializeField]
    GameObject boomerang;

    public Transform spawnParent;
    private SplineNode _firstNode;

    void Start() {
        _firstNode = spawnParent.GetComponent<Spline>().nodes[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)) {
            Instantiate(boomerang, _firstNode.Position, Quaternion.identity, spawnParent);
        }
    }
}
