using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Modin
{
    [CreateAssetMenu(fileName = "DialogueDB", menuName = "Modin/Dialogue/DB")]
    public class DialogueDB : SerializedScriptableObject
    {
        public List<DialogueChapter> chapters;

        public bool TryGetChapter(string id, out DialogueChapter result)
        {
            result = chapters.Find(x => x.id == id);
            return result != null;
        }

        public bool TryGetNextChapter(DialogueChapter currentChapter, out DialogueChapter result)
        {
            int index = chapters.FindIndex(x => x.id == x.nextID);
            if (index >= 0 && index < chapters.Count - 1)
            {
                result = chapters[index + 1];
                return true;
            }

            result = null;
            return false;
        }
        
        public bool TryResolveSnapshot
        (
            DialogueSnapshot snapshot,
            out DialogueChapter chapter,
            out DialogueSequence sequence,
            out DialogueLine line)
        {
            chapter = null;
            sequence = null;
            line = null;

            if (!TryGetChapter(snapshot.chapterID, out chapter))
            {
                Debug.LogError($"{nameof(DialogueChapter)}가 유효하지 않습니다. ({snapshot.chapterID})");
                return false;
            }

            if (!chapter.TryGetSequence(snapshot.sequenceID, out sequence))
            {
                Debug.LogError($"{nameof(DialogueSequence)}가 유효하지 않습니다. ({snapshot.sequenceID})");
                return false;
            }

            if (!sequence.TryGetLine(snapshot.lineID, out line))
            {
                Debug.LogError($"{nameof(DialogueLine)}가 유효하지 않습니다. ({snapshot.lineID})");
                return false;
            }

            return true;
        }
    }
}