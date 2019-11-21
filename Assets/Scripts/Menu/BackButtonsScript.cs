using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BackButtonsScript : MonoBehaviour
{
    public  Button myButton;
    private MyFileSystem fs;
    void Start()
    {
        Button btn = myButton.GetComponent<Button>();
        fs = GameObject.FindObjectOfType<MyFileSystem>();

        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        fs.goBack();
        Debug.Log("You have clicked the back button!");
    }

}
