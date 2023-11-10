using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyJumpMap : MonoBehaviour
{
    int player;

    [SerializeField] Transform TeleportLocation;
    [SerializeField] GameObject ClearUI;

    void Start()
    {
        player = LayerMask.NameToLayer("Player");
    }

    void Update()
    {
        CheckJumpClear();
    }

    /// <summary>
    /// 2023.1.1 / LJ
    /// 점프맵 클리어 했는지 검사
    /// </summary>
    void CheckJumpClear()
    {
        Collider[] cols = Physics.OverlapBox(new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z), transform.localScale, Quaternion.identity, 1 >> player);
        if (cols.Length != 0)
        {
            foreach (Collider collider in cols)
            {
                if (!collider.GetComponent<NetworkingObject>().isMine) // 사용자 일때
                    continue;
                collider.transform.position = TeleportLocation.position;
                // Canvas UI Active
                StartCoroutine(ClearJump());
            }
        }
    }

    /// <summary>
    /// 2023.1.1 / LJ
    /// 점프맵 클리어 UI 화면에 표시
    /// </summary>
    IEnumerator ClearJump()
    {
        ClearUI.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        ClearUI.SetActive(false);
    }
}
