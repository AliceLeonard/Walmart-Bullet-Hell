using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class RestartMenu : GameObject
    {
        private Game game;
        private Button startButton;
        private Button exitButton;
        private GameObject[] children;
        private int childCount;

        public RestartMenu(Game game) : base()
        {
            children = new GameObject[10]; // Adjust the size based on your needs
            this.game = game;
            childCount = 0;

            CreateRestartMenu();
            this.game = game;
        }

        void CreateRestartMenu()
        {
            // Set up the background
            EasyDraw background = new EasyDraw(game.width, game.height);
            background.Fill(0, 0, 0);
            background.Rect(0, 0, game.width, game.height);
            AddChild(background);

            // Create Start button
            startButton = new Button("Restart", 200, 100);
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

