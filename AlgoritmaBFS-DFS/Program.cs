using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;


public class AlgoritmaBFS_DFS
{
    class Program
    {
        static string[] Pattern = { ".h", ".cpp", ".c", ".txt", ".pas", ".cs", ".html", ".ada", ".css", ".hs" };

        static int count_found = 0;
        static double progress_dfs = 0.0;
        static double progress_bfs = 0.0;

        static void DFS(string[] inp_list, string inp_dir, string inp_find, double inp_weight)
        {
            //hitung pembagian progress bar
            double w_dir = 0, w_file = 0;
            string[] file_list = Directory.GetFiles(@inp_dir, "*.*");
            if (inp_list.Length + file_list.Length > 0)
            {
                w_dir = inp_weight * (double)inp_list.Length / (double)(inp_list.Length + file_list.Length);
                w_file = inp_weight * (double)file_list.Length / (double)(inp_list.Length + file_list.Length);
            }
            else
            {
                progress_dfs += inp_weight;
            }
            if (inp_list.Length > 0)
            {
                foreach (string m_list in inp_list)
                {
                    try
                    {
                        string[] sub_list = Directory.GetDirectories(@m_list);
                        DFS(sub_list, m_list, inp_find, (double)w_dir / inp_list.Length);
                    }
                    catch (Exception e)
                    {
                        progress_dfs += (double)w_dir / inp_list.Length;
                        Console.WriteLine("{0}", progress_dfs * 100);
                        //Console.WriteLine("Direktori {0} tidak bisa diakses.",m_list);
                    }

                }
            }

            List<string> file_text = new List<string>();
            foreach (string m_list in file_list)
            {
                if (Pattern.Contains<string>(System.IO.Path.GetExtension(m_list)))
                {
                    file_text.Add(m_list);
                }
                else
                {
                    progress_dfs += (double)w_file / file_list.Length; //masih belum optimal, harusnya untuk tiap file, bukan hanya file teks
                    Console.WriteLine("{0}", progress_dfs);
                }
            }
            foreach (string m_list in file_text)
            {
                progress_dfs += (double)w_file / file_list.Length; //masih belum optimal, harusnya untuk tiap file, bukan hanya file teks
                Console.WriteLine("{0}", progress_dfs);
                string file_cont = File.ReadAllText(m_list);
                int i = file_cont.IndexOf(inp_find);
                if (i < 0)
                {
                    //Kata tidak ketemu
                }
                else
                {
                    int p_start = 0;
                    int p_finish = 0;
                    Console.WriteLine("{0}", m_list);	//Tambahin ngeluarin index star highlight /baa
                    if (i - 5 < 0)
                    {
                        p_start = 0;
                    }
                    else
                    {
                        p_start = i - 6;
                    }
                    if (i + inp_find.Length + 5 > file_cont.Length)
                    {
                        p_finish = file_cont.Length;
                    }
                    else
                    {
                        p_finish = i + inp_find.Length + 5;
                    }
                    for (int j = p_start; j < p_finish; j++)
                    {
                        Console.Write("{0}", file_cont[j]);
                    }
                    Console.WriteLine();
                    count_found++;
                }
            }
        }

        static void BFS(string inp_dir, string inp_find)
        {
            Queue<Tuple<string, double>> Q_dir = new Queue<Tuple<string, double>>();
            var temp = new Tuple<string, double>(inp_dir, 1.0);
            Q_dir.Enqueue(temp);
            Queue<string> Q_file = new Queue<string>();
            do
            {
                int qfile_size;
                double w_dir = 0, w_file = 0;
                temp = Q_dir.Dequeue();
                string cur_dir = temp.Item1;
                //Console.WriteLine("{0}", cur_dir); //DEBUG
                string[] dir_list = Directory.GetDirectories(@cur_dir);
                string[] file_list = Directory.GetFiles(@cur_dir, "*.*");
                if (dir_list.Length + file_list.Length > 0)
                {
                    w_dir = temp.Item2 * (double)dir_list.Length / (double)(dir_list.Length + file_list.Length);
                    w_file = temp.Item2 * (double)file_list.Length / (double)(dir_list.Length + file_list.Length);
                }
                else
                {
                    progress_bfs += temp.Item2;
                }
                if (dir_list.Length > 0)
                {
                    foreach (string m_list in dir_list)
                    {
                        try
                        {
                            string[] decoy = Directory.GetDirectories(@m_list); //decoy untuk memancing exception
                            var tem = new Tuple<string, double>(m_list, (double)w_dir / dir_list.Length);
                            Q_dir.Enqueue(tem);
                        }
                        catch (Exception e)
                        {
                            progress_bfs += (double)w_dir / dir_list.Length; //masih belum optimal, harusnya untuk tiap file, bukan hanya file teks
                            //Console.WriteLine("{0}", progress_bfs);
                            //Console.WriteLine("Direktori {0} tidak bisa diakses.",cur_dir);
                        }
                    }
                }
                foreach (string m_list in file_list)
                {
                    if (Pattern.Contains<string>(System.IO.Path.GetExtension(m_list)))
                    {
                        Q_file.Enqueue(m_list);
                    }
                    else
                    {
                        progress_bfs += (double)w_file / file_list.Length; //masih belum optimal, harusnya untuk tiap file, bukan hanya file teks
                        //Console.WriteLine("{0}", progress_bfs);
                    }
                }
                qfile_size = Q_file.Count;
                while (Q_file.Count != 0)
                {
                    progress_bfs += (double)w_file / file_list.Length; //masih belum optimal, harusnya untuk tiap file, bukan hanya file teks
                    //Console.WriteLine("{0}", progress_bfs);
                    string cur_file = Q_file.Dequeue();
                    string file_cont = File.ReadAllText(cur_file);
                    int i = file_cont.IndexOf(inp_find);
                    if (i < 0)
                    {
                        //Kata tidak ketemu
                    }
                    else
                    {
                        int p_start = 0;
                        int p_finish = 0;
                        Console.WriteLine("{0}", cur_file);	//Tambahin ngeluarin index highlight //baa
                        if (i - 5 < 0)
                        {
                            p_start = 0;
                        }
                        else
                        {
                            p_start = i - 6;
                        }
                        if (i + inp_find.Length + 5 > file_cont.Length)
                        {
                            p_finish = file_cont.Length;
                        }
                        else
                        {
                            p_finish = i + inp_find.Length + 5;
                        }
                        for (int j = p_start; j < p_finish; j++)
                        {
                            Console.Write("{0}", file_cont[j]);
                        }
                        Console.WriteLine();
                        count_found++;
                    }
                }
            } while (Q_dir.Count != 0);
        }

        static void Main(string[] args)
        {

            string str = "kami plagiat google";
            Console.WriteLine("{0}", str);

            string inp_find = Console.ReadLine();
            int Index = str.IndexOf(inp_find);
            if (Index < 0)
            {
                Console.WriteLine("not found");
            }
            else
            {
                Console.WriteLine("Found at {0}", Index);
            }
            string inputus = Console.ReadLine();

            string[] dirr = Directory.GetDirectories(@inputus);
            Console.WriteLine("DFS");
            DFS(dirr, inputus, inp_find, 1.0);
            Console.WriteLine("{0} {1}", count_found, progress_dfs);
            count_found = 0;
            Console.WriteLine("BFS");
            BFS(inputus, inp_find);
            Console.WriteLine("{0} {1}", count_found, progress_bfs);

            string a = Console.ReadLine();
        }
    }
}
