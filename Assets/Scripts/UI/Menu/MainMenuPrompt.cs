using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuPrompt : CoreFunc
{
    #region [ PARAMETERS ]

    private GameObject promptText;
    private bool promptVis = true;

	#endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Awake()
    {
        promptText = transform.GetChild(0).gameObject;
        UpdatePrompt();
    }

    void Update()
    {
        if (Input.GetKeyDown(controls.menu.pause))
        {
            promptVis = !promptVis;
            promptText.SetActive(promptVis);
        }
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    private void UpdatePrompt()
    {
        promptText.GetComponent<TextMeshProUGUI>().text = "PRESS " + keynames.names[controls.menu.pause] + " TO SHOW/HIDE THE MENU";
    }
}
