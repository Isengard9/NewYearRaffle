using System;
using UnityEngine;

namespace Managers
{
    public class ManagerContainer : MonoBehaviour
    {
        public static ManagerContainer Instance;


        public MailManager mailManager = new MailManager();
        public RandomManager randomManager = new RandomManager();

        private void Awake()
        {
            if (Instance is not null)
            {
                Destroy(Instance.gameObject);
            }

            Instance = this;

        }
        
    }
}