using System;

namespace HotelReservations
{
	class Program
	{
		static void Main(string[] args)
		{
			int size;
			Hotel hotel;

			size = 1001;
			// Under assumption that size of the hotel can't be less than 1 or higher than 1000 we throw exception in constructor
			// Other way would be to check the size before passing it to constructor
			try
			{
				hotel = new Hotel(size);

				hotel.BookRoom(0, 0);
				hotel.BookRoom(0, 2);
				hotel.BookRoom(2, 4);
				hotel.BookRoom(2, 2);

				hotel.PrintLedger();
			}
			catch (ArgumentException e)
			{
				Console.WriteLine("{0}: {1}", e.GetType().Name, e.Message);
			}

			size = 2;
			try
			{
				hotel = new Hotel(size);


				hotel.BookRoom(10, 10);
				hotel.BookRoom(0, 2);
				hotel.BookRoom(2, 4);
				hotel.BookRoom(2, 2);

				hotel.PrintLedger();
			}
			catch (ArgumentException e)
			{
				Console.WriteLine("{0}: {1}", e.GetType().Name, e.Message);
			}


		}
	}
}
