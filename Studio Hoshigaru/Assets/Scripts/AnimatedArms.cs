using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedArms : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [HideInInspector] public Vector2 direction;

    void Update()
    {
        Debug.Log(transform.rotation.eulerAngles.z);
        Vector2 bowPosition = transform.position;
        Vector2 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - bowPosition;
        transform.right = direction;
    }
}