using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] private Storage linkedStorage;
    [SerializeField] private ZoneTypes zoneType;

    private void Start()
    {
        if (linkedStorage == null)
        {
            linkedStorage = transform.parent.GetComponent<Storage>();
        }
    }

    private enum ZoneTypes
    {
        Get,
        Set
    }

    private void OnTriggerStay(Collider other)
    {
        Storage otherStorage = other.GetComponentInChildren<Storage>();

        switch ((int)zoneType)
        {
            case (int)ZoneTypes.Get:
                linkedStorage.SendItemTo(otherStorage);
                break;
            case (int)ZoneTypes.Set:
                otherStorage.SendItemTo(linkedStorage);
                break;
        }
    }
}
