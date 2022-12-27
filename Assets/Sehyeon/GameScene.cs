using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf.Protocol;

public class GameScene : MonoBehaviour
{
    [ContextMenu("StartGame")]
    public void StartGame()
    {
        C_UpdateGameStateReq gameStatePacket = new C_UpdateGameStateReq();
        gameStatePacket.State = GameState.Playing;

        NetworkManager.Instance.Send(gameStatePacket);
    }

    [ContextMenu("EndGame")]
    public void EndGame()
    {
        C_UpdateGameStateReq gameStatePacket = new C_UpdateGameStateReq();
        gameStatePacket.State = GameState.Ending;

        NetworkManager.Instance.Send(gameStatePacket);
    }
}
