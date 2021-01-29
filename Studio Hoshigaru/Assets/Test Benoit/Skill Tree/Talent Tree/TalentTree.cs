using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentTree : MonoBehaviour
{
    private int points = 10;

    [SerializeField]
    private Talent[] talents;

    [SerializeField]
    private Talent[] unlockedByDefault;

    [SerializeField]
    private Text talentPointText;

    public int MyPoint
    {
        get
        {
            return points;
        }
        set 
        {
            points = value;
            UpdateTalentPointText();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        ResetTalents();
    }

    public void TryUseTalent(Talent talent)
    {
        if (MyPoint > 0 && talent.Click())
        {
            MyPoint --;
        }
    }

    private void ResetTalents()
    {
        UpdateTalentPointText();

        foreach (Talent talent in talents)
        {
            talent.Lock();
        }

        foreach (Talent talent in unlockedByDefault)
        {
            talent.Unlock();
        }
    }

    private void UpdateTalentPointText()
    {
        talentPointText.text = " "+ points.ToString();
    }
}
