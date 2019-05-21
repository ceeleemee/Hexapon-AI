using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Monetization;

public class UnityAdsScript : MonoBehaviour
{

    string gameId = "3157034";
    bool testMode = true;
    public string bannerPlacement = "banner";
    


    void Start()
    {
        Monetization.Initialize(gameId, testMode); // initial the SDK
        StartCoroutine(ShowBannerWhenReady());
    }

    private void Update()
    {
        Advertisement.Banner.Show();
    }
    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(bannerPlacement))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Show(bannerPlacement);
    }




    }