using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtManager : MonoBehaviour
{
  //dirt spawn info
  //rotation: x = 90
  //position: y = -11.11395

  //also moves in x and y by 5
  public bool allClean;
  public int numDirt;
  public GameObject dirt;
  public GameObject roomba;
  public AudioSource finishSound;
  private List<GameObject> dirtChildren = new List<GameObject>();
  public GameObject victoryScreen;

  public float minZ = -13.35f;
  public float maxZ = 21.65f;

  public float maxX = 20f;
  public float minX = -15f;

    // Start is called before the first frame update
    void Start()
    {
      roomba = GameObject.Find("Roomba");
      numDirt = transform.childCount;
      foreach (Transform trans in transform)
      {
        dirtChildren.Add(trans.gameObject);
      }
    }

    // Update is called once per frame
    void Update()
    {
      foreach (GameObject obj in dirtChildren)
      {
        allClean = true;
        if (obj.GetComponent<DirtLogic>().isDirty)
        {
          allClean = false;
          break;
        }
      }
      
      if (allClean)
      {
        Debug.Log("All clean");
        finishSound.Play();
        roomba.GetComponent<RoombaMovement>().isON = false;

        //display next level screen
      }
  }
}
