using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtLogic : MonoBehaviour
{
  //y = -1.8

    public bool isDirty = true;
    public ParticleSystem cleanParticles;
    public float particleSystemLifetime;
    private ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

  void OnTriggerEnter(Collider collision)
  {
    if (collision.gameObject.tag == "Roomba")
    {
      isDirty = false;
      particles = Instantiate(cleanParticles, gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);
    }
    else
    {
      Debug.Log("Dirt not colliding with roomba: " + collision.gameObject.tag + "NAME: " + collision.gameObject.name);
    }
  }

    // Update is called once per frame
  void Update()
  {
    //check if Roomba is above
    if (!isDirty)
    {
      GetComponent<SpriteRenderer>().enabled = false;
      particleSystemLifetime -= Time.deltaTime;
      if (particleSystemLifetime <= 0)
        Destroy(particles);
    }
  }
}
