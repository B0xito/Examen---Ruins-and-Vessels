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
    [SerializeField] Vector3 initialCamPos;
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
            print("1");
            initialCamPos = playerCamera.gameObject.transform.localPosition;
            StartCoroutine(StartCollapse());
        }
    }

    private IEnumerator StartCollapse()
    {
        print("2");
        float elapsed = 0.0f;

        while (elapsed < shakeDuration && !isCollidingWithShelter)
        {
            print("3");
            Vector3 originalCamPos = playerCamera.transform.localPosition;

            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            playerCamera.transform.localPosition = new Vector3(originalCamPos.x + x, originalCamPos.y + y, originalCamPos.z);

            elapsed += Time.deltaTime;

            yield return null;

            playerCamera.transform.localPosition = originalCamPos;
        }
        if (!isCollidingWithShelter)
        {
            Debug.Log("Time out! Position restablishied");
            player.transform.position = initialPosition.position;
        }

        print("4");
        playerCamera.gameObject.transform.localPosition = initialCamPos;
        print("5");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shelter"))
        {
            isCollidingWithShelter = true;
            playerCamera.gameObject.transform.localPosition = initialCamPos;
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