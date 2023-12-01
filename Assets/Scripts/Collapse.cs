using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Collapse : MonoBehaviour
{
    [Header("Shake Variables")]
    [SerializeField] Camera playerCamera;
    [SerializeField] Transform player;
    [SerializeField] float shakeDuration = 5f;
    [SerializeField] float shakeMagnitude = 0.7f;
    [SerializeField] Transform initialPosition;
    [SerializeField] Vector3 initialCamPos;
    [SerializeField] bool isCollidingWithShelter = false;
    [SerializeField] PlayerInteractions playerInteractions;
    [SerializeField] float collapseProbabilities;
    [SerializeField] float collapseTotalProbabilities;
    [SerializeField] float capCollapseProbability = 100;
    [SerializeField] Image collapseBar;
    [SerializeField] TMP_Text timer;
    [SerializeField] bool isShaking;

    private void Start()
    {
        player = GetComponent<Transform>();
        //playerCamera = GetComponentInChildren<Camera>();
        collapseTotalProbabilities = 0;
        capCollapseProbability = 100;
    }

    void Update()
    {
        timer.text = " ";
        //collapseBar.fillAmount = collapseTotalProbabilities / capCollapseProbability;
        if (collapseTotalProbabilities >= capCollapseProbability && !isShaking)  // Reemplaza esto con la condición para iniciar el colapso
        {
            print("1");
            initialCamPos = playerCamera.gameObject.transform.localPosition;
            isShaking = true;
            StartCoroutine(StartCollapse());
            collapseBar.fillAmount = collapseTotalProbabilities / capCollapseProbability;
        }
    }

    public void CollapseProb()
    {
        collapseProbabilities = Random.Range(0, 25);
        collapseTotalProbabilities += collapseProbabilities;
        float prob = collapseTotalProbabilities / capCollapseProbability;
        Debug.Log(prob);
        collapseBar.fillAmount = collapseTotalProbabilities / capCollapseProbability;
    }

    private IEnumerator StartCollapse()
    {
        print("2");
        float elapsed = 0.0f;
        timer.text = elapsed.ToString("F0");

        while (elapsed < shakeDuration)
        {
            print("3");
            Vector3 originalCamPos = playerCamera.transform.localPosition;

            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            playerCamera.transform.localPosition = new Vector3(originalCamPos.x + x, originalCamPos.y + y, originalCamPos.z);

            elapsed += Time.deltaTime;
            timer.text = elapsed.ToString("F0");

            yield return null;

            playerCamera.transform.localPosition = originalCamPos;
        }
        if (!isCollidingWithShelter)
        {
            Debug.Log("Time out! Position restablishied");
            player.transform.position = initialPosition.position;
            collapseTotalProbabilities = 0;
        }

        print("4");
        playerCamera.gameObject.transform.localPosition = initialCamPos;
        collapseTotalProbabilities = 0;
        collapseBar.fillAmount = collapseTotalProbabilities / capCollapseProbability;
        isShaking = false;
        print("5");
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