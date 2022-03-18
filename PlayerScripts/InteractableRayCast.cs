using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableRayCast : MonoBehaviour
{
    void FixedUpdate()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layer_mask = LayerMask.GetMask("InteractableObjects");

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        //layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 4, layer_mask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            if (hit.collider.tag == "Bed") 
            {
                BedManager.instance.updateBedUI();
                BedScript.instance.showBedUI();
            }
            if (hit.collider.tag == "Meal")
            {

            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 4, Color.white);
        }
    }
}
