using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public Image icon;
    public TMP_Text amountText;

    public Item item { get; private set; }

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;

        if (item.isStackable)
        {
            amountText.gameObject.SetActive(true);
            amountText.text = item.amount.ToString();
        }
        else
            amountText.gameObject.SetActive(false);
    }

    public void UseItem()
    {
        if (item != null)
        {
            if (!(item as Equipment))
                item.Use();
        }
    }

    public void RemoveItem()
    {
        Inventory.instance.Remove(item);
        Destroy(gameObject);
    }
}
