using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : CoreFunc
{
    #region [ PARAMETERS ]

    private GameObject groupParent;
    private MeshCollider meshCollider;
    private List<GameObject> barriers = new List<GameObject>();
    private List<Rigidbody> pieces = new List<Rigidbody>();

    [SerializeField] float breakForce;

    #endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Awake()
    {
        GetComponents();
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    private void GetComponents()
    {
        groupParent = gameObject.transform.parent.gameObject;

        meshCollider = gameObject.GetComponent<MeshCollider>();

        barriers = GetChildrenWithTag(groupParent, "Barrier");

        List<GameObject> pieceObjs = GetChildrenWithComponent<Rigidbody>(groupParent);
        foreach (GameObject piece in pieceObjs)
        {
            pieces.Add(piece.GetComponent<Rigidbody>());
        }
    }

    public void Break(Vector3 hitPoint)
    {
        foreach (GameObject barrier in barriers)
        {
            barrier.SetActive(false);
        }

        meshCollider.enabled = false;

        foreach (Rigidbody piece in pieces)
        {
            piece.AddExplosionForce(breakForce, hitPoint, meshCollider.bounds.size.x);
        }
    }
}
