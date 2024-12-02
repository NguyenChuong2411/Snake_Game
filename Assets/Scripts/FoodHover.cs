using UnityEngine;

public class FoodHover : MonoBehaviour
{
    private float hoverSpeed = 5f; // Speed of hovering
    private float hoverHeight = 0.1f; // Height of hovering
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        // Calculate vertical oscillation using Mathf.Sin
        float offsetY = Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        transform.position = initialPosition + new Vector3(0, offsetY, 0);
    }
}
