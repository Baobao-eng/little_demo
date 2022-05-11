using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeLight : MonoBehaviour
{
    
    [Range(0.0f,1.0f)]
    [SerializeField] float time;
    [SerializeField] float fullDayLength;
    [SerializeField] float startTime;
    [SerializeField] float timeRate;
    [SerializeField] Vector3 noon;

    [Header("Sun")]
    [SerializeField] Light sun;
    [SerializeField] Gradient sunColor;
    [SerializeField] AnimationCurve sunIntensity;

    [Header("Moon")]
    [SerializeField] Light moon;
    [SerializeField] Gradient moonColor;
    [SerializeField] AnimationCurve moonIntensity;

    [Header("Other Lighting")]
    [SerializeField] AnimationCurve lightIntensity;
    [SerializeField] AnimationCurve reflectionIntensity;

    [Header("Campfire")]
    [SerializeField] ParticleSystem lightUp;
    // Start is called before the first frame update
    void Start()
    {
        timeRate = 1.0f / fullDayLength;
        time = startTime;
        

    }

    // Update is called once per frame
    void Update()
    {
        time += timeRate * Time.deltaTime;

        if(time >= 1.0f)

            time = 0.0f;
        
        sun.transform.eulerAngles = (time - 0.25f) * noon * 4.0f;
        moon.transform.eulerAngles = (time - 0.75f) * noon * 4.0f;

        sun.intensity = sunIntensity.Evaluate(time);
        moon.intensity = moonIntensity.Evaluate(time);

        sun.color = sunColor.Evaluate(time);
        moon.color = moonColor.Evaluate(time);

        if(sun.intensity == 0 && sun.gameObject.activeInHierarchy)
        {
            sun.gameObject.SetActive(false);
        }else if(sun.intensity > 0 && !sun.gameObject.activeInHierarchy )
        {
            lightUp.Stop();
            
            sun.gameObject.SetActive(true);
          
        }

        if (moon.intensity == 0 && moon.gameObject.activeInHierarchy)
        {
            moon.gameObject.SetActive(false);
        }
        else if (moon.intensity > 0 && !moon.gameObject.activeInHierarchy)
        {
            lightUp.Play();
             
            moon.gameObject.SetActive(true);
        }

        RenderSettings.ambientIntensity = lightIntensity.Evaluate(time);
        RenderSettings.ambientIntensity = reflectionIntensity.Evaluate(time);
    }

   
}
