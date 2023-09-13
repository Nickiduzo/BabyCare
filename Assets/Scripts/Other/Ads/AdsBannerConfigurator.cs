using CAS;
using CAS.AdObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsBannerConfigurator : MonoBehaviour
{
    private BannerAdObject activeBanner;
    void Start()
    {  
        float aspectRatio = Mathf.Max(Screen.width, Screen.height) / Mathf.Min(Screen.width, Screen.height);
        bool isTablet = (DeviceDiagonalSizeInInches() > 6.5f && aspectRatio < 2f);

        activeBanner = BannerAdObject.Instance;
        activeBanner.LoadAd();
        if (isTablet)
        {
            activeBanner.SetAdSize(AdSize.Leaderboard);
        }
        else
        {
            activeBanner.SetAdSize(AdSize.AdaptiveFullWidth);
        }
    }

    //check screen resolution
    private float DeviceDiagonalSizeInInches()
    {
        float screenWidth = Screen.width / Screen.dpi;
        float screenHeight = Screen.height / Screen.dpi;
        float diagonalInches = Mathf.Sqrt(Mathf.Pow(screenWidth, 2) + Mathf.Pow(screenHeight, 2));

        return diagonalInches;
    }

}
