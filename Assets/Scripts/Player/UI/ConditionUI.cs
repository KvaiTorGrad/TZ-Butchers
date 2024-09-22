using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConditionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countMoney;
    [SerializeField] private Image _scaleCondition;
    [SerializeField] private TextMeshProUGUI _scaleConditionText;

    private void Start()
    {
        GameManager.Instance.Bank.UpdateConditionUI += SetConditionForUI;
        GameManager.Instance.Bank.ConditionLvl += SetColor;
    }
    private void SetConditionForUI(int currentCondition, int maxCondiiton)
    {
        _countMoney.text = currentCondition.ToString();
        _scaleCondition.fillAmount = (float)currentCondition / (float)maxCondiiton;
    }
    private void SetColor(int lvl)
    {
        Color color = Color.white;
        string text = string.Empty;
        switch (lvl)
        {
            case 1:
                color = Color.red;
                text = "Бомж";
                break;
            case 2:
                color = new Color(1f, 0.5f, 0);
                text = "Бедный";
                break;
            case 3:
                color = Color.yellow;
                text = "Состоятельный";
                break;
            case 4:
                color = Color.green;
                text = "Успешный";
                break;
            case 5:
                color = Color.green;
                text = "Богатый";
                break;
        }
        _scaleCondition.color = color;
        _scaleConditionText.color = color;
        _scaleConditionText.text = text;
    }

    private void OnDestroy()
    {
        GameManager.Instance.Bank.UpdateConditionUI -= SetConditionForUI;
        GameManager.Instance.Bank.ConditionLvl -= SetColor;
    }
}
