using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : HUD
{
    #region [ PARAMETERS [

    [SerializeField] Color activeColour;
    private Color inactiveColour = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    private Image indictorHighlight;
    private Image indictorIcon;

    #endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Awake()
    {
        GetComponents();
    }
    
    void Start()
    {
        ActiveInd(false);
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    private void GetComponents()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject target = gameObject.transform.GetChild(i).gameObject;
            if (target.CompareTag("Highlight"))
            {
                indictorHighlight = target.GetComponent<Image>();
            }
            if (target.CompareTag("Icon"))
            {
                indictorIcon = target.GetComponent<Image>();
                inactiveColour = indictorIcon.color;
            }
        }
    }

    public void ActiveInd(bool indIsActive)
    {
        if (indIsActive)
        {
            indictorHighlight.color = activeColour;
            indictorIcon.color = activeColour;
        }
        else
        {
            indictorHighlight.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            indictorIcon.color = inactiveColour;
        }
    }
}
