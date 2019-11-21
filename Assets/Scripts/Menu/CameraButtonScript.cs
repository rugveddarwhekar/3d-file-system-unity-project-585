using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Button myButton;
    CameraControl cameraControl;
    void Start()
    {
        Button btn = myButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        cameraControl = GameObject.FindObjectOfType<CameraControl>();
    }


    void TaskOnClick()
    {
        cameraControl.changeCameraPosition();
        Debug.Log("You have clicked the camera button!");
    }
}
