using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;

    [HideInInspector]
    public Transform parentAfterDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;

        ChangeImageAlpha();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Draggging");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;

        ChangeImageAlpha();
    }

    // Change image alpha
    void ChangeImageAlpha()
    {
        Color color = image.color;
        float newAlpha = color.a == 1f ? 0.6f : 1f;
        color.a = newAlpha;
        image.color = color;
    }
}
