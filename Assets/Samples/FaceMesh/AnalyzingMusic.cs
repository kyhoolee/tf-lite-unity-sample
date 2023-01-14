using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace RhythmTool
{

    public class AnalyzingMusic : MonoBehaviour
    {
        public RhythmAnalyzer analyzer;
        public RhythmPlayer player;
        private RhythmEventProvider eventProvider;
        [SerializeField] private FaceGameController faceGameController;
        public AudioClip audioClip;
        private RhythmData rhythmData;
        private float prevTime;
        private List<Onset> onsets;
        private List<Beat> beats;
        private List<Value> values;
        private List<Chroma> chromas;

        void Awake()
        {
            onsets = new List<Onset>();
            beats = new List<Beat>();
            // eventProvider = (RhythmEventProvider)player.targets[0];
        }

        void Start()
        {
            // player.rhythmData.audioClip = audioClip;
            rhythmData = analyzer.Analyze(audioClip, 6);
            player.rhythmData = rhythmData;
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

        }
        void PlayerPlay()
        {
            player.Play();
        }
        public void Play()
        {
            player.Stop();
            //Clear the list.
            beats.Clear();
            onsets.Clear();
            //Find all beats for the part of the song that is currently playing.
            player.rhythmData.GetFeatures<Beat>(beats, 0, audioClip.length);
            player.rhythmData.GetFeatures<Onset>(onsets, 0, audioClip.length);
            // player.rhythmData.GetFeatures<Value>(values, 0, audioClip.length);
            // player.rhythmData.GetFeatures<Chroma>(chromas, 0, audioClip.length);


            // float maxStrength = onsets.Max(x => x.strength);
            // float minStrength = onsets.Min(x => x.strength);
            // float avg = (maxStrength + minStrength) / 2;
            // Debug.Log(onsets.Count + " " + maxStrength + " " + minStrength);
            // Debug.Log("A beat occurred at " + 60 / (beats[0].bpm*Time.deltaTime));
            // if (!faceGameController.isGenerate)
            //     faceGameController.Spawn((60 / beats[0].bpm));

            // Invoke(nameof(PlayerPlay), beats[0].timestamp + 1f);
            // 200ms : 10m-2.72s
            Invoke(nameof(PlayerPlay), 2.72f -  onsets[0].timestamp);
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
    }
}
