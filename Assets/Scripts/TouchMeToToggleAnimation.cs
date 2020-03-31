using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class TouchMeToToggleAnimation : MonoBehaviour
{
    [Header("Drag animator object here")]
    public Animator animator;
    [Header("Drag waiting and action animation clip here")]
    public AnimationClip animClipFirst;
    public AnimationClip animClipSecond;
    [Header("Sets transition speed")]
    public float fadeDuration = 0.2f;

    bool state;

    void Start()
    {
        if ( animator == null )
        {
            animator = GetComponent<Animator>();
        }

        if ( animator == null )
        {
            Debug.Log("Error, no animation controller found");
        }
        state = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    if (animator != null)
                    {
                        state = !state;
                        animator.CrossFade( state ? animClipSecond.name : animClipFirst.name, fadeDuration);
                    }
                }
            }
        }
    }
}
