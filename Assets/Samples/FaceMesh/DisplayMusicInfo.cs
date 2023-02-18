using UnityEngine;
using TMPro;

public class DisplayMusicInfo : MonoBehaviour
{
    [SerializeField] private RectTransform begin_board;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _length;
    void Awake()
    {
        TextMeshProUGUI[] musicInfoTexts = begin_board.GetComponentsInChildren<TextMeshProUGUI>();
        _name = musicInfoTexts[0];
        _length = musicInfoTexts[1];

    }

    public void SetInfo(AudioClip audioClip)
    {   
        // display name, time of song
        _name.text = audioClip.name;
        _length.text = SecToTime(audioClip.length);
    }

    string SecToTime(float t)
    {
        int m = (int)t / 60;
        int s = (int)t % 60;

        string res = ((m < 10) ? "0" + m.ToString() : m.ToString()) + ":" + ((s < 10) ? "0" + s.ToString() : s.ToString());
        return res;
    }
}
