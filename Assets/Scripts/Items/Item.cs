using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public new string name = "New Item";
    public GameObject prefab = null;
    public Sprite icon = null;
    public float weight = 20;

    public virtual void Use()
    {
        Debug.Log("Used " + name);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
