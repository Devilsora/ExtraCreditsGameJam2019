﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class RoombaMovement : MonoBehaviour
{
  public enum Orientation
  {
    North,
    East,
    South,
    West
  };

  public Orientation or = 0; //from a compass, 0 is North (y rotation = 0 deg)
                          //1 is East (y rotation = 90)
                          //2 is South (y rotation = 180)
                          //3 is West (y rotation = 270)

  public Orientation fearOrientation;

  private Vector3 moveNorth = Vector3.forward;
  private Vector3 moveEast = Vector3.right;
  private Vector3 moveSouth = Vector3.back;
  private Vector3 moveWest = Vector3.left;

  public Vector3[] orientationVectors = { Vector3.forward, Vector3.right, Vector3.back, Vector3.left};

  public Vector3[] orientationEulers =
    {Vector3.zero, new Vector3(0, 90, 0), new Vector3(0, 180, 0), new Vector3(0, 270, 0)};

  public bool isON = false; //starts off until you press its button
  public bool isMoving = true;
  public bool moveFinished = false;
  public bool checkedNextPositions = false;
  public bool isAfraid = false;

  public float startingMoveDist = 2f;
  public float movedDistance = 0f;
  public float timeBetweenMoves = 1.5f;
  public float speed;
  public float distCheck = 1.5f;
  float moveTimer = 0.0f;

  //valid z values, 23,17,11...
  //valid x = 19, 13.5, 
  

  public AudioSource DirectionChangeSound;
  public AudioSource AfraidSound;
  public AudioSource dirtCleanSound;

  // Start is called before the first frame update
  void Start()  
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      Debug.DrawRay(transform.position, moveEast  * distCheck, Color.red);
      Debug.DrawRay(transform.position, moveNorth * distCheck, Color.red);
      Debug.DrawRay(transform.position, moveWest  * distCheck, Color.red);
      Debug.DrawRay(transform.position, moveSouth * distCheck, Color.red);

    speed = startingMoveDist * Time.deltaTime;
        //move 1 grid space per "in game time" if ON

      if (isON)
      {
        if (moveFinished)
        {
          if (moveTimer >= timeBetweenMoves)
          {
            moveTimer = 0.0f;
            isMoving = true;
            moveFinished = false;
            checkedNextPositions = false;
          }
          else
          {
            moveTimer += Time.deltaTime;

            if (!checkedNextPositions)
            {
              Orientation prev = or;
              or = DetermineOrientation();
              checkedNextPositions = true;

              if (prev != or)
              {
                transform.eulerAngles = orientationEulers[(int) or];
                DirectionChangeSound.Play();
              }
          }
          }
        }

        

        if (isMoving && !moveFinished)
        {
          //keep moving until dist 
          movedDistance += speed;

          switch (or)
          {
            case Orientation.North:
              transform.position += speed * moveNorth;
              break;

            case Orientation.East:
              transform.position += speed * moveEast;
            break;

            case Orientation.South:
              transform.position += speed * moveSouth;
            break;

            case Orientation.West:
              transform.position += speed * moveWest;
            break;

            default:

              break;
          }

          if (movedDistance >= startingMoveDist)
          {
            moveFinished = true;
            isMoving = false;
            movedDistance = 0f;

            //start timer until it moves again
            
          }
      }
        //check if current direction is free
      }

    }

  public void SetOrientation(int newOrientation)
  {
    Debug.Log("Changing orientation to: " + (Orientation) newOrientation);
    or = (Orientation)newOrientation;
  }

  public void SetOrientation(Orientation newOr)
  {
    Debug.Log("Changing orientation to: " + newOr);
    or = newOr;
  }

  public Orientation DetermineOrientation()
  {
    bool[] validOrientations = {false, false, false, false};
    List<Orientation> possibleOrientations = new List<Orientation>();
    Orientation nextOrientation = or;

    //Debug.DrawRay(transform.position, moveEast, Color.red);
    //Debug.DrawRay(transform.position, moveNorth * 1.01f, Color.red);
    //Debug.DrawRay(transform.position, moveWest, Color.red);
    //Debug.DrawRay(transform.position, moveSouth, Color.red);

    RaycastHit hit;

    if (isAfraid)
    {
      if (!Physics.Raycast(transform.position, orientationVectors[(int) or], out hit, distCheck))
      {
        return or;
      }
    }


    if (Physics.Raycast(transform.position, moveNorth, out hit, distCheck))
    {
      if ((hit.transform.gameObject.tag != "Furniture" && hit.transform.gameObject.tag != "Wal"))
      {
        validOrientations[0] = true;
        if (isAfraid)
          isAfraid = false;
      }
      else
      {
        Debug.Log("On north tag, hit object with tag " + hit.transform.gameObject.tag);
      }
    }
    else
    {
      validOrientations[0] = true;
    }
      

    if (Physics.Raycast(transform.position, moveEast, out hit, distCheck))
    {

      if ((hit.transform.gameObject.tag != "Furniture" && hit.transform.gameObject.tag != "Wal"))
      {
        validOrientations[1] = true;
        if (isAfraid)
          isAfraid = false;
      }
      else
      {
        Debug.Log("On east tag, hit object with tag " + hit.transform.gameObject.tag);
      }
    }
    else
    {
      validOrientations[1] = true;
    }
      

    if (Physics.Raycast(transform.position, moveSouth, out hit, distCheck))
    {
      if ((hit.transform.gameObject.tag != "Furniture" && hit.transform.gameObject.tag != "Wal"))
      {
        validOrientations[2] = true;
        if (isAfraid)
          isAfraid = false;
      }
      else
      {
        Debug.Log("On south tag, hit object with tag " + hit.transform.gameObject.tag);
      }
    }
    else
    {
      validOrientations[2] = true;
    }

    if (Physics.Raycast(transform.position, moveWest, out hit, distCheck))
    {
      if ((hit.transform.gameObject.tag != "Furniture" && hit.transform.gameObject.tag != "Wal"))
      {
        validOrientations[3] = true;
        if (isAfraid)
          isAfraid = false;
      }
      else
      {
        Debug.Log("On west tag, hit object with tag " + hit.transform.gameObject.tag);
      }
    }
    else
    {
      validOrientations[3] = true;
    }

    if (Physics.Raycast(transform.position, orientationVectors[(int)or], out hit, distCheck))
    {
      Debug.Log("Object in front of roomba");
      if (hit.transform.gameObject.tag == "FearFurniture")
      {
        isAfraid = true;
        AfraidSound.Play();
      }
    }
    else
    {
      Debug.Log("Hit nothing in current orientation");
    }

    Debug.Log("Valid orientations: " + validOrientations[0].ToString() + "  " + validOrientations[1].ToString() + "   " + validOrientations[2].ToString() + "   " + validOrientations[3].ToString());

    for (int i = 0; i < 4; i++)
    {
      if (validOrientations[i])
      {
        possibleOrientations.Add((Orientation)i);
      }
    }

    if (possibleOrientations.Count == 1)
      nextOrientation = possibleOrientations[0];

    if (isAfraid)
    {
      switch (or)
      {
        case Orientation.North:
          return Orientation.South;

        case Orientation.East:
          return Orientation.West;

        case Orientation.South:
          return Orientation.North;

        case Orientation.West:
          return Orientation.East;
      }
      

    }

    else if (possibleOrientations.Count > 1 && possibleOrientations.Count < 4)
    {
      //check for locked in corner cases
      if (possibleOrientations.Count == 2)
      {
        switch (nextOrientation)
        {
          case Orientation.North:
            if ((possibleOrientations[0] == Orientation.South || possibleOrientations[1] == Orientation.South) &&
                possibleOrientations[0] == Orientation.East || possibleOrientations[1] == Orientation.East)
            {
              return Orientation.East;
            }
            break;

          case Orientation.East:
            if ((possibleOrientations[0] == Orientation.South || possibleOrientations[1] == Orientation.South) &&
                possibleOrientations[0] == Orientation.West || possibleOrientations[1] == Orientation.West)
            {
              return Orientation.South;
            }
            break;

          case Orientation.South:
            if ((possibleOrientations[0] == Orientation.North || possibleOrientations[1] == Orientation.North) &&
                possibleOrientations[0] == Orientation.West || possibleOrientations[1] == Orientation.West)
            {
              return Orientation.West;
            }
            break;

          case Orientation.West:
            if ((possibleOrientations[0] == Orientation.North || possibleOrientations[1] == Orientation.North) &&
                possibleOrientations[0] == Orientation.East || possibleOrientations[1] == Orientation.East)
            {
              return Orientation.North;
            }
            break;
        }
      }
      //go through list of orientations and find which one is closest to the current one
      for (int i = 0; i < possibleOrientations.Count; i++)
      {
        if (nextOrientation == possibleOrientations[i])
        {
          Debug.Log("Valid direction exists already, use that one: " + nextOrientation);
          break; //don't need to go through and pick other orientations if we already have one that works
        }
        else
        {
          //nextOrientation = (Orientation) possibleOrientations.Max();
          

          //nextOrientation = (Orientation)possibleOrientations.Min(x => Mathf.Abs((int)x - (int)nextOrientation));

          //(Orientation)possibleOrientations.OrderBy(x => Mathf.Abs(or - x )).First();

          //(Orientation)possibleOrientations.Min(x => Mathf.Abs((int)x - (int)nextOrientation));
        }
      }
      //nextOrientation = (Orientation)possibleOrientations.Min();
      nextOrientation = possibleOrientations.OrderBy(newOR => Mathf.Abs(newOR - nextOrientation)).First();

    }
    
    Debug.Log("Chosen orientation: " + nextOrientation);
    checkedNextPositions = true;

    return nextOrientation;
  }

  public void OnTriggerEnter(Collider collider)
  {
    if (collider.gameObject.tag == "Dirt")
    {
      Debug.Log("Ran into dirt");
      if (collider.gameObject.GetComponent<SpriteRenderer>().enabled)
      {
        dirtCleanSound.Play();
      }
    }
    else
    {
      Debug.Log("Did not run into dirt");
    }
  }
  

  
}

