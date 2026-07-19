using System;
using Microsoft.Xna.Framework.Audio;

namespace Mafia
{
    public class MafiaBufferContainer
    {
        public SoundEffect Jump;
        public SoundEffect Tiun;
        public SoundEffect Coin;
        public SoundEffect Switch;
        public SoundEffect BoxMove;
        public SoundEffect BoxFall;
        public SoundEffect Lift;
        public SoundEffect Spring;
        public SoundEffect Fall;
        public SoundEffect Ue;
        public SoundEffect Hyakuyen;
        public SoundEffect Stiana;

        public MafiaBufferContainer(MafiaApplication app)
        {
            Jump = app.Content.Load<SoundEffect>("jump");
            Tiun = app.Content.Load<SoundEffect>("tiun");
            Coin = app.Content.Load<SoundEffect>("coin");
            Switch = app.Content.Load<SoundEffect>("switch");
            BoxMove = app.Content.Load<SoundEffect>("boxmove");
            BoxFall = app.Content.Load<SoundEffect>("boxfall");
            Lift = app.Content.Load<SoundEffect>("lift");
            Spring = app.Content.Load<SoundEffect>("spring");
            Fall = app.Content.Load<SoundEffect>("fall");
            Ue = app.Content.Load<SoundEffect>("ue");
            Hyakuyen = app.Content.Load<SoundEffect>("hyakuyen");
            Stiana = app.Content.Load<SoundEffect>("stiana");
        }

        public void Dispose()
        {
            if (Jump != null)
            {
                Jump.Dispose();
                Jump = null;
            }

            if (Tiun != null)
            {
                Tiun.Dispose();
                Tiun = null;
            }

            if (Coin != null)
            {
                Coin.Dispose();
                Coin = null;
            }

            if (Switch != null)
            {
                Switch.Dispose();
                Switch = null;
            }

            if (BoxMove != null)
            {
                BoxMove.Dispose();
                BoxMove = null;
            }

            if (BoxFall != null)
            {
                BoxFall.Dispose();
                BoxFall = null;
            }

            if (Lift != null)
            {
                Lift.Dispose();
                Lift = null;
            }

            if (Spring != null)
            {
                Spring.Dispose();
                Spring = null;
            }

            if (Fall != null)
            {
                Fall.Dispose();
                Fall = null;
            }

            if (Ue != null)
            {
                Ue.Dispose();
                Ue = null;
            }

            if (Hyakuyen != null)
            {
                Hyakuyen.Dispose();
                Hyakuyen = null;
            }

            if (Stiana != null)
            {
                Stiana.Dispose();
                Stiana = null;
            }
        }
    }
}
