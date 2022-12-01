using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mail;
using UnityEngine;

namespace Managers
{
    [System.Serializable]
    public class MailManager
    {
        private SmtpClient smtpClient;
        private MailAddress myMailAdress;

        [Multiline]
        [SerializeField] public string MessageBody;
        [Multiline]
        [SerializeField] public string MessageSubject;
        
        public void LoginMail(string myMail, string myPassword, string myUserName, string smtpMailHost, int port)
        {
            try
            {
                smtpClient = new SmtpClient(smtpMailHost, port);
                smtpClient.Credentials = new System.Net.NetworkCredential(
                    myMail,
                    myPassword);
                smtpClient.EnableSsl = true;

                myMailAdress = new MailAddress(
                    myMail,
                    myUserName,
                    System.Text.Encoding.UTF8);
                
            }
            catch (Exception e)
            {
                Debug.LogError($"Mail connection error message:\n{e.Message}");
            }
        }


        public void SendMail(Match match)
        {   
            var userMail = match.UserName.Split(",")[0];
            var matchMail = match.MatchName.Split(",")[0];
            var matchName = match.MatchName.Split(",")[1];

            MessageBody += $"\nYour match is: {matchName} and his/her mail {matchMail}";
            try
            {
                var to = new MailAddress(userMail);
                var message = new MailMessage(myMailAdress, to);
                message.Body = MessageBody;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.Subject = MessageSubject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                
                smtpClient.SendCompleted += SendCompletedCallback;
                smtpClient.Send(message);
            }

            catch (Exception ep)
            {
                Debug.LogError($"Sending to -> {userMail}  mail error message\n{ep.Message}");
            }
        }

        private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            var token = (string)e.UserState;

            if (e.Cancelled)
            {
                Debug.LogError("Send canceled " + token);
            }

            if (e.Error != null)
            {
                Debug.LogError("[ " + token + " ] " + " " + e.Error.ToString());
            }
            else
            {
                Debug.Log("Message sent.");
                
                ManagerContainer.Instance.randomManager.SendMails();
            }
        }
    }
}