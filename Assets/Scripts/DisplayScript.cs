using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayScript : MonoBehaviour
{
    [SerializeField]
    private GameObject character;
    [SerializeField]
    private GameObject coin;
    [SerializeField]
    private TMPro.TextMeshProUGUI textDistance;

    const float neutralDistance = 7f;  // переход от синего к красному тексту

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(
            character.transform.position,
            coin.transform.position);

        textDistance.text = distance.ToString("0.0");
        distance /= neutralDistance;
        textDistance.color = new Color(
            1 / (1 + distance),
            0.2f,
            distance / (1 + distance));
    }
}
/* Обеспечить случайное появление монеты: не ближе 10 и не дальше 20 от персонажа
 * (числовые значения задавать константами)
 * Контролировать выход за пределы мира (гор)
 * ** корректировать высоту появления монеты на высоту земли в данной точке
 * * если монета спавнится с колизией (деревья, горы,...), то переспавнить
 */
