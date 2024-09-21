using SingleTon;

public class Controller : SingletonBase<Controller>
{
    private IControlleble _controlleble;
    private ControlsGame _inputActions;
    public void SetControlleble(IControlleble controlleble) => _controlleble = controlleble;

    protected override void Awake()
    {
        base.Awake();
        _inputActions = new();
    }
    private void Start()
    {
        StartGame();
    }
    private void StartGame()
    {
        _inputActions.Enable();
    }
    void Update()
    {
        if (!_inputActions.Player.enabled || _controlleble == null) return;
        _controlleble.MovementForward();
        _controlleble.MovementHorizontal(_inputActions);
    }
}

public interface IControlleble
{
    public float RangePosition { get; }
    public float MinXPosition { get; set; }
    public float MaxXPosition { get; set; }
    public void MovementForward();
    public void MovementHorizontal(ControlsGame inputActions);
    public void TurningDirection(Side side, float rotateY);

}