using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private Vector2 _move;
    private float _moveX;
    private float _moveY;
    
    private Vector3 _offset;
    public PlayerController player;
    void Start()
    {
        _offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        // _move = Mouse.current.delta.ReadValue();
        // _moveX = _move.x;
        // _moveY = _move.y;
        // //CustomLogger.Log(_move.ToString());
        // transform.RotateAround(player.transform.position,Vector3.up,_moveX*Time.deltaTime);
        transform.position = player.transform.position + _offset;
    }

}
