using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public Transform itemsParent;

    [Header("Inventory")]
    public int space;
    public List<Item> items = new List<Item>();

    PlayerStats playerStats;

    void Start()
    {
        playerStats = PlayerManager.instance.playerStats;
    }

    public bool Add(Item newItem)
    {
        if (items.Count >= space)
        {
            Debug.Log("Not enought space!");
            return false;
        }

        items.Add(newItem);
        playerStats.currentWeight += newItem.weight;

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        playerStats.currentWeight -= item.weight;

        InventoryUI.instance.itemsInUI.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void DropItem(ItemButton itemButton)
    {
        Vector3 dropPosition = Camera.main.ScreenToWorldPoint(
            new Vector3(
                Input.mousePosition.x,
                Input.mousePosition.y,
                Camera.main.transform.position.y
            )
        );
        GameObject droppedItem = Instantiate(
            itemButton.item.prefab,
            dropPosition,
            Quaternion.identity
        );
        droppedItem.transform.SetParent(itemsParent);
        itemButton.RemoveItem();
    }
}
