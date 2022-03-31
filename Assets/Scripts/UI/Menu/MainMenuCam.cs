using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCam : CoreFunc
{
    #region [ PARAMETERS ]

    private float yRot;
    [SerializeField] float yRotRate;

	#endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Awake()
    {
        yRot = transform.eulerAngles[1];
    }

    void FixedUpdate()
    {
        RotateCam();
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */
	
    private void RotateCam()
    {
        yRot += yRotRate * Time.deltaTime;
        if (yRot > 180.0f)
        {
            yRot -= 360.0f;
        }
        else if (yRot < -180.0f)
        {
            yRot += 360.0f;
        }
        transform.eulerAngles = new Vector3(0.0f, yRot, 0.0f);
    }
}
