using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservations
{
	class Room
	{
		public Room(int Number, HashSet<int> BookedDays)
		{
			RoomNumber = Number;
			_BookedDays = BookedDays;
		}

		// Function that calculates the gap between booked days and chosen days
		public int CalculateGap(int StartDate, int EndDate)
		{
			int LeftNeighbor = _MinNeighbor;
			int RightNeighbor = _MaxNeighbor;
			int LeftGap;
			int RightGap;
			int Gap;

			foreach (int day in _BookedDays)
			{
				if (day < StartDate && day > LeftNeighbor)
				{
					LeftNeighbor = day;
				}

				if (day > EndDate && day < RightNeighbor)
				{
					RightNeighbor = day;
				}

			}

			LeftGap = StartDate - LeftNeighbor;
			RightGap = RightNeighbor - EndDate;

			Gap = LeftGap < RightGap ? LeftGap : RightGap;

			return Gap;
		}

		private static readonly int _MinNeighbor = -1;
		private static readonly int _MaxNeighbor = 366;

		public int RoomNumber { get; }
		private readonly HashSet<int> _BookedDays;
		public HashSet<int> BookedDays { get { return _BookedDays; } set { _BookedDays.UnionWith(value); } }
	}
}
