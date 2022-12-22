using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class PlayerStateManager : MonoBehaviour
{
    [Header("Player State")]
    private PlayerState state;
    public PlayerState State
    {
        get { return state; }
        set
        {
            switch (value)
            {
                case PlayerState.Idle:
                    Switching();
                    break;
                case PlayerState.Move:
                    PlayerMoved();
                    break;
                case PlayerState.AFK:
                    isAFK = true;
                    break;
            }
            state = value;
        }
    }

    [Header("AFK")]
    private float afkTimer = 0.0f;
    [SerializeField] [Range(0f,6000f)] private float SetTimer = 300f;
    [SerializeField] private bool isAFK = false;

    [Header("Player Animator")]
    [SerializeField] private Animator anim;

    [Header("Debugger")]
    public bool timerLog;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        if (State == PlayerState.Idle)
            Switching();
    }

    void Update()
    {
        Debug.Log($":: Player State -> {State}");
    }

    /// <summary>
    /// 2022.12.20 / LJ
    /// afk상태 검사 시작
    /// </summary>
    void Switching()
    {
        afkTimer = 0.0f;
        AFKTimer();
    }

    /// <summary>
    /// 2022.12.20 / LJ
    /// Idle 상태가 SetTimer만큼인지 확인하고 넘으면 AFK 상태로 전환
    /// </summary>
    void AFKTimer()
    {
        if (timerLog) Debug.Log($":: AFKTimer Start -> {afkTimer}s");
        if (State == PlayerState.Idle && !isAFK)
        {
            afkTimer += Time.deltaTime;
            if (afkTimer > SetTimer) // 시간이 넘었을 때
            {
                // 상태 전환
                State = PlayerState.AFK;
                // 잠수 전용 외형으로 변경 및 애니메이션 실행
                anim.SetBool("AFK", true);
                return;
            }
            Invoke("AFKTimer", Time.deltaTime);
            return;
        }
    }

    /// <summary>
    /// 2022.12.20 / LJ
    /// 플레이어의 상태가 Move로 바꼈을때 실행 되는 메서드
    /// </summary>
    void PlayerMoved()
    {
        isAFK = false;
        // 외형 변환 및 애니메이션 실행
        anim.SetBool("AFK", false);
    }
}
