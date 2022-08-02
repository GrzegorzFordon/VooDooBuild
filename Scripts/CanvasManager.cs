using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;
    public TextMeshProUGUI textScore;
    private Dictionary<string, TextMeshProUGUI> tmproDict = new Dictionary<string, TextMeshProUGUI>();

    private void Awake()
    {
        instance = this;
        tmproDict.Add("Score", textScore);
        TrySetText("Score", "0000");
    }

    public void TrySetText(string key, string value)
    {
        if (tmproDict.TryGetValue(key, out TextMeshProUGUI text))
        {
            text.text = value;
        }
    }
}
