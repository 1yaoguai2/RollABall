using TMPro;
using UnityEngine;
using UnityEngine.UI;
using XTools.UI;

public class MenuCanvasController : MonoBehaviour
{
    public TextMeshProUGUI gameStatusText;
    public TextMeshProUGUI scoreText;
    public Button btnRest;
    public Button btnContinue;
    public Button btnGameSettings;
    public Button btnExit;

    [Header("游戏设置")] 
    public GameObject settingBg;
    public Button btnCloseSetting;
    public Toggle setFPS;
    private GetFPS _getFPS;

    private BasePanel _basePanel;

    private void Awake()
    {
        _basePanel = GetComponent<BasePanel>();
        _basePanel.openEvent += OnOpenPanel;
        _basePanel.closeEvent += OnClosePanel;
        _getFPS = GameObject.Find("GetFPS").GetComponent<GetFPS>();
    }

    void Start()
    {
        btnRest.onClick.AddListener(() =>
        {
            EventSoManager.Instance.raiseLevelEventSo.RaisedEvent();
        });
        btnContinue.onClick.AddListener(() =>
        {
            UIManager.Instance.ClosePanel(UIManager.Instance.sceneMenuName);
        });
        btnExit.onClick.AddListener(() =>
        {
            UIManager.Instance.Exit();
        });
        //设置界面
        btnGameSettings.onClick.AddListener(() =>{settingBg.SetActive(true);});
        btnCloseSetting.onClick.AddListener(() => {settingBg.SetActive(false);});
        setFPS.isOn = _getFPS.show;
        setFPS.onValueChanged.AddListener((value) =>
        {
            _getFPS.ShowFPSControl();
        });
    }


    //界面打开
    private void OnOpenPanel()
    {
        btnContinue.gameObject.SetActive(!StaticModel.IsGameOver);
        gameStatusText.text = StaticModel.IsGameOver ? "游戏结束" : "游戏暂停";
        scoreText.text = StaticModel.Score.ToString();
        Time.timeScale = 0;
    }
    //界面关闭
    private void OnClosePanel()
    {
        Time.timeScale = 1;
    }
}
