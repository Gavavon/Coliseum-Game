using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MonoBehaviour
{
    public static Dagger instance;
    private void Awake()
    {
        instance = this;
    }

    public IEnumerator PlayerHitDestroy(GameObject OBJ)
    {
        //RagDoll
        yield return new WaitForSeconds(15f);
        Destroy(OBJ);
    }
    public IEnumerator HitDestroy(GameObject OBJ) 
    {
        OBJ.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f); ;
        yield return new WaitForSeconds(0.5f);
        while (OBJ.transform.localScale.x > 0) 
        {
            OBJ.transform.localScale += new Vector3(-0.01f, -0.01f, -0.01f);
        }
        Destroy(OBJ);
    }
    public IEnumerator TimeOutDestroy(GameObject OBJ)
    {
        yield return new WaitForSeconds(20f);
        Destroy(OBJ);
    }

}