using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using XTools.UI;

public class MainCanvasController : BasePanel
{
    public TextMeshProUGUI scoreText;

    public Button btnSetting;
    public TextMeshProUGUI gameNameText;
    public GameObject leftInput;

    private void Start()
    {
        InvokeRepeating("UpdateScore", 0f, 1f);
        btnSetting.onClick.AddListener(GameSuspended);
        StartCoroutine(CloseGameName());
#if UNITY_ANDROID
                leftInput.SetActive(true);
#endif
    }

    private IEnumerator CloseGameName()
    {
        float a = 1f;
        while (a > 0)
        {
            a = gameNameText.color.a;
            a -= Time.deltaTime / 2f;
            gameNameText.color = new Color(255, 255, 255, a);
            yield return null;
        }

        gameNameText.SetActive(false);
    }

    private void UpdateScore()
    {
        scoreText.text = StaticModel.Score.ToString();
    }

    //游戏暂停
    public void GameSuspended()
    {
        UIManager.Instance.OpenPanel(UIManager.Instance.sceneMenuName);
    }
}