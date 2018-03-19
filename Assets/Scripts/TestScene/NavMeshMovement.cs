using UnityEngine;
using UnityEngine.AI;

public class NavMeshMovement : MonoBehaviour {

    [SerializeField]
    Transform _destination;

    NavMeshAgent _navMeshAgent;

	// Use this for initialization
	void Start ()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        SetDestination();
	}

    public void SetDestination()
    {
        if (_destination != null)
        {
            Vector3 targetVector = _destination.transform.position;
            _navMeshAgent.SetDestination(targetVector);
        }
    }
}
