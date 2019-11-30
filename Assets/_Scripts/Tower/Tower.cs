using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    public enum Classifier { Low, Medium, High, Support }

    [Range(2f, 20f)]
    public float radius;
    public Sprite thumbnail;
    public Classifier attack_descriptor;
    public Classifier speed_descriptor;


}
