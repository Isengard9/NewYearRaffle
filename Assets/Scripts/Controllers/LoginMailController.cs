using System;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class LoginMailController : MonoBehaviour
    {
        private string myMailAdress;
        private string myMailPassword;
        private string myMailHost;
        private string myHostPort;
        private string myUserName;

        public void SetMail(string mail)
        {
            myMailAdress = mail;

            if (String.IsNullOrEmpty(myMailAdress))
            {
                Debug.LogError("Mail address cannot be null or empty");
            }
        }

        public void SetMyPassword(string password)
        {
            myMailPassword = password;

            if (string.IsNullOrEmpty(myMailPassword))
            {
                Debug.LogError("Password cannot be null or empty");
            }
        }

        public void SetMailHost(string host)
        {
            myMailHost = host;

            if (string.IsNullOrEmpty(myMailHost))
            {
                Debug.LogError("Mail host cannot be null or empty");
            }
        }

        public void SetMailHostPort(string port)
        {
            myHostPort = port;

            if (string.IsNullOrEmpty(myHostPort))
            {
                Debug.LogError("Mail host port cannot be null or empty");
            }
        }

        public void SetMyUserName(string userName)
        {
            myUserName = userName;

            if (string.IsNullOrEmpty(myUserName))
            {
                Debug.LogError("User name cannot be null or empty");
            }
        }

        public void SaveMyMailSettings()
        {
            if (String.IsNullOrEmpty(myMailAdress))
            {
                Debug.LogError("Mail address cannot be null or empty");
                return;
            }

            if (string.IsNullOrEmpty(myMailPassword))
            {
                Debug.LogError("Password cannot be null or empty");
                return;
            }

            if (string.IsNullOrEmpty(myMailHost))
            {
                Debug.LogError("Mail host cannot be null or empty");
                return;
            }

            if (string.IsNullOrEmpty(myHostPort))
            {
                Debug.LogError("Mail host port cannot be null or empty");
                return;
            }

            if (string.IsNullOrEmpty(myUserName))
            {
                Debug.LogError("User name cannot be null or empty");
            }

            ManagerContainer.Instance.mailManager.LoginMail(myMailAdress, myMailPassword, myUserName, myMailHost,
                Int32.Parse(myHostPort));
            
            UIController.Instance.ShowSelectUser();
        }
    }
}