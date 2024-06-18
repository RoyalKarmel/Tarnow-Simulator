using System.Collections.Generic;
using UnityEngine;

public class RecipesUI : MonoBehaviour
{
    public Transform contentPanel;
    GameObject recipeUiPrefab;
    List<Recipe> recipes;

    void Start()
    {
        recipeUiPrefab = GameAssets.instance.recipeBarPrefab;
        recipes = CraftingManager.instance.recipes;

        GenerateRecipesUI();
    }

    void GenerateRecipesUI()
    {
        foreach (Recipe recipe in recipes)
        {
            GameObject newRecipe = Instantiate(recipeUiPrefab, contentPanel);

            newRecipe.GetComponent<RecipeBar>().Initialize(recipe);
        }
    }
}
