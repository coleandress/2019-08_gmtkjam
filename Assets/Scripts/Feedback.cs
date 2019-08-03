using TMPro;
using UnityEngine;

public class Feedback : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = "";
    }

    public void UpdateText(string t)
    {
        _text.text = t;
    }
}
