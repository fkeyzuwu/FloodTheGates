using UnityEngine;

public class DisableTestCamera : MonoBehaviour
{
    void Awake()
    {
        Destroy(gameObject);
    }
}
