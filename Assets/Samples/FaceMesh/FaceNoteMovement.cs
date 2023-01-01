using UnityEngine;

public class FaceNoteMovement : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 forwardForce = new Vector3(0, 110f, 0);
    float positionYStart = 13f;
    float positionYEnd = -10f;
    [SerializeField] private FacePlayerMovement player;
    [SerializeField] private NoteState state = NoteState.Free;
    private SpriteRenderer spriteRenderer;
    public Sprite[] arrSprite;
    /* FaceNote type
    0: down
    1: right
    2: mouth open
    3: left
    4: up
    ** note: left and right of player is inveresed
    */
    public int noteType = -1;
    int scoreType = 0;
    private void Awake()
    {
        // get object ridibody
        rb = GetComponent<Rigidbody>();
        rb.rotation = Quaternion.identity;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        noteType = Random.Range(0, arrSprite.Length);
        ChangeTex(arrSprite[noteType]);
        this.state = NoteState.Used;
    }

    public void ChangeTex(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void FixedUpdate()
    {   // make note move into player
        //forwardForce += 1;
        rb.AddForce(-forwardForce * Time.deltaTime);
        if (rb.transform.position.y < positionYEnd)
        {
            this.gameObject.SetActive(false);
            this.state = NoteState.Free;
            player.NextNote();
        }
    }
    /*
    replace and reset spirit for note when it is reactive
    */
    public void RandomOnReset(float posRange, Vector3 generatePos)
    {
        this.gameObject.transform.localPosition =
            new Vector3(
                Random.Range(-posRange, posRange), positionYStart, 1f)
            + generatePos;
        noteType = Random.Range(0, arrSprite.Length);
        ChangeTex(arrSprite[noteType]);
        this.state = NoteState.Used;
    }
    // replace and set spirit when firstly created
    public void Init(float posRange, Vector3 generatePos, FacePlayerMovement player = null)
    {
        this.player = player;
        this.gameObject.transform.localPosition =
            new Vector3(
                Random.Range(-posRange, posRange), positionYStart, 1f)
            + generatePos;
        noteType = Random.Range(0, arrSprite.Length);
        ChangeTex(arrSprite[noteType]);
    }
    public Vector3 GetPostion()
    {
        return gameObject.transform.position;
    }

    void DisableScore()
    {
        player.score[scoreType].gameObject.SetActive(false);
    }

    void DisableScoreAll()
    {
        for (int i = 0; i < player.score.Length; i++)
            player.score[i].gameObject.SetActive(false);

    }

    public void IsDone()
    {   
        if (state == NoteState.Used)
        {
            float posY = gameObject.transform.position.y;
            if (posY >= 6f)
            {
                scoreType = 0;
            }
            else if (posY >= -3f && posY < 5f)
            {
                scoreType = 1;
            }
            else scoreType = 2;
            DisableScoreAll();
            player.score[scoreType].gameObject.SetActive(true);
            Invoke(nameof(DisableScore), 1f);
            gameObject.SetActive(false);

            player.NextNote();
        }
    }
}
enum NoteState
{
    Free,
    Used,
    Done
}