using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : CoreFunc
{
    #region [ PARAMETERS ]

    public enum pickupType { energyRestore, healthRestore };
    [SerializeField] public pickupType type;
    [SerializeField] public int power = 1;

    #endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Update()
    {
        if ((int)type == 0)
        {
            transform.localEulerAngles += new Vector3(0.0f, 1.0f, 0.0f) * Time.deltaTime;
        }
    }
}
