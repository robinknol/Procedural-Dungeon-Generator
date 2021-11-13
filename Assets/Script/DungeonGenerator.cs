using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script
{
    public class DungeonGenerator : MonoBehaviour
    {
        [SerializeField] private int startPos;
        [SerializeField] private Vector2 size;
        [SerializeField] private Vector2 offset;
        [SerializeField] private GameObject room;

        private List<Cell> _board;
        
        private class Cell
        {
            public bool Visited;
            public readonly bool[] Status = new bool[4];
        }

        private void Start()
        {
            MazeGenerator();
            GenerateDungeon();
        }

        private void GenerateDungeon()
        {
            for (var i = 0; i < size.x; i++)
            {
                for (var j = 0; j < size.y; j++)
                {
                    var newRoom = Instantiate(room, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                    newRoom.UpdateRoom(_board[Mathf.FloorToInt(i+j*size.x)].Status);

                    newRoom.name = " " + i + "-" + j;
                }
            }
        }
        
        private void MazeGenerator()
        {
            _board = new List<Cell>();

            for (var i = 0; i < size.x; i++)
            {
                for (var j = 0; j < size.y; j++)
                {
                    _board.Add(new Cell());
                }
            }

            var currentCell = startPos;
            var path = new Stack<int>();

            while (true)
            {
                _board[currentCell].Visited = true;

                var neighbours = CheckNeighbours(currentCell);

                if (neighbours.Count == 0)
                {
                    if (path.Count == 0)
                    {
                        break;
                    }
                    
                    currentCell = path.Pop();
                    continue;
                }
                
                path.Push(currentCell);

                var newCell = neighbours[Random.Range(0, neighbours.Count)];

                if (newCell > currentCell)
                {
                    if (newCell - 1 == currentCell)
                    {
                        _board[currentCell].Status[2] = true;
                        currentCell = newCell;
                        _board[currentCell].Status[3] = true;
                        continue;
                    }
                    
                    _board[currentCell].Status[1] = true;
                    currentCell = newCell;
                    _board[currentCell].Status[0] = true;
                    
                    continue;
                }
                
                if (newCell + 1 == currentCell)
                {
                    _board[currentCell].Status[3] = true;
                    currentCell = newCell;
                    _board[currentCell].Status[2] = true;
                    
                    continue;
                }
                
                _board[currentCell].Status[0] = true;
                currentCell = newCell;
                _board[currentCell].Status[1] = true;
            }
            GenerateDungeon();
        }

        private List<int> CheckNeighbours(int cell)
        {
            var neighbours = new List<int>();

            if (cell - size.x >= 0 && !_board[Mathf.FloorToInt(cell - size.x)].Visited)
                neighbours.Add(Mathf.FloorToInt(cell - size.x));
            
            if (cell + size.x < _board.Count && !_board[Mathf.FloorToInt(cell + size.x)].Visited) 
                neighbours.Add(Mathf.FloorToInt(cell + size.x));

            if ((cell + 1) % size.x != 0 && !_board[Mathf.FloorToInt(cell + 1)].Visited)
                neighbours.Add(Mathf.FloorToInt(cell + 1));

            if (cell % size.x != 0 && !_board[Mathf.FloorToInt(cell - 1)].Visited)
                neighbours.Add(Mathf.FloorToInt(cell - 1));

            return neighbours;
        }
    }
}