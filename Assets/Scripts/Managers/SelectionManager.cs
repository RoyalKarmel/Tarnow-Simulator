using TMPro;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public TMP_Text interactionText;

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;
            var interactable = selectionTransform.GetComponent<Interactable>();

            if (interactable && interactable.playerInRange)
            {
                interactionText.text = selectionTransform.GetComponent<Interactable>().name;
                interactionText.gameObject.SetActive(true);
            }
            else
                interactionText.gameObject.SetActive(false);
        }
        else
            interactionText.gameObject.SetActive(false);
    }
}
