using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FibonacciSphere : MonoBehaviour
{
    public int samples = 10;

    // Start is called before the first frame update
    void Start()
    {
        FibonacciSphereDraw();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FibonacciSphereDraw()
    {
        //int samples = 10;

        float rnd = 1;
        bool randomize = true;

        if (randomize)
            rnd = Random.value * samples;

        float offset = 2.0f / samples;
        float increment = Mathf.PI * (3.0f - Mathf.Sqrt(5.0f));

        for (int i = 0; i < samples; i++)
        {
            float y = ((i * offset) - 1) + (offset / 2);
            float r = Mathf.Sqrt(1 - Mathf.Pow(y, 2));

            float phi = ((i + rnd) % samples) * increment;

            float x = Mathf.Cos(phi) * r;
            float z = Mathf.Sin(phi) * r;

            var gObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            gObj.transform.position = new Vector3(x + transform.position.x, y + transform.position.y, z + transform.position.z);
            gObj.transform.localScale *= 0.1f;

            gObj.transform.GetComponent<Renderer>().material.color = new Color(x, y, z);

            gObj.transform.SetParent(transform);

        }
    }
}
