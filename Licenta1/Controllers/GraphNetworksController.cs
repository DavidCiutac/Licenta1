 using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Licenta1.Contracts;
using Licenta1.Data;
using Licenta1.Models;
using static System.Collections.Specialized.BitVector32;

namespace Licenta1.Controllers
{
    public class GraphNetworksController : Controller
    {
        private readonly IGraphNetworkRepository graphNetworkRepository;
        private readonly IStationsRepository stationsRepository;
        private readonly IMapper mapper;

        public GraphNetworksController(IGraphNetworkRepository graphNetworkRepository, IMapper mapper, IStationsRepository stationsRepository)
        {
            this.graphNetworkRepository = graphNetworkRepository;
            this.mapper = mapper;
            this.stationsRepository = stationsRepository;
          
        }

        // GET: GraphNetworks
        public async Task<IActionResult> Index()
        {
            var model = mapper.Map<List<GraphNetworkVM>>(await graphNetworkRepository.GetGraphNetworkAsync1());
            return View(model);
        }

        // GET: GraphNetworks/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var graphNetwork=await graphNetworkRepository.GetGraphNetworkAsync(id);
          
            if (graphNetwork == null)
            {
                return NotFound();
            }
            
            var model=mapper.Map<GraphNetworkDetailsVM>(graphNetwork);
            return View(model);
        }

        // GET: GraphNetworks/Create
        public async Task<IActionResult> Create()
        {
         
            var model = new GraphNetworkCreateVM
            {
                Station1= new SelectList(await stationsRepository.GetAllAsync(), "Id", "Name"),
                Station2 = new SelectList(await stationsRepository.GetAllAsync(), "Id", "Name")
            };
        return View(model);
        }

        // POST: GraphNetworks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( GraphNetworkCreateVM graphNetworkVM)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var model = mapper.Map<GraphNetwork>(graphNetworkVM);
                    model.Station1 = await stationsRepository.GetAsync(model.Station1_Id);
                    model.Station2 = await stationsRepository.GetAsync(model.Station2_Id);
          
                    await graphNetworkRepository.AddAsync(model);
                    
                    return RedirectToAction(nameof(Index));
                }
                
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An Error Has Occurred. Please Try Again Later");
            }

            graphNetworkVM.Station1 = new SelectList(await stationsRepository.GetAllAsync(), "Id", "Name", graphNetworkVM.Station1_Id);
            graphNetworkVM.Station2 = new SelectList(await stationsRepository.GetAllAsync(), "Id", "Name", graphNetworkVM.Station2_Id);


            return View(graphNetworkVM);
        }

        // GET: GraphNetworks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var graphNetwork = await graphNetworkRepository.GetAsync(id);
            if (graphNetwork == null)
            {
                return NotFound();
            }
            var model = new GraphNetworkCreateVM
            {
                Station1 = new SelectList(await stationsRepository.GetAllAsync(), "Id", "Name"),
                Station2 = new SelectList(await stationsRepository.GetAllAsync(), "Id", "Name")
            };
        //    model = mapper.Map<GraphNetworkVM>(graphNetwork);
            return View(model);
        }

        // POST: GraphNetworks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GraphNetworkCreateVM graphNetworkVM)
        {
            if (id != graphNetworkVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var model = mapper.Map<GraphNetwork>(graphNetworkVM);
                    model.Station1 = await stationsRepository.GetAsync(model.Station1_Id);
                    model.Station2 = await stationsRepository.GetAsync(model.Station2_Id);
                   
                    await graphNetworkRepository.UpdateAsync(model);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await graphNetworkRepository.Exists(graphNetworkVM.Id))
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
            graphNetworkVM.Station1 = new SelectList(await stationsRepository.GetAllAsync(), "Id", "Name", graphNetworkVM.Station1_Id);
            graphNetworkVM.Station2 = new SelectList(await stationsRepository.GetAllAsync(), "Id", "Name", graphNetworkVM.Station2_Id);
            return View(graphNetworkVM);
        }

        // GET: GraphNetworks/Delete/5

        public async Task<IActionResult> Delete(int? id)
        {

            var graphNetwork = await graphNetworkRepository.GetAsync(id);
            if (graphNetwork == null)
            {
                return NotFound();
            }
            var model=mapper.Map<GraphNetworkDetailsVM>(graphNetwork);

            return View(model);
        }

        // POST: GraphNetworks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await graphNetworkRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddNeighbours(int id)
        {
            var station = await stationsRepository.GetAsync(id);
            StationVM stationModel = mapper.Map<StationVM>(station);
            var model = new GraphNetworkAddNeighbourVM
            {
                Station1 = stationModel,
                Station1_Id = stationModel.Id,
                Station2 = new SelectList(await stationsRepository.GetAllAsync(), "Id", "Name")
            };
            return View(model);
        }



        public async Task<IActionResult> Create1(GraphNetworkAddNeighbourVM graphNetworkAddNeighbourVM)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    var model = mapper.Map<GraphNetwork>(graphNetworkAddNeighbourVM);
                    model.Station1 = await stationsRepository.GetAsync(model.Station1_Id);
                    model.Station2 = await stationsRepository.GetAsync(model.Station2_Id);
                    await graphNetworkRepository.AddAsync(model);
                    

                    return RedirectToAction("AddNeighbours", new { id = model.Station1_Id });
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An Error Has Occurred. Please Try Again Later");
            }

          
            graphNetworkAddNeighbourVM.Station2 = new SelectList(await stationsRepository.GetAllAsync(), "Id", "Name", graphNetworkAddNeighbourVM.Station2_Id);


            return View("Home",graphNetworkAddNeighbourVM);
        }

    }
}
