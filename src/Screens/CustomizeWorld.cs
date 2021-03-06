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
using CivOne.GFX;
using CivOne.Templates;

namespace CivOne.Screens
{
	internal class CustomizeWorld : BaseScreen
	{
		private int _landMass = -1, _temperature = -1, _climate = -1, _age = -1;
		private bool _hasUpdate = true;
		
		private int GetMenuWidth(string title, string[] items)
		{
			int i = 0;
			Picture[] texts = new Picture[items.Length + 1];
			texts[i++] = Resources.Instance.GetText(" " + title, 0, 15);
			foreach (string item in items)
				texts[i++] = Resources.Instance.GetText(" " + item, 0, 5);
			return (texts.Select(t => t.Width).Max()) + 6;
		}
		
		private Menu CreateMenu(int y, string title, EventHandler setChoice, params string[] menuTexts)
		{
			Menu menu = new Menu(Canvas.Image.Palette.Entries)
			{
				Title = title,
				X = 203,
				Y = y,
				Width = GetMenuWidth(title, menuTexts),
				TitleColour = 15,
				ActiveColour = 11,
				TextColour = 79,
				DisabledColour = 8,
				FontId = 0
			};
			
			Menu.Item menuItem;
			for (int i = 0; i < menuTexts.Length; i++)
			{
				menu.Items.Add(menuItem = new Menu.Item(menuTexts[i], i));
				menuItem.Selected += setChoice;
			}
			menu.ActiveItem = 1;
			return menu;
		}
		
		private void SetLandMass(object sender, EventArgs args)
		{
			Console.WriteLine("Customize World - Land Mass: {0}", _landMass);
			_landMass = (sender as Menu.Item).Value;
			_hasUpdate = true;
		}
		
		private void SetTemperature(object sender, EventArgs args)
		{
			Console.WriteLine("Customize World - Temperature: {0}", _temperature);
			_temperature = (sender as Menu.Item).Value;
			_hasUpdate = true;
		}
		
		private void SetClimate(object sender, EventArgs args)
		{
			Console.WriteLine("Customize World - Climate: {0}", _climate);
			_climate = (sender as Menu.Item).Value;
			_hasUpdate = true;
		}
		
		private void SetAge(object sender, EventArgs args)
		{
			Console.WriteLine("Customize World - Age: {0}", _age);
			_age = (sender as Menu.Item).Value;
			_hasUpdate = true;
		}
		
		public override bool HasUpdate(uint gameTick)
		{
			if (!_hasUpdate) return false;
			
			if (_landMass < 0) AddMenu(CreateMenu(6, "LAND MASS:", SetLandMass, "Small", "Normal", "Large"));
			else if (_temperature < 0) AddMenu(CreateMenu(56, "TEMPERATURE:", SetTemperature, "Cool", "Temperate", "Warm"));
			else if (_climate < 0) AddMenu(CreateMenu(106, "CLIMATE:", SetClimate, "Arid", "Normal", "Wet"));
			else if (_age < 0) AddMenu(CreateMenu(156, "AGE:", SetAge, "3 billion years", "4 billion years", "5 billion years"));
			else
			{
				Destroy();
				Map.Generate(_landMass, _temperature, _climate, _age);
				Common.AddScreen(new Intro());
			}
			
			_hasUpdate = false;
			return true;
		}
		
		public CustomizeWorld()
		{
			Cursor = MouseCursor.Pointer;
			
			Picture background = Resources.Instance.LoadPIC("CUSTOM");
			
			_canvas = new Picture(320, 200, background.Image.Palette.Entries);
			AddLayer(background, 0, 0);
		}
	}
}