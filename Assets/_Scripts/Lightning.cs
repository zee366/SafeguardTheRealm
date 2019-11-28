using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    private List<GameObject> _bolts;
    private float t = 0.0f;
    private float angle = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        _bolts = new List<GameObject>();
        foreach(Transform child in transform) {
            _bolts.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        angle += 1080 * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        t += Time.deltaTime;
        if(t > 1.2f)
            t = 0.0f;
        Debug.Log(t);
        ShowBolt(t);
    }

    void ShowBolt(float t) {
        if(t > 0.0f && t < 0.2f) {
            _bolts[0].GetComponent<Renderer>().enabled = true;
            _bolts[1].GetComponent<Renderer>().enabled = false;
            _bolts[2].GetComponent<Renderer>().enabled = false;
            _bolts[3].GetComponent<Renderer>().enabled = false;
            _bolts[4].GetComponent<Renderer>().enabled = false;
            _bolts[5].GetComponent<Renderer>().enabled = false;
        }
        else if(t > 0.2f && t < 0.4f) {
            _bolts[0].GetComponent<Renderer>().enabled = false;
            _bolts[1].GetComponent<Renderer>().enabled = true;
            _bolts[2].GetComponent<Renderer>().enabled = false;
            _bolts[3].GetComponent<Renderer>().enabled = false;
            _bolts[4].GetComponent<Renderer>().enabled = false;
            _bolts[5].GetComponent<Renderer>().enabled = false;
        }
        else if(t > 0.4f && t < 0.6f) {
            _bolts[0].GetComponent<Renderer>().enabled = false;
            _bolts[1].GetComponent<Renderer>().enabled = false;
            _bolts[2].GetComponent<Renderer>().enabled = true;
            _bolts[3].GetComponent<Renderer>().enabled = false;
            _bolts[4].GetComponent<Renderer>().enabled = false;
            _bolts[5].GetComponent<Renderer>().enabled = false;
        }
        else if(t > 0.6f && t < 0.8f) {
            _bolts[0].GetComponent<Renderer>().enabled = false;
            _bolts[1].GetComponent<Renderer>().enabled = false;
            _bolts[2].GetComponent<Renderer>().enabled = false;
            _bolts[3].GetComponent<Renderer>().enabled = true;
            _bolts[4].GetComponent<Renderer>().enabled = false;
            _bolts[5].GetComponent<Renderer>().enabled = false;
        }
        else if(t > 0.8f && t < 1.0f) {
            _bolts[0].GetComponent<Renderer>().enabled = false;
            _bolts[1].GetComponent<Renderer>().enabled = false;
            _bolts[2].GetComponent<Renderer>().enabled = false;
            _bolts[3].GetComponent<Renderer>().enabled = false;
            _bolts[4].GetComponent<Renderer>().enabled = true;
            _bolts[5].GetComponent<Renderer>().enabled = false;
        }
        else if(t > 1.0f && t < 1.2f) {
            _bolts[0].GetComponent<Renderer>().enabled = false;
            _bolts[1].GetComponent<Renderer>().enabled = false;
            _bolts[2].GetComponent<Renderer>().enabled = false;
            _bolts[3].GetComponent<Renderer>().enabled = false;
            _bolts[4].GetComponent<Renderer>().enabled = false;
            _bolts[5].GetComponent<Renderer>().enabled = true;
        }
    }
}
