using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Adventure.InteractionSystem
{
    [Serializable]
    public class Dialogue
    {
        public readonly List<string> Lines;
        public readonly Transform Target;

        public Dialogue(Transform target)
        {
            Target = target;
            Lines = new List<string>();
        }

        public Dialogue Add(string line)
        {
            Lines.Add(line);
            return this;
        }

        public Dialogue Add(string speaker, string line)
        {
            Lines.Add(speaker + ": " + line);
            return this;
        }
    }
}