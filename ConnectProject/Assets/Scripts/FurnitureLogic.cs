using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureLogic : MonoBehaviour
{

    public bool isSelected;
    public Material original;
    public Material highlighted;
    //public material invalidLocation?
    public FurnitureItemButtonLogic parentBtn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if (isSelected)
      {
        //can control with WASD/arrow keys
        GetComponent<MeshRenderer>().material = highlighted;

        if (Input.GetKeyDown(KeyCode.W))
        {
          transform.position += Vector3.back * 5;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
        transform.position += Vector3.forward * 5;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
          transform.eulerAngles += new Vector3(0, -90, 0);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
          transform.eulerAngles += new Vector3(0, 90, 0);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
          //check if location is valid first, otherwise make self invalid

          isSelected = false;
        }
      }
      else
      {
        GetComponent<MeshRenderer>().material = original;

        if (Input.GetMouseButtonDown(1))
        {
          Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
          RaycastHit hit;

          if (Physics.Raycast(ray, out hit))
          {
            if (hit.transform.name == gameObject.name)
            {
              parentBtn.usedQuantity--;
              Destroy(gameObject);
            }
            else
            {
              Debug.Log("hit name: " + hit.transform.name + "  object name: " + gameObject.name);
            }

          }
        }

        if (Input.GetMouseButtonDown(0))
        {
          Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
          RaycastHit hit;

          if (Physics.Raycast(ray, out hit))
          {
            if (hit.transform.name == gameObject.name)
            {
              isSelected = true;
            }
            else
            {
              Debug.Log("hit name: " + hit.transform.name + "  object name: " + gameObject.name);
            }

          }
        }

      }
    }
}
