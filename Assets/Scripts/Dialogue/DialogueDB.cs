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
        
        public bool TryResolveSnapshot(DialogueSnapshot snapshot,
            out DialogueChapter chapter,
            out DialogueSequence sequence,
            out DialogueLine line)
        {
            chapter = null;
            sequence = null;
            line = null;

            if (!TryGetChapter(snapshot.ChapterID, out chapter))
                return false;

            if (chapter.TryGetSequence(snapshot.SequenceID, out sequence))
                return false;

            if (sequence.TryGetLine(snapshot.LineID, out line))
                return false;

            return true;
        }
    }
}