using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreFunc : MonoBehaviour
{
    public static GameState gameState = new GameState();
    public static Controls controls = new Controls();

    public static void Pause()
    {
        Time.timeScale = 0.0f;
    }
    
    public static void Resume()
    {
        Time.timeScale = 1.0f;
    }

    public static float ToRad(float degrees)
    {
        return degrees * Mathf.PI / 180.0f;
    }

    public static float ToDeg(float radians)
    {
        return radians * 180.0f / Mathf.PI;
    }

    public List<GameObject> GetChildrenWithComponent<T>(GameObject parentObj)
    {
        List<GameObject> childrenWithComponent = new List<GameObject>();
        if (parentObj.transform.childCount > 0)
        {
            for (int i = 0; i < parentObj.transform.childCount; i++)
            {
                GameObject child = parentObj.transform.GetChild(i).gameObject;
                if (child.GetComponent<T>() != null)
                {
                    childrenWithComponent.Add(child);
                }
            }
        }
        return childrenWithComponent;
    }
    
    public List<GameObject> GetChildrenWithTag(GameObject parentObj, string tag)
    {
        List<GameObject> childrenWithTag = new List<GameObject>();
        if (parentObj.transform.childCount > 0)
        {
            for (int i = 0; i < parentObj.transform.childCount; i++)
            {
                GameObject child = parentObj.transform.GetChild(i).gameObject;
                if (child.CompareTag(tag))
                {
                    childrenWithTag.Add(child);
                }
            }
        }
        return childrenWithTag;
    }

    /*public static void DoColourTransition(GameObject obj, Color clrStart, Color clrEnd, float time)
    {

    }

    public static IEnumerator ColourTransition(GameObject obj, Color clrStart, Color clrEnd, float time)
    {

    }*/
}

public class GameState
{
    public bool isPaused;
    public bool isInEditor;

    public GameState()
    {
        this.isPaused = false;
        this.isInEditor = Application.isEditor;
    }
}
