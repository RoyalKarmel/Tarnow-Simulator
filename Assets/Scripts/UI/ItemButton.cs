using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public Image icon;

    Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }

    public void RemoveItem()
    {
        Inventory.instance.Remove(item);
        Destroy(gameObject);
    }
}
