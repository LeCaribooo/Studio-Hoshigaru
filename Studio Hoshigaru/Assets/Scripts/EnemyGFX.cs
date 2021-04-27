using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    // Start is called before the first frame update
    private AIPath aIPath;

    private void Start()
    {
        aIPath = GetComponentInParent<AIPath>();
    }

    // Update is called once per frame
    void Update()
    {

        if (aIPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-0.225f, 0.2175f, 1f);
        }
        else if (aIPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(0.255f, 0.2175f, 1f);
        }
    }
}
