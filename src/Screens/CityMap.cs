// CivOne
//
// To the extent possible under law, the person who associated CC0 with
// CivOne has waived all copyright and related or neighboring rights
// to CivOne.
//
// You should have received a copy of the CC0 legalcode along with this
// work. If not, see <http://creativecommons.org/publicdomain/zero/1.0/>.

using System;
using System.Linq;
using CivOne.Enums;
using CivOne.Events;
using CivOne.GFX;
using CivOne.Interfaces;
using CivOne.Templates;

namespace CivOne.Screens
{
	internal class CityMap : BaseScreen
	{
		private readonly City _city;

		private readonly Picture _background;
		
		private bool _update = true;
		
		public event EventHandler MapUpdate;

		private void DrawResources(ITile tile, int x, int y)
		{
			int count = (tile.Food + tile.Shield + tile.Trade);

			if (count == 0)
			{
				AddLayer(Icons.Unhappy, x + 4, y + 4);
				return;
			}

			int iconsPerLine = 2;
			int iconWidth = 8;
			if (count > 4) iconsPerLine = (int)Math.Ceiling((double)count / 2);
			if (iconsPerLine == 3) iconWidth = 4;
			if (iconsPerLine >= 4) iconWidth = 2;

			for (int i = 0; i < count; i++)
			{
				Picture icon;
				if (i >= tile.Food + tile.Shield) icon = Icons.Trade;
				else if (i >= tile.Food) icon = Icons.Shield;
				else icon = Icons.Food; 

				int xx = (x + ((i % iconsPerLine) * iconWidth));
				int yy = (y + (((i - (i % iconsPerLine)) / iconsPerLine) * 8));
				AddLayer(icon, xx, yy);
			}
		}
		
		public override bool HasUpdate(uint gameTick)
		{
			if (_update)
			{
				_canvas.FillLayerTile(_background);
				_canvas.AddBorder(1, 1, 0, 0, 82, 82);
				_canvas.FillRectangle(0, 82, 0, 2, 82);
				
				ITile[,] tiles = _city.CityRadius;
				for (int xx = 0; xx < 5; xx++)
				for (int yy = 0; yy < 5; yy++)
				{
					ITile tile = tiles[xx, yy];
					if (tile == null) continue;
					AddLayer(Resources.Instance.GetTile(tile), (xx * 16) + 1, (yy * 16) + 1);
					if (tile.City != null)
					{
						AddLayer(Icons.City(tile.City, smallFont: true), (xx * 16) + 1, (yy * 16) + 1);
					}
					else if (tile.Units.Any(u => u.Owner != _city.Owner))
					{
						IUnit[] units = tile.Units.Where(u => u.Owner != _city.Owner).ToArray();
						AddLayer(units[0].GetUnit(units[0].Owner), (xx * 16) + 1, (yy * 16) + 1);
						if (units.Length > 1)
							AddLayer(units[0].GetUnit(units[0].Owner), (xx * 16), (yy * 16));
					}
					if (!Settings.RevealWorld)
					{
						if (!Human.Visible(tile, Direction.West)) AddLayer(Resources.Instance.GetFog(Direction.West), (xx * 16) + 1, (yy * 16) + 1);
						if (!Human.Visible(tile, Direction.North)) AddLayer(Resources.Instance.GetFog(Direction.North), (xx * 16) + 1, (yy * 16) + 1);
						if (!Human.Visible(tile, Direction.East)) AddLayer(Resources.Instance.GetFog(Direction.East), (xx * 16) + 1, (yy * 16) + 1);
						if (!Human.Visible(tile, Direction.South)) AddLayer(Resources.Instance.GetFog(Direction.South), (xx * 16) + 1, (yy * 16) + 1);
					}

					if (_city.OccupiedTile(tile))
					{
						_canvas.FillRectangle(12, (xx * 16) + 1, (yy * 16) + 1, 16, 1);
						_canvas.FillRectangle(12, (xx * 16) + 1, (yy * 16) + 2, 1, 14);
						_canvas.FillRectangle(12, (xx * 16) + 1, (yy * 16) + 16, 16, 1);
						_canvas.FillRectangle(12, (xx * 16) + 16, (yy * 16) + 2, 1, 14);
					}

					if (_city.ResourceTiles.Contains(tile))
						DrawResources(tile, (xx * 16) + 1, (yy * 16) + 1);
				}
				
				_update = false;
			}
			return true;
		}

		public void Update()
		{
			_update = true;
		}
		
		public override bool MouseDown(ScreenEventArgs args)
		{
			if (args.X < 1 || args.X > 81 || args.Y < 1 || args.Y > 81) return false;
			int tileX = (int)Math.Floor(((double)args.X - 1) / 16);
			int tileY = (int)Math.Floor(((double)args.Y - 1) / 16);
			_city.SetResourceTile(_city.CityRadius[tileX, tileY]);
			_update = true;
			if (MapUpdate != null) MapUpdate(this, null);
			return true;
		}

		public void Close()
		{
			Destroy();
		}

		public CityMap(City city, Picture background)
		{
			_city = city;
			_background = background;

			_canvas = new Picture(84, 82, background.Palette);
		}
	}
}