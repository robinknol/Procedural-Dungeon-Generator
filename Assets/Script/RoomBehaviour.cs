using UnityEngine;

namespace Script
{
    public class RoomBehaviour : MonoBehaviour
    {
        public GameObject[] walls;
        public GameObject[] doors;

        public void UpdateRoom(bool[] status)
        {
            for (var i = 0; i < status.Length; i++)
            {
                doors[i].SetActive(status[i]);
                walls[i].SetActive(!status[i]);
            }
        }
    }
}
