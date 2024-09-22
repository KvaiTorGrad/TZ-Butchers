using ButchersGames;
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
        LevelManager.OnLevelStarted += StartGame;
        LevelManager.OnLevelEnted += EndGame;
    }

    private void StartGame()
    {
        _inputActions.Enable();
    }
    private void EndGame()
    {
        _inputActions.Disable();
    }
    void Update()
    {
        if (!_inputActions.Player.enabled) return;
        _controlleble?.MovementForward();
        _controlleble?.MovementHorizontal(_inputActions);
    }

    private void OnDestroy()
    {
        LevelManager.OnLevelStarted -= StartGame;
        LevelManager.OnLevelEnted -= EndGame;
    }
}

public interface IControlleble
{
    public PlayerParametrs PlayerParametrs { get;}
    public float MinXPosition { get; set; }
    public float MaxXPosition { get; set; }
    public void MovementForward();
    public void MovementHorizontal(ControlsGame inputActions);
    public void TurningDirection(Side side, float rotateY);

}