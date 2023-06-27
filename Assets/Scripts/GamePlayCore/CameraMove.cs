using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector3 _target;
    private Camera _cam;
    private PlayerController _playerController;
    private MoveLeft _moveLeft;
    private float _zPos;
    private float _camPos = 10;
    private float _yRange = 9.11f;
    private float _camSpeed = 13;
    private int _desktopFov = 44;
    private int _mobileFov = 35;

    void Start()
    {
        _cam = GetComponent<Camera>();
        _rb = GetComponent<Rigidbody>();
        _playerController = GameObject.Find("Player").GetComponentInChildren<PlayerController>();
        _moveLeft = FindObjectOfType<MoveLeft>();
        _target = new Vector3(0, 0, _camPos);
    }

    void Update()
    {
        Move();
        Fov();
    }

    void Move()
    {
        if(_playerController.GameOver)
        {
            transform.position = Vector3.Lerp(transform.position, _target, Time.deltaTime * (_moveLeft.CommonSpeed / _camSpeed));
            transform.position = new Vector3(transform.position.x, _yRange, transform.position.z);
        }
    }
    void Fov()
    {
        if(SystemInfo.deviceType == DeviceType.Desktop) _cam.fieldOfView = _desktopFov;
        else _cam.fieldOfView = _mobileFov;
    }
    
}
