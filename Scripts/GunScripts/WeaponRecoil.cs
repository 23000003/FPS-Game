using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil
{

    // Rotations
    private Vector3 currentRotation;
    private Vector3 targetRotation;

    // ADS Recoil
    private readonly float aimRecoilX;
    private readonly float aimRecoilY;
    private readonly float aimRecoilZ;

    // Hipfire Recoil
    private readonly float recoilX;
    private readonly float recoilY;
    private readonly float recoilZ;

    // Settings
    private readonly float snappiness;
    private readonly float returnSpeed;

    private readonly Camera camera = GameObject.FindGameObjectWithTag("CameraRecoil").GetComponent<Camera>();

    public WeaponRecoil(float recoilX, float recoilY, float recoilZ, float snappiness, float returnSpeed)
    {
        this.recoilX = recoilX;
        this.recoilY = recoilY;
        this.recoilZ = recoilZ;
        this.snappiness = snappiness;
        this.returnSpeed = returnSpeed;

        this.aimRecoilX = recoilX - 0.5f;
        this.aimRecoilY = recoilY - 0.7f;
        this.aimRecoilZ = recoilZ - 0.2f;

    }

    public Camera GetCamera() { return camera; }

    public void CameraConfig() // START
    {
        camera.nearClipPlane = 0.01f; // Prevent object clipping issue
    }

    public void UpdateRecoilPosition() // UPDATE
    {
        // Smoothly interpolate the current rotation towards the target rotation
        currentRotation = Vector3.Lerp(currentRotation, targetRotation, snappiness * Time.deltaTime);

        // Apply the rotation to the camera
        camera.transform.localRotation = Quaternion.Euler(currentRotation);

        // Gradually return the target rotation to zero (i.e., the camera is returning to normal)
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
    }

    public void TriggerRecoilFire()
    {
        bool isAiming = Input.GetKey(KeyCode.Mouse1);

        // If aiming down sights (ADS), apply a smaller recoil
        if (isAiming && !Input.GetKey(KeyCode.LeftShift))  // Assume shift is sprint
        {
            targetRotation += new Vector3(-aimRecoilX, Random.Range(-aimRecoilY, aimRecoilY), Random.Range(-aimRecoilZ, aimRecoilZ));
        }
        else
        {
            // Hipfire recoil (larger recoil in all directions)
            targetRotation += new Vector3(-recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
        }

        // Prevent excessive upward recoil
        targetRotation.y = Mathf.Clamp(targetRotation.y, -10f, 10f);  // Limiting vertical recoil

        // Optionally, clamp the x and z recoil as well to prevent extreme angles
        targetRotation.x = Mathf.Clamp(targetRotation.x, -10f, 10f);  // Prevent the camera from going too far up or down
        targetRotation.z = Mathf.Clamp(targetRotation.z, -5f, 5f);    // Slight recoil on the Z-axis, if needed
    }
}
