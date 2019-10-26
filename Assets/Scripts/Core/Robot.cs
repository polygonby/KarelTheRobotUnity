using UnityEngine;

public class Robot : MonoBehaviour
{
    public float MoveSpeed => _moveSpeed;
    public float TurnSpeed => _turnSpeed;

    [SerializeField, Tooltip("In units per second")]
    private float _moveSpeed = 1.0f;
    [SerializeField, Tooltip("In degrees per second")]
    private float _turnSpeed = 90.0f;
}
