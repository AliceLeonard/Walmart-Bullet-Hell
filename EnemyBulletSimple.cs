using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class EnemyBulletSimple : EnemyBullet

    {
        private PlayableArea playableArea;
        public EnemyBulletSimple(float x, float y, PlayableArea playableArea) : base(x, y, playableArea)
        {
            this.x = x;
            this.y = y;
            this.playableArea = playableArea;
        }

        public override void Update()
        {
            x -= speed;
            if (x < 0 + 10 || x > playableArea.Width - 10 || y > playableArea.Height - 10 || y < 0 + 10)

            {
                LateRemove();
            }
        }

        public override void OnCollision(GameObject other)
        {
            base.OnCollision(other); // Call the base class's OnCollision method
        }
    }
}
