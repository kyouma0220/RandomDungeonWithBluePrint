using UnityEngine;

namespace RandomDungeonWithBluePrint
{
    public class Relay
    {
        public int Section { get; set; }
        public Vector2Int Point { get; set; }

        public Relay(int section, Vector2Int point)
        {
            Section = section;
            Point = point;
        }
    }
}
