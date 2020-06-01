using System;
using HotelReservations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HotelReservations.UnitTests
{
	[TestClass]
	public class HotelTests
	{
		[TestMethod]
		public void RequestDenied_OutOfRange_Size1()
		{
			//Arrange
			int size = 1;
			var hotel = new Hotel(size);

			//Act
			bool result1 = hotel.BookRoom(-4, 2);
			bool result2 = hotel.BookRoom(200, 400);

			// Assert
			Assert.AreEqual(false, result1);
			Assert.AreEqual(false, result2);
		}

		[TestMethod]
		public void RequestsAccepted_Size3()
		{
			//Arrange
			int size = 3;
			var hotel = new Hotel(size);

			//Act
			bool result1 = hotel.BookRoom(0, 5);
			bool result2 = hotel.BookRoom(7, 13);
			bool result3 = hotel.BookRoom(3, 9);
			bool result4 = hotel.BookRoom(5, 7);
			bool result5 = hotel.BookRoom(6, 6);
			bool result6 = hotel.BookRoom(0, 4);

			// Assert
			Assert.AreEqual(true, result1);
			Assert.AreEqual(true, result2);
			Assert.AreEqual(true, result3);
			Assert.AreEqual(true, result4);
			Assert.AreEqual(true, result5);
			Assert.AreEqual(true, result6);
		}

		[TestMethod]
		public void RequestsDeclined_Size3()
		{
			//Arrange
			int size = 3;
			var hotel = new Hotel(size);

			//Act
			bool result1 = hotel.BookRoom(1, 3);
			bool result2 = hotel.BookRoom(2, 5);
			bool result3 = hotel.BookRoom(1, 9);
			bool result4 = hotel.BookRoom(0, 15);

			// Assert
			Assert.AreEqual(true, result1);
			Assert.AreEqual(true, result2);
			Assert.AreEqual(true, result3);
			Assert.AreEqual(false, result4);
		}

		[TestMethod]
		public void RequestsAcceptedAfterDecline_Size3()
		{
			//Arrange
			int size = 3;
			var hotel = new Hotel(size);

			//Act
			bool result1 = hotel.BookRoom(1, 3);
			bool result2 = hotel.BookRoom(0, 15);
			bool result3 = hotel.BookRoom(1, 9);
			bool result4 = hotel.BookRoom(2, 5);

			// Assert
			Assert.AreEqual(true, result1);
			Assert.AreEqual(true, result2);
			Assert.AreEqual(true, result3);
			Assert.AreEqual(false, result4);
			Assert.AreEqual(true, hotel.BookRoom(4, 9));
		}

		[TestMethod]
		public void ComplexRequests_Size2()
		{
			//Arrange
			int size = 2;
			var hotel = new Hotel(size);

			//Act
			bool result1 = hotel.BookRoom(1, 3);
			bool result2 = hotel.BookRoom(0, 4);
			bool result3 = hotel.BookRoom(2, 3);
			bool result4 = hotel.BookRoom(5, 5);
			bool result5 = hotel.BookRoom(4, 10);
			bool result6 = hotel.BookRoom(10, 10);
			bool result7 = hotel.BookRoom(6, 7);
			bool result8 = hotel.BookRoom(8, 10);
			bool result9 = hotel.BookRoom(8, 9);

			// Assert
			Assert.AreEqual(true, result1);
			Assert.AreEqual(true, result2);
			Assert.AreEqual(false, result3);
			Assert.AreEqual(true, result4);
			Assert.AreEqual(true, result5);
			Assert.AreEqual(true, result6);
			Assert.AreEqual(true, result7);
			Assert.AreEqual(false, result8);
			Assert.AreEqual(true, result9);

		}
	}
}
