using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System;
using System.Runtime.InteropServices.ComTypes;
 

namespace tubes
{
    class FileOpener
    {
        protected string[] m_filePattern = { ".h", ".cpp", ".c", ".txt", ".pas", ".cs", ".html", ".ada", ".css", ".hs",".java",".htm",".doc",".docx" };
        public string GetContent(string file_name) {
            return File.ReadAllText(file_name);
        }
        /*
        public string ReadDOCX(string file_name) {
            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            object miss = System.Reflection.Missing.Value;
            object path = @file_name;
            object readOnly = true;
            Microsoft.Office.Interop.Word.Document docs = word.Documents.Open(ref path, ref miss, ref readOnly, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss);
            string result = "";
            for (int i = 0; i < docs.Paragraphs.Count; i++)
            {
                result += " \r\n " + docs.Paragraphs[i + 1].Range.Text.ToString();
            }
            docs.Close();
            word.Quit();
            return result;
        }
        */
    }
}
