using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;

public class trialScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (var drive in DriveInfo.GetDrives())
        {
            Debug.Log($"Drive: {drive.Name} Root: { drive.RootDirectory}");
        }

        // Set a variable to the My Documents path.
        string docPath = @"C:";

        DirectoryInfo diTop = new DirectoryInfo(docPath);

        try
        {
            foreach (var fi in diTop.EnumerateFiles())
            {
                try
                {
                    Debug.Log($"{fi.FullName}\t\t{fi.Length}");
                }
                catch (UnauthorizedAccessException unAuthTop)
                {
                    Debug.LogWarning($"{unAuthTop.Message}");
                }
            }

            foreach (var di in diTop.EnumerateDirectories("*"))
            {
                try
                {
                    Debug.Log($"{ di.FullName}\t\t{di.Parent}");
                }
                catch (UnauthorizedAccessException unAuthDir)
                {
                    Debug.LogWarning($"{unAuthDir.Message}");
                }
            }
        }
        catch (DirectoryNotFoundException dirNotFound)
        {
            Debug.LogWarning($"{dirNotFound.Message}");
        }
        catch (UnauthorizedAccessException unAuthDir)
        {
            Debug.LogWarning($"unAuthDir: {unAuthDir.Message}");
        }
        catch (PathTooLongException longPath)
        {
            Debug.LogWarning($"{longPath.Message}");
        }


        float index = 0;
        foreach (var drive in DriveInfo.GetDrives())
        {
            Debug.Log($"Drive: {drive.Name} Root: { drive.RootDirectory}");

            // Create a primitive type cube game object
            var gObj = GameObject.CreatePrimitive(PrimitiveType.Cube);

            // Calculate the position of the game object in the world space
            int x = 0;
            float y = index + 1f;
            int z = 0;

            // Position the game object in world space
            gObj.transform.position = new Vector3(x, y, z);
            gObj.name = drive.Name;

            // Add DataNode component and update the attributes for later usage
            gObj.AddComponent<DataNode>();
            DataNode dn = gObj.GetComponent<DataNode>();
            dn.name = drive.Name;
            dn.Size = drive.TotalSize;

            index += 1.5f;
        }
    }

    RaycastHit hitInfo = new RaycastHit();

    // Update is called once per frame
    void Update()
    {
        // Check to see if the Left Mouse Button was clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Create a raycase from the screen-space into World Space, store the data in hitInfo Object
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                // if there is a hit, we want to get the DataNode component to extract the information
                DataNode dn = hitInfo.transform.GetComponent<DataNode>();
                //txtSelectedItemInfo.text = $"{dn.name} and {dn.Size}";
            }
        }
    }
}