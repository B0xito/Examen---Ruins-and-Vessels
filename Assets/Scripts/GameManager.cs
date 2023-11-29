using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Vessels Target")]
    [SerializeField] VesselsFabrication vesselsFabrication;

    public int redVesselsObj;
    public int blueVesselsObj;
    public int greenVesselsObj;
    public int goldenVesselsObj;

    [SerializeField] bool playerReady = false;
    [SerializeField] Animator exitDoorAnim; //Parent door

    private void Start()
    {
        vesselsFabrication = FindObjectOfType<VesselsFabrication>();
        redVesselsObj = Random.Range(1, 6);
        blueVesselsObj = Random.Range(1, 6);
        greenVesselsObj = Random.Range(1, 6);
        goldenVesselsObj = Random.Range(0, 3);
    }

    private void Update()
    {
        if (vesselsFabrication.redVesselsCount == redVesselsObj &&
            vesselsFabrication.blueVesselsCount == blueVesselsObj &&
            vesselsFabrication.greenVesselsCount == greenVesselsObj)
        {
            playerReady = true;
            exitDoorAnim.SetBool("ready", true);
        }
    }
}
