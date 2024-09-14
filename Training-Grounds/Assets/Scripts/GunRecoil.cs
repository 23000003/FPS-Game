using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Make this script as a concept of OOP (put the values in the specific gun)
public class GunRecoil : MonoBehaviour
{
    //Scripts
    //[SerializeField] PlayerMovements playerScript;

    private bool isAiming;

    //rotations
    private Vector3 currentRotation;
    private Vector3 targetRotation;

    //Hipfire
    [SerializeField] private float recoilX;
    [SerializeField] private float recoilY;
    [SerializeField] private float recoilZ;

    //ADS
    private float aimRecoilX;
    private float aimRecoilY;
    private float aimRecoilZ;

    //Settings
    [SerializeField] private float snappiness;
    [SerializeField] private float returnSpeed;

    private Camera cameraComponent;

    private void Awake()
    {
        // Will change this for multiple guns to determine its recoil
        this.aimRecoilX = recoilX - 1f;
        this.aimRecoilY = recoilY + 1f;
        this.aimRecoilZ = recoilZ;
    }

    private void Start()
    {
        cameraComponent = gameObject.AddComponent<Camera>();
        cameraComponent.nearClipPlane = 0.01f; //object bug
    }

    private void Update()
    {
        // Smoothly interpolate the current rotation towards the target rotation
        currentRotation = Vector3.Lerp(currentRotation, targetRotation, snappiness * Time.deltaTime);

        // Apply the rotation to the gun
        transform.localRotation = Quaternion.Euler(currentRotation);

        // Gradually return the target rotation to zero
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
    }

    public void RecoilFire()
    {
        // Add recoil to the target rotation
        bool Scope = Input.GetKey(KeyCode.Mouse1);

        if (Scope && !Input.GetKey(KeyCode.LeftShift))
        {
            targetRotation += new Vector3(-aimRecoilX, Random.Range(-aimRecoilY, aimRecoilY), Random.Range(-aimRecoilZ, aimRecoilZ));
            print(targetRotation);
            print(aimRecoilX);
            print(aimRecoilY);
        }
        else
        {
            targetRotation += new Vector3(-recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
        }
    }
}
