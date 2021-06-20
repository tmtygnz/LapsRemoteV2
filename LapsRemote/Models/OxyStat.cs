using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapsRemote.Vitals
{
	class OxyStat
	{
		public static int RandomOxyStat()
		{
			int rnd = new Random().Next(10, 50);
			return rnd;
		}
	}
}
