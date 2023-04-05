using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class LauncherController: MonoBehaviour
    {
        public List<GameObject> listOfLaunchers = new List<GameObject>();

        private void Start()
        {
            if(listOfLaunchers == null)
                return;

            foreach (var launchers in listOfLaunchers)
            {
                launchers.SetActive(false);
            }
        }

        public void ActivateLaunchers(int levelOfDifficulty)
        {
            for (int i = 0; i < levelOfDifficulty; i++)
            {
                listOfLaunchers[i].SetActive(true);
                listOfLaunchers[i].GetComponent<PotionLauncher>().StartShooting();
            }
        }
    }
}