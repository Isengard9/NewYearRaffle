using System;
using UnityEngine;

namespace Managers
{
    public class ManagerContainer : MonoBehaviour
    {
        public static ManagerContainer Instance;


        public MailManager mailManager;
        public RandomManager randomManager;

        private void Awake()
        {
            if (Instance is not null)
            {
                Destroy(Instance.gameObject);
            }

            Instance = this;

            CreateManagers();
        }


        private void CreateManagers()
        {
            mailManager = new MailManager();
            randomManager = new RandomManager();
        }
    }
}