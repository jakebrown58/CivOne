// CivOne
//
// To the extent possible under law, the person who associated CC0 with
// CivOne has waived all copyright and related or neighboring rights
// to CivOne.
//
// You should have received a copy of the CC0 legalcode along with this
// work. If not, see <http://creativecommons.org/publicdomain/zero/1.0/>.

using CivOne.Interfaces;

namespace CivOne.Civilizations
{
	internal class Mongol : ICivilization
	{
		public int Id
		{
			get
			{
				return 14;
			}
		}

		public string Name
		{
			get { return "Mongol"; }
		}

		public string NamePlural
		{
			get { return "Mongols"; }
		}

		public string LeaderName
		{
			get { return "Genghis Khan"; }
		}

		public byte PreferredPlayerNumber
		{
			get { return 7; }
		}

		public byte StartX
		{
			get { return 49; }
		}

		public byte StartY
		{
			get { return 19; }
		}
		
		public string[] CityNames
		{
			get
			{
				return new string[]
				{
					"Samarkand",
					"Bokhara",
					"Nishapur",
					"Karakorum",
					"Kashgar",
					"Tabriz",
					"Aleppo",
					"Kabul",
					"Ormuz",
					"Basra",
					"Khanbaryk",
					"Khorasan",
					"Shangtu",
					"Kazan",
					"Qyinsay",
					"Kerman"
				};
			}
		}
	}
}