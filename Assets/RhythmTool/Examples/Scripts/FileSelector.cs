using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RhythmTool.Examples
{
    /// <summary>
    /// The FileSelector shows a browser and imports a file, before analyzing and playing it.
    /// </summary>
    public class FileSelector : SongSelector
    {
        public Browser browser;

        public AudioImporter importer;

        void Awake()
        {
            browser.FileSelected += OnFileSelected;
            importer.Loaded += OnLoaded;
        }
        
        private void OnFileSelected(string path)
        {
            //Hide the browser.
            browser.gameObject.SetActive(false);

            //Start importing the selected song.
            importer.Import(path);
        }

        private void OnLoaded(AudioClip audioClip)
        {
            //Clean up old resources.
            Destroy(player.audioClip);
            Destroy(player.rhythmData);

            //Start analyzing the song.
            RhythmData rhythmData = analyzer.Analyze(audioClip);

            //Give the RhythmData to the RhythmPlayer.
            player.rhythmData = rhythmData;
        }

        public override void NextSong()
        {
            base.NextSong();

            //Show the browser.
            browser.gameObject.SetActive(true);
        }
    }
}