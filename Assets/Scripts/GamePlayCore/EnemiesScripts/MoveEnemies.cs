using UnityEngine;

public class MoveEnemies : MonoBehaviour
{
    [SerializeField] private bool _randomSpeed;
    private PlayerController _playerController;
    private MoveLeft _moveLeft;
    private Collider _distCollider;
    private Vector3 _rayOrigin;
    private float _rayY = 0.5f;
    private float _moveSpeed;
    private float _slowdownDist = 5.5f;
    private float _slowdown = 1.8f;


    private void Start()
    {
        _playerController = GameObject.Find("Player").GetComponentInChildren<PlayerController>();
        _moveLeft = FindObjectOfType<MoveLeft>();
        _slowdown = Random.Range(1.4f, 3f);
    }

    private void Update() => Move();

    private void FixedUpdate() => Ray();

    private void Move()
    {
        _moveSpeed = (_moveLeft.ObjectSpeed - (_playerController.CurrentMoveVector - 10 / _slowdown)) + _playerController.CurrentMoveVector;
        if(_playerController.Move < 0 && transform.position.z > 100) _moveSpeed = 0;
        if(!_playerController.GameOver) transform.Translate(Vector3.back * _moveSpeed * Time.deltaTime);
    }

    private void Ray()
    {
        _rayOrigin = new Vector3(transform.position.x, _rayY, transform.position.z);
        
        RaycastHit hit;
        Ray downRay = new Ray(_rayOrigin, Vector3.forward);
        Debug.DrawRay(_rayOrigin, Vector3.forward, Color.green);

        if (Physics.Raycast(downRay, out hit)) if(hit.distance <= _slowdownDist) _slowdown = 1.4f;
    }
}