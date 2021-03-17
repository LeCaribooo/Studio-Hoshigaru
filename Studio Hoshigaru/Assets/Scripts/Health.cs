using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Health : MonoBehaviour
{
    public PlayerSO playerSO;

    private int numOfHearts;
    public int numOfHits;

    public Image[] hearts;
    public Sprite emptyHeart;
    public Sprite quarterHeart;
    public Sprite halfHeart;
    public Sprite hquarterHeart;
    public Sprite fullHeart;

    void Start()
    {
        numOfHearts = playerSO.numOfHearts;
        numOfHits = playerSO.numOfHits;
    } 

    void Update()
    {
        if(numOfHits > numOfHearts * 4)
        {
            numOfHits = numOfHearts * 4;
        }

        if(numOfHits == 0)
        {
            hearts[0].sprite = emptyHeart;
        }
        for (int i = 0; i < hearts.Length; i++)
            {
                if (i < numOfHits / 4)
                {
                    hearts[i].sprite = fullHeart;
                }
                else if (i == numOfHits / 4)
                {
                    switch (numOfHits % 4)
                    {
                        case 1:
                            hearts[i].sprite = quarterHeart;
                            break;
                        case 2:
                            hearts[i].sprite = halfHeart;
                            break;
                        case 3:
                            hearts[i].sprite = hquarterHeart;
                            break;
                    }
                }
                else
                {
                    hearts[i].sprite = emptyHeart;
                }
                if (i < numOfHearts)
                {
                    hearts[i].enabled = true;
                }
                else
                {
                    hearts[i].enabled = false;
                }
        }
        
    }
}

