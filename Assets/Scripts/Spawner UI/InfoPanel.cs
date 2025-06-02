using TMPro;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] private string _titleAreaName;
    [SerializeField] private TMP_Text _titleArea;
    [SerializeField] private TMP_Text _textArea;

    private string defaultText = "0";

    private void Start()
    {
        _titleArea.text = _titleAreaName;
        _textArea.text = defaultText;
    }

    public void RefreshCount(int count)
    {
        _textArea.text = $"{count}";
    }
}
