using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

namespace InventoryExample.Control
{
    [RequireComponent(typeof(PlayerController))]
    public class PickupsInRange : MonoBehaviour
    {
        public List<PressToPickup> pickupsInRange = new List<PressToPickup>();
        PressToPickup targetPickup = null;
        float intervalToUpdate = .1f;
        float updateTimer = Mathf.Infinity;

        private void Update()
        {
            if (updateTimer > intervalToUpdate)
            {
                UpdateTarget();
                updateTimer = 0;
            }
            if (Input.GetKeyDown(KeyCode.P) && targetPickup.GetComponent<Pickup>().CanBePickedUp())
            {
                targetPickup.gameObject.GetComponent<Pickup>().PickupItem();
                pickupsInRange.Remove(targetPickup);
            }
            updateTimer += Time.deltaTime;
        }

        private void UpdateTarget()
        {
            if (pickupsInRange.Count == 0) return;
            if (pickupsInRange.Count == 1) targetPickup = pickupsInRange[0];
            else targetPickup = pickupsInRange[FindClosestPickup()];
            targetPickup.IsClosestPickup();
        }
        private int FindClosestPickup()
        {
            int closestPickup = 0;
            for (int i = 0; i < pickupsInRange.Count - 1; i++)
            {
                float dist = Vector3.Distance(pickupsInRange[closestPickup].transform.position, transform.position);
                if (dist < Vector3.Distance(pickupsInRange[i + 1].transform.position, transform.position))
                {
                    pickupsInRange[i + 1].NotClosestPickup();
                }
                else
                {
                    pickupsInRange[i].NotClosestPickup();
                    closestPickup = i + 1;
                }
            }
            return closestPickup;
        }
        public bool IsClosestPickup(PressToPickup pickup)
        {
            if (targetPickup == pickup) return true;
            else return false;
        }
        public void AddToPickupList(PressToPickup pressToPickup)
        {
            pickupsInRange.Add(pressToPickup);
        }
        public void RemoveFromPickupList(PressToPickup pressToPickup)
        {
            pickupsInRange.Remove(pressToPickup);
        }

    }
}
