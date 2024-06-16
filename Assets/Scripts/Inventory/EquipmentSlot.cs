using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : MonoBehaviour, IDropHandler
{
    public EquipSlot equipSlot;
    Equipment equipment;

    void Update()
    {
        if (transform.childCount == 0)
            RemoveEquipment();
    }

    // Dragging item
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        Draggable draggable = dropped.GetComponent<Draggable>();

        equipment = dropped.GetComponent<ItemButton>()?.item as Equipment;

        if (equipment != null && equipment.equipSlot == equipSlot)
        {
            if (transform.childCount == 0)
                draggable.parentAfterDrag = transform;
            else
            {
                RemoveEquipment();
                Transform currentChild = transform.GetChild(0);
                currentChild.SetParent(draggable.parentAfterDrag);
                draggable.parentAfterDrag = transform;
            }
            equipment.Use();
        }
    }

    void RemoveEquipment()
    {
        if (equipment != null)
        {
            InventoryUI.instance.itemsInUI.Add(equipment);
            EquipmentManager.instance.Unequip((int)equipSlot);
        }
    }
}
