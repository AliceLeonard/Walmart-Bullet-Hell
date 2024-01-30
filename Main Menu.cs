using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class MainMenu
    {
        private Button startButton;
        private Button exitButton;
        private GameObject[] children;
        private int childCount;

        public MainMenu()
        {
            children = new GameObject[10]; // Adjust the size based on your needs
            childCount = 0;

            CreateMainMenu();
        }

        void CreateMainMenu()
        {
            // Set up the background
            EasyDraw background = new EasyDraw(800, 600);
            background.Fill(255, 255, 255); // White background
            AddChild(background);

            // Create Start button
            startButton = new Button("Start", 200, 100);
            startButton.SetXY((800 - startButton.width) / 2, 600 / 3);
            startButton.OnButtonClick += StartButtonClick;
            AddChild(startButton);

            // Create Exit button
            exitButton = new Button("Exit", 200, 100);
            exitButton.SetXY((800 - exitButton.width) / 2, 2 * 600 / 3);
            exitButton.OnButtonClick += ExitButtonClick;
            AddChild(exitButton);
        }

        void StartButtonClick()
        {
            // Start the game when the "Start" button is clicked
            Console.WriteLine("Start button clicked");
            // Add code to start the game or transition to the game screen
        }

        void ExitButtonClick()
        {
            // Exit the program when the "Exit" button is clicked
            Console.WriteLine("Exit button clicked");
            //Environment.Exit(0);
        }

        void AddChild(GameObject child)
        {
            if (childCount < children.Length)
            {
                children[childCount] = child;
                childCount++;
            }
            else
            {
                Console.WriteLine("Error: Maximum number of children reached.");
            }
        }

        public void Update()
        {
            // Add any update logic here if needed
        }
    }
}