using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            // Check if it's a collectible
            CollectibleProp collectible = hit.collider.GetComponent<CollectibleProp>();
            if (collectible != null)
            {
                collectible.Interact();
                return;
            }

            // Check if it's a rotating door
            DoorInteraction door = hit.collider.GetComponent<DoorInteraction>();
            if (door != null)
            {
                door.Interact();
                return;
            }

            // Check if it's a sliding door
            SlidingDoor sliding = hit.collider.GetComponent<SlidingDoor>();
            if (sliding != null)
            {
                sliding.Interact();
                return;
            }

            // Check if it's a drawer
            DrawerMech drawer = hit.collider.GetComponent<DrawerMech>();
            if (drawer != null)
            {
                drawer.Interact();
                return;
            }

            // Optional: Debug info
            Debug.Log($"🟤 No interactable found on: {hit.collider.gameObject.name}");
        }
    }
}
