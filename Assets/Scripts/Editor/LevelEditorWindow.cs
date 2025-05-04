using GameElements;
using Level;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class LevelEditorWindow : OdinEditorWindow
    {
        private const int BoardSize = 8;
        private Vector2Int _selectedPosition = Vector2Int.zero;
        private ChessType _selectedType = ChessType.Pawn;
        private ColorSide _selectedSide = ColorSide.White;
        private bool _isEnemy = false;
        
        
        [Title("Редактор доски")]
        [GUIColor(0.9f, 0.95f, 1f)]
        [ShowInInspector]
        private void DrawBoard()
        {
            if (levelData == null)
            {
                SirenixEditorGUI.ErrorMessageBox("Нет активного LevelData.");
                return;
            }

            GUILayout.Label("Выбор фигуры:", EditorStyles.boldLabel);
            _selectedType = (ChessType)EditorGUILayout.EnumPopup("Тип фигуры", _selectedType);
            _selectedSide = (ColorSide)EditorGUILayout.EnumPopup("Сторона", _selectedSide);
            _isEnemy = EditorGUILayout.Toggle("Это враг?", _isEnemy);

            GUILayout.Space(10);
            GUILayout.Label("Кликни по клетке, чтобы разместить фигуру:", EditorStyles.boldLabel);

            for (int y = BoardSize - 1; y >= 0; y--)
            {
                GUILayout.BeginHorizontal();
                for (int x = 0; x < BoardSize; x++)
                {
                    Vector2Int pos = new Vector2Int(x, y);
                    string label = GetLabelForCell(pos);
                    if (GUILayout.Button(label, GUILayout.Width(50), GUILayout.Height(50)))
                    {
                        PlaceFigureAt(pos);
                    }
                }
                GUILayout.EndHorizontal();
            }
        }
        
        
        [MenuItem("RogueChess/Level Editor")]
        private static void OpenWindow()
        {
            GetWindow<LevelEditorWindow>().Show();
        }

        [Title("Настройки уровня")]
        [InlineEditor(InlineEditorModes.GUIOnly)]
        [HideLabel]
        [SerializeField] private LevelData levelData;

        [Button("Создать новый уровень")]
        private void CreateNewLevel()
        {
            levelData = ScriptableObject.CreateInstance<LevelData>();
            levelData.levelId = "Level_" + Random.Range(1, 1000);
            levelData.displayName = "Новый уровень";
        }

        [Button("Сохранить уровень в Resources/Levels")]
        private void SaveLevel()
        {
            if (levelData == null)
            {
                Debug.LogWarning("Нет данных для сохранения.");
                return;
            }

            string path = $"Assets/Resources/Levels/{levelData.levelId}.asset";
            AssetDatabase.CreateAsset(levelData, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"Сохранён: {path}");
        }

        [Button("Очистить все фигуры")]
        private void ClearFigures()
        {
            if (levelData == null) return;
            levelData.playerFigures.Clear();
            levelData.enemyFigures.Clear();
        }
        
        private string GetLabelForCell(Vector2Int pos)
        {
            foreach (var fig in levelData.playerFigures)
                if (fig.position == pos)
                    return $"W\n{fig.type.ToString()[0]}";

            foreach (var fig in levelData.enemyFigures)
                if (fig.position == pos)
                    return $"B\n{fig.type.ToString()[0]}";

            return "";
        }

        private void PlaceFigureAt(Vector2Int pos)
        {
            ChessPlacement placement = new ChessPlacement { type = _selectedType, position = pos };

            if (_isEnemy)
            {
                levelData.enemyFigures.RemoveAll(p => p.position == pos);
                levelData.enemyFigures.Add(placement);
            }
            else
            {
                levelData.playerFigures.RemoveAll(p => p.position == pos);
                levelData.playerFigures.Add(placement);
            }
        }
    }
}