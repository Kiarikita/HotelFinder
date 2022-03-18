using HotelFinder.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelFinder.Business.Abstract
{
    public interface IHotelService
    {
        List<Hotel> GetAllHotels();

        Hotel GetHotelById(int id);

        Hotel GetHotelByName(string name);

        Hotel UpdateHotel(Hotel hotel);

        Hotel CreateHotel(Hotel hotel);

        void DeleteHotel(int id);
    }
}
