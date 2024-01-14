using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;
    public TextMeshPro speedText;

    void Update()
    {      
        speed = rb.velocity.magnitude * 3.6f;
        speedText.text = Mathf.Round(speed) + "km/h";
    }
}
