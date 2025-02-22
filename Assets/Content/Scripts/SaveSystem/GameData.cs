using System;
using Content.Scripts.Services;

namespace SaveSystem
{
    [Serializable]
    public class GameData
    {
        public int Version = 5;
        public int Version2 = 4;
        
        public PlayerData PlayerData = new();
    }
}