using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnityAdsitem : MonoBehaviour
{
    public GameObject but;
    public Text rewardedTxt;

    private void Update()
    {
        rewardedTxt.text = "NO AD AVAILABLE NOW!";
    }

    public void WatchVideoAd()
    {
        SoundManager.Click();
        
        // if (AdsManager.Instance)
        // {
        //     SoundManager.Click();
        //     AdsManager.AdResult += AdsManager_AdResult;
        //     AdsManager.Instance.ShowRewardedAds();
        // }
    }

    // private void AdsManager_AdResult(bool isSuccess, int rewarded)
    // {
    //     AdsManager.AdResult -= AdsManager_AdResult;
    //     if (isSuccess)
    //     {
    //         GlobalValue.SavedCoins += rewarded;
    //         SoundManager.PlaySfx(SoundManager.Instance.soundPurchased);
    //     }
    // }
}
