using UnityEngine;
using UnityEngine.UI;
using TensorFlowLite;
using RhythmTool;

public class StartPlayerController : MonoBehaviour
{
    [SerializeField] private RectTransform ready;
    [SerializeField] private RectTransform faceTemplate;
    public RawImage bg_face { get; set; }
    public RawImage match_zone { set; get; }
    public Text notice { set; get; }
    public RectTransform Ready { get => ready; }
    public RectTransform FaceTemplate { get => faceTemplate; }
    float st = 0f;
    bool detected = false;
    public bool readyIsDone = false;
    bool faceIsInScreen = false;
    [SerializeField] private AnalyzingMusic analyzingMusic;

    void Awake()
    {
        RawImage[] rawImageArr = faceTemplate.gameObject.GetComponentsInChildren<RawImage>();
        bg_face = rawImageArr[0];
        notice = faceTemplate.gameObject.GetComponentInChildren<Text>();
        match_zone = rawImageArr[1];
        match_zone.gameObject.SetActive(false);

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!detected && Time.time - st >= 3f)
        {
            detected = true;
            // Debug.Log("Detected Complete");
            bg_face.gameObject.SetActive(false);
            notice.gameObject.SetActive(false);
            ready.gameObject.SetActive(true);
        }
        else if (detected && !readyIsDone)
        {
            readyIsDone = true;
            ready.gameObject.SetActive(true);
            Invoke(nameof(HasDoneReady), 5f);
        }

    }

    public void Detected()
    {
        bg_face.color = Color.green;
        notice.text = detected ? "" : "Keep your face in 3s";
        if (!faceIsInScreen)
        {
            faceIsInScreen = true;
            if (!detected) st = Time.time;
        }
    }

    public void NotDetected()
    {
        bg_face.color = Color.red;
        notice.text = "Move your face to center";
        faceIsInScreen = false;
    }

    public void HasDoneReady()
    {
        ready.gameObject.SetActive(false);
        match_zone.gameObject.SetActive(true);
        gameObject.GetComponent<FaceGameController>().isGenerate = true;
        analyzingMusic.Play();

    }
}
