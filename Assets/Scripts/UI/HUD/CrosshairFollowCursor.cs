using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairFollowCursor : UI
{
    void Start()
    {
        
    }

    void Update()
    {
        UpdateCrosshairPos();
    }

    private void UpdateCrosshairPos()
    {
        gameObject.transform.position = Input.mousePosition;
    }
}
