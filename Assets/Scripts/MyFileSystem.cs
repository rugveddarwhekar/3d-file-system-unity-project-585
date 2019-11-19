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

        float x_Start = 0, y_start = 0;
        int columnlength = 2, rowlength = 3;
        float x_space = 2, y_space = 2;
        int numberOfDrives = 0;
        //float index_x = 0;
        List<DriveInfo> detail_drive = new List<DriveInfo>();
        foreach (var drive in DriveInfo.GetDrives())
        {
            detail_drive.Add(drive);
            numberOfDrives++;
        }
            //float index_y = 0;
            //float x = 0;
            //float y = 0;
            for (int i = 0, j = 0; j < numberOfDrives; i++, j++)
            {
                Debug.Log($"Drive: {detail_drive[j].Name} Root: { detail_drive[j].RootDirectory}");

                // Create a primitive type cube game object
                var gObj = GameObject.CreatePrimitive(PrimitiveType.Cube);


                // Calculate the position of the game object in the world space

                //x = index_x + 1f;
                //y = index_y + 1f;
                int z = 0;

                // Position the game object in world space
                //gObj.transform.position = new Vector3(x, y, z);
                gObj.transform.position = new Vector3(x_Start + (x_space * (i % columnlength)), y_start + (y_space * (i / columnlength)), z);

                // Add DataNode component and update the attributes for later usage
                gObj.AddComponent<DataNode>();
                DataNode dn = gObj.GetComponent<DataNode>();
                dn.Name = detail_drive[j].Name;
                dn.Size = detail_drive[j].TotalSize;
                dn.FullName = detail_drive[j].RootDirectory.FullName;
                dn.IsDrive = true;

                //index_y += 3f;
            }
            //index_x += 3f;
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

        //i add this comment for trial....
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
                        //WaitForSeconds wait = new WaitForSeconds(1f);
                        if (Vector3.Distance(hitInfo.transform.position, transform.position) > 3f)
                        {

                            if (samples > 1)
                            {
                                //Generate();
                               // yield return wait;
                               transform.position = (hitInfo.transform.position + (new Vector3(0.0f, 0.0f, 0.0f)));
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
