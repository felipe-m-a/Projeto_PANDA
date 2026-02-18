using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Adventure.InteractionSystem
{
    public struct Dialogue
    {
        public readonly Transform Target;
        public readonly List<string> Lines;

        public Dialogue(Transform target)
        {
            Target = target;
            Lines = new List<string>();
        }

        public void AddLine(string text, string name = null)
        {
            if (name != null)
                text = name + ":\n" + text;
            Lines.Add(text);
        }
    }
}