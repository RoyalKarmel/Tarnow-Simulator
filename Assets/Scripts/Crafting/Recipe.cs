using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Crafting/Recipe")]
public class Recipe : ScriptableObject
{
    public new string name = "New Recipe";

    [Header("Recipe")]
    public List<Item> ingredients;
    public Item result;

    [Range(2, 9)]
    public int maxIngredients = 9;

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
            name = "Przepis na " + result.name;
    }
}
