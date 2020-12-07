using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RandomDungeonWithBluePrint
{
    public class Field
    {
        public int MaxRoomNum;
        public Vector2Int Size;
        public List<Section> Sections;
        public List<Connection> Connections;
        public List<Vector2Int> Branches;

        public Grid Grid { get; } = new Grid();
        public List<Room> Rooms => Sections.Where(s => s.ExistRoom).Select(s => s.Room).ToList();
        public bool RoomIsFull => MaxRoomNum <= Rooms.Count;
        public bool ExistRoomToBeMake => !Sections.Where(s => !s.ExistRoom && s.MakeRoomWeight > 0).All(s => s.ExistRoom);

        public void BuildGrid()
        {
            Grid.Build(Size, Rooms, Branches);
        }

        public Section GetSection(int index)
        {
            return Sections?.FirstOrDefault(s => s.Index == index);
        }

        // sectionがどこからも繋がってないかどうか
        public bool IsIsolatedSection(Section section)
        {
            return !Connections.Any(c => c.ConnectedAny(section.Index));
        }

        // どこからも繋がっていなくて、周りに繋がってるSectionがある物を返す
        public IEnumerable<Section> IsolatedAndExistConnectedSectionAroundSections()
        {
            return Sections.Where(s => IsIsolatedSection(s) && ExistConnectedSectionAround(s));
        }

        // Sectionsの中から、sectionに隣接している物を返す
        public IEnumerable<Section> GetSectionsAdjoinWith(Section section)
        {
            return Sections.Where(s => section != s && section.AdjoinWith(s));
        }

        // sectionに隣接していて、どこからか繋がっているSectionを返す
        public IEnumerable<Section> GetSectionsAdjoinWithRoute(Section section)
        {
            return GetSectionsAdjoinWith(section).Where(s => Connections.Any(c => c.ConnectedAny(s.Index)));
        }

        // sectionの周囲にどこからか繋がっているSectionが存在しているかどうか
        public bool ExistConnectedSectionAround(Section section)
        {
            return GetSectionsAdjoinWith(section).Any(s => !IsIsolatedSection(s));
        }

        public bool Connected(Section a, Section b)
        {
            return Connections.Any(c => c.Connected(a.Index, b.Index));
        }
    }
}