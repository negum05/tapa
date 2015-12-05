using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tapa
{
	class Coordinates
	{
		public int x { get; set; }	// x座標
		public int y { get; set; }	// y座標

		public Coordinates()
		{
			this.x = -100;
			this.y = -100;
		}

		public Coordinates(int a, int b)
		{
			this.x = a;
			this.y = b;
		}

		public Coordinates(Coordinates co)
		{
			this.x = co.x;
			this.y = co.y;
		}
		
		// 出力用
		public void printCoordinates() {
			Console.Write(" (" + this.x + "," + this.y + ") ");
		}
	}
}
