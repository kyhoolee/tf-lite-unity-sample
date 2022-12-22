using UnityEngine;

public class FaceNoteMovement : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 forwardForce = new Vector3(0, 110f, 0);
    float positionYStart = 10f;
    float positionYEnd = -5f;

    public FacePlayerMovement player;
    private SpriteRenderer spriteRenderer;
    public Sprite[] arrSprite;

    /* FaceNote type
    0: down
    1: left
    2: mouth open
    3: right
    4: up
    */
    public int noteType = -1;

    public int GetNoteType() => noteType;

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
    }

    public Vector3 GetPostion()
    {
        return gameObject.transform.position;
    }
}
