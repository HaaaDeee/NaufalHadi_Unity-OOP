using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    //fields
    [SerializeField] Weapon weaponHolder;

    Weapon weapon;

    void Awake()
    {
        if(weaponHolder != null)
        {
            weapon = Instantiate(weaponHolder);
        }
        else
        {
            Debug.LogError("Weapon Holder is null");
            enabled = false;
        }
    }

    void Start()
    {
        if (weapon == null)
        {
            Debug.LogError("Weapon is null");
        }
        else
        {
            TurnVisual(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null)
        {
            return;
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            if(other.GetComponentInChildren<Weapon>() != null)
            {
                TurnVisual(false, other.GetComponentInChildren<Weapon>());
            }
            weapon.transform.SetParent(other.transform);
            weapon.transform.localPosition = Vector3.zero;
            TurnVisual(true, weapon);
        }
    }

    void TurnVisual(bool on)
    {
        weapon.gameObject.SetActive(on);
    }

    void TurnVisual(bool on, Weapon weapon)
    {
        weapon.gameObject.SetActive(on);
    }
}
