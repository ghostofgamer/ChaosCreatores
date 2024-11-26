using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterChooseUI : MonoBehaviour
{
    public UpgradedCharacterParameter gunParameter;
    public int ID = 0;
    public Text price;
    public Image iconImg;
    int _price = 1;
    Button ownBtn;

    void Start()
    {
        ownBtn = GetComponent<Button>();

        if (!GameMode.Instance ||  GlobalValue.worldPlaying > 1 || gunParameter.unlockAtLevel <= GlobalValue.levelPlaying)
        {
            if (GameMode.Instance)
            {
                switch (gunParameter.gunType) {
                    case GunType.DualPistol:
                        _price = GameMode.Instance.dualPistolPrice;
                        break;
                    case GunType.Submachine:
                         _price = GameMode.Instance.submachineGunPrice;
                        break;
                    case GunType.Shotgun:
                        _price = GameMode.Instance.shotgunPrice;
                        break;
                    case GunType.Machinegun:
                        _price = GameMode.Instance.machinegunPrice;
                        break;
                }
            }

            price.text = "$" + _price;
        }
        else
        {
            iconImg.color = Color.black;
            price.text = "Unlock lv " + gunParameter.unlockAtLevel;
            // GetComponent<Button>().interactable = false;
            ownBtn.interactable = false;
        }
    }

    private void Update()
    {
        // ownBtn.interactable = GameManager.Instance.Player.upgradedCharacterID.gunType != gunParameter.gunType;
    }

    public void SetCharacter()
    {
        if (GlobalValue.SavedCoins >= _price)
        {
            GlobalValue.SavedCoins -= _price;
            CharacterManager.Instance.SetPlayer(gunParameter.gunType);
        }
        else
            SoundManager.PlaySfx(SoundManager.Instance.soundNotEnoughCoin);
    }
}
