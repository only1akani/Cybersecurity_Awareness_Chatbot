using System;
using System.Drawing;
using System.Media;

namespace Cybersecurity_Awareness_Chatbot
{
    public class WelcomeScreen
    {
        //Auto get the path directory
        private string full_path = AppDomain.CurrentDomain.BaseDirectory;
        public WelcomeScreen()
        {
            //Calling sound method
            PlayGreetingSound();

            //Calling logo method
            DisplayAsciiLogo();
        }

        //Method to play sound
        private void PlayGreetingSound()
        { 
            //Check if the path is auto collected
            Console.WriteLine(full_path);

            //Replacing the \bin\Debug  
            string correct_path = full_path.Replace(@"\bin\Debug\", @"\greet.wav");

            //Check if audio found
            Console.WriteLine(correct_path);

            //Use the soundPlay class to play the audio
            //Creating an instance for the soundPlay class
            //With an object name greet
            SoundPlayer greet = new SoundPlayer(correct_path);

            //Play the sound using the play method
            greet.Play();

        }
        //Method to turn logo into ascii
        private void DisplayAsciiLogo()
        {
            //Path of the logo
            string path = full_path.Replace(@"\bin\Debug\", @"\logo.jpg");

            Bitmap image = new Bitmap(path);

            //Resize for better console fit
            int width = 100;
            int height = 70; //(image.Height * width) / image.Width;
            Bitmap resized = new Bitmap(image, new Size(width, height));

            //Colour for asci
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            //Asci characters
            string asciiChars = "@#S%?*+;:,. ";

            //Start by the height
            for (int y = 0; y < resized.Height; y++)
            {
                //then width
                for (int x = 0; x < resized.Width; x++)
                {
                    //Colour the pixel on x and y
                    Color pixel = resized.GetPixel(x, y);

                    //Convert to grayscale
                    int gray = (pixel.R + pixel.G + pixel.B) / 3;

                    //Map grayscale to Asci
                    int index = (gray * (asciiChars.Length - 1)) / 255;

                    Console.Write(asciiChars[index]);
                }
                Console.WriteLine();
            }
        }
    }
}