using UnityEngine;

public class FaceNoteMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 forwardForce = new Vector3(0, 150f, 0);

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
        rb.rotation = Quaternion.identity;
        spriteRenderer=GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        noteType = Random.Range(0,arrSprite.Length);
        ChangeTex(arrSprite[noteType]);
    }

    public void ChangeTex(Sprite sprite){
        spriteRenderer.sprite=sprite;
    }


    public void FixedUpdate()
    {   // make note move into player
        //forwardForce += 1;
        rb.AddForce(-forwardForce * Time.deltaTime);
    }

    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }

    public FaceNoteMovement GetNoteMovement()
    {
        return this;
    }
}
