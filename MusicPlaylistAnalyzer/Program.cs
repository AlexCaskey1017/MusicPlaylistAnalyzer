using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace MusicPlaylistAnalyzer
{
    class Name
    {
        public string name;
        public string artist;
        public string album;
        public string genre;
        public int size;
        public int time;
        public int year;
        public int plays;

        public Name(string name, string artist, string album, string genre, int size, int time, int year, int plays)
        {
            this.name = name;
            this.artist = artist;
            this.album = album;
            this.genre = genre;
            this.size = size;
            this.time = time;
            this.year = year;
            this.plays = plays;

        }
    }

    class Program

    {
        static void Main(string[] args)

        {
            //Console.WriteLine("what do you want your file name to be?: ");

            //string input = Console.ReadLine();
            //string fileName = input + ".txt";

            if (args.Length != 2)

            {
                Console.WriteLine("File name has to be at least two letters long");
                return;
            }

            string fileName = args[0];
            string outputName = args[1];

            using (var reader = new StreamReader(fileName))
            {

                List<Name> listA = new List<Name>();
                bool isFirst = true;
                while (!reader.EndOfStream)

                {
                    var line = reader.ReadLine();
                    var values = line.Split('\t');
                    if (isFirst)

                    {
                        isFirst = false;
                        continue;
                    }

                    if (isFirst == false)

                    {
                        var song = new Name(values[0], values[1], values[2], values[3], Int32.Parse(values[4]), Int32.Parse(values[5]), Int32.Parse(values[6]), Int32.Parse(values[7]));



                        listA.Add(song);
                    }

                }

                string report = null;
                int i;

                try
                {
                    if (File.Exists(fileName) == false)
                    {
                        Console.WriteLine("Welcome to Music playlist analyzer!");
                    }
                    else
                    {
                        StreamReader sr = new StreamReader(fileName);
                        i = 0;
                        string line = sr.ReadLine();
                        while ((line = sr.ReadLine()) != null)
                        {
                            i = i + 1;
                            try
                            {
                                string[] strings = line.Split('\t');

                                if (strings.Length < 8)
                                {
                                    Console.Write("Report is holding too many elements.");
                                    Console.WriteLine($"Row {i} contains {strings.Length}  values. It should contain less than 8.");
                                    break;
                                }

                            }
                            catch
                            {
                                Console.Write("The file is having trouble being written.");
                                break;
                            }
                        }
                        sr.Close();
                    }
                }


                catch (Exception)
                {
                    Console.WriteLine("The file is having trouble being opened.");
                }


                try
                {
                    Name[] songs = listA.ToArray();
                    using (StreamWriter write = new StreamWriter(outputName))
                    {
                        write.WriteLine("Music Playlist Report\n");


                        //#1

                        var Plays = from musicStats in listA where musicStats.plays >= 200 select musicStats.name;

                        report += "\nSongs with 200 or more plays: \n \n";
                        foreach (string name in Plays)
                        {
                            report += name + "\n";
                        }

                        //#2

                        Console.WriteLine("");

                        var SongsGenreAlternative = from musicStats in listA where musicStats.genre == "Alternative" select musicStats;
                        i = 0;
                        foreach (Name musicStats in SongsGenreAlternative)

                        {
                            i++;
                        }

                        report += $"\nHow many songs are in the playlist with the Genre of 'Alternative'?: {i}\n \n";

                        //#3

                        Console.WriteLine("");

                        var SongsGenreHipHop = from musicStats in listA where musicStats.genre == "Hip-Hop/Rap" select musicStats;
                        i = 0;
                        foreach (Name musicStats in SongsGenreHipHop)

                        {
                            i++;
                        }

                        report += $"\nHow many songs are in the playlist with the Genre of 'Hip-Hop/Rap'?: {i}\n \n";

                        //#4

                        Console.WriteLine("");

                        var WelcomeToFish = from musicStats in listA where musicStats.album == "Welcome to the Fishbowl" select musicStats.name;

                        report += "\nWhat songs are in the playlist from the album Welcome to the Fishbowl?: \n \n";
                        foreach (string song in WelcomeToFish)
                        {
                            report += song + "\n";
                        }

                        //#5

                        Console.WriteLine("");

                        var BeforeSeventy = from musicStats in listA where musicStats.year < 1970 select musicStats.name;

                        report += "\nWhat are the songs in the playlist from before 1970?: \n \n";
                        foreach (string song in BeforeSeventy)
                        {
                            report += song + "\n";
                        }

                        //#6

                        Console.WriteLine("");

                        var MoreThanEightyFive = from musicStats in listA where musicStats.name.Length > 85 select musicStats.name;

                        report += "\nWhat are the song names that are more than 85 characters long?: \n \n";
                        foreach (string name in MoreThanEightyFive)
                        {
                            report += name + "\n";
                        }

                        //#7

                        Console.WriteLine("");

                        var LongestSong = from musicStats in listA orderby musicStats.time descending select musicStats.name;

                        report += "\nWhat is the longest song?: \n \n";
                        {
                            report += LongestSong.First();
                        }

                        write.Write(report);
                        write.Close();
                    }
                    Console.WriteLine("The report has been generated in the file" + outputName);
                }


                catch (Exception)
                {
                    Console.WriteLine("File can not be written or opened make sure you are following: dotnet MusicPlaylistAnalyzer.dll <music_playlist_file_path> <report_file_path>");
                }

                Console.ReadLine();
            }
        }
    }
}
