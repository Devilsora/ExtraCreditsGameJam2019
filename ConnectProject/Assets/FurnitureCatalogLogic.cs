using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnitureCatalogLogic : MonoBehaviour
{
    public GameObject PlacedFurniture;
    public bool needToDeactive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      foreach (Transform child in PlacedFurniture.transform)
      {
        if (child.gameObject.GetComponent<FurnitureLogic>().isSelected)
        {
          needToDeactive = true;
          break;
        }
      }

      if (needToDeactive)
      {
        ChangeButtonStatus(false);
      }
      else
      {
        ChangeButtonStatus(true);
      }
    }

    public void ChangeButtonStatus(bool newStatus)
    {
      foreach (Transform child in transform)
      {
        child.gameObject.GetComponent<Button>().interactable = newStatus;
      }
    }
}
