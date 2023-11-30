using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum PieceColor
//{
//    none,
//    red,
//    blue,
//    green,
//    golden
//}

public class SpawnItem : MonoBehaviour
{
    //De donde spawnean
    MeshRenderer meshRender;
    [SerializeField] GameObject rift;

    [Header("Spawning Variables")]
    //Items a spawnear
    public GameObject[] prefabs;
    //Probabilidades de spawneo de X item (funciona a la par de la lista de prefabs)
    public float[] SpawnProbabilities;
    //Area de spawneo
    public BoxCollider spawnCollider;
    private Coroutine spawnObj;

    public List<GameObject> spawnedItems = new List<GameObject>();

    private void Start()
    {
        // Adquiere el Mesh Renderer del item que tenga el script
        meshRender = GetComponent<MeshRenderer>();
    }

    public void Mined(int howMuch)
    {
        meshRender.enabled = false;
        StartCoroutine(InvokeSpawnRandom(howMuch));
    }

    // Invoca SpawnRandom()
    private IEnumerator InvokeSpawnRandom(int howMuch)
    {
        for (int i = 0; i < howMuch; i++)
        {
            SpawnRandom();
        }
        yield return null;
    }

    // Spawnea el item
    void SpawnRandom()
    {
        // Escoge un prefab random de la lista basandose en las probabilidades
        int prefabIndex = GetRandomPrefabIndex();

        // Selecciona un punto random dentro del area de spawneo
        Vector3 randomPosition = GetRandomPositionInSpawnArea();

        // Instancia el prefab en aquella posicion
        Instantiate(prefabs[prefabIndex], randomPosition, Quaternion.identity);

        Debug.Log(prefabIndex + " " + "spawned!");
    }

    // Selecciona el item a spawnear
    int GetRandomPrefabIndex()
    {
        // Calcula la probabilidad total
        float totalProbability = 0;
        for (int i = 0; i < SpawnProbabilities.Length; i++)
        {
            totalProbability += SpawnProbabilities[i];
        }

        // Escoge un valor random entre 0 y la probabilidad total
        float randomValue = Random.value * totalProbability;

        // Determina el numero del prefab a spawnear
        float currentProbability = 0;
        for (int i = 0; i < SpawnProbabilities.Length; i++)
        {
            currentProbability += SpawnProbabilities[i];
            if (randomValue <= currentProbability)
            {
                return i;
            }
        }

        // Esto nunca deberia de pasar
        return 0;
    }

    // Selecciona la posicion de spawneo
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

    public void AddSpawned(GameObject item)
    {
        spawnedItems.Add(item);
    }

    public void RemoveSpawned(GameObject item) 
    {
        Debug.Log("Removed item");

        //spawnedItems.Remove(item);

        for (int i = 0; i < spawnedItems.Count; i++)
        {
            if (spawnedItems[i] == item)
            {
                spawnedItems.RemoveAt(i);
            }

            if (spawnedItems.Count == 0)
            {
                Destroy(rift);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedPiece") || other.CompareTag("BluePiece") ||
            other.CompareTag("GreenPiece") || other.CompareTag("GoldenPiece") ||
            other.GetComponent<Consumable>())
        {
            if (!spawnedItems.Contains(other.transform.gameObject))
            {
                AddSpawned(other.transform.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RedPiece") || other.CompareTag("BluePiece") ||
            other.CompareTag("GreenPiece") || other.CompareTag("GoldenPiece") ||
            other.GetComponent<Consumable>())
        {
            RemoveSpawned(other.transform.gameObject);
        }
    }
}
