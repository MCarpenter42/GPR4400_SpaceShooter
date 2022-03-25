using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : CoreFunc
{
    #region [ PARAMETERS ]

    private Camera mainCam;
    private Vector3 screenCentre;

    [SerializeField] float deadzoneScale = 1.0f;
    private float deadzoneSize;

    private GameObject uiCanvas;

    private RectTransform crosshairRect_Static;
    private RectTransform crosshairRect_Dynamic;

    #endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Awake()
    {
        mainCam = Camera.main;
        float xPos = mainCam.pixelWidth / 2.0f;
        float yPos = mainCam.pixelHeight / 2.0f;
        screenCentre = new Vector3(xPos, yPos, 0.0f);
        GetUIComponents();
        SetDeadzone();
    }

    void Start()
    {
    }

    void Update()
    {
        Steering();
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    private void SetDeadzone()
    {
        Vector3 refScale = uiCanvas.GetComponent<CanvasScaler>().referenceResolution;
        float baseScale = 0.2f;
        float newScale = baseScale * deadzoneScale / 2;
        float rectScale = refScale[1] * newScale;
        deadzoneSize = screenCentre[1] * newScale;
        crosshairRect_Static.sizeDelta = new Vector2(rectScale, rectScale);
        crosshairRect_Dynamic.sizeDelta = new Vector2(rectScale, rectScale);
    }

    private void GetUIComponents()
    {
        uiCanvas = GameObject.FindGameObjectWithTag("UI");

        GameObject crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        for (int i = 0; i < crosshair.transform.childCount; i++)
        {
            GameObject obj = crosshair.transform.GetChild(i).gameObject;
            if (obj.CompareTag("Static"))
            {
                crosshairRect_Static = obj.GetComponent<RectTransform>();
            }
            else if (obj.CompareTag("Dynamic"))
            {
                crosshairRect_Dynamic = obj.GetComponent<RectTransform>();
            }
        }
    }

    private void Steering()
    {
        float trueDisp = RelativeCursorPos().magnitude;
        float adjustedDisp = trueDisp - deadzoneSize;
        if (trueDisp > deadzoneSize)
        {
            Vector3 adjustedCursorPos = RelativeCursorPos() * (adjustedDisp / trueDisp);
            Debug.Log("Cursor outside deadzone | " + adjustedCursorPos);
        }
        else
        {
            Debug.Log("Cursor inside deadzone");
        }
    }

    private Vector3 RelativeCursorPos()
    {
        return Input.mousePosition - screenCentre;
    }
}
