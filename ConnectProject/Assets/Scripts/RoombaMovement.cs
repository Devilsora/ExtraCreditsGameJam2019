using System.Collections;
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

  private Vector3 moveNorth = Vector3.right;
  private Vector3 moveEast = Vector3.back;
  private Vector3 moveSouth = Vector3.left;
  private Vector3 moveWest = Vector3.forward;

  public bool isON = false; //starts off until you press its button
  public bool isMoving = true;
  public bool moveFinished = false;
  public bool checkedNextPositions = false;

  public float startingMoveDist = 1f;
  public float movedDistance = 0f;
  public float timeBetweenMoves = 1.5f;
  public float speed;
  float moveTimer = 0.0f;

  


  // Start is called before the first frame update
  void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
              or = DetermineOrientation();
              checkedNextPositions = true;
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

    if (Physics.Raycast(transform.position, moveNorth, out hit, 1.01f))
    {
      if ((hit.transform.gameObject.tag != "Furniture" && hit.transform.gameObject.tag != "Wal"))
      {
        validOrientations[0] = true;
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
      

    if (Physics.Raycast(transform.position, moveEast, out hit, 1.01f))
    {

      if ((hit.transform.gameObject.tag != "Furniture" && hit.transform.gameObject.tag != "Wal"))
      {
        validOrientations[1] = true;
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
      

    if (Physics.Raycast(transform.position, moveSouth, out hit, 1.01f))
    {
      if ((hit.transform.gameObject.tag != "Furniture" && hit.transform.gameObject.tag != "Wal"))
      {
        validOrientations[2] = true;
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

    if (Physics.Raycast(transform.position, moveWest, out hit, 1.01f))
    {
      if ((hit.transform.gameObject.tag != "Furniture" && hit.transform.gameObject.tag != "Wal"))
      {
        validOrientations[3] = true;
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

  
}

