using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class MapControllerUI : MonoBehaviour {
//	public Transform BlockLevel;

    public GameObject[] objects; // Массив объектов для переключения
    private int currentIndex = 0;
    public Image[] pageImages;
    public Sprite _currentSprite;
    public Sprite _notCurrentSprite;
    public Text[] text;
    public Color activeColor = Color.yellow; // Цвет активной страницы
    public Color inactiveColor = Color.gray;
    
	public RectTransform BlockLevel;
	public int howManyBlocks = 3;

	public float step = 720f;
    
	private float newPosX = 0;
    public Text worldTxt;
	int currentPos = 0;
	public AudioClip music;
	// Use this for initialization
	void Start () {
        //SetDots();
        SetWorldNumber();
        
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(i == currentIndex);
        }
        UpdatePageColors();
    }

    void SetWorldNumber()
    {
        worldTxt.text = currentPos + 1 + "/" + howManyBlocks;
    }
    //void SetDots()
    //{
    //    foreach(var obj in Dots)
    //    {
    //        obj.color = new Color(1, 1, 1, 0.5f);
    //        obj.rectTransform.sizeDelta = new Vector2(28, 28);
    //    }

    //    Dots[currentPos].color = Color.yellow;
    //    Dots[currentPos].rectTransform.sizeDelta = new Vector2(38, 38);
    //}

    void OnEnable(){
		SoundManager.PlayMusic (music);
		Debug.LogWarning ("ON ENALBE");

	}

	void OnDisable(){
		SoundManager.PlayMusic (SoundManager.Instance.musicsGame);
	}

    public void SetCurrentWorld(int world)
    {
        currentPos += (world - 1);

        newPosX -= step * (world - 1);
        newPosX = Mathf.Clamp(newPosX, -step * (howManyBlocks - 1), 0);

        SetMapPosition();

        SetWorldNumber();
    }

    public void SetMapPosition()
    {
        BlockLevel.anchoredPosition = new Vector2(newPosX, BlockLevel.anchoredPosition.y);
    }

    bool allowPressButton = true;
    public void Next()
    { 
             objects[currentIndex].SetActive(false);
             currentIndex = (currentIndex + 1) % objects.Length;
             objects[currentIndex].SetActive(true);
             UpdatePageColors();
        /*if (allowPressButton)
        {
            StartCoroutine(NextCo());
        }*/
    }
    
    private void UpdatePageColors()
    {
        // Обновляем цвета изображений в зависимости от текущего индекса
        for (int i = 0; i < pageImages.Length; i++)
        {
            // pageImages[i].color = (i == currentIndex) ? activeColor : inactiveColor;
            pageImages[i].sprite = (i == currentIndex) ? _currentSprite : _notCurrentSprite;
        }
        for (int i = 0; i < pageImages.Length; i++)
        {
            text[i].GetComponent<Outline>().enabled = (i != currentIndex);
        }
        
    }
    
    IEnumerator NextCo()
    {
        
       
        
        
        
        allowPressButton = false;

        SoundManager.Click();

        if (newPosX != (-step * (howManyBlocks - 1)))
        {
            currentPos++;

            newPosX -= step;
            newPosX = Mathf.Clamp(newPosX, -step * (howManyBlocks - 1), 0);
            
        }
        else
        {
            allowPressButton = true;
            yield break;

            //currentPos = 0;

            //newPosX = 0;
            //newPosX = Mathf.Clamp(newPosX, -step * (howManyBlocks - 1), 0);


        }

        BlackScreenUI.instance.Show(0.15f);

        yield return new WaitForSeconds(0.15f);
        SetMapPosition();
        BlackScreenUI.instance.Hide(0.15f);

        SetWorldNumber();


        allowPressButton = true;

    }

    public void Pre()
    {
        objects[currentIndex].SetActive(false);
        currentIndex = (currentIndex - 1 + objects.Length) % objects.Length;
        objects[currentIndex].SetActive(true);
        
        UpdatePageColors();
        /*if (allowPressButton)
        {
            StartCoroutine(PreCo());
        }*/
    }

    IEnumerator PreCo()
    {
        allowPressButton = false;
        SoundManager.Click();
        if (newPosX != 0)
        {
            currentPos--;

            newPosX += step;
            newPosX = Mathf.Clamp(newPosX, -step * (howManyBlocks - 1), 0);


        }
        else
        {
            allowPressButton = true;
            yield break;
            //currentPos = howManyBlocks - 1;

            //newPosX = -999999;
            //newPosX = Mathf.Clamp(newPosX, -step * (howManyBlocks - 1), 0);

        }

        BlackScreenUI.instance.Show(0.15f);

        yield return new WaitForSeconds(0.15f);
        SetMapPosition();
        BlackScreenUI.instance.Hide(0.15f);

        SetWorldNumber();


        allowPressButton = true;

    }

	public void UnlockAllLevels(){
		GlobalValue.LevelPass = (GlobalValue.LevelPass + 1000);
		UnityEngine.SceneManagement.SceneManager.LoadScene (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().buildIndex);
		SoundManager.Click ();
	}
}
