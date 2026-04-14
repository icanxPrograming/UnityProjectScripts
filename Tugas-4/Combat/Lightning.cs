using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    void Start()
    {
        Animator anim = GetComponent<Animator>();

        if (anim != null)
        {
            float animLength = anim.GetCurrentAnimatorStateInfo(0).length;
            Destroy(gameObject, animLength);
        }
        else
        {
            Destroy(gameObject, 1f);
        }
    }
}