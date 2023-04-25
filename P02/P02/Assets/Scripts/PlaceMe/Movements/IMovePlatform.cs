using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace BrashCreations.PlaceMe.Scripts.Movements
{
    public interface IMovePlatform
    {
        [Tooltip("Initial start after been added")]
        public void StartMoving(List<Transform> tripPositions, LoopType loopType, float startingDelay, float restTime, float speed, float stoppingDistance);
        [Tooltip("Stop moving the platform")]
        public void StopMoving();
        [Tooltip("Restart moving platform from where it stopped")]
        public void RestartMoving();
    }
}