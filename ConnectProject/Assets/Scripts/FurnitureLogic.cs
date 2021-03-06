﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureLogic : MonoBehaviour
{
    public bool isSelected;
    public bool canPlace;
  public float clickDelta = 0.35f;
    public Material original;
    public Material highlighted;
    public Material invalidLocation;
    public FurnitureItemButtonLogic parentBtn;
    public AudioSource FurnitureSpawn;

    // Start is called before the first frame update
    void Start()
    {
      //original = GetComponent<MeshRenderer>().material;
      gameObject.transform.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
      if (isSelected)
      {
        canPlace = true;
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, gameObject.transform.GetChild(0).GetComponent<Renderer>().bounds.extents.z);

        foreach (Collider col in colliders)
        {
          if ( (col.gameObject.tag == "Furniture" || col.gameObject.tag == "Wal" || col.gameObject.tag == "Dirt") && col.gameObject.transform.parent != gameObject.transform)
          {
            Debug.Log("Collider data: " + col.gameObject.name);
            Debug.Log("Found thing you can't place onto");
            canPlace = false;
            break;
          }
        }
        //check if location is valid first, otherwise make self invalid
        if (canPlace == false)
        {
          //GetComponent<MeshRenderer>().material = invalidLocation;
        }
        else
        {
          //GetComponent<MeshRenderer>().material = highlighted;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
          transform.position += Vector3.back * 5.70439f;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
          transform.position += Vector3.forward * 5.70439f;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
          transform.position += Vector3.right * 5.70439f;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
          transform.position += Vector3.left * 5.70439f;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
          transform.eulerAngles += new Vector3(0, -90, 0);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
          transform.eulerAngles += new Vector3(0, 90, 0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
          parentBtn.usedQuantity--;
          Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
          if (canPlace)
          {
            isSelected = false;
            FurnitureSpawn.Play();
          }
        }
      }
      else
      {
        //GetComponent<MeshRenderer>().material = original;

        if (Input.GetMouseButtonDown(1))
        {
          Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
          RaycastHit hit;

          if (Physics.Raycast(ray, out hit))
          {
            if (hit.transform.name == gameObject.name || hit.transform.gameObject.transform.parent == gameObject.transform)
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
          if (Input.GetMouseButtonDown(0))
          {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
              if (hit.transform.name == gameObject.name || hit.transform.gameObject.transform.parent == gameObject.transform)
              {
                isSelected = true;
              }
              else
              {
                Debug.Log("hit name: " + hit.transform.name + "  object name: " + gameObject.name + " in furniturelogic");
              }

            }
          } 
        }

      }
    }
}
