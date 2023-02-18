using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class ChangeScenesController : MonoBehaviour
{
    [SerializeField] private GameObject resultCanvas;
    [SerializeField] private GameObject recordCanvas;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private Text scoreText;
    [SerializeField] private FloatSO scoreSO;
    [SerializeField] private RawImage[] star;
    void Awake()
    {
        videoPlayer.playOnAwake = false;
    }

    void Start()
    {
        // display star and score
        star[StarScore(scoreSO.Value)].gameObject.SetActive(true);
        scoreText.text = scoreSO.Value.ToString();
        // video record is have recorded
        string[] videoList = Directory.GetFiles(Directory.GetCurrentDirectory() + "/VideoKit");
        videoPlayer.url = videoList[videoList.Length - 1];
    }

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    public void ViewRecordBtn()
    {
        recordCanvas.SetActive(true);
        videoPlayer.Play();
        // StartCoroutine(Play());
    }

    public void BackBtn()
    {
        videoPlayer.Stop();
        recordCanvas.SetActive(false);
    }

    IEnumerator Play()
    {

        yield return new WaitForSecondsRealtime(1);

        videoPlayer.Play();
    }

    int StarScore(float score){
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
