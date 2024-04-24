using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    private LineRenderer _lr;
    private float _maxLaserDistance = 50f;
    
    [SerializeField] private Camera _mainCam;
    
    private void Awake()
    {
        _lr = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        StartCoroutine(ActivateLaser());
        
        _lr.positionCount = 2;
        _lr.SetPosition(0, transform.position + Vector3.up);

        var ray = _mainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit, _maxLaserDistance))
        {
            _lr.SetPosition(1, hit.point);
        }
        else
        {
            _lr.SetPosition(1, ray.origin + ray.direction * _maxLaserDistance);
        }
    }

    private IEnumerator ActivateLaser()
    {
        _lr.enabled = true;
        yield return new WaitForSeconds(1.0f);
        _lr.enabled = false;
    }
}
