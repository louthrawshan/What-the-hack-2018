using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPS : MonoBehaviour {
    public static GPS Instance { set; get; }
    public float longitude;
    public float latitude;

	// Use this for initialization
	private void Start () {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        StartCoroutine(StartLocationService());
	}

    private IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser) {
            Debug.Log("user has not enabled GPS");
            yield break;
        }
    
        Input.location.Start();
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 0) {
            Debug.Log("timed out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed) {
            Debug.Log("unable to find device location");
            yield break;
        }

        latitude = Input.location.lastData.latitude ;
        longitude = Input.location.lastData.longitude;

        yield break;

    }

    // Update is called once per frame
    private void Update () {
		
	}
}
