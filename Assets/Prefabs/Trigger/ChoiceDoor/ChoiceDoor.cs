using UnityEngine;

public class ChoiceDoor : Item
{
    private enum Choice
    {
        School,
        Party
    }
    [SerializeField] private Choice _choice;
    [SerializeField] private AudioClip _clip;
    public override void Include(bool isInclude)
    {
        transform.root.gameObject.SetActive(isInclude);
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.TryGetComponent(out IControlleble player))
        {
            switch (_choice)
            {
                case Choice.School:
                    GameManager.Instance.Bank.SetCondition(20);
                    break;
                case Choice.Party:
                    GameManager.Instance.Bank.SetCondition(-20);
                    break;
            }
            SFXManager.Instance.PlayAudioClip.Invoke(_clip);
        }
    }
}
