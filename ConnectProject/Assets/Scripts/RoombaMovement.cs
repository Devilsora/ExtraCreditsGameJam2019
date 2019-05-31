using System.Collections;
using System.Collections.Generic;
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
  public bool isMoving = false;
  public bool moveFinished = false;

  public float startingMoveDist = 1f;
  public float movedDistance = 0f;
  public float speed;

  
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
        isMoving = true;
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
            movedDistance = 0f;
          }
            
          

        }
        //check if current direction is free
        
        


      }

    }

  public void setOrientation(int newOrientation)
  {
    or = (Orientation)newOrientation;
  }

  public void setOrientation(Orientation newOr)
  {
    or = newOr;
  }
}
