using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtManager : MonoBehaviour
{
  //dirt spawn info
  //rotation: x = 90
  //position: y = -11.11395

  //also moves in x and y by 5

  public int numDirt;
  public GameObject dirt;
  public AudioSource finishSound;

  public float minZ = -13.35f;
  public float maxZ = 21.65f;

  public float maxX = 20f;
  public float minX = -15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
