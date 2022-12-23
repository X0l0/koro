using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TallGrass : MonoBehaviour
{
    private Animator animator;
    public GameObject forwardGrass;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag != "NonTriggeringCollider" && other.gameObject.tag != "WildKuro"){
            animator.Play("rustle");
            forwardGrass.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        forwardGrass.SetActive(false);
    }
}
