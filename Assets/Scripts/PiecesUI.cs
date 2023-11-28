using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesUI : MonoBehaviour
{
    Animator uiAnim;
    [SerializeField] PlayerInteractions playerInteractions;

    private void Start()
    {
        uiAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerInteractions.addingPiece == true)
        {
            OpenPreview();
        }
        else
        {
            ClosePreview();
        }
    }

    void OpenPreview()
    {
        uiAnim.SetFloat("status", 1);
    }

    void ClosePreview()
    {
        uiAnim?.SetFloat("status", 0);
    }
}
