using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System;


namespace tubes
{
    class FileOpener
    {
        protected string[] m_filePattern = { ".h", ".cpp", ".c", ".txt", ".pas", ".cs", ".html", ".ada", ".css", ".hs" };
        public string GetContent(string file_name) {
            return File.ReadAllText(file_name);
        }
    }
}
