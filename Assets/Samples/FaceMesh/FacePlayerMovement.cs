using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FacePlayerMovement : MonoBehaviour
{
    [SerializeField] private FaceGameController gameController;
    public RectTransform[] score;
    // make queue to check note following order
    private List<FaceNoteMovement> q = new List<FaceNoteMovement>();
    
    public void AddNoteMovement(FaceNoteMovement noteMovement)
    {
        // Debug.Log(noteMovement.gameObject.name + " enqueue");
        q.Add(noteMovement);
    }

    public FaceNoteMovement GetNoteMovement()
    {
        return (q.Count() > 0) ? q.First() : null;
    }

    public void NextNote()
    {
        if (q.Count() > 0)
        {
            // Debug.Log(q.Dequeue().gameObject.name + " dequeue");
            q.RemoveAt(0);
        }
    }

    public int GetCount()
    {
        return q.Count();
    }
    
    public FaceNoteMovement CheckFaceNoteMatch(int noteType){
        FaceNoteMovement res = null;
        for(int i = 0; i < q.Count(); i++){
            if(q[i].NoteType == noteType){
                res = q[i];
                q.RemoveAt(i);
                break;
            }
        }
        return res;
    }
}