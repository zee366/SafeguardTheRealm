using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketScript : MonoBehaviour
{
    [SerializeField]
    GameObject transformation;


    List<int> level1 = new List<int> { 75, 25, 0 };
    List<int> level2 = new List<int> { 50, 45, 5 };
    List<int> level3 = new List<int> { 30, 55, 15 };
    List<int> level4 = new List<int> { 25, 45, 30 };
    List<int> level5 = new List<int> { 15, 40, 45 };


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (randomValue() <= level1[0]) {
            transform.Translate(Vector2.up * 1);
        }
        if (randomValue() > level1[0]) {
            transform.Translate(-Vector2.up * 1);
        }
    }

    private int randomValue() {
        System.Random random = new System.Random();
        int x = random.Next(101);
        return x;
    }
    
}
