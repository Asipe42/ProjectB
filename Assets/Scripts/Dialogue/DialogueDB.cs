using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modin
{
    [CreateAssetMenu(fileName = "DialogueDB", menuName = "Modin/Dialogue/DB")]
    public class DialogueDB : SerializedScriptableObject
    {
        public List<DialogueChapter> Chapters;

        public bool TryGetChapter(string id, out DialogueChapter result)
        {
            result = Chapters.Find(x => x.ID == id);
            return result != null;
        }

        public bool TryGetNextChapter(DialogueChapter currentChapter, out DialogueChapter result)
        {
            int index = Chapters.FindIndex(x => x.ID == x.NextID);
            if (index >= 0 && index < Chapters.Count - 1)
            {
                result = Chapters[index + 1];
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

            if (!TryGetChapter(snapshot.ChapterID, out chapter))
            {
                Debug.LogError($"{nameof(DialogueChapter)}가 유효하지 않습니다. ({snapshot.ChapterID})");
                return false;
            }

            if (!chapter.TryGetSequence(snapshot.SequenceID, out sequence))
            {
                Debug.LogError($"{nameof(DialogueSequence)}가 유효하지 않습니다. ({snapshot.SequenceID})");
                return false;
            }

            if (!sequence.TryGetLine(snapshot.LineID, out line))
            {
                Debug.LogError($"{nameof(DialogueLine)}가 유효하지 않습니다. ({snapshot.LineID})");
                return false;
            }

            return true;
        }
    }
}