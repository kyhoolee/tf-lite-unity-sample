using System.Collections;
using UnityEngine;

public class FaceGenerate : MonoBehaviour
{
    public FaceGameController gameController;
    public GameObject faceNotePrefab;
    private FaceNoteMovement note;
    int i = 0;

    [SerializeField] private GameObject topLeft;
    [SerializeField] private GameObject bottomRight;

    float timeStartGenerate = 3f;
    float spawnTimeRange = 3f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateNote());
    }

    void stopGenerate() {
        CancelInvoke();
    }


    // Update is called once per frame
    void Update()
    {   
        
    }

    IEnumerator  CreateNote()
    {
        while(true){
            // 1. Get game-region size 
        Vector3 tL = topLeft.transform.position;
        Vector3 bR = bottomRight.transform.position;

        float posRange = (bR.x - tL.x)/2;
        Vector3 generatePos = new Vector3(tL.x + posRange, tL.y, 0);

        // 2. Generate note 
        var go = Instantiate(faceNotePrefab);
        Debug.Log(""+go.name, go);
        note=go.GetComponent<FaceNoteMovement>();
        note.gameObject.name = (i + 1).ToString();
        note.gameObject.transform.localPosition = 
            new Vector3(
                Random.Range(-posRange, posRange), 0f, 1f) 
            + generatePos;
        gameController.player.AddNoteMovement(note);
        i++;

        Debug.Log("Generate new note:: " + i + " :: " + note);
        yield return new WaitForSeconds(spawnTimeRange);
        }
    }

    void DestroyNote(GameObject note)
    {
        Destroy(note.gameObject);
    }
}
