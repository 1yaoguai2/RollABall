using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Transform _playerTrans;

    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _playerTrans = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (_playerTrans is not null)
        {
            _navMeshAgent.SetDestination(_playerTrans.position);
        }
    }
}
