using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.position, transform.position)>3f)
        {
            transform.position = (target.position + (new Vector3(0.0f, 0.0f, 2.5f)));
        }
        transform.LookAt(target);
    }
}
