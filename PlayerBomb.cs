using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
     class PlayerBomb: Sprite
    {
        float speed = 3;
        private PlayableArea playableArea;


        public PlayerBomb (float x, float y, PlayableArea playableArea) : base("bullet.png")
        {
            this.x = x; this.y = y;
            this.playableArea = playableArea;
        }

        public void Update()
        {
            x+=speed;

            if (x > playableArea.Width || x < 0)
            {
                LateRemove();
            }
        }
        void OnCollision(GameObject other)
        {
            if (other is Enemy)
            {
                LateRemove();
            }

        }


    }
}
