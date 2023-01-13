using UnityEngine;
using UnityEngine.UI;
using RhythmTool;
public class DisplayMusicInfo : MonoBehaviour
{   
    [SerializeField] private RectTransform begin_board;
    public Text _name {set; get;}
    public Text _length {set; get;}

    AudioClip audioClip;
    void Awake(){
        audioClip = gameObject.GetComponent<AnalyzingMusic>().audioClip;
        Text[] musicInfoTexts = begin_board.GetComponentsInChildren<Text>();
        _name = musicInfoTexts[0];
        _length = musicInfoTexts[1];
        _name.text = audioClip.name;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
