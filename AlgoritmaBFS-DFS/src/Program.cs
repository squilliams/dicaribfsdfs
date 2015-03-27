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

                string str = "kami plagiat google";
                Console.WriteLine("{0}", str);
                string inp_find = Console.ReadLine();
                string inputus = Console.ReadLine();
                DFS dfs = new DFS();
                BFS bfs = new BFS();               
                //dfs.Execute(inputus,inp_find);
                bfs.Execute(inputus,inp_find);
                Console.WriteLine("masuk cons");
                string inputusa = Console.ReadLine();
            }
        }
    }
}