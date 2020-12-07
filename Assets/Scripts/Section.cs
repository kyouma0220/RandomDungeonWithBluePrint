using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RandomDungeonWithBluePrint
{
    public class Section
    {
        public int Index;
        public RectInt Rect;
        public Room Room;
        public Relay Relay;
        public Vector2Int MinRoomSize;
        public int MakeRoomWeight;
        public bool RoomIndispensable;

        public int Width => Rect.width;
        public int Height => Rect.height;
        public bool ExistRoom => Room != null;
        
        public Section()
        {
        }

        public Section(FieldBluePrint.Section bluePrint)
        {
            Index = bluePrint.Index;
            Rect = bluePrint.Rect;
            MakeRoomWeight = bluePrint.MakeRoomWeight;
            RoomIndispensable = bluePrint.RoomIndispensable;
            MinRoomSize = bluePrint.MinRoomSize;
        }

        public int AdjoiningWithDirection(Section other)
        {
            return Rect.AdjoiningWithDirection(other.Rect);
        }

        public bool AdjoinWith(Section other)
        {
            return AdjoiningWithDirection(other) != Constants.Direction.Error;
        }

        public Vector2Int GetEdge(Section other, Vector2Int initial = default)
        {
            return Rect.GetEdge(AdjoiningWithDirection(other), initial);
        }

        public IEnumerable<Joint> GetUnConnectedJoints(int direction)
        {
            return Room.GetUnconnectedJoints(direction);
        }

        public IEnumerable<Joint> GetConnectedJoints(int direction)
        {
            return Room.GetConnectedJoints(direction);
        }

        public bool ExistUnconnectedJoints(int direction)
        {
            return !ExistRoom || GetUnConnectedJoints(direction).Any();
        }
    }
}