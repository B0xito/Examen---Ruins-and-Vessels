using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    void Start()
    {
        // Establecer la posición de la cámara
        transform.position = new Vector3(0, 10, -10);

        // Establecer la rotación de la cámara
        transform.rotation = Quaternion.Euler(45, 0, 0);
    }
}

