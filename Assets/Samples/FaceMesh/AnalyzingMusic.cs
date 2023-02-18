using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NatML.VideoKit;
using System.Collections;

namespace RhythmTool
{

    public class AnalyzingMusic : MonoBehaviour
    {
        [SerializeField] private RhythmAnalyzer analyzer;
        [SerializeField] private RhythmPlayer player;
        [SerializeField] private FaceGameController faceGameController;
        [SerializeField] private AudioClip audioClip;
        public AudioClip AudioClip { set { audioClip = value; } get => audioClip; }
        private RhythmData rhythmData;
        private float prevTime;
        private List<Onset> onsets;
        [SerializeField] private VideoKitRecorder recorder;
        private string soundPath;
        private string audioName = "dubstep_evolution.mp3";
        [SerializeField] private DisplayMusicInfo displayMusicInfo;
        [SerializeField] private SongSO songOS;


        [System.Obsolete]
        void Awake()
        {
            onsets = new List<Onset>();
            // soundPath = "file://" + Application.streamingAssetsPath + "/Sound/";
            soundPath = songOS.Path.Replace("\\", "/");
            StartCoroutine(LoadAudio());
        }

        [System.Obsolete]
        IEnumerator LoadAudio()
        {
            WWW request = GetAudioFromFile(soundPath, audioName);
            yield return request;

            this.audioClip = request.GetAudioClip();
            audioClip.name = songOS.NameSong;

            PlayAudioFile();
        }

        private void PlayAudioFile()
        {
            rhythmData = analyzer.Analyze(audioClip, 6);
            player.rhythmData = rhythmData;
            displayMusicInfo.SetInfo(audioClip);
        }

        [System.Obsolete]
        WWW GetAudioFromFile(string path, string filename)
        {
            // string audioToLoad = string.Format(path + "{0}", filename);
            string audioToLoad = path;
            WWW request = new WWW(audioToLoad);
            return request;
        }


        void Start()
        {
            // player.rhythmData.audioClip = audioClip;
            // rhythmData = analyzer.Analyze(audioClip, 6);
            // player.rhythmData = rhythmData;
            // Start analyzing a song
            //Find a track with Beats
            /*
            1. Beats: These represent the rhythm of the song.
            2. Onsets: These represent the start of a note.
            3. Chroma: Chroma features are closely related to pitch and represent the most prominent notes at a certain time in the song.
            4. Segments: These represent sections of the song at which large changes in average volume occur. These changes often indicate different segments of a song.
            5. Volume: These represent the volume of the song at different points in time.
            */
        }

        void Update()
        {
            // Get the current playback time of the AudioSource
            if (audioClip != null && player.audioSource.timeSamples == audioClip.samples)
            {
                // Debug.Log("end");
                if (recorder.status == VideoKitRecorder.Status.Recording)
                    recorder.StopRecording();
                Invoke(nameof(SongA), 1f);

            }

        }
        void PlayerPlay()
        {
            player.Play();
        }
        public void Play()
        {
            player.Stop();
            //Clear the list.
            onsets.Clear();
            //Find all beats for the part of the song that is currently playing.
            player.rhythmData.GetFeatures<Onset>(onsets, 0, audioClip.length);

            // float maxStrength = onsets.Max(x => x.strength);
            // float minStrength = onsets.Min(x => x.strength);
            // float avg = (maxStrength + minStrength) / 2;
            // Debug.Log(onsets.Count + " " + maxStrength + " " + minStrength);
            // Debug.Log("A beat occurred at " + 60 / (beats[0].bpm*Time.deltaTime));
            // if (!faceGameController.isGenerate)
            //     faceGameController.Spawn((60 / beats[0].bpm));

            // Invoke(nameof(PlayerPlay), beats[0].timestamp + 1f);
            // 200ms : 10m-2.72s
            Invoke(nameof(PlayerPlay), 2.72f - onsets[0].timestamp);
            for (int i = 0; i < onsets.Count; i++)
            {
                // Debug.Log(onsets[i].timestamp + "  " + onsets[i].strength + " " + onsets[i].length);
                // if (onsets[0].strength >= avg)
                Invoke(nameof(SpawnOnset), onsets[i].timestamp);
            }

        }
        void SpawnOnset()
        {
            faceGameController.Spawn();
        }

        void SongA()
        {
            SceneManager.LoadScene("ResultScene", LoadSceneMode.Single);
        }
    }
}
