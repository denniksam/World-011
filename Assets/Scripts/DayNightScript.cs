using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightScript : MonoBehaviour
{
    [SerializeField]
    private Material daySkybox;
    [SerializeField]
    private Material nightSkybox;

    private Light _sun;    // Directional light 1
    private Light _moon;   // Directional light 0.5

    const float _fullDayTime = 30f;   // секунд на суточный цикл
    const float _deltaAngle = -360 / _fullDayTime;   // угол поворота света за 1 с

    float _dayTime;   // текущее время
    float _dayPhase;  // фаза суток приведенная к диапазону [0..1]
    

    void Start()
    {
        _sun = GameObject.Find("SunLight").GetComponent<Light>();
        _moon = GameObject.Find("MoonLight").GetComponent<Light>();
    }

    void LateUpdate()
    {
        _dayTime += Time.deltaTime;
        _dayTime %= _fullDayTime;
        _dayPhase = _dayTime / _fullDayTime;

        _sun.transform.Rotate(_deltaAngle * Time.deltaTime, 0, 0);
        _moon.transform.Rotate(_deltaAngle * Time.deltaTime, 0, 0);

        float koef = Mathf.Abs(Mathf.Cos(_dayPhase * 2f * Mathf.PI));
        // коэф. плавного перехода текстур и освещения

        if(_dayPhase > 0.25f && _dayPhase < 0.75f)  // ночная фаза
        {
            if (RenderSettings.skybox != nightSkybox)
            {
                RenderSettings.skybox = nightSkybox;
            }
            RenderSettings.ambientIntensity = koef / 2f;     // свет неба (дополнительный)            
        }
        else  // дневная фаза
        {
            if (RenderSettings.skybox != daySkybox)
            {
                RenderSettings.skybox = daySkybox;
            }
            RenderSettings.ambientIntensity = koef;
        }
        RenderSettings.skybox.SetFloat("_Exposure", 0.2f + koef * 0.8f);  // яркость (видимость) самой текстуры
    }
}
/* Д.З. Сгруппировать источники света в общий элемент и вращать его
 * для изменения фаз дня.
 * Реализовать "выключение" источников света, дающий свет снизу
 * (днем - Луна, ночью - Солнце)
 */