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
        public async Task<IActionResult> Get() 
        {
            var hotels = await _hotelService.GetAllHotels();
            return Ok(hotels); // StatusCode = 200 ve hotels(data) dönecek
        }

        /// <summary>
        /// Seçili Oteli Getirir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotel = await _hotelService.GetHotelById(id);
            if (hotel != null)
            {
                return Ok(hotel); //200
            }

            return NotFound(); //404
        }

        [HttpGet()]
        [Route("[action]/{name}")]
        public async Task<IActionResult> GetHotelByName(string name)
        {
            var hotel = await _hotelService.GetHotelByName(name);
            return Ok(hotel);

        }

        /// <summary>
        /// Otel Ekler
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> Post(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                var model = await _hotelService.CreateHotel(hotel);
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
        public async Task<IActionResult> Put(Hotel hotel)
        {
            if (await _hotelService.GetHotelById(hotel.Id) != null)
            {
                return Ok(await _hotelService.UpdateHotel(hotel));
            }

            return NotFound();
        }

        /// <summary>
        /// Oteli Siler
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _hotelService.GetHotelById(id) != null)
            {
                await _hotelService.DeleteHotel(id);
                return Ok();
            }

            return NotFound();

        }

    }
}
