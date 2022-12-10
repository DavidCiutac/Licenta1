 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Licenta1.Contracts;
using Licenta1.Data;
using Licenta1.Models;
using Licenta1.Repository;

namespace Licenta1.Controllers
{
    public class StationsController : Controller
    {
        
        private readonly IStationsRepository stationsRepository;
        private readonly IMapper mapper;
       

        public StationsController(IStationsRepository stationsRepository, IMapper mapper)
        {
           
            this.stationsRepository = stationsRepository;
            this.mapper = mapper;
        }

        // GET: Stations
        public async Task<IActionResult> Index()
        {
            var model = mapper.Map<List<StationVM>>(await stationsRepository.GetAllAsync());
            return View(model);
                          
        }

        // GET: Stations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
           

            var station = await stationsRepository.GetAsync(id);
          
            if (station == null)
            {
                return NotFound();
            }
            var model = mapper.Map<StationVM>(station);
            return View(model);
        }
        
        // GET: Stations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StationVM stationVM)
        {
            if (ModelState.IsValid)
            {
                var station = mapper.Map<Station>(stationVM);
                var st=await stationsRepository.AddAsync(station);

                // return RedirectToAction("AddNeighbours", new RouteValueDictionary { new { Controller="GraphNetworkController",Action= "AddNeighbours"},station } );
                return RedirectToAction( "AddNeighbours", "GraphNetworks",new {id=station.Id}); 
            }
            return View(stationVM);
        }

        // GET: Stations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            

            var station = await stationsRepository.GetAsync(id);
            if (station == null)
            {
                return NotFound();
            }
            var stationVM=mapper.Map<StationVM>(station);
            return View(stationVM);
        }

        // POST: Stations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  StationVM stationVM)
        {
            if (id != stationVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var station1=mapper.Map<Station>(stationVM);
                    await stationsRepository.UpdateAsync(station1);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await stationsRepository.Exists(stationVM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(stationVM);
        }

        // GET: Stations/Delete/5
/*        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var station = await stationsRepository.GetAsync(id);
    
            if (station == null)
            {
                return NotFound();
            }
            var model = mapper.Map<StationVM>(station);
            return View(model);
        }
*/
        // POST: Stations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            await  stationsRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

       
    }
}
