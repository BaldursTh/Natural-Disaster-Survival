using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(menuName = "Player", fileName = "PlayerData")]
    public class PlayerData : ScriptableObject
    {
        public float moveAcceleration;
        public float moveSpeedCap;
        public float playerGravity;
        public float jumpForce;

    }
}
