using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway
{

    private readonly float amount;
    private readonly float maxAmount;
    private readonly float smoothAmount;

    private Vector3 initialPosition;
    private readonly Transform transform;

    public WeaponSway(float amount, float maxAmount, float smoothAmount, Transform transform)
    {
        this.amount = amount;
        this.maxAmount = maxAmount;
        this.smoothAmount = smoothAmount;
        this.initialPosition = transform.localPosition;
        this.transform = transform;
    }

    public void UpdateSway()
    {
        float movementX = -Input.GetAxis("Mouse X") * amount;
        float movementY = -Input.GetAxis("Mouse Y") * amount;
        movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
        movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

        Vector3 finalPosition = new Vector3(movementX, movementY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothAmount);
    }
}


