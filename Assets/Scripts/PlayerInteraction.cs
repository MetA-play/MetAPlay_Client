using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] [Range(0f, 20f)] private float interactionRadius = 5f;

    [SerializeField] private int layerBench;

    Vector3 pivot;

    [SerializeField] Transform closeTrans;

    void Start()
    {
        layerBench = LayerMask.NameToLayer("Bench");    
    }

    void Update()
    {
        pivot = new Vector3(transform.position.x, 0, transform.position.z);
        CheckBench();
        interacter();
    }

    /// <summary>
    /// 사정거리 안에 상호작용 가능한 오브젝트가 있고 특정 키를 누르면 상호작용을 실행
    /// </summary>
    void interacter()
    {
        if (Input.GetKeyUp(KeyCode.F) && closeTrans != null)
        {
            closeTrans.GetComponent<LobbyObject>().interaction.Invoke(transform);
        }
    }

    /// <summary>
    /// 2022.12.29 / LJ
    /// 주위에 벤치가 있는지 체크
    /// </summary>
    void CheckBench()
    {
        Collider[] benches = Physics.OverlapSphere(transform.position, interactionRadius, 1 << layerBench);
        Debug.Log(benches.Length);
        if (benches.Length > 0)
        {
            closeTrans = ClosetObject(benches);
        }
        else
            closeTrans = null;
    }

    /// <summary>
    /// 2022.12.29 / LJ
    /// 주변에 가장 가까운 오브젝트를 반환
    /// </summary>
    Transform ClosetObject(Collider[] objs)
    {
        Transform close = null;
        float closetDis = float.PositiveInfinity;
        foreach (Collider obj in objs)
        {
            Vector3 offset = pivot - obj.transform.position;
            if (offset.sqrMagnitude < closetDis * closetDis && obj.transform == closeTrans)
            {
                continue;
            }
            close = obj.transform;
        }

        return close;
    }
}
