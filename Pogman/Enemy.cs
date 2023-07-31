using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pogman {
	internal class Enemy {
		public Vector2f MapPosition {
			get; set;
		}

		public Enemy(Vector2f mapPosition) {
			MapPosition = mapPosition;
		}
	}
}
