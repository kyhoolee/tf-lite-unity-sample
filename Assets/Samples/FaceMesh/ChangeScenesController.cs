using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class ChangeScenesController : MonoBehaviour
{
    [SerializeField] private GameObject recordCanvas;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private Text scoreText;
    [SerializeField] private FloatSO scoreSO;
    [SerializeField] private RawImage[] star;   // list spirit star images
    void Awake()
    {
        videoPlayer.playOnAwake = false;
    }

    void Start()
    {
        // 1. display star and score
        star[StarScore(scoreSO.Value)].gameObject.SetActive(true);
        scoreText.text = scoreSO.Value.ToString();

        // 2. video record is have recorded
        string[] videoList = Directory.GetFiles(Directory.GetCurrentDirectory() + "/VideoKit");
        videoPlayer.url = videoList[videoList.Length - 1];  // get lastest record video
    }

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    public void ViewRecordBtn()
    {
        recordCanvas.SetActive(true);
        videoPlayer.Play();
    }

    public void BackBtn()
    {
        videoPlayer.Stop();
        recordCanvas.SetActive(false);
    }

    int StarScore(float score){
        // display star follow score player had after play finish
        if(score <= 100){
            return 0;
        }
        else if(score <= 200){
            return 1;
        }
        else if(score <= 300){
            return 2;
        }
        else if(score <= 400){
            return 3;
        }
        return 4;
    }
}
