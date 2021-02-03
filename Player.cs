using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp
{
    enum PlayerType
    {
        None,
        User,
        AI
    }
    class Player
    {
        public Car car;
        protected PlayerType type;
        protected int speed;
        protected float distance;
        protected short crashCnt;

        protected Player()
        {

        }

        public void SetCar(CarType carType)
        {
            switch (carType)
            {
                case CarType.NA:
                    car = new NA();
                    speed = car.GetHorsePower();
                    break;
                case CarType.Turbo:
                    car = new Turbo();
                    speed = car.GetHorsePower();
                    break;
                case CarType.Super:
                    car = new Super();
                    speed = car.GetHorsePower();
                    break;
                default:
                    Console.WriteLine("뚜벅이 인생");
                    break;
            }
        }
        public int GetSpeed() { return speed; }
        public void SetDistance(float distance) { this.distance += distance; }
        public float GetDistance() { return distance; }
        public void Mistake(int value)
        {
            Console.WriteLine("대충 박는 모습");
            crashCnt++;
            car.SetDurability(value);
        }
        public short GetCrashCnt() { return crashCnt; }
    }

    class User : Player
    {
        public User(PlayerType type)
        {
            this.type = type;
        }
    }

    class AI : Player
    {
        public AI(PlayerType type)
        {
            this.type = type;
            SetRandomCar();
        }

        private void SetRandomCar()
        {
            Random rand = new Random();
            int randNum = rand.Next(1, 4);
            switch (randNum)
            {
                case 1:
                    SetCar(CarType.NA);
                    break;
                case 2:
                    SetCar(CarType.Turbo);
                    break;
                case 3:
                    SetCar(CarType.Super);
                    break;
                default:
                    Console.WriteLine("AI가 기권하였습니다!"); // 이 옵션을 사용하기 위해선 Game.cs에서 StartRace()함수가 호출되기 전에 경기가 취소될 수 있도록 작성할 것
                    break;
            }
        }
    }
}
