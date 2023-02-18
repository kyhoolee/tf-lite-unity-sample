using UnityEngine;
using UnityEngine.UI;
using RhythmTool;


public class StartPlayerController : MonoBehaviour
{
    [SerializeField] private RectTransform ready;
    [SerializeField] private RectTransform faceTemplate;
    public RawImage bg_face { get; set; }
    public RectTransform match_zone { set; get; }
    public Text notice { set; get; }
    public RectTransform Ready { get => ready; }
    public RectTransform FaceTemplate { get => faceTemplate; }
    private float st = 0f;
    private bool detected = false;              // has detected in 3s
    public bool readyIsDone = false;            // count is done
    private bool faceIsInScreen = false;        // player is keep face in center
    [SerializeField] private AnalyzingMusic analyzingMusic;
    [SerializeField] private RectTransform recordNotice;
    private bool isChooseRecord = false;

    void Awake()
    {
        RawImage[] rawImageArr = faceTemplate.gameObject.GetComponentsInChildren<RawImage>();
        bg_face = rawImageArr[0];
        notice = faceTemplate.gameObject.GetComponentInChildren<Text>();
        RectTransform[] x = faceTemplate.GetComponentsInChildren<RectTransform>();
        for (int i = 0; i < x.Length; i++)
        {
            if (x[i].name == "match zone")
            {
                match_zone = x[i];
                break;
            }
        }
        recordNotice.gameObject.SetActive(true);
        match_zone.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {   
        //1. recording or not
        //2. keep your face in 3s
        //3. count to start
        if (isChooseRecord)
        {
            // Debug.Log("detected: " + detected);
            if (detected && !readyIsDone)
            {
                readyIsDone = true;
                ready.gameObject.SetActive(true);
                Invoke(nameof(HasDoneReady), 5f);
            }
            else if (faceIsInScreen && !readyIsDone && (Time.time - st >= 3f))
            {
                detected = true;
                // Debug.Log("Detected Complete");
                bg_face.gameObject.SetActive(false);
                notice.gameObject.SetActive(false);
                // ready.gameObject.SetActive(true);
            }
        }


    }

    public void Detected()
    {
        bg_face.color = Color.green;
        // notice.text = detected ? "" : "Keep your face in 3s";
        if (!faceIsInScreen)
        {
            faceIsInScreen = true;
            if (!detected) st = Time.time;
        }
    }

    public void NotDetected()
    {
        bg_face.color = Color.red;
        // notice.text = "Move your face to center";
        faceIsInScreen = false;
    }

    public void HasDoneReady()
    {
        ready.gameObject.SetActive(false);
        match_zone.gameObject.SetActive(true);
        gameObject.GetComponent<FaceGameController>().isGenerate = true;
        analyzingMusic.Play();

    }

    public void RecordBtn()
    {
        recordNotice.gameObject.SetActive(false);
        isChooseRecord = true;
        // record method in button onclick inspector
    }
}
