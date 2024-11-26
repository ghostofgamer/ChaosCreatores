﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuHomeScene : MonoBehaviour {
	public static MainMenuHomeScene Instance;
    public GameObject StartUI;
    public GameObject MenuScene;
	public GameObject MapUI;
	public GameObject Loading;
    public GameObject Settings;
    public GameObject TestOption;
	public string facebookLink;
    public string twitterLink = "https://twitter.com/";

    public Text[] coinTxt;

    [Header("Sound and Music")]
    public Image soundImage;
    public Image musicImage;
    public Sprite soundImageOn, soundImageOff, musicImageOn, musicImageOff;

    void Awake(){
		Instance = this;
        StartUI.SetActive(true);
        MenuScene.SetActive(false);
        if (Loading != null)
			Loading.SetActive (false);
		if (MapUI != null)
            MapUI.SetActive (false);
        if (Settings)
            Settings.SetActive(false);

        if (GameMode.Instance)
            TestOption.SetActive(GameMode.Instance.showTestOption);
    }

    public void OpenMenuScene()
    {
        SoundManager.Click();
        StartUI.SetActive(false);
        MenuScene.SetActive(true);
    }

    public void LoadScene(){
		if (Loading != null)
			Loading.SetActive (true);
        
        StartCoroutine(LoadAsynchronously("Playing"));
    }

    public void LoadScene(string sceneNamage)
    {
        if (Loading != null)
            Loading.SetActive(true);

        StartCoroutine(LoadAsynchronously(sceneNamage));
    }
    
	IEnumerator Start () {
		CheckSoundMusic();
        if (GlobalValue.isFirstOpenMainMenu)
        {
            GlobalValue.isFirstOpenMainMenu = false;
            SoundManager.Instance.PauseMusic(true);
            SoundManager.PlaySfx(SoundManager.Instance.beginSoundInMainMenu);
            yield return new WaitForSeconds(SoundManager.Instance.beginSoundInMainMenu.length);
            SoundManager.Instance.PauseMusic(false);
            SoundManager.PlayMusic(SoundManager.Instance.musicsGame);
        }
    }

    void Update() {
        CheckSoundMusic();

        foreach (var ct in coinTxt)
        {
            ct.text = GlobalValue.SavedCoins + "";
        }
	}

	public void OpenMap(bool open){
        SoundManager.Click();
        StartCoroutine(OpenMapCo(open));
	}

    IEnumerator OpenMapCo(bool open)
    {
        yield return null;
        BlackScreenUI.instance.Show(0.2f);
        MapUI.SetActive(open);
        BlackScreenUI.instance.Hide(0.2f);
    }

	public void Facebook(){
        SoundManager.Click();
		Application.OpenURL (facebookLink);
	}

    public void Twitter()
    {
        SoundManager.Click();
        Application.OpenURL(twitterLink);
    }

    public void ExitGame()
    {
        SoundManager.Click();
        Application.Quit();
    }

    public void Setting(bool open)
    {
        SoundManager.Click();
        Settings.SetActive(open);
    }

    #region Music and Sound
    public void TurnSound()
    {
        GlobalValue.isSound = !GlobalValue.isSound;
        soundImage.sprite = GlobalValue.isSound ? soundImageOn : soundImageOff;
PlayerPrefs.SetInt("Sound", GlobalValue.isSound ? 1 : 0);
        SoundManager.SoundVolume = GlobalValue.isSound ? 1 : 0;
    }

    public void TurnMusic()
    {
        GlobalValue.isMusic = !GlobalValue.isMusic;
        musicImage.sprite = GlobalValue.isMusic ? musicImageOn : musicImageOff;
        PlayerPrefs.SetInt("Music", GlobalValue.isMusic ? 1 : 0);
        SoundManager.MusicVolume = GlobalValue.isMusic ? SoundManager.Instance.musicsGameVolume : 0;
    }
    #endregion

    private void CheckSoundMusic()
    {
        int soundValue = PlayerPrefs.GetInt("Sound", 1);
        int musicValue = PlayerPrefs.GetInt("Music", 1);
        soundImage.sprite = soundValue==1 ? soundImageOn : soundImageOff;
        musicImage.sprite = musicValue==1 ? musicImageOn : musicImageOff;
        
        /*SoundManager.SoundVolume = soundValue==1 ? 1 : 0;
        SoundManager.MusicVolume = musicValue==1 ? SoundManager.Instance.musicsGameVolume : 0;*/
        /*soundImage.sprite = GlobalValue.isSound ? soundImageOn : soundImageOff;
        musicImage.sprite = GlobalValue.isMusic ? musicImageOn : musicImageOff;*/
        
        GlobalValue.isSound = soundValue==1? true : false;
        GlobalValue.isMusic = musicValue==1? true : false;
        
        SoundManager.SoundVolume = GlobalValue.isSound ? 1 : 0;
        SoundManager.MusicVolume = GlobalValue.isMusic ? SoundManager.Instance.musicsGameVolume : 0;
    }

    public void Tutorial(){
		SoundManager.Click ();
		SceneManager.LoadScene ("Tutorial");
	}

    public Slider slider;
    public Text progressText;
    IEnumerator LoadAsynchronously(string name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            progressText.text = (int)progress * 100f + "%";
            yield return null;
        }
    }

    public void ResetData()
    {
        if (GameMode.Instance)
            GameMode.Instance.ResetDATA();
    }

    public void SetMaxCoin()
    {
        GlobalValue.SavedCoins = 99999;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void UnlockAll()
    {
        GlobalValue.LevelPass = 99999;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);

    }
}
