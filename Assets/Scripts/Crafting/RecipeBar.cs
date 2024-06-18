using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBar : MonoBehaviour
{
    public Image resultIcon;
    public TMP_Text resultName;
    public Transform ingredientsParent;
    Image[] ingredientsIcons;

    public void Initialize(Recipe recipe)
    {
        ingredientsIcons = ingredientsParent.GetComponentsInChildren<Image>();

        resultIcon.sprite = recipe.result.icon;
        resultName.text = recipe.result.name;

        for (int i = 0; i < ingredientsIcons.Length; i++)
        {
            if (i < recipe.ingredients.Count && recipe.ingredients[i] != null)
                ingredientsIcons[i].sprite = recipe.ingredients[i].icon;
            else
                ingredientsIcons[i].enabled = false;
        }
    }
}
