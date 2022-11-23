using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TallGrass : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other){
        animator.Play("rustle");
    }
}
