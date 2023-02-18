using UnityEngine;
using System.Collections.Generic;

public class FaceGenerate : MonoBehaviour
{
    [SerializeField] private FaceGameController gameController;
    [SerializeField] private GameObject faceNotePrefab;
    private FaceNoteMovement note;
    int index_note = 0;
    [SerializeField] private GameObject topLeft;
    [SerializeField] private GameObject bottomRight;
    private Vector3 tL;
    private Vector3 bR;
    private float posRange;
    private Vector3 generatePos;
    public int number_notes = 15;
    private List<GameObject> pooledNotes;
    void Awake()
    {
        // 1. Get game-region size 
        tL = topLeft.transform.position;
        bR = bottomRight.transform.position;

        posRange = (bR.x - tL.x) / 2;
        generatePos = new Vector3(tL.x + posRange, bR.y, 0);
        // create pool notes
        pooledNotes = new List<GameObject>();
        for (int i = 0; i < number_notes; i++)
        {
            pooledNotes.Add(CreateNote());
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public GameObject Spawn()
    {
        for (int i = 0; i < pooledNotes.Count; i++)
        {
            if (!pooledNotes[i].activeSelf)
            {
                // 1. when reactive, rerandom position and spirit
                pooledNotes[i].GetComponent<FaceNoteMovement>().RandomOnReset(posRange, generatePos);
                // 2. add facenot to list
                gameController.Player.AddNoteMovement(pooledNotes[i].GetComponent<FaceNoteMovement>());
                pooledNotes[i].SetActive(true);
                return pooledNotes[i];
            }
        }
        return null;
    }

    GameObject CreateNote()
    {
        // 2. Generate note 
        var go = Instantiate(faceNotePrefab);
        note = go.GetComponent<FaceNoteMovement>();
        note.gameObject.name = (index_note++ + 1).ToString();
        note.Init(gameController.Player);
        go.SetActive(false);
        return go;
    }
}