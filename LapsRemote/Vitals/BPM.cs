using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapsRemote.Vitals
{
	class BPM
	{
		public static int RandomBPM()
		{
			int rnd = new Random().Next(35, 37);
			return rnd;
		}
	}
}
