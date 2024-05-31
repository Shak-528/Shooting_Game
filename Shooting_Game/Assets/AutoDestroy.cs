using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float lifetime = 1.0f; // The lifetime of the object in seconds

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
