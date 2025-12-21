using UnityEngine;

public class RaycastInteractor : MonoBehaviour
{
    public GameObject PerformRaycast(float range, LayerMask layerMask)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, range, layerMask))

        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * range, Color.red);
            Debug.Log("Did not Hit");
            return null;
        }

        return hit.transform.gameObject;
    }
}
