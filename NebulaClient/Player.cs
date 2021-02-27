﻿using NebulaClient.MonoBehaviours;
using UnityEngine;

namespace NebulaClient
{
    public class Player
    {
        public ushort PlayerId { get; protected set; }
        public Transform PlayerTransform { get; set; }
        public Transform PlayerModelTransform { get; set; }
        public RemotePlayerAnimator Animator {get; set;}

        public Player(ushort playerId)
        {
            PlayerId = playerId;
        }
    }
}
