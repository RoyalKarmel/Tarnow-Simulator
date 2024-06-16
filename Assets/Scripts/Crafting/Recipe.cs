using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Crafting/Recipe")]
public class Recipe : Item
{
    [Header("Recipe")]
    public List<Item> ingredients;
    public Item result;

    [Range(2, 9)]
    [SerializeField]
    int maxIngredients = 9;

    void OnValidate()
    {
        if (ingredients != null && ingredients.Count > maxIngredients)
        {
            ingredients = ingredients.GetRange(0, maxIngredients);
            Debug.LogWarning(
                $"Exceeded max ingredients for {name}. Only the first {maxIngredients} ingredients will be used."
            );
        }

        if (result != null)
        {
            this.name = "Recipe for " + result.name;
            this.prefab = result.prefab;
            this.icon = result.icon;
            this.weight = 0;
        }
    }

    public override void Use()
    {
        base.Use();

        CraftingManager.instance.knownRecipes.Add(this);
        RemoveFromInventory();
    }
}
