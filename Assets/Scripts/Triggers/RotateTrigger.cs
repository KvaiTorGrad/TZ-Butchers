using UnityEngine;

public class RotateTrigger : MonoBehaviour
{
    private enum DirectionRotate
    {
        Left,
        Right
    }
    [SerializeField] private DirectionRotate _directionRotate;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.TryGetComponent(out IControlleble player))
        {
            float direction = 0;
            switch (_directionRotate)
            {
                case DirectionRotate.Left:
                    direction = -90;
                    player.TurningDirection(Side.X,direction);
                    player.MaxXPosition = transform.position.x + player.RangePosition;
                    player.MinXPosition = transform.position.x - player.RangePosition;
                    break;
                case DirectionRotate.Right:
                    direction = 90;
                    player.TurningDirection(Side.Z, direction);
                    player.MaxXPosition = transform.position.z + player.RangePosition;
                    player.MinXPosition = transform.position.z - player.RangePosition;
                    break;
            }
        }
    }
}
