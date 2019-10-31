using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSample : MonoBehaviour
{
    public int numObjects = 10;
    public GameObject prefab;

    void Start()
    {
        float r = 1.0f;
        Vector3 center = transform.position;
        for (int i = 0; i < numObjects; i++)
        {
            //if (i > 10)
            //    r += 1;
            Vector3 pos = RandomCircle(center, (i+1)*0.1f, i);
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
            var g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            g.transform.position = pos;
            g.transform.rotation = rot;
            g.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

    Vector3 RandomCircle(Vector3 center, float radius, int index)
    {
        Vector3 pos;
        //if(index<10)
        {
            float ang = 0.1f * index * 360; // * 360;
            Debug.Log($"Angle: {ang}");
            pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
            pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
            pos.z = center.z; // + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        }
        //else
        //{
        //    float ang = 0.1f * index * 360; // * 360;
        //    pos.x = center.x;// + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        //    pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        //    pos.z = center.z + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        //}
        return pos;
    }
}
