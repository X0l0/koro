using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class antistuckpoint : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)//on the attack trigger hitbox colliding with the enemies hitbox
    {
        if (collision.CompareTag("collisionbox"))//compares tag for enemy, might be removable because of layering?
        {
            StartCoroutine("collideroff");
            //Debug.Log("antistuckpoint activated1");
        }
    }

    public IEnumerator collideroff()
    {
        transform.parent.gameObject.GetComponentInParent<CapsuleCollider2D>().enabled = false;
        //gameObject.GetComponentInParent<PolygonCollider2D>().enabled = false;
        //Debug.Log("colliderdeactivated");
        yield return new WaitForSeconds(0.1f);
        transform.parent.gameObject.GetComponentInParent<CapsuleCollider2D>().enabled = true;
        //gameObject.GetComponentInParent<PolygonCollider2D>().enabled = true;
        //Debug.Log("collideractivated");
    }
}
