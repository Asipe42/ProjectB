using System.Linq;
using Sirenix.OdinInspector.Demos.RPGEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;

namespace Modin
{
    public class DialogueSequenceEditor : OdinMenuEditorWindow
    {
        [MenuItem("Tools/Dialogue/Sequence Editor")]
        private static void Open()
        {
            DialogueSequenceEditor window = GetWindow<DialogueSequenceEditor>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
        }
        
        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree tree = new OdinMenuTree(true);
            DialogueSequence[] sequences = AssetDatabase.FindAssets("t:DialogueSequence")
                .Select(guid => AssetDatabase.LoadAssetAtPath<DialogueSequence>(AssetDatabase.GUIDToAssetPath(guid)))
                .ToArray();
            
            tree.Add("Sequences", new DialogueSequenceTable(sequences));
            tree.AddAllAssetsAtPath("Sequences", "Assets/Data/Dialogue", typeof(DialogueSequence), true, true);
            
            return tree;
        }
    }
}