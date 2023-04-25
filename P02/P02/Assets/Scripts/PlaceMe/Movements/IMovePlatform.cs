using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace BrashCreations.PlaceMe.Scripts.Movements
{
    public interface IMovePlatform
    {
        public void StartMoving(List<Transform> tripPositions, LoopType loopType, float startingDelay, float restTime, float speed, float stoppingDistance);
        public void StopMoving();
        public void RestartMoving();
    }
}