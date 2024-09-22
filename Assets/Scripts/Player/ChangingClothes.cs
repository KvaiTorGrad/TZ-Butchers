using UnityEngine;

public class ChangingClothes : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer[] clothes;
    [SerializeField] private AudioClip _clip;
    private int _activeLvlCloth = 1;
    private void Start()
    {
        GameManager.Instance.Bank.ConditionLvl += Change;
    }
    private void Change(int lvl)
    {
        clothes[_activeLvlCloth].gameObject.SetActive(false);
        switch (lvl)
        {
            case 1:
                _activeLvlCloth = 0;
                break;
            case 2:
                _activeLvlCloth = 1;
                break;
            case 3:
                _activeLvlCloth = 2;
                break;
            case 4:
                _activeLvlCloth = 3;
                break;
            case 5:
                _activeLvlCloth = 4;
                break;
        }
        clothes[_activeLvlCloth].gameObject.SetActive(true);
    }
    private void OnDestroy()
    {
        GameManager.Instance.Bank.ConditionLvl -= Change;
    }
}
