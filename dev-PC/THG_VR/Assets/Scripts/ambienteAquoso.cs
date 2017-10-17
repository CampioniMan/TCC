using UnityEngine;
using System.Collections;

public class ambienteAquoso : MonoBehaviour {

    public float waterLevel = 0;
    private bool isUnderwater;
    private Color underwaterColor;

    void Start()
    {
        underwaterColor = new Color(0.27f, 0.58f, 0.65f, 0.8f); //#44D7A6
        RenderSettings.fogDensity = 0.06f;
        RenderSettings.fog = false;
    }

    void Update()
    {
        if ((transform.position.y < waterLevel) != isUnderwater)
        {
            isUnderwater = transform.position.y < waterLevel;
            if (isUnderwater) setUnderwater();
            if (!isUnderwater) setNormal();
        }
    }

    void setNormal()
    {
        RenderSettings.fog = false;
    }

    void setUnderwater()
    {
        RenderSettings.fogColor = underwaterColor;
        RenderSettings.fog = true;
    }
}
