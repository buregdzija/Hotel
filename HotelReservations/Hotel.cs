using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelReservations
{
	public class Hotel
	{
		public Hotel(int Size)
		{
			if (Size < 1 || Size > 1000)
				throw new ArgumentException("Please select value between 1 and 1000", Size.ToString()); 
			_Size = Size;
			_Rooms = new List<Room>();
		}

		// Prints all the booked days per each room
		public void PrintLedger()
		{
			foreach (var item in _Rooms)
			{
				Console.Write("Room " + item.RoomNumber + ": ");
				HashSet<int> set =item.BookedDays;
				foreach (int number in set)
				{
					Console.Write(number + " ");
				}
				Console.WriteLine();
			}
		}

		// Book days in a room if possible
		public bool BookRoom(int StartDate, int EndDate)
		{
			_NumberOfBookings++;
			// Check if chosen date interval is valid
			if (!ValidDate(StartDate, EndDate)) { return false; }

			// Generate interval of days based on chosen start and end dates
			HashSet<int> ChosenDays = GenerateDaysSet(StartDate, EndDate);

			// Find available room
			int AvailableRoomIndex = FindAvailableRoom(StartDate, EndDate);

			if (AvailableRoomIndex != -1)
			{
				Console.WriteLine(String.Format("Booking {0, -5} | Days: {1, -5} {2, -5} | Status: Accept  | Room: {3, -5}", _NumberOfBookings, StartDate, EndDate, _Rooms[AvailableRoomIndex].RoomNumber));
				return true;
			}
			else
			{
				Console.WriteLine(String.Format("Booking {0, -5} | Days: {1, -5} {2, -5} | Status: Decline", _NumberOfBookings, StartDate, EndDate));
				return false;
			}
		}

		private int FindAvailableRoom(int StartDate, int EndDate)
		{
			// Smallest gap is initially set to maximum amount of days available
			int SmallestGap = _MaximumDays;

			// Index of optimal candidate (0-999), -1 if there is no available room
			int RoomIndex = -1;

			HashSet<int> ChosenDays = GenerateDaysSet(StartDate, EndDate);

			// List of already booked rooms where we can possibly book optimal period of time
			List<Room> Candidates = FindCandidates(ChosenDays);

			// Find the smallest gap between the candidates
			foreach (Room candidate in Candidates)
			{
				int Gap = candidate.CalculateGap(StartDate, EndDate);

				if (Gap < SmallestGap)
				{
					SmallestGap = Gap;
					RoomIndex = candidate.RoomNumber - 1;
				}
			}

			// We book the chosen days in the optimal room
			if (RoomIndex != -1)
			{
				_Rooms[RoomIndex].BookedDays = ChosenDays;
			}

			// If there were no candidates, and there are empty rooms, we pick a new room to place the reservation
			if (RoomIndex == -1 && _Rooms.Count() < _Size)
			{
				RoomIndex = _Rooms.Count();
				Room room = new Room(RoomIndex + 1, ChosenDays);
				_Rooms.Add(room);
			}

			return RoomIndex;
		}

		// Function that finds the candidates amongst already used rooms and returns list of rooms
		private List<Room> FindCandidates(HashSet<int> ChosenDays)
		{
			List<Room> Candidates = new List<Room>();
			HashSet<int> BookedDays;

			foreach (Room room in _Rooms)
			{
				BookedDays = room.BookedDays;

				// If it is possible to book chosen period of days we put the room in the candidate list
				if (!BookedDays.Overlaps(ChosenDays))
				{
					Candidates.Add(room);
				}
			}

			return Candidates;
		}

		// Function that checks if entered date is valid
		private Boolean ValidDate(int StartDate, int EndDate)
		{
			if (StartDate < 0)
			{
				Console.WriteLine("Declined: Bad date " + StartDate);
				return false;
			}

			if (EndDate > 365)
			{
				Console.WriteLine("Declined: Bad date " + EndDate);
				return false;
			}

			if (StartDate > EndDate)
			{
				Console.WriteLine("Declined: Start Date > End Date ");
				return false;
			}

			return true;
		}

		// Function that generates set of days between start and end date
		private HashSet<int> GenerateDaysSet(int StartDate, int EndDate)
		{
			HashSet<int> days = new HashSet<int>();
			for (int k = StartDate; k <= EndDate; k++)
			{
				days.Add(k);
			}

			return days;
		}

		private int _NumberOfBookings;
		private static readonly int _MaximumDays = 366; 
		private readonly int _Size; // Number of rooms the Hotel has
		private List<Room> _Rooms; // List of used rooms in a Hotel
	};
};
