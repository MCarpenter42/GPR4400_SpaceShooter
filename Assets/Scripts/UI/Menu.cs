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

	#endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Awake()
    {
        frames = GetChildrenWithTag(this.gameObject, "MenuFrame");
    }

    void Start()
    {
        if (isPauseMenu)
        {
            ShowMenu(false);
        }
    }
	
    void Update()
    {
        if (isPauseMenu && Input.GetKeyDown(controls.menu.pause))
        {
            TogglePause();
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
        gameState.isPaused = !gameState.isPaused;
    }

    public void ShowMenu(bool show)
    {
        if (show)
        {
            frames[activeFrame].SetActive(true);
            isActive = true;
            CursorVis(true);
        }
        else
        {
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
        activeFrame = frameID;
        ShowMenu(false);
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
}
