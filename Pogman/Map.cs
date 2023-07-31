using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Pogman {

	public enum MapElement {
		Empty = 0,
		Wall = 1
	}

	internal class Map {
		MapElement[,] _map;
		Player Player { get; }

		List<Enemy> Enemies { get; }

		private Map(uint x = 40, uint y = 40) {
			_map = new MapElement[x, y];
			Player = new Player();
			Enemies = new List<Enemy>();
		}

		public static Map CreateFromFile(string fileName) {
			if (!File.Exists(fileName)) {
				return null;
			}

			var fileMap = new Image(fileName);
			var map = new Map(fileMap.Size.X, fileMap.Size.Y);

			for (uint i = 0; i < fileMap.Size.X; i++) {
				for (uint j = 0; j < fileMap.Size.Y; j++) {
					var px = fileMap.GetPixel(i, j);
					map._map[i, j] = (MapElement)(px == Color.Black ? 0 : 1);
					
					if(px == Color.Red) map.Enemies.Add(new Enemy(new Vector2f(i, j)));
					else if(px == Color.Blue) map.Player.MapPosition = new Vector2f(i, j);
					
				}
			}

			return map;
		}
	}
}
