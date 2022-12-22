using UnityEngine;
using UnityEngine.UI;

using TensorFlowLite;

public class FaceGameController : MonoBehaviour
{
    float timeSpace = 0.00f;
    float logicTimeRange = 0.5f;
    public FacePlayerMovement player;
    public Text scoreText;

    [SerializeField] private FaceMeshProcessor faceMeshProcess;
    [SerializeField] private GameObject topLeft;
    [SerializeField] private GameObject bottomRight;
    [SerializeField] private RectTransform ready;
    public bool isGenerate = false;
    [SerializeField] private FaceGenerate faceGenerate;

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

    void ReadyDone(){
        ready.gameObject.SetActive(false);
        isGenerate = true;
    }

    void Start(){
        Invoke(nameof(ReadyDone), 3.0f);
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
                if (lastNode != null)
                {
                    faceNoteMatch(lastNode, faceMesh); 
                } else 
                {
                    //scoreText.text = "Next-note";
                }
            } else 
            {
                scoreText.text = "No-face";
            }
        }

    }


    private bool faceNoteMatch(FaceNoteMovement note, FaceMesh.Result face) {
        bool result = false;

        Vector2 angles = faceAngle(face);

        bool isDown = (angles[1] < 170 && angles[1] > 0);
        bool isUp = (angles[1] > -160 && angles[1] < 0);
        bool isLeft = (angles[0] > -160 && angles[0] < 0);
        bool isRight = (angles[0] < 160 && angles[0] > 0);

        bool isStraight = Mathf.Abs(angles[0]) > 170 && Mathf.Abs(angles[1]) > 170;



        switch(note.noteType) 
        {
        case 0:
            // down
            break;
        case 1:
            // left
            break;
        case 2:
            // mouth
            break;
        case 3:
            // right
            break;
        case 4:
            // up
            break;
        default:
            // not defined
            break;
        }

        return result;
    }


    private Vector2 faceAngle(FaceMesh.Result face) {
        

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


    private Vector2 faceMouth(FaceMesh.Result face) {
        

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

    


}

