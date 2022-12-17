using UnityEngine;

namespace GenifyStudio.Scripts.Tools.Log
{
    public class FPS : MonoBehaviour
    {
        public enum DisplayFPS { YES, NO }

        private float      deltaTime  = 0.0f;
        public  Color      textColor  = new Color(1f, 0.0f, 0f, 1.0f);
        public  DisplayFPS displayFPS = DisplayFPS.YES;

        private float sumFPS         = 0;
        private int   nSample        = 0;
        private bool  isRecordingFPS = false;

        private void Awake()
        {
            if (Application.targetFrameRate < 60)
            {
                Application.targetFrameRate = 60;
            }
#if UNITY_EDITOR
            Application.targetFrameRate = 300;
#endif
        }

        private void Update()
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
            if (isRecordingFPS)
            {
                sumFPS += GetFPS();
                nSample++;
            }
        }

        #region Record FPS
        public void StartRecordFPS()
        {
            sumFPS         = 0;
            nSample        = 0;
            isRecordingFPS = true;
        }

        public void StopRecordFPS()
        {
            isRecordingFPS = false;
        }

        public void ResumeRecordFPS()
        {
            isRecordingFPS = true;
        }

        public int GetAverageFPS()
        {
            return (int)(sumFPS / nSample);
        }
        #endregion

        private void OnGUI()
        {
            if (displayFPS == DisplayFPS.YES)
            {
                int w = Screen.width, h = Screen.height;

                GUIStyle style = new GUIStyle();

                Rect rect = new Rect(0, 0, w, h * 2 / 100);
                style.alignment        = TextAnchor.UpperLeft;
                style.fontSize         = h * 2 / 100;
                style.normal.textColor = textColor;
                float msec = deltaTime * 1000.0f;
                float fps = GetFPS();
                string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
                GUI.Label(rect, text, style);
            }
        }

        public float GetFPS()
        {
            if (deltaTime < 1 / 300f)
            {
                return 60;
            }
            return 1.0f / deltaTime;
        }
    }
}