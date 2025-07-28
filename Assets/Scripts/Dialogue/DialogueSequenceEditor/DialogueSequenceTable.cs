using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

namespace Modin
{
    public class DialogueSequenceTable
    {
        [TableList(IsReadOnly = true, AlwaysExpanded = true), ShowInInspector]
        private readonly List<DialogueSequenceWarapper> sequences;

        public DialogueSequence this[int index]
        {
            get { return this.sequences[index].Sequence; }
        }

        public DialogueSequenceTable(IEnumerable<DialogueSequence> sequences)
        {
            this.sequences = sequences.Select(x => new DialogueSequenceWarapper(x)).ToList();
        }

        private class DialogueSequenceWarapper
        {
            private DialogueSequence sequence; 

            public DialogueSequence Sequence
            {
                get { return this.sequence; }
            }

            public DialogueSequenceWarapper(DialogueSequence sequence)
            {
                this.sequence = sequence;
            }
        }
    }
}