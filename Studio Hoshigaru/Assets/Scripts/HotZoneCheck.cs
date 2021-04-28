using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotZoneCheck : MonoBehaviour
{
    private Patroller enemyParent;
    private bool inRange;
    private Animator anim;

    private void Awake()
    {
        enemyParent = GetComponentInParent<Patroller>();
        anim = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if(inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            if(enemyParent.target != null)
            {
                if(enemyParent.target.position.x < enemyParent.transform.position.x)
                {
                    enemyParent.Flip("left");
                }
                else
                {
                    enemyParent.Flip("right");
                }
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            inRange = false;
            gameObject.SetActive(false);
            enemyParent.triggerArea.SetActive(true);
            enemyParent.inRange = false;
            enemyParent.target = null;
            if(enemyParent.cooling == false)
            {
                enemyParent.Move();
            }
        }
    }
}
