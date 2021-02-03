using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp
{
    enum CarType
    {
        None,
        NA,
        Turbo,
        Super
    }

    class Car
    {
        protected int horsePower;
        protected int durability;

        public int GetHorsePower() { return horsePower; }
        public int GetDurability() { return durability; }
        public virtual void SetDurability(int value)
        {
            durability -= value;
        }
    }

    class NA : Car
    {
        public NA()
        {
            horsePower = 120;
            durability = 100;
        }
    }

    class Turbo : Car
    {
        public Turbo()
        {
            horsePower = 200;
            durability = 90;
        }

        public override void SetDurability(int value)
        {
            durability -= value * 2;
        }
    }

    class Super : Car
    {
        public Super()
        {
            horsePower = 400;
            durability = 75;
        }

        public override void SetDurability(int value)
        {
            durability -= value * 6;
        }
    }
}
