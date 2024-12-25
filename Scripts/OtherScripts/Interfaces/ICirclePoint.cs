using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public interface ICirclePoint
{

    void OnTriggerEnter(Collider other);

    void OnTriggerExit(Collider other);

    void DisableProgressBar();

    void EnableProgressBar();

}
