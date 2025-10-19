using UnityEngine;

public class DevinRoomTrigger : MonoBehaviour
{
    public DevinRoomManager drm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("touch");
            drm.Initialize();
        }
    }
}
