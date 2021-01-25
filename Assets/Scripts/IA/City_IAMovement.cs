using UnityEngine;
using UnityEngine.AI;

public class City_IAMovement : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent = null;
    Vector3 target = Vector3.zero;
    float initialSpeed = 0, initialAccel = 0;

    public bool IsValid => agent && target != Vector3.zero;
    public bool IsAtPosition
    {
        get
        {
            Vector3 _targetPos = new Vector3(target.x, 0, target.z);
            Vector3 _myPos = new Vector3(transform.position.x, 0, transform.position.z);
            return Vector3.Distance(_myPos, _targetPos) < 2;
        }
    }

    public void InitializeMovement()
    {
        initialSpeed = agent.speed;
        initialAccel = agent.acceleration;
        InvokeRepeating("MoveTo", Random.Range(0f, .5f), .5f);
    }
    void MoveTo()
    {
        SetDestination(IsValid ? target : transform.position);
        if (IsAtPosition)
            gameObject.SetActive(false);
    }
    void SetDestination(Vector3 _targetPosition)
    {
        if (!IsValid || !isActiveAndEnabled) return;
        agent.SetDestination(_targetPosition);
    }
    public void SetVelocity(float _speedCoef)
    {
        agent.speed = initialSpeed * _speedCoef;
        agent.acceleration = initialAccel * _speedCoef;
    }
    public void SetTarget(Vector3 _position) => target = _position;
}
