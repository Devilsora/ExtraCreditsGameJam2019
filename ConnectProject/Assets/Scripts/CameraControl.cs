using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
  //public GameObject [] walls;
  //private float wallDecFactor = -7f;

  public GameObject centralPt;

  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      //transform.LookAt(centralPt.transform.position);

      if (Input.GetKeyDown(KeyCode.D))
      {
        //rotate central point
        Vector3 currRot = centralPt.transform.rotation.eulerAngles;

        centralPt.transform.eulerAngles = new Vector3(currRot.x, currRot.y - 90, currRot.z); 
      }

      if (Input.GetKeyDown(KeyCode.A))
      {
        //rotate central point
        Vector3 currRot = centralPt.transform.rotation.eulerAngles;

        centralPt.transform.eulerAngles = new Vector3(currRot.x, currRot.y + 90, currRot.z);
      }

      //turn off walls and lower based on rotation Y value

      //rot 0 = S and E hidden
      //rot 90 = S and W hidden
      //rot -180 = N and W hidden
      //rot -270 = N and E hidden

  }
}
