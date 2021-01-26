using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talent : MonoBehaviour
{
    private Image sprite;

    private void Awake()
    {
        sprite = GetComponent<Image>();
    }

    public void Lock()
    {
        sprite.color = Color.gray;
    }

    public void Unlock()
    {
        sprite.color = Color.white;
    }
}
