using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public new string name;
    public bool playerInRange { get; private set; } = false;
    public Transform player { get; private set; }

    void Start()
    {
        player = PlayerManager.instance.player.transform;

        if (string.IsNullOrEmpty(name))
            name = gameObject.name;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        playerInRange = distance <= radius;

        if (playerInRange)
        {
            if (
                Input.GetButtonDown("Interact")
                && SelectionManager.instance.selectedObject == gameObject
            )
                Interact();
        }
    }

    public virtual void Interact()
    {
        Debug.Log("Interacted with " + transform.name);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
