using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using System.Web;

namespace EmoticonBot
{
    public static class Utils
    {
        private static string[] bad_words = File.ReadAllLines(HttpContext.Current.Server.MapPath("/App_Data/badwords.txt"));
        private static string[] hello_words = File.ReadAllLines(HttpContext.Current.Server.MapPath("/App_Data/hellowords.txt"));
        private static string[] goodbye_words = File.ReadAllLines(HttpContext.Current.Server.MapPath("/App_Data/byewords.txt"));
        private static string[] thanks_words = File.ReadAllLines(HttpContext.Current.Server.MapPath("/App_Data/thankswords.txt"));
        private static string[] help_words = File.ReadAllLines(HttpContext.Current.Server.MapPath("/App_Data/helpwords.txt"));
        private static string[] welcome_words = File.ReadAllLines(HttpContext.Current.Server.MapPath("/App_Data/welcomewords.txt"));
        private static string[] dontbedirty = File.ReadAllLines(HttpContext.Current.Server.MapPath("/App_Data/dontbedirty.txt"));
        private static string[] command_words = File.ReadAllLines(HttpContext.Current.Server.MapPath("/App_Data/commandwords.txt"));
        private static string[] dunno_words = File.ReadAllLines(HttpContext.Current.Server.MapPath("/App_Data/idunno.txt"));
        private static string[] stoplist = File.ReadAllLines(HttpContext.Current.Server.MapPath("/App_Data/stoplist.txt"));
        private static string[][] smile = File.ReadAllLines(HttpContext.Current.Server.MapPath("/App_Data/smiles.txt")).Select(s => s.Split('\t')).ToArray();

        public static string NextTo(this string[] str, string pat)
        {
            for (int i = 0; i < str.Length - 1; i++)
            {
                if (str[i] == pat) return str[i + 1];
            }
            return "";
        }

        public static bool IsPresent(this string[] str, string pat)
        {
            for (int i = 0; i < str.Length; i++)
                if (str[i] == pat)
                    return true;

            return false;
        }
        public static bool IsSwearWord(this string str)
        {
            for (int j = 0; j < bad_words.Length; j++)
            {
                if (str.Contains(bad_words[j])) return true;
            }
            return false;
        }
        public static bool IsHello(this string str)
        {
            for (int j = 0; j < hello_words.Length; j++)
            {
                if (str.Contains(hello_words[j])) return true;
            }
            return false;
        }
        public static bool IsGoodBye(this string str)
        {
            for (int j = 0; j < goodbye_words.Length; j++)
            {
                if (str.Contains(goodbye_words[j])) return true;
            }
            return false;
        }
        public static bool IsThanks(this string str)
        {
            for (int j = 0; j < thanks_words.Length; j++)
            {
                if (str.Contains(thanks_words[j])) return true;
            }
            return false;
        }
        public static bool IsCommand(this string str)
        {
            for (int j = 0; j < command_words.Length; j++)
            {
                if (str.Contains(command_words[j])) return true;
            }
            return false;
        }
        public static bool IsHelp(this string str)
        {
            for (int j = 0; j < help_words.Length; j++)
            {
                if (str.Contains(help_words[j])) return true;
            }
            return false;
        }
        public static string SayHello()
        {
            Random rnd = new Random();
            int i = rnd.Next(hello_words.Length);
            return hello_words[i];
        }
        public static string SayBye()
        {
            Random rnd = new Random();
            int i = rnd.Next(goodbye_words.Length);
            return goodbye_words[i];
        }
        public static string Dunno()
        {
            Random rnd = new Random();
            int i = rnd.Next(dunno_words.Length);
            return dunno_words[i];
        }
        public static string SayWelcome()
        {
            Random rnd = new Random();
            int i = rnd.Next(welcome_words.Length);
            return welcome_words[i];
        }
        public static string SayNoSwear()
        {
            Random rnd = new Random();
            int i = rnd.Next(dontbedirty.Length);
            return dontbedirty[i];
        }

        public static string Rephrase(string str)
        {
            
            StringBuilder sb = new StringBuilder();
            string result = "";
            bool flag=true;
            foreach (string word in str.Trim().Split())
            {


                result = word;
                for (int i = 0; i < smile.GetLength(0); i++)
                    for (int j = 1; j < smile[i].GetLength(0); j++)
                        if (flag && word.Contains(smile[i][j]) && !string.IsNullOrWhiteSpace(smile[i][j]))
                        {
                            result = word + " " + smile[i][0];
                            flag = false;
                        }


                sb.AppendFormat("{0} ", result);

            }
            if (flag)
                sb.AppendFormat("{0} ", ":-)");

            return sb.ToString();

        }


    }


}