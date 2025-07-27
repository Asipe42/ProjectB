using System.Collections.Generic;
using System.IO;
using System.Text;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Modin
{
    [CreateAssetMenu(fileName = "SoundConfig", menuName = "Modin/Sound/SoundConfig")]
    public class SoundConfig : SerializedScriptableObject
    {
        [SerializeField] [Sirenix.OdinInspector.FilePath] 
        private string codePath;
        
        public Dictionary<string, AudioClip> clipMap;

#if UNITY_EDITOR
        [Button]
        private void GenerateKey()
        {
            if (clipMap == null || clipMap.Count == 0)
            {
                Debug.LogWarning("SoundConfig: clipMap이 비어 있습니다.");
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("namespace Modin");
            sb.AppendLine("{");
            sb.AppendLine("    public static class SoundKey");
            sb.AppendLine("    {");

            foreach (var key in clipMap.Keys)
            {
                sb.AppendLine($"        public const string {key} = \"{key}\";");
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            File.WriteAllText(codePath, sb.ToString(), Encoding.UTF8);
            AssetDatabase.Refresh();
        }
#endif
    }
}