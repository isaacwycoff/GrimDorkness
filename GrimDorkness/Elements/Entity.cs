using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace GrimDorkness
{
    // flags for different entity states
    [Flags]
    public enum EntityState
    {
        Attacking = 0x01,               // currently attacking
        Dying = 0x02,                   // dying
        Invulnerable = 0x04,            
        Farting = 0x08
    }

    public enum Team
    {
        Player = 1,
        Enemy = 2,
        PowerUp = 3
    }

    public enum Direction
    {
        South = 0,
        SouthEast = 1,
        East = 2,
        NorthEast = 3,
        North = 4,
        NorthWest = 5,
        West = 6,
        SouthWest = 7

    }

    class Entity: Element
    {
        // common variables for all entities:
        protected int health;
        protected int maxHealth;
        protected int damage;
        protected int speed;
        protected EntityState state;

        protected Team team;


        public Team GetTeam()
        {
            return team;
        }

        public override bool isVisible()
        {
            if (health <= 0) return false;          // TODO: instead make a DEAD flag

            return base.isVisible();
        }

        virtual public Boolean TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                health = 0;
                return false;
            }
            return true;


        }

        virtual public bool Collision()
        {
            if (markedForDeath || health <= 0) return false;

            return true;
        }



    }
}
