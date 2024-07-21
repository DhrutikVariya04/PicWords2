using UnityEngine;
using UnityEngine.UI;

public class Playing : MonoBehaviour
{
    public Text titel, Levelcount;
    public Button[] buttons, ansButtons;
    public GameObject menu, play, winning, answerButtonfab, answerParent;
    public Image MainImage;
    public Sprite[] LevelImages;
    string answer, b = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    int[] I;
    int level;
    string[] rightanswer = { "CAT", "WOMAN", "RAIN", "HOT", "ORANGE" };

    public void OnEnable()
    {
        process();
        level = PlayerPrefs.GetInt("level");

        I = new int[answer.Length];
        Levelcount.text = "" + (level + 1);
        MainImage.sprite = LevelImages[level];

        for (int i = 0; i < answer.Length; i++)
        {
            Instantiate(answerButtonfab, answerParent.transform);
        }
        ansButtons = answerParent.GetComponentsInChildren<Button>();

        for (int i = 0; i < ansButtons.Length; i++)
        {
            int index = i;
            ansButtons[i].GetComponentInChildren<Text>().fontSize = 23;
            ansButtons[i].onClick.AddListener(() =>
             {
                 if (ansButtons[index].GetComponentInChildren<Text>().text != "")
                 {
                     string s = ansButtons[index].GetComponentInChildren<Text>().text;
                     buttons[I[index]].GetComponentInChildren<Text>().text = s;
                     ansButtons[index].GetComponentInChildren<Text>().text = "";
                 }
             });


        }
    }
    int j = 0;

    void process()
    {
        var randomAnswer = genrateAnswer();

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            char[] z = answer.ToCharArray();
            buttons[i].GetComponentInChildren<Text>().text = "" + randomAnswer[i];
            buttons[i].GetComponentInChildren<Text>().fontSize = 23;
            buttons[i].onClick.AddListener(() =>
            {
                for (int i = 0; i < answer.Length; i++)
                {
                    if (ansButtons[i].GetComponentInChildren<Text>().text == "")
                    {
                        string s = buttons[index].GetComponentInChildren<Text>().text;
                        ansButtons[i].GetComponentInChildren<Text>().text = s;
                        buttons[index].GetComponentInChildren<Text>().text = "";
                        I[i] = index;
                        checkWin();
                        break;
                    }
                }
            });

        }

        void checkWin()
        {
            for (int i = 0; i < ansButtons.Length; i++)
            {
                if (ansButtons[i].GetComponentInChildren<Text>().text != "" + answer[i])
                {
                    return;
                }
            }

            for (int i = 0; i < ansButtons.Length; i++)
            {
                var button = ansButtons[i];
                button.transform.parent = null;
                Destroy(button.gameObject);
            }
            j = 0;
            play.SetActive(false);
            winning.SetActive(true);

        }
    }

    private char[] genrateAnswer()
    {
        int OldLength = 12;
        answer = rightanswer[PlayerPrefs.GetInt("level")];
        var temp = answer;

        for (int i = temp.Length; i < 12; i++)
        {
            temp += b[Random.Range(0, b.Length)];
        }
        var arr = temp.ToCharArray();
        shuffle(arr);

        for (int i = 0; i < arr.Length; i++)
        {
            print(arr[i]);
        }

        return arr;
    }

    void shuffle(char[] texts)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < texts.Length; t++)
        {
            char tmp = texts[t];
            int r = Random.Range(t, texts.Length);
            texts[t] = texts[r];
            texts[r] = tmp;
        }


    }

    public void Back()
    {
        j = 0;

        for (int i = 0; i < ansButtons.Length; i++)
        {
            var button = ansButtons[i];
            button.transform.parent = null;
            Destroy(button.gameObject);
        }
        play.SetActive(false);
        menu.SetActive(true);
    }

}
