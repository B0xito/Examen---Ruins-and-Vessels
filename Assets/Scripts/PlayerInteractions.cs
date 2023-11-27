using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInteractions : MonoBehaviour
{
    #region Movement Variables
    [Header("Movement Variables")]
    [Range(1f, 6f)]
    [SerializeField] float speed;
    float ogSpeed;
    [Range(7f, 10f)]
    [SerializeField] float runSpeed;

    public Image staminaBar;
    [SerializeField] float stamina, maxStamina;
    [SerializeField] float runCost;

    [SerializeField] float chargeRate;
    private Coroutine recharge;
    #endregion

    #region Mining Variables
    [Header("Mining Variables")]
    [Range(1f, 5f)]
    [SerializeField] float rayDistance;
    [SerializeField] LayerMask minableMask;
    #endregion

    private void Start()
    {
        maxStamina = 100f;
        stamina = maxStamina;
        ogSpeed = speed;
    }

    void Update()
    {
        Movement();
        Mining();
    }

    void Movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Esto hace que el personaje se mueva
        transform.position = transform.position + movement * speed * Time.deltaTime;

        // Esto hace que el personaje mire hacia la dirección de movimiento
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);
        }

        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            Debug.Log("Running");
            speed = runSpeed;
            stamina -= runCost;
            if (stamina < 0) stamina = 0;
        }
        else
        {
            speed = ogSpeed;
            if (stamina < maxStamina)
            {
                if (recharge != null) StopCoroutine(recharge);
                recharge = StartCoroutine(RechargeStamina());
            }
        }
        staminaBar.fillAmount = stamina / maxStamina;

    }

    private IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(1f);

        while (stamina < maxStamina)
        {
            stamina += chargeRate / 10f;
            if (stamina > maxStamina) stamina = maxStamina;
            staminaBar.fillAmount = stamina / maxStamina;
            yield return new WaitForSeconds(.1f);
        }
    }

    void Mining()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.red);

        //Revisa con un rayo desde la posicion 0,0 del player hacia adelante si hay un gameobject
        //con el layer minable, si es asi entra el if
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, minableMask))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("Mining");
                if (hit.collider.CompareTag("Rift"))
                {
                    Debug.Log("GameObject spawned");

                }
                else
                {
                    Debug.Log("Not rift");
                }
            }
        }
    }
}