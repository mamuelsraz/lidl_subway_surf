using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class Backend : MonoBehaviour
{
    int depth = 10;
    public Text[] score_displays;

    private void Start()
    {
        depth = score_displays.Length;
        Login();
    }

    public void show_stats(int score)
    {
        Send_leaderboard(score);
    }

    public void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, Success, Fail);
    }
    void Success(LoginResult result)
    {
        Debug.Log("fuck yeah");
    }
    void Fail(PlayFabError result)
    {
        Debug.LogError(result);
    }
    public void Send_leaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate { StatisticName = "score", Value = score }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, Leaderboard_success, Fail);
    }
    void Leaderboard_success(UpdatePlayerStatisticsResult result)
    {
        #region NEROZKLIKNOUT
        System.Threading.Thread.Sleep(500); //tohohle řádku si nevšímejte
        #endregion
        Get_leaderboard();
    }
    public void Get_leaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "score",
            StartPosition = 0,
            MaxResultsCount = depth
        };
        PlayFabClientAPI.GetLeaderboard(request, Get_leaderboard_success, Fail);
    }
    void Get_leaderboard_success(GetLeaderboardResult result)
    {
        for (int i = 0; i < score_displays.Length; i++)
        {
            if (i < result.Leaderboard.Count)
            {
                var data = result.Leaderboard[i];
                score_displays[i].text = $"{data.Position + 1}. {data.DisplayName} {data.StatValue}";
            }
            else score_displays[i].text = "";
        }
    }
    public void Send_name(string name)
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = name
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, Name_success, Fail);
    }
    void Name_success(UpdateUserTitleDisplayNameResult result)
    {

    }

    public void Recieved_success(GetUserDataResult result)
    {
        if (result.Data.ContainsKey("Name"))
        {

        }
    }
}
