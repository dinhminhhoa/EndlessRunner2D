using UnityEngine;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;
    }

    public float currentScore = 0f;

    public bool isPlaying = false;

    public UnityEvent onPlay = new UnityEvent();
    public UnityEvent onGameOver = new UnityEvent();

    public Data data;

    private void Start()
    {
        string loadedData = SaveSystem.Load("save");

        if (loadedData != null)
        {
            data = JsonUtility.FromJson<Data>(loadedData);
        }
        else
        {
            data = new Data();

        }
    }

    private void Update()
    {
        if (isPlaying)
            currentScore += Time.deltaTime;
    }

    public void StartGame()
    {
        onPlay.Invoke();
        isPlaying = true;
        currentScore = 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void GameOver()
    {
        if (data.highscore < currentScore)
        {
            data.highscore = currentScore;

            string saveString = JsonUtility.ToJson(data);

            SaveSystem.Save("save",saveString);
        }

        isPlaying = false;

        onGameOver.Invoke();
    }

    public string PrettyScore()
    {
        return Mathf.RoundToInt(currentScore).ToString();
    }

    public string PrettyHighscore()
    {
        return Mathf.RoundToInt(data.highscore).ToString();
    }
}
