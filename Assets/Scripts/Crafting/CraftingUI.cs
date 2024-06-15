using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour
{
    #region Singleton

    public static CraftingUI instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of CraftingUI found!");
            return;
        }

        instance = this;
    }

    #endregion

    public Transform craftingParent;

    public Image craftingResult;

    Inventory inventory;
    InventorySlot[] craftingSlots;

    void Start()
    {
        inventory = Inventory.instance;
        craftingSlots = craftingParent.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {
        UpdateCraftingResult();
    }

    // Craft items
    public void Craft()
    {
        List<Item> craftingItems = new List<Item>();

        foreach (var slot in craftingSlots)
        {
            if (slot.transform.childCount > 0)
            {
                ItemButton itemButton = slot.transform.GetChild(0).GetComponent<ItemButton>();
                craftingItems.Add(itemButton.item);
            }
        }

        Item craftedItem = CraftingManager.instance.CheckCraftingResult(craftingItems);
        CraftingManager.instance.Craft(
            craftedItem,
            new List<InventorySlot>(craftingSlots),
            inventory
        );

        ClearCraftingResult();
    }

    #region Crafting Slots UI
    void UpdateCraftingResult()
    {
        List<Item> craftingItems = new List<Item>();

        foreach (var slot in craftingSlots)
        {
            if (slot.transform.childCount > 0)
            {
                ItemButton itemButton = slot.transform.GetChild(0).GetComponent<ItemButton>();
                craftingItems.Add(itemButton.item);
            }
        }

        Item craftedItem = CraftingManager.instance.CheckCraftingResult(craftingItems);

        if (craftedItem != null)
        {
            craftingResult.enabled = true;
            craftingResult.sprite = craftedItem.icon;
        }
        else
            ClearCraftingResult();
    }

    void ClearCraftingResult()
    {
        craftingResult.sprite = null;
        craftingResult.enabled = false;
    }

    // Return items to inventory slots after closing inventory UI
    public void ReturnCraftingItemsToInventory(InventorySlot[] slots)
    {
        foreach (var slot in craftingSlots)
        {
            if (slot.transform.childCount > 0)
            {
                Transform itemTransform = slot.transform.GetChild(0);
                ItemButton itemButton = itemTransform.GetComponent<ItemButton>();

                // Find an empty inventory slot
                for (int i = 0; i < slots.Length; i++)
                {
                    if (slots[i].transform.childCount == 0)
                    {
                        slots[i].AddItem(itemButton.item);
                        break; // Exit the loop once the item is added
                    }
                }

                Destroy(itemTransform.gameObject);
            }
        }
    }

    #endregion
}
