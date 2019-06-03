using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerButton : MonoBehaviour
{
  public GameObject roomba;
  private RoombaMovement movement;
  public AudioSource OnSound;
  public AudioSource OffSound;
  public AudioSource ActiveSound;

    // Start is called before the first frame update
    void Start()
    {
      roomba = transform.parent.gameObject;
      movement = roomba.GetComponent<RoombaMovement>();
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.KeypadEnter))
      {
        movement.isON = !movement.isON;
        movement.moveFinished = !movement.moveFinished;
        Debug.Log("Power swapped");
    }


      if (Input.GetMouseButtonDown(0))
      {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
          if (hit.transform.name == gameObject.name)
          {
            movement.isON = !movement.isON;
            movement.moveFinished = !movement.moveFinished;
            Debug.Log("Power swapped");
            if (movement.isON)
            {
              OnSound.Play();
              ActiveSound.Play();
            }
            else
            {
              OffSound.Play();
              ActiveSound.Stop();
            }
          }
          else
          {
            Debug.Log("hit name: " + hit.transform.name + "  object name: " + gameObject.name);
          }

        }
      }
      
    }
}
