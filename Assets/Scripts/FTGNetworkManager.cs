using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class FTGNetworkManager : NetworkManager
{
    [Header("Battle Scenes Setup")]
    [SerializeField] private int instances = 2;
    //currently hardcoded to 2.
    //when we will have menu scene, we will add number of instances based on the number of players in the game

    [Scene]
    [SerializeField] private string battleScene;

    private bool battleScenesLoaded = false;

    public List<Scene> battleScenes { get; } = new List<Scene>();

    int clientIndex;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        StartCoroutine(OnServerAddPlayerDelayed(conn));
    }

    IEnumerator OnServerAddPlayerDelayed(NetworkConnection conn)
    {
        // wait for server to async load all subscenes for game instances
        while (!battleScenesLoaded)
            yield return null;

        // Send Scene message to client to additively load the game scene
        conn.Send(new SceneMessage { sceneName = battleScene, sceneOperation = SceneOperation.LoadAdditive });

        // Wait for end of frame before adding the player to ensure Scene Message goes first
        yield return new WaitForEndOfFrame();

        base.OnServerAddPlayer(conn);

        conn.identity.GetComponent<Player>().Data.ID = clientIndex; //use this to determine whos using each scene
        clientIndex++;
    }

    public override void OnStartServer()
    {
        StartCoroutine(ServerLoadBattleScenes());
    }

    IEnumerator ServerLoadBattleScenes()
    {
        for (int index = 1; index <= instances; index++)
        {
            yield return SceneManager.LoadSceneAsync(battleScene, new LoadSceneParameters { loadSceneMode = LoadSceneMode.Additive });

            Scene newScene = SceneManager.GetSceneAt(index);
            battleScenes.Add(newScene);
        }

        battleScenesLoaded = true;
    }

    public override void OnStopServer()
    {
        NetworkServer.SendToAll(new SceneMessage { sceneName = battleScene, sceneOperation = SceneOperation.UnloadAdditive });
        StartCoroutine(ServerUnloadBattleScenes());
        clientIndex = 0;
    }

    // Unload the subScenes and unused assets and clear the subScenes list.
    IEnumerator ServerUnloadBattleScenes()
    {
        for (int index = 0; index < battleScenes.Count; index++)
            yield return SceneManager.UnloadSceneAsync(battleScenes[index]);

        battleScenes.Clear();
        battleScenesLoaded = false;

        yield return Resources.UnloadUnusedAssets();
    }
    public override void OnStopClient()
    {
        // make sure we're not in host mode
        if (mode == NetworkManagerMode.ClientOnly)
            StartCoroutine(ClientUnloadBattleScenes());
    }

    // Unload all but the active scene, which is the "container" scene
    IEnumerator ClientUnloadBattleScenes()
    {
        for (int index = 0; index < SceneManager.sceneCount; index++)
        {
            if (SceneManager.GetSceneAt(index) != SceneManager.GetActiveScene())
                yield return SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(index));
        }
    }
}
