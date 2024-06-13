using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform player { get; private set; }

    void Start()
    {
        player = PlayerManager.instance.player.transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= radius)
            if (Input.GetButtonDown("Interact"))
                Interact();
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
