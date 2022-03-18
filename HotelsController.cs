using HotelFinder.Business.Abstract;
using HotelFinder.Business.Concrete;
using HotelFinder.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private IHotelService _hotelService;

        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        /// <summary>
        /// Tüm Otelleri Listeler
        /// </summary>
        /// <returns>List of Hotel döndürür.</returns>
        [HttpGet]
        public IActionResult Get() 
        {
            var hotels = _hotelService.GetAllHotels();
            return Ok(hotels); // StatusCode = 200 ve hotels(data) dönecek
        }

        /// <summary>
        /// Seçili Oteli Getirir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("[action]/{id}")]
        public IActionResult GetHotelById(int id)
        {
            var hotel = _hotelService.GetHotelById(id);
            if (hotel != null)
            {
                return Ok(hotel); //200
            }

            return NotFound(); //404
        }

        [HttpGet()]
        [Route("[action]/{name}")]
        public IActionResult GetHotelByName(string name)
        {
            var hotel = _hotelService.GetHotelByName(name);
            return Ok(hotel);

        }

        /// <summary>
        /// Otel Ekler
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPost()]
        public IActionResult Post(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                var model = _hotelService.CreateHotel(hotel);
                return CreatedAtAction("Get", new { id = model.Id}, model);
            }
            else
            {
                return BadRequest(ModelState);
            }
            
        }

        /// <summary>
        /// Oteli Günceller
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPut()]
        public IActionResult Put(Hotel hotel)
        {
            if (_hotelService.GetHotelById(hotel.Id) != null)
            {
                return Ok(_hotelService.UpdateHotel(hotel));
            }

            return NotFound();
        }

        /// <summary>
        /// Oteli Siler
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_hotelService.GetHotelById(id) != null)
            {
                _hotelService.DeleteHotel(id);
                return Ok();
            }

            return NotFound();

        }

    }
}
