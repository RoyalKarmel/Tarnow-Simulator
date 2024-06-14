using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;

    [HideInInspector]
    public Transform parentAfterDrag;
    ItemButton itemButton;

    void Start()
    {
        itemButton = GetComponent<ItemButton>();
    }

    #region Dragging

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

        if (IsPointerOverUIObject())
            transform.SetParent(parentAfterDrag);
        else
            Inventory.instance.DropItem(itemButton);

        image.raycastTarget = true;
        ChangeImageAlpha();
    }

    #endregion

    // Change image alpha
    void ChangeImageAlpha()
    {
        Color color = image.color;
        float newAlpha = color.a == 1f ? 0.6f : 1f;
        color.a = newAlpha;
        image.color = color;
    }

    bool IsPointerOverUIObject()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
}
