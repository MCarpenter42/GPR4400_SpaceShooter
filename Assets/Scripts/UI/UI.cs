using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : CoreFunc
{
    #region [ PARAMETERS ]

    public enum colourCycleTypes
    {
        rainbowCycle,
        twoColourCycle,
        quickPulseCycle,
    };

    #endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    public Coroutine DoColourCycle(Image img, int cycleType, bool realTime, int clrIndex1, int clrIndex2)
    {
        if (cycleType == 0)
        {
            return StartCoroutine(RainbowCycle(img, 2.0f, realTime));
        }
        if (cycleType == 1)
        {
            return StartCoroutine(TwoColourCycle(img, 1.5f, realTime, clrList[clrIndex1], clrList[clrIndex2]));
        }
        if (cycleType == 2)
        {
            return StartCoroutine(PulseCycle(img, 0.3f, realTime, clrList[clrIndex1], clrList[clrIndex2]));
        }
        else
        {
            return null;
        }
    }

    public IEnumerator RainbowCycle(Image img, float segmentTime, bool realTime)
    {
        while (true)
        {
            StartCoroutine(ColourTransition(img, clrList[(int)colours.rainbowRed], clrList[(int)colours.rainbowOrange], segmentTime, realTime));
            if (realTime)
            {
                yield return new WaitForSecondsRealtime(segmentTime);
            }
            else
            {
                yield return new WaitForSeconds(segmentTime);
            }

            StartCoroutine(ColourTransition(img, clrList[(int)colours.rainbowOrange], clrList[(int)colours.rainbowYellow], segmentTime, realTime));
            if (realTime)
            {
                yield return new WaitForSecondsRealtime(segmentTime);
            }
            else
            {
                yield return new WaitForSeconds(segmentTime);
            }

            StartCoroutine(ColourTransition(img, clrList[(int)colours.rainbowYellow], clrList[(int)colours.rainbowGreen], segmentTime, realTime));
            if (realTime)
            {
                yield return new WaitForSecondsRealtime(segmentTime);
            }
            else
            {
                yield return new WaitForSeconds(segmentTime);
            }

            StartCoroutine(ColourTransition(img, clrList[(int)colours.rainbowGreen], clrList[(int)colours.rainbowBlue], segmentTime, realTime));
            if (realTime)
            {
                yield return new WaitForSecondsRealtime(segmentTime);
            }
            else
            {
                yield return new WaitForSeconds(segmentTime);
            }

            StartCoroutine(ColourTransition(img, clrList[(int)colours.rainbowBlue], clrList[(int)colours.rainbowPurple], segmentTime, realTime));
            if (realTime)
            {
                yield return new WaitForSecondsRealtime(segmentTime);
            }
            else
            {
                yield return new WaitForSeconds(segmentTime);
            }

            StartCoroutine(ColourTransition(img, clrList[(int)colours.rainbowPurple], clrList[(int)colours.rainbowRed], segmentTime, realTime));
            if (realTime)
            {
                yield return new WaitForSecondsRealtime(segmentTime);
            }
            else
            {
                yield return new WaitForSeconds(segmentTime);
            }
        }
    }

    public IEnumerator TwoColourCycle(Image img, float segmentTime, bool realTime, Color clr1, Color clr2)
    {
        while (true)
        {
            StartCoroutine(ColourTransition(img, clr1, clr2, segmentTime * 0.8f, realTime));
            if (realTime)
            {
                yield return new WaitForSecondsRealtime(segmentTime);
            }
            else
            {
                yield return new WaitForSeconds(segmentTime);
            }
            
            StartCoroutine(ColourTransition(img, clr2, clr1, segmentTime * 0.8f, realTime));
            if (realTime)
            {
                yield return new WaitForSecondsRealtime(segmentTime);
            }
            else
            {
                yield return new WaitForSeconds(segmentTime);
            }
        }
    }
    
    public IEnumerator PulseCycle(Image img, float segmentTime, bool realTime, Color clr1, Color clr2)
    {
        while (true)
        {
            StartCoroutine(ColourTransition(img, clr1, clr2, segmentTime, realTime));
            if (realTime)
            {
                yield return new WaitForSecondsRealtime(segmentTime);
            }
            else
            {
                yield return new WaitForSeconds(segmentTime);
            }
            
            StartCoroutine(ColourTransition(img, clr2, clr1, segmentTime, realTime));
            if (realTime)
            {
                yield return new WaitForSecondsRealtime(segmentTime * 3.0f);
            }
            else
            {
                yield return new WaitForSeconds(segmentTime * 3.0f);
            }
        }
    }

}
