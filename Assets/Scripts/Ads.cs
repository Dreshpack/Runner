using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class Ads : MonoBehaviour
{ 
    private void OnEnable()
    {
        InitializeAds();
    }

    private void InitializeAds()
    {
        MobileAds.Initialize(InitializationStatus => { });
    }
}
