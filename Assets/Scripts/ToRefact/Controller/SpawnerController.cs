using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerController : MonoBehaviour
{
    private CharacterManager _characterManager;
    private Spawner _spawner;
    private Touch myTouch;
    private float _rotationSpeed;
    private float _returnSpeed;
    private float _deltaAngle;

    private void Awake()
    {
        _characterManager = FindObjectOfType<CharacterManager>();
        _spawner = FindObjectOfType<Spawner>();
        _rotationSpeed = _spawner.RotationSpeed;
        _returnSpeed = _spawner.ReturnSpeed;
        _deltaAngle = _spawner.DeltaAngle;

        StartCoroutine(Spawn());
        /*
        foreach(var character in _characterManager.CharacterPool)
        {
            var model = Instantiate(character.PrefabPreview, _spawner.Instance.transform.position, _spawner.Instance.transform.rotation);
            model.transform.parent = _spawner.Instance.transform;
            model.name = character.Name;
            model.SetActive(false);
            _spawner.Model.Add(model);
        */
    }

    IEnumerator Spawn()
    {
        yield return null;

        foreach (var character in _characterManager.CharacterPool)
        {
            var model = Instantiate(character.PrefabPreview, _spawner.Instance.transform.position, _spawner.Instance.transform.rotation);
            model.transform.parent = _spawner.Instance.transform;
            model.name = character.Name;
            model.SetActive(false);
            _spawner.Model.Add(model);
        }
    }

    void Update()
    {
        if (!Main.Instance.MapMenu.activeSelf)
        {
            if (Input.touchCount == 1)
            {
                myTouch = Input.GetTouch(0);
                _spawner.Instance.transform.Rotate(Vector3.down * myTouch.deltaPosition.x * _rotationSpeed);
            }
            else if (_spawner.Instance.transform.rotation != _spawner.StartPosition.rotation)
            {
                float angle = _spawner.StartPosition.eulerAngles.y - _spawner.Instance.transform.eulerAngles.y;
                if (angle > 180) { angle -= 360; }
                else if (angle < -180) { angle += 360; }
                print(angle);
                if (angle > _deltaAngle)
                {
                    _spawner.Instance.transform.Rotate(Vector3.up * _returnSpeed * _rotationSpeed);
                }
                else if (angle < -_deltaAngle)
                {
                    _spawner.Instance.transform.Rotate(Vector3.down * _returnSpeed * _rotationSpeed);
                }
                else _spawner.Instance.transform.rotation = _spawner.StartPosition.rotation;
            }
        }
    }
}

