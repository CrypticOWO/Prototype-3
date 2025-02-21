using UnityEngine;

public class EffectTimer : MonoBehaviour
{
    void Start()
        {
            // Destroy the GameObject after 0.25 seconds
            Destroy(gameObject, 0.2f);
        }
}
