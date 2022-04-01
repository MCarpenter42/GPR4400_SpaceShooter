using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class CoreFunc : MonoBehaviour
{
    public static GameState gameState = new GameState();
    public static Controls controls = new Controls();
    public static Keynames keynames = new Keynames();

    public static Color[] clrList = new Color[]
    {
        // RAINBOW COLOURS
        new Color(1.000f, 0.000f, 0.000f, 1.000f),
        new Color(1.000f, 0.600f, 0.000f, 1.000f),
        new Color(1.000f, 1.000f, 0.000f, 1.000f),
        new Color(0.000f, 1.000f, 0.145f, 1.000f),
        new Color(0.000f, 0.816f, 1.000f, 1.000f),
        new Color(0.557f, 0.000f, 1.000f, 1.000f),

        // GENERAL
        new Color(1.000f, 1.000f, 1.000f, 1.000f),
        new Color(0.000f, 0.000f, 0.000f, 1.000f),
        new Color(0.027f, 0.027f, 0.027f, 1.000f),

        // OTHER

    };
    public enum colours
    {
        // RAINBOW COLOURS
        rainbowRed,
        rainbowOrange,
        rainbowYellow,
        rainbowGreen,
        rainbowBlue,
        rainbowPurple,

        // GENERAL
        pureWhite,
        pureBlack,
        nearBlack,

        // OTHER

    };

    public void Pause()
    {
        Time.timeScale = 0.0f;
        gameState.isPaused = true;
    }
    
    public void Resume()
    {
        Time.timeScale = 1.0f;
        gameState.isPaused = false;
    }

    public void GoToScene(int index)
    {
        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }

    public void GoToScene(char dir)
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if (dir == '+')
        {
            index += 1;
        }
        else if (dir == '-')
        {
            index -= 1;
        }
        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }

    public void Exit()
    {
        if (gameState.isInEditor)
        {
            Debug.Log("Exiting to edit mode...");
            EditorApplication.ExitPlaymode();
        }
        else
        {
            Application.Quit();
        }
    }

    public static float ToRad(float degrees)
    {
        return degrees * Mathf.PI / 180.0f;
    }

    public static float ToDeg(float radians)
    {
        return radians * 180.0f / Mathf.PI;
    }

    public static List<GameObject> GetChildrenWithComponent<T>(GameObject parentObj)
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

    public static List<GameObject> GetChildrenWithTag(GameObject parentObj, string tag)
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

    public Coroutine DoColourTransition(Image img, Color clrStart, Color clrEnd, float time, bool realTime)
    {
        return StartCoroutine(ColourTransition(img, clrStart, clrEnd, time, realTime));
    }

    public IEnumerator ColourTransition(Image img, Color clrStart, Color clrEnd, float time, bool realTime)
    {
        int aFrames = (int)(100.0f * time);
        float aFrameTime = time / (float)aFrames;

        for (int i = 1; i <= aFrames; i++)
        {
            float delta = (float)i / (float)aFrames;
            if (realTime)
            {
                yield return new WaitForSecondsRealtime(aFrameTime);
            }
            else
            {
                yield return new WaitForSeconds(aFrameTime);
            }
            img.color = Color.Lerp(clrStart, clrEnd, delta);
        }
    }

    public Coroutine DoColourFlash(Image img, Color clrFlash, float time, bool vanishAfter, bool realTime)
    {
        Color clrStart = img.color;
        return StartCoroutine(ColourFlash(img, clrStart, clrFlash, time, vanishAfter, realTime));
    }

    public IEnumerator ColourFlash(Image img, Color clrStart, Color clrFlash, float time, bool vanishAfter, bool realTime)
    {
        int aFrames = (int)(100.0f * time);
        float aFrameTime = time / (float)aFrames;
        aFrames /= 2;

        for (int i = 1; i <= aFrames; i++)
        {
            float delta = (float)i / (float)aFrames;
            if (realTime)
            {
                yield return new WaitForSecondsRealtime(aFrameTime);
            }
            else
            {
                yield return new WaitForSeconds(aFrameTime);
            }
            img.color = Color.Lerp(clrStart, clrFlash, delta);
        }

        for (int i = 1; i <= aFrames; i++)
        {
            float delta = (float)i / (float)aFrames;
            if (realTime)
            {
                yield return new WaitForSecondsRealtime(aFrameTime);
            }
            else
            {
                yield return new WaitForSeconds(aFrameTime);
            }
            img.color = Color.Lerp(clrFlash, clrStart, delta);
        }

        if (vanishAfter)
        {
            img.gameObject.SetActive(false);
        }
    }

    public static Color GetColor(int clrIndex)
    {
        return clrList[clrIndex];
    }

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

public class Keynames
{
    public Dictionary<string, string> names = new Dictionary<string, string>
    {
        // LETTERS
        { "a", "A" },
        { "b", "B" },
        { "c", "C" },
        { "d", "D" },
        { "e", "E" },
        { "f", "F" },
        { "g", "G" },
        { "h", "H" },
        { "i", "I" },
        { "j", "J" },
        { "k", "K" },
        { "l", "L" },
        { "m", "M" },
        { "n", "N" },
        { "o", "O" },
        { "p", "P" },
        { "q", "Q" },
        { "r", "R" },
        { "s", "S" },
        { "t", "T" },
        { "u", "U" },
        { "v", "V" },
        { "w", "W" },
        { "x", "X" },
        { "y", "Y" },
        { "z", "Z" },

        // NUMBERS
        { "0", "0" },
        { "1", "1" },
        { "2", "2" },
        { "3", "3" },
        { "4", "4" },
        { "5", "5" },
        { "6", "6" },
        { "7", "7" },
        { "8", "8" },
        { "9", "9" },

        // SYMBOLS
        { "-", "-" },
        { "=", "=" },
        { "!", "!" },
        { "@", "@" },
        { "#", "#" },
        { "$", "$" },
        { "£", "£" },
        { "€", "€" },
        { "%", "%" },
        { "^", "^" },
        { "&", "&" },
        { "*", "*" },
        { "(", "(" },
        { ")", ")" },
        { "_", "_" },
        { "+", "+" },
        { "[", "[" },
        { "]", "]" },
        { "`", "`" },
        { "{", "{" },
        { "}", "}" },
        { "~", "~" },
        { ";", ";" },
        { "'", "'" },
        { "\\", "\\" },
        { ":", ":" },
        { "\"", "\"" },
        { "|", "|" },
        { ",", "," },
        { ".", "." },
        { "/", "/" },
        { "<", "<" },
        { ">", ">" },
        { "?", "?" },
        { "equals", "=" },
        { "euro", "€" },

        // NUMPAD
        { "[0]", "NUM 0" },
        { "[1]", "NUM 1" },
        { "[2]", "NUM 2" },
        { "[3]", "NUM 3" },
        { "[4]", "NUM 4" },
        { "[5]", "NUM 5" },
        { "[6]", "NUM 6" },
        { "[7]", "NUM 7" },
        { "[8]", "NUM 8" },
        { "[9]", "NUM 9" },
        { "[.]", "NUM POINT" },
        { "[/]", "NUM SLASH" },
        { "[*]", "NUM MULTIPLY" },
        { "[-]", "NUM MINUS" },
        { "[+]", "NUM PLUS" },

        // ARROW KEYS
        { "up", "UP ARROW" },
        { "down", "DOWN ARROW" },
        { "left", "LEFT ARROW" },
        { "right", "RIGHT ARROW" },

        // TEXT FUNCTIONS
        { "backspace", "BACKSPACE" },
        { "delete", "DEL" },
        { "tab", "TAB" },
        { "clear", "CLR" },
        { "return", "RTN" },
        { "space", "SPACEBAR" },
        { "enter", "ENTER" },
        { "insert", "INSERT" },

        // MODIFIER
        { "left shift", "L SHIFT" },
        { "right shift", "R SHIFT" },
        { "left ctrl", "L CTRL" },
        { "right ctrl", "R CTRL" },
        { "left alt", "L ALT" },
        { "right alt", "R ALT" },
        { "left cmd", "L CMD" },
        { "right cmd", "R CMD" },
        { "left super", "L SUPER" },
        { "right super", "R SUPER" },
        { "alt gr", "ALT GR" },

        // CONTROL
        { "pause", "PAUSE" },
        { "escape", "ESC" },
        { "home", "HOME" },
        { "end", "END" },
        { "page up", "PG UP" },
        { "page down", "PG DOWN" },
        { "compose", "COMPOSE" },
        { "help", "HELP" },
        { "print screen", "PRNT SCRN" },
        { "sys req", "SYS REQ" },
        { "break", "BREAK" },
        { "menu", "MENU" },
        { "power", "POWER" },
        { "undo", "UNDO" },

        // LOCK
        { "numlock", "NUM LOCK" },
        { "caps lock", "CAPS LOCK" },
        { "scroll lock", "SCROLL LOCK" },

        // FUNCTION
        { "f1", "F1" },
        { "f2", "F2" },
        { "f3", "F3" },
        { "f4", "F4" },
        { "f5", "F5" },
        { "f6", "F6" },
        { "f7", "F7" },
        { "f8", "F8" },
        { "f9", "F9" },
        { "f10", "F10" },
        { "f11", "F11" },
        { "f12", "F12" },
        { "f13", "F13" },
        { "f14", "F14" },
        { "f15", "F15" },

        // MOUSE
        { "mouse 0", "LMB" },
        { "mouse 1", "RMB" },
        { "mouse 2", "MMB" },
        { "mouse 3", "MSE 4" },
        { "mouse 4", "MSE 5" },
        { "mouse 5", "MSE 6" },
        { "mouse 6", "MSE 7" },
    };
}
