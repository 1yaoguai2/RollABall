using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 _move;
    private float _moveX;
    private float _moveY;

    private Rigidbody _rg;
    public float moveSpeed = 1f;

    private Transform _cameraTrans;
    private Vector3 _startPos;

    private void Awake()
    {
        _rg = GetComponent<Rigidbody>();
        _cameraTrans = Camera.main.transform;
    }

    private void Start()
    {
        StaticModel.IsGameOver = false;
        _startPos = transform.position;
        EventSoManager.Instance.raiseLevelEventSo.OnRaiseEvent += OnRaiseGame;
    }

    

    private void FixedUpdate()
    {
        _rg.AddForce((_cameraTrans.forward *_moveY + _cameraTrans.right * _moveX) * moveSpeed);
        if (transform.position.y < -20f)
        {
            GameOver();
        }
        Debug.Log(_move);
    }

    private void OnMove(InputValue inputValue)
    {
        _move = inputValue.Get<Vector2>();
        _moveX = _move.x;
        _moveY = _move.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            StaticModel.Score++;
        }
        else if(other.CompareTag("Enemy"))
        {
           GameOver();
        }
    }

    //游戏结束
    private void GameOver()
    {
        StaticModel.IsGameOver = true;
        EventSoManager.Instance.gameOverEventSo.RaisedEvent();
        CustomLogger.Log("游戏结束");
    }
    
    //游戏重启
    private void OnRaiseGame()
    {
        StaticModel.IsGameOver = false;
        transform.position = _startPos;
        _rg.Sleep();
    }
}
