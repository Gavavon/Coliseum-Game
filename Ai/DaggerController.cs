using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerController : MonoBehaviour
{
    public GameObject daggerProjectile;
    public Transform daggerThrowPos;

    public Collision playerCol;

    public float shootForce;

    [HideInInspector]
    public  GameObject clone;

    public static DaggerController instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        daggerProjectile.GetComponent<Rigidbody>().velocity = Vector3.zero;
        daggerProjectile.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
    [ContextMenu("createDaggerClone")]
    public void CreateDaggerClone() 
    {
        clone = Instantiate(daggerProjectile, daggerThrowPos.position, daggerThrowPos.rotation);
        clone.transform.SetParent(daggerProjectile.transform, false);
        clone.GetComponent<Rigidbody>().velocity = Vector3.zero;
        clone.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
    [ContextMenu("AddDaggerForce")]
    public void AddDaggerForce()
    {
        Vector3 directionWithSpread = new Vector3(1, 1, 0);
        clone.transform.forward = directionWithSpread.normalized;
        clone.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
    }
    void OnCollisionEnter(Collision collision)
    {
        const bool temp = true;
        switch (true) 
        {
            case temp when collision == playerCol:
                Dagger.instance.PlayerHitDestroy(daggerProjectile);
                break;
            case temp when collision != playerCol:
                Dagger.instance.HitDestroy(daggerProjectile);
                break;
            default:
                Dagger.instance.TimeOutDestroy(daggerProjectile);
                break;

        }
    }

}
