using UnityEngine;
using System.Collections;

public class NightDaySystem : MonoBehaviour
{
    public float daySpeed;
    GameObject daySky, moon, sun, star1, star2;
    float angle, opacity;

    void Start()
    {
        daySky = GameObject.Find("daySky");
        moon = GameObject.Find("TheMoon");
        sun = GameObject.Find("TheSun");
        star1 = GameObject.Find("Stars1");
        star2 = GameObject.Find("Stars2");
        angle = 45;
    }


    void Update()
    {
        angle += daySpeed*Time.deltaTime *6;
        opacity = (angle + 90) / 180;
        moon.transform.eulerAngles = new Vector3(0, 0, angle);
        sun.transform.eulerAngles = new Vector3(0, 0, angle);
        if (angle > 270)
        {
            angle = angle - 360;
        }
        if (opacity < 1)
        {
            daySky.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, opacity);
            star1.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 - opacity);
            star2.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 - opacity);
        }
        else
        {
            daySky.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 - (opacity - 1));
            star1.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (opacity - 1));
            star2.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (opacity - 1));

        }
    }
}