using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorState : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rb;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _animator.SetFloat("velocityX",Mathf.Abs(_rb.velocity.x));
        _animator.SetFloat("velocityY",_rb.velocity.y);
    }
}
