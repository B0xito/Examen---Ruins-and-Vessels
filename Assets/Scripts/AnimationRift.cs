using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRift : MonoBehaviour
{
    [SerializeField] PlayerInteractions playerInteractions;

    private void Start()
    {
        playerInteractions = FindObjectOfType<PlayerInteractions>();
    }

    public void CallFunction()
    {
        playerInteractions.SpawnItemsFromRift();
    }
}
