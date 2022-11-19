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

namespace Licenta1.Controllers
{
    public class TrainRoutesController : Controller
    {
        
        private readonly ITrainRouteRepository trainRouteRepository;
        private readonly IMapper mapper;
        private readonly INodeRepository nodeRepository;
        private readonly IStationsRepository stationsRepository;
        

        public TrainRoutesController( IMapper mapper, ITrainRouteRepository trainRouteRepository, INodeRepository nodeRepository, IStationsRepository stationsRepository)
        {
            
            this.trainRouteRepository = trainRouteRepository;
            this.mapper = mapper;
            this.nodeRepository = nodeRepository;
            this.stationsRepository = stationsRepository;
        }

        // GET: TrainRoutes
        public async Task<IActionResult> Index()
        {
            var model = mapper.Map<List<TrainRouteVM>>(await trainRouteRepository.GetAllAsync());
           return View(model);
        }

        // GET: TrainRoutes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var model=await trainRouteRepository.GetAsync(id);
            if(model == null)
            {
                return NotFound();
            }
            var model1 = mapper.Map<TrainRouteVM>(model);
            return View(model1);
        }

        // GET: TrainRoutes/Create
        public async Task<IActionResult> Create()
        {
            
            var model = new TrainRouteCreateVM
            {
                CurrentStation = new SelectList(await stationsRepository.GetAllAsync(), "Id", "Name")
            };
            
            return View(model);
        }
        public async Task<IActionResult> Create1(TrainRouteCreateVM model)
        {
            model = await trainRouteRepository.CreateNeighbourList1(model);
            if (model == null)
                return View("Create");

            return View("Create",model);
        }
     

        // POST: TrainRoutes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrainRouteCreateVM trainRouteCreateVM)
        {
            if (!ModelState.IsValid)
            {


                var  trainRoute=mapper.Map<TrainRoute>(trainRouteCreateVM);
                trainRoute.RStations = trainRouteCreateVM.Rute;
                await trainRouteRepository.AddAsync(trainRoute);
              
                return RedirectToAction(nameof(Index));
            }
            trainRouteCreateVM.CurrentStation = new SelectList(await stationsRepository.GetAllAsync(), "Id", "Name");

            return View(trainRouteCreateVM);
        }

        // GET: TrainRoutes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var trainRoute=await trainRouteRepository.GetAsync(id);
            if (trainRoute == null)
            {
                return NotFound();
            }
            return View(trainRoute);
        }

        // POST: TrainRoutes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RStations")] TrainRoute trainRoute)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await trainRouteRepository.UpdateAsync(trainRoute);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! (await TrainRouteExistsAsync(trainRoute.Id)))
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
            return View(trainRoute);
        }

        // GET: TrainRoutes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
        
            var trainRoute = await trainRouteRepository.GetAsync(id);
            if (trainRoute == null)
            {
                return NotFound();
            }
            var model=mapper.Map<TrainRouteVM>(trainRoute);

            return View(model);
        }

        // POST: TrainRoutes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           
            var trainRoute = await trainRouteRepository.GetAsync(id);
            if (trainRoute != null)
            {
               
                await trainRouteRepository.DeleteAsync(id);
            }
            
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TrainRouteExistsAsync(int id)
        {
            return await trainRouteRepository.Exists(id);
        }
    }
}
