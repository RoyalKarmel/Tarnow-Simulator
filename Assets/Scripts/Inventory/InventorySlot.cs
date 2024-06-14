using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void AddItem(Item newItem)
    {
        GameObject item = Instantiate(GameAssets.instance.itemButtonPrefab, transform);

        ItemButton itemButton = item.GetComponent<ItemButton>();
        itemButton.AddItem(newItem);
    }

    // Dragging item
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        Draggable draggable = dropped.GetComponent<Draggable>();

        if (transform.childCount == 0)
            draggable.parentAfterDrag = transform;
        else
        {
            Transform currentChild = transform.GetChild(0);
            currentChild.SetParent(draggable.parentAfterDrag);
            draggable.parentAfterDrag = transform;
        }
    }
}
