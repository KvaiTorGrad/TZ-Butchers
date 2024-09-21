using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IControlleble
{
    [SerializeField] private float _speedForward;
    [SerializeField] private float _speedSide;
    [SerializeField] private float _speedRotate;
    [Range(0, 3), SerializeField] private float _rangePosition;
    private float _minXPosition, _maxXPosition;


    private delegate void LimitSide();
    private LimitSide _limitSide;

    public float RangePosition => _rangePosition;

    public float MinXPosition { get => _minXPosition; set => _minXPosition = value; }
    public float MaxXPosition { get => _maxXPosition; set => _maxXPosition = value; }

    private void Awake()
    {
        MinXPosition = -RangePosition;
        MaxXPosition = RangePosition;
        InitLimitationSide(Side.X);
    }
    private void Start()
    {
        Controller.Instance.SetControlleble(this);
    }

    public void MovementForward()
    {
        transform.Translate(_speedForward * Time.deltaTime * Vector3.forward);
    }

    public void MovementHorizontal(ControlsGame inputActions)
    {
        var directionX = inputActions.Player.Move.ReadValue<Vector2>().x;
        if (directionX == 0) return;
        transform.Translate(_speedSide * directionX * Time.deltaTime * Vector3.right);
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
            var rotate = Mathf.Lerp(transform.eulerAngles.y, direction, Time.deltaTime * _speedRotate);
            if (direction > transform.eulerAngles.y)
                rotate = Mathf.CeilToInt(rotate);
            else
                rotate = Mathf.FloorToInt(rotate);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotate, transform.eulerAngles.z);
            yield return null;
        }
    }
}

public enum Side
{
    X,
    Z
}