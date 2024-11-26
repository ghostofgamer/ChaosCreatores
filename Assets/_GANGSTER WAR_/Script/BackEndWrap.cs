/*using System;
using AlphaLayer.Backend;
using UnityEngine;

namespace _GANGSTER_WAR_.Script
{
    public class BackEndWrap : MonoBehaviour
    {
        [SerializeField] private AlphaLayerGamesBackendService backendService;

        private int coins = 200;
        private int levelPass;
        
        public static BackEndWrap Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                
                backendService.Authorization(OnAuthorizationCallback, OnErrorCallback);
                backendService.GetProfile(OnUserProfileCallback, callbackError: OnErrorCallback);
            }
        }

        private void OnAuthorizationCallback(AlphaLayerGamesBackendService.AuthResponse response)
        {
            backendService.DataStorageProvider.SaveHeader("Authorization", $"Bearer {response.token}");
            Debug.Log("JWT successfully saved in data storage!");
        }
        
        private void OnUserProfileCallback(AlphaLayerGamesBackendService.UserProfileResponse response)
        {
            var profileInfo =$"[Profile]\nUsername: {response.username}\nBalance: {response.balance}\nScore: {response.score}";
            Debug.Log(profileInfo);
        }
        
        private void OnErrorCallback(Exception error)
        {
            Debug.LogError(error);
        }

        public int GetSavedCoins()
        {
            return coins;
        }

        public void SetSavedCoins(int coins)
        {
            this.coins = coins;

            var rewardData = new AlphaLayerGamesBackendService.RewardData
            {
                reward = this.coins,
                score = levelPass
            };
            
            backendService.PostReward(rewardData, callback: OnPostRewardCallback, callbackError: OnErrorCallback);
        }
        
        public int GetLevelPassCoins()
        {
            return levelPass;
        }

        public void SetLevelPassCoins(int levelPass)
        {
            this.levelPass = levelPass;
            
            var rewardData = new AlphaLayerGamesBackendService.RewardData
            {
                reward = coins,
                score = this.levelPass
            };
            
            backendService.PostReward(rewardData, callback: OnPostRewardCallback, callbackError: OnErrorCallback);
        }

        private void OnPostRewardCallback(string result)
        {
            Debug.Log($"OnPostRewardCallback(): {result}");
        }
    }
}*/