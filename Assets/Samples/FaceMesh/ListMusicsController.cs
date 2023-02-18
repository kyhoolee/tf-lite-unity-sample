using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ListMusicsController : MonoBehaviour
{
    [SerializeField] private GameObject song;
    [SerializeField] private Transform panel;
    private RectTransform rect;
    [SerializeField] private Transform disc;
    int index = 0;
    float startXPosFirstItem = 360f;
    float startYPosFirstItem = 40f;
    float lenghtList = 1280f;
    float itemHeight = 200f;
    private string[] drives;
    private List<string> listMusic;
    [SerializeField] private SongSO songSO;
    [SerializeField] private PowerSO powerSO;
    [SerializeField] private FloatSO starSO;
    [SerializeField] private TextMeshProUGUI numberCharge;
    [SerializeField] private Transform chargeNotice;
    public Transform ChargeNotice { get => chargeNotice; }
    [SerializeField] private TextMeshProUGUI starHome;
    void Awake()
    {
        listMusic = new List<string>();
        rect = panel.GetComponent<RectTransform>();
        drives = Directory.GetFiles(Directory.GetCurrentDirectory().Replace('\\', '/') + "/Assets/raw_music/raw");
        listMusic = selectedMp3File(drives);

        lenghtList = (listMusic.Count * itemHeight + 1280 - 300) > 1280 ? (listMusic.Count * itemHeight + 1280 - 300) : lenghtList;
        startYPosFirstItem = lenghtList / 2 - 600;

        chargeNotice.gameObject.SetActive(false);
    }

    void Start()
    {
        numberCharge.text = powerSO.Value.ToString();
        starHome.text = starSO.Value.ToString();
        disc.localPosition = new Vector3(0, startYPosFirstItem, 0);

        for (int i = 0; i < listMusic.Count; i++)
        {
            index += 1;
            GameObject newSong = GameObject.Instantiate(song);
            newSong.name = "song" + index.ToString();
            newSong.GetComponent<SongController>().Init(index.ToString(), listMusic[i], (starSO.Value >= ((index-1)* 5)));
            newSong.transform.position = new Vector3(startXPosFirstItem, startYPosFirstItem + 170 - i * itemHeight, 0); // 150 = distance between disc and frist song list
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
            powerSO.Value -= 1;
            SceneManager.LoadScene("FaceMeshGame", LoadSceneMode.Single);
        }
        else
        {
            chargeNotice.gameObject.SetActive(false);
        }
    }
}
