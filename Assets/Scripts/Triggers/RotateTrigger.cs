using UnityEngine;

public class RotateTrigger : Item
{
    private enum DirectionRotate
    {
        Left,
        Right
    }
    [SerializeField] private DirectionRotate _directionRotate;
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.TryGetComponent(out IControlleble player))
        {
            float direction = 0;
            switch (_directionRotate)
            {
                case DirectionRotate.Left:
                    direction = -90;
                    player.TurningDirection(Side.X,direction);
                    player.MaxXPosition = transform.position.x + player.PlayerParametrs.RangePosition;
                    player.MinXPosition = transform.position.x - player.PlayerParametrs.RangePosition;
                    break;
                case DirectionRotate.Right:
                    direction = 90;
                    player.TurningDirection(Side.Z, direction);
                    player.MaxXPosition = transform.position.z + player.PlayerParametrs.RangePosition;
                    player.MinXPosition = transform.position.z - player.PlayerParametrs.RangePosition;
                    break;
            }
        }
    }
}
