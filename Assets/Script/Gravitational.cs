using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Garavitational : MonoBehaviour
{

    public static List<Garavitational> otherGameObject;

    private Rigidbody rb;
    const float G = 0.006674f; //6.674


    [SerializeField] bool planet = false;
    [SerializeField] int orbitSpeed = 1000;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (otherGameObject == null) { otherGameObject = new List<Garavitational>(); }
        otherGameObject.Add(this);

        if (!planet)
        {
            rb.AddForce(Vector3.left * orbitSpeed);
        }
    }


    void FixedUpdate()
    {
        foreach (Garavitational obj in otherGameObject)
        { if (obj != this) { AttractionForce(obj); } }
    }

    void AttractionForce(Garavitational other)
    {
        Rigidbody otherRb = other.rb;
        Vector3 dir = rb.position - otherRb.position;
        float dist = dir.magnitude;
        if (dist == 0) { return; }
        float forceMagnitude = G * ((rb.mass * otherRb.mass) / Mathf.Pow(dist, 2));
        Vector3 gravitationalForce = forceMagnitude * dir.normalized;
        otherRb.AddForce(gravitationalForce);


    }

}
