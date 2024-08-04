using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPS_Counter : MonoBehaviour
{

    public TMP_Text fpsText;

    void Update()
    {
        fpsText.text = (1f / Time.unscaledDeltaTime).ToString("F0") + " FPS";
    }
}
