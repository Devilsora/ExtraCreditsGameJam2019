using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FurnitureItemButtonLogic : MonoBehaviour
{
    public GameObject furnitureItem; //furniture spawned
    public GameObject placedFurnitureParent;
    public int maxQuantity;
    public int usedQuantity;
    public float itemPreferredHeight;
    public TextMeshProUGUI quantityText;
    private Button parentButton;
    public GameObject itemImage;
    GameObject itemImageChild;

    public Vector3 adjustmentOnObj;

  // Start is called before the first frame update
  void Start()
    {
      quantityText.text = maxQuantity.ToString();
      parentButton = GetComponent<Button>();
      placedFurnitureParent = GameObject.Find("PlacedFurniture");
      

      parentButton.onClick.AddListener(() =>
      {
        FurnitureItemClick();
      });
      itemImageChild = itemImage.transform.GetChild(0).gameObject;
      itemImageChild.transform.rotation = Quaternion.identity;
      
      
    }

    // Update is called once per frame
    void Update()
    {
    quantityText.text = (maxQuantity - usedQuantity).ToString();

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
      //Debug.Log("Clicked");
      if (usedQuantity < maxQuantity)
      {
        usedQuantity++;

        GameObject furniturePrefab = Instantiate(furnitureItem, placedFurnitureParent.transform); //new Vector3(placedFurnitureParent.transform.position.x, 15, placedFurnitureParent.transform.position.z), Quaternion.identity, 
        furniturePrefab.transform.position += adjustmentOnObj;
        furniturePrefab.GetComponent<FurnitureLogic>().isSelected = true;
        furniturePrefab.GetComponent<FurnitureLogic>().parentBtn = this;
      }

      //spawn item in parented to enviornment
      //positioned for room center at x = -1.92, y = -9.32, z = 0
    }

  public void FurnitureItemClick(BaseEventData eventData)
  {
    Debug.Log("Clicked");
    if (usedQuantity < maxQuantity)
    {
      usedQuantity++;

      GameObject furniturePrefab = Instantiate(furnitureItem, placedFurnitureParent.transform);
      Debug.Log("Furniture prefab poos: " + furniturePrefab.transform.position);
      furniturePrefab.transform.position += adjustmentOnObj;
      Debug.Log("Furniture prefab poos: " + furniturePrefab.transform.position);
      furniturePrefab.GetComponent<FurnitureLogic>().isSelected = true;
      furniturePrefab.GetComponent<FurnitureLogic>().parentBtn = this;

    }

    //spawn item in parented to enviornment
    //positioned for room center at x = -1.92, y = -9.32, z = 0
  }

  public void PointerEnter(BaseEventData eventData)
  {
    Debug.Log("Pointer entered");
    StartCoroutine(RotateObj());
  }

  public void PointerExit(BaseEventData eventData)
  {
    Debug.Log("Pointer left");
    StopAllCoroutines();
    //itemImageChild.transform.rotation = Quaternion.identity;
  }

  IEnumerator RotateObj()
  {
    while (true)
    {
      itemImageChild.transform.Rotate(Vector3.up, 2);
      yield return new WaitForSeconds(0.01f);
    }
  }
}
