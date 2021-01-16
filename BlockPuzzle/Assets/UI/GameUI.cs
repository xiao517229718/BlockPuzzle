using BlockPuzzle;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameUI : MonoBehaviour
{

    public delegate void GameOverDelegate();
    public static GameOverDelegate gameOverEvent;

    public Image gameoverImg;
    public Image gameStartImg;
    public Image gameSaveImg;
    public Button gameStartBtn;
    public Button gameContinueBtn;
    public Button gameOverBtn;
    public Button gameStateBtn;
    public Button gameExitBtn;
    public Button gameSureBtn;
    public Button gameCanelBtn;
    public Button gameBackBtn;
    public Text gameState;
    public Text gameTxt;

    private void Awake()
    {
        gameStartBtn.onClick.AddListener(delegate { OnStartClick(); });
        gameOverBtn.onClick.AddListener(delegate { OnGameOverClick(); });
        gameStateBtn.onClick.AddListener(delegate { OnStateClick(); });
        gameContinueBtn.onClick.AddListener(delegate { OnContinueClick(); });
        gameExitBtn.onClick.AddListener(delegate { OnExitClick(); });
        gameSureBtn.onClick.AddListener(delegate { OnSureClick(); });
        gameCanelBtn.onClick.AddListener(delegate { OnCanelClick(); });
        gameBackBtn.onClick.AddListener(delegate { OnBackClick(); });
    }
    private void OnEnable()
    {
        gameOverEvent += OnGameOver;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameStartImg.rectTransform.DOScale(new Vector3(1f, 1f, 1f), 0.03f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnStartClick()
    {

        GameController.Instance.NewGame();
        gameStartImg.rectTransform.DOScale(new Vector3(0, 0, 0), 0.03f);
        // GameController.Instance.StartGame();
        gameState.text = "游戏中";

    }

    private void OnContinueClick()
    {
        //GameController.Instance.ClearScene();
        gameStartImg.rectTransform.DOScale(new Vector3(0, 0, 0), 0.03f);
        GameController.Instance.RePlay();
        gameState.text = "游戏中";

    }

    private void OnStateClick()
    {
        if (gameState.text.Equals("游戏中"))
        {
            gameState.text = "暂停";
            GameController.pause = true;
        }
        else
        {
            gameState.text = "游戏中";
            GameController.pause = false;
        }
    }

    private void OnExitClick()
    {
        Application.Quit();
    }

    public void OnSureClick()
    {
        gameSaveImg.rectTransform.DOScale(new Vector3(0, 0, 0), 0.03f);
        gameStartImg.rectTransform.DOScale(new Vector3(1f, 1f, 1f), 0.03f);
        //GameController.Instance.ClearScene();
        //TODO游戏保存
        MapHelper.SaveMap();

    }

    public void OnCanelClick()
    {
        gameSaveImg.rectTransform.DOScale(new Vector3(0, 0, 0), 0.03f);
        gameStartImg.rectTransform.DOScale(new Vector3(1f, 1f, 1f), 0.03f);
        GameController.Instance.ClearScene();
        //清楚游戏
        MapController.Instance.AllToOneFill(0);

    }

    public void OnBackClick()
    {
        gameSaveImg.rectTransform.DOScale(new Vector3(1f, 1f, 1f), 0.03f);
    }

    public void OnGameOverClick()
    {
        gameoverImg.rectTransform.DOScale(new Vector3(0, 0, 0), 0.03f);
        gameStartImg.rectTransform.DOScale(new Vector3(1f, 1f, 1f), 0.03f);
        GameController.Instance.ClearScene();
        //清空map
    }

    public void OnGameOver()
    {
        gameoverImg.rectTransform.DOScale(new Vector3(1f, 1f, 1f), 0.03f);
        GameController.creatNext = false;
    }

    private void OnDisable()
    {
        gameOverEvent -= OnGameOver;
    }

}
