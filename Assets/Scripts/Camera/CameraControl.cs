using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float minX = -360.0f;
    public float maxX = 360.0f;

    public float minY = -45.0f;
    public float maxY = 45.0f;

    public float sensX = 100.0f;
    public float sensY = 100.0f;

    float rotationY = 0.0f;
    float rotationX = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private float speed = 2.0f;
    
    public void changeCameraPosition()
    {
        //
        transform.Rotate(Vector3.left, 0.45f);

    }
    
    void Update()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    rotationX += Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
        //    rotationY += Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;
        //    rotationY = Mathf.Clamp(rotationY, minY, maxY);
        //    transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        //}

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(-Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }


        if(Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(Vector3.right, -10 * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(Vector3.right, 10 * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up, -10 * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, 10 * speed * Time.deltaTime);
        }


    }
}
