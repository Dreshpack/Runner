using GoogleMobileAds.Api;
using UnityEngine;
using System;
using System.Collections;
using TMPro;

public class RevivalAds : MonoBehaviour
{
    [SerializeField] Collision _collision;
    [SerializeField] TMP_Text _adsButtonText;

    public delegate void getBorder(GameObject border);
    private string _rewardedUnitId = "ca-app-pub-3940256099942544/5224354917";
    private RewardedAd _ads;
    public Action isPlaying;
    public event Action played;

    private void OnEnable()
    {
        _ads = new RewardedAd(_rewardedUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        _ads.LoadAd(adRequest);
        _ads.OnUserEarnedReward += HandleUserEarnedReward;
    }

    private void HandleUserEarnedReward(object sender, Reward reward)
    {
        played?.Invoke();
    }

    public void AdsCoroutine()
    {
        StartCoroutine(ShowAds());
    }    

    private IEnumerator ShowAds()
    {
        _adsButtonText.text = "Loading ADS";
        yield return new WaitUntil(predicate: () => _ads.IsLoaded());
        //while(!_ads.IsLoaded())
        if(_ads.IsLoaded())
        {
            isPlaying?.Invoke();
            _ads.Show();
        }

    }

}
