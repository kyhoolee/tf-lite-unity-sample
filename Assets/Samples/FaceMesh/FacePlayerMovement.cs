using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FacePlayerMovement : MonoBehaviour
{   
    public FaceGameController gameController;
    public float timeNote = -1.0f;
    // make queue to check note following order
    Queue<FaceNoteMovement> q = new Queue<FaceNoteMovement>();

    public void AddNoteMovement(FaceNoteMovement noteMovement)
    {
        q.Enqueue(noteMovement);
    }

    public FaceNoteMovement GetNoteMovement()
    {
        return (q.Count > 0) ? q.Peek() : null;
    }

    public void NextNote()
    {
        if (q.Count > 0)
        {   
            Destroy(q.Peek().gameObject, 0f);
            q.Dequeue();
        }
    }

    public int GetCount()
    {
        return q.Count();
    }
}
