using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp
{
    enum GameType
    {
        None,
        HvsH,
        HvsAI
    }
    enum RaceFlag
    {
        None,
        Win,
        Lose,
        DistanceWin,
        DurabiltyWin,
        DistanceLose,
        DurabiltyLose,
    }

    class Game
    {
        GameType type;
        CarType carType;
        Player player = null;
        Player ai = null;

        public void EnterGame()
        {
            Console.Clear();
            Console.WriteLine("-- 자동차 레이스 --");
            Console.WriteLine("게임을 선택해주세요.");
            Console.WriteLine("[1] AI 대결");
            Console.WriteLine("[2] 월드매치 (업데이트 예정)");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Console.WriteLine("AI 대결 세팅 중입니다.");
                    type = GameType.HvsAI;
                    SelectCar();
                    break;
                case "2":
                    Console.WriteLine("안돼 돌아가");
                    break;
                default:
                    break;
            }

            SetRace(type);
        }

        public void SelectCar()
        {
            Console.WriteLine("사용할 자동차를 선택해주세요.");
            Console.WriteLine("[1] 자연흡기 120 마력 (실수 가중치 0)");
            Console.WriteLine("[2] 터보차져 200 마력 (실수 가중치 2)");
            Console.WriteLine("[3] 슈퍼차져 400 마력 (실수 가중치 5)");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    carType = CarType.NA;
                    break;
                case "2":
                    carType = CarType.Turbo;
                    break;
                case "3":
                    carType = CarType.Super;
                    break;
                default:
                    break;
            }
        }

        public void SetRace(GameType type)
        {
            switch (type)
            {
                case GameType.HvsAI:
                    player = new User(PlayerType.User);
                    player.SetCar(carType);
                    ai = new AI(PlayerType.AI);
                    StartRace();
                    break;
                case GameType.HvsH:
                    // 대충 네트워크 연결
                    break;
                default:
                    break;
            }
        }

        public void StartRace()
        {
            Console.Clear();
            Console.WriteLine("경기 시작!");
            while (true)
            {
                Console.ReadLine(); // 누를 때마다 진행

                Random rand = new Random();
                int mistakeRate = rand.Next(0, 100);
                int damage = rand.Next(1, 5);
                float distance = 0;
                if (mistakeRate <= 25) // 플레이어턴
                {
                    Console.Write("!! 플레이어가 ");
                    player.Mistake(damage);
                }
                else
                {
                    distance = player.GetSpeed() * 0.2f;
                    player.SetDistance(distance);
                }

                mistakeRate = rand.Next(0, 100);
                damage = rand.Next(1, 5);
                if (mistakeRate <= 33) // 인공지능턴
                {
                    Console.Write("!! AI가 ");
                    ai.Mistake(damage);
                }
                else
                {
                    distance = ai.GetSpeed() * 0.2f;
                    ai.SetDistance(distance);
                }

                RaceFlag flag = JudgeRace();
                if (flag == RaceFlag.DistanceWin || flag == RaceFlag.DistanceLose || flag == RaceFlag.DurabiltyWin || flag == RaceFlag.DurabiltyLose)
                {
                    EndRace(flag);
                    Console.ReadLine();
                    break;
                }
            }
        }

        public RaceFlag JudgeRace()
        {
            RaceFlag flag = RaceFlag.None;
            float distance = player.GetDistance();
            int durability = player.car.GetDurability();
            Console.WriteLine($"플레이어 현재 거리 {distance}m, 남은 내구도 {durability}");
            if(distance > 1000f)
            {
                Console.WriteLine("플레이어가 승리했습니다!");
                flag = RaceFlag.DistanceWin;
            }
            else if(durability < 0)
            {
                Console.WriteLine("플레이어가 패배했습니다!");
                flag = RaceFlag.DurabiltyLose;
            }

            distance = ai.GetDistance();
            durability = ai.car.GetDurability();
            Console.WriteLine($"AI 현재 거리 {distance}m, 남은 내구도 {durability}");
            if (distance > 1000f)
            {
                Console.WriteLine("AI가 승리했습니다!");
                flag = RaceFlag.DistanceLose;
            }
            else if (durability < 0)
            {
                Console.WriteLine("AI가 패배했습니다!");
                flag = RaceFlag.DurabiltyWin;
            }

            return flag;
        }

        public void EndRace(RaceFlag flag)
        {
            Console.WriteLine();
            Console.WriteLine("경기 종료");
            float distance = player.GetDistance();
            int durability = player.car.GetDurability();
            if (flag == RaceFlag.DistanceWin)
            {
                distance = distance - ai.GetDistance();
                Console.WriteLine($"플레이어가 {distance}m 만큼 앞섬");
            }
            else if (flag == RaceFlag.DurabiltyWin)
            {
                durability = durability - ai.car.GetDurability();
                Console.WriteLine($"플레이어가 {durability} 내구도 앞섬");
            }
            else if (flag == RaceFlag.DistanceLose)
            {
                distance = ai.GetDistance() - distance;
                Console.WriteLine($"AI가 {distance}m 만큼 앞섬");
            }
            else if (flag == RaceFlag.DurabiltyLose)
            {
                durability = ai.car.GetDurability() - durability;
                Console.WriteLine($"AI가 {durability} 내구도 앞섬");
            }
            Console.WriteLine($"플레이어 충돌 수 : {player.GetCrashCnt()}");
            Console.WriteLine($"AI 충돌 수 : {ai.GetCrashCnt()}");
        }
    }
}
