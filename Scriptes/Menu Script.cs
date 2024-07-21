using UnityEngine;
using UnityEngine.UI;

public class MenuScreapt : MonoBehaviour
{
    public GameObject menu, play;
    public Sprite[] LevelImages;
    public Image MainImages;
    public Text Levelcount;

    private void Start()
    {
        PlayerPrefs.DeleteAll();
    }

    public void Play()
    {
        menu.SetActive(false);
        play.SetActive(true);
    }

    private void OnEnable()
    {
        int level = PlayerPrefs.GetInt("level", 0);
        MainImages.sprite = LevelImages[level];
        Levelcount.text = "" + (level + 1);
    }

}
