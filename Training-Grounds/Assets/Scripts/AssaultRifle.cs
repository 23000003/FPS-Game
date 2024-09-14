using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//POLYMORPHISM
public class AssaultRifle : GunTrigger
{
    protected override void Start()
    {
        base.Start(); // to start its inherited functionality
        GunSettings(40f, 0.1f, 1.0f, 0f, 30, 200);
    }

    protected override void Update()
    {
        base.Update(); // to start its inherited functionality
    }
}
