using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Latlongtodirection : MonoBehaviour {
    public float lat_2;
    public float long_2;
    public TextMesh text;
    
	// Use this for initialization
	void Start () {
        float distance1 = distance();
        text.text = "dis:" + distance1.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        float distance1 = distance();
        text.text = "dis:" + distance1.ToString();
        change_direction();
	}

    public float distance() {
        float long_1 = GPS.Instance.longitude;
        float lat_1 = GPS.Instance.latitude;
        
        int R = 6371;
        var lat_rad_1 = Mathf.Deg2Rad * lat_1;
        var lat_rad_2 = Mathf.Deg2Rad * lat_2;
        var d_lat_rad = Mathf.Deg2Rad * (lat_2 - lat_1);
        var d_long_rad = Mathf.Deg2Rad * (long_2 - long_1);
        var a = Mathf.Pow(Mathf.Sin(d_lat_rad / 2), 2) + (Mathf.Pow(Mathf.Sin(d_long_rad / 2), 2) * Mathf.Cos(lat_rad_1) * Mathf.Cos(lat_rad_2));
        var c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        var total_dist = R * c * 1000; // convert to meters

        return total_dist;
        
    }


    public double DegreeBearing()
    {
        double lon1 = GPS.Instance.longitude;
        double lat1 = GPS.Instance.latitude;
        double lon2 = Double.Parse(long_2.ToString());
        double lat2 = Double.Parse(lat_2.ToString());

        var dLon = ToRad(lon2 - lon1);
        var dPhi = Math.Log(
            Math.Tan(ToRad(lat2) / 2 + Math.PI / 4) / Math.Tan(ToRad(lat1) / 2 + Math.PI / 4));
        if (Math.Abs(dLon) > Math.PI)
            dLon = dLon > 0 ? -(2 * Math.PI - dLon) : (2 * Math.PI + dLon);
        return ToBearing(Math.Atan2(dLon, dPhi));
    }
    public static double ToRad(double degrees)
    {
        return degrees * (Math.PI / 180);
    }

    public static double ToDegrees(double radians)
    {
        return radians * 180 / Math.PI;
    }

    public static double ToBearing(double radians)
    {
        // convert radians to degrees (as bearing: 0...360)
        return (ToDegrees(radians) + 360) % 360;
    }

    public void change_direction() {
        float rotate = Input.compass.magneticHeading - float.Parse(transform.rotation.ToString());
        transform.Rotate(rotate, 0, 0);
    }

}
