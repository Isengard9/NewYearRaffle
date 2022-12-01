using System;
using Managers;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class UIController : MonoBehaviour
    {
        public static UIController Instance;

        private void Awake()
        {
            if (Instance is not null)
            {
                Destroy(Instance.gameObject);
            }

            Instance = this;
        }

        #region Buttons

        [SerializeField] private Button SendMailButton;
        [SerializeField] private Button SelectUserButton;

        #endregion

        public void ShowSelectUser()
        {
            SelectUserButton.gameObject.SetActive(true);
        }
        public void ShowSendMail(int mailersCount)
        {
            SendMailButton.gameObject.SetActive(true);
            SendMailButton.GetComponentInChildren<TMP_Text>().text = $"Send mails to {mailersCount} people";
        }

        public void SendMails()
        {
            ManagerContainer.Instance.randomManager.SendMails();
        }

        public void SelectFile()
        {
            ManagerContainer.Instance.randomManager.SelectFile();
        }
    }
}