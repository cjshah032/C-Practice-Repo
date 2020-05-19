using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TaskA
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] v = Directory.GetFiles("C:\\DC++ (Shared)\\TV SERIES\\Suits\\Season 10");
            Episode[] eps = new Episode[v.Length/2];
            int j = 0;
            foreach (string filen in v)
            {
                string name1 = Path.GetFileName(filen);
                string Extension = Path.GetExtension(filen);
                if (Extension == ".srt")
                {
                    string[] separator = new string[]{" - ", "x","."};
                    string[] names = name1.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    eps[j] = new Episode(int.Parse(names[2]), names[3], names[0], int.Parse(names[1]));
                    string NewfileName = eps[j].CreateFileName(Extension); 
                    Directory.SetCurrentDirectory(Path.GetDirectoryName(filen));
                    File.Copy(name1, NewfileName);
                    File.Delete(filen);
                    j++;
                }

                else if(Extension == ".mp4")
                {
                    string[] names = name1.Split(' ');
                    int index = names.Length - 1;
                    string[] seps = new string[] { "S", "E", "."};
                    string[] Seas_Eps = names[index].Split(seps, StringSplitOptions.RemoveEmptyEntries);
                    for(int r=0; r<j; r++)
                    {
                        if (eps[r].IsSameEps(int.Parse(Seas_Eps[0]), int.Parse(Seas_Eps[1])))
                        {
                            string NewName = eps[r].CreateFileName(Extension);
                            Directory.SetCurrentDirectory(Path.GetDirectoryName(filen));
                            File.Copy(name1, NewName);
                            File.Delete(filen);
                            break;
                        }
                        else continue;
                    }
                }
                
            }
        }

        class Episode
        {
            int EpisodeNo;
            string SeriesName, EpisodeName;
            int SeasonNo;

            public Episode(int EpNo, string EpName, string Series, int Season)
            {
                EpisodeNo = EpNo;
                EpisodeName = EpName;
                SeriesName = Series;
                SeasonNo = Season;
            }

            public void DisplayInfo()
            {
                Console.WriteLine("Info Regarding object of the class Episode");
                Console.WriteLine($"Series: {SeriesName}, Season: {SeasonNo}");
                Console.WriteLine($"Episode No {EpisodeNo}, {EpisodeName}");
                Console.WriteLine("----------------------------------------------------------------------------------------------");
            }

            public string CreateFileName(string Extension)
            {
                string fileName = string.Format("{0} - S{1:d2}E{2:d2} - {3}{4}", SeriesName, SeasonNo, EpisodeNo, EpisodeName, Extension); 
                return fileName;
            }
            
            public bool IsSameEps(int Season, int EpNo)
            {
                if (Season == SeasonNo && EpNo == EpisodeNo)
                    return true;
                return false;
            }
        }
    }
}
