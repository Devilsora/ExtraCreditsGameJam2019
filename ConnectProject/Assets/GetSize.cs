using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Floor dimensions: " + GetComponent<Renderer>().bounds.size.x + " by  " + GetComponent<Renderer>().bounds.size.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
