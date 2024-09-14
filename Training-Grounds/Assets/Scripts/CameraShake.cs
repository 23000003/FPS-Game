using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 originalPosition;
    private float shakeDuration = 0.01f;
    private float shakeMagnitude = 0.05f;
    private float dampingSpeed = 0.5f;
    private float initialShakeMagnitude;

    private void Start()
    {
        // Store the original position of the camera
        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        if (shakeDuration > 0)
        {
            // Calculate the new position
            transform.localPosition = originalPosition + Random.insideUnitSphere * shakeMagnitude;

            // Reduce shake magnitude over time
            shakeDuration -= Time.deltaTime * dampingSpeed;
            shakeMagnitude = Mathf.Lerp(initialShakeMagnitude, 0f, 1 - (shakeDuration / initialShakeMagnitude));

            if (shakeDuration <= 0)
            {
                // Reset position
                transform.localPosition = originalPosition;
            }
        }
    }

    // Method to trigger the shake
    public void Shake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
        initialShakeMagnitude = magnitude;
    }
}
