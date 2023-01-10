using UnityEngine;
using System.Collections.Generic;
using System.Timers;

public class FaceGenerate : MonoBehaviour
{
    [SerializeField] private FaceGameController gameController;
    [SerializeField] private GameObject faceNotePrefab;
    private FaceNoteMovement note;
    int index_name = 0;
    int _i = -1;
    [SerializeField] private GameObject topLeft;
    [SerializeField] private GameObject bottomRight;
    Vector3 tL;
    Vector3 bR;
    float posRange;
    Vector3 generatePos;
    public int number_notes = 10;
    [SerializeField] List<GameObject> pooledNotes;

    public float timeStartGenerate = 0f;
    public float spawnTimeRange = 0.5f;

    void Awake()
    {
        // 1. Get game-region size 
        tL = topLeft.transform.position;
        bR = bottomRight.transform.position;

        posRange = (bR.x - tL.x) / 2;
        generatePos = new Vector3(tL.x + posRange, tL.y, 0);
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

    // Update is called once per frame
    void Update()
    {

    }


    void FixedUpdate()
    {
        // after spawnTimeRange second, we get note in pool to spawn
        timeStartGenerate += Time.deltaTime;
        if (timeStartGenerate >= spawnTimeRange)
        {
            timeStartGenerate = 0;
            Spawn();
        }
    }

    GameObject Spawn()
    {
        _i++;
        // if (_i == pooledNotes.Count)
        // {
        //     _i = 0;
            
        // }
        for (int i = 0; i < pooledNotes.Count; i++)
        {
            if (!pooledNotes[i].activeSelf)
            {
                // Debug.Log("Spawn " + id);
                // 1. when reactive, rerandom position and spirit
                pooledNotes[i].GetComponent<FaceNoteMovement>().RandomOnReset(posRange, generatePos);
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
        note.gameObject.name = (index_name++ + 1).ToString();
        note.Init(gameController.Player);
        // go.SetActive(false);
        return go;
    }
}