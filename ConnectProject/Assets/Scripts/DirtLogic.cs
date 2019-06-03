using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtLogic : MonoBehaviour
{
    public bool isDirty;
    public ParticleSystem cleanParticles;
    public float particleSystemLifetime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    //check if Roomba is above
      if (isDirty)
      {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.up, out hit, 1f))
        {
          if (hit.transform.gameObject.tag == "Roomba")
          {
            isDirty = false;
          }
        }
      }
      else
      {
        
      }
      
    }
}
