// CivOne
//
// To the extent possible under law, the person who associated CC0 with
// CivOne has waived all copyright and related or neighboring rights
// to CivOne.
//
// You should have received a copy of the CC0 legalcode along with this
// work. If not, see <http://creativecommons.org/publicdomain/zero/1.0/>.

using CivOne.Enums;
using CivOne.Interfaces;
using CivOne.Templates;

namespace CivOne.Units
{
	internal class Fighter : BaseUnit
	{
		public Fighter() : base(6, 4, 2, 10)
		{
			Class = UnitClass.Air;
			Type = Unit.Fighter;
			Name = "Fighter";
			RequiredTech = null;
			ObsoleteTech = null;
		}
	}
}