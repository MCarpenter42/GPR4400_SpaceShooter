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

    private Rigidbody rb;

    [SerializeField] float maxMoveSpeed = 1.0f;
    [SerializeField] float sprintMulti = 4.0f;

    [SerializeField] float maxRotSpeed = 1.0f;
    [SerializeField] bool invertPitch = false;
    private int pitchInv = -1;

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
        rb = gameObject.GetComponent<Rigidbody>();
        if (invertPitch)
        {
            pitchInv = 1;
        }
    }

    void Start()
    {
    }

    void Update()
    {
        Steering();
        Movement();
    }

    void FixedUpdate()
    {
        CapSpeeds();
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

            float rotScalePitch = adjustedCursorPos[1] / (screenCentre[1] - deadzoneSize) * pitchInv;
            float rotScaleYaw = adjustedCursorPos[0] / (screenCentre[0] - deadzoneSize);

            Vector3 rotScaleVector = new Vector3(rotScalePitch, rotScaleYaw, 0.0f);

            rb.AddRelativeTorque(rotScaleVector * maxRotSpeed);
        }
        else
        {
        }
    }

    private Vector3 RelativeCursorPos()
    {
        return Input.mousePosition - screenCentre;
    }

    private void Movement()
    {
        Vector3 movementInput = new Vector3();
        if (Input.GetKey(controls.movement.forward))
        {
            movementInput[2] += 1;
        }
        if (Input.GetKey(controls.movement.back))
        {
            movementInput[2] -= 1;
        }
        if (Input.GetKey(controls.movement.right))
        {
            movementInput[0] += 1;
        }
        if (Input.GetKey(controls.movement.left))
        {
            movementInput[0] -= 1;
        }
        if (Input.GetKey(controls.movement.up))
        {
            movementInput[1] += 1;
        }
        if (Input.GetKey(controls.movement.down))
        {
            movementInput[1] -= 1;
        }

        rb.AddRelativeForce(movementInput * maxMoveSpeed * Sprint());
    }

    private float Sprint()
    {
        if (Input.GetKey(controls.movement.sprint))
        {
            return sprintMulti;
        }
        else
        {
            return 1.0f;
        }
    }

    private void CapSpeeds()
    {
        Vector3 capVel = new Vector3();
        Vector3 capRot = new Vector3();
        if (rb.velocity.magnitude > maxMoveSpeed)
        {
            capVel = rb.velocity.normalized * maxMoveSpeed;
            rb.velocity = capVel;
        }
        if (rb.angularVelocity[0] > maxRotSpeed || rb.angularVelocity[1] > maxRotSpeed)
        {
            float xRot = rb.angularVelocity[0];
            float yRot = rb.angularVelocity[1];
            float zRot = rb.angularVelocity[2];
            Mathf.Clamp(xRot, -maxRotSpeed, maxRotSpeed);
            Mathf.Clamp(yRot, -maxRotSpeed, maxRotSpeed);
            capRot = new Vector3(xRot, yRot, zRot);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Objective"))
        {
            Debug.Log("Collided with objective item");
            Destroy(other.gameObject, 0.1f);
        }
    }
}
