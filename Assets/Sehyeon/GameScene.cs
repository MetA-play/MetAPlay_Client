using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf.Protocol;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    public Button StartButton;

    [SerializeField] TMP_Text winnerUI;
    private bool isInit = false;
    
    public void Init()
    {
        if (isInit) return;
        isInit = true;

        if (ObjectManager.Instance.PlayerCount() > 1)
        {
            StartButton.gameObject.SetActive(false);
        }
    }

    public void GameStart()
    {
        StartButton.interactable = false;
        StartButton.gameObject.SetActive(false);

        // 게임 시작 패킷 전송
        C_UpdateGameStateReq gameStatePacket = new C_UpdateGameStateReq();
        gameStatePacket.State = GameState.Playing;
        NetworkManager.Instance.Send(gameStatePacket);
    }

    public void OnGameEnd(string winner)
    {
        winnerUI.text = $"{winner} 승리!";
        winnerUI.gameObject.SetActive(true);
    }

    private IEnumerator GoLobby()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Lobby");
    }
}
