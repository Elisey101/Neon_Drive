using UnityEngine;

public class DestroyOutOfMap : MonoBehaviour
{
    private int _objOutPos = -5;

    void Update()
    {
        if(transform.position.z < _objOutPos) Destroy(gameObject);
    }
}
