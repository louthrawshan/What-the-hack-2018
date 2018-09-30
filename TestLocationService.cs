using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestLocationService : MonoBehaviour
{
    IEnumerator Start()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location
        Input.location.Start(0.1f,0.1f);
        Input.compass.enabled = true;

        // Wait until service initializes
        int maxWait = 5;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude);
        }
    }

void OnGUI()
    {
        string latitude;
        string longitude;
        latitude = Input.location.lastData.latitude.ToString();
        longitude = Input.location.lastData.longitude.ToString();
        GUI.Label(new Rect(100, 300, 200, 40), "Location: " + latitude + " " + longitude);
        GUI.Label(new Rect(200, 300, 200, 40), "Compass heading = " + Input.compass.magneticHeading);
    }

}