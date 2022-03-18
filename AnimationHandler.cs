using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    Animator animator;
    Vector2 input;

    int isSprintingParam = Animator.StringToHash("IsSprinting");

    int inputX = Animator.StringToHash("InputX");
    int inputY = Animator.StringToHash("InputY");

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        input.x = StarterAssetsInputs.instance.look.x;
        input.y = StarterAssetsInputs.instance.look.y;

        animator.SetFloat(inputX, input.x);
        animator.SetFloat(inputY, input.y);

        animator.SetBool(isSprintingParam, StarterAssetsInputs.instance.sprint);
    }
}
