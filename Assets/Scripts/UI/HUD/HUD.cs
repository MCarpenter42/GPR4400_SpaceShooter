using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : UI
{
    [SerializeField] private Texture2D empty;
    private CursorMode cursorMode;

    void Start()
    {
        cursorMode = CursorMode.Auto;
        Cursor.SetCursor(empty, Vector2.zero, cursorMode);
    }

    void Update()
    {
        
    }
}
