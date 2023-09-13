using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var builder = CAS.MobileAds.BuildManager();
        builder.WithInitListener((success, error) => {
            // The user completes the flow here

            // Consent status can be checked via
             var consent = CAS.MobileAds.settings.userConsent;

            // Initialize other 3rd-party SDKs
        });


        CAS.MobileAds.ValidateIntegration();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
