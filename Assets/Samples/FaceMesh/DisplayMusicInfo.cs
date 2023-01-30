using UnityEngine;
using UnityEngine.UI;
using RhythmTool;
using TMPro;

public class DisplayMusicInfo : MonoBehaviour
{   
    [SerializeField] private RectTransform begin_board;
    public TextMeshProUGUI _name {set; get;}
    public TextMeshProUGUI _length {set; get;}

    AudioClip audioClip;
    void Awake(){
        audioClip = gameObject.GetComponent<AnalyzingMusic>().audioClip;
        TextMeshProUGUI[] musicInfoTexts = begin_board.GetComponentsInChildren<TextMeshProUGUI>();
        _name = musicInfoTexts[0];
        _length = musicInfoTexts[1];

    }
    // Start is called before the first frame update
    void Start()
    {
        _name.text = audioClip.name;
        _length.text = SecToTime(audioClip.length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    string SecToTime(float t){
        int m = (int)t / 60;
        int s = (int)t % 60;

        string res = ((m < 10) ? "0" + m.ToString() : m.ToString()) + ":" + ((s < 10) ? "0" + s.ToString() : s.ToString());
        return res;
    }
}
