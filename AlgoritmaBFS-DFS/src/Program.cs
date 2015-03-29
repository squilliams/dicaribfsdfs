using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace tubes
{
    public class AlgoritmaBFS_DFS
    {
        class Program
        {
            static void Main(string[] args)
            {

                //string str = "kami plagiat google";
                //Console.WriteLine("{0}", str);
                string inp_find = args[0];
                string inputus = args[1];
                string Choice = args[2];
                //string inp_find=Console.ReadLine();
                //string inputus = Console.ReadLine();
                //string Choice = Console.ReadLine();
                
                if (Choice.Equals("DFS"))
                {
                    DFS dfs = new DFS();
                    dfs.Execute(inputus, inp_find);
                }
                else if (Choice.Equals("BFS"))
                {
                    BFS bfs = new BFS();
                    bfs.Execute(inputus,inp_find);
                }
                //string inputusa = Console.ReadLine();
            }
        }
    }
}