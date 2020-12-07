using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RandomDungeonWithBluePrint
{
    public class Grid
    {
        public Vector2Int Size => new Vector2Int(grid.First().Count, grid.Count);
        private readonly List<List<int>> grid = new List<List<int>>();
        public int this[int x, int y] => grid[y][x];

        public void Build(Vector2Int size, List<Room> rooms, List<Vector2Int> branches)
        {
            MakeGrid(size.x, size.y);
            for (var i = 0; i < size.y; i++)
            {
                for (var j = 0; j < size.x; j++)
                {
                    grid[i][j] = (int) Constants.MapChipType.Wall;
                }
            }

            foreach (var room in rooms)
            {
                foreach (var pos in room.Rect.allPositionsWithin)
                {
                    grid[pos.y][pos.x] = (int) Constants.MapChipType.Floor;
                }
            }

            foreach (var branch in branches.Distinct())
            {
                grid[branch.y][branch.x] = (int) Constants.MapChipType.Floor;
            }
        }

        private void MakeGrid(int x, int y)
        {
            for (var i = 0; i < y; i++)
            {
                grid.Add(new List<int>());
                for (var j = 0; j < x; j++)
                {
                    grid.Last().Add(0);
                }
            }
        }
    }
}
