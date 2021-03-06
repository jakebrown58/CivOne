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
	internal class Egyptian : ICivilization
	{
		public int Id
		{
			get
			{
				return 4;
			}
		}

		public string Name
		{
			get { return "Egyptian"; }
		}

		public string NamePlural
		{
			get { return "Egyptians"; }
		}

		public string LeaderName
		{
			get { return "Ramesses"; }
		}

		public byte PreferredPlayerNumber
		{
			get { return 4; }
		}

		public byte StartX
		{
			get { return 41; }
		}

		public byte StartY
		{
			get { return 24; }
		}
		
		public string[] CityNames
		{
			get
			{
				return new string[]
				{
					"Thebes",
					"Memphis",
					"Oryx",
					"Heliopolis",
					"Gaza",
					"Alexandria",
					"Byblos",
					"Cairo",
					"Coptos",
					"Edfu",
					"Pithom",
					"Busirus",
					"Athribus",
					"Mendes",
					"Tanis",
					"Abydos"
				};
			}
		}
	}
}