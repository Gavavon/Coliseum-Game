using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{

    Animator animator;
    Vector2 input;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        input.x = Input.GetAxis(GameConstants.k_AxisNameHorizontal);
        input.y = Input.GetAxis(GameConstants.k_AxisNameVertical);

        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.y);
    }
}
