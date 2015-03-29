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
    class DFS:FileOpener
    {
        public DFS(){  //Default constructor
            m_progress = 0.0;
            m_resultNumber = 0;
            m_result = new Queue<Tuple<string, string, int>>();
        }
        public void Execute(string inp_dir,string inp_find){    //Execute the DFS
            try
            {
                string[] inp_dir_list = Directory.GetDirectories(@inp_dir);
                Search(inp_dir_list, inp_dir, inp_find, 1.0);
                Console.WriteLine("{0}", m_resultNumber);
                while (m_result.Count != 0)
                {
                    var temp = m_result.Dequeue();
                    Console.WriteLine("{0}~^{1}", temp.Item1, temp.Item2);
                }
            }
            catch (Exception e) {
                //Console.WriteLine("Direktori {0} tidak bisa diakses.",inp_dir);
            }
        }
        protected void Search(string[] inp_list, string inp_dir, string inp_find, double inp_weight){   //Recursive DFS searching
            double w_dir = 0, w_file = 0;
            string[] file_list = Directory.GetFiles(@inp_dir, "*.*");
            
            if (inp_list.Length + file_list.Length > 0){
                w_dir = inp_weight * (double)inp_list.Length / (double)(inp_list.Length + file_list.Length);
                w_file = inp_weight * (double)file_list.Length / (double)(inp_list.Length + file_list.Length);
            }else{
                m_progress += inp_weight;
            }
            if (inp_list.Length > 0){
                foreach (string m_list in inp_list){
                    try{
                        string[] sub_list = Directory.GetDirectories(@m_list);
                        Search(sub_list, m_list, inp_find, (double)w_dir / inp_list.Length);
                    }
                    catch (Exception e){
                        m_progress += (double)w_dir / inp_list.Length;

                        //Console.WriteLine("{0}", m_progress * 100);

                        //Console.WriteLine("Direktori {0} tidak bisa diakses.",m_list);
                    }
                }
            }
            List<string> supported_file = new List<string>();
            foreach (string m_list in file_list){
                if (m_filePattern.Contains<string>(System.IO.Path.GetExtension(m_list))){
                    supported_file.Add(m_list);
                }else{
                    //File not supported
                    m_progress += (double)w_file / file_list.Length;
                    //Console.WriteLine("{0}", m_progress);
                }
            }
            foreach (string m_list in supported_file){
                string file_cont = GetContent(m_list);
                int i = file_cont.IndexOf(inp_find);
                if (i < 0){
                    //Kata tidak ketemu
                }else{
                    int p_start = 0;
                    int p_finish = 0;
                    string quote="";
                    if (i - 10 <= 0){
                        p_start = 0;
                    }else{
                        p_start = i - 11;
                    }
                    if (i + inp_find.Length + 10 > file_cont.Length){
                        p_finish = file_cont.Length;
                    }else{
                        p_finish = i + inp_find.Length + 10;
                    }
                    for (int j = p_start; j < p_finish; j++){
                        quote+=file_cont[j];
                        quote = quote.Replace(System.Environment.NewLine, " ");
                        quote = quote.Replace("\r\n", " ");
                        quote = quote.Replace("\n", " ");

                    }
                    var temp = new Tuple<string, string, int>(m_list, quote, i);
                    m_result.Enqueue(temp);
                    m_progress += (double)w_file / file_list.Length; //masih belum optimal, harusnya untuk tiap file, bukan hanya file teks
                    //Console.WriteLine("{0}", m_progress);

                    m_resultNumber++;
                }
            }
        }
        public int GetResultNumber(){   //Getter for result number
            return m_resultNumber;
        }
        static private int m_resultNumber;
        static private double m_progress;
        static private Queue<Tuple<string, string, int>> m_result;    
    }  
}
