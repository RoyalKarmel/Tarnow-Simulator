using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    #region Singleton

    public static InventoryUI instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of InventoryUI found!");
            return;
        }

        instance = this;
    }

    #endregion

    public Transform itemsParent;

    public bool isOpen { get; private set; }

    Inventory inventory;
    InventorySlot[] slots;

    [HideInInspector]
    public List<Item> itemsInUI = new List<Item>();

    [SerializeField]
    GameObject inventoryUI;

    void Start()
    {
        inventoryUI.SetActive(false);

        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        inventory.space = slots.Length;
    }

    // Update is called once per frame
    void Update()
    {
        isOpen = inventoryUI.activeSelf;

        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!isOpen);

            if (isOpen)
                Cursor.lockState = CursorLockMode.Locked;
            else
                Cursor.lockState = CursorLockMode.None;
        }
    }

    void UpdateUI()
    {
        foreach (var item in inventory.items)
        {
            if (!itemsInUI.Contains(item))
            {
                for (int i = 0; i < slots.Length; i++)
                {
                    if (slots[i].transform.childCount == 0)
                    {
                        slots[i].AddItem(item);
                        itemsInUI.Add(item);
                        break; // Exit the loop once the item is added
                    }
                }
            }
        }
    }
}
