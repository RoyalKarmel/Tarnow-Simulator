using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : MonoBehaviour, IDropHandler
{
    public EquipSlot equipSlot;
    public Transform slotTransform;
    public TMP_Text equipSlotText;
    Equipment equipment;

    void Start()
    {
        equipSlotText.text = equipSlot.ToString();
    }

    void Update()
    {
        if (slotTransform.childCount == 0)
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
            if (slotTransform.childCount == 0)
                draggable.parentAfterDrag = slotTransform;
            else
            {
                RemoveEquipment();
                Transform currentChild = slotTransform.GetChild(0);
                currentChild.SetParent(draggable.parentAfterDrag);
                draggable.parentAfterDrag = slotTransform;
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
