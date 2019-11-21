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

       

        for (int i = 0; i < samples; i++)
        {
            

            var gObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            gObj.transform.position = new Vector3(x + transform.position.x, y + transform.position.y, z + transform.position.z);
            gObj.transform.localScale *= 0.1f;

            gObj.transform.GetComponent<Renderer>().material.color = new Color(x, y, z);

            gObj.transform.SetParent(transform);

        }
    }
}
