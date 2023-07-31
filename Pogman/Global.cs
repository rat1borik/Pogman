using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;
using SFML.System;

using System.Collections.Concurrent;

namespace Pogman {

	static class Global {

		public const float COLLIDE_DISTANCE = 2;

		// Map
		static Map MainMap;

		// Window
		static RenderWindow window;
		static uint WindowWidth, WindowHeight;
		static Vector2f WindowCenter;

		// Debug info
		static string PreviousKey = "";
		static int PreviousKeyCount = 0;
		static string debugText = "";

		static void Main(string[] args) {
			PrepareWindow();

			// Load all resources temp shit
			var debugFont = new Font("font.otf");

			var debugInfo = new Text() {
				Font = debugFont,
				CharacterSize = 14,
				FillColor = Color.Green,
				Position = new Vector2f(WindowWidth - 200, 0)
			};

			MainMap = Map.CreateFromFile(@"map.bmp");

			while (window.IsOpen) {
				//debug
				debugText = String.Format("Key pressed: {0} : {1} times", PreviousKey, PreviousKeyCount);


				debugInfo.DisplayedString = debugText;
				debugInfo.Position = new Vector2f(WindowWidth - (debugText.Split('\n').OrderByDescending(s => s.Length).ToArray()[0].Length * (debugInfo.CharacterSize / 2)) - 20, 0);

				if (window.HasFocus()) {
					ProvideInput();
				}

				window.Clear(Color.Black);
				window.DispatchEvents();

				// Draw
#if DEBUG
				window.Draw(debugInfo);
#endif

				// Finally, display the rendered frame on screen
				window.Display();
			}


		}
		public static void ProvideInput() {
		}

		private static void PrepareWindow() {
			WindowWidth = VideoMode.DesktopMode.Width;
			WindowHeight = VideoMode.DesktopMode.Height;
			WindowCenter = new Vector2f(WindowWidth / 2, WindowHeight / 2);


			window = new RenderWindow(new SFML.Window.VideoMode(WindowWidth, WindowHeight), "Pogman", SFML.Window.Styles.None);

			window.SetVerticalSyncEnabled(true);
			window.SetMouseCursorVisible(false);
			window.KeyPressed += Window_KeyPressed;

			window.Closed += Window_Closed;
		}

		private static void Window_KeyPressed(object sender, SFML.Window.KeyEventArgs e) {
			var window = (Window)sender;
			if (PreviousKey != e.Code.ToString()) {
				PreviousKeyCount = 0;
			}
			PreviousKey = e.Code.ToString();
			PreviousKeyCount += 1;
			if (e.Code == Keyboard.Key.Escape) {
				window.Close();
			}
		}

		private static void Window_Closed(object sender, EventArgs e) {
			var window = (Window)sender;
			window.Close();
		}
	}
}