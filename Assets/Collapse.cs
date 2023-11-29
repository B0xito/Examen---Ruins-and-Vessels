using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collapse : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] Transform player;
    [SerializeField] float shakeDuration = 5f;
    [SerializeField] float shakeMagnitude = 0.7f;
    [SerializeField] Transform initialPosition;
    [SerializeField] bool isCollidingWithShelter = false;

    private void Start()
    {
        player = GetComponent<Transform>();
        playerCamera = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))  // Reemplaza esto con la condición para iniciar el colapso
        {
            StartCoroutine(StartCollapse());
        }
    }

    private IEnumerator StartCollapse()
    {
        float elapsed = 0.0f;

        while (elapsed < shakeDuration && !isCollidingWithShelter)
        {
            Vector3 originalCamPos = playerCamera.transform.position;

            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            playerCamera.transform.position = new Vector3(originalCamPos.x + x, originalCamPos.y + y, originalCamPos.z);

            elapsed += Time.deltaTime;

            yield return null;

            playerCamera.transform.position = originalCamPos;
        }

        if (!isCollidingWithShelter)
        {
            player.transform.position = initialPosition.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shelter"))
        {
            isCollidingWithShelter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Shelter"))
        {
            isCollidingWithShelter = false;
        }
    }
}
