using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public Transform[] backgrounds; // contient tous les layers du background (et foreground)
    private float[] parallaxScales; //mouvement de camera pour les layers
    public float smoothing = 1f; //le nom parle de lui meme pour chaque layer (a fixer absolument au dessus de 0)

    private Transform cam; //reference a la camera reliee (principale)
    private Vector3 previousCamPos; //position de la cam a la frame precedente

    void Awake() // appele avant la fonction start
    {
        //reference de la camera
        cam = Camera.main.transform;
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //la frame precedente a la position de la frame actuelle
        previousCamPos = cam.position;
        
        //assigne a chaque background le parralaxScales correspondant
        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //pour chaque background
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];
            //initialise la position visee
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;
            
            //cree la position visee
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y,backgrounds[i].position.z);
            
            //effet "smooth" entre la position actuelle et la position visee
            backgrounds[i].position =
                Vector3.Lerp (backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);

        }
        
        //bouge la position precedente de la cam a la position a la fin de la frame
        previousCamPos = cam.position;

    }
}
