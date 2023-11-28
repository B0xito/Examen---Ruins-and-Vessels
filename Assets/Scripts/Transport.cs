using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport : MonoBehaviour
{
    [SerializeField] Transform playerPosition;

    [SerializeField] Transform initialPosition;
    [SerializeField] Transform finalPosition;
    [SerializeField] Vector3 finalPositionVector;

    private void Start()
    {
        finalPositionVector = finalPosition.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerPosition = other.gameObject.transform;
            playerPosition.transform.position = finalPositionVector;
        }
    }
}
