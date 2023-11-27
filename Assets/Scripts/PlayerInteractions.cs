using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;

public class PlayerInteractions : MonoBehaviour
{
    #region Movement Variables
    [Header("Movement Variables")]
    [SerializeField] float speed = 6f;
    [SerializeField] float ogSpeed;
    [SerializeField] float runSpeed = 9f;

    [Header("Stamina System")]
    public Image staminaBar;
    [SerializeField] float currentStamina;
    [SerializeField] float maxStamina;
    [SerializeField] float runCost;
    [SerializeField] float chargeRate;
    bool isRecharging;
    #endregion

    #region Mining Variables
    [Header("Mining Variables")]
    [Range(1f, 5f)]
    [SerializeField] float rayDistance;
    [SerializeField] LayerMask minableMask;
    #endregion

    #region Consumable Variables
    [Header("Consumable Variables")]
    [SerializeField] List<Consumable> consumables = new List<Consumable>();
    Consumable currentItem;
    [SerializeField] int itemAmount;
    [SerializeField] TMP_Text itemAmountText;
    [SerializeField] Image itemUI;
    [SerializeField] Sprite notItemSprite;
    #endregion

    private void Start()
    {
        maxStamina = 100f;
        currentStamina = maxStamina;
        ogSpeed = speed;
    }

    void Update()
    {
        Movement();

        Mining();

        CheckStamina();
        staminaBar.fillAmount = currentStamina / maxStamina;

        if (Input.GetKeyDown(KeyCode.C))
        {
            ConsumeItem(currentItem);
        }

        if (consumables.Count <= 0) { itemUI.sprite = notItemSprite; }
        else { itemUI.sprite = currentItem.consumableSprite; }
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
    }

    void CheckStamina()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (currentStamina > 0)
            {
                currentStamina -= runCost * Time.deltaTime;
                speed = runSpeed;
            }
            else
            {
                currentStamina = 0;               
                speed = ogSpeed;
            }
        }
        else
        {
            speed = ogSpeed;
            if (currentStamina < maxStamina)
            {
                if (!isRecharging) StartCoroutine(RechargeStamina());
            }
        }
    }

    IEnumerator RechargeStamina()
    {
        isRecharging = true;
        yield return new WaitForSeconds(1f);
        while (currentStamina < maxStamina)
        {
            currentStamina += chargeRate / 10f;
            if (currentStamina > maxStamina) currentStamina = maxStamina;
            yield return new WaitForSeconds(.1f);
        }
        isRecharging = false;
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

    public void AddConsumable(Consumable item)
    {
        consumables.Add(item);
        Debug.Log(item.GetComponent<Consumable>().consumableName + " " + "added to consumables");
        itemUI.sprite = item.GetComponent<Consumable>().consumableSprite;
    }

    public void ConsumeItem(Consumable item)
    {
        if (consumables.Contains(item))
        {
            Debug.Log(item.GetComponent<Consumable>().consumableName + " " + "removed from consumables");
            currentStamina += item.GetComponent<Consumable>().regenerationAmount;
            consumables.Remove(item);
            itemUI.sprite = notItemSprite;
            itemAmount--;
            itemAmountText.text = itemAmount.ToString();
            if (itemAmount != 0 || consumables.Count > 0) { currentItem = consumables[0]; }
            if (itemAmount <= 0)
            {
                itemAmount = 0;
                currentItem = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Consumable>())
        {
            Debug.Log("Trigger Consumable");
            Consumable thisConsumable = other.GetComponent<Consumable>();
            AddConsumable(thisConsumable);
            currentItem = thisConsumable;
            itemAmount++;
            itemAmountText.text = itemAmount.ToString();
            if (other.CompareTag("Item"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}