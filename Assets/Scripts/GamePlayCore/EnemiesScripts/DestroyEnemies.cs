using UnityEngine;

public class DestroyEnemies : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Respawn")) Destroy(transform.parent.gameObject);
    }
}
