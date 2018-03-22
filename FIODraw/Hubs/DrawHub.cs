﻿using Draw.Models;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Draw.Hubs
{
	public class DrawHub
		: Hub
	{
		private readonly List<Line> lines = new List<Line>();

		public Task Draw(int oldX, int oldY, int newX, int newY)
		{
			lines.Add(new Line(new Point(oldX, oldY), new Point(newX, newY), "COLOR"));
			return Clients.Others.SendAsync("draw", oldX, oldY, newX, newY);
		}

		public Task Clear()
		{
			lines.Clear();
			return Clients.Others.SendAsync("clear");
		}

		public Task Initialize() => Clients.Caller.SendAsync("initialize", lines);
	}
}