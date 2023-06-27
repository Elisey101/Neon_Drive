using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    private void Awake() => Instantiate(_player, new Vector3(0, 0, 7), transform.rotation, transform);
}
