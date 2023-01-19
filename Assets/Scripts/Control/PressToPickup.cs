using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

namespace InventoryExample.Control
{
    [RequireComponent(typeof(Pickup))]
    public class PressToPickup : MonoBehaviour
    {
        [SerializeField] KeyCode keyCode;
        [SerializeField] GameObject pickupIcon;
        bool pickupPressed;
        bool isClosest = false;
        private void Awake()
        {
            pickupIcon.SetActive(false);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<PickupsInRange>().AddToPickupList(this);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<PickupsInRange>().RemoveFromPickupList(this);
                pickupIcon.SetActive(false);
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (isClosest)
                {
                    pickupIcon.SetActive(true);
                }
                else
                {
                    pickupIcon.SetActive(false);
                }
            }
        }
        public void IsClosestPickup()
        {
            isClosest = true;
        }
        public void NotClosestPickup()
        {
            isClosest = false;
        }
    }
}