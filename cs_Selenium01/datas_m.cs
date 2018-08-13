using System.Collections.Generic;
using System.Linq;

namespace cs_Selenium01
{
    static class datas_m
    {
        static string fname_mails = "mails.csv";
        static string fname_settings = "settings.csv";

        static public List<List<string>> mails = mails_read(fname_mails);
        static public List<List<string>> settings = mails_read(fname_settings);

        static public List<List<string>> mails_read(string fname)
        {
            List<List<string>>  tmpl = new List<List<string>>();
           System.IO.StreamReader file = new System.IO.StreamReader(fname);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                tmpl.Add(line.Split(';').ToList<string>());
            }
            file.Close();
            return tmpl;
        }
    }
}
