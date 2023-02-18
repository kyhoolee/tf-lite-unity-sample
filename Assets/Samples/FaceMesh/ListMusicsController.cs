using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ListMusicsController : MonoBehaviour
{
    [SerializeField] private GameObject song;
    [SerializeField] private Transform panel; // container panel
    private RectTransform rect;     // container 
    [SerializeField] private Transform disc;
    private int index = 0;
    private float startXPosFirstItem = 360f;
    private float startYPosFirstItem = 40f;
    private float lenghtList = 1280f;
    private float itemHeight = 200f;
    private string[] drives;
    private List<string> listMusic;
    [SerializeField] private SongSO songSO;
    [SerializeField] private PowerSO powerSO;
    [SerializeField] private FloatSO starSO;
    [SerializeField] private TextMeshProUGUI numberCharge;
    [SerializeField] private Transform chargeNotice;
    public Transform ChargeNotice { get => chargeNotice; }
    [SerializeField] private TextMeshProUGUI starHome;
    [SerializeField] private string musicPath = "/Assets/raw_music/raw";
    void Awake()
    {
        listMusic = new List<string>();
        rect = panel.GetComponent<RectTransform>();
        //1. get all file in folder
        drives = Directory.GetFiles(Directory.GetCurrentDirectory().Replace('\\', '/') + musicPath);
        //2. parse music has extension is mp3
        listMusic = selectedMp3File(drives);
        //3. resize of panel contain list
        lenghtList = (listMusic.Count * itemHeight + 1280 - 300) > 1280 ? (listMusic.Count * itemHeight + 1280 - 300) : lenghtList;
        startYPosFirstItem = lenghtList / 2 - 600;

        chargeNotice.gameObject.SetActive(false);
    }

    void Start()
    {
        numberCharge.text = powerSO.Value.ToString();
        starHome.text = starSO.Value.ToString();
        //1. set position of disc
        disc.localPosition = new Vector3(0, startYPosFirstItem, 0);
        //2. display song in list
        for (int i = 0; i < listMusic.Count; i++)
        {
            index += 1;
            GameObject newSong = GameObject.Instantiate(song);
            newSong.name = "song" + index.ToString();
            // set name and path
            newSong.GetComponent<SongController>().Init(index.ToString(), listMusic[i], (starSO.Value >= ((index-1)* 5)));
            newSong.transform.position = new Vector3(startXPosFirstItem, startYPosFirstItem + 170 - i * itemHeight, 0); // 150 = distance between disc and frist song list
            // contained by container panel
            newSong.transform.SetParent(panel.transform);
        }
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, lenghtList);
        rect.localPosition = new Vector3(0, -2000, 0);
    }

    List<string> selectedMp3File(string[] input)
    {
        List<string> res = new List<string>();
        for (int i = 0; i < input.Length; i++)
        {
            // check file extension is mp3 or not
            string[] paths = input[i].Split('.');
            if (paths[paths.Length - 1] == "mp3")
                res.Add(input[i]);
        }
        return res;
    }

    public void AcceptChargePower(bool ok)
    {   
        if (ok && powerSO.CanDecreament())
        {   
            // -1 to play game
            powerSO.Value -= 1;
            SceneManager.LoadScene("FaceMeshGame", LoadSceneMode.Single);
        }
        else
        {
            chargeNotice.gameObject.SetActive(false);
        }
    }
}
