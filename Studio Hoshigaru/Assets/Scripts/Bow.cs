using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{


    public GameObject arrow;
    public float launchForce;
    public Transform shotPoint;

    public GameObject point;
    GameObject[] points;
    public int numberOfPoints;
    public float spaceBetweenPoints;

    public AnimatedArms animatedArms;

    void Update()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i].transform.position = PointPosition(i * spaceBetweenPoints);
        }
    }
    
    void Shoot()
    {
        GameObject newArrow = Instantiate(arrow, shotPoint.position, shotPoint.rotation);
        newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
    }


    Vector2 PointPosition(float t)
    {
        Vector2 position = (Vector2)shotPoint.position + (animatedArms.direction.normalized * launchForce * t) + 0.5f * Physics2D.gravity * (t * t);
        return position;
    }

    private void OnDisable()
    {
        foreach(GameObject point in points)
        {
            Destroy(point);
        }
    }

    private void OnEnable()
    {
        points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, shotPoint.position, Quaternion.identity);
        }
    }
}
