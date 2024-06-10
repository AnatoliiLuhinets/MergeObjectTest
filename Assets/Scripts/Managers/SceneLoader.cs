using Common;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class SceneLoader
    {
        public async UniTask LoadScene(string sceneName)
        {
            SceneManager.LoadSceneAsync(Consts.Scenes.LoadingScene, LoadSceneMode.Single);
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            
            await UniTask.WaitWhile(() => !operation.isDone);
            
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        }
    }
}
