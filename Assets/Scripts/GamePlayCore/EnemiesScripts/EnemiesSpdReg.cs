// using UnityEngine;

// public class EnemiesSpdReg : MonoBehaviour
// {
//     private MoveEnemies _moveEnemies;
//     private BoxCollider _boxCollider;
//     private float _spdRange;
//     private void Start() 
//     {
//       _moveEnemies = FindObjectOfType<MoveEnemies>();
//       _boxCollider = FindObjectOfType<BoxCollider>();
//       _spdRange = Random.Range(1.4f, 1.8f);
//     } 
//     private void OnTriggerEnter(Collider other)
//     {
//         if(other.CompareTag("Distance")) 
//         {
//           _moveEnemies.Slowdown = 1.4f;
//           _boxCollider.isTrigger = false;
//         }
//         else _moveEnemies.Slowdown = _spdRange;
//     }
// }


// --------------------------------------------------------------


// public class EnemiesSpdReg : MonoBehaviour
// {
//     private MoveEnemies _moveEnemies;
//     private BoxCollider _boxCollider;
//     private int _spdRange;
//     private void Start() 
//     {
//       _moveEnemies = FindObjectOfType<MoveEnemies>();
//       _boxCollider = FindObjectOfType<BoxCollider>();
//       _spdRange = Random.Range(1, 4);
//     }
    
//     private void OnTriggerEnter(Collider other)
//     {
//       if(other.CompareTag("Distance")) 
//       {
//         _moveEnemies.Slowdown = 1.4f;
//         // _boxCollider.enabled = false;
//       }
//       else 
//       {
//         if(_spdRange > 1) _moveEnemies.Slowdown = 1.4f;
//         else if(_spdRange == 1) _moveEnemies.Slowdown = 2f;
//       }
//     }
// }
