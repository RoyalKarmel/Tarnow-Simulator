using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    #region Singleton

    public static CraftingManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of Crafting Manager found!");
            return;
        }

        instance = this;
    }

    #endregion

    public List<Recipe> recipes = new List<Recipe>();
    public List<Recipe> knownRecipes = new List<Recipe>();

    public void Craft(Item craftedItem, List<InventorySlot> craftingSlots, Inventory inventory)
    {
        if (craftedItem != null)
        {
            // Remove ingredients from inventory
            foreach (var slot in craftingSlots)
            {
                if (slot.transform.childCount > 0)
                {
                    ItemButton itemButton = slot.transform.GetChild(0).GetComponent<ItemButton>();
                    inventory.Remove(itemButton.item);
                    Destroy(slot.transform.GetChild(0).gameObject);
                }
            }

            inventory.Add(craftedItem);
        }
        else
        {
            Debug.Log("No matching recipe found.");
        }
    }

    #region Utils
    public Item CheckCraftingResult(List<Item> craftingItems)
    {
        foreach (Recipe recipe in recipes)
        {
            if (IsMatchingRecipe(recipe, craftingItems))
                return recipe.result;
        }
        return null;
    }

    bool IsMatchingRecipe(Recipe recipe, List<Item> craftingItems)
    {
        // Ensure both lists have the same number of items
        if (recipe.ingredients.Count != craftingItems.Count)
            return false;

        // Create a copy of the crafting items list to check against
        List<Item> craftingItemsCopy = new List<Item>(craftingItems);

        // Check each ingredient in the recipe
        foreach (Item ingredient in recipe.ingredients)
        {
            // If the ingredient is found in the crafting items, remove it from the copy
            if (craftingItemsCopy.Contains(ingredient))
                craftingItemsCopy.Remove(ingredient);
            else
                return false;
        }

        // If all ingredients are matched, the crafting items list should be empty
        return craftingItemsCopy.Count == 0;
    }

    #endregion
}
