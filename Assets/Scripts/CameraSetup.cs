using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    void Start()
    {
        // Establecer la posici�n de la c�mara
        transform.position = new Vector3(0, 10, -10);

        // Establecer la rotaci�n de la c�mara
        transform.rotation = Quaternion.Euler(45, 0, 0);
    }
}

