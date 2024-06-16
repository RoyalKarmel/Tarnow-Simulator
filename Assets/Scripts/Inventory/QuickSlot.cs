using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuickSlot : MonoBehaviour, IDropHandler
{
    public KeyCode shortcut;
    public Transform slotTransform;
    public TMP_Text shortcutText;

    Item item;

    void Start()
    {
        shortcutText.text = GetKeyCodeString(shortcut);
    }

    void Update()
    {
        if (slotTransform.childCount > 0)
            AddItemToQuickSlot();
        else
            RemoveItemFromQuickSlot();

        // Check input
        if (Input.GetKeyDown(shortcut))
            UseItem();
    }

    // Dragging item
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        Draggable draggable = dropped.GetComponent<Draggable>();

        Item droppedItem = dropped.GetComponent<ItemButton>()?.item;
        if (droppedItem is Equipment)
            return;

        if (slotTransform.childCount == 0)
            draggable.parentAfterDrag = slotTransform;
        else
        {
            Transform currentChild = slotTransform.GetChild(0);
            currentChild.SetParent(draggable.parentAfterDrag);
            draggable.parentAfterDrag = slotTransform;
        }
    }

    #region Item
    // Add item to quick slot
    void AddItemToQuickSlot()
    {
        ItemButton itemButton = slotTransform.GetChild(0).GetComponent<ItemButton>();
        item = itemButton.item;

        Inventory.instance.Remove(item);
    }

    // Remove item from quick slot
    void RemoveItemFromQuickSlot()
    {
        if (item != null)
        {
            InventoryUI.instance.itemsInUI.Add(item);
            Inventory.instance.Add(item);

            item = null;
        }
    }

    // Use item in quick slot
    void UseItem()
    {
        if (item != null)
            item.Use();
    }
    #endregion

    // Remove "Alpha" from key code
    string GetKeyCodeString(KeyCode key)
    {
        string keyString = key.ToString();

        if (keyString.StartsWith("Alpha"))
            keyString = keyString.Substring(5);

        return keyString;
    }
}
