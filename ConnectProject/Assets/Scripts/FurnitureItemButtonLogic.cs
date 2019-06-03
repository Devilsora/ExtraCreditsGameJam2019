using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FurnitureItemButtonLogic : MonoBehaviour
{
    public GameObject furnitureItem; //furniture spawned
    public GameObject placedFurniture;
    public int maxQuantity;
    public int usedQuantity;
    public TextMeshProUGUI quantityText;
    private Button parentButton;
  
    // Start is called before the first frame update
    void Start()
    {
      quantityText.text = maxQuantity.ToString();
      parentButton = GetComponent<Button>();

      parentButton.onClick.AddListener(FurnitureItemClick);
    }

    // Update is called once per frame
    void Update()
    {
      if (usedQuantity > maxQuantity)
      {
        parentButton.interactable = false;
      }
      else
      {
        parentButton.interactable = false;
      }

      
    }

    public void FurnitureItemClick()
    {
      if (usedQuantity < maxQuantity)
      {
        usedQuantity++;

        GameObject furniturePrefab = Instantiate(furnitureItem, placedFurniture.transform);
        furniturePrefab.GetComponent<FurnitureLogic>().isSelected = true;

      }

      

      //spawn item in parented to enviornment
      //positioned for room center at x = -1.92, y = -9.32, z = 0
    }
}
