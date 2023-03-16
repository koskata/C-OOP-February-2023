using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using _04.BorderControl.Core.Interfaces;
using _04.BorderControl.Models;
using _04.BorderControl.Models.Interfaces;

namespace _04.BorderControl.Core
{
    public class Engine : IEngine
    {
        public void Run()
        {
            List<string> Ids = new List<string>();

            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] cmdArgs = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (cmdArgs.Length == 3)
                {
                    ICitizen citizen = new Citizen(cmdArgs[0], int.Parse(cmdArgs[1]), cmdArgs[2]);

                    Ids.Add(citizen.Id);
                }
                else if (cmdArgs.Length == 2)
                {
                    IRobot robot = new Robot(cmdArgs[0], cmdArgs[1]);

                    Ids.Add(robot.Id);
                }
            }

            string magicNum = Console.ReadLine();

            List<string> list = ValidIds(magicNum, Ids);

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }


        }

        private List<string> ValidIds(string magicNum, List<string> Ids)
        {
            List<string> list = new List<string>();

            int lengthOfMagicNum = magicNum.Length;

            foreach (var id in Ids)
            {

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < lengthOfMagicNum; i++)
                {
                    char digit = id[id.Length - i - 1];
                    sb.Append(digit);
                }

                StringBuilder number = new StringBuilder();
                for (int i = sb.Length - 1; i >= 0; i--)
                {
                    string temp = sb[i].ToString();
                    number.Append(temp);
                    
                }

                if (number.ToString() == magicNum)
                {
                    list.Add(id);
                }
            }

            return list;
        }
    }
}
