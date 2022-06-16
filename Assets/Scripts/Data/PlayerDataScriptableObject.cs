using UnityEngine;

namespace Data
{
    [CreateAssetMenu(order = 25, fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
    public class PlayerDataScriptableObject : ScriptableObject
    {
        public PlayerData PlayerData;
    }
}