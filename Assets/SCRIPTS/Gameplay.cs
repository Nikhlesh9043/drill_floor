using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gameplay : MonoBehaviour
{
    private static Gameplay instance;
    [SerializeField] GameObject player;
    [SerializeField] GameObject StartMenu;
    [SerializeField] GameObject LevelOver;
    [SerializeField] GameObject WinScreen;
    public TextMeshProUGUI coinText;


    private Rigidbody player_rigidbody;
    private PlayerController player_controller_script;

    public Animator camera_animator;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1.5f;
    }

    public static Gameplay GetInstance()
    {
        return instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        //GameValues.CurrentDisplayLevel = 19;
        GameValues.CurrentDisplayLevel = PlayerPrefs.GetInt("CurrentLevel", 1);

        GameValues.CoinCount = PlayerPrefs.GetInt("CoinCount", 0);
        coinText.text = GameValues.CoinCount.ToString();

        player_rigidbody = player.GetComponent<Rigidbody>();
        player_controller_script = player.GetComponent<PlayerController>();

        //LevelCreation.GetInstance().SetupLevel(GameValues.CurrentLevel);
        ResetLevel();
        player_controller_script.ResetPlayer();
        LevelOver.SetActive(false);
    }

    public void StartButtonPress()
    {
        LevelCreation.GetInstance().SpawnEnemies();
        player_rigidbody.isKinematic = false;
        StartMenu.SetActive(false);

        player_controller_script.PlayDrillAnimation();
        GameValues.GameOver = false;
        camera_animator.Play("Player");
    }

    public void GameOver(bool win)
    {
        GameValues.GameOver = true;
        GameValues.Win = win;
        WinScreen.SetActive(win);

        if (win)
        {
            player.GetComponent<CapsuleCollider>().isTrigger = true;
            Invoke("DelayGameOver", 8f);
            camera_animator.Play("Car");
            GameValues.CurrentDisplayLevel += 1;
            PlayerPrefs.SetInt("CurrentLevel", GameValues.CurrentDisplayLevel);

            EmojiController.GetInstance().HideEmoji();
        }
        else
        {
            LevelOver.SetActive(true);
            player_rigidbody.isKinematic = true;

            EmojiController.GetInstance().ShowEmoji(0);
        }
        player_controller_script.StopDrillAnimation();
    }

    void DelayGameOver()
    {
        LevelOver.SetActive(true);
        player.GetComponent<Rigidbody>().isKinematic = true;
        //player.SetActive(false);
    }

    public void ContinueButtonPress()
    {
        StartMenu.SetActive(true);
        LevelOver.SetActive(false);
        ResetLevel();

    }

    public void ResetLevel()
    {
        GameValues.Win = false;
        LevelCreation.GetInstance().ResetLevel();
        LevelCreation.GetInstance().SetupLevel(GameValues.CurrentLevel);

        player_controller_script.ResetPlayer();
        camera_animator.Play("Enemy");

        EmojiController.GetInstance().ResetEmoji();
    }

}
