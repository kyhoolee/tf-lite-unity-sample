using UnityEngine;
using UnityEngine.UI;

using TensorFlowLite;

public class FaceGameController : MonoBehaviour
{
    float timeSpace = 0.00f;
    float logicTimeRange = 0.5f;
    [SerializeField] private FacePlayerMovement player;
    public FacePlayerMovement Player { get => player; }
    public Text scoreText;

    [SerializeField] private FaceMeshProcessor faceMeshProcess;
    [SerializeField] private GameObject topLeft;
    [SerializeField] private GameObject bottomRight;
    [SerializeField] private RectTransform ready;
    public bool isGenerate = false;
    [SerializeField] private FaceGenerate faceGenerate;
    float preH = -20;
    float preW = -20;

    void DisableText()
    {
        scoreText.gameObject.SetActive(false);
    }

    void ChangeScore(int score)
    {
        scoreText.gameObject.SetActive(true);
        // Update score

        Invoke(nameof(DisableText), 0.5f);
    }

    void ReadyDone()
    {
        ready.gameObject.SetActive(false);
        isGenerate = true;
    }

    void Start()
    {
        //1. count 1 -> 3 to ready 
        ready.gameObject.SetActive(true);
        Invoke(nameof(ReadyDone), 5.0f);
    }

    private void FixedUpdate()
    {
        faceGenerate.gameObject.SetActive(isGenerate);

        // get press space time
        if (Time.time > timeSpace) // Input.GetMouseButtonDown(0) && 
        {
            timeSpace = Time.time + logicTimeRange;
            FaceNoteMovement lastNode = player.GetNoteMovement();
            FaceMesh.Result faceMesh = faceMeshProcess.getFaceMeshResult();
            if (faceMesh != null)
            {
                if (faceMesh.score < 0)
                {
                    scoreText.text = "No-face";
                }
                else if (lastNode != null)
                {
                    faceNoteMatch(lastNode, faceMesh);
                }
                else
                {
                    //scoreText.text = "Next-note";
                }
            }
            else
            {
                scoreText.text = "No-face";
            }
        }

    }

    string BoolToString(bool b)
    {
        return (b) ? "T" : "F";
    }

    private bool faceNoteMatch(FaceNoteMovement note, FaceMesh.Result face)
    {
        bool result = false;

        Vector2 angles = faceAngle(face);
        Vector2 mouth = faceMouth(face);

        bool isDown = (angles[1] < 175 && angles[1] > 0);
        bool isUp = (angles[1] > -160 && angles[1] < 0);
        bool isLeft = (angles[0] > -160 && angles[0] < 0);
        bool isRight = (angles[0] < 175 && angles[0] > 0);

        bool isStraight = Mathf.Abs(angles[0]) > 175 && Mathf.Abs(angles[1]) > 175;

        scoreText.text = mouth[0] + " | " + mouth[1];
        /*
        0: down         - T F F F F
        1: right        - F F T F F 
        2: mouth open   - 
        3: left          - F F F T F
        4: up           - F T F F F
        ** note: left and right of player is inveresed
        */
        // Debug.Log("D U L R S: " + isDown + " " + isUp + " " + isLeft + " " + isRight + " " + isStraight);
        string faceState = BoolToString(isDown) + BoolToString(isUp) + BoolToString(isLeft) + BoolToString(isRight) + BoolToString(isStraight);
        // Debug.Log(faceState);
        int faceNoteType = -1;
        switch (faceState)
        {
            case "TFFFF":
                // down
                Debug.Log("down");
                faceNoteType = 0;
                break;
            case "FFTFF":
                // left
                Debug.Log("left");
                faceNoteType = 3;
                break;
            case "FFFFT":
                // straight
                // Debug.Log("straight");
                // mouth
                if (mouth[0] > 2 && mouth[1] < 1)
                {
                    Debug.Log("open mouth");
                    faceNoteType = 2;
                }
                break;
            case "FFFTF":
                // right
                Debug.Log("right");
                faceNoteType = 1;
                break;
            case "FTFFF":
                // up
                Debug.Log("up");
                faceNoteType = 4;
                break;
            default:
                // not defined
                break;
        }

        if (note.noteType == faceNoteType)
        {
            // note.IsDone();
        }

        return result;
    }


    private Vector2 faceAngle(FaceMesh.Result face)
    {


        Vector3 center = face.keypoints[6];
        Vector3 left = face.keypoints[226];
        Vector3 right = face.keypoints[446];
        Vector3 up = face.keypoints[151];
        Vector3 down = face.keypoints[164];

        Vector3 leftRight = right - left;
        Vector3 downUp = up - down;

        float xzAngle = Vector2.SignedAngle(new Vector2(leftRight[0], leftRight[2]), new Vector2(1, 0));
        float yzAngle = Vector2.SignedAngle(new Vector2(downUp[1], downUp[2]), new Vector2(1, 0));


        scoreText.text = "" + xzAngle + " || " + yzAngle;

        return new Vector2(xzAngle, yzAngle);
    }


    private Vector2 faceMouth(FaceMesh.Result face)
    {
        Vector3 left = face.keypoints[292];
        Vector3 right = face.keypoints[62];
        Vector3 up = face.keypoints[13];
        Vector3 down = face.keypoints[14];
        //calculating size of mouth opening
        float height = -(up[1] - down[1]);
        float width = -(left[0] - right[0]);
        //ratio of length after and before
        float x = 0;
        float y = 0;

        if (preH != -20)    x = (height / preH);
        if (preW != -20)    y = (width / preW);

        preH = height;
        preW = width;

        return new Vector2(x, y);
    }




}