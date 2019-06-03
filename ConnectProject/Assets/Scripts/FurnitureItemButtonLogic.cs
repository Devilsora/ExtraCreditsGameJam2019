using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FurnitureItemButtonLogic : MonoBehaviour
{
    public GameObject furnitureItem; //furniture spawned
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
      
    }

    public void FurnitureItemClick()
    {
      if (usedQuantity < maxQuantity)
      {
        usedQuantity++;
      }
    }
}
