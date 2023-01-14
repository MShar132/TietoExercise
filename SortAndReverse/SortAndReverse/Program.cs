using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using SortAndReverse.Tests;
using SortAndReverse.BusinessLogic.Utils.Interfaces;
using SortAndReverse.BusinessLogic.Utils;

namespace SortAndReverse
{
    class Program
    {
        private static ILog log;

        public static void Main()
        {
            string file = string.Empty;
            string logPath = string.Empty;
            string revPath = string.Empty;
            string singlesPath = string.Empty;

            FileUtil util = new FileUtil();

            try
            {
                // Load configuration
                IConfiguration config = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json")
                                    .Build();

                // Get values from the config given their key 
                file = config.GetSection("Settings:InputFilePath").Value;
                logPath = config.GetSection("Settings:LogFilePath").Value;
                revPath = config.GetSection("Settings:ReversedFilePath").Value;
                singlesPath = config.GetSection("Settings:SinglesFilePath").Value;
                


                if (!string.IsNullOrEmpty(logPath))
                {
                    string logfilename = logPath + String.Format("\\Log_{0}.log", DateTime.Today.ToString("yyyyMMdd"));
                    log = LogManager.GetLogger(logfilename);

                }
                else
                {
                    Console.WriteLine("Please configure log path correctly in appsetting config file.");
                }

                if (string.IsNullOrEmpty(file) || string.IsNullOrEmpty(logPath) || string.IsNullOrEmpty(revPath) || string.IsNullOrEmpty(singlesPath))
                {
                    log.Error("Please check if InputFilePath/LogFilePath/OutputFilePath have been configured correctly");
                }
                else
                {

                    log.Info("InputFilePath/LogFilePath/OutputFilePath have been read from appsettings.");
                }
            }
            catch (Exception e)
            {
                log.Error("Please check if application configuration is done correctly. Exception: " + e.Message);

            }
            try
            {
                if (file != null)
                {
                    log.Info("Attempting to read the input file. " + file);
                    string[] words = util.ReadFile(file); //read the file to be sorted/reversed

                    if (words.Length > 0)
                    {
                         
                        log.Info("File Read. Attempting to sort the file.");
                        string[] sortedWords = util.Sort(words); // return sorted text
                        log.Info("File sorted. Attempting to reverse the file.");
                        char[][] reverseChars = util.Reverse(sortedWords); //return reversed text


                        if (revPath != null)
                        {
                            util.WriteCharArrayToFile(reverseChars, revPath); //write the sorted and reversed text to file
                        }
                        log.Info("Output file created (sorted and reversed). " + revPath);

                        log.Info("Creating Singles file");
                        string[] sorted = util.SortWithoutDuplicates(words); // return sorted text without duplicates
                        if (singlesPath != null)
                        {
                            util.WriteStringArrayToFile(sorted, singlesPath); //write the output from above to file
                        }
                        log.Info("Output file created (sorted and duplicates removed). " + singlesPath);
                    }
                }

                //Running unit test with mock input
                string[] mockInput = new string[] { "Asd", "Bce", "Aes" };
                string[] sortedOutput = UnitTest.TestSort(mockInput);

                bool pass = false;

                if (sortedOutput[0] == "Aes" && sortedOutput[1] == "Asd" && sortedOutput[2] == "Bce")
                {
                    pass = true;
                }

                Console.WriteLine("TestSort execution passed: " + pass);
            }
      
            catch (Exception e)
            {
                log.Error("Exception raised; this may be due to missing input file, incorrect Input/Output Path specified or issue in sorting/reversing logic . Exact Exception message is:  " + e.Message + "Stack Trace: " + e.StackTrace);
            }


            finally
            {
                log.Dispose();
            }


        }

       
    }

}