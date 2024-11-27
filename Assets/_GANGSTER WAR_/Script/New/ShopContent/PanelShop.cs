using UnityEngine;
using UnityEngine.UI;

public class PanelShop : MonoBehaviour
{
    [SerializeField] private Image _gunImageButton;
    [SerializeField] private Image _defImageButton;
    [SerializeField] private Sprite _active;
    [SerializeField] private Sprite _deactive;
    [SerializeField] private int _index;
    [SerializeField] private bool _isGunPanel;

    private void Start()
    {
        if (_index == 1)
            Open();
        else
            Close();
    }

    public void Open()
    {
        gameObject.SetActive(true);
        ChangeValue();
    }

    public void Close()
    {
        gameObject.SetActive(false);
        ChangeValue();
    }

    private void ChangeValue()
    {
        if (_isGunPanel)
        {
            _gunImageButton.sprite = _active;
            _defImageButton.sprite = _deactive;
        }
        else
        {
            _gunImageButton.sprite = _deactive;
            _defImageButton.sprite = _active;
        }
    }
}
