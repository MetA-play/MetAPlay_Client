namespace Define
{
    public enum PlayerState
    {
        Idle,
        Move,
        AFK
    }
}

namespace Game
{
    public enum GameKinds
    {
        Log,
        Spleef,
        Runner,



        Preparing
    }
}

namespace Customization
{
    public enum Part
    {
        Head,
        Body,
        Leg
    }

    public enum Head
    {
        선택안함,
        요리사모자,
        신사모자,
        인디언머리띠,
        칼
    }

    public enum Body
    {
        선택안함,
        망토
    }

    public enum Leg
    {
        선택안함,
        오리발
    }
}