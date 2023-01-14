using UnityEngine;

public class FaceNoteMovement : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 forwardForce = new Vector3(0, 200f, 0);
    float positionYStart = -9f;
    float positionYEnd = 1f;
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
    [SerializeField] private int noteType = -1;
    public int NoteType { get => noteType; }
    static int prevNoteType = -1;
    int scoreType = 0;
    float st = 0;
    private void Awake()
    {
        // get object ridibody
        rb = GetComponent<Rigidbody>();
        rb.rotation = Quaternion.identity;
        rb.velocity = new Vector3(0, 0, 0);
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {   
        // this.state = NoteState.Used;
    }
    public void FixedUpdate()
    {
        // make note falling
        rb.velocity = forwardForce * 1 / 50;
        if (rb.transform.position.y > positionYEnd)
        {
            this.gameObject.SetActive(false);
            this.state = NoteState.Free;
            player.NextNote();
            Debug.Log(Time.time - st);
        }
    }
    int RandomSpiritIndex()
    {
        int number_random = Random.Range(0, arrSprite.Length);
        if (number_random == prevNoteType)
        {
            number_random++;
            number_random = (number_random == arrSprite.Length) ? 0 : number_random;
        }
        prevNoteType = number_random;
        return number_random;
    }

    public void ChangeTex(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
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
        noteType = RandomSpiritIndex();
        ChangeTex(arrSprite[noteType]);
        this.state = NoteState.Used;
        st = Time.time;
    }
    // replace and set spirit when firstly created
    public void Init(FacePlayerMovement player = null)
    {
        this.player = player;
        // this.gameObject.SetActive(false);
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

    public int IsDone()
    {
        if (state == NoteState.Used)
        {
            float posY = gameObject.transform.position.y;
            if (posY > 1f)
            {
                scoreType = 1;
            }
            else if (posY >= -0.5f && posY <= 1f)
            {
                scoreType = 0;
            }
            else scoreType = 2;
            DisableScoreAll();
            player.score[scoreType].gameObject.SetActive(true);
            Invoke(nameof(DisableScore), 1f);
            gameObject.SetActive(false);

            player.NextNote();
            return (scoreType == 1) ? 25 : ((scoreType == 0) ? 50 : 15);
        }
        return 0;
    }
}
enum NoteState
{
    Free,
    Used,
    Done
}