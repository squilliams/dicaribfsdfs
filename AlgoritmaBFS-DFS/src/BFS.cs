using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;


namespace tubes{
    class BFS : FileOpener{
        public BFS() {  //Default constructor
            m_progress = 0.0;
            m_resultNumber = 0;
            m_result = new Queue<Tuple<string, string, int>>();
        }
        public void Execute(string inp_dir, string inp_find){    //Execute the BFS
            try{
                Search(inp_dir, inp_find);
                Console.WriteLine("{0}", m_resultNumber);
                while (m_result.Count != 0)
                {
                    var temp = m_result.Dequeue();
                    Console.WriteLine("{0}~{1}~{2}", temp.Item1, temp.Item2, temp.Item3);
                }
            }
            catch(Exception e) {
                //Console.WriteLine("Direktori {0} tidak bisa diakses.",inp_dir);
            }
        }
        protected void Search(string inp_dir, string inp_find){   //Iterative BFS searching
            Queue<Tuple<string, double>> Q_dir = new Queue<Tuple<string, double>>();
            var temp = new Tuple<string, double>(inp_dir, 1.0);
            Q_dir.Enqueue(temp);
            Queue<string> Q_file = new Queue<string>();
            do {
                int qfile_size;
                double w_dir = 0, w_file = 0;
                temp = Q_dir.Dequeue();
                string cur_dir = temp.Item1;
                //Console.WriteLine("{0}", cur_dir); //DEBUG
                string[] dir_list = Directory.GetDirectories(@cur_dir);
                string[] file_list = Directory.GetFiles(@cur_dir, "*.*");
                if (dir_list.Length + file_list.Length > 0){
                    w_dir = temp.Item2 * (double)dir_list.Length / (double)(dir_list.Length + file_list.Length);
                    w_file = temp.Item2 * (double)file_list.Length / (double)(dir_list.Length + file_list.Length);
                } else{
                    m_progress += temp.Item2;
                }
                if (dir_list.Length > 0){
                    foreach (string m_list in dir_list){
                        try{
                            string[] decoy = Directory.GetDirectories(@m_list); //decoy untuk memancing exception
                            var tem = new Tuple<string, double>(m_list, (double)w_dir / dir_list.Length);
                            Q_dir.Enqueue(tem);
                        }
                        catch (Exception e){
                            m_progress += (double)w_dir / dir_list.Length;
                            Console.WriteLine("{0}", m_progress);
                            //Console.WriteLine("Direktori {0} tidak bisa diakses.",cur_dir);
                        }
                    }
                }
                List<string> supported_file = new List<string>();
                foreach (string m_list in file_list) {
                    if (m_filePattern.Contains<string>(System.IO.Path.GetExtension(m_list))){
                        supported_file.Add(m_list);
                    }else{
                        //File not supported
                        m_progress += (double)w_file / file_list.Length;
                        Console.WriteLine("{0}", m_progress);
                    }
                }
                foreach (string m_list in supported_file){
                    string file_cont = GetContent(m_list);
                    int i = file_cont.IndexOf(inp_find);
                    if (i < 0) {
                        //Kata tidak ketemu
                    } else {
                        int p_start = 0;
                        int p_finish = 0;
                        string quote = "";
                        if (i - 5 < 0) {
                            p_start = 0;
                        } else{
                            p_start = i - 6;
                        }
                        if (i + inp_find.Length + 5 > file_cont.Length) {
                            p_finish = file_cont.Length;
                        }else {
                            p_finish = i + inp_find.Length + 5;
                        }
                        for (int j = p_start; j < p_finish; j++) {
                            quote += file_cont[j];
                        }
                        var temp1 = new Tuple<string, string, int>(m_list, quote, i);
                        m_result.Enqueue(temp1);
                        m_progress += (double)w_file / file_list.Length; //masih belum optimal, harusnya untuk tiap file, bukan hanya file teks
                        Console.WriteLine("{0}", m_progress);
                        m_resultNumber++;
                    }
                }
            } while (Q_dir.Count != 0);
        }
        public int GetResultNumber() {   //Getter for result number
            return m_resultNumber;
        }
        static private int m_resultNumber;
        static private double m_progress;
        static private Queue<Tuple<string, string, int>> m_result;
    }
}
