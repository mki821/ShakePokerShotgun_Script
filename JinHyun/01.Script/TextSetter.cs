using TMPro;
using UnityEngine;

public class TextSetter : MonoBehaviour
{
    public TextMeshProUGUI[] percents;
    public void SetPercentText()
    {
        for (int i = 0; i < percents.Length; i++)
        {
            percents[i].text = Mathf.Floor(GetRandomCard.instance.shapeWeights[i].shapeWeight * 100f) / 100f + "%";
        }
    }
}
