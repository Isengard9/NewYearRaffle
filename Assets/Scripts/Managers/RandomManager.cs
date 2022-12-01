using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Controllers;
using Managers;
using Mono.Cecil.Cil;
using SimpleFileBrowser;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class Match
{
    [SerializeField] public string UserName = "";
    [SerializeField] public string MatchName = "";
}

[System.Serializable]
public class RandomManager
{
    //NC Created

    [SerializeField] private List<string> mailList = new List<string>();
    private List<Match> matches = new List<Match>();

    private int mailSendingIndex = 0;
    
    public void SelectFile()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Users", ".txt"));
        var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        FileBrowser.AddQuickLink("Desktop", desktopPath);

        FileBrowser.ShowLoadDialog((paths) =>
            {
                Debug.Log("Selected: " + paths[0]);
                ReadUsers(paths[0]);
            },
            () => { Debug.Log("Canceled"); },
            FileBrowser.PickMode.Files, false, desktopPath, null, "Select users", "Select");
    }

    private void ReadUsers(string path)
    {
        var users = File.ReadAllText(path);

        mailList = users.Split(Environment.NewLine,
            StringSplitOptions.RemoveEmptyEntries).ToList();

        if (mailList.Count <= 1)
        {
            Debug.LogError("There is not enough people in list");
        }

        MatchUsers();
        mailSendingIndex = 0;
    }

    private void MatchUsers()
    {
        foreach (var user in mailList)
        {
            var randomMatch = FindRandomUser(user);
            matches.Add(new Match
            {
                UserName = user,
                MatchName = randomMatch
            });
        }

        UIController.Instance.ShowSendMail(matches.Count);
    }

    private string FindRandomUser(string userName)
    {
        var randomUser = mailList[Random.Range(0, mailList.Count)];
        if (randomUser == userName)
        {
            randomUser = FindRandomUser(userName);
        }

        if (matches.Exists(x => x.MatchName == randomUser))
        {
            randomUser = FindRandomUser(userName);
        }

        return randomUser;
    }

    public void SendMails()
    {
        var match = matches[mailSendingIndex];

        ManagerContainer.Instance.mailManager.SendMail(match);
        mailSendingIndex++;
    }
}