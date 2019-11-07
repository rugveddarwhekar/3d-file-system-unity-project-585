using System;
using System.IO;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;


public class MyFileSystem : MonoBehaviour
{
    public Text txtSelectedNode;
    public Text txtHoveredOverNode;
    public Transform target;


    DataNode currentSelectedNode;

    // Start is called before the first frame update
    void Start()
    {
        txtSelectedNode.text = "";
        txtHoveredOverNode.text = "";

        float index = 0;
        foreach (var drive in DriveInfo.GetDrives())
        {
            Debug.Log($"Drive: {drive.Name} Root: { drive.RootDirectory}");

            // Create a primitive type cube game object
            var gObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);

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
            dn.Name = drive.Name;
            dn.Size = drive.TotalSize;
            dn.FullName = drive.RootDirectory.FullName;
            dn.IsDrive = true;

            index += 3f;
        }
    }

    RaycastHit hitInfo = new RaycastHit();
    
    void Update()
    {
        #region HANDLE MOUSE INTERACTION
        // Create a raycase from the screen-space into World Space, store the data in hitInfo Object
        bool Hoverhit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
        if (Hoverhit)
        {
            if (hitInfo.transform.GetComponent<DataNode>() != null)
            {
                // if there is a hit, we want to get the DataNode component to extract the information
                DataNode dn = hitInfo.transform.GetComponent<DataNode>();
                txtHoveredOverNode.text = $"{dn.FullName}";
            }
        }
        else
        {
            txtHoveredOverNode.text = $"";
        }
        #endregion

        // Check to see if the Left Mouse Button was clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Create a raycase from the screen-space into World Space, store the data in hitInfo Object
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if(hit)
            {
                if(hitInfo.transform.GetComponent<DataNode>()!=null)
                {
                    // if there is a hit, we want to get the DataNode component to extract the information
                    DataNode dn = hitInfo.transform.GetComponent<DataNode>();

                    if(dn.IsFolder)
                    {
                        DirectoryInfo diTop = new DirectoryInfo(dn.FullName);
                        int samples = diTop.GetDirectories("*").Length;
                        dn.gameObject.transform.Translate(Vector3.forward * -(samples%2)*1.5f, Space.Self);

                        //dn.NewPosition = (Vector3.forward * (samples % 2));
                        //dn.Move = true;

                        // update line renderer component
                        hitInfo.transform.GetComponent<LineRenderer>().SetPosition(1, dn.gameObject.transform.position);

                        if (Vector3.Distance(hitInfo.transform.position, transform.position) > 3f)
                        {

                            if (samples > 10)
                            {
                                transform.position = (hitInfo.transform.position + (new Vector3(0.0f, 0.0f, 1.5f)));
                            }
                        }
                        transform.LookAt(hitInfo.transform);

                        diTop = null;
                    }

                    txtSelectedNode.text = $"Selected Node: {dn.FullName} Size Is: {dn.Size}";
                    dn.IsSelected = true;

                    dn.ProcessNode();

                    if (currentSelectedNode == null)
                    {
                        currentSelectedNode = dn;
                    }
                    else
                    {
                        currentSelectedNode.IsSelected = false;
                        currentSelectedNode = dn;
                    }

                }
            }
        }
    }
}
