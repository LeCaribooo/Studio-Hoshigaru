using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talent : MonoBehaviour
{
    private Image sprite;

    private int currentcount;

    [SerializeField]
    private Talent childTalent;

    [SerializeField]
    private int alreadyuse;

    [SerializeField]
    private bool _unlock;
    
    private void Awake()
    {
        sprite = GetComponent<Image>();
    }

    public bool Click()
    {
        if (currentcount < alreadyuse && _unlock)
        {
            currentcount++;
            Lock();
            if (childTalent != null)
            {
                childTalent.Unlock();
            }
            return true;
        }

        return false;
    }

    public void Lock()
    {
        sprite.color = Color.gray;
    }

    public void Unlock()
    {
        _unlock = true;
        sprite.color = Color.white;
         
    }
}
