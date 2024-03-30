using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public UnityEvent thisTriggerEvent;
    [SerializeField] private GameObject theObject;

    private void OnTriggerEnter(Collider other)
    {
        if (theObject != null)
        {
            theObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (theObject != null)
        {
            theObject.SetActive(false);
        }
    }
}
