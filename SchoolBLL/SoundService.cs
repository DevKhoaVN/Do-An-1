using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using Spectre.Console;


namespace EntryLogManagement.SchoolBLL
{
    internal class SoundService
    {

        public void PlaySoundCamera()
        {

            try
            {
                using (var audioFile = new AudioFileReader("C:\\Users\\khoav\\OneDrive\\Tài liệu\\ADO.NET\\SESSION\\Camera\\Tiếng⧸âm thanh đồng xu (tiền) để ghép vào video ｜ Coin (money) sound effect for edit vi.wav"))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();

                    int IsCount = 0;       

                    while (outputDevice.PlaybackState == PlaybackState.Playing && IsCount <= 5)
                    {
                        IsCount++;
                        var key = Console.ReadLine();
                        if (key != "null")
                        {
                            break;
                        }
                    }

                    outputDevice.Stop();
                }

            }
            catch (Exception ex)
            {
                AnsiConsole.Markup("[red]Lỗi âm thanh : " + ex.Message);
                AnsiConsole.WriteLine();
            }
        }
        public void PlaySoundLog()
        {

            try
            {
                using (var audioFile = new AudioFileReader("C:\\Users\\khoav\\OneDrive\\Tài liệu\\ADO.NET\\SESSION\\Mail\\Alter.wav"))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();

                    int IsCount = 0;

                    while (outputDevice.PlaybackState == PlaybackState.Playing && IsCount <= 10)
                    {
                       
                        IsCount++;

                        var key = Console.ReadLine();
                        if (key != "null")
                        {
                            break;
                        }
                    }


                    outputDevice.Stop();
                }

            }
            catch (Exception ex)
            {
                AnsiConsole.Markup("[red]Lỗi âm thanh :[/] " + ex.Message);
                AnsiConsole.WriteLine();
            }
        }
    }
}
