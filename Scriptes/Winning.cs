using UnityEngine;
using UnityEngine.UI;

public class Winning : MonoBehaviour
{
    public GameObject Play, winning;
    public Text text;
    int level;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Next()
    {
        PlayerPrefs.SetInt("level", level + 1);
        winning.SetActive(false);
        Play.SetActive(true);
    }

    private void OnEnable()
    {
        level = PlayerPrefs.GetInt("level");
        text.text = "Level "+(level + 1)+" Completed";
    }
}
