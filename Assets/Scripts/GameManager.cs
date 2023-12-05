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

    [Header("Rifts Generation")]
    [SerializeField] GameObject rift;
    public int totalRifts;
    [SerializeField] int maxRifts = 20;
    [SerializeField] BoxCollider spawnCollider;
    [SerializeField] bool isMined;

    private void Start()
    {
        VesselsSet();
    }

    private void Update()
    {
        if (vesselsFabrication.redVesselsCount == redVesselsObj &&
            vesselsFabrication.blueVesselsCount == blueVesselsObj &&
            vesselsFabrication.greenVesselsCount == greenVesselsObj)
        {
            VesselsAdd();
        }

        if (totalRifts < maxRifts && isMined == false)
        {
            Mined(maxRifts -= totalRifts);
            Debug.Log("mined");
        }
    }


    void Mined(int howMuch)
    {
        isMined = true;
        for (int i = 0; i < howMuch; i++)
        {
            SpawnRiftInMap();
        }
        isMined = false;
        maxRifts = 20;
    }

    //IEnumerator InvokeSpawnRandom(int howMuch)
    //{
    //    for (int i = 0; i < howMuch; i++)
    //    {
    //        SpawnRiftInMap();
    //    }
    //    yield return null;
    //}

    void SpawnRiftInMap()
    {
        // Selecciona un punto random dentro del area de spawneo
        Vector3 randomPosition = GetRandomPositionInSpawnArea();

        // Instancia el prefab en aquella posicion
        Instantiate(rift, randomPosition, Quaternion.identity);

        totalRifts++;
        Debug.Log("Rift spawned");
    }

    Vector3 GetRandomPositionInSpawnArea()
    {
        // Genera una posicion random dentro del collider
        Vector3 randomPosition = new Vector3(
            Random.Range(spawnCollider.bounds.min.x, spawnCollider.bounds.max.x),
            Random.Range(spawnCollider.bounds.min.y, spawnCollider.bounds.max.y),
            Random.Range(spawnCollider.bounds.min.z, spawnCollider.bounds.max.z)
        );

        // Regresa la posicion random
        return randomPosition;
    }

    void VesselsSet()
    {
        vesselsFabrication = FindObjectOfType<VesselsFabrication>();
        redVesselsObj = Random.Range(1, 6);
        blueVesselsObj = Random.Range(1, 6);
        greenVesselsObj = Random.Range(1, 6);
        goldenVesselsObj = Random.Range(0, 3);
    }

    void VesselsAdd() 
    {       
        playerReady = true;
        exitDoorAnim.SetBool("ready", true);        
    }

}
