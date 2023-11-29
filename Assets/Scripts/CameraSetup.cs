using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    void Start()
    {
        // Establecer la posición de la cámara
        transform.position = new Vector3(0, 0, 0);

        // Establecer la rotación de la cámara
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}

