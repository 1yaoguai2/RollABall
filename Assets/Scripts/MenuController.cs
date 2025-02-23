using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using XTools.UI;

public class MenuController : MonoBehaviour
{
    public GameSceneSO level1SceneSo;
    private SceneInstance _currentLevelScene;

    void Start()
    {
        LoadLevel(0);
        EventSoManager.Instance.raiseLevelEventSo.OnRaiseEvent += OnRaiseSceneEvent;
        EventSoManager.Instance.gameOverEventSo.OnRaiseEvent += OnGameOverEvent;
    }

    //重新开始游戏
    private void OnRaiseSceneEvent()
    {
        //B1UG:卸载场景失败 
        //currentScene.UnloadSceneAsync();
        var handle = Addressables.UnloadSceneAsync(_currentLevelScene);
        handle.Completed += (obj) =>
        {
            UIManager.Instance.ClosePanel(UIManager.Instance.sceneMenuName);
            LoadLevel(0);
        };
    }

    //游戏结束
    private void OnGameOverEvent()
    {
        UIManager.Instance.OpenPanel(UIManager.Instance.sceneMenuName);
    }


    //加载关卡
    private void LoadLevel(int index)
    {
        var handle = Addressables.LoadSceneAsync(level1SceneSo.sceneReference, LoadSceneMode.Additive);
        handle.Completed += (obj) => { _currentLevelScene = obj.Result; };
    }
}