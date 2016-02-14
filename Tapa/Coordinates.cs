using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tapa
{
	class Coordinates
	{
		// 向き
		public static readonly Coordinates ZERO = new Coordinates(0, 0);
		public static readonly Coordinates LEFT = new Coordinates(0, -1);
		public static readonly Coordinates RIGHT = new Coordinates(0, 1);
		public static readonly Coordinates UP = new Coordinates(-1, 0);
		public static readonly Coordinates DOWN = new Coordinates(1, 0);

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

		public static Coordinates operator+ (Coordinates aaa, Coordinates bbb)
		{
			return new Coordinates(aaa.x + bbb.x, aaa.y + bbb.y);
		}

		public static Coordinates operator- (Coordinates aaa, Coordinates bbb)
		{
			return new Coordinates(aaa.x - bbb.x, aaa.y - bbb.y);
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType()) { return false; }

			Coordinates co = (Coordinates)obj;
			return (this.x == co.x) && (this.y == co.y);
		}

		public override int GetHashCode()
		{
			return x ^ y;
		}

		// 出力用
		public void printCoordinates() {
			Console.Write(" (" + this.x + "," + this.y + ") ");
		}
	}
}
