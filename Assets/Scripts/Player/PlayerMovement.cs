using ButchersGames;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IControlleble
{
    private Animator _animator;
    private delegate void LimitSide();
    private LimitSide _limitSide;
    [SerializeField] private PlayerParametrs _playerParametrs;
    public PlayerParametrs PlayerParametrs => _playerParametrs;

    public float MinXPosition { get; set; }
    public float MaxXPosition { get; set; }

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        MinXPosition = -PlayerParametrs.RangePosition;
        MaxXPosition = PlayerParametrs.RangePosition;
        InitLimitationSide(Side.X);
    }
    private void Start()
    {
        GameManager.Instance.Bank.ConditionLvl += SetAnimationMove;
        LevelManager.OnLevelEnted += SetAnimationEnd;
        Controller.Instance.SetControlleble(this);
    }

    public void MovementForward()
    {
        transform.Translate(PlayerParametrs.SpeedForward * Time.deltaTime * Vector3.forward);
    }

    public void MovementHorizontal(ControlsGame inputActions)
    {
        var directionX = inputActions.Player.Move.ReadValue<Vector2>().x;
        if (directionX == 0) return;
        transform.Translate(PlayerParametrs.SpeedSide * directionX * Time.deltaTime * Vector3.right);
        _limitSide();
    }
    private void InitLimitationSide(Side side)
    {
        switch (side)
        {
            case Side.X:
                _limitSide = LimitSideX;
                break;
            case Side.Z:
                _limitSide = LimitSideZ;
                break;
        }
    }
    private void LimitSideX()
    {
        var rangeX = Mathf.Clamp(transform.position.x, MinXPosition, MaxXPosition);
        transform.position = new Vector3(rangeX, transform.position.y, transform.position.z);
    }
    private void LimitSideZ()
    {
        var rangeZ = Mathf.Clamp(transform.position.z, MinXPosition, MaxXPosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, rangeZ);
    }

    public void TurningDirection(Side side, float rotateY)
    {
        InitLimitationSide(side);
        StartCoroutine(RotatePlayer(rotateY));
    }

    private IEnumerator RotatePlayer(float rotateY)
    {
        var direction = transform.eulerAngles.y + rotateY;
        while (transform.eulerAngles.y != direction)
        {
            var rotate = Mathf.Lerp(transform.eulerAngles.y, direction, Time.deltaTime * PlayerParametrs.SpeedRotate);
            if (direction > transform.eulerAngles.y)
                rotate = Mathf.CeilToInt(rotate);
            else
                rotate = Mathf.FloorToInt(rotate);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotate, transform.eulerAngles.z);
            yield return null;
        }
    }

    private void SetAnimationMove(int lvl) => _animator.SetFloat("LvlCondition", lvl);

    private void SetAnimationEnd() => _animator.SetTrigger("EndGame");

    private void OnDestroy()
    {
        GameManager.Instance.Bank.ConditionLvl -= SetAnimationMove;
        LevelManager.OnLevelEnted -= SetAnimationEnd;
    }
}

public enum Side
{
    X,
    Z
}