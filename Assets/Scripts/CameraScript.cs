using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private GameObject _character;   // ссылка на объект-персонаж
    private Vector3 _offset;         // смещение камеры и персонажа
    private float _angleX;           // накопленный угол поворота камеры по Х
    private float _angleY;           // накопленный угол поворота камеры по Y
    private float _sensX = 150;      // Чувствительность камеры к поворотам по гориз.
    private float _sensY = 100;      // ... по вертикали

    void Start()
    {
        _character = GameObject.Find("Character");   // поиск по имени (имя - в иерархии объектов)
        _offset = this.transform.position - _character.transform.position;
        _angleX = 0;
        _angleY = this.transform.eulerAngles.x;  // смещение по Y == вращение вокруг X
    }

    private void Update()
    {
        float mx = Input.GetAxis("Mouse X");   // данные о перемещении мыши по Х
        float my = Input.GetAxis("Mouse Y");

        _angleX += mx * Time.deltaTime * _sensX;
        _angleY -= my * Time.deltaTime * _sensY;   // "-" для инверсии вращения по верт.
    }

    void LateUpdate()
    {
        this.transform.position = 
            _character.transform.position + 
            Quaternion.Euler(0, _angleX, 0) * _offset;

        this.transform.eulerAngles = new Vector3(_angleY, _angleX, 0);

        if( ! Input.GetMouseButton(0))  // 0 - ЛКМ, 1 - ПКМ, 2 - СКМ, 3-доп(и назад, и вперед)
        {
            // вращаем и сам персонаж по камере если не зажата ЛКМ
            _character.transform.eulerAngles = new Vector3(0, _angleX, 0);
        }
        // _character.transform.forward
    }
}
/* Управление камерой
 * 1. Следование
 *   - при старте определяем взаимное размещение (вектор) камеры и персонажа
 *   - при Update() определяем положение персонажа и переносим камеру к
 *      персонажу с учетом начального размещения
 * 2. Вращение
 *   - данные от мыши приходят про ее перемещение (а не положение), поэтому
 *      для суммарного эффекта все данные о перемещении нужно накапливать
 *      
 * Д.З. Ограничить изменение угла вертикального наклона камеры - 
 *  границы подобрать (примерно -45..30)
 *  ** поменять управление персонажем - двигать учитывая его поворот
 *  
 * Д.З. Реализовать элластичный дизайн игрового меню - адаптивность
 * к разным размерам и пропорциям экрана.
 * ** добавить в меню блоки настроек "Сложность" (показывать расстояние до монеты,
 *    дальность спавна, ...)
 *    блок "Звук" - громкость (звук, эффекты) и общее выключение
 */
