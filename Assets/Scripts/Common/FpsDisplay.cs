using UnityEngine;

namespace Common{
    public class FpsDisplay : MonoBehaviour{
        public Font font;
        public bool showVersion = true;
        private float _deltaTime;

        private void Awake(){
            Application.targetFrameRate = 60;
        }

        private void Update(){
            _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
        }

        private void OnGUI(){
            int w = Screen.width, h = Screen.height;

            var style = new GUIStyle();

            var rect = new Rect(80, 5, w, h * 50);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 2 / 100;
            style.normal.textColor = new Color(1f, 1f, 0.49f);
            style.font = font;
            var msec = _deltaTime * 1000.0f;
            var fps = 1.0f / _deltaTime;
            var textVersion = $"  v.{Application.version}";
            var text = $"{fps:0.} FPS ";
            GUI.Label(rect, showVersion ? text + textVersion : text, style);
        }
    }
}