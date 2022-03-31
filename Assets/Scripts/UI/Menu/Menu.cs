using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : UI
{
    #region [ PARAMETERS ]

    [SerializeField] bool isPauseMenu = false;
    private bool isActive = true;

    private List<GameObject> frames = new List<GameObject>();
    private int activeFrame = 0;

    [SerializeField] Texture2D normal = null;
    [SerializeField] Texture2D hidden;
    private CursorMode cursorMode = CursorMode.Auto;

    private List<GameObject> panels = new List<GameObject>();

	#endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Awake()
    {
        frames = GetChildrenWithTag(this.gameObject, "MenuFrame");
        GetPanels();
    }

    void Start()
    {
        ShowMenu(false);
        if (!isPauseMenu)
        {
            activeFrame = 0;
        }
        PanelsColourCycle();
    }
	
    void Update()
    {
        if (Input.GetKeyDown(controls.menu.pause))
        {
            ShowMenu(!isActive);
        }
    }

    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            if (isActive)
            {
                CursorVis(true);
            }
            else
            {
                CursorVis(false);
            }
        }
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    private void GetPanels()
    {
        foreach (GameObject frame in frames)
        {
            for (int i = 0; i < frame.transform.childCount; i++)
            {
                GameObject target = frame.transform.GetChild(i).gameObject;
                if (target.CompareTag("MenuPanel"))
                {
                    panels.Add(target);
                }
            }
        }
    }

    public void TogglePause()
    {
        if (gameState.isPaused == true)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void ShowMenu(bool show)
    {
        if (show)
        {
            if (isPauseMenu)
            {
                Pause();
            }
            frames[activeFrame].SetActive(true);
            isActive = true;
            CursorVis(true);
        }
        else
        {
            if (isPauseMenu)
            {
                Resume();
            }
            foreach (GameObject frame in frames)
            {
                frame.SetActive(false);
            }
            isActive = false;
            CursorVis(false);
        }
    }

    public void ChangeFrame(int frameID)
    {
        frames[activeFrame].SetActive(false);
        activeFrame = frameID;
        frames[frameID].SetActive(true);
    }

    private void CursorVis(bool vis)
    {
        if (vis)
        {
            if (gameState.isInEditor)
            {
                Cursor.SetCursor(normal, Vector2.zero, cursorMode);
            }
            Cursor.visible = true;
        }
        else
        {
            if (gameState.isInEditor)
            {
                Cursor.SetCursor(hidden, Vector2.zero, cursorMode);
            }
            Cursor.visible = false;
        }
    }

    private void PanelsColourCycle()
    {
        foreach (GameObject panel in panels)
        {
            MenuPanel panelHandler = panel.GetComponent<MenuPanel>();
            if (panelHandler.doColourCycle)
            {
                DoColourCycle(panel.GetComponent<Image>(), (int)panelHandler.cycleType, true, (int)panelHandler.clr1, (int)panelHandler.clr2);
            }
        }
    }
}
