using TMPro;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    #region Singleton

    public static SelectionManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of Selection Manager found!");
            return;
        }

        instance = this;
    }

    #endregion

    public TMP_Text interactionText;
    public GameObject selectedObject { get; private set; }

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
                selectedObject = interactable.gameObject;

                interactionText.text = selectionTransform.GetComponent<Interactable>().name;
                interactionText.gameObject.SetActive(true);
            }
            else
            {
                interactionText.gameObject.SetActive(false);
                selectedObject = null;
            }
        }
        else
            interactionText.gameObject.SetActive(false);
    }
}
