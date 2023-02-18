using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SongController : MonoBehaviour
{
    private Song song;
    [SerializeField] private Text number;
    [SerializeField] private TextMeshProUGUI songName;
    [SerializeField] private TextMeshProUGUI author;
    [SerializeField] private TextMeshProUGUI time;
    private Button btn;
    [SerializeField] private SongSO songOS;
    private bool canPlay = true;
    void Awake()
    {
        song = new Song();
    }
    void Start()
    {
        btn = gameObject.GetComponentInChildren<Button>();
        btn.interactable = canPlay;
        // click button to show charge power notice
        btn.onClick.AddListener(ChangeScene);
    }
    /*
        Display each song info of list
    */
    public void Init(string num, string path, bool ok)
    {   // declare Song
        KeyValuePair<string, string> nameSongAndAuthor = GetInfo(path);
        song.Path = path;
        song.SongName = nameSongAndAuthor.Key;
        // display text
        number.text = num;
        songName.text = song.SongName;
        author.text = nameSongAndAuthor.Value;
        // check player is has star enough to play or not
        canPlay = ok;
    }

    KeyValuePair<string, string> GetInfo(string path)
    {
        string[] pathName = path.Split('.');
        // split song name and author
        string[] songInfo = pathName[pathName.Length - 2].Split('\\')[1].Split('-');

        string songName = parseName(songInfo[0]);
        string author = (songInfo.Length > 1 ? songInfo[1] : "Ms-Records");
        return new KeyValuePair<string, string>(songName, author);
    }

    string parseName(string oldName)
    {
        string newName = "";
        string[] names = oldName.Split('_');
        // upcase first character
        foreach (string name in names)
        {
            newName += char.ToUpper(name[0]) + name.Substring(1) + " ";
        }
        return newName;
    }
    void ChangeScene()
    {
        songOS.SetSong(songName.text, author.text, song.Time, song.Path);
        GameObject.Find("ChooseSongController").GetComponent<ListMusicsController>().ChargeNotice.gameObject.SetActive(true);
    }
}
/*
    Song to contain filepath of song, name, and time
*/
class Song
{
    private string path;
    private string songName;
    private float time;
    public string Path { set { path = value; } get => path; }
    public string SongName { set { songName = value; } get => songName; }
    public float Time { set { time = value; } get => time; }
}