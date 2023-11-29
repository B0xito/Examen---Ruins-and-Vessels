using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VesselsFabrication : MonoBehaviour
{
    [SerializeField] PlayerInteractions playerInteractions;
    [SerializeField] GameManager gameManager;
    [SerializeField] Animator piecesUIAnim;

    public int redVesselsCount;
    [SerializeField] TMP_Text redVesselsText;

    public int blueVesselsCount;
    [SerializeField] TMP_Text blueVesselsText;

    public int greenVesselsCount;
    [SerializeField] TMP_Text greenVesselsText;

    public int goldenVesselsCount;
    [SerializeField] TMP_Text goldenVesselsText;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        redVesselsText.text = redVesselsCount.ToString() + " " + "/" + " " + gameManager.redVesselsObj.ToString();
        blueVesselsText.text = blueVesselsCount.ToString() + " " + "/" + " " + gameManager.blueVesselsObj.ToString();
        greenVesselsText.text = greenVesselsCount.ToString() + " " + "/" + " " + gameManager.greenVesselsObj.ToString();
        goldenVesselsText.text = goldenVesselsCount.ToString() + " " + "/" + " " + gameManager.goldenVesselsObj.ToString();

    }

    void FabricateRed()
    {
        playerInteractions.redCount -= 10;
        redVesselsCount++;
    }

    void FabricateBlue()
    {
        playerInteractions.blueCount -= 10;
        blueVesselsCount++;      
    }

    void FabricateGreen()
    {
        playerInteractions.greenCount -= 10;
        greenVesselsCount++;
    }

    void FabricateGolden() 
    { 
        playerInteractions.goldenCount -= 10;
        goldenVesselsCount++;
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerInteractions>())
        {
            piecesUIAnim.SetFloat("status", 1);
            playerInteractions = other.GetComponent<PlayerInteractions>();
            if (playerInteractions.redCount >= 10) { FabricateRed(); }
            if (playerInteractions.blueCount >= 10) { FabricateBlue(); }
            if (playerInteractions.greenCount >= 10) { FabricateGreen(); }
            if (playerInteractions.goldenCount >= 10) { FabricateGolden(); }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        piecesUIAnim.SetFloat("status", 0);
    }
}
