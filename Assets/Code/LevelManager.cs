using Cinemachine;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject cameraPrefab, playerPrefab;

    private int cameraID = -1, playerID = -1;
    private string cameraName = "Camera";
    private string playerName = "Player";

    public void CreateCameraAndPlayer(int playerCount)
    {
        if (cameraID < 0 || playerID < 0)
        {
            Create(playerCount);
            return;
        }

        GameObject cameraGO = GameObject.Find(cameraName);
        if (cameraGO == null)
        {
            CreateCamera();
        }
        else
        {
            cameraID = cameraGO.GetInstanceID();
        }

        GameObject playerGO = GameObject.Find(playerName);
        if (playerGO == null)
        {
            CreatePlayer(playerCount);
        }
        else
        {
            playerID = playerGO.GetInstanceID();
        }
    }

    private void Create(int playerCount)
    {
        CreateCamera();

        CreatePlayer(playerCount);
    }
    private void CreatePlayer(int playerCount)
    {
        GameObject playerGO = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        playerID = playerGO.GetInstanceID();
        playerGO.name = playerName;
        playerGO.GetComponent<PlayerCrowd>().StartQuanties = playerCount;

        GameObject cameraGO = GameObject.Find(cameraName);
        cameraGO.GetComponentInChildren<CinemachineVirtualCamera>().Follow = playerGO.transform;
    }
    private void CreateCamera()
    {
        GameObject cameraGO = Instantiate(cameraPrefab, Vector3.zero, Quaternion.identity);
        cameraID = cameraGO.GetInstanceID();
        cameraGO.name = cameraName;
    }
}
